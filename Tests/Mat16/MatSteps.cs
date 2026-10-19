using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorks.Pickle;
using RimWorld;
using Verse;
using Verse.AI;

namespace AMJ.Culture.MatFeasibility16
{
    // Disposable 1.6 research patch. Only explicitly registered Bedroll instances
    // are considered. The actual Culture product DLL/Defs remain unchanged.
    [HarmonyPatch(typeof(Toils_Ingest), "CarryIngestibleToChewSpot",
        new Type[] { typeof(Pawn), typeof(TargetIndex) })]
    public static class OwnedMatMealPoC
    {
        public static readonly HashSet<Building_Bed> Mats = new HashSet<Building_Bed>();
        public static readonly Dictionary<Pawn, Building_Bed> Picked =
            new Dictionary<Pawn, Building_Bed>();
        public static int OwnerSelectionChecks;
        public static int OtherOwnerRejected;
        public static int RoomMoodLookups;
        public static int ComparedOrdinarySeats;

        // Use actual loaded thoughts/room stats, never irori label or guessed modifiers.
        public static float ActualMealMood(IntVec3 cell, Map map)
        {
            Room room = cell.GetRoom(map);
            if (room == null) return 0f;
            int stage = RoomStatDefOf.Impressiveness.GetScoreStageIndex(
                room.GetStat(RoomStatDefOf.Impressiveness));
            if (stage < 0 || stage >= ThoughtDefOf.AteInImpressiveDiningRoom.stages.Count)
                return 0f;
            var thought = ThoughtDefOf.AteInImpressiveDiningRoom.stages[stage];
            RoomMoodLookups++;
            return thought == null ? 0f : Math.Max(0f, thought.baseMoodEffect);
        }

        public static bool HigherBenefit(float matMood, float seatMood,
            float matComfort, float seatComfort, float matDistance, float seatDistance)
        {
            if (matMood != seatMood) return matMood > seatMood;
            if (matComfort != seatComfort) return matComfort > seatComfort;
            return matDistance <= seatDistance;
        }

        [HarmonyPriority(Priority.Last)]
        public static void Postfix(Pawn pawn, TargetIndex ingestibleInd, ref Toil __result)
        {
            if (__result == null || __result.initAction == null) return;
            Action prior = __result.initAction;
            Toil travel = __result;
            travel.initAction = delegate
            {
                Pawn actor = travel.actor;
                if (actor == null || !actor.Spawned || actor.CurJob == null ||
                    actor.CurJob.def != JobDefOf.Ingest ||
                    actor.ownership == null)
                {
                    prior(); return;
                }
                Building_Bed own = actor.ownership.OwnedBed;
                if (own == null || !Mats.Contains(own))
                {
                    OtherOwnerRejected++;
                    prior(); return;
                }
                OwnerSelectionChecks++;
                Thing food = actor.CurJob.GetTarget(ingestibleInd).Thing;
                Map map = actor.Map;
                IntVec3 cell = own.Position;
                if (food == null || food.def.ingestible == null ||
                    food.def.ingestible.chairSearchRadius <= 0f ||
                    !own.Spawned || own.Map != map ||
                    own.ForPrisoners || own.Medical ||
                    own.def.building.isSittable ||
                    !cell.Standable(map) ||
                    own.IsForbidden(actor) || own.IsBurning() ||
                    own.HostileTo(actor) || !own.IsSociallyProper(actor) ||
                    (own.def.CanHaveFaction && own.Faction != actor.Faction) ||
                    !actor.CanReserve(own, 1, -1, null, false) ||
                    !actor.CanReserveSittableOrSpot(cell) ||
                    cell.GetDangerFor(actor, map) != Danger.None ||
                    (cell - actor.Position).LengthHorizontal >
                        food.def.ingestible.chairSearchRadius)
                {
                    prior(); return;
                }
                // The comparison is called only for an actual meal Job and
                // only searches eligible chairs inside the existing radius.
                Thing usual = GenClosest.ClosestThingReachable(
                    actor.Position, map,
                    ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial),
                    PathEndMode.OnCell,
                    TraverseParms.For(actor, Danger.Deadly, TraverseMode.ByPawn, false),
                    food.def.ingestible.chairSearchRadius,
                    candidate => candidate.def.building != null &&
                        candidate.def.building.isSittable &&
                        candidate.Spawned && candidate.Position.Standable(map) &&
                        !candidate.IsForbidden(actor) &&
                        !candidate.IsBurning() &&
                        !candidate.HostileTo(actor) &&
                        candidate.IsSociallyProper(actor) &&
                        actor.CanReserve(candidate, 1, -1, null, false) &&
                        actor.CanReserveSittableOrSpot(candidate.Position) &&
                        candidate.Position.GetDangerFor(actor,map) == Danger.None);

                if (usual != null)
                {
                    ComparedOrdinarySeats++;
                    if (!HigherBenefit(
                         ActualMealMood(cell,map), ActualMealMood(usual.Position,map),
                         own.GetStatValue(StatDefOf.Comfort),
                         usual.GetStatValue(StatDefOf.Comfort),
                         (cell-actor.Position).LengthHorizontal,
                         (usual.Position-actor.Position).LengthHorizontal))
                    {
                        prior(); return;
                    }
                }
                if (!actor.ReserveSittableOrSpot(cell, actor.CurJob, false))
                {
                    prior(); return;
                }
                map.pawnDestinationReservationManager.Reserve(actor, actor.CurJob, cell);
                actor.pather.StartPath(cell, PathEndMode.OnCell);
                Picked[actor] = own;
                Log.Message("AMJ_MAT_FEASIBLE_OWNER_ROUTE pawn=" + actor.ThingID +
                    " pos=" + cell + " rotation=" + own.Rotation.AsInt +
                    " otherChair=" + (usual != null) +
                    " mood=" + ActualMealMood(cell,map));
            };
        }
    }

    public static class GameThread
    {
        public static Task Run(Action action)
        {
            var result = new TaskCompletionSource<bool>();
            Type driver = typeof(PickleContext).Assembly.GetType(
                "RimWorks.Pickle.Runtime.PickleDriver", true);
            driver.GetMethod("Post", new[] { typeof(Action) }).Invoke(null,
                new object[] { (Action)(() => {
                    try {
                        if (!UnityData.IsInMainThread)
                            throw new Exception("Not on actual Unity main thread");
                        action(); result.SetResult(true);
                    } catch (Exception e) { result.SetException(e); }
                }) });
            return result.Task;
        }
    }

    [PickleSteps]
    public sealed class MatProofSteps
    {
        private static IntVec3 Empty(Map map, IntVec3 after)
        {
            for(int z=Math.Max(9,after.z);z<map.Size.z-9;z++)
                for(int x=9;x<map.Size.x-9;x++)
                {
                    IntVec3 c=new IntVec3(x,0,z);
                    if ((c-after).LengthHorizontal<14f) continue;
                    bool clear=true;
                    foreach(var test in new[]{c,c+IntVec3.North,c+IntVec3.East,c+IntVec3.West,
                                               c+IntVec3.South})
                        if(!test.InBounds(map)||!test.Standable(map)||
                           test.GetEdifice(map)!=null) clear=false;
                    if(clear) return c;
                }
            return IntVec3.Invalid;
        }

        private static Job Meal(Thing food)
        {
            Job result=JobMaker.MakeJob(JobDefOf.Ingest, food);
            result.count=1;
            return result;
        }

        [Then("an owner-only mat works in both rotations with loaded meal bonus scoring")]
        public async Task Verify(PickleContext ctx)
        {
            Pawn ownerA=null, ownerB=null, outsider=null;
            Building_Bed matA=null,matB=null;
            Thing mealA=null,mealB=null,mealOutside=null;
            Map map=null;
            float baseA=0f,baseB=0f;
            bool ateA=false,ateB=false,comfortA=false,comfortB=false;
            bool laidA=false,laidB=false,outsiderTried=false;
            IntVec3 a=IntVec3.Invalid,b=IntVec3.Invalid;
            await ctx.WaitUntil(() => {
                Type t=Type.GetType("AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                PropertyInfo f=t==null?null:t.GetProperty("IsPostLoaded");
                return f!=null && (bool)f.GetValue(null,null);
            },90f);
            await ctx.WaitFrames(2);
            await GameThread.Run(() => {
                map=Find.CurrentMap;
                ctx.Require(map!=null,"1.6 original map not loaded");
                ThingDef bed=DefDatabase<ThingDef>.GetNamed("Bedroll");
                ThingDef cloth=DefDatabase<ThingDef>.GetNamed("Cloth");
                ThingDef food=DefDatabase<ThingDef>.GetNamed("MealSimple");
                PawnKindDef colonist=DefDatabase<PawnKindDef>.GetNamed("Colonist");
                ctx.Require(!bed.building.isSittable && bed.Size.x*bed.Size.z==2,
                    "Bedroll baseline must be 1x2 and NOT universally sittable");
                new Harmony("sucro.amj.culture.mat-feasibility16").PatchAll(
                    typeof(OwnedMatMealPoC).Assembly);
                a=Empty(map,new IntVec3(1,0,1));
                b=Empty(map,a);
                ctx.Require(a.IsValid && b.IsValid,"No separated empty 1x2 cells");
                ownerA=PawnGenerator.GeneratePawn(colonist,Faction.OfPlayer);
                ownerB=PawnGenerator.GeneratePawn(colonist,Faction.OfPlayer);
                outsider=PawnGenerator.GeneratePawn(colonist,Faction.OfPlayer);
                GenSpawn.Spawn(ownerA,a+IntVec3.West,map,WipeMode.Vanish);
                GenSpawn.Spawn(ownerB,b+IntVec3.West,map,WipeMode.Vanish);
                GenSpawn.Spawn(outsider,a+IntVec3.South,map,WipeMode.Vanish);
                matA=(Building_Bed)ThingMaker.MakeThing(bed,cloth);
                matB=(Building_Bed)ThingMaker.MakeThing(bed,cloth);
                matA.SetFactionDirect(Faction.OfPlayer);
                matB.SetFactionDirect(Faction.OfPlayer);
                GenSpawn.Spawn(matA,a,map,Rot4.South,WipeMode.Vanish);
                GenSpawn.Spawn(matB,b,map,Rot4.East,WipeMode.Vanish);
                ctx.Require(matA.Rotation==Rot4.South &&
                   matB.Rotation==Rot4.East &&
                   matA.OccupiedRect().Width*matA.OccupiedRect().Height==2 &&
                   matB.OccupiedRect().Width*matB.OccupiedRect().Height==2,
                   "South or East rotated physical bed footprint failed");
                ownerA.ownership.ClaimBedIfNonMedical(matA);
                ownerB.ownership.ClaimBedIfNonMedical(matB);
                ctx.Require(ownerA.ownership.OwnedBed==matA &&
                            ownerB.ownership.OwnedBed==matB &&
                            outsider.ownership.OwnedBed!=matA,
                            "Existing Bed ownership failed");
                OwnedMatMealPoC.Mats.Add(matA);
                OwnedMatMealPoC.Mats.Add(matB);
                ctx.Require(!matA.def.building.isSittable,
                    "Owner-only proof must not globally enable bed sitting");
                baseA=0.05f; baseB=0.05f;
                ownerA.needs.comfort.CurLevel=baseA;
                ownerB.needs.comfort.CurLevel=baseB;
                mealA=GenSpawn.Spawn(ThingMaker.MakeThing(food),a+IntVec3.East,map,WipeMode.Vanish);
                mealB=GenSpawn.Spawn(ThingMaker.MakeThing(food),b+IntVec3.East,map,WipeMode.Vanish);
                mealOutside=GenSpawn.Spawn(ThingMaker.MakeThing(food),a+IntVec3.South,map,WipeMode.Vanish);
                Find.TickManager.CurTimeSpeed=TimeSpeed.Ultrafast;
                ownerA.jobs.StartJob(Meal(mealA),JobCondition.InterruptForced);
                ownerB.jobs.StartJob(Meal(mealB),JobCondition.InterruptForced);
                Log.Message("AMJ_MAT_FEASIBLE_START south="+a+" east="+b);
            });
            for(int i=0;i<55;i++)
            {
                await ctx.WaitTicks(65);
                await GameThread.Run(()=>{
                    if(ownerA.CurJob!=null && ownerA.CurJob.def==JobDefOf.Ingest &&
                       ownerA.Position==matA.Position) ateA=true;
                    if(ownerB.CurJob!=null && ownerB.CurJob.def==JobDefOf.Ingest &&
                       ownerB.Position==matB.Position) ateB=true;
                    if(ownerA.needs.comfort.CurLevel>baseA+0.005f) comfortA=true;
                    if(ownerB.needs.comfort.CurLevel>baseB+0.005f) comfortB=true;
                });
                if(ateA&&ateB&&comfortA&&comfortB && 
                    (mealA.Destroyed||!mealA.Spawned) &&
                    (mealB.Destroyed||!mealB.Spawned)) break;
            }
            await GameThread.Run(()=>{
                ctx.Require(ateA&&ateB,"Owner failed seated Ingest in one physical rotation");
                ctx.Require(comfortA&&comfortB,"Owner did not receive real seat Comfort");
                ctx.Require((mealA.Destroyed||!mealA.Spawned) &&
                            (mealB.Destroyed||!mealB.Spawned),"Normal Ingest did not complete");
                ctx.Require(OwnedMatMealPoC.Picked.TryGetValue(ownerA,out var pA)&&pA==matA &&
                            OwnedMatMealPoC.Picked.TryGetValue(ownerB,out var pB)&&pB==matB,
                            "Special seat route did not select each person's own mat");
                ctx.Require(ownerA.ownership.OwnedBed==matA &&
                            ownerB.ownership.OwnedBed==matB,
                            "Eating displaced original owner");
                Log.Message("AMJ_MAT_FEASIBLE_ROTATIONS_OWNER_MEALS_OK");
                // A normal third colonist has no assigned mat. Vanilla and Culture
                // must not pick either unsittable Bedroll as a meal chair.
                outsider.jobs.StartJob(Meal(mealOutside),JobCondition.InterruptForced);
            });
            for(int i=0;i<36;i++)
            {
                await ctx.WaitTicks(60);
                await GameThread.Run(()=>{
                    if(outsider.CurJob!=null && outsider.CurJob.def==JobDefOf.Ingest &&
                       (outsider.Position==matA.Position ||
                        outsider.Position==matB.Position)) outsiderTried=true;
                });
                if(mealOutside.Destroyed||!mealOutside.Spawned)break;
            }
            await GameThread.Run(()=>{
                ctx.Require(!OwnedMatMealPoC.Picked.ContainsKey(outsider) &&
                            !outsiderTried,
                            "Non-owner consumed another pawn's dedicated meal seat");
                ctx.Require(mealOutside.Destroyed||!mealOutside.Spawned,
                            "Non-owner fallback ordinary eating did not complete");
                ctx.Require(OwnedMatMealPoC.OtherOwnerRejected>0,
                            "Owner-filter rejection path not exercised");
                ctx.Require(!matA.def.building.isSittable,
                            "Test leaked universal bed sitting into shared Def");
                // Ensure loaded game data has actual positive and zero thought stages
                // and comparator gives the larger positive stage precedence,
                // then Comfort and distance for ties. No made-up bonus numbers.
                var stages=ThoughtDefOf.AteInImpressiveDiningRoom.stages;
                float lowest=0f,highest=0f;
                foreach(var stage in stages)
                    if(stage!=null) highest=Math.Max(highest,stage.baseMoodEffect);
                ctx.Require(highest>0f,"Loaded game lacks any positive dining-room stage");
                ctx.Require(OwnedMatMealPoC.HigherBenefit(highest,lowest,0f,100f,15f,1f),
                            "Meal buff comparison did not override nearer comfortable chair");
                ctx.Require(!OwnedMatMealPoC.HigherBenefit(lowest,highest,100f,0f,1f,15f),
                            "Lower mood incorrectly outranks higher mood");
                ctx.Require(OwnedMatMealPoC.HigherBenefit(0f,0f,0.6f,0.4f,10f,2f),
                            "Equal buff should prefer higher Comfort");
                ctx.Require(!OwnedMatMealPoC.HigherBenefit(0f,0f,0.6f,0.6f,8f,2f),
                            "Equal bonus & Comfort should prefer shorter route");
                float actual=OwnedMatMealPoC.ActualMealMood(matA.Position,map);
                ctx.Require(actual>=0f && OwnedMatMealPoC.RoomMoodLookups>0,
                            "Actual game room impressiveness scoring failed");
                Log.Message("AMJ_MAT_FEASIBLE_BUFF_COMPARATOR_OK loadedStages="+
                    stages.Count+" nativeRoomMood="+actual);
                // Newly generated healthy pawns may be fully rested and cancel
                // a direct LayDown job before the next 20-tick observation.
                // Hold ordinary Rest low to test actual bed use, not job timing.
                ctx.Require(ownerA.needs.rest!=null && ownerB.needs.rest!=null,
                            "Generated owners must have ordinary rest needs");
                ownerA.needs.rest.CurLevel=0.05f;
                ownerB.needs.rest.CurLevel=0.05f;
                ownerA.jobs.StartJob(JobMaker.MakeJob(JobDefOf.LayDown,matA),
                                     JobCondition.InterruptForced);
                ownerB.jobs.StartJob(JobMaker.MakeJob(JobDefOf.LayDown,matB),
                                     JobCondition.InterruptForced);
            });
            for(int i=0;i<35;i++)
            {
                await ctx.WaitTicks(20);
                await GameThread.Run(()=>{
                    laidA |= ownerA.CurJob!=null && ownerA.CurJob.def==JobDefOf.LayDown &&
                       ownerA.GetPosture()==PawnPosture.LayingInBed &&
                       ownerA.ownership.OwnedBed==matA;
                    laidB |= ownerB.CurJob!=null && ownerB.CurJob.def==JobDefOf.LayDown &&
                       ownerB.GetPosture()==PawnPosture.LayingInBed &&
                       ownerB.ownership.OwnedBed==matB;
                });
                if(laidA&&laidB)break;
            }
            await GameThread.Run(()=>{
                if (!laidA || !laidB)
                    Log.Warning("AMJ_MAT_FEASIBLE_LAYDOWN_DIAGNOSTIC"+
                        " Ajob="+(ownerA.CurJob==null?"null":ownerA.CurJob.def.defName)+
                        " Aposture="+ownerA.GetPosture()+
                        " Arest="+ownerA.needs.rest.CurLevel+
                        " Apos="+ownerA.Position+
                        " Aowned="+(ownerA.ownership.OwnedBed==matA)+
                        " Bjob="+(ownerB.CurJob==null?"null":ownerB.CurJob.def.defName)+
                        " Bposture="+ownerB.GetPosture()+
                        " Brest="+ownerB.needs.rest.CurLevel+
                        " Bpos="+ownerB.Position+
                        " Bowned="+(ownerB.ownership.OwnedBed==matB));
                ctx.Require(laidA&&laidB,
                            "Existing LayDown failed after both owner-only meals");
                ctx.Require(matA.Spawned&&matB.Spawned &&
                            matA.Rotation==Rot4.South&&matB.Rotation==Rot4.East &&
                            !matA.def.building.isSittable,
                            "Bed rotation or shared Def changed unexpectedly");
                Log.Message("AMJ_MAT16_OWNER_ROTATION_MEAL_REST_POC_OK");
            });
        }
    }
}
