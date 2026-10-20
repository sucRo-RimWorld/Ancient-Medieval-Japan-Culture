using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using RimWorks.Pickle;
using RimWorld;
using Verse;

namespace AMJ.Culture.Test
{
    [PickleSteps]
    public sealed class RefinedFoodSteps
    {
        private static readonly string[] MemoryNames = {
            "AMJ_ExpectedRefinedFoodBrown", "AMJ_ExpectedRefinedFoodCoarse",
            "AMJ_GourmandCoarseRice", "AMJ_GourmandCoarseGrains"
        };

        private static Task OnGameThread(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            Type driver = typeof(PickleContext).Assembly.GetType(
                "RimWorks.Pickle.Runtime.PickleDriver", true);
            MethodInfo post = driver.GetMethod("Post", new[] { typeof(Action) });
            if (post == null) throw new MissingMethodException(driver.FullName, "Post");
            post.Invoke(null, new object[] { (Action)delegate {
                try { action(); tcs.SetResult(true); }
                catch (Exception e) { tcs.SetException(e); }
            } });
            return tcs.Task;
        }

        private static MethodInfo PatchMethod(string method)
        {
            Type type = Type.GetType(
                "AMJ.Culture.RefinedFoodExpectationPatch, AMJ.Culture", true);
            MethodInfo found = type.GetMethod(method, BindingFlags.Public |
                BindingFlags.NonPublic | BindingFlags.Static);
            if (found == null) throw new MissingMethodException(type.FullName, method);
            return found;
        }

        private static int Coarseness(Thing thing)
        {
            return (int)PatchMethod("Coarseness").Invoke(null, new object[] { thing });
        }

        private static bool Abundant(Map map)
        {
            return (bool)PatchMethod("HasAbundantSupplies").Invoke(null, new object[] { map });
        }

        private static void Apply(Thing food, Pawn pawn, float consumed = 0.5f)
        {
            PatchMethod("Postfix").Invoke(null, new object[] { food, pawn, consumed });
        }

        private static List<Thought> OurThoughts(Pawn pawn)
        {
            var thoughts = new List<Thought>();
            pawn.needs.mood.thoughts.GetAllMoodThoughts(thoughts);
            return thoughts.Where(x => x.def != null &&
                MemoryNames.Contains(x.def.defName)).ToList();
        }

        private static void Clear(Pawn pawn)
        {
            foreach (string name in MemoryNames)
            {
                ThoughtDef def = DefDatabase<ThoughtDef>.GetNamedSilentFail(name);
                if (def != null) pawn.needs.mood.thoughts.memories.RemoveMemoriesOfDef(def);
            }
        }

        private static void Expect(PickleContext ctx, Pawn pawn, string expected, int amount)
        {
            var thoughts = OurThoughts(pawn);
            ctx.Require(thoughts.Count == (expected == null ? 0 : 1),
                "Unexpected expectation memory count: " + string.Join(",",
                  thoughts.Select(x => x.def.defName)));
            if (expected != null)
                ctx.Require(thoughts[0].def.defName == expected &&
                    Math.Abs(thoughts[0].MoodOffset() - amount) < 0.01f,
                    "Expectation Thought identity/effect differs: " +
                    thoughts[0].def.defName + "=" + thoughts[0].MoodOffset());
        }

        private static Thing Food(string defName, string grain = null)
        {
            ThingDef def = DefDatabase<ThingDef>.GetNamedSilentFail(defName);
            if (def == null) throw new InvalidOperationException("Missing food " + defName);
            Thing food = ThingMaker.MakeThing(def);
            if (grain != null)
            {
                var ingredients = food.TryGetComp<CompIngredients>();
                if (ingredients == null || ingredients.ingredients == null)
                    throw new InvalidOperationException("Missing recorded ingredients " + defName);
                ThingDef ingredient = DefDatabase<ThingDef>.GetNamedSilentFail(grain);
                if (ingredient == null)
                    throw new InvalidOperationException("Missing real grain ingredient " + grain);
                ingredients.ingredients.Add(ingredient);
            }
            return food;
        }

        private static void SetTraits(Pawn pawn, params string[] names)
        {
            pawn.story.traits.allTraits.Clear();
            foreach (string name in names)
            {
                TraitDef def = DefDatabase<TraitDef>.GetNamedSilentFail(name);
                if (def == null) throw new InvalidOperationException("Missing Trait " + name);
                pawn.story.traits.allTraits.Add(new Trait(def, 0));
            }
        }

        private static void SeedFood(Map map, List<Thing> staged, int count)
        {
            ThingDef meal = DefDatabase<ThingDef>.GetNamed("MealSimple");
            var cells = new List<IntVec3>();
            for (int z = 3; z < map.Size.z - 3; z++)
                for (int x = 3; x < map.Size.x - 3; x++)
                {
                    var cell = new IntVec3(x, 0, z);
                    if (cell.Standable(map) && cell.GetFirstItem(map) == null)
                        cells.Add(cell);
                }
            if (cells.Count < count / Math.Max(1, meal.stackLimit) + 5)
                throw new InvalidOperationException("Insufficient stock staging cells");
            var stockpile = new Zone_Stockpile((StorageSettingsPreset)0, map.zoneManager);
            map.zoneManager.RegisterZone(stockpile);
            int remaining = count;
            for (int i = 0; remaining > 0; i++)
            {
                stockpile.AddCell(cells[i]);
                Thing item = ThingMaker.MakeThing(meal);
                item.stackCount = Math.Min(remaining, meal.stackLimit);
                GenSpawn.Spawn(item, cells[i], map);
                staged.Add(item);
                remaining -= item.stackCount;
            }
        }

        private static void Refresh(Map map)
        {
            object counter = map.resourceCounter;
            MethodInfo update = counter.GetType().GetMethods(BindingFlags.Public |
                BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(
                x => x.Name == "UpdateResourceCounts" && x.GetParameters().Length == 0);
            if (update != null) update.Invoke(counter, new object[0]);
            // The 12-day path short-circuits before the wealth threshold.
            // Forcing a full map wealth recount during a Pickle game-thread
            // callback is unnecessary and can hold the game loop for too long.
        }

        [Then("refined food expectations obey Grains and meal constraints")]
        public async Task Verify(PickleContext ctx)
        {
            await ctx.WaitUntil(() => {
                Type t = Type.GetType(
                    "AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                PropertyInfo property = t == null ? null : t.GetProperty(
                    "IsPostLoaded", BindingFlags.Public | BindingFlags.Static);
                return property != null && (bool)property.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);

            Pawn pawn = null;
            Map map = null;
            bool grains = false;
            var foodStocks = new List<Thing>();
            await OnGameThread(() => {
                map = Find.CurrentMap;
                ctx.Require(map != null, "No original Quickstart map");
                grains = ModsConfig.IsActive("sucro.ancientmedievaljapan.core");
                foreach (string name in MemoryNames)
                {
                    ThoughtDef memory = DefDatabase<ThoughtDef>.GetNamedSilentFail(name);
                    ctx.Require(memory != null && memory.stages.Count == 1 &&
                        Math.Abs(memory.durationDays - 0.5f) < 0.001f,
                        "Culture food Thought missing/wrong duration: " + name);
                }
                PawnKindDef colonist = DefDatabase<PawnKindDef>.GetNamed("Colonist");
                pawn = PawnGenerator.GeneratePawn(colonist, Faction.OfPlayer);
                ctx.Require(pawn != null && pawn.story != null && pawn.needs?.mood != null,
                    "Fresh player pawn lacks mood");
                IntVec3 site = IntVec3.Invalid;
                for (int z = 3; z < map.Size.z - 3 && !site.IsValid; z++)
                    for (int x = 3; x < map.Size.x - 3; x++)
                        if (new IntVec3(x,0,z).Standable(map)) {
                            site = new IntVec3(x,0,z); break;
                        }
                ctx.Require(site.IsValid, "No pawn spawn cell");
                GenSpawn.Spawn(pawn, site, map);
                ctx.Require(pawn.Spawned && pawn.Faction == Faction.OfPlayer,
                    "Pawn spawn failed");
                pawn.story.Childhood = DefDatabase<BackstoryDef>.GetNamed("PrivilegedProdigy70");
                SetTraits(pawn);
                ctx.Require(map.mapPawns.ColonistsSpawnedCount > 0, "No colonists count");
                Clear(pawn);
                if (!grains)
                {
                    // A temporary test-only rice Def mirrors the real Grains ID.
                    // The package-id guard must prevent any memory, even though
                    // Coarseness would be 1 and the consumed result is positive.
                    Thing rice = Food("AMJC_RiceMeal");
                    ctx.Require(Coarseness(rice) == 1, "Fixture rice not classified");
                    Apply(rice, pawn);
                    Expect(ctx, pawn, null, 0);
                    Log.Message("AMJ_CULTURE_REFINED_NO_GRAINS_OK");
                }
                else
                {
                    ctx.Require(Coarseness(Food("AMJC_RiceMeal")) == 1,
                        "Legacy brown rice classification");
                    ctx.Require(Coarseness(Food("AMJC_MixedGrainMeal", "RawRice")) == 1,
                        "Grains brown mixed bowl classification");
                    ctx.Require(Coarseness(Food("AMJC_MixedGrainMeal", "AMJC_Millet")) == 2,
                        "Grains millet bowl classification");
                    ctx.Require(Coarseness(Food("AMJC_MixedGrainPorridge", "AMJC_Barley")) == 2,
                        "Grains barley porridge classification");
                    ctx.Require(Coarseness(Food("AMJC_MixedGrainMeal", "AMJC_WhiteRice")) == 0,
                        "All-white mixed bowl must be exempt");
                    ctx.Require(Coarseness(Food("AMJC_WhiteRiceMeal")) == 0 &&
                        Coarseness(Food("AMJC_WhiteRicePorridge")) == 0,
                        "White rice products must be exempt");
                    ctx.Require(Coarseness(Food("AMJC_MixedGrainMeal")) == 0,
                        "Unrecorded ingredients must not be guessed");
                    Apply(Food("AMJC_MixedGrainMeal", "AMJC_Millet"), pawn, 0f);
                    Expect(ctx, pawn, null, 0);
                    // An original quickstart starts without player food stocks.
                    // A no-surplus meal must never create a new penalty.
                    Refresh(map);
                    ctx.Require(!Abundant(map),
                        "Initial Quickstart resource counter unexpectedly abundant");
                    Apply(Food("AMJC_RiceMeal"), pawn);
                    Expect(ctx, pawn, null, 0);
                    Log.Message("AMJ_CULTURE_REFINED_SHORTAGE_OK");
                    int colonists = map.mapPawns.ColonistsSpawnedCount;
                    Log.Message("AMJ_CULTURE_REFINED_STOCKPILE_START");
                    SeedFood(map, foodStocks, (int)Math.Ceiling(
                        colonists * 1.6f * 12f / 0.9f) + 20);
                    Log.Message("AMJ_CULTURE_REFINED_STOCKPILE_READY");
                    Refresh(map);
                    Log.Message("AMJ_CULTURE_REFINED_RESOURCE_REFRESH_READY");
                    ctx.Require(Abundant(map),
                        "12-day real food supplies did not reach abundance threshold: " +
                         map.resourceCounter.TotalHumanEdibleNutrition + " / colonists=" + colonists);
                }
            });
            if (!grains) return;
            await ctx.WaitTicks(2);
            await OnGameThread(() => {
                ctx.Require(Abundant(map), "Resource cache unexpectedly stale");
                SetTraits(pawn);
                Clear(pawn);
                Apply(Food("AMJC_RiceMeal"), pawn);
                Expect(ctx, pawn, "AMJ_ExpectedRefinedFoodBrown", -1);
                Apply(Food("AMJC_MixedGrainMeal", "AMJC_Millet"), pawn);
                Expect(ctx, pawn, "AMJ_ExpectedRefinedFoodCoarse", -2);
                SetTraits(pawn, "Gourmand");
                Clear(pawn);
                Apply(Food("AMJC_RiceMeal"), pawn);
                Expect(ctx, pawn, "AMJ_GourmandCoarseRice", -2);
                Apply(Food("AMJC_MixedGrainPorridge", "AMJC_Barley"), pawn);
                Expect(ctx, pawn, "AMJ_GourmandCoarseGrains", -3);
                Apply(Food("AMJC_MixedGrainMeal", "AMJC_Millet"), pawn);
                Expect(ctx, pawn, "AMJ_GourmandCoarseGrains", -3);
                SetTraits(pawn, "Gourmand", "Ascetic");
                Clear(pawn);
                Apply(Food("AMJC_MixedGrainMeal", "AMJC_Millet"), pawn);
                Expect(ctx, pawn, null, 0);
                SetTraits(pawn, "Gourmand");
                Clear(pawn);
                Apply(Food("AMJC_WhiteRiceMeal"), pawn);
                Apply(Food("AMJC_MixedGrainMeal", "AMJC_WhiteRice"), pawn);
                Apply(Food("AMJC_MixedGrainMeal"), pawn);
                Expect(ctx, pawn, null, 0);
                Log.Message("AMJ_CULTURE_REFINED_GRAINS_SURPLUS_MOOD_OK");
                // Does not claim an observed ordinary Ingest job: the loaded game's
                // real Postfix and MemoryThoughtHandler are called directly, isolating
                // the meal expectation from chair/job orchestration.
            });
        }
    }
}
