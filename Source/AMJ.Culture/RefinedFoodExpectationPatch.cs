using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AMJ.Culture
{
    // Event-only assessment: no per-tick work, no inventory/map scan and no
    // new MapComponent. The existing Culture Harmony bootstrap PatchAll loads it.
    [HarmonyPatch(typeof(Thing), "Ingested", new Type[] { typeof(Pawn), typeof(float) })]
    public static class RefinedFoodExpectationPatch
    {
        private const float NutritionPerColonistDay = 1.6f;
        private const float MinimumDaysWithWealth = 3f;
        private const float DaysWithNoWealthRequirement = 10f;
        private const float HighColonyWealth = 80000f;

        // Grains is optional. Its own About.xml retains the historical Core package ID.
        private const string GrainsPackageId = "sucro.ancientmedievaljapan.core";

        private const string BrownRiceMeal = "AMJC_RiceMeal";
        private const string BrownRicePorridge = "AMJC_RicePorridge";
        private const string MixedMeal = "AMJC_MixedGrainMeal";
        private const string MixedPorridge = "AMJC_MixedGrainPorridge";

        private static readonly HashSet<string> PrivilegedChildhoods =
            new HashSet<string>(StringComparer.Ordinal)
            {
                "PrivilegedProdigy70",
                "Pampered87",
                "CoreDilettante33",
                "StudentSocialite89"
            };

        private static readonly HashSet<string> CoarseGrainIngredients =
            new HashSet<string>(StringComparer.Ordinal)
            {
                "AMJC_Millet",
                "AMJC_Buckwheat",
                "AMJC_Barley",
                "AMJC_Wheat",
                "DankPyon_RawWheat"
            };

        private const string WealthyBrown = "AMJ_ExpectedRefinedFoodBrown";
        private const string WealthyCoarse = "AMJ_ExpectedRefinedFoodCoarse";
        private const string GourmandBrown = "AMJ_GourmandCoarseRice";
        private const string GourmandCoarse = "AMJ_GourmandCoarseGrains";

        // 0 = untracked, 1 = brown rice/low-polish, 2 = other coarse grains.
        internal static int Coarseness(Thing food)
        {
            if (food == null || food.def == null)
                return 0;

            string name = food.def.defName;
            if (name == BrownRiceMeal || name == BrownRicePorridge)
                return 1;

            if (name != MixedMeal && name != MixedPorridge)
                return 0;

            // Grains' common meals may be made from ONE grain, including
            // entirely white rice. Inspect only their already-recorded ingredient
            // Defs. Unknown/empty records produce no speculative penalty.
            CompIngredients comp = food.TryGetComp<CompIngredients>();
            if (comp == null || comp.ingredients == null ||
                comp.ingredients.Count == 0)
                return 0;

            bool hasBrownRice = false;
            for (int i = 0; i < comp.ingredients.Count; i++)
            {
                ThingDef ingredient = comp.ingredients[i];
                if (ingredient == null)
                    continue;
                if (CoarseGrainIngredients.Contains(ingredient.defName))
                    return 2;
                if (ingredient.defName == "RawRice")
                    hasBrownRice = true;
            }
            return hasBrownRice ? 1 : 0;
        }

        internal static bool HasPrivilegedFoodUpbringing(Pawn pawn)
        {
            return pawn != null && pawn.story != null &&
                pawn.story.Childhood != null &&
                PrivilegedChildhoods.Contains(pawn.story.Childhood.defName);
        }

        // Both values are precomputed by the game. The nutrition threshold
        // short-circuits BEFORE reading the wealth watcher whenever possible.
        internal static bool HasAbundantSupplies(Map map)
        {
            if (map == null || map.resourceCounter == null ||
                map.wealthWatcher == null || map.mapPawns == null)
                return false;

            int count = map.mapPawns.ColonistsSpawnedCount;
            if (count < 1)
                return false;
            float days = map.resourceCounter.TotalHumanEdibleNutrition /
                (count * NutritionPerColonistDay);

            if (days >= DaysWithNoWealthRequirement)
                return true;
            if (days < MinimumDaysWithWealth)
                return false;
            return map.wealthWatcher.WealthTotal >= HighColonyWealth;
        }

        // Original Thing.Ingested returns 0 for unsuccessful consumption.
        public static void Postfix(Thing __instance, Pawn ingester, float __result)
        {
            // Culture alone must never evaluate or grant Grains-specific coarse food thoughts.
            if (!ModsConfig.IsActive(GrainsPackageId))
                return;

            if (__result <= 0f || ingester == null ||
                ingester.Faction != Faction.OfPlayer || ingester.IsPrisoner ||
                ingester.IsSlave || ingester.Map == null ||
                ingester.story == null || ingester.story.traits == null ||
                ingester.needs == null || ingester.needs.mood == null)
                return;

            int coarse = Coarseness(__instance);
            if (coarse == 0)
                return;

            // Ascetics never receive an extra expectation penalty.
            if (ingester.story.traits.HasTrait(TraitDefOf.Ascetic))
                return;

            TraitDef gourmandDef = DefDatabase<TraitDef>.GetNamedSilentFail("Gourmand");
            bool gourmand = gourmandDef != null && ingester.story.traits.HasTrait(gourmandDef);
            if (!gourmand && !HasPrivilegedFoodUpbringing(ingester))
                return;

            if (!HasAbundantSupplies(ingester.Map))
                return;

            string memoryName = gourmand
                ? (coarse == 1 ? GourmandBrown : GourmandCoarse)
                : (coarse == 1 ? WealthyBrown : WealthyCoarse);

            ThoughtDef memory = DefDatabase<ThoughtDef>.GetNamedSilentFail(memoryName);
            if (memory == null)
                return;

            // Enforce exactly one expectation effect, including when a pawn
            // has both a privileged childhood and Gourmand. Refresh the
            // 12-hour memory rather than stacking separate meals.
            var memories = ingester.needs.mood.thoughts.memories;
            RemoveIfPresent(memories, WealthyBrown);
            RemoveIfPresent(memories, WealthyCoarse);
            RemoveIfPresent(memories, GourmandBrown);
            RemoveIfPresent(memories, GourmandCoarse);
            memories.TryGainMemory(memory);
        }

        private static void RemoveIfPresent(MemoryThoughtHandler memories, string name)
        {
            ThoughtDef memory = DefDatabase<ThoughtDef>.GetNamedSilentFail(name);
            if (memory != null)
                memories.RemoveMemoriesOfDef(memory);
        }
    }
}
