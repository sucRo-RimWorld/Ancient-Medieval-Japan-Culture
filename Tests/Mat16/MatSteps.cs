using System;
using System.Reflection;
using System.Threading.Tasks;
using RimWorks.Pickle;
using RimWorld;
using Verse;
using Verse.AI;

namespace AMJ.Culture.MultiuseMatPoC
{
    // Disposable loaded-game feasibility research. Modifies Bedroll Def only
    // in the isolated test process; never part of a Culture product DLL/XML.
    public static class MainThread
    {
        public static Task Do(Action a)
        {
            var done = new TaskCompletionSource<bool>();
            Type t = typeof(PickleContext).Assembly.GetType(
                "RimWorks.Pickle.Runtime.PickleDriver", true);
            t.GetMethod("Post", new[] { typeof(Action) }).Invoke(null,
                new object[] { (Action)(() => {
                    try {
                        if (!UnityData.IsInMainThread)
                            throw new Exception("Must execute in Unity game thread");
                        a(); done.SetResult(true);
                    } catch(Exception ex) { done.SetException(ex); }
                }) });
            return done.Task;
        }
    }

    [PickleSteps]
    public sealed class MultiuseMatSteps
    {
        [Then("an owned one-by-two mat supports eating and sleeping without relocation")]
        public async Task Check(PickleContext ctx)
        {
            Building_Bed mat = null;
            Pawn pawn = null;
            Thing meal = null;
            IntVec3 site = IntVec3.Invalid;
            bool ateOnMat = false, ateSuccessfully = false, gainedComfort = false;
            bool actuallySlept = false;
            float startComfort = 0f;
            bool originalSittable = false;

            await ctx.WaitUntil(() => {
                Type t = Type.GetType(
                    "AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                PropertyInfo f = t == null ? null : t.GetProperty("IsPostLoaded");
                return f != null && (bool)f.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);
            await MainThread.Do(() => {
                Map map = Find.CurrentMap;
                ctx.Require(map != null, "No actual 1.6 map");
                ThingDef bedDef = DefDatabase<ThingDef>.GetNamedSilentFail("Bedroll");
                ThingDef cloth = DefDatabase<ThingDef>.GetNamedSilentFail("Cloth");
                ThingDef foodDef = DefDatabase<ThingDef>.GetNamedSilentFail("MealSimple");
                PawnKindDef kind = DefDatabase<PawnKindDef>.GetNamedSilentFail("Colonist");
                ctx.Require(bedDef != null && cloth != null && foodDef != null && kind != null,
                            "Original game Bedroll/Cloth/MealSimple/Colonist unavailable");
                ctx.Require(bedDef.IsBed && bedDef.Size.x * bedDef.Size.z == 2 &&
                            bedDef.passability == Traversability.Standable,
                            "Native Bedroll is not a standable sleeping building");
                originalSittable = bedDef.building.isSittable;
                ctx.Require(!originalSittable,"Baseline Bedroll unexpectedly sittable");
                // Minimal technical change being evaluated: same physical
                // Building_Bed is also an eligible seating place. No extra
                // Thing, invisible chair, automatic folding or relocation.
                bedDef.building.isSittable = true;
                for(int z=8;z<map.Size.z-8 && !site.IsValid;z++)
                  for(int x=8;x<map.Size.x-8 && !site.IsValid;x++) {
                    var s=new IntVec3(x,0,z);
                    var cells=new[]{s,s+IntVec3.North,s+IntVec3.West,s+IntVec3.East};
                    bool clear=true;
                    foreach(var c in cells)
                        if(!c.InBounds(map)||!c.Standable(map)||c.GetEdifice(map)!=null)
                           clear=false;
                    if(clear)site=s;
                  }
                ctx.Require(site.IsValid,"No clear test bedding site");
                pawn=PawnGenerator.GeneratePawn(kind,Faction.OfPlayer);
                GenSpawn.Spawn(pawn,site+IntVec3.West,map,WipeMode.Vanish);
                mat=ThingMaker.MakeThing(bedDef,cloth) as Building_Bed;
                ctx.Require(mat!=null,"Bedroll did not produce original Building_Bed");
                mat.SetFactionDirect(Faction.OfPlayer);
                GenSpawn.Spawn(mat,site,map,Rot4.South,WipeMode.Vanish);
                ctx.Require(mat.Spawned && mat.Position.Standable(map)
                    && mat.def.building.isSittable,"Mat missing or not sittable in actual map");
                pawn.ownership.ClaimBedIfNonMedical(mat);
                ctx.Require(pawn.ownership.OwnedBed==mat,"Owned bed assignment failed");
                pawn.needs.comfort.CurLevel=0.05f;
                startComfort=pawn.needs.comfort.CurLevel;
                meal=GenSpawn.Spawn(ThingMaker.MakeThing(foodDef),
                                  site+IntVec3.East,map,WipeMode.Vanish);
                Job job=JobMaker.MakeJob(JobDefOf.Ingest,meal);
                job.count=1;
                Find.TickManager.CurTimeSpeed=TimeSpeed.Ultrafast;
                pawn.jobs.StartJob(job,JobCondition.InterruptForced);
                Log.Message("AMJ_MAT16_START owningPawn="+pawn.ThingID+
                    " bed="+mat.ThingID+" site="+site+" dual=true");
            });
            for (int i=0;i<45;i++) {
                await ctx.WaitTicks(80);
                await MainThread.Do(()=>{
                    if(pawn.CurJob!=null && pawn.CurJob.def==JobDefOf.Ingest &&
                       pawn.Position==site)
                        ateOnMat=true;
                    if(pawn.needs.comfort.CurLevel>startComfort+0.005f)
                        gainedComfort=true;
                    if(meal.Destroyed || !meal.Spawned)ateSuccessfully=true;
                });
                if(ateOnMat && gainedComfort && ateSuccessfully)break;
            }
            await MainThread.Do(()=>{
                Log.Message("AMJ_MAT16_EAT_DIAGNOSTIC onMat="+ateOnMat+
                     " gainedComfort="+gainedComfort+" complete="+ateSuccessfully+
                     " comfort="+pawn.needs.comfort.CurLevel+
                     " ownerMaintained="+(pawn.ownership.OwnedBed==mat));
                ctx.Require(ateOnMat,"Actual Ingest job did not target owned mat cell");
                ctx.Require(gainedComfort,"Actual daytime mat did not grant Comfort");
                ctx.Require(ateSuccessfully,"Actual meal on mat did not complete");
                ctx.Require(pawn.ownership.OwnedBed==mat,"Eating lost bedding owner assignment");
                pawn.jobs.StartJob(JobMaker.MakeJob(JobDefOf.LayDown,mat),
                                   JobCondition.InterruptForced);
            });
            for(int i=0;i<30;i++){
                await ctx.WaitTicks(20);
                await MainThread.Do(()=>{
                    if(pawn.CurJob!=null && pawn.CurJob.def==JobDefOf.LayDown &&
                       pawn.GetPosture()==PawnPosture.LayingInBed &&
                       pawn.ownership.OwnedBed==mat && mat.Spawned && mat.Position==site)
                       actuallySlept=true;
                });
                if(actuallySlept)break;
            }
            await MainThread.Do(()=>{
                ctx.Require(actuallySlept,"Original assigned sleeping Job failed after mat meal");
                ctx.Require(mat.def.IsBed && mat.def.building.isSittable &&
                            mat.Position==site && mat.Spawned &&
                            pawn.ownership.OwnedBed==mat,
                            "Single 1x2 physical mat no longer supports both functions");
                mat.def.building.isSittable=originalSittable;
                Log.Message("AMJ_MAT16_ONE_THING_DAY_EAT_NIGHT_SLEEP_PASS");
            });
        }
    }
}
