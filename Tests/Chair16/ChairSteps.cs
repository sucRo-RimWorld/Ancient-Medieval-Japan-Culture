using System;
using System.Reflection;
using System.Threading.Tasks;
using RimWorks.Pickle;
using RimWorld;
using Verse;
using Verse.AI;

namespace AMJ.Culture.Test
{
    internal static class GameThread
    {
        internal static Task Run(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            Action callback = delegate
            {
                try
                {
                    if (!UnityData.IsInMainThread)
                        throw new InvalidOperationException("Chair acceptance is not on the Unity main thread");
                    action();
                    tcs.SetResult(true);
                }
                catch (Exception e) { tcs.SetException(e); }
            };
            Type driver = typeof(PickleContext).Assembly.GetType("RimWorks.Pickle.Runtime.PickleDriver", true);
            MethodInfo post = driver.GetMethod("Post", new[] { typeof(Action) });
            if (post == null) throw new MissingMethodException(driver.FullName, "Post(Action)");
            post.Invoke(null, new object[] { callback });
            return tcs.Task;
        }
    }

    [PickleSteps]
    public sealed class ChairSteps
    {
        [Then("table-free chair dining gains real comfort")]
        public async Task CheckChair(PickleContext ctx)
        {
            Pawn pawn = null;
            Thing seat = null;
            Thing meal = null;
            Thing tableSeat = null;
            Thing diningTable = null;
            ThingDef tableDef = null;
            bool sawTableSeat = false, sawTableMeal = false;
            bool sawStandingMeal = false, sawFallbackMeal = false;
            float startComfort = 0f;
            bool sawMealOnSeat = false, sawComfort = false, sawFinish = false;
            // Quickstarts has delayed pawn setup after the map first appears.
            await ctx.WaitUntil(() =>
            {
                // Quickstart and Pickle steps are compiled as separate assemblies.
                Type readyClass = Type.GetType(
                    "AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                PropertyInfo readyFlag = readyClass == null ? null :
                    readyClass.GetProperty("IsPostLoaded", BindingFlags.Public | BindingFlags.Static);
                return readyFlag != null && (bool)readyFlag.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);
            await GameThread.Run(delegate
            {
                Map map = Find.CurrentMap;
                ctx.Require(map != null, "Quickstart map missing");
                ThingDef chair = DefDatabase<ThingDef>.GetNamedSilentFail("Stool");
                ThingDef wood = DefDatabase<ThingDef>.GetNamedSilentFail("WoodLog");
                ThingDef mealDef = DefDatabase<ThingDef>.GetNamedSilentFail("MealSimple");
                tableDef = DefDatabase<ThingDef>.GetNamedSilentFail("Table1x2c");
                PawnKindDef colonist = DefDatabase<PawnKindDef>.GetNamedSilentFail("Colonist");
                ThoughtDef thought = DefDatabase<ThoughtDef>.GetNamedSilentFail("AteWithoutTable");
                ctx.Require(chair != null && wood != null && tableDef != null &&
                            mealDef != null && colonist != null && thought != null,
                            "Culture chair, meal, colonist or Thought not loaded");
                ctx.Require(chair.building != null && chair.building.isSittable,
                            "Chair is not an actual sit-capable building");
                ctx.Require(chair.GetStatValueAbstract(StatDefOf.Comfort) > 0f,
                            "Chair has no Comfort stat");
                ctx.Require(thought.stages.Count > 0 && thought.stages[0].baseMoodEffect == 0f,
                            "Independent AteWithoutTable mood penalty remains");
                IntVec3 site = IntVec3.Invalid;
                for (int z = 5; z < map.Size.z - 5 && !site.IsValid; z++)
                    for (int x = 5; x < map.Size.x - 8; x++)
                    {
                        var p = new IntVec3(x, 0, z);
                        // Only cells used by the three focused dining situations:
                        // free chair, a more distant table-adjacent chair, then no chair.
                        IntVec3[] required = {
                            p, p + IntVec3.West, p + IntVec3.East,
                            p + new IntVec3(0, 0, 4),
                            p + new IntVec3(1, 0, 3),
                            p + new IntVec3(1, 0, 4),
                            p + new IntVec3(1, 0, 5)
                        };
                        bool clear = true;
                        foreach (IntVec3 cell in required)
                        {
                            if (!cell.InBounds(map) || !cell.Standable(map) ||
                                cell.GetEdifice(map) != null)
                            { clear = false; break; }
                        }
                        if (clear) { site = p; break; }
                    }
                ctx.Require(site.IsValid, "No clear three-cell site for the chair meal");
                seat = ThingMaker.MakeThing(chair, wood);
                if (chair.CanHaveFaction)
                    seat.SetFactionDirect(Faction.OfPlayer);
                seat = GenSpawn.Spawn(seat, site, map, WipeMode.Vanish);
                ctx.Require(seat != null && seat.Spawned, "Chair did not spawn");
                ctx.Require(seat.Faction == Faction.OfPlayer, "Player chair has no player faction");
                pawn = PawnGenerator.GeneratePawn(colonist, Faction.OfPlayer);
                ctx.Require(pawn != null, "Colonist Pawn generation failed");
                GenSpawn.Spawn(pawn, site + IntVec3.West, map, WipeMode.Vanish);
                ctx.Require(pawn.needs != null && pawn.needs.comfort != null,
                            "Pawn has no Comfort need");
                pawn.needs.comfort.CurLevel = 0.05f;
                startComfort = pawn.needs.comfort.CurLevel;
                meal = ThingMaker.MakeThing(mealDef);
                meal = GenSpawn.Spawn(meal, site + IntVec3.East, map, WipeMode.Vanish);
                ctx.Require(meal != null && meal.Spawned, "MealSimple did not spawn");
                Job ingest = JobMaker.MakeJob(JobDefOf.Ingest, meal);
                ingest.count = 1;
                Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
                pawn.jobs.StartJob(ingest, JobCondition.InterruptForced);
                Log.Message("AMJ_CULTURE_CHAIR16_START chair=" + site);
            });
            for (int batch = 0; batch < 25; batch++)
            {
                await ctx.WaitTicks(80);
                await GameThread.Run(delegate
                {
                    if (pawn == null || !pawn.Spawned) return;
                    if (pawn.CurJob != null && pawn.CurJob.def == JobDefOf.Ingest &&
                        pawn.Position == seat.Position)
                        sawMealOnSeat = true;
                    if (pawn.needs.comfort != null &&
                        pawn.needs.comfort.CurLevel > startComfort + 0.005f)
                        sawComfort = true;
                    if (meal == null || !meal.Spawned || meal.Destroyed)
                        sawFinish = true;
                });
                if (sawMealOnSeat && sawComfort && sawFinish) break;
            }
            await GameThread.Run(delegate
            {
                Log.Message("AMJ_CULTURE_CHAIR16_DIAGNOSTIC"
                    + " job=" + (pawn.CurJob == null ? "none" : pawn.CurJob.def.defName)
                    + " position=" + pawn.Position
                    + " seat=" + seat.Position
                    + " sawOnSeat=" + sawMealOnSeat
                    + " sawComfort=" + sawComfort
                    + " sawFinish=" + sawFinish
                    + " comfortBefore=" + startComfort
                    + " comfortAfter=" + (pawn.needs.comfort == null ? -1f : pawn.needs.comfort.CurLevel)
                    + " seatEdifice=" + (seat.Position.GetEdifice(seat.Map) == seat)
                    + " seatReservable=" + pawn.CanReserve(seat, 1, -1, null, false)
                    + " foodCarried=" + (pawn.carryTracker.CarriedThing != null));
                ctx.Require(sawMealOnSeat, "Pawn did not eat on table-free chair");
                ctx.Require(sawComfort, "Eating on chair did not increase Comfort");
                ctx.Require(sawFinish, "The meal was not consumed");
                Log.Message("AMJ_CULTURE_CHAIR16_TABLE_FREE_OK");
            });

            // The already-established standalone Stool is deliberately nearer.
            // Vanilla must still select the farther Stool adjoining a real table.
            await GameThread.Run(delegate
            {
                Map map = pawn.Map;
                ThingDef wood = DefDatabase<ThingDef>.GetNamed("WoodLog");
                ThingDef chairDef = DefDatabase<ThingDef>.GetNamed("Stool");
                IntVec3 nextSeat = seat.Position + new IntVec3(0, 0, 4);
                IntVec3 nextTable = seat.Position + new IntVec3(1, 0, 4);
                tableSeat = ThingMaker.MakeThing(chairDef, wood);
                if (chairDef.CanHaveFaction)
                    tableSeat.SetFactionDirect(Faction.OfPlayer);
                tableSeat = GenSpawn.Spawn(tableSeat, nextSeat, map, WipeMode.Vanish);
                diningTable = ThingMaker.MakeThing(tableDef, wood);
                if (tableDef.CanHaveFaction)
                    diningTable.SetFactionDirect(Faction.OfPlayer);
                diningTable = GenSpawn.Spawn(diningTable, nextTable, map, WipeMode.Vanish);
                ctx.Require(tableSeat != null && tableSeat.Spawned &&
                            diningTable != null && diningTable.Spawned &&
                            (tableSeat.Position + IntVec3.East).GetEdifice(map) == diningTable,
                            "Table-adjacent seat setup failed");
                meal = GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("MealSimple")),
                                     seat.Position + IntVec3.East, map, WipeMode.Vanish);
                Job second = JobMaker.MakeJob(JobDefOf.Ingest, meal);
                second.count = 1;
                pawn.jobs.StartJob(second, JobCondition.InterruptForced);
            });
            for (int batch = 0; batch < 25; batch++)
            {
                await ctx.WaitTicks(80);
                await GameThread.Run(delegate
                {
                    if (pawn == null || !pawn.Spawned) return;
                    if (pawn.CurJob != null && pawn.CurJob.def == JobDefOf.Ingest &&
                        pawn.Position == tableSeat.Position)
                        sawTableSeat = true;
                    if (meal == null || !meal.Spawned || meal.Destroyed)
                        sawTableMeal = true;
                });
                if (sawTableSeat && sawTableMeal) break;
            }
            await GameThread.Run(delegate
            {
                ctx.Require(sawTableSeat,
                            "Existing table-adjacent Stool was not prioritized over nearer table-free Stool");
                ctx.Require(sawTableMeal, "Meal at table-adjacent Stool did not finish");
                Log.Message("AMJ_CULTURE_CHAIR16_TABLE_PRECEDENCE_OK");
            });

            // No eligible chair at all: do not strand the normal ingest job.
            await GameThread.Run(delegate
            {
                seat.Destroy(DestroyMode.Vanish);
                tableSeat.Destroy(DestroyMode.Vanish);
                diningTable.Destroy(DestroyMode.Vanish);
                ctx.Require(seat.Destroyed && tableSeat.Destroyed &&
                            diningTable.Destroyed, "Seat removal setup failed");
                meal = GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("MealSimple")),
                                     pawn.Position + IntVec3.East, pawn.Map, WipeMode.Vanish);
                Job fallback = JobMaker.MakeJob(JobDefOf.Ingest, meal);
                fallback.count = 1;
                pawn.jobs.StartJob(fallback, JobCondition.InterruptForced);
            });
            for (int batch = 0; batch < 25; batch++)
            {
                await ctx.WaitTicks(80);
                await GameThread.Run(delegate
                {
                    if (pawn == null || !pawn.Spawned) return;
                    if (pawn.CurJob != null && pawn.CurJob.def == JobDefOf.Ingest &&
                        pawn.GetPosture() == PawnPosture.Standing)
                        sawStandingMeal = true;
                    if (meal == null || !meal.Spawned || meal.Destroyed)
                        sawFallbackMeal = true;
                });
                if (sawStandingMeal && sawFallbackMeal) break;
            }
            await GameThread.Run(delegate
            {
                ctx.Require(sawStandingMeal,
                            "Standing eating fallback without any chair was not observed");
                ctx.Require(sawFallbackMeal, "Meal without any chair did not finish");
                Log.Message("AMJ_CULTURE_CHAIR16_NO_CHAIR_FALLBACK_OK");
                Log.Message("AMJ_CULTURE_CHAIR16_MEAL_COMFORT_OK");
            });
        }
    }
}
