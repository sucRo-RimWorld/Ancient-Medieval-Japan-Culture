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
    // Native-generation probe: source-level tradeability is not sufficient.
    [PickleSteps]
    public sealed class DrugWorldProviderSteps
    {
        private static readonly string[] Forbidden = {
            "SmokeleafJoint", "PsychiteTea", "Yayo", "Flake", "WakeUp", "GoJuice",
            "Ambrosia", "DankPyon_HappyPowder", "DankPyon_VigorPill",
            "DankPyon_BattleElixir", "DankPyon_Stardust", "DankPyon_Luhix",
            "DankPyon_HornetHoney", "DankPyon_Mixture_HappyPowder",
            "DankPyon_Mixture_BattleElixir", "DankPyon_Mixture_VigorPill",
            "DankPyon_Mixture_Luhix", "DankPyon_Mixture_Stardust",
            "DankPyon_KnowledgePotion", "DankPyon_Paregoric", "DankPyon_Miracle",
            "DankPyon_Mixture_Knowledge", "DankPyon_Mixture_Paregoric",
            "DankPyon_Mixture_Miracle"
        };

        private static Task OnGameThread(Action work)
        {
            var done = new TaskCompletionSource<bool>();
            var driver = typeof(PickleContext).Assembly.GetType(
                "RimWorks.Pickle.Runtime.PickleDriver", true);
            var post = driver.GetMethod("Post", new[] { typeof(Action) });
            if (post == null) throw new MissingMethodException(driver.FullName, "Post");
            post.Invoke(null, new object[] { new Action(delegate {
                try {
                    if (!UnityData.IsInMainThread)
                        throw new InvalidOperationException("World stock probe off Unity thread");
                    work(); done.SetResult(true);
                } catch (Exception ex) { done.SetException(ex); }
            }) });
            return done.Task;
        }

        private static void RejectForbidden(PickleContext ctx, IEnumerable<Thing> things,
                                            HashSet<string> forbidden, string generator,
                                            ref int checkedThings)
        {
            foreach (Thing item in things) {
                ctx.Require(item != null && item.def != null,
                    "Null generated item from " + generator);
                Thing thing = item is MinifiedThing m && m.InnerThing != null
                    ? m.InnerThing : item;
                ctx.Require(!forbidden.Contains(thing.def.defName),
                    "Forbidden drug actually generated: " + thing.def.defName
                    + " in " + generator);
                checkedThings++;
            }
        }

        [Then("Culture actual trader stock and normal quest rewards contain no excluded drugs")]
        public async Task TestRealGeneration(PickleContext ctx)
        {
            await ctx.WaitUntil(() => {
                Type ready = Type.GetType(
                    "AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                var flag = ready == null ? null
                    : ready.GetProperty("IsPostLoaded",
                        BindingFlags.Public | BindingFlags.Static);
                return flag != null && (bool)flag.GetValue(null, null);
            }, 90f);
            await ctx.WaitFrames(2);
            await OnGameThread(delegate {
                bool mo = ModsConfig.IsActive("DankPyon.Medieval.Overhaul");
                ctx.Require(mo, "MO source must be loaded for this provider test");
                var excluded = new HashSet<string>(Forbidden);
                var forbiddenDefs = Forbidden
                    .Select(id => DefDatabase<ThingDef>.GetNamedSilentFail(id))
                    .Where(def => def != null).ToList();
                ctx.Require(forbiddenDefs.Count == Forbidden.Length,
                    "Original prohibited Vanilla/MO drug or precursor Def missing");
                var all = DefDatabase<TraderKindDef>.AllDefsListForReading;
                var kinds = all.Where(k => k.stockGenerators != null
                    && k.stockGenerators.Any(g =>
                        forbiddenDefs.Any(def => g.HandlesThingDef(def))))
                    .OrderBy(k => k.defName, StringComparer.Ordinal).ToList();
                ctx.Require(kinds.Count > 0,
                    "No real trader generators recognize forbidden drug category");
                var trader = DefDatabase<ThingSetMakerDef>.GetNamedSilentFail("TraderStock");
                ctx.Require(trader != null && trader.root != null,
                    "Original trader stock generator missing");
                int stockItems = 0, stockKinds = 0, rewarded = 0;
                // In-game generator with real selected trader kinds, rather than
                // inspecting tradeability on individual ThingDefs.
                foreach (var kind in kinds.Take(12)) {
                    var p = default(ThingSetMakerParams);
                    p.traderDef = kind;
                    for (int trial = 0; trial < 2; trial++) {
                        var generated = trader.root.Generate(p);
                        RejectForbidden(ctx, generated, excluded,
                            "trader " + kind.defName, ref stockItems);
                    }
                    stockKinds++;
                }
                ctx.Require(stockKinds > 0 && stockItems > 0,
                    "Trader generation did not produce actual stock");
                // Vanilla standard item rewards: sample the actual maker, not a
                // fake enumerated whitelist or just the reward category metadata.
                var reward = DefDatabase<ThingSetMakerDef>
                    .GetNamedSilentFail("Reward_ItemsStandard");
                ctx.Require(reward != null && reward.root != null,
                    "Original standard quest item reward generator missing");
                var rp = default(ThingSetMakerParams);
                rp.totalMarketValueRange = new FloatRange(400f, 2200f);
                for (int trial = 0; trial < 12; trial++) {
                    var generated = reward.root.Generate(rp);
                    RejectForbidden(ctx, generated, excluded,
                        "Reward_ItemsStandard", ref rewarded);
                }
                ctx.Require(rewarded > 0,
                    "Standard reward generator did not create actual items");
                Log.Message("AMJ_CULTURE_DRUG_PROVIDERS16_OK"
                    + " traderKinds=" + stockKinds
                    + " traderThings=" + stockItems
                    + " rewardThings=" + rewarded
                    + " moLoaded=" + mo);
            });
        }
    }
}
