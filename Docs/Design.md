# AMJ Culture — design and first acceptance

## Development purpose, phase and priority (2026-10-16 review)

Culture's product goal is to remove historically incongruous **character generation, culturally contingent mood/social judgments, and intoxicant gameplay** from ancient-to-medieval Japanese settlement play **without breaking the useful Vanilla/MO gameplay roles**. One neutral meal Thought is a small completed slice, not the product goal. Culture combines Backstories, generic Pawn-generation interactions, Traits, relevant Needs/Thoughts and psychoactive Drug/effect/supply UX; Shared Sleeping, general furniture, medicinal remedies, brewing, and ordinary food Jobs remain other owners' responsibilities.

**Actual implementation maturity:** the narrow `AteWithoutTable` zero-mood XML prototype, bounded `Transhumanist` and 31 original adult + 12 original child background exclusions are on `main`; the final ordinary-generation exclusion scope is 67, and the original 32 portable historical background candidates have all now received authored English/Japanese histories with six starting-item substitutions (see the latest section). The [second-wave source change](SFBackgroundSecondWave.md) adds a further 12 adult and 12 child rules; its updated production DLL and native acceptance are tracked separately below. The single real Vanilla 1.6 character-generation acceptance below passed, but complete MO/1.7 coverage and saved-game migration were not tested. The no-table prototype's static XML check passed; the generation source still needs focused loaded-Def/gameplay acceptance. Historical background/trait handling, drugs and minimum enza functionality have *approved ownership/boundaries or research candidates*, but **not** complete per-feature effect values, spawn handling, recipes, migration or final implementation specifications. Do not describe Culture overall as implemented or entering integration validation.

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
