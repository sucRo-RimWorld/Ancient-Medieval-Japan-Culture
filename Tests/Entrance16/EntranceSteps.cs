using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using RimWorks.Pickle;
using RimWorld;
using Verse;
using Verse.AI;

namespace AMJ.Culture.Test
{
    internal static class EntranceMainThread
    {
        internal static Task Run(Action action)
        {
            var result = new TaskCompletionSource<bool>();
            Action call = delegate
            {
                try
                {
                    if (!UnityData.IsInMainThread)
                        throw new InvalidOperationException("Entrance scenario must run on the game thread");
                    action();
                    result.SetResult(true);
                }
                catch (Exception e) { result.SetException(e); }
            };
            Type driver = typeof(PickleContext).Assembly.GetType("RimWorks.Pickle.Runtime.PickleDriver", true);
            MethodInfo method = driver.GetMethod("Post", new[] { typeof(Action) });
            if (method == null) throw new MissingMethodException(driver.FullName, "Post(Action)");
            method.Invoke(null, new object[] { call });
            return result.Task;
        }
    }


    // Disposable test-only component. Serialized by the actual RimWorld save loader,
    // and used in a separate native process to verify reload and resumed Goto.
    public sealed class EntranceReloadProbe : GameComponent
    {
        public Pawn Subject;
        public IntVec3 EntranceCell = IntVec3.Invalid;
        public IntVec3 NoShoesCell = IntVec3.Invalid;
        public IntVec3 ResumeTarget = IntVec3.Invalid;
        private int phase;
        private int ticks;
        public EntranceReloadProbe(Game game) { }

        public override void ExposeData()
        {
            Scribe_References.Look(ref Subject, "entranceProbeSubject");
            Scribe_Values.Look(ref EntranceCell, "entranceProbeThreshold", IntVec3.Invalid);
            Scribe_Values.Look(ref NoShoesCell, "entranceProbeNoShoes", IntVec3.Invalid);
            Scribe_Values.Look(ref ResumeTarget, "entranceProbeTarget", IntVec3.Invalid);
        }

        private static object GetCultureComponent(object owner, string name)
        {
            Type type = Type.GetType("AMJ.Culture." + name + ", AMJ.Culture", true);
            foreach (MethodInfo m in owner.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
                if (m.Name == "GetComponent" && m.IsGenericMethodDefinition &&
                    m.GetParameters().Length == 0)
                    return m.MakeGenericMethod(type).Invoke(owner, null);
            throw new MissingMethodException("Cannot find GetComponent<T> for " + name);
        }

        public override void GameComponentUpdate()
        {
            if (Environment.GetEnvironmentVariable("AMJ_CULTURE_ENTRANCE16_RELOAD") != "1" ||
                phase == 3 || Find.CurrentMap == null || Find.TickManager == null)
                return;
            if (++ticks < 45) return;
            try
            {
                if (phase == 0)
                {
                    Map map = Find.CurrentMap;
                    object zones = GetCultureComponent(map, "EntranceZoneMap");
                    object state = GetCultureComponent(Current.Game, "EntranceFootwearState");
                    bool intact = Subject != null && Subject.Spawned && Subject.Map == map &&
                                  EntranceCell.IsValid && NoShoesCell.IsValid &&
                                  ResumeTarget.IsValid &&
                                  (bool)zones.GetType().GetMethod("Contains", new[] { typeof(IntVec3) })
                                    .Invoke(zones, new object[] { EntranceCell }) &&
                                  (bool)zones.GetType().GetMethod("IsNoShoes", new[] { typeof(IntVec3) })
                                    .Invoke(zones, new object[] { NoShoesCell }) &&
                                  (bool)state.GetType().GetMethod("IsBarefoot", new[] { typeof(Pawn) })
                                    .Invoke(state, new object[] { Subject });
                    Log.Message("AMJ_CULTURE_ENTRANCE16_RELOAD_STATE intact=" + intact);
                    if (!intact) throw new InvalidOperationException("Saved entrance/no-shoes/footwear not restored");
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
                    Job move = JobMaker.MakeJob(JobDefOf.Goto, ResumeTarget);
                    move.playerForced = true;
                    Subject.jobs.StartJob(move, JobCondition.InterruptForced);
                    phase = 1;
                }
                else if (phase == 1 && Subject != null && Subject.Spawned &&
                         Subject.Position == ResumeTarget)
                {
                    Log.Message("AMJ_CULTURE_ENTRANCE16_RELOAD_OK resumedGoto=true");
                    phase = 3;
                    UnityEngine.Application.Quit();
                }
                else if (ticks > 1800)
                    throw new InvalidOperationException("Reloaded Goto did not reach target");
            }
            catch (Exception error)
            {
                Log.Error("AMJ_CULTURE_ENTRANCE16_RELOAD_FAIL " + error);
                phase = 3;
                UnityEngine.Application.Quit();
            }
        }
    }

    [UnityEngine.Scripting.Preserve]
    [StaticConstructorOnStartup]
    public static class EntranceReloadBoot
    {
        static EntranceReloadBoot()
        {
            if (Environment.GetEnvironmentVariable("AMJ_CULTURE_ENTRANCE16_RELOAD") != "1")
                return;
            LongEventHandler.ExecuteWhenFinished(delegate
            {
                Log.Message("AMJ_CULTURE_ENTRANCE16_RELOAD_START");
                GameDataSaveLoader.LoadGame("AMJEntrance16BoundaryFunctional");
            });
        }
    }

    [PickleSteps]
    public sealed class EntranceSteps
    {
        private static Type CultureType(string name)
        {
            Type result = Type.GetType("AMJ.Culture." + name + ", AMJ.Culture");
            if (result == null) throw new InvalidOperationException("Culture component type not loaded: " + name);
            return result;
        }

        private static object Component(object owner, string className)
        {
            Type type = CultureType(className);
            MethodInfo method = null;
            foreach (MethodInfo candidate in owner.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
                if (candidate.Name == "GetComponent" && candidate.IsGenericMethodDefinition &&
                    candidate.GetParameters().Length == 0)
                {
                    method = candidate;
                    break;
                }
            if (method == null) throw new MissingMethodException(owner.GetType().FullName, "GetComponent<T>()");
            return method.MakeGenericMethod(type).Invoke(owner, null);
        }

        private static object Call(object owner, string method, params object[] args)
        {
            // RimWorld designators may overload the same operation name.
            // Resolve the exact parameters rather than throwing AmbiguousMatchException.
            Type[] parameters = new Type[args.Length];
            for (int n = 0; n < args.Length; n++)
                parameters[n] = args[n].GetType();
            MethodInfo target = owner.GetType().GetMethod(method, parameters);
            if (target == null) throw new MissingMethodException(owner.GetType().FullName, method);
            return target.Invoke(owner, args);
        }

        private static bool Barefoot(object state, Pawn pawn)
        {
            return (bool)Call(state, "IsBarefoot", pawn);
        }

        private static bool Casting(object state, Pawn pawn)
        {
            PropertyInfo active = state.GetType().GetProperty("ActivePauses",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return ((IDictionary)active.GetValue(state, null)).Contains(pawn);
        }

        private static void Walk(Pawn pawn, IntVec3 destination)
        {
            Job job = JobMaker.MakeJob(JobDefOf.Goto, destination);
            job.playerForced = true; // Do not let autonomous wandering replace a player-ordered crossing.
            pawn.jobs.StartJob(job, JobCondition.InterruptForced);
        }

        [Then("entrance adjacency and shoe change operate with real paths")]
        public async Task Check(PickleContext ctx)
        {
            Pawn pawn = null;
            object zone = null;
            object state = null;
            IntVec3 start = IntVec3.Invalid;
            IntVec3 end = IntVec3.Invalid;
            bool observedRemoveCast = false;
            bool observedPutOnCast = false;
            bool observedBarefoot = false;
            bool observedShoes = false;
            bool observedExit = false;
            bool validAfterPainting = false;

            await ctx.WaitUntil(() =>
            {
                Type quick = Type.GetType("AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                PropertyInfo flag = quick == null ? null : quick.GetProperty("IsPostLoaded");
                return flag != null && (bool)flag.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);

            await EntranceMainThread.Run(delegate
            {
                Map map = Find.CurrentMap;
                ctx.Require(map != null, "Quickstart map absent");
                zone = Component(map, "EntranceZoneMap");
                state = Component(Current.Game, "EntranceFootwearState");
                ctx.Require(zone != null && state != null, "Culture map/game state components missing");

                DesignationCategoryDef category = DefDatabase<DesignationCategoryDef>.GetNamedSilentFail("Zone");
                ctx.Require(category != null, "Vanilla Zone designation category missing");
                FieldInfo field = typeof(DesignationCategoryDef).GetField("specialDesignatorClasses");
                ctx.Require(field != null, "Zone specialDesignatorClasses field missing");
                IEnumerable tools = field.GetValue(category) as IEnumerable;
                bool entranceTool = false, forbiddenTool = false;
                if (tools != null)
                    foreach (object tool in tools)
                    {
                        string name = tool == null ? "" : tool.ToString();
                        if (name.Contains("Designator_EntranceAdd")) entranceTool = true;
                        if (name.Contains("Designator_NoShoesAdd")) forbiddenTool = true;
                    }
                ctx.Require(entranceTool && forbiddenTool,
                    "Culture entrance and no-shoes designators absent from loaded Zone category");

                for (int z = 4; z < map.Size.z - 5 && !start.IsValid; z++)
                    for (int x = 4; x < map.Size.x - 9; x++)
                    {
                        IntVec3 candidate = new IntVec3(x, 0, z);
                        bool clear = true;
                        // The two-threshold Goto occupies only x=0..8, z=0.
                        // Do not require a much larger 13x6 empty rectangle in
                        // original 1.7 maps; the off-path inactive probe is separate.
                        for (int dx = 0; dx <= 8; dx++)
                        {
                            IntVec3 cell = candidate + new IntVec3(dx, 0, 0);
                            if (!cell.InBounds(map) || !cell.Standable(map) ||
                                cell.GetEdifice(map) != null)
                            { clear = false; break; }
                        }
                        if (clear) { start = candidate; break; }
                    }
                ctx.Require(start.IsValid, "No open area for two painted thresholds");
                IntVec3 first = start + new IntVec3(2, 0, 0);
                IntVec3 second = start + new IntVec3(6, 0, 0);
                IntVec3 invalid = start + new IntVec3(0, 0, 4);
                Call(zone, "Set", invalid, true);
                ctx.Require(!(bool)Call(zone, "HasConnectedNoShoesContact", invalid),
                    "Nonadjacent threshold incorrectly treated as active");
                Call(zone, "Set", first, true);
                Call(zone, "Set", second, true);
                for (int dx = 3; dx <= 5; dx++)
                    Call(zone, "SetNoShoes", start + new IntVec3(dx, 0, 0), true);
                validAfterPainting = (bool)Call(zone, "HasConnectedNoShoesContact", first) &&
                                     (bool)Call(zone, "HasConnectedNoShoesContact", second);
                ctx.Require(validAfterPainting, "Threshold must activate when edge-adjacent");

                PawnKindDef colonist = DefDatabase<PawnKindDef>.GetNamed("Colonist");
                pawn = PawnGenerator.GeneratePawn(colonist, Faction.OfPlayer);
                ctx.Require(pawn != null, "Colonist pawn generation failed");
                GenSpawn.Spawn(pawn, start, map, WipeMode.Vanish);
                end = start + new IntVec3(8, 0, 0);
                Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
                Walk(pawn, end);
                Log.Message("AMJ_CULTURE_ENTRANCE_TEST_READY first=" + first +
                            " second=" + second + " noShoes=3..5 invalid=" + invalid);
            });

            for (int cycle = 0; cycle < 200; cycle++)
            {
                await ctx.WaitTicks(5);
                await EntranceMainThread.Run(delegate
                {
                    observedRemoveCast |= Casting(state, pawn) && !Barefoot(state, pawn);
                    observedPutOnCast |= Casting(state, pawn) && Barefoot(state, pawn);
                    observedBarefoot |= Barefoot(state, pawn);
                    observedExit |= pawn.Position == end;
                    observedShoes |= pawn.Position == end && !Barefoot(state, pawn);
                });
                if (observedRemoveCast && observedPutOnCast && observedShoes) break;
            }

            await EntranceMainThread.Run(delegate
            {
                Log.Message("AMJ_CULTURE_ENTRANCE_DIAGNOSTIC"
                    + " removeCast=" + observedRemoveCast + " putOnCast=" + observedPutOnCast
                    + " barefoot=" + observedBarefoot + " shoesAtExit=" + observedShoes
                    + " valid=" + validAfterPainting + " last=" + pawn.Position
                    + " target=" + end + " job=" + (pawn.CurJob == null ? "none" : pawn.CurJob.def.defName));
                ctx.Require(observedRemoveCast, "No shoe-removal cast occurred on valid entrance");
                ctx.Require(observedBarefoot, "Pawn never became barefoot inside no-shoes area");
                ctx.Require(observedPutOnCast, "No put-on-shoes cast occurred at second entrance");
                ctx.Require(observedShoes, "Pawn did not reach the destination wearing shoes");
                ctx.Require(observedExit, "Original Goto job never reached its destination");
                Log.Message("AMJ_CULTURE_ENTRANCE_FUNCTION_OK");
            });
        }

        [Then("entrance boundary editing dirt filtering and save work")]
        public async Task CheckRemaining(PickleContext ctx)
        {
            Pawn pawn = null;
            object zone = null, state = null;
            IntVec3 origin = IntVec3.Invalid, destination = IntVec3.Invalid;
            bool observedCast = false, arrived = false;

            await ctx.WaitUntil(() =>
            {
                Type quick = Type.GetType("AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                PropertyInfo ready = quick == null ? null : quick.GetProperty("IsPostLoaded");
                return ready != null && (bool)ready.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);
            await EntranceMainThread.Run(delegate
            {
                Map map = Find.CurrentMap;
                ctx.Require(map != null, "Quickstart map not loaded");
                zone = Component(map, "EntranceZoneMap");
                state = Component(Current.Game, "EntranceFootwearState");
                ctx.Require(zone != null && state != null, "Entrance component absent");

                DesignationCategoryDef category = DefDatabase<DesignationCategoryDef>.GetNamedSilentFail("Zone");
                ctx.Require(category != null, "Zone architect category absent");
                FieldInfo field = typeof(DesignationCategoryDef).GetField("specialDesignatorClasses");
                ctx.Require(field != null, "Special designator list unavailable");
                bool hasEntrance = false, hasNoShoes = false, hasRemoval = false;
                foreach (object type in (IEnumerable)field.GetValue(category))
                {
                    string name = type == null ? "" : type.ToString();
                    if (name.Contains("Designator_EntranceAdd")) hasEntrance = true;
                    if (name.Contains("Designator_NoShoesAdd")) hasNoShoes = true;
                    if (name.Contains("Designator_NoShoesRemove")) hasRemoval = true;
                }
                ctx.Require(hasEntrance && hasNoShoes && hasRemoval,
                            "Paint/remove designators not in loaded Zone category");

                for (int z = 8; z < map.Size.z - 8 && !origin.IsValid; z++)
                    for (int x = 8; x < map.Size.x - 19; x++)
                    {
                        IntVec3 p = new IntVec3(x, 0, z);
                        bool clear = true;
                        for (int dx = 0; dx <= 12 && clear; dx++)
                            for (int dz = -1; dz <= 4; dz++)
                            {
                                IntVec3 cell = p + new IntVec3(dx, 0, dz);
                                if (!cell.InBounds(map) || !cell.Standable(map) ||
                                    cell.GetEdifice(map) != null)
                                { clear = false; break; }
                            }
                        if (clear) { origin = p; break; }
                    }
                ctx.Require(origin.IsValid, "No clean area for boundary checks");

                IntVec3 invalid = origin + new IntVec3(3, 0, 0);
                IntVec3 valid = origin + new IntVec3(9, 0, 3);
                IntVec3 diagonal = invalid + new IntVec3(1, 0, 1);
                IntVec3 gap = invalid + new IntVec3(2, 0, 0);
                IntVec3 adjacent = invalid + IntVec3.East;
                object entranceDesignator = Activator.CreateInstance(CultureType("Designator_EntranceAdd"));
                ctx.Require(!"AMJ_Culture_EntranceNotAdjacent".Translate().ToString()
                            .Contains("AMJ_Culture_EntranceNotAdjacent"),
                            "Untranslated nonadjacent entrance warning");
                Log.Message("AMJ_CULTURE_ENTRANCE16_WARNING_LOCALIZED_OK");
                Call(entranceDesignator, "DesignateSingleCell", invalid);
                ctx.Require((bool)Call(zone, "Contains", invalid), "Designator failed to mark entrance");
                ctx.Require(!(bool)Call(zone, "HasConnectedNoShoesContact", invalid),
                            "Entrance without no-shoes area should be inactive");
                Call(zone, "SetNoShoes", diagonal, true);
                Call(zone, "SetNoShoes", gap, true);
                ctx.Require(!(bool)Call(zone, "HasConnectedNoShoesContact", invalid),
                            "Diagonal or one-cell gap incorrectly activated entrance");

                Call(zone, "SetNoShoes", adjacent, true);
                ctx.Require((bool)Call(zone, "HasConnectedNoShoesContact", invalid),
                            "Cardinal-edge adjacency did not activate entrance");
                ctx.Require(!(bool)Call(zone, "IsNoShoes", invalid),
                            "Entrance cell illegally became a no-shoes cell");
                Call(zone, "SetNoShoes", adjacent, false);
                ctx.Require(!(bool)Call(zone, "HasConnectedNoShoesContact", invalid),
                            "Removing neighboring no-shoes area failed to deactivate entrance");
                Call(zone, "SetNoShoes", diagonal, false);
                Call(zone, "SetNoShoes", gap, false);

                Call(zone, "Set", valid, true);
                Call(zone, "SetNoShoes", valid + IntVec3.East, true);
                ctx.Require((bool)Call(zone, "HasConnectedNoShoesContact", valid),
                            "Control: independent valid entrance not active");

                PawnKindDef kind = DefDatabase<PawnKindDef>.GetNamed("Colonist");
                pawn = PawnGenerator.GeneratePawn(kind, Faction.OfPlayer);
                ctx.Require(pawn != null, "Pawn could not generate");
                GenSpawn.Spawn(pawn, origin, map, WipeMode.Vanish);
                destination = origin + new IntVec3(8, 0, 0);
                Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
                Walk(pawn, destination);
                Log.Message("AMJ_CULTURE_ENTRANCE_REMAINING_START invalid=" + invalid +
                            " valid=" + valid + " destination=" + destination);
            });
            for (int n = 0; n < 70; n++)
            {
                await ctx.WaitTicks(15);
                await EntranceMainThread.Run(delegate
                {
                    if (pawn == null || !pawn.Spawned) return;
                    observedCast |= Casting(state, pawn);
                    arrived |= pawn.Position == destination;
                });
                if (arrived) break;
            }
            await EntranceMainThread.Run(delegate
            {
                ctx.Require(arrived, "Pawn did not complete travel past nonadjacent entrance");
                ctx.Require(!observedCast && !Barefoot(state, pawn),
                            "Invalid entrance performed a shoe change");

                // Verify cancellation of an in-progress cast when the author
                // edits the mapped no-shoes boundary. No tick benchmark here.
                MethodInfo begin = state.GetType().GetMethod("BeginCast",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                ctx.Require(begin != null, "Cast state API missing");
                begin.Invoke(state, new object[] { pawn, true, Find.TickManager.TicksGame });
                ctx.Require(Casting(state, pawn), "Cast fixture could not start");
                Call(zone, "SetNoShoes", origin + new IntVec3(10, 0, 3), false);
                ctx.Require(!Casting(state, pawn),
                            "Editing no-shoes zone failed to cancel pending cast");

                // Restore the edited contact before saving and before the drafted
                // crossing. Otherwise the fixture tests a disconnected entrance.
                Call(zone, "SetNoShoes", origin + new IntVec3(10, 0, 3), true);
                ctx.Require((bool)Call(zone, "HasConnectedNoShoesContact",
                                      origin + new IntVec3(9, 0, 3)),
                            "Saved control entrance must be active after cancellation");

                // Emulate shoes carrying both dirt and blood: only dirt must
                // be discarded when a successful cast switches to barefoot.
                FieldInfo carriedField = typeof(Pawn_FilthTracker).GetField("carriedFilth",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                ctx.Require(carriedField != null, "Carried filth tracker field missing");
                IList carried = carriedField.GetValue(pawn.filth) as IList;
                ctx.Require(carried != null, "Carried filth is not a supported list");
                ThingDef dirtDef = DefDatabase<ThingDef>.GetNamedSilentFail("Filth_Dirt");
                ThingDef bloodDef = DefDatabase<ThingDef>.GetNamedSilentFail("Filth_Blood");
                ctx.Require(dirtDef != null && bloodDef != null,
                            "Vanilla dirt/blood filth defs missing");
                Filth dirt = ThingMaker.MakeThing(dirtDef) as Filth;
                Filth blood = ThingMaker.MakeThing(bloodDef) as Filth;
                ctx.Require(dirt != null && blood != null, "Filth fixtures not constructible");
                carried.Add(dirt);
                carried.Add(blood);
                MethodInfo set = state.GetType().GetMethod("SetBarefoot",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                ctx.Require(set != null, "Barefoot update method missing");
                set.Invoke(state, new object[] { pawn, true });
                ctx.Require(Barefoot(state, pawn), "Pawn barefoot state did not change");
                ctx.Require(!carried.Contains(dirt), "Dirt remained carried after shoes removed");
                ctx.Require(carried.Contains(blood), "Blood was incorrectly removed with dirt");
                Log.Message("AMJ_CULTURE_ENTRANCE_BOUNDARY_FILTH_OK");

                // Persist a test-only identity and the actual boundary for second-process reload.
                EntranceReloadProbe probe = Current.Game.GetComponent<EntranceReloadProbe>();
                ctx.Require(probe != null, "Test-only reload probe missing from game");
                probe.Subject = pawn;
                probe.EntranceCell = origin + new IntVec3(9, 0, 3);
                probe.NoShoesCell = probe.EntranceCell + IntVec3.East;
                probe.ResumeTarget = origin + new IntVec3(11, 0, 3);
                ctx.Require(probe.ResumeTarget.Standable(Find.CurrentMap),
                            "No open post-reload Goto target");

                // Real game save to the isolated temporary profile; verify
                // both zone keys and pawn state serialized to the .rws file.
                string name = "AMJEntrance16BoundaryFunctional";
                MethodInfo save = typeof(GameDataSaveLoader).GetMethod("SaveGame",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                    null, new[] { typeof(string) }, null);
                ctx.Require(save != null, "Native SaveGame API missing");
                save.Invoke(null, new object[] { name });
                string file = Path.Combine(GenFilePaths.SaveDataFolderPath,
                                            "Saves", name + ".rws");
                ctx.Require(File.Exists(file), "Native game save file not produced");
                string xml = File.ReadAllText(file);
                ctx.Require(xml.Contains("AMJCultureEntranceCells") &&
                            xml.Contains("AMJCultureNoShoesCells") &&
                            xml.Contains("AMJCultureBarefootPawnIds"),
                            "Real save omitted Culture threshold/area/footwear keys");
                Log.Message("AMJ_CULTURE_ENTRANCE_SAVE_KEYS_OK");
                Log.Message("AMJ_CULTURE_ENTRANCE_REMAINING_OK");
            });
        }

        [Then("drafted crossing does not delay emergency movement")]
        public async Task CheckDraftedCrossing(PickleContext ctx)
        {
            Pawn drafted = null;
            object state = null;
            IntVec3 destination = IntVec3.Invalid;
            bool arrived = false, everPaused = false, everBarefoot = false;
            await EntranceMainThread.Run(delegate
            {
                Map map = Find.CurrentMap;
                ctx.Require(map != null, "Drafted test map not present");
                EntranceReloadProbe probe = Current.Game.GetComponent<EntranceReloadProbe>();
                ctx.Require(probe != null && probe.EntranceCell.IsValid,
                            "Drafted test entrance not prepared");
                state = Component(Current.Game, "EntranceFootwearState");
                ctx.Require(state != null, "Footwear state unavailable");
                IntVec3 start = probe.EntranceCell + IntVec3.West;
                destination = probe.EntranceCell + new IntVec3(3, 0, 0);
                ctx.Require(start.Standable(map) && destination.Standable(map),
                            "Drafted crossing cells blocked");
                drafted = PawnGenerator.GeneratePawn(
                    DefDatabase<PawnKindDef>.GetNamed("Colonist"), Faction.OfPlayer);
                ctx.Require(drafted != null, "Drafted colonist not generated");
                GenSpawn.Spawn(drafted, start, map, WipeMode.Vanish);
                ctx.Require(drafted.drafter != null, "Drafted colonist has no drafter");
                drafted.drafter.Drafted = true;
                Walk(drafted, destination);
                Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
            });
            for (int n = 0; n < 95; n++)
            {
                await ctx.WaitTicks(12);
                await EntranceMainThread.Run(delegate
                {
                    if (drafted == null || !drafted.Spawned) return;
                    everPaused |= Casting(state, drafted);
                    everBarefoot |= Barefoot(state, drafted);
                    arrived |= drafted.Position == destination;
                });
                if (arrived) break;
            }
            await EntranceMainThread.Run(delegate
            {
                ctx.Require(arrived, "Drafted Goto did not complete through threshold");
                ctx.Require(!everPaused && !everBarefoot,
                            "Drafted pawn blocked by shoe-changing cast");
                Log.Message("AMJ_CULTURE_ENTRANCE16_DRAFTED_BYPASS_OK");
            });
        }
    }
}
