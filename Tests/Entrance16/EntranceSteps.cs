using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
            MethodInfo method = owner.GetType().GetMethod("GetComponent", BindingFlags.Instance | BindingFlags.Public);
            if (method == null) throw new MissingMethodException(owner.GetType().FullName, "GetComponent");
            return method.MakeGenericMethod(type).Invoke(owner, null);
        }

        private static object Call(object owner, string method, params object[] args)
        {
            return owner.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.Public)
                .Invoke(owner, args);
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

                for (int z = 8; z < map.Size.z - 8 && !start.IsValid; z++)
                    for (int x = 8; x < map.Size.x - 19; x++)
                    {
                        IntVec3 candidate = new IntVec3(x, 0, z);
                        bool clear = true;
                        for (int dx = 0; dx <= 12 && clear; dx++)
                            for (int dz = -1; dz <= 4; dz++)
                            {
                                IntVec3 cell = candidate + new IntVec3(dx, 0, dz);
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

            for (int cycle = 0; cycle < 65; cycle++)
            {
                await ctx.WaitTicks(15);
                await EntranceMainThread.Run(delegate
                {
                    observedRemoveCast |= Casting(state, pawn) && !Barefoot(state, pawn);
                    observedPutOnCast |= Casting(state, pawn) && Barefoot(state, pawn);
                    observedBarefoot |= Barefoot(state, pawn);
                    observedExit |= pawn.Position == end;
                    observedShoes |= observedExit && !Barefoot(state, pawn);
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
                ctx.Require(pawn.Position == end, "Original Goto job never reached its destination");
                Log.Message("AMJ_CULTURE_ENTRANCE_FUNCTION_OK");
            });
        }
    }
}
