using System.Collections.Generic;
using HarmonyLib;
using RimWorld;

namespace AMJ.Culture
{
    // A Solid biography is a fixed name + childhood + adulthood pair.
    // RimWorld checks the ADULT background's category when it selects the
    // biography, so adding spawnCategories to the child Def does not block it.
    // Reject only the selected incompatible biography and let Vanilla use its
    // existing shuffled-bio fallback. Leave BackstoryDefs and saves untouched.
    [HarmonyPatch(typeof(PawnBioAndNameGenerator), "TryGetRandomUnusedSolidBioFor")]
    public static class SolidChildBioGatePatch
    {
        private static readonly HashSet<string> ExcludedChildhoods =
            new HashSet<string>
            {
                "ComputerGeek62",
                "GlitterworldKid85",
                "GlitterworldNerd20",
                "MechanoidNerd10",
                "SpaceCadet77",
                "SpaceFanboy22",
                "SpaceNerd97",
                "CorporateSlave22",
                "HackerKid98",
                "HackerProdigy84",
                "MechanoidHacker93",
                "VideoGamer16"
            };

        public static void Postfix(ref bool __result, ref PawnBio result)
        {
            if (!__result || result == null || result.childhood == null)
                return;

            if (ExcludedChildhoods.Contains(result.childhood.defName))
            {
                result = null;
                __result = false;
            }
        }
    }
}
