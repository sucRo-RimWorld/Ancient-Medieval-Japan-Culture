# AMJ Culture — design and first acceptance

## Development purpose, phase and priority (2026-10-16 review)

Culture's product goal is to remove historically incongruous **character generation, culturally contingent mood/social judgments, and intoxicant gameplay** from ancient-to-medieval Japanese settlement play **without breaking the useful Vanilla/MO gameplay roles**. One neutral meal Thought is a small completed slice, not the product goal. Culture combines Backstories, generic Pawn-generation interactions, Traits, relevant Needs/Thoughts, psychoactive Drug/effect/supply UX, and Japanese daily-life customs including indoor/outdoor footwear habits. **Culture is an integrated cultural overhaul; the author accepts it growing larger than other AMJ Mods. Module size alone is not a reason to split Culture-specific behavioral rules.** Shared Sleeping, general furniture, medicinal remedies, brewing, and ordinary food Jobs remain other owners' responsibilities.

**Actual implementation maturity:** the narrow `AteWithoutTable` zero-mood XML prototype, bounded `Transhumanist` and 31 original adult + 12 original child background exclusions are on `main`; the current ordinary-generation exclusion scope is 70, and 34 historical backgrounds have authored English/Japanese histories with six starting-item substitutions (see the latest section). The [second-wave source change](SFBackgroundSecondWave.md) adds a further 12 adult and 12 child rules; its updated production DLL and native acceptance are tracked separately below. The single real Vanilla 1.6 character-generation acceptance below passed, but complete MO/1.7 coverage and saved-game migration were not tested. The no-table prototype's static XML check passed; the generation source still needs focused loaded-Def/gameplay acceptance. Historical background/trait handling, drugs and minimum enza functionality have *approved ownership/boundaries or research candidates*, but **not** complete per-feature effect values, spawn handling, recipes, migration or final implementation specifications. Do not describe Culture overall as implemented or entering integration validation.

**Next Culture specification work:** prioritize the user's high-execution-impact historical drug/intoxicant UX together with related `DrugDesire`/Need/Trait rules. The author now permits losing powerful drug-based emergency mood rescue; preserve Def identities, coherent addiction/withdrawal mechanics and existing-save references while constraining ordinary sources. In the same bounded character-generation design effort, resolve the evidenced `Transhumanist` natural and forced-Backstory paths in Vanilla/MO, without indiscriminate TraitDef deletion or re-auditing all third-party backgrounds. Work from the existing Project [drug research](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/PsychoactiveSubstancesCandidate.md) and [character research](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md); make each accepted final decision here before adding its source. Enza material/Comfort/job adapter and broader ethics/other backgrounds remain separate later decisions without fixed next-task precedence. This local sequencing does **not** override the Project-wide P0 Environment/Grains repair and closeout work. The approved drug UX review has elevated execution priority ahead of new Ironmaking expansion; general Culture reconstruction classification remains P2.

**Validation timing:** keep the acceptance contract below as a *future promotion gate*, not the current development task. Defer the dedicated Culture 1.6 Vanilla/MO E2E harness, 1.7 compatibility checks, CI construction and broad regression matrix until a coherent implemented behavior requires those checks or a concrete unknown blocks its design. A single narrowly scoped loaded-Def check is permissible if needed to resolve a specific implementation ambiguity. Do not repeat retired stool demonstrations, grow tests to justify the runner infrastructure, or promote source parsing to runtime PASS. Do not publish the unaccepted prototype.

**Stage 1 baseline (2026-10-17):** The complete identified Vanilla 784 BackstoryDef / 43 TraitDef and MO 2 TraitDef input set has been indexed and conservatively sorted into Stage 2 working actions in [Stage1VanillaMOBackgroundTraitInventory.md](Stage1VanillaMOBackgroundTraitInventory.md) and [its 829-definition CSV](Data/VanillaMOBackgroundTraitStage1Screen.csv). This is exhaustive *inventory and triage*, **not** exhaustive verified Core-1.6/1.7 narrative judgment, proof of all pawn eligibility or permission to create new Japanese backgrounds. Stage 2 owns source-based final decisions and fixes; Stage 3 may add Japan-only content only after Stage 2. Preserve the approved unique-only exemption based on actual exclusive assignment, not mere name.

## Historical background and trait audit (2026-10-17)

**実装順序（作者確定、2026-10-17）：** **①VanillaとMedieval Overhaul（MO）の背景・特性を洗い出す → ②中世日本に不適切な既存背景・特性と関連する通常生成／強制付与・心情を必要な範囲で修正する → ③その後に初めて、中世日本独自の新規背景・特性を検討・実装する。** 第①②段階では原定義・人物生成経路・既存MO修正の重複／副作用を照合し、固有ポーン専用で通常生成されない背景・特性は対象外とする。既存の生成制限・史実向け改稿はこの第②段階の成果として保持し、無用な再作業はしない。戦場経験・死体目撃への習熟を表す中世日本独自の背景や新特性は第③段階まで**保留**する。既存Vanilla/MO特性の不適切な心情効果を必要最小限で修正すること自体は第②段階に含むが、その名目で中世日本独自の新規Defを先行追加しない。段階移行は第①②段階で確認された不整合が処理され、追加が必要な未充足の体験が残る場合だけ判断する。これは背景・特性系統内の順序であり、Cultureのイス・食事や他Modの工程・薬物設計の承認を変更しない。

**Audit eligibility boundary (author decision, 2026-10-17):** Exclude backgrounds and traits **assigned exclusively to particular unique/predefined pawns** from Culture's remaining historical-fit audit and modifications **only when they cannot occur in ordinary Vanilla/MO pawn generation**. Do not count such unique-only entries as pending ordinary-generation conflicts, change their special assignments, or remove their Defs. A `Solid` source path, named/fixed `PawnBio`, or missing childhood `spawnCategories` alone does **not** prove unique-only: normal generation can choose named Solid biographies. Keep any background or forced trait reachable by ordinary generated pawns in scope, including the Solid-selection path. Where reachability is not yet established, mark it unknown rather than assuming exemption or applying new restrictions. This narrows **future work only**; it does not retroactively undo the 70 existing source exclusions or 34 rewrites without specific evidence that a rule targets unique-only content.

### 追加の通常生成Shuffled背景4件（2026-10-17）

原Core 1.6／1.7 metadata v2で **Shuffled** と確認できた4件のみ生成カテゴリを制限する。専用固有ポーンへしか割り当てられない背景・特性は対象外。一般の`Solid`背景には通常生成可能なものもあるが、今回挙がった成人5件・幼少3件は個別到達経路が未確認のため変更しない。

| Def | 原スロット・通常生成カテゴリ | 元設定と処理 |
|---|---|---|
| `JoywireArtist8` | 成人：Offworld/Pirate/Scientist | 快楽電子インプラントのソフトウェア開発。通常カテゴリ除外 |
| `VRDesigner87` | 成人：Offworld/Scientist/Farmer | 仮想現実シミュレーション制作。通常カテゴリ除外 |
| `OrganFarm67` | 幼少：Offworld/Outlander/ImperialCommon/Madman | 人体を人工臓器培養に利用。通常カテゴリ除外 |
| `VatgrownSoldier8` | 幼少：Offworld/Pirate/ImperialCommon/ImperialFighter/Vatgrown | バイオ兵器施設での人工培養と加速成長。通常カテゴリ除外 |

正本：`Patches/AnachronisticAdultBackstories.xml`および`Patches/AnachronisticShuffledChildBackstories.xml`。従来の成人46件（別ファイル`FerventResearcher72`を含む）・Solid幼少24件に、成人2件・Shuffled幼少2件を追加。**成人48＋Solid幼少24＋Shuffled幼少2＝74件**を新規通常生成から制限し、改稿34件は変更しない。元のBackstoryDef、技能、強制Trait、初期所持品、保存済み人物、特定の固有人物への直接割当には影響を与えない。XML変更のみで、既存の1.6/1.7製品DLLはそのまま使用する。

SF内容の根拠はLudeon公式翻訳に保存された旧英語原文コメントであり、現行Core本文とのバイト一致は保証しない。原XMLの定義・年齢スロット・生成カテゴリは両バージョンの作者所有ソース台帳で確認した。新しいXMLの実ゲームロード結果は未検証で、既存の検証PASSはこの4件を含まない。PRごとにRimWorldを起動せず、実装完了後の集約検証に含める。

The owner-side [BackgroundTraitAudit.md](BackgroundTraitAudit.md) records the source-grounded Vanilla/MO character and optional medieval-pack historical-fit classifications.

The follow-up [SFBackgroundIndividualAudit.md](SFBackgroundIndividualAudit.md) assesses **all 50 originally SF/technology-flagged backgrounds individually** using an archived official translation's English comments and the authentic 1.6/1.7 metadata; it also identifies **26 additional cases missed by that flag**. The preserved English comments are not confirmed to be current Core 1.6/1.7 primary text, so its historical assessments are candidates, not new gameplay-exclusion approvals. No wholesale 50-background suppression is authorized. The 43-name trait crosswalk is provisional where modern 1.6/1.7 Core text has not been independently compared; Vanilla's 784 backgrounds were enumerated but **not** exhaustively historically approved. A review flag alone is not approval to disable that background. Preserve the approved narrow Transhumanist outcome already implemented in source; any other new background exclusions, story rewrites or altered Trait/Thought effects require their own authentic-content decision and then the smallest useful source change. This audit is not a new test mandate or game behavior change.

## Psychoactive substances: ordinary-supply restriction

### Native loaded-game drug validation (2026-10-17, after final DLL repair)

**Actual product source:** Culture main `659c17c8c9b113628d65bc36de2319357672bf88`, with the 1.6 DLL SHA-256 `79507a668fce1c0b70da61ded1cc4029004cd36b8c1de3202999819ce432ac26` and the 1.7 DLL SHA-256 `4977609a8df3b8e4f28dcdff26dc77ea01f758473181c0c2a44eb35f49abaa51` (each 24,576 bytes). They contain the new-pawn chemical addiction filter, prior drug supply patches and lazily initialized entrance progress Materials. Both were compiled by the licensed build-only Project runs [1.6 #39042550352](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39042550352) and [1.7 #39042553033](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39042553033).

| Actual isolated profile | Evidence | Drug scenario | Runtime ERROR | Exit | Result |
|---|---|---|---|---|---|
| Original RimWorld 1.6.4871 rev598, MO 1.6.2.2 + VEF/SYR; two original audio clips WAV-converted in disposable clone | [Project #39043086965](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39043086965) | Pickle 1/1, single marker once | 0 (both Unity and independent raw [ERROR] scan) | 0 | **PASS** |
| Original RimWorld 1.7.4977 rev1248, Vanilla | [Project #39043089770](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39043089770) | Pickle 1/1, single marker once | 0; FMOD 0 | 0 | **PASS** |

**What exactly the single scenario asserts:** final loaded ThingDefs lack normal `recipeMaker` on six Vanilla banned products plus Ambrosia; banned `tradeability=Sellable` and Beer retained; Penoxycyline remains craftable; psychoactive crops are unsowable; AmbrosiaSprout `baseChance=0`. MO profile additionally checks six banned hard drugs, five forbidden inherited alchemy-mixture makers absent, three nonexcluded medical/fantasy mixture makers present, and no direct HornetHoney nest spawner. It also generates one ordinary Pawn, confirms the Culture initial-Hediff Harmony entry is patched, directly supplies one forbidden Psychite addiction and one permitted Alcohol addiction on the disposable Pawn, and invokes the Postfix; the banned one is removed and Alcohol remains. This is **not** a statistical sample demonstrating every naturally addicted PawnKind, nor a quest/trader inventory generation sample. The original standalone drug-use and old-save import/resave were **not** exercised.

**Observed regression and fix:** prior [1.6 MO #39042030479](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39042030479) was correctly **FAIL** despite drug Pickle 1/1: two Unity `Tried to create a material from a different thread` ERRORs originated at `AMJ.Culture.EntranceZoneMap..cctor` on asynchronous `Map.FillComponents`. Culture PR #82 (main `417d7d63b5a9387db4b6acedaeb478e9c9076819`) deferred both progress-bar Material allocations to rendering; compiled binaries integrated by Culture PR #85 main `659c17c8c9b113628d65bc36de2319357672bf88`. Both clean reruns above establish ERROR0 **for these loaded drug scenarios**. They do **not** prove the separate entrance zone gameplay, visual cast bar, saved data or TPS regression. Keep this distinction in reporting.

**Still OPEN:** generic Vanilla quest/reward/trader stock providers, explicit inventory/scripts (including forced-background/scenario exceptions), third-party injection, MO 1.7, real older save reload and manual consumption of old banned drugs. Do not claim total absence from any world reward or retroactive old-save treatment. No new global ThingDef/QuestDef/PawnKind blacklist is approved without evidence of an actual source. Do not initiate repeated identical native tests.

 (2026-10-17 author supersession)

**Accepted player-facing rule:** Unlike the earlier 2026-10-16/early-2026-10-17 proposal, Culture **does not have to preserve a powerful drug-based emergency mood rescue**. The author accepts the corresponding increased difficulty: historical Japanese settlements may lack an equivalent to yayo, flake or HappyPowder. **Do not invent a potent tea, sake buff, synthetic drug or historically unsupported medieval substitute just to restore those mood bonuses.** Investigation of non-drug measures available to ordinary medieval Japanese people belongs to a separate Culture/research workstream; its outcome is not a prerequisite for this narrower suppression.

**What remains:** Vanilla Beer and existing MO Ale/Cider/Mead/Wine retain their normal recreational, drunkenness, tolerance, addiction and withdrawal behavior. Later Japanese sake production is owned by a drinks/fermentation feature, not by a duplicate Culture brewing chain. Historical tea (and Grains' potential grain-based warm drinks) is low-intensity ordinary recreation, **not** renamed psychite tea or an emergency pharmaceutical. Alcohol tolerance (上戸/下戸), subjective preference and acquired alcohol dependency must not be conflated. Culture preserves existing `DrugDesire` Def IDs and actual behavior until that separate trait review makes a coherent decision.

**Ordinary-play exclusion targets (first source slice):**
- **Vanilla:** `SmokeleafJoint`, `PsychiteTea`, `Yayo`, `Flake`, `WakeUp`, `GoJuice`; `Ambrosia` is removed from the ordinary spontaneous sprout event and remains sellable. No new drug products are introduced.
- **Medieval Overhaul 1.6.2.2 (optional):** `DankPyon_HappyPowder`, `DankPyon_VigorPill`, `DankPyon_BattleElixir`, `DankPyon_Stardust`, `DankPyon_Luhix`, `DankPyon_HornetHoney` as the six evidenced `Hard` recreational/extraordinary drugs. The existing HappyPowder `isCombatEnhancingDrug=false` local correction is retained for legacy item use, and is not a reason to restore ordinary supply.
- **Outside this slice:** Vanilla `Luciferium` (irreversible extraordinary dependency) and `Penoxycyline` (medicine); MO `DankPyon_Paregoric`, `DankPyon_Miracle`, `DankPyon_KnowledgePotion` (medical/fantasy effects that need separate owner/era decisions). `DrugDesire`, other TraitDefs, background changes, Ideology DLC, third-party drug Defs, beer and other MO alcohol are untouched.

### Exact XML implementation and preserved references

1. `Patches/VanillaDrugSupplyRestriction.xml` and `Patches/MedievalOverhaulDrugSupplyRestriction.xml` remove each targeted explicit `recipeMaker` **and change its `ParentName` to the corresponding non-craftable base** (`MakeableDrugBase → DrugBase`, `MakeableDrugPillBase → DrugPillBase`) before XML inheritance resolution. Removing only the explicit `recipeMaker` is **insufficient**: the `Makeable*` parent would restore a recipe at inheritance time, allowing the excluded drug to remain craftable. The non-craftable parent retains the same ingestible/pill base properties but no inherited bill. Only the 6 Vanilla and 5 MO already-approved excluded *craftable* drug ThingDefs are reparented; Ambrosia/HornetHoney already use non-craftable bases. These patches also replace an explicitly present `tradeability` field if any, then add `tradeability=Sellable`. This is intended to stop **normal trader sales** while still permitting the player to **sell already-owned stacks**. Guards mean missing/version-different XML fields do not produce missing-XPath errors. MO patch is `PatchOperationFindMod` and is not a new hard dependency.
2. `Patches/VanillaDrugCropsAndAmbrosia.xml` removes `sowTags` from `Plant_Smokeleaf` and `Plant_Psychoid` so normal grow-zone crop selection does not offer new cultivation; it changes `IncidentDef=AmbrosiaSprout` `baseChance` to zero while keeping the Def intact. Older already-planted crops and existing Ambrosia bushes/items remain. The underlying `PlantDef` and harvested leaves are not deleted.
3. **No ThingDef, PlantDef, IncidentDef, ChemicalDef, NeedDef, HediffDef or ThoughtDef is removed or renamed.** No existing held item, pawn dependency, saved DrugPolicy/trait, mood intensity, addiction/withdrawal severity, overdose value or manual consumption is rewritten. Unlike the obsolete release-order rule, new-production suppression **does not wait for a pharmaceutical replacement**. Old dependent pawns may have to withdraw, which is a deliberate gameplay consequence requiring old-save compatibility verification, not automatic forced cure.
4. This first pass **does not demonstrate total world-generation exclusion**. Remaining possible routes needing separate targeted implementation include MO alchemy intermediate-mixture bills that inherit a common parent `recipeMaker`, special sources of HornetHoney, Vanilla/MO explicit starting gear and addicted pawn generation, scenario-supplied drugs, random stock generators that bypass `tradeability`, quests, raid loot, cargo, already spawned map items and third-party patches. It is wrong to call the whole set removed from all ordinary play until each live route is either eliminated or explicitly accepted.
5. Original supplied MO 1.6.2.2 drug XML confirmed the listed six ThingDefs and direct `recipeMaker` on five (HornetHoney has no direct maker). Historical Core XML references confirm Vanilla recipe/sow/incident names, but the actual original 1.6/1.7 Core and MO 1.7 **resolved Defs are not yet reverified for this delta**. Static XML/XPath is a source check, not runtime evidence. Do not launch a game or configure automatic native CI per small XML PR. One consolidated acceptance near feature closeout should check the real 1.6/1.7 loaded Defs, ordinary trade/bills/crops/incident, new-pawn addictions and legacy-item use with ERROR0 and an explicit unsupported-provider limit.

**Inheritance correction (2026-10-17):** The initial 2026-10-17 XML source only removed the concrete `recipeMaker`; source inspection of original Vanilla `DrugBases.xml` and supplied original MO `Drug_HappyPowder.xml`/`Drug_VigorPill.xml` demonstrates inheritance from `MakeableDrugBase` / `MakeableDrugPillBase` would repopulate it. The separate MO mixture-base remedy below is unaffected and still needed because its parent is `DankPyon_MixtureBase`. For Vanilla: SmokeleafJoint/PsychiteTea/Yayo/Flake/GoJuice reparent to DrugBase, WakeUp to DrugPillBase. For MO: HappyPowder/BattleElixir/Stardust/Luhix to DrugBase, VigorPill to DrugPillBase. Existing chemical identities, ingestible effects, pills, alcohol, Penoxycyline, other recipes and all historical-use dependencies remain. **Test expectation:** final loaded banned item `recipeMaker` null and no generated recipe, while retained Beer and medical crafting bills remain. Original XML/structural validation alone is not game acceptance.

### MO production and world-spawn leaks — bounded follow-up (2026-10-17)

Two optional, MO 1.6.2.2 original-source-grounded `PatchOperationFindMod` XML files close routes that are **not** governed by the first source slice:

- `Patches/MedievalOverhaulAlchemyMixtureSupply.xml`: MO uses an abstract `ThingDef Name="DankPyon_MixtureBase"` holding the `recipeMaker` inherited by **all eight** concrete mixtures. Removing `recipeMaker` only from finished hard-drug ThingDefs does not stop synthesis of their precursors. The patch removes this abstract parent's recipeMaker and reattaches **the exact original seven-field recipeMaker** to `DankPyon_Mixture_Miracle`, `DankPyon_Mixture_Paregoric`, and `DankPyon_Mixture_Knowledge`. Five targeted hard-drug mixture items (`HappyPowder`, `BattleElixir`, `VigorPill`, `Luhix`, `Stardust`) no longer inherit a newly generated crafting bill. Their ThingDefs, old inventories, and their `ProcessorFramework.ProcessDef` finishing definitions remain **intact**, allowing already-owned mixtures to finish processing; no medical alchemy item/recipe is intentionally removed. This precise inheritance behavior needs loaded-game confirmation.
- `Patches/MedievalOverhaulHardDrugWorldSources.xml`: Six drug-specific **ruin shelves** have their `MedievalOverhaul.BuildingLootableExtension/randomItems` lists redirected to existing Vanilla `MedicineHerbal` instead of the original banned MO hard-drug stacks. Ruin shelf buildings and their loot counts/rarities remain unchanged; the loot's resale value may decrease. Original `DankPyon_HornetNest` loses only its `DankPyon_HornetHoney` comp spawner and killed-leavings entry. The nest itself, insect defender spawning, jelly spawner, plant/environment, other loot shelves and existing saved honey stacks remain. This is not a rewrite of honey's item or addiction Def.

**Static source evidence:** Original author-supplied MO 1.6.2.2 `1.6/Defs/Drugs/Drug_Bases_and_Mixtures.xml`, `1.6/Defs/ThingDefs_Buildings/Buildings_RuinsLootable.xml`, `1.6/Defs/ThingDefs_Buildings/Buildings_Natural.xml`. XPath-match counts against those actual three original source trees: abstract recipeMaker **1**, medical survivors **3**, suppressed mixtures **5**, affected loot lists **6**, targeted honey spawner **1**, honey killed-leavings **1**, unaffected jelly spawner **1**. This is only static XML provider verification, **not** a RimWorld 1.6/1.7 loaded Def, process-job, ruin exploration, save-load or runtime PASS. No new DLL or native tests were run.

**Remaining open routes:** explicit quest/raid/starting pawn drug inventories, addiction on newly generated pawns, nonstandard external stock generators, 1.7 MO file/processor-framework differences, saved active cauldrons or hornet nests. Do not claim universal generation suppression or delete existing Hediff/Need/ProcessDef references. Actual check at consolidated acceptance: unwanted alchemy mixture **bills absent**, medical mixture **bills present**, old mixtures/processes usable, six ruin shelves yield no banned hard drugs, hornet nest produces jelly but no hard drug, and no gameplay/patch errors.

**Reuse/ownership decision:** Project `Docs/Research/PsychoactiveSubstancesCandidate.md` already compares Vanilla/MO, VE brewing/traits and non-VE Medieval Drugs/WTL supply approaches. Neither importing extra VE content nor adding the non-VE medievalized drugs meets this **subtractive** historically bounded behavior; a small optional Culture XML patch is preferred to requiring more content/dependencies. No third-party standalone drug-removal addon is mandated. The 2026-10-16 research's instruction to maintain an **unbroken emergency mood-drug role** is expressly superseded here, but its drug-identity/owner/source facts and historical tea cautions remain usable.

### Trait and Backstory decisions

- **`DrugDesire` degree 2 / 1 / −1:** retain its identifiers, original effective behavior and save references for now. Interest/fascination/teetotaler must be evaluated as different dispositions, **not** renamed indiscriminately to "likes sake", alcohol tolerance or addiction. Any future behavior change must match the final set of usable substances, DrugPolicy and dependencies. 上戸／下戸 (physiological tolerance) and sake preference remain distinct proposals; no invented trait degrees, spawn weights or effect numbers.
- **`Transhumanist` (body modder):** intended new-generation contract is to prevent the anachronistic mechanized-body aspiration in Culture-covered ordinary human generation. This is *not* a prohibition on historically plausible prosthetic treatment. Preferred narrow implementation candidate is to disable Vanilla natural-roll commonality and **separately** remove unsuitable forced-Backstory entry points; neither a commonality-only patch nor deleting TraitDef meets the goal.
- **`FerventResearcher72`:** Vanilla source evidence establishes that the adult Backstory forces `Transhumanist` and participates in Offworld/Pirate categories; existing 1.7 evidence confirms real forced assignment. Its older publicly preserved English narrative describes a home planet near “transcendence”, bureaucrats and bioethics committees, **not merely a period-neutral scholar**. The preferred Culture behavior is therefore **to exclude this Backstory from applicable ordinary period-Japanese generation**, rather than silently remove its forced Trait and preserve an incongruous narrative. Before production, ensure the owned 1.6 source still has that meaning and choose the smallest safe category/filter intervention. Do not delete BackstoryDef or globally rewrite forcedTraits. [Original evidence ledger](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md); [older translated source preserving English notes](https://www.madxingjin.tech/git/SiteAdmin/RimWorld-ChineseSimplified/src/commit/48684ca0a6c47e26376f01313840254a323f5652/Core/Backstories/Backstories.xml).
- **Existing pawns:** do not remove their `Transhumanist`, `BodyPurist`, or existing mood/opinion ThoughtDefs; avoid retroactive emotional/stat changes. Treat old-pawn conversion, if ever justified, as a separate choice. MO Ideology memes are not a substitute for Vanilla Trait generation; uninstalled DLC is out of scope.
- **External Backstories:** no automatic global removal of third-party forcedTraits, and no compulsory audit of every historical/medieval Backstory package merely to ship a bounded Vanilla/MO fix. Optional compatibility must be grounded in a reported conflict or supported profile.

### Initial addiction on newly generated pawns — targeted C# source (2026-10-17)

**Approved behavior:** A newly generated ordinary Vanilla/MO human should not start with dependence upon chemicals which Culture has removed from normal supply, without deleting dependency identities or disabling alcohol-related drug mechanics. Altering every `PawnKindDef.chemicalAddictionChance` to zero is explicitly **wrong**, because it also suppresses naturally generated alcohol dependence. Leave `PawnKindDef`, `ChemicalDef`, `NeedDef`, `HediffDef`, `DrugPolicy` and all old-save pawn instances unmodified.

**Implementation:** `Source/AMJ.Culture/InitialDrugAddictionGenerationPatch.cs` targets private Vanilla 1.6 `Verse.PawnGenerator.GenerateInitialHediffs(Pawn, PawnGenerationRequest)` with a Harmony Postfix. That method is part of fresh-pawn creation, not existing-pawn redressing or save load; vanilla scenario-defined pawn hediffs are installed *after* it. A generated human's initial hediff list is filtered **only** for the chemical's resolved `addictionHediff` and accompanying `toleranceHediff`; only the two removed Hediff instances (when present) are removed through `Pawn_HealthTracker.RemoveHediff`. No ordinary injuries, alcohol or medical hediffs are selected. The chemical set is Vanilla `Ambrosia`, `Smokeleaf`, `Psychite` (shared by yayo/flake/psychite tea), `WakeUp`, `GoJuice`; optional MO `DankPyon_Chemical_HappyPowder`, `DankPyon_Chemical_VigorPill`, `DankPyon_Chemical_BattleElixir`, `DankPyon_Chemical_Stardust`, `DankPyon_Chemical_Luhix`, `DankPyon_HornetHoney`. Optional chemicals resolve via `GetNamedSilentFail`; no hard MO dependency.

**Preservation:** Alcohol (`Alcohol`) is absent from this filter, as are Vanilla Luciferium, other medicines and deliberate later scenario-imposed starting hediffs. Existing held banned drugs remain usable and able to create addictions in gameplay (to avoid save-breaking redefinition); only the initial *random* generated-dependence outcome is filtered. Generation may have already applied an associated `ChemicalDamageSevere` independent of addiction on some MO/Vanilla candidates; we do not remove a generic organ-injury Hediff without reliable attribution. Existing saved affected or addicted Pawns are never processed retroactively. Third-party patches placed after this method can add chemical addiction later; this cannot be represented as global prohibition.

**Evidence and acceptance limitation:** The 1.6 vanilla pawn-generation trace documents `GenerateInitialHediffs(Pawn, PawnGenerationRequest)` before scene `Scenario.Notify_NewPawnGenerating`; it avoids the high-cost universal `PawnGenerator.GeneratePawn` Postfix. Actual MO 1.6.2.2 `ChemicalDef` names and addiction/tolerance links were confirmed for all six MO candidates. The Vanilla chemical identities were checked against preserved Core XML source, not against both current licensed loaded Core distributions. A source C# change is NOT active gameplay until **both versioned production DLLs are genuinely rebuilt** from the exact accepted source and integrated through a protected PR. A compiled DLL by itself is NOT a loaded-game test. Intended consolidated tests: (1) fresh Vanilla/MO pawns with chemical-addiction chances yield no excluded addiction/tolerance; (2) alcohol addiction can still generate; (3) old preexisting dependency remains after save load; (4) manually consuming old banned drugs can still form addiction; (5) scenario-added hediffs are preserved; (6) 1.6 and 1.7 assemblies load with ERROR0. Do not run the complete game simply to multiply sample counts.

### Compiled per-version dependency filter (2026-10-17)

**Production binaries are integrated:** The real 1.6 and 1.7 `AMJ.Culture.dll` files were compiled using licensed RimWorld managed assemblies and the exact Culture source commit `009d36c75d0d9f557ab63563723e78efc6cfbc8c`. Both builds include `InitialDrugAddictionGenerationPatch.cs`, as well as the previously shipped seat, lodging and solid-child patches. `LoadFolders.xml` continues to choose the per-version assembly; no root assembly path was added.

| Game | Project compile-only workflow | DLL SHA-256 | Bytes |
|---|---|---|---|
| 1.6 | [#39034597068](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39034597068) | `5e2f9a930210c6223b7a21259f8f689f39241d248887b2b2a3b419a9b252f77d` | 12,800 |
| 1.7 | [#39034604350](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39034604350) | `a36d5a5774feb7b0304f40a378099b7d8fd170a16885b63146bfbdb576366234` | 12,800 |

The production DLLs were supplied by the two authenticated builder jobs' Base64 transfer and stored as Git blobs on the Culture topic branch, not rebuilt in ChatGPT's local scratch space. **Compile-only SUCCESS is not native gameplay success.** A focused real-game acceptance remains pending for initial generated addiction, alcohol exception, old saved dependencies, deliberate scenario-created dependencies, and original loaded Vanilla/MO 1.6/1.7 behavior. Nothing here authorizes Steam release.

### New-pawn combat-drug inventory flag (2026-10-17 follow-up)

`Patches/CombatDrugGenerationRestrictions.xml` sets existing `CompProperties_Drug.isCombatEnhancingDrug=true` to `false` for Vanilla `Yayo` and `GoJuice`, plus optional MO `DankPyon_BattleElixir` and `DankPyon_HornetHoney`. The earlier separate `Patches/MedievalOverhaulHappyPowderCombatUse.xml` already disables that same selection flag for `DankPyon_HappyPowder`. Original MO `Luhix` and `Stardust` already have `false`. Other optional MO or third-party combat drugs are not touched.

**Purpose and limit:** This closes the known `PawnKindDef.combatEnhancingDrugsChance` AI-gear selection route for these banned products **without globally setting every pawn kind's chance to zero**, and without deleting item Defs, preventing manual use of old stocks, or changing combat stat/effect values. Original MO 1.6.2.2 BattleElixir/HornetHoney XML each has one matching true flag; original Core XML identifies Yayo/GoJuice as true. XML-only; no new DLL rebuild or game run needed. Other `fixedInventory`, `inventoryOptions`, reward, trader, quest and third-party explicit item references may still supply drugs; the in-game selection behavior and provider/version differences remain pending the consolidated runtime check.

### MO 1.6.2.2 source audit boundary

The supplied MO 1.6.2.2 XML tree has direct supply references in alchemy, lootable ruins and HornetNest; existing Culture patches cover these. References in `Buildings_Storage.xml` are storage filters, and must remain for legacy items. Original `SpaceBartender0` is already excluded from ordinary selection by the separate Stage2 category patch. No further explicit MO XML source was identified in this scoped scan. Generic game rewards, third-party providers, MO binary behavior and MO 1.7 are not verified. The current exclusion rule must be checked against loaded original 1.6/1.7 game Defs before release.

### Next implementation / acceptance boundary

- Finish ordinary-generation exclusion only along demonstrated Vanilla/MO supply paths; prioritize newly generated drug-addicted pawns and explicit mixed-stock or reward pathways that still produce suppressed drugs.
- Keep the source Defs usable for an old save, and distinguish **preserving existing references** from guaranteeing enough fresh supply to treat addiction. Do not blanket-remove Hediffs or force retroactive cures.
- Do not place new tea/sake recipes, medical substances or Japanese-history prose in Culture without their own owning design and author approval.
- After the real feature source is coherent, perform the smallest consolidated gameplay acceptance, not a native game launch for each XML/PR. No publication without the owning release gate.

**Status:** XML production/trade/crop/ambrosia restriction source prepared; complete trade/loot/quest/pawn generation controls, legacy-save experience and loaded 1.6/1.7 behavior remain **OPEN**. This is not a finished "drugs-off" release.

## Chair-only meals — revised implementation scope (2026-10-17)

**Current author requirement supersedes the enza-only route:** Existing usable chairs
(Vanilla and MO included) must be eligible ordinary eating destinations **with no
adjacent table**. The aim is actual seated eating/Comfort, not merely neutralizing
`AteWithoutTable`. This rule applies to any existing valid sit-capable building;
it does **not** require creating/approving an enza, changing furniture artwork,
making a picnic zone, or introducing a new table or flooring mechanic.

**Picnic Area reference and implementation boundary:** The pinned Picnic Area
`Toils_Ingest.CarryIngestibleToChewSpot` Postfix establishes the useful pattern:
choose a normal `isSittable` furniture destination without a neighboring
`SurfaceType.Eat`, reserve the chair and destination, and path to it as an
ordinary Ingest travel Toil. Picnic-specific zones, random choice, weather,
positive Thought and global `GenGrid.HasEatSurface` changes are **not**
required by Culture. Source comparison: [Picnic Area pinned Toil adapter](https://github.com/emipa606/PicnicArea/blob/388896ec696dcf1f3f735315468c0ee26e910e77/Source/PicnicArea/Toils_Ingest_CarryIngestibleToChewSpot.cs).

**Smallest bounded implementation:** For ordinary `JobDefOf.Ingest`, keep the
Vanilla travel Toil. First check whether a reachable, valid, reservable
`isSittable` seat **adjacent to an `Eat` surface** exists, using the
same furniture search radius as Vanilla; if so, run the original Toil.
The **existence of any table by itself is not a condition**: a table with
no usable adjoining chair must not prevent using a separate table-free
chair. Otherwise find an existing owned/faction-neutral, socially proper,
unforbidden, non-burning, safe, reachable, reservable, standable
`isSittable` chair within that radius. Reserve that chair and destination,
path to its cell and let the original Ingest Job consume food and gain
Comfort. If no chair qualifies or reservation fails, run the original
Toil. Do not edit `GenGrid.HasEatSurface`, globally change all furniture,
alter `FinalizeIngest`, food radius, dining-room rewards, Comfort rates,
or non-Ingest Jobs. The existing `AteWithoutTable=0` remains independent.

**Current binary / actual acceptance (2026-10-17):** The unchanged
`Source/AMJ.Culture/TablelessChairMealPatch.cs` is now packaged inside the
current 8,704-byte RimWorld 1.6 production `Assemblies/AMJ.Culture.dll`
(SHA-256 `2fa6ab6429bd9f81d48633bea0998b47c7255fbb8658b73addc697d8ddf24010`),
alongside separate Background logic. The build-only original 1.6 source
[Project #39004690614](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39004690614)
was SUCCESS; the old 7,680-byte chair-only DLL is superseded.

[Focused native game #39005539686](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005539686)
**SUCCESS** in original `RimWorld 1.6.4871 rev598` with pinned exact
Culture source `1f806e962abcc5c2437b85fed81121519492c948` and
current production DLL SHA above. One actual Pickle scenario asserted:
(1) table-free Vanilla Stool seated meal with Comfort increase and
full consumption; (2) a more distant usable table-adjacent Stool
was chosen ahead of a nearer standalone Stool; (3) after all the
seats/table were removed, ordinary standing Ingest completed.
Pickle 1/1, native ERROR 0, raw Unity log ERROR 0, process exit 0,
original game unchanged. No production DLL or C# changes were required.
An initial attempt [#39005451249](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005451249)
was blocked **before game launch** by a stale binary SHA check,
corrected via Project PR #588. Earlier single-chair PASS #39002811535
remains valid for its explicitly older source and DLL identity.

**Actual MO compatibility slice (2026-10-17):** An original MO 1.6.2.2
`DankPyon_RusticStool` (`isSittable=true`, base Comfort 0.5) was used to
complete a normal table-free Ingest Job with measurable Comfort gain in
[Project run #39007512785](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39007512785).
Source-pinned Culture `6902a96870e3ed32f05b2d633b9dbfbcfd970e69`,
**8,704-byte DLL** SHA-256
`2fa6ab6429bd9f81d48633bea0998b47c7255fbb8658b73addc697d8ddf24010`,
original RimWorld 1.6.4871 rev598, MO+VEF+SYR, Pickle 1/1,
Unity/raw ERROR 0, exit 0, no original game modification.
**Sound caveat:** the two known external MO/SYR unsupported MP3 clips were
converted to temporary 44.1kHz PCM WAV **only inside the disposable game
clone**. The unmodified original-MP3 configuration did not establish
ERROR-zero; never confuse the functional dining PASS with this separate
external audio defect.

Since that run, another Culture Backstory implementation compiled a
**9,216-byte current main DLL**, SHA-256
`0d944eb47a1e648acdbc13ca75948e075e5ccfd71fefe1d4196c54d5a6c5be03`.
The chair production C# Git blob is still
`04c1f579ee96a131d95e2aab47953fe5536a9844`, but **the current
9,216-byte artifact was not the tested MO binary**. No extra MO test is
warranted solely due to unrelated Backstory work; a later integrated
release decision can assess the material risk.

**Not verified by these scoped seat tests:** all other MO/third-party chairs,
positive dining-room Thoughts and RimWorld 1.7 production compatibility.
Validation must correspond to a real compatibility issue or release requirement,
not a large speculative test matrix.

**Boundary:** The historically appropriate enza is still an optional
later furniture candidate requiring its own materials, artwork and balance
acceptance. The change here concerns the *seat-selection rule for existing
furniture*, not author acceptance of any new furnishing.

## Scope of this prototype

This first source-only prototype changes **only** the existing `AteWithoutTable` Thought mood stage to `0`. Preserve the same ThoughtDef identity; do not add a compensating mood buff, hide a zero-effect memory from the UI, or alter the food Job, seating, rooms, nutrition, filth, and other Thoughts.

The independently designed historically appropriate **enza (円座)** floor seat / actual Comfort mechanic is a separate future implementation unit, not in this change. Culture remains standalone and does not require MO, any VE content pack, Picnic Area or another furniture Mod. The broader integrated Culture scope (Backstories, Traits, drugs) is planned but not implemented in this prototype.

Background and prior-art/need audit: [Project Culture candidate specification](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md). That document is a staging reference; the owner repository takes over accepted implementation and version-specific validation when bootstrapped.

## Acceptance contract

1. Real original RimWorld 1.6 Vanilla and Vanilla+MO loaded `ThoughtDef=AteWithoutTable`: patch operation targets existing stage(s), and resulting mood effect is exactly `0`; no patch operation error.
2. An actual meal without an eating table no longer causes a negative mood effect from `AteWithoutTable`, and creates no replacement positive buff. Record whether an inert memory remains in the Needs UI separately; this is not a release blocker for the narrow patch.
3. Standard tables/seats and actual Comfort gain are unchanged; a normal impressive dining room still awards the standard positive Thought when the unmodified game would award it.
4. Other food-quality, dirty food/filth, nutrition and environmental penalties remain unchanged.
5. Native test log is complete, with zero attributable runtime ERRORs, explicit exact game/Mod source and test profile; original game files, installed providers, saves and player ModsConfig must not be modified.
6. RimWorld 1.7 compatibility is unclaimed until its own actual loaded-Def and real-meal checks pass. Only then expand `supportedVersions`.

Do not substitute XML parsing or the Project's retired stool proof-of-concept for tests (1)–(6). Avoid re-running the retired test-only research fixture.

## Static source contract

`Patches/AteWithoutTable.xml` contains exactly one `PatchOperationReplace` aimed at `Defs/ThoughtDef[defName="AteWithoutTable"]/stages/li/baseMoodEffect`, with a single `baseMoodEffect` element and text `0`. `About/About.xml` declares only RimWorld `1.6` until runtime validation.

**Status (2026-10-16):** Source-only, XML static validation. No actual native 1.6/1.7 loaded-game or gameplay evidence; not approved for Workshop publication.

## Transhumanist new-pawn restriction (implementation scope, 2026-10-17)

**Outcome:** Newly generated ordinary Vanilla/MO humans should not receive the anachronistic `Transhumanist` trait through normal random selection or the known Vanilla adult backstory `FerventResearcher72`. Existing pawns, TraitDef identities, ThoughtDefs, `BodyPurist` and prosthetic treatment remain unchanged.

`Patches/TranshumanistGeneration.xml` implements two verified-loadable, narrowly targeted XML operations:

1. Set `TraitDef[Transhumanist]/commonality=0` for normal random selection, retaining the TraitDef.
2. Replace `BackstoryDef[FerventResearcher72]/spawnCategories` (`Offworld`, `Pirate`) with the deliberately unused category `AMJ_CultureExcluded`. That source backstory explicitly describes technological transcendence and is not made historically suitable merely by removing its trait.

**Runtime correction (2026-10-17):** The former third `PatchOperationRemove` targeted `BackstoryDef[FerventResearcher72]/forcedTraits/li[traitDef="Transhumanist"]` and emitted a reproducible ERROR in genuine RimWorld 1.6 when Culture loaded. It is removed, not silently counted as working. Existing forced-trait data in this retained BackstoryDef is unchanged. Normal eligible-category generation is excluded by operation 2; **direct, category-bypassing assignment by an external generator may still force the trait and is not covered**. A correct direct-assignment defense requires its own genuine loaded-Def design; do not retry speculative invalid XPath removals during the independent meal test.

The sentinel category must not be added to a Pawn generation filter. This is an ordinary-category exclusion, **not** a claim that explicitly modded generators cannot override categories or inject the Trait. External Backstory packs and DLC remain outside this narrowly approved slice. Do not delete or rewrite broad Backstory libraries.

**Acceptance, once the implementation is assembled:** inspect authentic 1.6 loaded Defs for exact patch targets and category eligibility; compare commonality and backstory category; flag direct forced assignment as outside this patch rather than claiming suppression; generate one ordinary Vanilla pawn and one MO pawn without errors; confirm changed source preserves existing-pawn identities and other traits. A single consolidated native acceptance near release is sufficient; do not create a sampling matrix or run a game on every PR. Missing XPath or ERROR is failure, not a PASS. No native acceptance or 1.7 support is claimed by this source change.

## Ordinary generation of unambiguously anachronistic adult backgrounds (2026-10-17)

**Player goal:** Newly generated ordinary Vanilla/MO historical-Japan pawns must not receive adult backstories whose actual story depends on spacecraft, orbital fleets, computer networks, AI, robot/mechanoid manufacturing, gene engineering, or similar non-period technology. This is an **ordinary background-category restriction**, not rewriting trait effects, deleting old defNames or removing portable historical professions.

**Production source:** `Patches/AnachronisticAdultBackstories.xml` applies **30 separate `PatchOperationReplace` rules** only to existing `BackstoryDef/spawnCategories` nodes. All 30 exact defNames, slot=`Adulthood` and nonempty original category lists were cross-checked against the authentic Core 1.6/1.7 schema-v2 metadata inventory produced from the user's licensed game; these metadata files are SHA-identical at the two audited versions. Their narrative/technological incompatibility is independently supported by Ludeon's English comments in historical official translation sources; **those comments have not been demonstrated byte-identical to the latest licensed 1.6/1.7 prose**, so this is a bounded high-confidence original-background restriction, not a completed 784-background prose audit. [Individual period review](SFBackgroundIndividualAudit.md).

The thirty target DefNames: `AerospaceEngineer44`, `ComputerEngineer10`, `ComputerEngineer36`, `DeepSpaceSurveyor1`, `GlitterworldSurgeon15`, `OrbitalReservist22`, `SpaceMarine51`, `SpaceMarineMedic10`, `SpaceNavyTech37`, `SpaceResearcher25`, `SpaceTechnician29`, `SpaceshipSalesman34`, `StarshipJanitor33`, `AIProgrammer22`, `AIResearcher94`, `AceFighterPilot54`, `ArchotechSpy75`, `BattleMechanic27`, `BattlefieldTech52`, `CombatMedtech59`, `CorpResearcher71`, `EngineeredPilot75`, `GeneticEngineer89`, `GeneticScientist66`, `MechWarVeteran77`, `NetworkEngineer34`, `Roboticist1`, `Roboticist41`, `RocketEngineer53`, `RocketPioneer19`.

**Effects and boundaries:** Every such category is changed to the intentionally unused `AMJ_CultureExcluded`; retain the full Def and its title, skills, work restrictions, traits and possessions for old saves and explicitly assigned pawns. Together with `FerventResearcher72` in `TranshumanistGeneration.xml`, the current **source** covers 31 adult Defs in ordinary category-filtered generation. `Offworld`-only histories may already be ineligible to certain historical factions; the patch still avoids unhistorical vanilla/other categories where present, without changing any faction itself. Never register the sentinel with any backstory filter.

**Separately handled by the compiled Solid biography gate below:** The 12 same-priority **Solid childhood** histories (`ComputerGeek62`, `GlitterworldKid85`, `GlitterworldNerd20`, `MechanoidNerd10`, `SpaceCadet77`, `SpaceFanboy22`, `SpaceNerd97`, `CorporateSlave22`, `HackerKid98`, `HackerProdigy84`, `MechanoidHacker93`, `VideoGamer16`) have **no `spawnCategories` XML element** in original Core. Blindly copying the adult `PatchOperationReplace` rule creates missing-XPath errors. Do not invent a `PatchOperationAdd`; the following section documents the actual named-bio selection interception. In particular `MechanoidHacker93` forces `DrugDesire=1` and `VideoGamer16` forces `DrugDesire=2`. Old saved-pawn Def identities are preserved, but no old-save load was run. Do not classify this narrow historical filter as completion of Culture character historical suitability.

**Single downstream acceptance, not PR-by-PR native testing:** verify all 30 loaded adult `spawnCategories` become the sentinel and each originally exists; no XML PatchOperation failure, unrelated categories/traits/skills unchanged; ordinary Vanilla/MO pawn generation still completes; existing saved Def names remain resolvable. Verify children's actual independent path **when implementing that path**, not by irrelevant mass pawn sampling. Vanilla 1.6 real loaded-Def acceptance **PASS** for 31 adult categories and ordinary pawn generation in [Project #39005480093](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005480093), Pickle 1/1, native ERROR 0, exit 0. MO/1.7 and save-load remain unverified. Do not automatically launch RimWorld in CI or add a new test matrix.


## Solid biography childhood exclusion (2026-10-17)

**User-visible goal:** Do not generate new named Vanilla solid biographies whose fixed childhood background depends on computers, robotics, off-world fleets, digital entertainment, virtual reality or mechanoid invention. This complements the separate 30-adult XML category gate. Do **not** remove any BackstoryDef, erase existing pawn backgrounds, change unrelated traits, or prohibit manually edited existing pawns.

**Actual path / source:** In RimWorld 1.6, `PawnBioAndNameGenerator.GiveAppropriateBioAndNameTo` chooses between named Solid and shuffled biographies. The Solid path uses `TryGetRandomUnusedSolidBioFor`, selecting a `PawnBio` name, `childhood` and `adulthood` bundle. Selection category filtering is based on the **adult** background; a category-free `Solid_Child` BackstoryDef cannot reliably be removed by blindly adding or replacing child `spawnCategories`. This method and original 1.6 signatures are independently supported by [Pawn generation process](https://www.rimworldwiki.com/wiki/Modding_Tutorials/Pawn_Generation_Process), [Solid bio selection](https://www.rimworldwiki.com/wiki/Backstories#Random_vs_player-generated) and [HAR's explicit 1.6-compatible patch of the same method](https://github.com/erdelf/AlienRaces/blob/master/Source/AlienRace/AlienRace/HarmonyPatches.cs).

**Code:** `Source/AMJ.Culture/SolidChildBioGatePatch.cs` is registered by the existing `CultureMealSeatBootstrap` Harmony `PatchAll()` startup (no new dependency). A postfix inspects a **successful Solid biography selection**; if its childhood `defName` is in the exact twelve-item blocklist, return `false` and `null` for the selected bio, so Vanilla's existing shuffled-bio fallback runs. It does not reroll large numbers of pawns or alter the SolidBioDatabase list. Successful other Solid selections and vanilla shuffled backgrounds are unchanged. Later external code that force-assigns a biography outside this selection method is out of scope.

**Exact twelve `Solid_Child` identifiers:** `ComputerGeek62`, `GlitterworldKid85`, `GlitterworldNerd20`, `MechanoidNerd10`, `SpaceCadet77`, `SpaceFanboy22`, `SpaceNerd97`, `CorporateSlave22`, `HackerKid98`, `HackerProdigy84`, `MechanoidHacker93`, `VideoGamer16`. The existing [owned 1.6/1.7 Core metadata evidence](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/VanillaCoreCharacterDefsSourceAudit20261016.md) confirms all twelve are childhood slot in `BackstoryDefs/Solid/Solid_Child.xml`, all missing direct `spawnCategories`, and the last two force `DrugDesire=1` and `DrugDesire=2` respectively. Their SF narratives were reviewed in [SF individual audit](SFBackgroundIndividualAudit.md), with historic English comment version limitations noted there.

**Production status (2026-10-17):** The updated 1.6 `Assemblies/AMJ.Culture.dll` **is packaged on `main`**. It was compiled from exact C# source commit `310f233cfb1f37d88dc64e31a38c882f63f40c6b` by the existing *build-only* isolated 1.6 job [Project #39004690614](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39004690614), SUCCESS; the binary is **8,704 bytes**, SHA-256 `2fa6ab6429bd9f81d48633bea0998b47c7255fbb8658b73addc697d8ddf24010`. The original chair-meal source is unchanged and included in the new assembly; the previous chair-only 7,680-byte DLL is superseded. The build did not start RimWorld. **The separate Vanilla 1.6 acceptance has now passed; no MO, RimWorld 1.7, or existing-save load success is claimed** merely by the build or that narrowly scoped acceptance. Final consolidated acceptance should prove the named-bio method is patched, blocked candidates fall through to normal shuffled generation, allowed named bios remain eligible, vanilla pawn generation succeeds without runtime errors, and existing saved pawns remain intact. Do not create PR-triggered native tests or repeat large generation matrices.


### One consolidated Vanilla 1.6 runtime acceptance (2026-10-17)

**PASS (actual game):** [Project Actions #39005480093](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005480093) executed the exact installed Culture production binary from source commit `18636756e1a722466009a013ad17d6e8997ce40d` (8,704-byte DLL SHA-256 `2fa6ab6429bd9f81d48633bea0998b47c7255fbb8658b73addc697d8ddf24010`) in a disposable original `RimWorld 1.6.4871 rev598` game. Reused the existing isolated Culture runner in `--suite background` mode; no chair test replay or PR-triggered native testing. Formal acceptance source: `Tests/Background16/BackgroundSteps.cs` and Project `Tools/PersistentHost/culture_chair16.py`.

| Test specification (actual assertion) | Evidence / result |
|---|---|
| 31 exact adult `BackstoryDef` instances remain loadable and each `spawnCategories=[AMJ_CultureExcluded]` | `AMJ_CULTURE_BACKGROUNDS16_LOADED_OK adult=31 child=12`, PASS |
| All twelve listed childhood `BackstoryDef` identities still load; `BodyPurist` remains | Same loaded-Def marker, PASS |
| Loaded `Transhumanist` `TraitDef` private `commonality=0`, not assumed to be a public C# property | Read via reflection from the actual game-loaded class, PASS |
| Actual Harmony postfix registered for `TryGetRandomUnusedSolidBioFor`; one real blocked fixed bio returns false/null; one real allowed fixed bio remains unchanged | `AMJ_CULTURE_BACKGROUNDS16_SOLID_OK blocked=MechanoidNerd10 allowed=FallenProdigy40`, PASS |
| One ordinary newly generated player `Colonist` receives none of the 43 blocked adult/child histories | `AMJ_CULTURE_BACKGROUNDS16_PAWN_OK`, PASS |
| One Pickle scenario; no engine errors, normal exit, unchanged source executable/Assembly-CSharp | 1/1 PASS, `native_errors=0`, `raw_log_error_entries=0`, exit 0, source hashes unchanged |

**Initial failed attempt, not a gameplay regression:** [#39005233520](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005233520) stopped before starting RimWorld because test-only code incorrectly referenced private `TraitDef.commonality` as a public member (CS1061). Corrected the test's field access, changed no production XML/DLL, reran only the affected scenario, and obtained the PASS above.

**Evidence bounds:** This tests the actual loaded categories for all 31 adults, retention of twelve child Defs, installed Harmony registration and actual blacklist behavior on **one excluded and one allowed SolidBio**. It does **not** force each of twelve children through an independent in-game random selection, directly exercise Vanilla shuffled fallback after every rejected candidate, prove no external direct-assign path, load a preexisting saved pawn, or test MO/1.7. Existing save identifiers are preserved in source and loaded Defs; that is not the same as save-load validation. Do not launch more native tests solely to raise counts. Culture's wider historical content and drug effects are not complete and Workshop release is unapproved.


## SF人物背景の第二次生成制限（2026-10-17）

**適用する仕様の正本:** [SFBackgroundSecondWave.md](SFBackgroundSecondWave.md) の24件。Vanilla 1.6/1.7の作者所有Core原Def台帳でID・年齢スロット・成人期生成カテゴリを照合し、Ludeon公式翻訳2系統が保持する英語原文から具体的なSF設定を確認した。これはSFの職業名だけを基準に除外したものではない。原文が宇宙船・惑星間交易・未来工学・デジタル娯楽等を前提にする間は**新規通常生成を制限**する。旧セーブで使用済みのBackstoryDef、固有名、能力、特性、初期所持品等は変更・削除しない。史実に転用可能な職業は原文の日本化候補として別扱いを維持する。

**製品実装:** 成人期は `Patches/AnachronisticAdultBackstories.xml` の対象を旧30件から**42件**へ拡張し、既存の `FerventResearcher72` 1件と合わせて**43件**。幼少期は `Source/AMJ.Culture/SolidChildBioGatePatch.cs` の固定人物の除外対象を旧12件から**24件**へ拡張する。したがって通常生成制限の原Def合計は**67件**。幼少期のカテゴリを不存在のXMLノードに追加するのではなく、既存のSolid人物選択経路の除外で対処する。外部Modの直接設定には一般保証を付けない。

**製品ビルド:** Project [build-only #39007502577](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39007502577) SUCCESS。Cultureソース `1abfddfbb1eb339c5d00915bd5d8dd2d3e944a85` を元のRimWorld 1.6/Harmony参照でコンパイルした、9,216バイトの `Assemblies/AMJ.Culture.dll`、SHA-256 `0d944eb47a1e648acdbc13ca75948e075e5ccfd71fefe1d4196c54d5a6c5be03` を同一topicブランチへ収録した。実ゲームは起動せず、既存の着席コードは変更していない。

**検証と互換の境界:** [過去のVanilla 1.6実ゲームPASS #39005480093](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005480093) は旧31＋12件のみであり、二次追加分24件の受け入れ試験は含まない。変更したC#の新製品DLLは正規RimWorld 1.6/Harmony参照でビルドされ、製品ソースと同じPRに収録済み。ビルド結果と必要な最後の実ゲーム受け入れは別に管理し、追加のサンプル数・ゲーム再起動を開発目的化しない。完全な784件の本文審査、MO/1.7、旧セーブ読み込みはいずれも未完了であり、公開許可にはならない。


## 中世日本へ転用可能なVanilla人物背景の改稿（2026-10-17）

**実装済み：15件（第一群5件＋第二群10件）。** 利用可能な仕事・人物の多様性を維持しつつ、SF世界での経歴を前近代日本で成立する物語へ置換する。ソース正本：`Patches/HistoricalOccupationBackstories.xml`（英語の`BackstoryDef` `title`・`titleShort`・`description`）、`Languages/Japanese/DefInjected/BackstoryDef/HistoricalOccupationBackstories.xml`（日本語の`title`・`titleShort`・`description`・解決後の`description`）。ゲームの `[PAWN_nameDef]` 変数を維持する。ゲーム本体の背景定義を**削除せず**、`defName`・`slot`・`spawnCategories`・能力ボーナス・`workDisables`・`requiredWorkTags`・強制Traits・所持品を変更しない。特定のSF DefNameが残っていても、通常表示のタイトルと経歴は史実相当へ置き換わる。本人が望む将来の改稿やほかの言語は別途対象にできる。

| Vanilla defName／slot | 表示名・題材の変更 | 維持する実ゲーム上の元データと判断 |
|---|---|---|
| `SpaceMerchant97`／成人・固定 | 宇宙商人 → **廻船商人**（Coastal merchant） | 社交+5、建築+4、製作+2、射撃+2、`Caring`不可。湊・市での売買、船の傭船、修繕、非情な利害関係として説明 |
| `SpaceSmuggler6`／幼少・固定 | 宇宙密輸人 → **密荷運びの子**（Smuggler's child） | 射撃・格闘・社交+3、医療+2、栽培・研究・芸術マイナス。関所・湊の通行と密荷運びの家業に改稿 |
| `SpacePirate55`／成人・シャッフル | 宇宙海賊 → **海上の略奪者**（Sea raider） | `Pirate`カテゴリと`Violent`必須、射撃・格闘各+4をそのまま使う。中世の商船への襲撃を説明 |
| `SpaceHunter79`／成人・固定 | 宇宙猟師 → **山野の猟師**（Mountain hunter） | `Hunter`カテゴリ、射撃+5、製作+3、医療+2など。弓・罠・野営の作業を説明 |
| `SpaceStationCook63`／成人・固定 | 宇宙基地の料理人 → **船中の炊事役**（Ship's cook） | 料理+6、格闘+4、`Caring, Social`不可。少ない水・食材を使う船の炊事、対人関係への不適応を説明 |

### 第二群（追加10件）：歴史的な職種・境遇への置換

| Vanilla defName／slot | 英日改稿の題材 | 維持するゲーム上の効果・制約 |
|---|---|---|
| `Computer80`／幼少・固定 | **算木使いの子**（Counting-board child） | 研究+4、製作+3、社交作業不可、栽培−3。荘園の物資勘定に従事 |
| `IndustrialOrphan13`／幼少・シャッフル | **鉱山の孤児**（Mining orphan） | 採掘+3、製作+3、研究−2。鉱石運搬と道具の修繕に従事 |
| `SpaceHumanitarian9`／成人・固定 | **施しの世話役**（Charitable caretaker） | 料理+4、医療+2、栽培+2、射撃−2。寺院での炊き出し・病人の世話 |
| `SpaceMarine16`／成人・シャッフル | **水軍の切込兵**（Naval boarding fighter） | 射撃+7、格闘+5、`Violent`必須。敵船に乗り込む水軍の戦闘員 |
| `SpaceRaider71`／成人・固定 | **海賊衆の頭領**（Sea-raider captain） | 射撃+8、社交+5、格闘+3。沿岸の襲撃者を率いる |
| `SpaceTactician28`／成人・シャッフル | **兵法家**（Military strategist） | 研究+4、射撃+4、`Intellectual`必須、社交・芸術作業不可。兵法の研究に従事 |
| `SpacerOrphan77`／幼少・固定 | **難船の孤児**（Shipwreck orphan） | 格闘+4、射撃+2、製作+2、芸術・採掘作業不可。難破後の自活 |
| `FactoryDrone58`／幼少・固定 | **工房奉公の子**（Workshop apprentice） | 勤勉系特性`Industriousness=1`強制を維持、建築+3、製作+3、芸術不可 |
| `FactoryWorker58`／成人・シャッフル | **荷運びの人足**（Pack-train laborer） | 調教+3、建築+3、製作+2、研究・芸術・料理作業不可。馬を使った荷運び |
| `SpaceyachtPilot89`／幼少・固定 | **船頭見習い**（Boatman's apprentice） | 社交+4、格闘+2、料理作業不可。旅人や役人の舟運を手伝う |

同じ`Patches/HistoricalOccupationBackstories.xml`と日本語`DefInjected`に追記。両群15件とも、**新規のDefを加えず**既存`BackstoryDef`の可視テキストのみ改稿し、67件の除外数、取得可能アイテム、ゲーム内薬物、座席C#、セーブ参照を変えない。原資料由来の`defName`には`Space`・`Factory`等の英字が残るが、英日表示の本文には未来技術を残さない。将来、その他の言語や他Modと競合する翻訳は別途判断する。第二群についてもゲーム内翻訳の採用順位・日本語描画は未検証。

**選定条件:** Ludeonの旧英語原文コメント2系統とユーザー所有Core 1.6/1.7の正規メタデータ（`VanillaCoreCharacterDefsSourceAudit20261016.md`）の`slot`・生成カテゴリ・技能・職務制約を突き合わせた。英語コメントと現行版の原文が完全一致することは保証しない。許可した職業の「名称だけ」を変えず、各行の能力・不適性に説明文を合わせた。日本文化に固有でない職業名を史実性の根拠として自動採用しない。

**影響・受け入れ範囲:** 当初の67件（成人43／幼少24）の除外ルールや`Transhumanist`・薬物・座席・既存Assemblyには一切変更を加えない。今回の5件は**除外からの復帰ではなく、新たな改稿のみ**（いずれも既存の除外リストには無い）。英語・日本語の解決後`BackstoryDef`タイトル・短縮名・説明が一致すること、5件とも元のカテゴリ・スキル等が不変であることを最終統合検証で確認する。新DLLのビルドは不要。**今回の日本語／英語表示は実ゲーム未検証**。現行以外の翻訳（第三言語）や原Defを再パッチする他Modについて、変更後表示の保証はない。旧セーブで同じDefを参照できる設計だが、セーブ実読み込みは未検証。

## RimWorld 1.7 バージョン別実装（2026-10-17）

製品Modの対応版を1.6と1.7に分離する。RimWorld 1.6の既存コンパイル済みDLL（9,216バイト、SHA-256 `0d944eb47a1e648acdbc13ca75948e075e5ccfd71fefe1d4196c54d5a6c5be03`）は一切変更せず `1.6/Assemblies/AMJ.Culture.dll` に移す。1.7用DLL（9,216バイト、SHA-256 `96a55e102c6ab0155f4c5336466afe6d398dbcefd915486bd31bd957cdf84e58`）は `1.7/Assemblies/AMJ.Culture.dll` に置く。共通の `Patches/`、`Languages/`、`About/` はルートに残し、`LoadFolders.xml` で `v1.6`／`v1.7` のロード先を明示する。**旧ルート `Assemblies/AMJ.Culture.dll` を残すと1.7に重複DLLが混入するため削除する。**

1.7 DLLは[Projectのビルド専用実行 #39015931527](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39015931527) で、Cultureソース `bd07ceb34619a992a934ca0a7add7be1f1ae8a8b`、正規Linux RimWorld `1.7.4977 rev1248` の `Assembly-CSharp.dll`（SHA-256 `3d4f1ddfe71bd8760cb488255e3bdeac9366f537dc1ef813cc6f8d09824e2da9`）、UnityStubs及び正規Harmony 3.0を使ってビルドした。食事先選択と固定人物生成制限の2つの製品C#を含む。ビルド実行ではゲームを起動せず、正規ゲームを変更しない。

**実ゲーム受け入れ基準（未確認）:** 1.7でロードしたときに正しいDLLだけが有効であること、食事のテーブルなしイス着席・Comfort・完食、テーブル隣接イス優先、イスなし時の立ち食い、`AteWithoutTable`の中立化、成人43・固定幼少24の生成除外と通常Pawn生成、英日表示の原定義、実行時ERROR0を必要範囲で確認する。1.6の旧版実ゲームPASSや1.7への単純コンパイル成功を1.7の実ゲームPASSに転用しない。DLC・MO・他家具・旧セーブについて未検証を維持する。


## 転用可能なSF背景32件の改稿完成 — 最終17件（2026-10-17）

旧[個別監査](SFBackgroundIndividualAudit.md)の `R` 32件について、先行15件の英日改稿に続き、残る17件の **英語`BackstoryDef/title,titleShort,baseDesc`と日本語`DefInjected/title,titleShort,baseDesc,description`** を追加した。実際の製品ファイルは `Patches/HistoricalOccupationBackstories.xml` および `Languages/Japanese/DefInjected/BackstoryDef/HistoricalOccupationBackstories.xml`。両ファイルを合わせて32件、英語96置換（各3箇所）、日本語128項目（各4箇所）。17件ともDef名・成人/幼少期スロット・spawnCategories・技能・職務/労働制約・強制Trait・能力値の変更はない。

| 原Def | 日本語改稿／職業・境遇 | 正規Core 1.6/1.7 台帳との整合 |
|---|---|---|
| `DeepSpaceMiner3` | 鉱山の鉱夫 | 成人、採掘+7、建築+3、格闘+2、ManualDumb/ManualSkilled必須 |
| `Engineer40` | 市の鍛冶職人 | 成人、製作+8、建築+2、社交+2、料理−2 |
| `GlitterworldEmpath26` | 村の仲裁人 | 成人、社交+8、医療+2、Social必須、暴力作業不可 |
| `GlitterworldOfficer60` | 警固の組頭 | 成人、格闘+6、芸術+4、Violent必須、単純労働不可 |
| `GlitterworldRoyal24` | 武家の縁組候補 | 幼少、社交・料理・芸術+2、単純労働不可、`Beauty=-1`の既存強制Traitは維持 |
| `HumanComputer83` | 荘園の勘定役 | 成人、研究+7、射撃+3、Intellectual必須、芸術不可 |
| `SpaceExplorer53` | 孤島の生還者 | 成人、射撃・社交+4、建築・栽培・採掘+3、Caring不可 |
| `SpaceMarine19` | 歴戦の荒武者 | 成人、射撃+6、格闘+4、Caring/Social不可、芸術・製作− |
| `SpaceMarine5` | 古参の足軽 | 成人、射撃+8、格闘+6、Caring/Social不可 |
| `SpaceMarine9` | 誓約の武士 | 成人、射撃+6、格闘+5、社交−2、芸術不可 |
| `SpaceMarine94` | 軍船の切込武者 | 成人、射撃+7、格闘+5、料理不可 |
| `SpaceNavyDoctor72` | 水軍の療治役 | 成人、医療+5、射撃+3、社交+2、初期医薬品置換 |
| `SpaceshipChef41` | 陣中の膳夫 | 成人、料理+7、医療+5、掃除不可、初期食糧置換 |
| `StarshipDoctor37` | 医術を究めた医師 | 成人、医療+8、研究+5、格闘−2、社交−3、初期医薬品置換 |
| `VoidspaceRaider94` | 仇討ちの無頼者 | 成人、射撃+4、製作+3、医療・採掘・格闘+2、芸術不可 |
| `WanderingHealer52` | 遍歴の療治人 | 成人、医療+7、社交+3、料理+2、暴力不可、初期医薬品置換 |
| `CorporateManager76` | 荘園の差配人 | 成人、社交+6、研究+2、Social必須 |

### 近未来アイテムの新規生成時のみの置換（6件）

**正本:** `Patches/HistoricalOccupationStartingPossessions.xml`。元の `possessions/<ThingDef名>` を持つ指定された `BackstoryDef` だけを書き換える。RimWorld原実装 `PossessionThingDefCountClass.LoadDataFromXmlCustom` は要素名をアイテムDefとして読んでおり、`<li><thingDef>` 形式のリストではない。XML XPath と置換後の要素名をこの仕様で静的確認した。物品ThingDef自体は削除しない。既存セーブ内の物品・職業Def・化学依存・DrugPolicy・所有者が抱える所持品には変更を加えない。

| 元Def | もとの初期アイテム | 新規割当時の初期アイテム | 目的 |
|---|---|---|---|
| `DeepSpaceMiner3` | `BlocksVacstone ×20`（Odyssey `MayRequire`） | `Steel ×20` | 宇宙素材を通常の金属材料へ。Odysseyが無く当該要素が不在なら**条件付きパッチは何も変更しない** |
| `Engineer40` | `ComponentIndustrial ×3` | `Steel ×30` | 工業用機械部品を修理用の金属材料へ |
| `SpaceNavyDoctor72` | `Penoxycyline ×10` | `MedicineHerbal ×10` | 合成予防薬ではなく傷の手当てに使える薬草薬へ |
| `SpaceshipChef41` | `MealNutrientPaste ×4` | `MealSimple ×4` | 栄養ペーストでなく普通の食事へ |
| `StarshipDoctor37` | `MedicineUltratech ×2` | `MedicineHerbal ×2` | 超技術医療品を薬草薬へ |
| `WanderingHealer52` | `MedicineIndustrial ×3` | `MedicineHerbal ×3` | 工業製医薬品を薬草薬へ |

`Steel`は既存Vanillaの抽象的な汎用金属素材として利用し、新たな製鉄設備や史実上の鋼材量を意味しない。`MedicineHerbal`と`MealSimple`も既存Vanilla Defを再利用する。Medicine/Grains/Alcohol製造等の他Modにレシピ、薬効、作物の所有を移動したわけではない。すべての元アイテムのDef自体を残し、他の入手経路や既存Pawn依存を破壊しない。歴史的職業の装備をゲーム中で個別生成する元経路は維持される。

**正本の参照・検証境界:** 元Defのスロット・技能・所持品は作者所有の[Core 1.6/1.7監査](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/VanillaCoreCharacterDefsSourceAudit20261016.md)（原配布物由来SHA付きmetadata v2）、旧設定の本文はLudeon公認の英語原文コメントで確認。後者が現行ゲームの本文とバイト一致する保証はない。今回はXMLの実装が正本であり、前回の1.6製品DLLをそのまま使用する。**実ゲームの表示、パッチのロード、初期所持品の変化は未検証**。最終的な集中受け入れで、この32件の日本語／英語テキスト、6件の新規割当時の所持品、43＋24件の除外制限維持、通常Pawn生成・セーブ参照を必要十分な項目として確認する。テスト目的の全背景784件抽選やPR起動ごとのゲームテストは実施しない。MO/1.7/旧セーブ実読み込みは未検証。今回の改稿完成をCultures全体の完成・公開許可とは扱わない。

### 原人物背景の説明文フィールド修正（2026-10-17）

1.7実ゲーム[検証 #39016338621](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39016338621)はイス食事3分岐・成人43・幼少24の制限でPickle1/1を通過したものの、当時実装済み15件の背景説明文のXPath `/baseDesc` でERROR15件となり、総合判定FAIL。正規1.7 Core XMLの[タグ照合 #39016659873](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39016659873)で当該15件全ての原フィールドが `description` であることを確認。原Core/Defsは1.6／1.7で同一SHA。

**確定修正:** その後並行して改稿された17件を含め、現行製品 `Patches/HistoricalOccupationBackstories.xml` の32件すべてで、説明の `xpath` と `value` の対象を `description` に変更する。タイトル・Def名・技能・カテゴリ・セーブ参照・Assemblyは変更しない。実ゲームでERROR0を確認するまで1.7総合PASSは主張しない。

### RimWorld 1.7 正規実ゲームの最終受け入れ結果（2026-10-17）

**PASS：** [Project Actions #39016905072](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39016905072)。Culture製品正本 `e7d98318bce64032be5609835b4f1022953a1104`、正規RimWorld Linux `1.7.4977 rev1248`、1.7用製品DLL SHA-256 `96a55e102c6ab0155f4c5336466afe6d398dbcefd915486bd31bd957cdf84e58`。実ゲーム1シナリオのPickle `total=passed=1`、FAILED/SKIPPED/FLAKY=0、正常終了、Unity・FMOD ERROR0、元ゲームデータ変更なし。実際に検証した項目：バニラStool単独着席・快適性上昇・完食、より遠いテーブル隣接イス優先、イスなし時の立ち食い完了、ロード済み成人43件の生成カテゴリ制限と幼少24件のDef保持、固有人物の除外ゲートと対照例、通常Pawn1人の生成・禁止背景不在。これら7つの到達マーカーが各1回。

**先行FAILと修正の区別：** 初回 [#39016338621](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39016338621) では上記のPickle処理はPASSしたが、人物説明文15件の無効 `/baseDesc` XPath によるERRORが15件あり総合FAIL。原定義[フィールド照合 #39016659873](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39016659873)に従い後続17件を含む全32件の正本XMLを `description` へ修正後、今回のERROR0 PASSを得た。C# DLLの再ビルドは不要だった。

**未検証の切り分け：** 今回のERROR0はXMLロード時に失敗しなかった証拠だが、32件の説明文を個別に画面表示して英日確認したこと、6件の初期所持品を個別生成したこと、1.6の最新DLLとMOの総合、1.7のMO/他Mod、旧セーブ読み込み、DLC、食堂の正のThoughtを実ゲームで確認したことを意味しない。確定済みの食事先・人物生成の挙動を保ちながら、個別の変更に実益がある場合だけ追加確認する。


## 背景からの強制特性・共同寝所の特性評価（2026-10-17）

### SF設定を持つ強制特性の流入を最小限で止める

実体参照はProject保存のオリジナル Vanilla Core 1.6/1.7 `BackstoryDef` 台帳。英語旧本文はLudeon公式翻訳に保存されたコメントを参照し、現行ゲーム本文との完全同一性は保証しない。

| Def | 現行の職務・強制特性 | 新規普通生成だけに適用する処理 |
|---|---|---|
| `TestSubject90` 成人期 | 遺伝子治療の人体実験。Offworld/Outlander/Pirate/Cult、`BodyPurist=0`強制、射撃+6/格闘+8/社交−4 | `spawnCategories=AMJ_CultureExcluded`。強制Trait/旧セーブの参照は残す |
| `HunterOfTheKing53` 成人期 | 遺伝子改変獣の襲来。Offworld、`BodyPurist=0`強制、射撃+6/格闘+4/医療+3 | 同上。狩猟と武士の一般的役割は削除対象ではなく、この未来設定のみ停止 |
| `SpaceBartender0` 成人期 | 宇宙基地の酒場。Offworld/Pirate、`Kind=0`強制、`PsychiteTea ×3` 所持 | 同上。既存のKindや薬物Def、ドラッグ依存・既存品を変更しない。酒場経歴・初期品の歴史的改稿は薬物仕様の担当範囲に触れるため保留 |
| `ApprenticeOracle83` 幼少期 | Cultカテゴリ、祭儀の訓練＋未来的な冷凍睡眠装置。研究+2/社交+3/医療+2 | 通常生成を維持し、神事見習いの子（shrine ritual apprentice）へ英日改稿。技術上のSF本文のみ置換 |
| `DischargedSoldier53` 成人期 | Offworld/Pirate、`DrugDesire=2`・`TooSmart=0`強制、射撃+6/格闘+4/料理+2 | 通常生成を維持し、追放された足軽（expelled foot soldier）へ英日改稿。酩酊物への嗜好は意味を残す。薬物の定義・必要量・入手方法は変えない |

前回の成人期43件＋今回の3件＝**成人期46件**、幼少期の生成除外**24件**は変更なし、通常生成制限は合計**70件**。従来の歴史改稿32件＋今回の2件＝**改稿34件**。生成制限と改稿が互いに重ならないこと、元背景・特性・技能・強制Traitの識別子が存続することが必要。Patch `Patches/AnachronisticAdultBackstories.xml`、`Patches/HistoricalOccupationBackstories.xml`、`Languages/Japanese/DefInjected/BackstoryDef/HistoricalOccupationBackstories.xml`が実装正本。

`DrugDesire` はこのチャットでは**背景に付随する強制指定の維持だけ**を扱い、薬物アイテム・処方・入手・依存・特性効果の再設計は実施しない。一般的な`BodyPurist`の発生率や低技術義肢に関する好悪も無断変更しない。`Transhumanist`の生成制限と既存セーブの定義維持は従来通り。

### Greedy/Jealous 共同寝所を認める特性効果

**問題:** Vanilla `ThoughtWorker_Greedy` と `ThoughtWorker_BedroomJealous` は `Pawn_Ownership.OwnedRoom` を参照する。共同寝所では自分の寝具があるのに個室所有者とみなされず、Greedyは無条件に「寝室がない」の−8を受け、Jealousは質のよい他人の寝室との比較が0基準になる。共同就寝の実際の部屋の質を無視し、古代～中世日本の標準的な寝所を劣悪と決めつける。

**実装正本:** `Source/AMJ.Culture/SharedLodgingTraitPatch.cs`、`Patches/SharedLodgingTraitThoughts.xml`、日本語`Languages/Japanese/DefInjected/TraitDef/SharedLodgingTraits.xml`と`ThoughtDef/SharedLodgingTraits.xml`。Harmony postfixで**個室のOwnedRoomがなく、割り当て済みの寝具（`ownership.Bedroom`）が`RoomRoleDefOf.Barracks`のときだけ**Sharedの部屋を利用する。Greedyは元の`Impressiveness`段階とMood値（最大−8→十分豪華なら0）を評価する。Jealousは**同じ寝所に寝る人を比較対象から除き**、自分より豪華な別の共同寝所または個室の住人への不満だけを残す。通常の個室・寝具なし・囚人・客人・非ColonistではVanillaを変更しない。RimWorldの部屋の質、Shared SleepingのBarracks Thought、食事・睡眠妨害、Royaltyの個室要求は変更しない。

表示テキストは「個室が欲しい」ではなく「立派な寝所が欲しい」へ変更する。Greedy/JealousのTraitDef/ThoughtDef、負のMoodステージ値、固有Def名、自然発生率、Traitの相互排他関係、既存セーブ参照は保存する。既存の`Greedy`/ `Jealous` の趣旨とペナルティを消さず、共同寝所における判定だけ時代設定に適合させる。Harmonyの有無でDef読み込みの依存条件は従来通り。**C#新処理の1.6/1.7製品DLLへの再コンパイル必須**で、旧DLLでの座席・人物生成PASSをそのまま新処理のPASSと扱わない。

**テスト前提／未検証:** 静的にオリジナル`Pawn_Ownership.Bedroom`/ `RoomRoleDefOf.Barracks`/ `ThoughtWorker`の公開APIと実装を参照した。コンパイル確認後、最後の一度の実ゲーム受け入れで、共同寝所（粗末・豪華）でGreedyの段階に差が出ること、全員同室でJealousが誤発火しないこと、より良い別室との比較は維持されること、個室・寝具なしの元挙動、英日名称の実際のロード、既存43/24→46/24除外と2件改稿を確認する。現時点の**実ゲーム効果・日本語表示・セーブ読込・MO互換は未検証**。テストだけを先に増やさない。


### Two-version packaged production status (2026-10-17)

The **same exact owner source commit** `b1bc7e5a43c1f2e3017369a111f3c1700d413a80` was compiled into both DLLs without launching RimWorld:

| Version | Isolated build-only evidence | Packaged production path | Size | SHA-256 |
|---|---|---|---|---|
| 1.6 | [Project #39018173163](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39018173163), SUCCESS | `1.6/Assemblies/AMJ.Culture.dll` | 11,264 B | `0895332d4a817bf4227b00bf4d707943e793fc15c379b9950df76ec2112c9458` |
| 1.7 | [Project #39018244508](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39018244508), SUCCESS | `1.7/Assemblies/AMJ.Culture.dll` | 11,264 B | `9005cd7a98f7eec02ce51fd0f4ec1d3dc804a212f62df651c8de32a2eb98138d` |

The existing `LoadFolders.xml` selects these version-specific assemblies. The 1.6 SDK collects the new C# file implicitly; the 1.7 isolated builder's explicitly enumerated sources were updated in [Project PR #608](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/pull/608). Both binaries include the previously accepted chair source, existing Solid-child gate, and the new Greedy/Jealous harmony patches. **Only compilation was verified**, not actual mood values or loaded-game effects. Previously published Vanilla 1.7 real-game evidence #39016905072 relates to the *previous* 9,216-byte DLL and the 43+24 list, not the new 11,264-byte binaries and 46+24 restrictions.


## Cultural Thought and social-norm triage outside seating (2026-10-17)


**作者方針の更新（2026-10-17。下記の葬送調査候補より優先）：** 葬儀や葬送手段を細分化する新システム、専用設備、死体処理の複雑化は**Cultureの今回の対象外**。Vanillaの墓・火葬・死体の目撃・未埋葬の心情は原則維持し、風葬などを特別扱いする判定も増やさない。下記の葬送調査は研究記録であり、実装待ちタスクではない。

**背景・特性担当への候補（作者が区別を承認、2026-10-17）：** 鎌倉〜戦国期の戦場を経験した武士・足軽、戦場の遺体処理に従事した者などについて、**「死体を見慣れている」経歴**を検討してよい。戦闘が身近な職務・経験であっても、**殺害が無条件に許された、あるいは仲間の死に無関心だったと解釈しない**。1232年の『御成敗式目』第10条は口論・酒宴での殺人や仇討ちを刑罰の対象としており、武家法にも殺害を規制する規範がある（[玉川大学資料・第10条現代語訳](https://www.tamagawa.ac.jp/sisetu/kyouken/kamakura/goseibaishikimoku/)／[日本大学法学部図書館・御成敗式目解説](https://www.law.nihon-u.ac.jp/library/htmls-201901/about_us.html)）。ゲーム上は少なくとも、**戦場で敵を殺す／戦場の死体を目撃する／家族・戦友を失う／腐敗や未処理の遺体を見る／非戦闘員・捕虜を殺す**を混同しない。前二者への習熟が後三者の悲しみや倫理的反応を消す根拠にはならない。宗教的な殺生観との葛藤も排除しない。**新規Def、特性、出現頻度、心情の対象・軽減数値は未決定**。既存MOの `DankPyon_Fighter`／`DankPyon_Veteran` の関連Thought無効化とVanillaの仕組みを先に照合し、重複機能を増やさない。背景・特性の具体的な評価・設計・実装は別作業が所有し、本系統ではコードやXMLを変更しない。


**対象の境界:** イス単独での食事、`AteWithoutTable`、背景・特性の定義と生成は本項の作業外。背景・特性を扱う別系統へユーザーを伝言役にしない。`SleptInBarracks` の一般的な扱いは Shared Sleeping 所有。本項は**中世日本に合わない可能性がある Thought／社会的判定の優先調査結果**であり、新たなDef効果数値や全住民共通の倫理観を承認・実装したものではない。

1. **葬送と死体:** Vanilla の `Colonist left unburied`（仲間の遺体が未処理なら心情−10）や近くの死体目撃Thoughtを、すべて無効化するのは不適切。中世日本には風葬・寺院への遺体移送など多様な葬送慣行があり、死穢の観念も存在した。ゲーム上の区別候補は「遺体の無秩序な放置」と「適切に指定された葬送／処理」。単なる世界観だけを理由に数値を0にしない。Vanilla墓・火葬は既存処理で有効で、別途未実装の葬送設備を前提にしてはならない。根拠: [国立国会図書館所蔵論文](https://ndlsearch.ndl.go.jp/books/R000000004-I2840449)、[中世の葬送と遺体移送](https://cir.nii.ac.jp/crid/1390001205138598400)、[RimWorld Thought一覧](https://rimworldwiki.com/wiki/Thoughts)。
2. **捕虜の処遇・処刑・追放:** Vanilla の捕虜売却・殺害・無実の捕虜死亡・臓器摘出などのThoughtは独立に存在する。中世日本でも時代・身分・関係・行為の正当性で評価は変わるため、捕虜一般の心情低下を「中世だから」一律撤去する案は不採用。具体的な不整合が確認されれば、対象Thoughtと発火条件を個別に決める。既存の [Greyer morals (Continued)](https://steamcommunity.com/sharedfiles/filedetails/?id=2018947066) は当該Thoughtの緩和という先行事例だが、食人や人体素材まで一括して変えるため、日本文化再構成用の必須Modにはしない。
3. **婚姻・親族・社会評価、裸体:** 現行Vanillaの恋愛／婚姻、裸に対するMood／Opinionは研究候補に留める。歴史上の婚姻や服飾の差を理由に、既存の関係生成・同性／異性の指向・家族のセーブ参照や最低限の身体保護を包括変更しない。実際のプレイ上の不自然な発火条件が分かった場合だけ、時代・地域・場面を限定する。

**Medieval Overhaul 既存処理との境界:** 提供された原Mod 1.6.2.2の `1.6/Patches/Core/Add_Trait_Thoughts.xml` は、`ObservedLayingCorpse`、`ObservedLayingRottingCorpse`、捕虜死亡・処刑・追放・死亡目撃等の `nullifyingTraits` に MO の `DankPyon_Fighter`／`DankPyon_Veteran` を追加しようとする（2操作間に重複指定あり）。これは**MO特性に関連する既存の例外処理**であり、Culture側から全体的な死亡Thought調整で上書きしない。ロード後の重複・有効性は未確定。個別特性そのものの改変は別作業。

**再利用判断（当初の監査結果）：** [Sky Burial](https://steamcommunity.com/sharedfiles/filedetails/?id=2389525728) は指定場所への遺体安置という技術的先行事例だが、既存公開版は1.4向けの鳥葬モチーフであり、日本の風葬とそのまま同一視できない。Greyer morals も含め、追加の必須依存・独立した新設備・新C#を正当化しない。**後続の承認済み区別に基づくMOの局所的Thought修正は、以下に記す。** 新しい葬送処理の実装は引き続き対象外。


### MOの戦闘経験特性と死亡・捕虜心情の局所的整合（2026-10-17）

**実装対象と理由：** 作者が承認した「戦場・死体への慣れ」と「親しい人物の死亡への悲しみ／無実の捕虜や処刑への社会的評価」の分離を、MO 1.6.2.2の原XML `1.6/Patches/Core/Add_Trait_Thoughts.xml` と照合。既存MOは `PawnWithGoodOpinionDied` と `KnowPrisonerDiedInnocent` の `nullifyingTraits` に `DankPyon_Fighter` および `DankPyon_Veteran` を追加し、後者の `DankPyon_Veteran` は同ファイル内の2操作で重複追加を試みる。さらに `KnowGuestExecuted`、`WitnessedDeathAlly`、`ColonistBanishedToDie`、`PrisonerBanishedToDie`、`ObservedLayingRottingCorpse` に `DankPyon_Veteran` を追加する。前者3件だけでなく、**味方の死亡目撃・死に至る追放・腐敗死体**も、通常の死体目撃への耐性とは独立して扱う必要がある。

**Cultureの製品修正：** `Patches/MedievalOverhaulDeathThoughts.xml` を追加し、MOD名 `Medieval Overhaul` がある場合のみ、次の7定義から、*MOが追加した該当Traitだけ* を `PatchOperationConditional` + `PatchOperationRemove` で削除する。対象ノードがなければ無操作とする。1.6／1.7共通のXMLだが、実物の比較根拠は配布MO 1.6.2.2のみ。

- `PawnWithGoodOpinionDied`：`DankPyon_Fighter`、`DankPyon_Veteran` をnullifyingTraitsから外し、親しい人物の死亡時にVanillaの心情判定を使う。
- `KnowPrisonerDiedInnocent`：同じ2特性を外し、無実の捕虜の死亡への反応を戻す。
- `KnowGuestExecuted`：`DankPyon_Veteran` を外し、Vanillaの正当・不当な処刑の区別と既存心情値を戻す。
- `WitnessedDeathAlly`：`DankPyon_Veteran` を外し、味方の死亡を目撃したときの感情を残す。
- `ColonistBanishedToDie`／`PrisonerBanishedToDie`：同特性を外し、死に至る追放を戦歴だけで無関心扱いしない。
- `ObservedLayingRottingCorpse`：同特性を外し、**腐敗死体の目撃を通常の死体目撃とは区別**する。

**維持するもの：** `ObservedLayingCorpse` のMO戦士・古参兵の例外は維持し、**通常の死体目撃への慣れ**を尊重する。`WitnessedDeathNonAlly` の他人・敵の死亡目撃、死に至らない追放、`DeniedJoining`、`ColonyPrisonerEscaped` に関するMO例外、VanillaのTraitDefと背景、全Thoughtの元の心情数値・発火条件は**変更しない**。腐敗した死体を見慣れることは、戦場経験から自動的には推定しない。Cultureの `About/About.xml` にMOの正確なPackage ID `DankPyon.Medieval.Overhaul` を任意の `loadAfter` として指定し、MO側の既存Patch適用後にCultureの修正を行う。Harmony以外の必須依存は追加しない。

**2026-10-17 正規RimWorld 1.6＋MO実ゲームのロード済みDef受け入れ PASS：** [Project Actions #39025744804](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39025744804)。検証対象のCulture製品コミット `d08eb3d530f5517370c4257b6c3f1634d76b1a9b`、1.6用DLL SHA256 `0895332d4a817bf4227b00bf4d707943e793fc15c379b9950df76ec2112c9458`、RimWorld `1.6.4871 rev598`、原MO `1.6.2.2` とVEF/SYR、隔離コピー内のMO音声だけWAV変換。ロード済みThoughtDefの7対象から当該Fighter/Veteranの `nullifyingTraits` が消えていること、`ObservedLayingCorpse` の両特性による耐性が維持されていることを実ゲーム内で確認。合わせて既存のMO派閥・PawnKind・生成経路の単一シナリオも成功。Pickle 1/1、`AMJ_CULTURE_MO_THOUGHTS16_OK` 1回、Unity ERROR 0、終了コード0、元ゲーム変更なし。**実際の死亡・処刑・追放イベントを発生させてMood値を確認したテストではない。** 元MP3の未加工実行、1.7＋MO、旧セーブの実読み込みもこの実行では未確認。**別系統の背景・特性Stage2で後に追加された87件の生成制限については、このテストが受け入れを示すものではない。** XML静的確認の結果も維持。Steam未アップロード。

## 第2段階 — 既存Vanilla/MO人物背景・特性の時代不整合修正（2026-10-17開始）

**実装原則：** 第1段階の829件台帳を既存の作業索引とし、新しい全件棚卸しを行わない。通常Pawn生成で実際に選択可能な原背景だけを対象とし、固有Pawn専用定義を除外する。日本独自の新規背景・特性は第2段階が完了するまで追加しない。元のDef名、旧セーブ参照、強制Trait、技能・作業制限は、個別設計を変更しない限り保存する。歴史的に転用可能な職業は、制限よりも史実に合う改稿を優先する。日本語新規本文は承認手順を維持し、未承認のままゲームに追加しない。

**実装済み第1修正群：** `Patches/AnachronisticAdultBackstories.xml` に成人13件のカテゴリ制限を追加。1.6/1.7正規Coreの既取得メタデータは、対象13件すべての `Adulthood`・カテゴリ存在・両バージョン一致を支持する。旧公式英語原文コメントで明示的なSF内容を個別照合。制限対象・内容根拠・生成経路・現行原文との証拠上の差・転用可能な候補は [BackgroundTraitAudit.md の第2段階項](BackgroundTraitAudit.md#第2段階第1修正群--既存sf背景13件の通常生成制限2026-10-17) に記録する。カテゴリを `AMJ_CultureExcluded` に置き換えるだけで、元Defや既存Pawnを削除しない。

**数値（生成を制限したDef単位）：** 成人48→**61**、幼少26はそのまま、計74→**87**。歴史改稿**34**件、六つの初期所持品置換、既存Trait・MO専用Thought補正、1.6/1.7バイナリは変更なし。旧原文アーカイブと現在のライセンスCore長文の完全一致、および87件のロード後動作は未検証。これを最終Stage 2完了や公開許可とは扱わない。

**次の作業順序：** 第1修正群以外の実際に通常生成され得るSF背景→史実改稿可能な仕事→残るTraitの効果と強制付与→Stage 2の一括受け入れ。MOの `DankPyon_Fighter` / `DankPyon_Veteran` と死亡・捕虜Thoughtの既存局所修正を再実装しない。実ゲーム起動は機能修正群の完成後に必要十分な一回へ集約し、編集ごとに繰り返さない。

### 第2段階の追補 — 一次分類の見逃し3件を生成制限（2026-10-17）

**第2修正群：** `AlcoholicTrucker93`、`BloodgameSurvivor6`、`MedicalScientist16` の成人3件を、既存 `Patches/AnachronisticAdultBackstories.xml` の通常生成カテゴリ制限へ追加。原文理由、正規1.6/1.7メタデータによる `Solid_Adult`/カテゴリ確証、技能・WorkDisableを保持する根拠は[人物背景監査の第2修正群](BackgroundTraitAudit.md#第2段階第2修正群--一次分類で見逃されたsf背景3件の生成制限2026-10-17)を参照。貨物輸送・医療・戦歴という一般的な職業自体の全面禁止ではない。未承認の日本語改稿や追加日本独自Defは書き込まない。旧セーブの定義と初期所持品・Traitは変更しない。

**新計数：** 成人61→**64**、幼少26維持、生成制限総数87→**90**、歴史的改稿34件維持。1.6/1.7現在の本文と旧公式アーカイブ原文の完全一致は未確定。実ゲームは未起動で、最終的なロード結果・生成率の受け入れは未了。第2段階全体は継続中。

### 第2段階・第3修正群 — ShuffledのSF背景2件を通常生成から制限（2026-10-17）

既存原Vanilla 1.6/1.7でカテゴリ・slot・Shuffled生成経路が一致する `TestSubject15`（幼少期：SF超兵士施設）と `IllegalShipwright76`（成人期：宇宙船の違法エネルギー兵装）を、それぞれ既存 `Patches/AnachronisticShuffledChildBackstories.xml`、`Patches/AnachronisticAdultBackstories.xml` の `spawnCategories=AMJ_CultureExcluded` に追加。採否は元の未来的原文に対するものであり、人物の一般的な船大工職、戦傷者や子ども全般を排除する判断ではない。技能・作業制限・元Def・旧セーブ参照・追加Traitは変更しない。史実に沿う日本語背景文章は未承認であり、英訳や新規背景作成はしない。分類根拠と原カテゴリ：[BackgroundTraitAudit](BackgroundTraitAudit.md#第2段階第3修正群--生成経路が確定したshuffled背景2件2026-10-17)。

**更新後の正本計数：** 成人65（成人XML64＋独立XML1）、幼少27（Solid24＋Shuffled3）、生成制限合計**92**、既存歴史的改稿**34**。静的原ソース照合のみで、今回のRimWorld実機確認はなし。第2段階の残る史実改稿候補や固有のみの生成経路問題は保留。日本独自新規背景・特性は第3段階まで不可。

### 第2段階・第4修正群 — 現代の職業背景2件とTraitの強制経路を整理（2026-10-17）

`VideoProducer83` と `CosmeticSurgeon36` は、元のCore 1.6/1.7の `Solid_Adult` と通常生成カテゴリを対象別に確認した。映像制作・現代美容整形という独立業態をそのまま許可できないため、原の人物能力・Def・既存Pawnを保持したまま `Patches/AnachronisticAdultBackstories.xml` の通常生成カテゴリを一時的に制限。既存の芸能・文筆・医師一般は除外しておらず、適切な史実本文・技能整合を備えた改稿で再利用可能。証拠、元カテゴリ・技能、付与Trait：[BackgroundTraitAudit 第4修正群](BackgroundTraitAudit.md#第2段階第4修正群--改稿承認待ちの現代専業背景2件を通常生成制限2026-10-17)。

**現行計数：成人67（XML66＋別XML1）、幼少27（Solidゲート24＋Shuffled3）、通常生成制限94。既存史実改稿34。** 版別の正規メタデータは一致するが、現在の配布Core長文・1.6/1.7ロード後動作・旧セーブは未検証。

**既存Traitの作用・強制付与：** 再取得や829件再監査は行わず、原Coreの元の強制割当6背景に対する `Transhumanist`、`BodyPurist`、`DrugDesire` の経路を確定した。 `Transhumanist` は自然付与 `commonality=0` と強制背景 `FerventResearcher72` のカテゴリ除外を維持。 `BodyPurist` は通常の義体化欲求とは別の反対特性であり、他の元背景との整合判断まではそのTraitと2件の強制割当を保持する。 `DrugDesire` は既存成人 `DischargedSoldier53` と、ゲート済みSolid幼少2件から強制されるので、緊急心情対処・依存・離脱等を保持する他作業と衝突させず元効果と強制割当を維持する。 `Greedy` / `Jealous`、MO Fighter/Veteranの既存パッチは変更なし。

**文章承認の時期：** 作者の2026-10-17指示により、既存背景・特性の日本語改稿案の確認はPhase 2の文章以外の修正・生成経路整理が終わった段階で**まとめて一回**行う。未承認の日本語本文を製品XMLへ適用したり独自英訳を先行実装しない。日本独自の新規背景・特性は引き続きPhase 3。不要なRimWorld起動・CI自動ゲームテストは避け、成果物変更がまとまった段階で必要最小限の1回の実機受入れを行う。

### 第2段階・日本語改稿の一括確認用候補原稿（2026-10-17、未承認）

作者指定に従い、文章以外の第2段階対応が終わるまで**途中の日本語校閲は依頼しない**。追加の史実転用候補35件（成人32・幼少期3）を[Stage2JapaneseReviewDraft.md](Stage2JapaneseReviewDraft.md)に**承認待ちの日本語原稿**として用意した。内訳は既存Solid成人19件、Shuffled成人13件、Shuffled幼少3件。元のDefID・スロット・元の技能やWorkDisables・強制Trait・ゲームデータ・所持品・生成カテゴリは変更していない。既存34件の製品上の英日改稿や94件の生成制限とも重複しない。

この35件は**製品に反映されていない**。そのため現時点の原ゲーム文にSF・現代語が残る可能性がある。最終の一括作者レビューで日本語の題名・略称・本文を確定した後に、確定版のみを英訳して原のBackstoryDef XML・日本語DefInjectedに組み込む。現行1.6/1.7ライセンスCore長文本文との完全一致、さらに各Solid名前付き背景の個別通常生成可能性は、既存メタデータだけでは証明できないため、レビュー前の全件無条件実装は行わない。第3段階の日本専用新Defは引き続き対象外。

現在の第2段階の文章以外の状態：既存SF個別監査の除外（X）42件は全て生成制限済み、転用可能（R）32件は全て既存の34件の英日改稿に含まれ、再作業しない。追加の未修正・転用可能候補は今回の35件などであり、残る曖昧なSolid通常到達性は未証明のまま。既存Traitの強制経路・効果は変えず、薬物系の並行仕様と矛盾する全削除を避ける。実ゲーム検証は原稿の確定とソース反映後に集約する。

### 第2段階・第5修正群 — 研究・技術系SF成人背景5件の生成制限（2026-10-17）

正規Vanilla Core 1.6/1.7原メタデータで成人通常生成カテゴリが存在し、旧公式原文で**全脳マッピング・ソフトウェア自動化・ロボット工学・即時通信技術・遺伝子実験**が職歴の核をなす `NeuroScientist19` / `SoftwareDeveloper84` / `ExiledResearcher44` / `EnergyResearcher89` / `MadScientist31` を、`Patches/AnachronisticAdultBackstories.xml` でカテゴリ選択から暫定的に外す。原のバックストーリーDef、技能・workDisables・Trait・所持品・既存Pawnは変更しない。史実に転用できる医術・伝令・技術師等の職能を一律に排除する趣旨ではない。個別根拠：[BackgroundTraitAudit 第5修正群](BackgroundTraitAudit.md#第2段階第5修正群--追加のsf研究技術職5件の通常生成制限2026-10-17)。現行ゲームの原文完全一致や自然抽選は未実機検証。

**現行の実装制限：成人72（成人カテゴリXML71＋独立1）、幼少27（Solidゲート24＋Shuffled3）、合計99。** 製品へ適用済み既存歴史改稿は34件、一括作者承認待ち日本語原稿は35件。原稿・翻訳の承認を得る前にゲームデータへ挿入せず、文章以外の残作業を先に進める。固有Pawn専用除外と旧セーブ参照維持の原則、現行Trait・薬物系並行開発の所有境界は維持する。


## 玄関ゾーン・擬似履物脱着（2026-10-17 作者承認／両版DLLビルド済・実機未検証）

**ロードスレッド修正（2026-10-17）:** 正規RimWorld 1.6＋MO隔離実ゲームの薬物検証 [Project #39042030479](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39042030479) で、薬物関連Pickle 1/1自体は成功したが、`Map.FillComponents` がバックグラウンドロードスレッドで `EntranceZoneMap` の static readonly `Material` を初期化し `Tried to create a material from a different thread` ERROR×2を発生させた。このため実行全体はFAIL。描画用 `ProgressFilled` / `ProgressEmpty` を静的フィールド初期化から `MapComponentDraw` 時の遅延生成に変更する。Unityの色・バー寸法・保存キー・履物処理・TPSには変更なし。各版の再ビルドと実機再検証で ERROR=0 と玄関描画を確認するまでは完了扱いしない。薬物の合格マーカーだけでランタイムPASSを主張しない。


### 文化的目的・責務

屋外から屋内へ土足のまま移動するRimWorldの振る舞いを、日本での土間・板間・座敷の区別に合わせて変更する。中世の全住宅に独立した近代的玄関があったという設定にはしない。**玄関ゾーンは家具でも床材でもなく、土足と非土足の生活空間の境界**をプレイヤーが指定する仕組み。全面板張りの家屋でも指定できる。生活習慣に属するためCultureが所有し、Environmentの床材やShared Sleepingの寝所仕様には変更を加えない。

Cultureは人物背景・特性・心情・食事習慣・嗜好品・玄関の生活作法を含む**総合的な日本文化オーバーホール**。この一貫した責務に属する限りCultureがAMJ内で最も大規模となっても許容し、Mod規模だけを理由に分割しない。ただし他の既存Modの実装責務を無条件に統合しない。

### プレイヤーに見える確定方針

1. 床や地形の種類に依存せず、任意の通行可能セルに重ねて設置できるプレイヤー指定「玄関ゾーン」を用意する。通常の建物運用で玄関なしでも問題なく移動・作業でき、倉庫・土間・作業場などは土足のまま使用できる。
2. 対象ポーンが玄関ゾーンを通って土足可能側→土足禁止エリアに移るとき、その場で一時停止し**履物を脱ぐ擬似キャスト**を実行する。土足禁止エリア→土足可能側では**履く擬似キャスト**を実行する。専用履物アイテムや実際の装備交換は行わず、進捗バーで動作を可視化する。
3. 脱着完了後、ポーン単位の**擬似土足／裸足状態**を切り替えて、元の移動・仕事を再開する。単に同じゾーン内を歩く、同じ状態で再入場するなどで繰り返しキャストしない。
4. 裸足状態では**屋外の履物が運ぶ土汚れ**が板間・座敷に持ち込まれることを抑える。血液、嘔吐、作業由来の汚れ、動物由来の汚れ等は変更しない。裸足なら屋外の土上でも絶対に汚れない、とはしない。
5. 徴兵、戦闘、火災・脅威からの退避、緊急救助等では停止・キャストを強制せず、行動が阻害されないようにする。敵・動物への遅延効果も持たせない。
6. 複数の出入口・屋根付き土間・廊下などを考慮し、**玄関を横切った方向とプレイヤー指定の土足禁止エリア**で判断する。玄関と土足禁止エリアは互いに別のセルで辺（上下左右）を共有するときだけ有効。無効な玄関も設置は可能だが、脱ぎ履きせず、指定時に警告する。屋根・床材・Room判定には依存させない。

### 実装時の初期設定と保留条件

- キャスト時間の**試験用初期値は90 ticks（約1.5秒）**とする。これは確定バランス値ではなく、最終値はプレイ時の負担を見て決める。
- 既存CultureのHarmony実装を再利用する。ゾーン定義・セーブ永続化・再ロード・マップ離脱/帰還・ゾーン撤去・キャスト中の割込みを扱う必要がある。
- 移動Jobの強制置換による運搬・食事・予約喪失を避ける。具体的な停止・再開処理とRimWorld 1.6/1.7のFilthTrackerの安全な介入点は、両版の実ゲーム/参照ソースで確認してから製品実装する。通常通行のパフォーマンスとループ防止を優先する。
- 初期はプレイヤー管理の人間系ポーンを対象とする。来客・訪問者・搬送中のポーン、既存Modの履物Apparel、裸足そのものの速度/温度効果は**初期の確定仕様に含めない**。後から必要なものだけ個別に検討する。
- 仕様承認だけでは実装完了を意味しない。C#ソースと両製品DLLはこの後の実装群で構築したが、保存形式やLoaded-Gameの実機証拠はまだない。既存の別機能のPASSは流用しない。

### 既存Modと独自実装の差分（2026-10-17、公開説明の先行比較）

| 候補 | 確認範囲 | 玄関ゾーンとの差分 |
|---|---|---|
| Vanilla / Medieval Overhaul | 既存Culture所有の挙動と仕様 | 擬似的な脱着状態とゾーン境界による切替は未実装。床そのものの追加とは別 |
| VE系の衣服追加 | 衣服・装備の外部追加は先行手法。最新版の全Defsは未監査 | 靴のアイテム化が必要なく、文化的な土足境界とキャストを扱う。VE内容Modは依存・公式互換対象外 |
| [Doormats](https://steamcommunity.com/sharedfiles/filedetails/?id=3239838811)（1.5/1.6対応、旧[SYR](https://steamcommunity.com/sharedfiles/filedetails/?id=1508778962)） | 作者の公開説明：革・布の設置家具。通過したポーンの汚れを落とし減速。出血・徴兵・逃走等では減速しない | 「マットで足を拭く」清掃設備であり、履物脱着のキャスト、ゾーン、土足／裸足状態の持続は目的外 |
| その他非VEの玄関・靴脱着Mod | 現時点で同等の配布済み動作を確認できていない。存在しないとは断定しない | 製品実装の前に近似候補の配布Defs/コード・依存・ライセンスを限定調査し、完全な代替がないか確定する |

**独立機能の必要性:** Doormatsと汚れ軽減の結果は部分的に重なるが、Cultureは家具設置/清掃ではなく、床に依存しないゾーンの境界で文化的な脱着状態を切り替える。Doormatsを依存に追加・その機能を置換・新規家具を販売する方針ではない。既存実装のコード流用や互換性は未検証のため保証しない。

### 実装後の最小受け入れ条件（RimWorld 1.6 / 1.7）

- 土足禁止エリアの境界を玄関経由で往復すると、脱ぐ/履くキャストを一度だけ可視化し、移動・作業を失わず完了する。玄関なし・非隣接・迂回侵入のケースではキャストしない。
- 板張り住宅と土間付き住宅の両方で、屋根に関係なく**指定された土足禁止範囲**の境界で切替が成立すること。脱着時は土汚れのみ抑制し、血液などは抑制しない。
- 徴兵・退避・救助中は着脱が行動阻害にならず、通常移動で経路探索が無限に反復しない。
- ゾーン変更・セーブ/ロード後に状態が破綻しない。両対応版の正規製品DLLをビルドした後、機能一式についてまとめて最小限の実ゲーム受け入れを行う。PRごとの起動やテスト数の増加は行わない。

### 第1実装群（2026-10-17、ソース・両版製品DLL構築済み／実ゲーム未受入）

- **指定と保存**：`Source/AMJ.Culture/EntranceZoneDesignators.cs` と `Patches/EntranceZoneDesignators.xml` でZone/Areaメニューに「玄関ゾーン」「玄関ゾーン解除」を追加。実セル集合は `EntranceZoneMap : MapComponent` が `Scribe_Collections` で保存する。既存の栽培／備蓄ゾーンと排他的な `Verse.Zone` は使わず、床・他ゾーンの上に重ねる。日英UIキーは各言語の `Keyed/EntranceZone.xml` に収録。
- **ポーン状態**：`EntranceFootwearState : GameComponent` がポーンの一意IDごとに裸足状態を保存する。マップを跨いだときの状態継続を意図する。キャストの進行は一時データとして保持し、ロード後に必要なら再開始する。装備・アパレル・ベッド・室内温度等は変更しない。
- **立ち止まりと再開**：既存のCulture Harmony起動時に `Pawn_PathFollower.PatherTick` Prefixを登録し、**玄関指定セルに入る直前の居室側と、同じ連続した玄関セル群から出る直後の居室側**を比較して、反対のときだけ玄関上で最長90tick停止する。元のJobの最終目的地は使わない。既存Job、Path、運搬予約には触らず、終了時は元のPatherTickへ戻す。進捗バーを表示する。徴兵／精神状態／明示的な救助・消火・逃走・戦闘などのJob／現在セルの危険時は省略する。
- **汚れ**：靴を脱ぐ際に `Pawn_FilthTracker.carriedFilth` に蓄積した `Filth_Dirt` のみ廃棄する。血液・吐瀉物など他の汚れや、既存RimWorldのFilth配置・生成処理は変更しない。これは「土汚れを室内へ運び込まない」ための第一実装であり、素足歩行時のすべての汚れ生成を無効化するものではない。
- **現在の未達範囲と既知の実装上の制限**：C#／XML／Keyedの実装を完了し、正規ライセンスゲーム参照で1.6／1.7両版DLLをコンパイル・PRに収録した。ただし**実ゲーム起動・挙動は未検証**。玄関へ入る前と出た後が同じ室内／屋外ならキャストしない。**屋外→玄関A→室内→玄関B→屋外**の一続きのJobでも、玄関Aで脱ぎ玄関Bで履く想定。経路変更やセーブ再読込時に進入元が取得できない場合は、推測による誤キャストを避けて通常移動を継続する。実ゲームでの正しいセル移動・複数玄関通過・室内廊下・ドア前後の `Room.PsychologicallyOutdoors` 判定は未検証。

**製品化判定**：両ゲーム版でC#ビルドが通り、変更したDLLを実際にパッケージしてから、最小1回に集約した実ゲーム検証で①塗布/解除と重複ゾーン②屋外→室内→屋外のキャスト各1回③持ち込んだ泥と血の扱い④運搬Job維持・緊急時省略・セーブ再読込を確認する。実ゲーム確認前に公開可能・動作確認済みと報告しない。

**ビルド証拠（同一の製品C#ソース SHA `bb70077c953a4cf13c6ecf01775006df8e4b8da5`）**：
- RimWorld 1.6：Project [build-only #39035330411](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39035330411) SUCCESS。`1.6/Assemblies/AMJ.Culture.dll`（20,480 bytes、SHA-256 `3d2568cae26cbe73ba67424bd955cfed4725f3411a25bfad80b1e7d30e62a2ec`）。元の1.6正規Managed参照とHarmonyでコンパイルした。
- RimWorld 1.7：Project [build-only #39035637637](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39035637637) SUCCESS。`1.7/Assemblies/AMJ.Culture.dll`（20,480 bytes、SHA-256 `fa64784c472ce8b6f1c16eeceff0f810d8b69110486adcd444b4b0165d3f62b8`）。元の1.7.4977 rev1248 Managed参照でコンパイルした。元ゲーム非変更。
- DLLはGitHubのベース64ビルドログ→Git Blob→コミットでバイト列のまま移送。以上は**C#コンパイル成功**の証拠であり、1.6／1.7のロード、玄関ゾーンの表示・移動・進捗、血液・泥・緊急時の挙動のPASSではない。`About/About.xml`の公開向け機能説明は、実ゲーム受入前には未更新のままにする。


### 第2実装群：複数玄関の経路ごとの判定修正（2026-10-17）

**不具合原因と対処**：第1実装では玄関に入る前の空間と `Pawn_PathFollower.Destination.Cell` の内外を比較していた。屋外→玄関A→屋内→玄関B→屋外という、始点と終点が屋外の単一運搬Jobでは、玄関Aの脱ぐ処理を省略してしまう。これを `Pawn_PathFollower.nextCell` に沿った**各玄関の局所的な通過判定**に置き換える。ゾーンに入る前の空間を記録し、連続した玄関ゾーンセル群から出る直前に、その次のセルの内外状態と比較する。進入/退出が同じ側なら何もしない。違う場合のみ、直前の玄関セルで90tickキャストして土足状態を切り替える。後続の玄関では再度判定する。アプローチ記録は元のJobおよびMapへ紐付け、別Job/Mapでの誤再利用を防ぐ。

**対象範囲**：同一建物の複数出入口、別建物への連続運搬、2マス以上に塗った連続玄関ゾーン、玄関ゾーンを通ったが元の側へ戻る経路。ゲームのJobを作り直さず、元の予約・経路・速度は維持する。入口のドアや屋根そのものを履物変更設備にしない。現行保存キー `AMJCultureEntranceCells` と `AMJCultureBarefootPawnIds` は変更しない。

**修正コードのビルド状況**：Cultureソース `dd0b58870c64d3699849b25ffdcea47b2c5c555b` を両ゲーム版の正規Managed参照でコンパイルした。RimWorld 1.6 [Project build-only #39037191464](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39037191464) SUCCESS、DLL SHA-256 `117676e0bc079e787ab31bdd6adb236320431706a54cdfb1c15e9cb580dd0c3d`（20,992 bytes）。RimWorld 1.7 [Project build-only #39037194754](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39037194754) SUCCESS、DLL SHA-256 `80ff5898835546722490598abf0e5c1b3808c127ce6836c535cd32e036f9c775`（20,992 bytes）。双方、実ゲームでの動作確認は未実施。成果DLLはバイナリのまま `1.6/Assemblies`／`1.7/Assemblies` に反映する。

**受け入れ境界**：C#コンパイル成功は実ゲームでのZone/Job/FilthのPASSを示さない。ゲーム起動は分割せず、玄関機能の最終受け入れ時にAから入りBから出る**単一Job**の通過を追加して確認する。


### 第3実装群：土足禁止エリアとの辺隣接必須（2026-10-17 作者承認・第1/第2実装群を上書き）

- **二種類の指定セル**：プレイヤーは従来の玄関ゾーンのほか、独立した「土足禁止エリア」を任意の通行可能セルに指定／解除できる。両者は床やVanillaの既存 Zone と共存するが、玄関セルと土足禁止セル同士は重ならない。保存キーは玄関 `AMJCultureEntranceCells` を維持し、土足禁止 `AMJCultureNoShoesCells` を追加する。旧セーブで土足禁止設定がない場合、旧玄関は非隣接・機能無効として残り、既存ゾーンの再指定を強制しない。
- **隣接と発火**：辺（上下左右）で玄関と土足禁止範囲が接するときのみ、玄関を横断して土足可能セルから土足禁止セルへ進むと脱ぐ、逆向きに進むと履く。斜め接触、1セル以上の空白、両側とも土足禁止、両側とも土足可能ではキャストしない。複数連続した玄関セルのどの端が土足禁止に接するかを局所経路で調べる。RimWorldの部屋・屋根判定やJobの最終目的地を参照しない。
- **非隣接玄関の扱い**：玄関の指定自体は許可するが、接する土足禁止範囲がないときは指定時に警告し、土足状態や汚れを変更しない。後から土足禁止範囲を指定すれば有効になり、範囲を撤去すれば即座に無効になる。塗りつぶし操作では警告を連続通知しないよう間引く。玄関を通らずに土足禁止範囲へ侵入しても自動で靴を脱がず、泥の除去も発動しない。
- **対象ポーン・Job**：従来どおり管理下の人型のみ、90tickの擬似キャスト、Job予約/経路は維持。緊急時は妨げない。エリア編集時は途中のキャストを取り消す。進入記録は一時データ、ポーンの土足状態は既存GameComponentに保存する（変更せず推測で同期しない）。
- **軽量化**：各セルが玄関／土足禁止かはHashSetで判定し、玄関がない／禁止エリアがないマップ、玄関から離れた移動を早期終了する。配置中の連続玄関セル探索と警告通知は**編集時のみ**、通常の移動tickでは行わない。イベント介入点の再検討・TPS実測は第3実装の両製品DLL構築と実ゲーム機能受け入れの**後**、別途記録する。
- **確認待ち**：1.6/1.7の実ゲームでの指定UI、警告、斜め／間隔／両側同属性、同一Jobでの複数玄関通過、保存とロード、予約・泥/血の取り扱いを確認する。ビルド成功と実ゲームPASSは別の事実として扱う。
- **最新ソースの製品DLLビルド済（実ゲーム未受入）**：Culture実装ソース `96dedd1e3df1c23f55a09fe04d713834c2e3b0a5` を正式な各ゲーム版のManaged参照でコンパイルし、それぞれCultureバージョン別Assembliesに収録した。RimWorld 1.6 [Project build-only #39039921469](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39039921469) SUCCESS、DLL 24,064 bytes／SHA-256 `edafc7fd37ab1f0f588ccd03bfa8dfb883ef7cac4e8015bafc9c1e55bb39f757`。RimWorld 1.7 [Project build-only #39040425332](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39040425332) SUCCESS、DLL 24,064 bytes／SHA-256 `f94fa23f9b80b8d9abb28d96e955874b74dafafd63a00f2d514bb4fb9ff847db`。両版はコンパイル成功のみであり、Harmony/Zone UI/警告/セーブ・実際の経路処理/TPSのPASSを意味しない。

### 玄関機能の実装完了後に行うTPS影響確認（2026-10-17 作者指定）

**実施順序**：玄関・土足禁止範囲の最終採用方式をC#／XMLに実装し、RimWorld 1.6／1.7の製品DLLを更新し、玄関通過・履物状態・汚れ・Job継続の機能確認を行った**後**にTPS影響を評価する。TPS計測を実装前の先行作業にしたり、計測のためにコード実装を遅らせたりしない。玄関機能はまだ正規の実ゲーム受け入れ未完了で、現時点のTPS実測値はない。

**測定の基本条件**：同一バージョン・同一Mod構成・同一セーブ／同規模ポーン数・同一ゲーム速度と負荷条件で、玄関処理なし／玄関処理ありを比較する。玄関0件の負荷、通常の住宅1件、複数玄関を通る移動、ポーンの移動が集中する条件を、既存機能の最終受け入れに集約して確認する。FPSや時間経過の印象ではなく、取得可能な実測TPS・tick当たり処理時間（必要時はHarmonyパッチ単位の実行時間／割当量）を使う。TPSが上限に張り付いて差が見えない条件では、測定条件・限界を明記する。

**性能上の実装原則**：毎tickの全ポーン・全セル走査、移動中の部屋や屋根の繰り返し探索、判定ごとのLINQ・GC割当、描画の常時更新を避ける。マップ単位で「玄関／土足禁止」の有無を早期判定し、必要な境界の通過にのみ定数時間のセル参照を行い、キャスト中だけ進捗描画する。Jobや予約を再生成しない。具体的な介入点や保存形態は最終実装と対応版のAPIで確かめて決めるため、`Pawn_PathFollower.PatherTick` 継続や新しい土足禁止エリアの追加を、このTPS要件だけで確定仕様にしない。

**報告と判定**：実測環境・対象コミット・1.6／1.7のバージョン・比較シナリオ・測定値・差分・観測限界を記録する。負荷が目立つ場合は、改善対象となる処理箇所を特定し、実装を修正してから影響箇所のみ再測定する。事前に根拠のないTPS低下許容率や「影響ゼロ」を保証しない。性能測定をPASSと称して実ゲームの機能確認を代替しない。

### 第2段階・第6修正群 — Solid幼少期8件と低頻度Solid_Rare成人期4件（2026-10-17）

**通常経路と実装：** 名前付き `Solid` Bioは通常Pawn生成でも候補になるため、`Solid` ファイル所属や固定人物名だけを理由に監査除外しない。過去のRimWorld生成経路・開発者由来Rare人物の抽選について確認したうえで、SFの人工培養児童、快楽回路依存、核動力の宇宙船、VR訓練などを述べる既存Solid幼少期**8件**について `Source/AMJ.Culture/SolidChildBioGatePatch.cs` の既存 `TryGetRandomUnusedSolidBioFor` Postfix対象を24→32に追加。成人側でカテゴリ抽選されるNamed Bioに対し、選出結果の幼少 `defName` を条件に拒否する。元Def、元能力・仕事・強制Trait、固有人物への直接割当・既存セーブは維持。

低頻度の開発者由来 `Solid_Rare` 9組もすべて通常生成不能とは断言できない。元の `spawnCategories` をもつ成人期で、明確に現代の`game developer`／`simulation developer`／`mod designer`を表す `JoeCustomAdulthood`、`LiamCustomAdulthood`、`OskarCustomAdulthood`、`TynanCustomAdulthood` の**4件のみ** `Patches/AnachronisticAdultBackstories.xml` の通常カテゴリ制限に追加。それ以外のRareの商売・採掘・古今の遊戯職は自動で削除しない。1.6/1.7正規Core元定義のカテゴリ、元の強制Trait（VatgrownAssassin20 `Psychopath=0`、ReclusiveProdigy96 `TooSmart=0`、Liam `Industriousness=1`、Tynan `Industriousness=-2`）は維持する。個別証拠と全ID：[人物背景監査 第6修正群](BackgroundTraitAudit.md#第2段階第6修正群--solid幼少期8件とsolid_rare成人期4件2026-10-17)。

**計数とビルド境界：** 元の制限99→**ソース上111件**（成人76＝カテゴリXML75＋独立1、幼少35＝Solid C#32＋Shuffled XML3）。**今回新規の幼少8件はC#ソースにのみ存在し、配布パッケージの1.6／1.7 DLLにはまだ含まれない。** そのためプレイ可能な製品で111件が有効になったとは報告しない。1.6/1.7両DLLを、玄関・薬物生成の並行変更を含む**最新Cultureソース全体**から改めてビルド・実物統合する必要がある。過去の製品DLLで上書きしない。実ゲーム受け入れは、残る日本語原稿の一括承認と製品反映後に集約する。既存改稿34件・一括未承認原稿35件は変更なし。日本独自の新規Defは第3段階まで追加しない。


### 第2段階・第6修正群の1.6／1.7製品DLLビルド完了（2026-10-17、実ゲーム未検証）

先行する「新しいSolid幼少期8件が製品DLLに未収録」という記述を、**本項の実物ビルド結果で更新**する。過去のCulture PR #78の両版製品DLLは、入口機能自体のビルドが成功していたが、使用された固定ソース `96dedd1e3df1c23f55a09fe04d713834c2e3b0a5` には `SolidChildBioGatePatch.cs` の制限が**24件しかない**（Culture PR #74版は32件）。新しい32件をすべて含む、同じCultureソース `366888bccc897b25ee714e1092c00cc3f7080ab2` にProjectの**既存手動起動・ビルド専用**ワークフローを固定し、1.6／1.7の両方を正規ライセンスゲームの参照DLLで再コンパイルした（Project PR #648 `e7f9347626e52b87cf017a6a7c9a2e34b606d490`）。

- **RimWorld 1.6:** Project [build-only #39041617611](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39041617611) SUCCESS、出力 `AMJ.Culture.dll` 24,064 bytes、SHA-256 `688c32a5ab5b1dbefe73ccffc2ebcb2287156cf565018c7e1ed60e0167161b9d`、GitBlob `296e0a6d02b2491dc1108d8d6a530bc81273750b`。
- **RimWorld 1.7:** Project [build-only #39041895169](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39041895169) SUCCESS、出力 `AMJ.Culture.dll` 24,064 bytes、SHA-256 `7fb340337eaf9c998e3beb866b21ebcc92e9135201b6ca07c287ae150e8fb004`、GitBlob `909598f3fd73673c95187d1dac4d355b1748b199`。
- **バイナリ実物：** 両GitBlobを元のバイト列のまま `1.6/Assemblies/AMJ.Culture.dll`／`1.7/Assemblies/AMJ.Culture.dll` に置換し、ソース変更用PRではなく別のバイナリ統合PRへ収録。元のビルド対象と統合元Cultureとの差分は、開始時点で製品C#に影響しない `Tests/Drug16/DrugSupplySteps.cs` の追加のみと確認した。玄関機能、初期薬物依存抑制、既存共同寝所などの並行C#も同じソースから取り込まれており、旧DLLへの巻き戻しを防止した。

**判定：** ソース生成制限111件（成人76＋幼少35）に必要な**製品バイナリのビルド／統合準備は完了**。ただしRimWorld 1.6／1.7の実ゲームロード・Pawn生成・旧セーブ・MO併用は一度もこの新バイナリで検証していない。**ビルド成功をゲーム動作PASSと呼ばない。** 作者が指定した残る日本語改稿35件の一括確認・承認／翻訳実装と、実ゲームの最小集約検証は引き続きPhase 2の残作業。Steamアップロード・新しい第3段階Defは実行しない。


### 第2段階・第7修正群 — SFのSolid幼少期23件・初期所持品17件を追加処理（2026-10-17）

正規の元Core 1.6／1.7メタデータで `Solid_Child`、`Childhood` の生成候補と判明し、公式アーカイブ英語原文が宇宙移住・冷凍睡眠・電子計算機・ゲーム・近代バイオ研究等を前提とする既存**23件**を、既存 `SolidChildBioGatePatch.cs` の固有名付き背景通常選択から外す。元の幼少期Def、強制Trait、技能、仕事制限、固有人物／旧セーブ、直接背景割当は維持する。特に `CaravanChild53` の性別に関する元設定等を不適切として排除する趣旨ではなく、宇宙社会に依存する元叙述を**未改稿のまま生成させない**ための暫定制限とする。史実に合う原稿が作者の一括承認を得た場合は同Defを再利用可能。項目別の根拠と全IDは[背景監査第7修正群](BackgroundTraitAudit.md#第2段階第7修正群--solid幼少期23件と通常生成時の時代不整合な所持品17件2026-10-17)。

また元Vanilla 1.6/1.7の `BackstoryDef/possessions` を点検し、生成時に持ち込む工業銃器・人工照明・近現代衣服・先進兵器・人体改造部品・禁止合成薬を、実在Vanillaの弓・槍・薬草薬・素材・簡単食・銀貨へ既存 `Patches/HistoricalOccupationStartingPossessions.xml` 内で**17件**置換。元のアイテムのDefやセーブ済み現物は削除せず、元DLC限定アイテムは存在チェック付きXPathとする。元の6件と合わせ**23件**を生成時に調整する。歴史的背景の本文34件、追加の未承認日本語原稿35件、背景付与Trait自体、他系統の薬物供給抑制コードを変更しない。狙いは通常出現する所持品の時代整合であり、新たなDLC前提を導入しない。

**実装対象の累計：既存背景の通常生成制限134件**（成人76、幼少58＝Solid C#55＋Shuffled3）、生成所持品置換23件（既存6＋今回17）。今回の幼少ゲート23件は**製品DLLの更新前**なので、実ゲームロードにおける効果は未証明。最新Culture全ソースから正規1.6/1.7 Managed参照で両版DLLを再ビルドして実物を差し替えた後も、ビルドをロード済み受入れと混同しない。残りの時代不整合可能性フラグ54件と改稿原稿は、一括日本語確認まで適切な範囲だけ選別し、未来職名だからと一律削除しない。


### 第2段階・第7修正群の正規1.6／1.7製品DLL統合（2026-10-17）

既出の「新規Solid幼少期23件はC#ソースのみ・DLL更新前」という状態は、本項の**正規元ゲーム参照による成果物実物**で更新する。原Culture `228d86be846ee75444f3331957a824f866ea9918`（第7修正群PR #86を含み、玄関のMaterialスレッド修正まで反映）を、Project PR #658 `dfda0e341948ed2359c0117e3102b58cb28bef25` によって既存の**手動発火ビルド専用**1.6／1.7ワークフローの共通ソースとして固定した。**現在の固有名付きSolid幼少期ゲートは55件**、対象総数は成人76＋幼少58＝134件。

- **1.6:** Project [build-only #39043583308](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39043583308) SUCCESS。RimWorld正規1.6ゲームManaged参照から生成した `AMJ.Culture.dll` **25,600 bytes**、SHA-256 `527629e8353ec878940c99b1ddb869f3d4a8747ec436107c466efc12355083ba`、GitBlob `f99c8a134bc76630d3f9969138d3862c30e5fd50`。
- **1.7:** Project [build-only #39043586558](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39043586558) SUCCESS、正規RimWorld 1.7.4977 rev1248 Managed参照、`AMJ.Culture.dll` **25,600 bytes**、SHA-256 `b87b7bf518cf8b3f76b9116de27be6f3a95abd4d5dd9d2aacb59490116cbb2e3`、GitBlob `e638bbc36c1275c6e6d71024da0c9faf71215854`。

二つの独立成果物GitBlobを `1.6/Assemblies/AMJ.Culture.dll`／`1.7/Assemblies/AMJ.Culture.dll` へ**別製品PRで実際に収録**する。前回PR #81の32件版DLLへ戻さない。使用したビルド元に対し、DLL作成直前に確認した後続変更はドキュメントのみであり、1.6/1.7の異なるゲームAPIや連携する玄関・ドラッグ・共同寝所パッチも同じ製品ソースから取り込まれた。元Coreの所持品XMLを含む第7修正群コードと現在ソースが一致する。

**これはコンパイルと実物格納の成功であり、今回のRimWorld 1.6／1.7の実ロード、新規Pawn生成（55ゲート／23所持品）、旧セーブ互換、MO併用の動作確認ではない。** 別機能の既存ゲーム確認結果でPhase 2をPASS扱いしない。残る一括未承認日本語背景原稿35件と、他の未改稿候補の時代整合を処理後、承認済み原文だけを翻訳・ゲーム適用し、最小限の結合ゲーム受入れを行う。新しい日本独自背景・特性は引き続き第3段階まで追加しない。
