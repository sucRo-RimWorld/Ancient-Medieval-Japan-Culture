# AMJ Culture — design and first acceptance

## Development purpose, phase and priority (2026-10-16 review)

Culture's product goal is to remove historically incongruous **character generation, culturally contingent mood/social judgments, and intoxicant gameplay** from ancient-to-medieval Japanese settlement play **without breaking the useful Vanilla/MO gameplay roles**. One neutral meal Thought is a small completed slice, not the product goal. Culture combines Backstories, generic Pawn-generation interactions, Traits, relevant Needs/Thoughts and psychoactive Drug/effect/supply UX; Shared Sleeping, general furniture, medicinal remedies, brewing, and ordinary food Jobs remain other owners' responsibilities.

**Actual implementation maturity:** only the narrow `AteWithoutTable` zero-mood XML prototype has accepted detailed behavior and source. Its static XML check passed; no loaded-game acceptance exists. Historical background/trait handling, drugs and minimum enza functionality have *approved ownership/boundaries or research candidates*, but **not** complete per-feature effect values, spawn handling, recipes, migration or final implementation specifications. Do not describe Culture overall as implemented or entering integration validation.

**Next Culture specification work:** prioritize the user's high-execution-impact historical drug/intoxicant UX together with related `DrugDesire`/Need/Trait rules. Preserve the emergency mood-rescue role, addiction/withdrawal consistency and existing-save references. In the same bounded character-generation design effort, resolve the evidenced `Transhumanist` natural and forced-Backstory paths in Vanilla/MO, without indiscriminate TraitDef deletion or re-auditing all third-party backgrounds. Work from the existing Project [drug research](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/PsychoactiveSubstancesCandidate.md) and [character research](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md); make each accepted final decision here before adding its source. Enza material/Comfort/job adapter and broader ethics/other backgrounds remain separate later decisions without fixed next-task precedence. This local sequencing does **not** override the Project-wide P0 Environment/Grains repair and closeout work. The approved drug UX review has elevated execution priority ahead of new Ironmaking expansion; general Culture reconstruction classification remains P2.

**Validation timing:** keep the acceptance contract below as a *future promotion gate*, not the current development task. Defer the dedicated Culture 1.6 Vanilla/MO E2E harness, 1.7 compatibility checks, CI construction and broad regression matrix until a coherent implemented behavior requires those checks or a concrete unknown blocks its design. A single narrowly scoped loaded-Def check is permissible if needed to resolve a specific implementation ambiguity. Do not repeat retired stool demonstrations, grow tests to justify the runner infrastructure, or promote source parsing to runtime PASS. Do not publish the unaccepted prototype.

## Drug and character-generation rollout decisions (2026-10-17)

**Player-facing purpose:** a normal ancient-to-medieval Japanese settlement should not routinely encounter obviously out-of-period intoxicants or machine-body aspirations, while retaining usable RimWorld mood-emergency responses, ordinary recreation, meaningful intoxication/addiction consequences, and character variety. This is a **design and release-order specification** from prior evidence, not a claim that the pending per-item drug replacements or new Trait patches are implemented.

### Preserve gameplay before suppressing supply

- **Do not ship a drugs-off patch.** Until a historically defensible replacement is actually available and manually consumable in the ordinary supported profile, leave existing mood-rescue options usable. Test count or an external runner's availability does not justify prematurely changing them.
- **Keep actual alcohol and its intoxication/addiction behavior.** Vanilla Beer and MO alcohol are retained as existing gameplay until the beverage/fermentation owner supplies historically appropriate production; Culture owns the related personality/effect/availability rules, not brewing recipes.
- **Protect three different functions:** everyday low-risk recreation, emergency mood rescue with meaningful price/side effects, and acquired chemical dependence/withdrawal. A single tea item is not a substitute for all three. Combat/fatigue stimulants and extraordinary curing are separate gameplay functions and are not automatically converted into mood drugs.
- **Never merely rename psychite tea to historical Japanese tea.** Historical tea is daily refreshment with era/region relevance, not a fictional psychite product or an emergency stimulant.
- **Keep existing Def references and dependencies until a real transition is designed.** Restrict new generation/trade/recipes only after replacement availability, DrugPolicy, Thought/Need/Hediff and existing-item/old-save consequences are addressed. Do not erase chemical Defs from saved pawns, forbid the only supply to addicted pawns, or automatically rewrite their current dependencies.

### Vanilla and MO drug dispositions — current non-destructive decisions

| Existing content | Action now | Necessary decision before any gameplay edit |
|---|---|---|
| Vanilla Beer; MO Ale/Cider/Mead/Wine | Retain existing supply/effects; no Culture-side new brewing chain | Historic selection/production belongs to brewing, while Culture resolves any drug effects and traits |
| Ambrosia, psychite tea, smokeleaf joint | Retain provisionally; historical substitution is a candidate, not yet implemented | Identify defensible material, effect/risk and availability **and** keep light recreation/mood-use roles |
| Yayo, flake; MO HappyPowder | Retain provisionally despite historical mismatch | Preserve at least one genuine crisis mood-response option and its acquisition, manual use and cost before restricting originals |
| Wake-up, go-juice; MO VigorPill/BattleElixir/Stardust/Luhix | No broad delete/rename | Assess separate fatigue, combat and extraordinary-effect roles; do not invent historical equivalents merely to preserve labels |
| Luciferium | No change in this slice | Extraordinary irreversible-dependency system needs a separate save/progression decision |
| MO Paregoric, Miracle, KnowledgePotion, HornetHoney; Vanilla Penoxycyline | No change in this slice | Medical effects and fantasy content require separate owner/era decisions; no blanket replacement under recreational drug work |

This table **does not** approve each drug as historically appropriate. It specifies what must *not* be broken while the final replace/restrict/retain choice remains open. Source/class evidence: [psychoactive substance research](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/PsychoactiveSubstancesCandidate.md).

### Trait and Backstory decisions

- **`DrugDesire` degree 2 / 1 / −1:** retain its identifiers, original effective behavior and save references for now. Interest/fascination/teetotaler must be evaluated as different dispositions, **not** renamed indiscriminately to "likes sake", alcohol tolerance or addiction. Any future behavior change must match the final set of usable substances, DrugPolicy and dependencies. 上戸／下戸 (physiological tolerance) and sake preference remain distinct proposals; no invented trait degrees, spawn weights or effect numbers.
- **`Transhumanist` (body modder):** intended new-generation contract is to prevent the anachronistic mechanized-body aspiration in Culture-covered ordinary human generation. This is *not* a prohibition on historically plausible prosthetic treatment. Preferred narrow implementation candidate is to disable Vanilla natural-roll commonality and **separately** remove unsuitable forced-Backstory entry points; neither a commonality-only patch nor deleting TraitDef meets the goal.
- **`FerventResearcher72`:** Vanilla source evidence establishes that the adult Backstory forces `Transhumanist` and participates in Offworld/Pirate categories; existing 1.7 evidence confirms real forced assignment. Its older publicly preserved English narrative describes a home planet near “transcendence”, bureaucrats and bioethics committees, **not merely a period-neutral scholar**. The preferred Culture behavior is therefore **to exclude this Backstory from applicable ordinary period-Japanese generation**, rather than silently remove its forced Trait and preserve an incongruous narrative. Before production, ensure the owned 1.6 source still has that meaning and choose the smallest safe category/filter intervention. Do not delete BackstoryDef or globally rewrite forcedTraits. [Original evidence ledger](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md); [older translated source preserving English notes](https://www.madxingjin.tech/git/SiteAdmin/RimWorld-ChineseSimplified/src/commit/48684ca0a6c47e26376f01313840254a323f5652/Core/Backstories/Backstories.xml).
- **Existing pawns:** do not remove their `Transhumanist`, `BodyPurist`, or existing mood/opinion ThoughtDefs; avoid retroactive emotional/stat changes. Treat old-pawn conversion, if ever justified, as a separate choice. MO Ideology memes are not a substitute for Vanilla Trait generation; uninstalled DLC is out of scope.
- **External Backstories:** no automatic global removal of third-party forcedTraits, and no compulsory audit of every historical/medieval Backstory package merely to ship a bounded Vanilla/MO fix. Optional compatibility must be grounded in a reported conflict or supported profile.

### Next actual feature decision and validation threshold

1. **First substantive design decision:** choose a historically supportable, obtainable mood-emergency substance/behavior (material, effect intensity, side effects, acquisition, manual use, addiction/withdrawal), then map the corresponding Vanilla/MO drug and `DrugDesire` interactions. **No specific replacement item or numeric effect has yet been approved**; do not fabricate this bridge or remove the originals first.
2. **Independently implementable candidate:** a limited `Transhumanist` *new-generation* suppression covering both natural rolling and the documented forced `FerventResearcher72` pathway, preserving all old-pawn Defs and treatments. Its era-appropriate background eligibility and exact 1.6 XML/patch mechanism must be verified before code is committed. Other Backstories/Traits do not have to be exhausted first.
3. Before **each** new production change, satisfy the existing VE/non-VE reuse/need decision for *that function* and confirm that it belongs to Culture rather than another owner; do not convert this into an open-ended audit of all external mods.
4. When code exists, verify **only its own changed gameplay** and the immediately exposed regressions. The dedicated `AteWithoutTable` E2E campaign, retired floor-seat experiment, unnecessary CI, and broad statistical character-generation surveys remain deferred. The no-table prototype is unaffected by these decisions.

**Status:** The separation of alcohol disposition/tolerance/dependence, preservation of emergency drug play, non-destructive migration, and exclusion of anachronistic *new* Transhumanist generation are accepted design direction. Per-substance replacements, doses, craft trees, spawn modifiers, `DrugDesire` replacement effects, final Backstory category patching and all runtime outcomes remain **unapproved/unimplemented**.

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

`Patches/TranshumanistGeneration.xml` implements three narrow operations:

1. Set `TraitDef[Transhumanist]/commonality=0` for normal random selection, retaining the TraitDef.
2. Replace `BackstoryDef[FerventResearcher72]/spawnCategories` (`Offworld`, `Pirate`) with the deliberately unused category `AMJ_CultureExcluded`. That source backstory explicitly describes technological transcendence and is not made historically suitable merely by removing its trait.
3. Remove that backstory's known `forcedTraits/li[traitDef="Transhumanist"]` as defense against direct, category-bypassing assignments. Preserve the BackstoryDef itself and all other fields for old-save references.

The sentinel category must not be added to a Pawn generation filter. This is an ordinary-category exclusion, **not** a claim that explicitly modded generators cannot override categories or inject the Trait. External Backstory packs and DLC remain outside this narrowly approved slice. Do not delete or rewrite broad Backstory libraries.

**Acceptance, once the implementation is assembled:** inspect authentic 1.6 loaded Defs for exact patch targets and category eligibility; compare commonality, backstory category and forced entry; generate one ordinary Vanilla pawn and one MO pawn without errors; confirm changed source preserves existing-pawn identities and other traits. A single consolidated native acceptance near release is sufficient; do not create a sampling matrix or run a game on every PR. Missing XPath or ERROR is failure, not a PASS. No native acceptance or 1.7 support is claimed by this source change.
