using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AMJ.Culture
{
    // Do not change PawnKindDef.chemicalAddictionChance globally: alcohol
    // addiction is historically appropriate and must still generate.
    //
    // GenerateInitialHediffs is called ONLY while a fresh Pawn is being
    // assembled. Saved Pawns and redressed/reused Pawns never pass through
    // this stage. An intentional ScenarioPart hediff is applied later and
    // must remain under the scenario author's control.
    [HarmonyPatch(typeof(PawnGenerator), "GenerateInitialHediffs")]
    public static class InitialDrugAddictionGenerationPatch
    {
        // These are ChemicalDef identities, not Drug ThingDef identities.
        // Multiple preparations share one chemical (all psychite drugs).
        // Missing MO chemicals are ignored for Vanilla-only profiles.
        private static readonly string[] ExcludedChemicalNames =
        {
            "Ambrosia",
            "Smokeleaf",
            "Psychite",
            "WakeUp",
            "GoJuice",
            "DankPyon_Chemical_HappyPowder",
            "DankPyon_Chemical_VigorPill",
            "DankPyon_Chemical_BattleElixir",
            "DankPyon_Chemical_Stardust",
            "DankPyon_Chemical_Luhix",
            "DankPyon_HornetHoney"
        };

        private static HashSet<HediffDef> excludedInitialHediffs;

        private static HashSet<HediffDef> ExcludedInitialHediffs
        {
            get
            {
                if (excludedInitialHediffs == null)
                {
                    HashSet<HediffDef> defs = new HashSet<HediffDef>();
                    foreach (string chemicalName in ExcludedChemicalNames)
                    {
                        ChemicalDef chemical =
                            DefDatabase<ChemicalDef>.GetNamedSilentFail(chemicalName);
                        if (chemical == null)
                            continue;

                        if (chemical.addictionHediff != null)
                            defs.Add(chemical.addictionHediff);
                        // An initial tolerance accompanies an initial
                        // addiction, not a legitimately consumed old drug.
                        if (chemical.toleranceHediff != null)
                            defs.Add(chemical.toleranceHediff);
                    }
                    excludedInitialHediffs = defs;
                }
                return excludedInitialHediffs;
            }
        }

        // __0 binds the first argument without depending on a possibly
        // renamed private parameter in RimWorld 1.6/1.7.
        public static void Postfix(Pawn __0)
        {
            if (__0 == null || __0.RaceProps == null ||
                !__0.RaceProps.Humanlike || __0.health == null ||
                __0.health.hediffSet == null)
                return;

            List<Hediff> hediffs = __0.health.hediffSet.hediffs;
            HashSet<HediffDef> excluded = ExcludedInitialHediffs;

            // Reverse order because RemoveHediff mutates the health list.
            for (int i = hediffs.Count - 1; i >= 0; i--)
            {
                Hediff hediff = hediffs[i];
                if (hediff != null && excluded.Contains(hediff.def))
                    __0.health.RemoveHediff(hediff);
            }
        }
    }
}
