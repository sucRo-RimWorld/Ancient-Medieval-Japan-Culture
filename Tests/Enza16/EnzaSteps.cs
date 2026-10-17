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
                        throw new InvalidOperationException("Enza acceptance is not on the Unity main thread");
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
    public sealed class EnzaSteps
    {
        [Then("table-free enza dining gains real comfort")]
        public async Task CheckEnza(PickleContext ctx)
        {
            Pawn pawn = null;
            Thing seat = null;
            Thing meal = null;
            float startComfort = 0f;
            bool sawMealOnSeat = false, sawComfort = false, sawFinish = false;
            // Quickstarts has delayed pawn setup after the map first appears.
            await ctx.WaitUntil(() =>
            {
                // Quickstart and Pickle steps are compiled as separate assemblies.
                Type readyClass = Type.GetType(
                    "AMJ.Culture.Test.EnzaQuickstart, AMJ.Culture.Enza16.Quickstart");
                PropertyInfo readyFlag = readyClass == null ? null :
                    readyClass.GetProperty("IsPostLoaded", BindingFlags.Public | BindingFlags.Static);
                return readyFlag != null && (bool)readyFlag.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);
            await GameThread.Run(delegate
            {
                Map map = Find.CurrentMap;
                ctx.Require(map != null, "Quickstart map missing");
                ThingDef enza = DefDatabase<ThingDef>.GetNamedSilentFail("AMJ_Enza");
                ThingDef mealDef = DefDatabase<ThingDef>.GetNamedSilentFail("MealSimple");
                PawnKindDef colonist = DefDatabase<PawnKindDef>.GetNamedSilentFail("Colonist");
                ThoughtDef thought = DefDatabase<ThoughtDef>.GetNamedSilentFail("AteWithoutTable");
                ctx.Require(enza != null && mealDef != null && colonist != null && thought != null,
                            "Culture enza, meal, colonist or Thought not loaded");
                ctx.Require(enza.building != null && enza.building.isSittable,
                            "Enza is not an actual sit-capable building");
                ctx.Require(enza.GetStatValueAbstract(StatDefOf.Comfort) > 0f,
                            "Enza has no Comfort stat");
                ctx.Require(thought.stages.Count > 0 && thought.stages[0].baseMoodEffect == 0f,
                            "Independent AteWithoutTable mood penalty remains");
                IntVec3 site = IntVec3.Invalid;
                for (int z = 5; z < map.Size.z - 5 && !site.IsValid; z++)
                    for (int x = 5; x < map.Size.x - 8; x++)
                    {
                        var p = new IntVec3(x, 0, z);
                        if (p.Standable(map) && (p + IntVec3.East).Standable(map) &&
                            (p + IntVec3.West).Standable(map) &&
                            p.GetEdifice(map) == null && (p + IntVec3.East).GetEdifice(map) == null &&
                            (p + IntVec3.West).GetEdifice(map) == null)
                        { site = p; break; }
                    }
                ctx.Require(site.IsValid, "No clear three-cell site for the enza meal");
                seat = ThingMaker.MakeThing(enza);
                if (enza.CanHaveFaction)
                    seat.SetFactionDirect(Faction.OfPlayer);
                seat = GenSpawn.Spawn(seat, site, map, WipeMode.Vanish);
                ctx.Require(seat != null && seat.Spawned, "Enza did not spawn");
                ctx.Require(seat.Faction == Faction.OfPlayer, "Player enza has no player faction");
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
                Log.Message("AMJ_CULTURE_ENZA16_START enza=" + site);
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
                ctx.Require(sawMealOnSeat, "Pawn did not eat on table-free enza");
                ctx.Require(sawComfort, "Eating on enza did not increase Comfort");
                ctx.Require(sawFinish, "The meal was not consumed");
                Log.Message("AMJ_CULTURE_ENZA16_MEAL_COMFORT_OK");
            });
        }
    }
}
