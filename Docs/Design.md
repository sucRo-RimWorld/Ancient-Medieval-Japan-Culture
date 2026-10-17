# AMJ Culture — design and first acceptance

## Development purpose, phase and priority (2026-10-16 review)

Culture's product goal is to remove historically incongruous **character generation, culturally contingent mood/social judgments, and intoxicant gameplay** from ancient-to-medieval Japanese settlement play **without breaking the useful Vanilla/MO gameplay roles**. One neutral meal Thought is a small completed slice, not the product goal. Culture combines Backstories, generic Pawn-generation interactions, Traits, relevant Needs/Thoughts and psychoactive Drug/effect/supply UX; Shared Sleeping, general furniture, medicinal remedies, brewing, and ordinary food Jobs remain other owners' responsibilities.

**Actual implementation maturity:** the narrow `AteWithoutTable` zero-mood XML prototype and the bounded `Transhumanist` new-generation source patch are on `main`, but neither constitutes loaded-game acceptance. The no-table prototype's static XML check passed; the generation source still needs focused loaded-Def/gameplay acceptance. Historical background/trait handling, drugs and minimum enza functionality have *approved ownership/boundaries or research candidates*, but **not** complete per-feature effect values, spawn handling, recipes, migration or final implementation specifications. Do not describe Culture overall as implemented or entering integration validation.

**Next Culture specification work:** prioritize the user's high-execution-impact historical drug/intoxicant UX together with related `DrugDesire`/Need/Trait rules. Preserve the emergency mood-rescue role, addiction/withdrawal consistency and existing-save references. In the same bounded character-generation design effort, resolve the evidenced `Transhumanist` natural and forced-Backstory paths in Vanilla/MO, without indiscriminate TraitDef deletion or re-auditing all third-party backgrounds. Work from the existing Project [drug research](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/PsychoactiveSubstancesCandidate.md) and [character research](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md); make each accepted final decision here before adding its source. Enza material/Comfort/job adapter and broader ethics/other backgrounds remain separate later decisions without fixed next-task precedence. This local sequencing does **not** override the Project-wide P0 Environment/Grains repair and closeout work. The approved drug UX review has elevated execution priority ahead of new Ironmaking expansion; general Culture reconstruction classification remains P2.

**Validation timing:** keep the acceptance contract below as a *future promotion gate*, not the current development task. Defer the dedicated Culture 1.6 Vanilla/MO E2E harness, 1.7 compatibility checks, CI construction and broad regression matrix until a coherent implemented behavior requires those checks or a concrete unknown blocks its design. A single narrowly scoped loaded-Def check is permissible if needed to resolve a specific implementation ambiguity. Do not repeat retired stool demonstrations, grow tests to justify the runner infrastructure, or promote source parsing to runtime PASS. Do not publish the unaccepted prototype.

## Historical background and trait audit (2026-10-17)

The owner-side [BackgroundTraitAudit.md](BackgroundTraitAudit.md) records the source-grounded Vanilla/MO character and optional medieval-pack historical-fit classifications.

The follow-up [SFBackgroundIndividualAudit.md](SFBackgroundIndividualAudit.md) assesses **all 50 originally SF/technology-flagged backgrounds individually** using an archived official translation's English comments and the authentic 1.6/1.7 metadata; it also identifies **26 additional cases missed by that flag**. The preserved English comments are not confirmed to be current Core 1.6/1.7 primary text, so its historical assessments are candidates, not new gameplay-exclusion approvals. No wholesale 50-background suppression is authorized. The 43-name trait crosswalk is provisional where modern 1.6/1.7 Core text has not been independently compared; Vanilla's 784 backgrounds were enumerated but **not** exhaustively historically approved. A review flag alone is not approval to disable that background. Preserve the approved narrow Transhumanist outcome already implemented in source; any other new background exclusions, story rewrites or altered Trait/Thought effects require their own authentic-content decision and then the smallest useful source change. This audit is not a new test mandate or game behavior change.

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

**Evidence / remaining gate:** A one-pawn experimental chair-only ingestion
route completed with Comfort gain and food consumption in actual 1.7 source
run #38899711992 (Project research). PR #6's different enza-only 1.6
implementation did not pass its first loaded-game scenario and is not an
accepted candidate for this general seating requirement. The generalized
source in `Source/AMJ.Culture/TablelessChairMealPatch.cs` is compiled against
isolated original RimWorld 1.6 Managed/Harmony and its matching DLL is staged
under `Assemblies/AMJ.Culture.dll` (source commit `aab06d25fd35c0eecdc3c818e06c90bf6a43fbca`,
Project build-only [run #38995487716](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/38995487716),
SHA-256 `609f8d2b2d4f9bc48f6ee86474f2b0e15c5d7abb2f342bd855dac526250a4dbb`). **Actual scoped 1.6 game acceptance:** After fixing an unrelated invalid
Backstory XPath via Culture PR #15, the matching production DLL completed
[Project native run #39002811535](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39002811535) on original 1.6.4871 rev598 (Culture source
`2bcfdc9343b3ea5c94c216893a74cdf2a7d40f30`, DLL SHA above). Actual
Vanilla `Stool` without any adjacent table was chosen during normal Ingest;
pawn Comfort increased and the meal was consumed. Pickle 1/1 PASS,
`native_errors=0`, `raw_log_error_entries=0`, exit 0, original game unchanged.
The earlier initial [run #38995788200](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/38995788200)
passed the meal but was overall FAIL on the now-removed unrelated XPath.
**Do not overgeneralize:** valid table-adjacent chair precedence, no-chair
fallback, other furniture/MO, dining-room bonuses and 1.7 compatibility
were not exercised by this one-scenario acceptance. The old 1.7 temporary
proof is not proof of current Culture 1.7 support. Verify remaining items
only when a relevant functional change, reported conflict or release gate
requires them; no repeated game launches solely to increase test coverage. No new research harness, repeated game startup or
furniture texture work before usable implementation. Any conflict with other
Ingest patches needs an actual identified mod/behavior, not speculative
open-ended audits.

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
