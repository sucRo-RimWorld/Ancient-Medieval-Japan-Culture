using System.Collections.Generic;
using HarmonyLib;
using RimWorld;

namespace AMJ.Culture.Characters
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
                "VideoGamer16",
                "FallenProdigy40",
                "ChildResearcher32",
                "HeadjackAddict4",
                "WealthyStudent59",
                "CorpBredStudent54",
                "CorpStudent95",
                "OffworldRecruit91",
                "AwkwardNerd48",
                "DataDecoder39",
                "Bookworm3",
                "FarmMechanic1",
                "DiscardedYouth93",
                "CloneFarmed21",
                "VatgrownAssassin20",
                "VatgrownScientist91",
                "JoywireAddict76",
                "LabGrownChild22",
                "DreadedBaby56",
                "GameFanatic86",
                "ReclusiveProdigy96",
                "AbandonedChild30",
                "AspiringPhysicist47",
                "CaravanChild53",
                "ChildScientist30",
                "Colonist22",
                "CrimeBossChild39",
                "DiplomatsChild97",
                "FeralChild85",
                "LogicalChild2",
                "MidworldCadet83",
                "MidworldGeek18",
                "PilotFan16",
                "ProfessionalGamer76",
                "RebelWriter82",
                "ShipChild46",
                "SocialPariah3",
                "StarforceCadet79",
                "StudentEngineer34",
                "ToxicChild96",
                "VatgrownSlavegirl8",
                "VideoGamer55",
                "VidtubeStar98",
                "VoyagerChild94"
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
