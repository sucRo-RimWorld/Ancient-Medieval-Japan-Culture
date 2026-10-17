using System;
using System.Collections;
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
    public sealed class Background16Steps
    {
        private static readonly string[] Adult =
        {
            "FerventResearcher72", "AerospaceEngineer44", "ComputerEngineer10",
            "ComputerEngineer36", "DeepSpaceSurveyor1", "GlitterworldSurgeon15",
            "OrbitalReservist22", "SpaceMarine51", "SpaceMarineMedic10",
            "SpaceNavyTech37", "SpaceResearcher25", "SpaceTechnician29",
            "SpaceshipSalesman34", "StarshipJanitor33", "AIProgrammer22",
            "AIResearcher94", "AceFighterPilot54", "ArchotechSpy75",
            "BattleMechanic27", "BattlefieldTech52", "CombatMedtech59",
            "CorpResearcher71", "EngineeredPilot75", "GeneticEngineer89",
            "GeneticScientist66", "MechWarVeteran77", "NetworkEngineer34",
            "Roboticist1", "Roboticist41", "RocketEngineer53", "RocketPioneer19"
        };

        private static readonly HashSet<string> Child = new HashSet<string>
        {
            "ComputerGeek62", "GlitterworldKid85", "GlitterworldNerd20",
            "MechanoidNerd10", "SpaceCadet77", "SpaceFanboy22",
            "SpaceNerd97", "CorporateSlave22", "HackerKid98",
            "HackerProdigy84", "MechanoidHacker93", "VideoGamer16"
        };

        private static Task OnGameThread(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            var driver = typeof(PickleContext).Assembly.GetType(
                "RimWorks.Pickle.Runtime.PickleDriver", true);
            var post = driver.GetMethod("Post", new[] { typeof(Action) });
            if (post == null)
                throw new MissingMethodException(driver.FullName, "Post(Action)");
            post.Invoke(null, new object[] { new Action(delegate
            {
                try
                {
                    if (!UnityData.IsInMainThread)
                        throw new InvalidOperationException("Not on Unity game thread");
                    action();
                    tcs.SetResult(true);
                }
                catch (Exception error) { tcs.SetException(error); }
            }) });
            return tcs.Task;
        }

        [Then("anachronistic backstories are restricted and ordinary pawns still generate")]
        public async Task Check(PickleContext ctx)
        {
            await ctx.WaitUntil(() =>
            {
                var ready = Type.GetType("AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                var prop = ready == null ? null : ready.GetProperty(
                    "IsPostLoaded", BindingFlags.Public | BindingFlags.Static);
                return prop != null && (bool)prop.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);
            await OnGameThread(delegate
            {
                ctx.Require(Adult.Length == 31 && Child.Count == 12,
                    "Incorrect feature population");
                foreach (string id in Adult)
                {
                    BackstoryDef backstory = DefDatabase<BackstoryDef>.GetNamedSilentFail(id);
                    ctx.Require(backstory != null, "Missing adult Def: " + id);
                    ctx.Require(backstory.spawnCategories != null &&
                        backstory.spawnCategories.Count == 1 &&
                        backstory.spawnCategories[0] == "AMJ_CultureExcluded",
                        "Adult category not restricted: " + id);
                }
                foreach (string id in Child)
                {
                    BackstoryDef backstory = DefDatabase<BackstoryDef>.GetNamedSilentFail(id);
                    ctx.Require(backstory != null, "Saved child Def identity missing: " + id);
                }
                TraitDef trait = DefDatabase<TraitDef>.GetNamedSilentFail("Transhumanist");
                ctx.Require(trait != null && trait.commonality == 0f,
                    "Random Transhumanist commonality not suppressed");
                ctx.Require(DefDatabase<TraitDef>.GetNamedSilentFail("BodyPurist") != null,
                    "BodyPurist Def identity was lost");
                Log.Message("AMJ_CULTURE_BACKGROUNDS16_LOADED_OK adult=" + Adult.Length
                    + " child=" + Child.Count);

                Assembly culture = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(x => x.GetName().Name == "AMJ.Culture");
                ctx.Require(culture != null, "Compiled Culture DLL not loaded");
                Type gate = culture.GetType("AMJ.Culture.SolidChildBioGatePatch");
                ctx.Require(gate != null, "Solid child production gate missing");
                MethodInfo method = gate.GetMethod("Postfix",
                    BindingFlags.Public | BindingFlags.Static);
                ctx.Require(method != null, "Solid child gate handler missing");
                MethodInfo target = typeof(PawnBioAndNameGenerator).GetMethod(
                    "TryGetRandomUnusedSolidBioFor",
                    BindingFlags.NonPublic | BindingFlags.Static);
                ctx.Require(target != null, "Vanilla solid selection target missing");

                Assembly harmony = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(x => x.GetName().Name == "0Harmony");
                ctx.Require(harmony != null, "Harmony assembly missing");
                Type harmonyType = harmony.GetType("HarmonyLib.Harmony");
                MethodInfo patchInfoMethod = harmonyType == null ? null :
                    harmonyType.GetMethod("GetPatchInfo",
                        BindingFlags.Public | BindingFlags.Static);
                ctx.Require(patchInfoMethod != null, "Harmony patch introspection unavailable");
                object info = patchInfoMethod.Invoke(null, new object[] { target });
                ctx.Require(info != null, "Vanilla solid selection method is not patched");
                object postfixes = info.GetType().GetProperty("Postfixes")?.GetValue(info, null);
                if (postfixes == null)
                    postfixes = info.GetType().GetField("Postfixes")?.GetValue(info);
                ctx.Require(postfixes is IEnumerable, "Harmony postfix list not available");
                bool owned = false;
                foreach (object patch in (IEnumerable)postfixes)
                {
                    object owner = patch.GetType().GetField("owner")?.GetValue(patch);
                    if (owner == null)
                        owner = patch.GetType().GetProperty("owner")?.GetValue(patch, null);
                    if ((owner as string) == "sucro.ancientmedievaljapan.culture.mealseat")
                        owned = true;
                }
                ctx.Require(owned, "Compiled Culture postfix was not registered");

                PawnBio blocked = SolidBioDatabase.allBios
                    .FirstOrDefault(x => x.childhood != null && Child.Contains(x.childhood.defName));
                PawnBio allowed = SolidBioDatabase.allBios
                    .FirstOrDefault(x => x.childhood != null && !Child.Contains(x.childhood.defName));
                ctx.Require(blocked != null && allowed != null,
                    "SolidBioDatabase lacks a suitable real blocked/allowed sample");
                object[] rejected = { true, blocked };
                method.Invoke(null, rejected);
                ctx.Require(!(bool)rejected[0] && rejected[1] == null,
                    "Blocked fixed child biography was not rejected");
                object[] retained = { true, allowed };
                method.Invoke(null, retained);
                ctx.Require((bool)retained[0] && ReferenceEquals(retained[1], allowed),
                    "Historically allowed fixed biography changed");
                Log.Message("AMJ_CULTURE_BACKGROUNDS16_SOLID_OK blocked="
                    + blocked.childhood.defName + " allowed=" + allowed.childhood.defName);

                PawnKindDef colonist = DefDatabase<PawnKindDef>.GetNamedSilentFail("Colonist");
                ctx.Require(colonist != null, "Vanilla Colonist missing");
                Pawn pawn = PawnGenerator.GeneratePawn(colonist, Faction.OfPlayer);
                ctx.Require(pawn != null && pawn.story != null && pawn.story.Childhood != null,
                    "Ordinary player pawn generation failed");
                ctx.Require(!Child.Contains(pawn.story.Childhood.defName),
                    "Generated child biography is forbidden");
                ctx.Require(pawn.story.Adulthood == null ||
                    !Adult.Contains(pawn.story.Adulthood.defName),
                    "Generated adult biography is forbidden");
                Log.Message("AMJ_CULTURE_BACKGROUNDS16_PAWN_OK");
            });
        }
    }
}
