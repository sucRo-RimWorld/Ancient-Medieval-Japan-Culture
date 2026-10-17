using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using RimWorks.Pickle;
using RimWorld;
using Verse;
using Verse.AI;

namespace AMJ.Culture.BeddingPoC
{
    // TEST-ONLY code. Never compiled into the Culture production assembly.
    // The serialized logical sleeping place intentionally outlives a spawned bed.
    public sealed class ProbeSleepingPlace : GameComponent
    {
        public Pawn Owner;
        public IntVec3 Position = IntVec3.Invalid;
        public Building_Bed DeployedBed;
        public MinifiedThing FoldedBed;
        public bool AutoFold = true;
        public int PackCount;
        public int DeployCount;
        private int reloadPhase;
        private int reloadFrames;
        public ProbeSleepingPlace(Game game) { }

        // Reload evidence runs in a second original-game process, not in Pickle.
        // A small disposable state machine avoids double-triggering install Jobs.
        public override void GameComponentUpdate()
        {
            if (System.Environment.GetEnvironmentVariable("AMJ_BEDDING_POC16_RELOAD") != "1" ||
                reloadPhase == 3 || Find.CurrentMap == null || Find.TickManager == null)
                return;
            reloadFrames++;
            if (reloadFrames < 40) return;
            try {
                if (reloadPhase == 0) {
                    bool intact = Owner != null && Owner.Spawned &&
                        Owner.Map == Find.CurrentMap && Position.IsValid &&
                        FoldedBed != null && FoldedBed.Spawned &&
                        !FoldedBed.Destroyed && DeployedBed == null &&
                        AutoFold && PackCount >= 2 &&
                        Position.GetEdifice(Find.CurrentMap) == null &&
                        (Position + IntVec3.North).GetEdifice(Find.CurrentMap) == null;
                    Log.Message("AMJ_BEDDING_POC16_RELOAD_STATE intact=" + intact +
                        " owner=" + (Owner == null ? "null" : Owner.ThingID) +
                        " folded=" + (FoldedBed == null ? "null" : FoldedBed.ThingID) +
                        " position=" + Position + " packed=" + PackCount);
                    if (!intact) throw new InvalidOperationException("F5 folded place failed genuine save reload");
                    Find.TickManager.CurTimeSpeed=TimeSpeed.Ultrafast;
                    MinifiedThing mini=FoldedBed;
                    mini.DeSpawn(DestroyMode.Vanish);
                    if(!Owner.inventory.innerContainer.TryAdd(mini))
                        throw new InvalidOperationException("F5 reloaded bed cannot be held");
                    var blueprint=GenConstruct.PlaceBlueprintForInstall(mini,Position,Find.CurrentMap,
                        Rot4.South,Faction.OfPlayer);
                    if(blueprint==null) throw new InvalidOperationException("F5 reloaded placement rejected");
                    JobDef job=DefDatabase<JobDef>.GetNamed("AMJ_BeddingPocDeploy");
                    Owner.jobs.StartJob(JobMaker.MakeJob(job,mini,blueprint),JobCondition.InterruptForced);
                    reloadPhase=1;
                }
                else if(reloadPhase==1 && DeployedBed != null && DeployedBed.Spawned &&
                    Owner.ownership.OwnedBed == DeployedBed && DeployedBed.Position == Position)
                {
                    Owner.jobs.StartJob(JobMaker.MakeJob(JobDefOf.LayDown,DeployedBed),
                                        JobCondition.InterruptForced);
                    reloadPhase=2;
                }
                else if(reloadPhase==2 && Owner.CurJob != null &&
                    Owner.CurJob.def==JobDefOf.LayDown &&
                    Owner.GetPosture()==PawnPosture.LayingInBed)
                {
                    Log.Message("AMJ_BEDDING_POC16_RELOAD_OK originalOwner=" +
                        Owner.ThingID + " site=" + Position + " deployCount=" +
                        DeployCount + " observedRealRest=true");
                    reloadPhase=3;
                    UnityEngine.Application.Quit();
                }
                else if(reloadFrames>2200)
                    throw new InvalidOperationException("F5 timed out waiting for redeploy/rest");
            }
            catch(Exception e) {
                reloadPhase=3;
                Log.Error("AMJ_BEDDING_POC16_RELOAD_FAIL " + e);
                UnityEngine.Application.Quit();
            }
        }

        public override void ExposeData()
        {
            Scribe_References.Look(ref Owner, "owner");
            Scribe_Values.Look(ref Position, "sleepCell", IntVec3.Invalid);
            Scribe_References.Look(ref DeployedBed, "deployed");
            Scribe_References.Look(ref FoldedBed, "folded");
            Scribe_Values.Look(ref AutoFold, "autoFold", true);
            Scribe_Values.Look(ref PackCount, "packCount", 0);
            Scribe_Values.Look(ref DeployCount, "deployCount", 0);
        }
    }

    // A concrete Pawn Job, not a C# direct-spawn shortcut for "unfold".
    public sealed class ProbeDeployDriver : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed) { return true; }
        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Misc.TakeItemFromInventoryToCarrier(pawn, TargetIndex.A);
            yield return Toils_Haul.CarryHauledThingToContainer();
            yield return Toils_Construct.MakeSolidThingFromBlueprintIfNecessary(TargetIndex.B, TargetIndex.C);
            Toil done = ToilMaker.MakeToil("PocBedAssigned");
            done.initAction = delegate
            {
                Building_Bed bed = TargetB.Thing as Building_Bed;
                if (bed == null || !bed.Spawned)
                {
                    EndJobWith(JobCondition.Incompletable);
                    return;
                }
                var state = Current.Game.GetComponent<ProbeSleepingPlace>();
                if (state.Owner != pawn || bed.Position != state.Position)
                {
                    EndJobWith(JobCondition.Incompletable);
                    return;
                }
                pawn.ownership.ClaimBedIfNonMedical(bed);
                state.DeployedBed = bed;
                state.FoldedBed = null;
                state.DeployCount++;
                Log.Message("AMJ_BEDDING_POC16_DEPLOYED position=" + bed.Position
                    + " owner=" + pawn.ThingID);
            };
            yield return done;
        }
    }

    [StaticConstructorOnStartup]
    public static class PocReloadBoot
    {
        static PocReloadBoot()
        {
            if (System.Environment.GetEnvironmentVariable("AMJ_BEDDING_POC16_RELOAD") != "1")
                return;
            LongEventHandler.ExecuteWhenFinished(delegate {
                Log.Message("AMJ_BEDDING_POC16_RELOAD_START");
                GameDataSaveLoader.LoadGame("AMJ_BeddingPoc16_Folded");
            });
        }
    }

    public static class GameThread
    {
        public static Task Run(Action callback)
        {
            var tcs = new TaskCompletionSource<bool>();
            Type driver = typeof(PickleContext).Assembly.GetType(
                "RimWorks.Pickle.Runtime.PickleDriver", true);
            MethodInfo post = driver.GetMethod("Post", new[] { typeof(Action) });
            post.Invoke(null, new object[] { (Action)delegate {
                try {
                    if (!UnityData.IsInMainThread) throw new Exception("Off main thread");
                    callback();
                    tcs.SetResult(true);
                } catch(Exception e) { tcs.SetException(e); }
            }});
            return tcs.Task;
        }
    }

    [PickleSteps]
    public sealed class BeddingSteps
    {
        [Then("one assigned bed really folds and returns to its sleeping place")]
        public async Task FoldAndRedeploy(PickleContext ctx)
        {
            Pawn pawn = null;
            Building_Bed bed = null;
            ProbeSleepingPlace place = null;
            IntVec3 site = IntVec3.Invalid;
            bool sawSleep = false;
            bool sawFolded = false;
            bool sawReinstalled = false;
            float occupiedSpace = 0f;
            float emptiedSpace = 0f;
            int installerJobs = 0;
            await ctx.WaitUntil(() => {
                Type t = Type.GetType("AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                PropertyInfo flag = t == null ? null : t.GetProperty("IsPostLoaded");
                return flag != null && (bool)flag.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);
            await GameThread.Run(() => {
                Map map = Find.CurrentMap;
                ctx.Require(map != null, "No original-game map");
                place = Current.Game.GetComponent<ProbeSleepingPlace>();
                ctx.Require(place != null, "Serialized sleeping-place GameComponent not created");
                ThingDef bedDef = DefDatabase<ThingDef>.GetNamedSilentFail("Bedroll");
                ThingDef cloth = DefDatabase<ThingDef>.GetNamedSilentFail("Cloth");
                PawnKindDef kind = DefDatabase<PawnKindDef>.GetNamedSilentFail("Colonist");
                ctx.Require(bedDef != null && cloth != null && kind != null,
                    "Vanilla bedroll/cloth/colonist missing");
                ctx.Require(bedDef.Minifiable && bedDef.Size.x * bedDef.Size.z == 2,
                    "Bedroll is not a minifiable 1x2 building");
                for (int z=8; z < map.Size.z-9 && !site.IsValid; z++)
                  for(int x=8; x < map.Size.x-9 && !site.IsValid; x++) {
                    IntVec3 s = new IntVec3(x,0,z);
                    if(s.Standable(map) && (s+IntVec3.North).Standable(map) &&
                       s.GetEdifice(map)==null &&
                       (s+IntVec3.North).GetEdifice(map)==null)
                       site=s;
                  }
                ctx.Require(site.IsValid, "No vacant 1x2 sleep site");
                pawn=PawnGenerator.GeneratePawn(kind,Faction.OfPlayer);
                GenSpawn.Spawn(pawn,site+IntVec3.West,map,WipeMode.Vanish);
                bed=ThingMaker.MakeThing(bedDef,cloth) as Building_Bed;
                ctx.Require(bed != null,"Bedroll was not a Building_Bed");
                bed.SetFactionDirect(Faction.OfPlayer);
                GenSpawn.Spawn(bed,site,map,Rot4.South,WipeMode.Vanish);
                ctx.Require(bed.Spawned && bed.Position==site,"Bedroll spawn failed");
                place.Position=site;place.Owner=pawn;place.DeployedBed=bed;
                pawn.ownership.ClaimBedIfNonMedical(bed);
                ctx.Require(pawn.ownership.OwnedBed==bed,"Claimed bed not owned");
                Room room=bed.GetRoom();
                occupiedSpace=room==null?0f:room.GetStat(RoomStatDefOf.Space);
                Find.TickManager.CurTimeSpeed=TimeSpeed.Ultrafast;
                pawn.jobs.StartJob(JobMaker.MakeJob(JobDefOf.LayDown,bed),JobCondition.InterruptForced);
                Log.Message("AMJ_BEDDING_POC16_F1 site="+site+" owner="+pawn.ThingID
                    +" roomSpace="+occupiedSpace);
            });
            for(int i=0;i<16;i++) {
                await ctx.WaitTicks(15);
                await GameThread.Run(() => {
                    if (pawn.CurJob != null && pawn.CurJob.def==JobDefOf.LayDown &&
                        pawn.GetPosture()==PawnPosture.LayingInBed)
                        sawSleep=true;
                });
                if(sawSleep)break;
            }
            await GameThread.Run(() => {
                ctx.Require(sawSleep,"F2 pawn did not actually lay in assigned bed");
                Map map=pawn.Map;
                map.designationManager.AddDesignation(new Designation(bed,DesignationDefOf.Uninstall));
                pawn.jobs.StartJob(JobMaker.MakeJob(JobDefOf.Uninstall,bed),JobCondition.InterruptForced);
            });
            MinifiedThing folded=null;
            for(int i=0;i<90;i++) {
                await ctx.WaitTicks(25);
                await GameThread.Run(() => {
                    Map map=pawn.Map;
                    if(!bed.Spawned) {
                        foreach(Thing thing in map.listerThings.ThingsInGroup(ThingRequestGroup.MinifiedThing))
                            if (thing is MinifiedThing mini &&
                                mini.GetInnerIfMinified()==bed) { folded=mini;break; }
                        if(folded!=null) {
                            place.FoldedBed=folded;place.DeployedBed=null;
                            place.PackCount++;
                            sawFolded=true;
                            Room room=site.GetRoom(map);
                            emptiedSpace=room==null?0f:room.GetStat(RoomStatDefOf.Space);
                        }
                    }
                });
                if(sawFolded)break;
            }
            await GameThread.Run(() => {
                ctx.Require(sawFolded,"F3 native Uninstall job did not yield minified bed");
                ctx.Require(site.GetEdifice(pawn.Map)==null &&
                   (site+IntVec3.North).GetEdifice(pawn.Map)==null,
                   "F3 bed footprint still has an edifice");
                ctx.Require(place.Owner==pawn && place.Position==site,
                    "F3 logical sleeping site disappeared");
                ctx.Require(folded!=null && folded.Spawned,"F3 real folded item missing");
                Log.Message("AMJ_BEDDING_POC16_F3 folded=true beforeSpace="+occupiedSpace
                    +" afterSpace="+emptiedSpace);
                // Vanilla 1.6 bed installation: move the only folded item into inventory
                // so the test deploy Job can use standard installation Toils.
                // A just-uninstalled MinifiedThing remains spawned on the map.
                // Vanilla ThingOwner refuses a spawned Thing: despawn safely
                // before moving the exact existing object into pawn inventory.
                ctx.Require(folded.Spawned, "F4 expected original uninstalled Thing on map");
                folded.DeSpawn(DestroyMode.Vanish);
                ctx.Require(!folded.Spawned && !folded.Destroyed,
                    "F4 despawn should not destroy the packed bed");
                ctx.Require(pawn.inventory.innerContainer.TryAdd(folded),
                    "F4 cannot carry unspawned minified bed");
                var bp=GenConstruct.PlaceBlueprintForInstall(folded,site,pawn.Map,
                      Rot4.South,Faction.OfPlayer);
                ctx.Require(bp!=null,"F4 install blueprint missing");
                JobDef deploy=DefDatabase<JobDef>.GetNamedSilentFail("AMJ_BeddingPocDeploy");
                ctx.Require(deploy!=null,"F4 deploy JobDef missing");
                pawn.jobs.StartJob(JobMaker.MakeJob(deploy,folded,bp),JobCondition.InterruptForced);
                installerJobs++;
            });
            for(int i=0;i<90;i++) {
                await ctx.WaitTicks(25);
                await GameThread.Run(() => {
                    if(place.DeployedBed != null && place.DeployedBed.Spawned &&
                       pawn.ownership.OwnedBed==place.DeployedBed &&
                       place.DeployedBed.Position==site)
                        sawReinstalled=true;
                });
                if(sawReinstalled)break;
            }
            await GameThread.Run(() => {
                ctx.Require(sawReinstalled,"F4 assigned bed did not reinstall in same cell");
                ctx.Require(place.Owner==pawn && place.AutoFold && place.Position==site,
                    "F4 original sleeping-site record changed");
                ctx.Require(place.DeployCount==1 && installerJobs==1,
                    "F4 deploy count indicates a repeated job");
                ctx.Require(place.FoldedBed==null,"F4 folded item reference not cleared");
                ctx.Require(place.DeployedBed.Spawned,"F4 deployed bed not physically present");
                // No claim of F5 save/load yet; this is a physical-Job diagnostic only.
                Log.Message("AMJ_BEDDING_POC16_F4_OK assigned="+pawn.ownership.OwnedBed
                    +" packed="+place.PackCount+" deployed="+place.DeployCount);
                Log.Message("AMJ_BEDDING_POC16_F1_F4_NATIVE_OK");
                // A second real Uninstall leaves the compact item spawned
                // in the map, where Scribe can save it and the owner reference.
                bed = place.DeployedBed;
                pawn.Map.designationManager.AddDesignation(
                    new Designation(bed, DesignationDefOf.Uninstall));
                pawn.jobs.StartJob(JobMaker.MakeJob(JobDefOf.Uninstall,bed),
                                   JobCondition.InterruptForced);
            });
            bool packedForSave = false;
            for(int i=0;i<90;i++) {
                await ctx.WaitTicks(25);
                await GameThread.Run(() => {
                    if (!bed.Spawned && !packedForSave) {
                        foreach(Thing thing in pawn.Map.listerThings.ThingsInGroup(
                            ThingRequestGroup.MinifiedThing))
                            if(thing is MinifiedThing m && m.GetInnerIfMinified()==bed) {
                                place.FoldedBed=m;place.DeployedBed=null;
                                place.PackCount++;
                                packedForSave=true;
                                break;
                            }
                    }
                });
                if(packedForSave)break;
            }
            await GameThread.Run(() => {
                ctx.Require(packedForSave,"F5 genuine second Uninstall failed");
                ctx.Require(place.Owner==pawn && place.Position==site &&
                            place.FoldedBed!=null && place.FoldedBed.Spawned,
                            "F5 pre-save logical site lost");
                GameDataSaveLoader.SaveGame("AMJ_BeddingPoc16_Folded");
                MethodInfo pathMethod=typeof(GenFilePaths).GetMethod(
                    "FilePathForSavedGame",BindingFlags.Static|BindingFlags.Public|
                    BindingFlags.NonPublic);
                string savePath=pathMethod==null?null:(string)pathMethod.Invoke(
                    null,new object[]{"AMJ_BeddingPoc16_Folded"});
                ctx.Require(!string.IsNullOrEmpty(savePath) && System.IO.File.Exists(savePath) &&
                   new System.IO.FileInfo(savePath).Length>1024,
                   "F5 original RimWorld save not written");
                Log.Message("AMJ_BEDDING_POC16_SAVED_FOLDED owner="+pawn.ThingID+
                    " position="+site+" packed="+place.PackCount+
                    " bytes="+new System.IO.FileInfo(savePath).Length);
            });
        }
    }
}
