# SF / modern-technology character-background review — 2026-10-17

## Scope, limitations and correction to the earlier triage

**Purpose:** Decide whether ordinary new ancient-to-medieval Japanese pawns can receive each Vanilla background. Preserve functioning character variety and the background/trait references in existing saves. Neither a futuristic *identifier* nor a generation category alone proves a current narrative is inappropriate.

**Source A (authentic game metadata):** Project Actions [Core 1.6/1.7 source audit v2](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/VanillaCoreCharacterDefsSourceAudit20261016.md), [run #38797500765](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/38797500765). Its **unexpired original archived CSV** was retrieved on 2026-10-17, rather than re-running any native game/runner job. 827 parsed concrete definitions (784 backgrounds, 43 traits), 1.6 and 1.7 identical metadata/SHA at its fixed source revisions. These are **not** necessarily equivalent to a later patched loaded Def.

**Source B (description evidence, lower confidence):** [Ludeon's archived Finnish translation source](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml), containing English original-description comments keyed by the exact old-style `defName`. Read each of the 50 IDs below in the archived source. These English comments are **not proven SHA-identical to licensed Core 1.6/1.7 descriptions** and may have been edited since preservation. The individual content decisions below are therefore **period-fit candidates**, not an asserted completed current-1.6 primary-text review. Only sufficiently verified authentic loaded text and spawn eligibility should authorize additional gameplay exclusions.

**Material audit defect found:** The old `TECHNOLOGY_SF_REVIEW=50` field is **not** the complete SF/modern-risk queue. For example, `AIProgrammer22`, `AIResearcher94`, `GeneticEngineer89`, `Roboticist1` and `RocketEngineer53` were `MANUAL_REVIEW`, while `MechanoidHacker93` was `DRUG_REFERENCES`, even though each archived description has explicitly futuristic content. No exact count of **all** anachronistic backgrounds is currently established. Do not change the historical flag count of 50 into a claim that only 50 are problematic.

### Disposition codes (not patch instructions)

- **X — current-story exclusion candidate:** the documented narrative fundamentally depends on later or fictional technology/institutions. It should not enter a medieval-Japan ordinary pawn pool *unchanged*. A future replacement or explicit exclusion is a separate production choice after verifying owned 1.6 text.
- **R — rewrite/reuse candidate:** the old story contains anachronistic setting, but a recognizable non-SF occupation or life experience may be retained with historically approved prose and compatible skills/effects. Do not suppress it while the replacement is missing unless normal generation is proven harmful.
- **H — hold:** insufficient evidence to determine whether it even needs a rewrite.
- **DONE SOURCE:** already approved source change exists; no native acceptance claimed.

The original XML has distinct **Solid vs Shuffled** background groups and many Solid childhood entries with **no explicit spawnCategories**. Replacing a category node as for `FerventResearcher72` cannot be assumed to work for every flagged background. Natural faction eligibility, direct assignments, empty categories, forced traits and starting items need their own targeted implementation checks. Never delete a `BackstoryDef` to avoid save-breakage.

## The original 50 technology/SF flags, **individually** reviewed

| DefName | Decision | Specific narrative issue / salvageable gameplay role |
|---|---|---|
| `AerospaceEngineer44` | X | Aircraft/spacecraft manufacture; no period equivalent to this trained specialty |
| `Computer80` | R | A confined human calculator; arithmetic itself is not SF, but the old setting needs review |
| `ComputerEngineer10` | X | Office programming and frontier advertisement |
| `ComputerEngineer36` | X | Computer engineering, electronics and social reliance on computers |
| `ComputerGeek62` | X | Personal computer use and cryptosleep hacking |
| `DeepSpaceMiner3` | R | Asteroid mining is SF; mining and associated manual skills are reusable |
| `DeepSpaceSurveyor1` | X | Interplanetary surveying and colonization |
| `Engineer40` | R | World-specific technology hub; tinkering, trade and repair are reusable |
| `GlitterworldEmpath26` | R | Elite empath training on a glitterworld; mediation is reusable without that setting |
| `GlitterworldKid85` | X | Genetically engineered parentage and advanced science |
| `GlitterworldNerd20` | X | Mechanoid companion and technological obsession |
| `GlitterworldOfficer60` | R | Glitterworld military setting; maintaining order and command are period-transferable |
| `GlitterworldRoyal24` | R | Interplanetary royal marriage; dynastic marriage can be redesigned historically |
| `GlitterworldSurgeon15` | X | Advanced cosmetic surgery on a medically futuristic world |
| `HumanComputer83` | R | Mental arithmetic may fit a historical clerk, but the industrial-world dictatorship does not |
| `IndustrialOrphan13` | R | Industrial mines and workhouses; orphaned laborer could be reframed for Japan |
| `MechanoidNerd10` | X | Child of mechanoid designers pursuing machine creation |
| `OrbitalReservist22` | X | Orbital garrison, defense systems and dropships |
| `SpaceCadet77` | X | Interplanetary fleet service and starship |
| `SpaceExplorer53` | R | Isolated on another planet; remote-island survival can be reworked without space travel |
| `SpaceFanboy22` | X | Spaceship fantasy and police-box setting |
| `SpaceHumanitarian9` | R | Crowded off-world cities; charitable treatment of the needy remains usable |
| `SpaceHunter79` | R | Interplanetary hunting expeditions; hunting skill/role may remain |
| `SpaceMarine16` | R | Boarding starships; soldier/ship-boarder role could be rewritten |
| `SpaceMarine19` | R | Space marine squad; harsh-warrior identity is transferable |
| `SpaceMarine5` | R | Interplanetary marine; combat trauma is reusable with different context |
| `SpaceMarine51` | X | Off-planet corporation and space-based private warfare contracts |
| `SpaceMarine9` | R | Imperial space commandos; militant group/camaraderie transferable, not setting |
| `SpaceMarine94` | R | Starship boarding in an imperial navy; naval warrior adaptation possible |
| `SpaceMarineMedic10` | X | Colony expedition, futuristic disease and machine-gun ammunition |
| `SpaceMerchant97` | R | Starship chartering; trader/transport merchant is reusable |
| `SpaceNavyDoctor72` | R | Medical service aboard starships; medical/naval role reusable, Penoxycyline possession also needs review |
| `SpaceNavyTech37` | X | Space navy electronics, radar and imaging systems |
| `SpaceNerd97` | X | Superhero and space-travel fantasy; no adequate unchanged period framing |
| `SpacePirate55` | R | Outer-space piracy; maritime piracy/wako-style role only after period-specific rewrite |
| `SpaceRaider71` | R | Raiding across planets; bandit/sea-raider role plausible |
| `SpaceResearcher25` | X | Life-long research in a spaceship inherited from parents |
| `SpaceSmuggler6` | R | Smuggling between planets; overland/coastal contraband transport role is reusable |
| `SpaceStationCook63` | R | Space-station kitchen; cook role transferable if original restrictions stay sensible |
| `SpaceTactician28` | R | Interplanetary naval warfare; military strategist role is reusable |
| `SpaceTechnician29` | X | Space-station technical maintenance |
| `SpaceTrafficker68` | H | Old narrative stresses inherited arms trade, not explicit space travel; title and actual 1.6 story need verification |
| `SpacerOrphan77` | R | Orphaned in spaceship destruction; orphan survival can be rewritten |
| `SpaceshipChef41` | R | Ship's cook; old source also gives nutrient-paste meals as starting possessions |
| `SpaceshipSalesman34` | X | Spacecraft retail and futuristic mutant-wheat research |
| `SpaceyachtPilot89` | R | Space yacht transport; vessel pilot/elite passenger transport transferable |
| `StarshipDoctor37` | R | Starship surgeon; surgeon role needs historically grounded treatment skills |
| `StarshipJanitor33` | X | Interstellar cryptosleep checks and mechanoid maintenance |
| `VoidspaceRaider94` | R | Spaceborne raiding; violent outlaw/pursuit narrative can be grounded historically |
| `WanderingHealer52` | R | Midworld-to-rimworld physician; itinerant healer possible after removal of planet setting |

**This set:** 50 different background identifiers; X=20, R=29, H=1. These counts describe this older English narrative review, **not** the final Vanilla game-Def exclusion count.

## Important missed cases, outside the old 50

These are exact original 1.6/1.7 **metadata identifiers** paired with archived English text when present. This is a selected list of concrete false negatives, **not** a second exhaustive scan of all 784 texts. The old `MANUAL_REVIEW` flag must not be treated as an implicitly historically approved status.

| DefName | Decision | Specific issue |
|---|---|---|
| `AIProgrammer22` | X | Military combat AI developer |
| `AIResearcher94` | X | Artificial intelligence, robotics and holography |
| `AceFighterPilot54` | X | Star academy and high-tech combat aircraft |
| `ArchotechSpy75` | X | Cybernetics research, archotech and mechanoid pursuit |
| `BattleMechanic27` | X | Fusion-era technical mercenary and custom high-tech equipment |
| `BattlefieldTech52` | X | Military mechanoid manipulation |
| `CombatMedtech59` | X | Military mechs and machine construction |
| `CorpResearcher71` | X | Corporate laboratory slavery and career ladder |
| `CorporateManager76` | R | Modern open-plan office; administrative supervision is more broadly transferable |
| `CorporateSlave22` | X | Forced virtual reality and digital violence |
| `EngineeredPilot75` | X | Genetic modification to operate advanced fighter aircraft |
| `FactoryDrone58` | R | Industrial factory-city childhood; poor laborer framing could be adapted |
| `FactoryWorker58` | R | Industrial factory employment; manual/animal-assisted manufacturing transferable |
| `GeneticEngineer89` | X | Genetic experimentation and engineering |
| `GeneticScientist66` | X | Commercial human/livestock genetic enhancement |
| `HackerKid98` | X | Computer hacking as child |
| `HackerProdigy84` | X | Computer-network intrusion career |
| `MechWarVeteran77` | X | Cross-world campaigns against rogue mechanoids |
| `NetworkEngineer34` | X | Computer-network uptime and packets |
| `Roboticist1` | X | Replacement robot sailor workforce |
| `Roboticist41` | X | Robots, mechanites and artificial intelligence |
| `RocketEngineer53` | X | Rocket-engineering school and space-mining firm |
| `RocketPioneer19` | X | Self-built rocket flight across planets |
| `MechanoidHacker93` | X | Original metadata: DrugDesire=1 forced; archival text describes a mechanoid-inventor family |
| `VideoGamer16` | X | Original metadata: DrugDesire=2 forced; archival text describes video games |
| `FerventResearcher72` | DONE SOURCE | Previously identified biotech/transcendence narrative and Transhumanist=0; narrow suppression already on main, native acceptance open |

For the already-implemented `FerventResearcher72` case, see `Patches/TranshumanistGeneration.xml`; preserving its original Def for saves remains mandatory. `MechanoidHacker93` and `VideoGamer16` are **Solid_Child** records with no explicit spawnCategories in the authentic metadata. They force `DrugDesire` degrees 1 and 2, respectively; a commonality-only Trait patch would not resolve either original background narrative or the background-dependent drug disposition.

## Implemented follow-up: ordinary adult category exclusions (2026-10-17)

User-approved implementation after this source audit: [`Patches/AnachronisticAdultBackstories.xml`](../Patches/AnachronisticAdultBackstories.xml) restricts the 30 **adult** records coded X in the two tables below, using existing `spawnCategories` from licensed Core 1.6/1.7 metadata. Combined with the independently implemented `FerventResearcher72` rule, 31 adult backgrounds have an ordinary category restriction **in source** and verified as loaded category restrictions in the one-scenario Vanilla 1.6 [native acceptance](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005480093). This is an actual Mod XML change, separate from the original research candidate dispositions. All other candidates remain unchanged.

Twelve child X records still have **no category node in Core source**; instead their preauthored Solid biography pathway is now guarded by [`SolidChildBioGatePatch.cs`](../Source/AMJ.Culture/SolidChildBioGatePatch.cs), compiled into the new [`AMJ.Culture.dll`](../Assemblies/AMJ.Culture.dll). The Harmony postfix rejects those childhoods at `PawnBioAndNameGenerator.TryGetRandomUnusedSolidBioFor` and uses Vanilla's shuffled fallback. This is source-built and **representative Vanilla 1.6 behavior is loaded-game verified**: Harmony registration, one banned/allowed Solid Bio, and one ordinary pawn (not all twelve random draws, MO/1.7, or existing save loading). Saved BackstoryDefs remain intact.

**Do not confuse:** the source-confirmed list of present adult category fields is solid evidence about **patch targets**, while the English original comments in translations remain supporting narrative evidence, not SHA-verified original 1.6/1.7 prose. Other optional mods can overwrite categories or directly assign backgrounds.

## Practical decisions for further production

1. **Reject the assumption that 50 flagged items are the complete scope**; stop using `TECHNOLOGY_SF_REVIEW` as a final inclusion/exclusion gate. The first definite safety target remains the already-approved `FerventResearcher72` rule, separate from this new research.
2. **Confirm only genuinely actionable original 1.6 descriptions before making additional exclusion patches.** Focus first on high-impact explicit modern/SF cases that can actually appear in normal Vanilla/MO generation: `AIProgrammer22`, `AIResearcher94`, `AerospaceEngineer44`, `VideoGamer16`, `MechanoidHacker93`, `GeneticEngineer89`, `Roboticist1`, etc. Use existing source records; no expensive game launch or open-ended rerun of the broad audit is justified.
3. **Do not turn portable professions into unnecessary character loss.** Miner, hunter, doctor, cook, merchant, sea raider and calculator roles may be retained by accurate Japanese prose and skill/trait assessment. A historical backstory rewrite requires the normal Japanese editorial approval rather than autogenerated final text.
4. **Treat source restrictions as distinct from save migration:** old Pawns, background defName, skill/work/forced-Trait semantics and independent psychological/drug rescue gameplay stay intact unless separately designed.
5. **Do not add an exclusion patch mechanically for all 50.** Authentic Core content, `spawnCategories` existence, fallback categories and actual generation code need to be established for the specific implemented candidates. No new native test, CI, fan-out statistics, third-party compatibility expansion or Steam publication is justified for this content review.

**Second-wave follow-up:** [24 additional high-confidence science-fiction childhood/adult biography exclusions](SFBackgroundSecondWave.md) now have exact IDs and source changes. These were missed by the initial 50-flag candidate cohort and include the prior native test's `FallenProdigy40` supposedly permitted comparison. No old native test establishes suitability of all backgrounds or acceptance of the new list.

**Completion of the original reusable-background cohort (2026-10-17):** All 32 entries classified `R` in this audit (29 from the original 50, 3 from the supplemental 26) now have actual English `BackstoryDef` story/title XML overrides and native Japanese DefInjected descriptions in [HistoricalOccupationBackstories.xml](../Patches/HistoricalOccupationBackstories.xml). The last 17 include six new-generation starting-item substitutions, documented in [Design.md](Design.md#転用可能なsf背景32件の改稿完成--最終17件2026-10-17) and implemented by [HistoricalOccupationStartingPossessions.xml](../Patches/HistoricalOccupationStartingPossessions.xml). The user-visible professions were rewritten; no 32 new background Defs were created and none of these 32 is suppressed by the 67-item ineligible-generation gate. This is **source-implemented, not loaded-game language/item acceptance**, not a full 784-description completeness claim.

**Status:** Completed **50/50 archived-description individual candidate review** plus **26 important outside-50 cases**, with provenance clearly scoped. Authentic 1.6/1.7 description equivalence, full 784-text historical adjudication, broader gameplay suppression and complete MO/1.7/save-load acceptance remain open. The 31 adult loaded categories and representative 12-child guard were accepted for Vanilla 1.6 in Project #39005480093. This report is not a release gate PASS or approval of new patch code.
