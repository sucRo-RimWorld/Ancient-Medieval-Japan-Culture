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
    public sealed class DrugSupplySteps
    {
        private static readonly string[] VanillaBanned =
        {
            "SmokeleafJoint", "PsychiteTea", "Yayo", "Flake",
            "WakeUp", "GoJuice"
        };

        private static readonly string[] MoBanned =
        {
            "DankPyon_HappyPowder", "DankPyon_VigorPill",
            "DankPyon_BattleElixir", "DankPyon_Stardust",
            "DankPyon_Luhix", "DankPyon_HornetHoney",
            "DankPyon_KnowledgePotion", "DankPyon_Paregoric",
            "DankPyon_Miracle"
        };

        private static Task MainThread(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            Type driver = typeof(PickleContext).Assembly.GetType(
                "RimWorks.Pickle.Runtime.PickleDriver", true);
            MethodInfo post = driver.GetMethod("Post", new[] { typeof(Action) });
            if (post == null)
                throw new MissingMethodException(driver.FullName, "Post");
            post.Invoke(null, new object[] { new Action(delegate
            {
                try
                {
                    if (!UnityData.IsInMainThread)
                        throw new InvalidOperationException("Not on Unity thread");
                    action();
                    tcs.SetResult(true);
                }
                catch (Exception error) { tcs.SetException(error); }
            }) });
            return tcs.Task;
        }

        private static ThingDef RequireThing(PickleContext ctx, string id)
        {
            ThingDef def = DefDatabase<ThingDef>.GetNamedSilentFail(id);
            ctx.Require(def != null, "Missing saved item Def " + id);
            return def;
        }

        private static void CheckDrug(PickleContext ctx, string id, bool hasMaker)
        {
            ThingDef def = RequireThing(ctx, id);
            ctx.Require((def.recipeMaker != null) == hasMaker,
                "Incorrect generated bill for " + id);
            if (!hasMaker)
                ctx.Require(def.tradeability.ToString() == "Sellable",
                    "Ordinary trader purchase not blocked for " + id);
        }

        [Then("Culture excluded chemicals are not produced or assigned to newly generated pawns")]
        public async Task Check(PickleContext ctx)
        {
            await ctx.WaitUntil(() =>
            {
                Type ready = Type.GetType(
                    "AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                PropertyInfo flag = ready == null ? null :
                    ready.GetProperty("IsPostLoaded", BindingFlags.Public | BindingFlags.Static);
                return flag != null && (bool)flag.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);
            await MainThread(delegate
            {
                bool mo = ModsConfig.IsActive("DankPyon.Medieval.Overhaul");
                foreach (string id in VanillaBanned)
                    CheckDrug(ctx, id, false);
                CheckDrug(ctx, "Ambrosia", false);
                ctx.Require(RequireThing(ctx, "Beer") != null,
                    "Historical beer disappeared");
                CheckDrug(ctx, "Penoxycyline", true);

                foreach (string id in new [] {"Plant_Smokeleaf", "Plant_Psychoid"})
                {
                    ThingDef plant = RequireThing(ctx, id);
                    ctx.Require(plant.plant != null &&
                        (plant.plant.sowTags == null || plant.plant.sowTags.Count == 0),
                        "Excluded psychoactive cultivation still permitted " + id);
                }

                IncidentDef sprout = DefDatabase<IncidentDef>.GetNamedSilentFail("AmbrosiaSprout");
                ctx.Require(sprout != null && sprout.baseChance == 0f,
                    "Ambrosia random sprout still enabled");

                if (mo)
                {
                    foreach (string id in MoBanned)
                        CheckDrug(ctx, id, false);
                    foreach (string id in new []
                    {
                        "DankPyon_Mixture_HappyPowder",
                        "DankPyon_Mixture_BattleElixir",
                        "DankPyon_Mixture_VigorPill",
                        "DankPyon_Mixture_Luhix",
                        "DankPyon_Mixture_Stardust",
                        "DankPyon_Mixture_Knowledge",
                        "DankPyon_Mixture_Paregoric",
                        "DankPyon_Mixture_Miracle"
                    })
                    {
                        ThingDef mixture = RequireThing(ctx, id);
                        ctx.Require(mixture.recipeMaker == null,
                            "Banned MO alchemy mixture bill still generated: " + id);
                        ctx.Require(mixture.tradeability.ToString() == "Sellable",
                            "Banned MO alchemy mixture still stockable: " + id);
                    }
                    // Retain real loaded cauldron ProcessDefs for old saved mixtures.
                    Type processType = AppDomain.CurrentDomain.GetAssemblies()
                        .Select(assembly => assembly.GetType("ProcessorFramework.ProcessDef", false))
                        .FirstOrDefault(type => type != null);
                    ctx.Require(processType != null, "MO cauldron ProcessDef type missing");
                    Type processDatabase = typeof(DefDatabase<>).MakeGenericType(processType);
                    MethodInfo getProcess = processDatabase.GetMethod(
                        "GetNamedSilentFail", new[] { typeof(string) });
                    ctx.Require(getProcess != null, "MO process Def lookup unavailable");
                    foreach (string id in new[]
                    {
                        "DankPyon_Process_Knowledge",
                        "DankPyon_Process_Paregoric",
                        "DankPyon_Process_Miracle"
                    })
                        ctx.Require(getProcess.Invoke(null, new object[] { id }) != null,
                            "Previously owned MO mixture process disappeared: " + id);

                    // Original MO 1.6.2.2 has one direct Paregoric ruin shelf.
                    ThingDef shelf = RequireThing(ctx, "DankPyon_LootableShelf_Paregoric");
                    object lootExtension = shelf.modExtensions == null ? null :
                        shelf.modExtensions.FirstOrDefault(extension =>
                            extension.GetType().FullName ==
                                "MedievalOverhaul.BuildingLootableExtension");
                    ctx.Require(lootExtension != null, "MO Paregoric ruin shelf loot extension absent");
                    FieldInfo lootItemsField = lootExtension.GetType().GetField(
                        "randomItems", BindingFlags.Instance | BindingFlags.Public |
                            BindingFlags.NonPublic);
                    ctx.Require(lootItemsField != null, "MO ruin shelf randomItems absent");
                    IEnumerable lootItems = lootItemsField.GetValue(lootExtension) as IEnumerable;
                    ctx.Require(lootItems != null, "MO ruin shelf randomItems unpopulated");
                    var itemNames = lootItems.Cast<object>()
                        .Select(item => item is ThingDef def ? def.defName : item?.ToString())
                        .ToList();
                    ctx.Require(itemNames.Count == 1 && itemNames[0] == "MedicineHerbal",
                        "MO Paregoric ruin shelf still yields an excluded drug");

                    // Historical items must remain instantiable; does not claim that
                    // old saves or complete manual ingest/UseEffect were exercised.
                    foreach (string id in new[]
                    {
                        "DankPyon_KnowledgePotion",
                        "DankPyon_Paregoric",
                        "DankPyon_Miracle"
                    })
                    {
                        Thing legacyItem = ThingMaker.MakeThing(RequireThing(ctx, id));
                        ctx.Require(legacyItem != null && legacyItem.def.defName == id,
                            "MO legacy item can no longer be instantiated: " + id);
                        legacyItem.Destroy();
                    }
                    ThingDef nest = RequireThing(ctx, "DankPyon_HornetNest");
                    ctx.Require(nest.comps != null &&
                        !nest.comps.Any(comp => comp.GetType().Name ==
                            "CompProperties_Spawner" &&
                            comp.GetType().GetField("thingToSpawn")?.GetValue(comp)
                                is ThingDef thing && thing.defName == "DankPyon_HornetHoney"),
                        "Giant hornet nest still spawns banned honey");
                }

                // Verify the production Harmony method is registered, not just
                // that the test can call an otherwise inert Postfix directly.
                Assembly culture = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(assembly => assembly.GetName().Name == "AMJ.Culture");
                ctx.Require(culture != null, "Culture production DLL not loaded");
                Type patchType = culture.GetType("AMJ.Culture.InitialDrugAddictionGenerationPatch");
                ctx.Require(patchType != null, "Initial-addiction C# patch absent");
                MethodInfo patchTarget = typeof(PawnGenerator).GetMethod(
                    "GenerateInitialHediffs", BindingFlags.NonPublic | BindingFlags.Static);
                ctx.Require(patchTarget != null, "Fresh-pawn generated-heddiff entry missing");
                Assembly harmony = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(assembly => assembly.GetName().Name == "0Harmony");
                ctx.Require(harmony != null, "Harmony runtime absent");
                Type harmonyType = harmony.GetType("HarmonyLib.Harmony");
                MethodInfo patchInfo = harmonyType.GetMethod("GetPatchInfo",
                    BindingFlags.Public | BindingFlags.Static);
                object info = patchInfo.Invoke(null, new object[] { patchTarget });
                ctx.Require(info != null, "No Harmony patch on initial Hediffs");

                PawnKindDef colonist = DefDatabase<PawnKindDef>.GetNamedSilentFail("Colonist");
                ctx.Require(colonist != null, "Colonist kind missing");
                Pawn pawn = PawnGenerator.GeneratePawn(colonist, Faction.OfPlayer);
                ctx.Require(pawn != null && pawn.health != null,
                    "Ordinary new pawn failed");
                ChemicalDef psychite = DefDatabase<ChemicalDef>.GetNamedSilentFail("Psychite");
                ChemicalDef alcohol = DefDatabase<ChemicalDef>.GetNamedSilentFail("Alcohol");
                ctx.Require(psychite != null && psychite.addictionHediff != null &&
                    alcohol != null && alcohol.addictionHediff != null,
                    "ChemicalDef addiction identity lost");
                Hediff psychiteAddiction = pawn.health.AddHediff(psychite.addictionHediff);
                Hediff alcoholAddiction = pawn.health.AddHediff(alcohol.addictionHediff);
                ctx.Require(pawn.health.hediffSet.HasHediff(psychite.addictionHediff) &&
                    pawn.health.hediffSet.HasHediff(alcohol.addictionHediff),
                    "Original Hediff classes could not be added to test pawn");
                // Postfix only runs for initial generation. Deliberate invocation
                // on a disposable Pawn demonstrates selective filtering without
                // altering any saved pawn or normal in-game consumption.
                MethodInfo postfix = patchType.GetMethod("Postfix",
                    BindingFlags.Public | BindingFlags.Static);
                ctx.Require(postfix != null, "Generation Postfix missing");
                postfix.Invoke(null, new object[] { pawn });
                ctx.Require(!pawn.health.hediffSet.HasHediff(psychite.addictionHediff),
                    "Forbidden fresh-pawn psychite addiction was retained");
                ctx.Require(pawn.health.hediffSet.HasHediff(alcohol.addictionHediff),
                    "Valid alcohol addiction was removed");
                Log.Message("AMJ_CULTURE_DRUGS_LOADED_OK profile=" + (mo ? "MO16" : "Vanilla17")
                    + " prohibitedRecipes=" + VanillaBanned.Length
                    + " alcoholRetained=true initialAddictionFiltered=true");
            });
        }
    }
}
