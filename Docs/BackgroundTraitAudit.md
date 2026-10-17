# Culture background / trait historical-fit audit — 2026-10-17

## Player-facing purpose and evidence standard

Identify why a pawn's history, trait or attached gameplay effect is inappropriate for **ancient through late-medieval/Sengoku Japan**, and resolve it without silently eliminating useful Vanilla/MO roles, pawn diversity or existing save references. Audit the **background description and historical institution, forced traits, items, disabled work, spawn categories and final generation pathway** together; do not delete a background merely because its identifier contains `Space`, or accept it merely because it says `Medieval`.

**Evidence levels:** `CONFIRMED SOURCE` = exact original or previously recorded source metadata; `CONTENT JUDGMENT` = period-fit review tied to identified content; `REVIEW` = plausible conflict but not enough primary-text evidence to implement; `IMPLEMENTED SOURCE / NOT GAME VERIFIED` = owner file exists, no claim of loaded behavior. A flagged record is **not** an exclusion decision.

Primary data:
- [Vanilla Core 1.6/1.7 original metadata audit](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/VanillaCoreCharacterDefsSourceAudit20261016.md): 784 `BackstoryDef`, 43 `TraitDef`, 18 original XML files equal by SHA across the two versions; 119 backgrounds with forced traits (125 assignments), 90 with possessions, three with explicit disallowed traits. Original text was **not** copied into GitHub.
- [Culture pre-split research and effects](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md) and [Vanilla/MO generation audit](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/VanillaMOCharacterGenerationAudit.md).
- Author-provided `3219596926.zip`: MO 1.6.2.2 XML directly re-inspected on 2026-10-17. Exactly two MO-specific trait definitions; MO adds no `BackstoryDef` in the audited 1.6 files.
- [Historical/Medieval Workshop original-source audit](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/WorkshopBackstoryOriginalsAudit20261016.md), plus the fixed [Anthology 162-record triage](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/Data/BackstoryAnthology16-Triage.csv) and [Medieval Backstories Patch 52-record triage](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/Data/MedievalBackstoriesPatch16-Triage.csv).
- Historical [public Core trait XML mirror](https://github.com/RimWorld-zh/RimWorld-Core/tree/master/Core/Defs/TraitDefs) describes 39 trait identifiers; [RimWorld Wiki traits](https://rimworldwiki.com/wiki/Traits) gives current background/effect context including `Sickly`, `Asexual`, `Bisexual`, `Recluse`. Neither is certified SHA-identical to owned 1.6/1.7 Core. The 43-name grouping below is a **provisional name-level crosswalk**, not a newly claimed direct 43/43 owned-Core text comparison.

No new native tests, CI, package installs, pawn sampling, or generated Mod patches were needed for this historical-content audit.

## Trait assessment — 43-name working index

Count and classifications are deliberately exclusive: **1 excluded for new generation + 4 effect-review + 38 provisionally retained = 43**. This is **not** a claim that every TraitDef in owned 1.6/1.7 was independently text-reviewed today. All generic traits retain their existing defName and save references.

| Action | TraitDef | Japanese historical/gameplay reason |
|---|---|---|
| NEW-GENERATION EXCLUSION | `Transhumanist` | The desire to mechanically transcend the human body is out-of-period; specifically affects artificial-body mood/opinion. Existing pawns retain trait/Thought references. |
| EFFECT REVIEW, NOT REMOVAL | `BodyPurist` | Physical aversion to implants may be humanly plausible; existing behavior also treats low-tech prostheses such as a peg leg as artificial. Review historical meaning and penalty, not remove automatically. |
| EFFECT REVIEW, NOT REMOVAL | `DrugDesire` (degrees 2/1/-1) | The three chemical-fascination/interest/abstention behaviors must be reconciled with historically appropriate usable intoxication and last-resort mood rescue; they are not interchangeable with sake preference or tolerance. |
| EFFECT REVIEW, NOT REMOVAL | `Greedy` | Ambition/possessiveness is timeless; mandatory impressive **private bedroom** and associated mood expectation may not suit collective historical living patterns. |
| EFFECT REVIEW, NOT REMOVAL | `Jealous` | Envy is timeless; comparing personally owned bedrooms needs cultural review independent of the underlying character trait. |
| PROVISIONAL KEEP | `Ascetic`, `Nudist`, `Undergrounder` | Lifestyle/environmental preferences are not intrinsically SF; analyze cultural Thoughts separately if a concrete problem appears. |
| PROVISIONAL KEEP | `Bloodlust`, `Psychopath`, `Cannibal`, `Masochist`, `Pyromaniac`, `Brawler`, `Wimp`, `Tough` | Temperament, conduct, combat and pain response can exist in any era. Keeping a trait is not an endorsement of behavior or its historical prevalence. |
| PROVISIONAL KEEP | `Kind`, `Abrasive`, `DislikesMen`, `DislikesWomen`, `Gay`, `Asexual`, `Bisexual`, `AnnoyingVoice`, `CreepyBreathing` | Personality, interpersonal attitudes, orientation and voice/physiological properties are not tied to modern technology. Do not turn a setting audit into erasure of people. |
| PROVISIONAL KEEP | `TooSmart`, `FastLearner`, `GreatMemory`, `Nimble`, `QuickSleeper`, `NightOwl`, `Gourmand`, `Recluse`, `Sickly` | General learning, physiology and life habits. Review a specific generated story or starting possession separately if needed. |
| PROVISIONAL KEEP | `SpeedOffset`, `NaturalMood`, `Nerves`, `Neurotic`, `Industriousness`, `ShootingAccuracy`, `Beauty`, `Immunity` | Fundamental movement, mood, work, skill, appearance and health variation. `ShootingAccuracy` covers accuracy as a game mechanic, not compulsory firearms. |
| PROVISIONAL KEEP | `PsychicSensitivity` | Preserves RimWorld's psychic-fiction gameplay; it is not a trait of modern industrial civilization. This is not an endorsement of any particular DLC religion system. |

The 38 provisionally retained names, exhaustively enumerated for cross-check:
`Nudist`, `Bloodlust`, `Kind`, `Psychopath`, `Cannibal`, `Abrasive`, `TooSmart`, `Brawler`, `Masochist`, `NightOwl`, `Ascetic`, `Gay`, `AnnoyingVoice`, `CreepyBreathing`, `Pyromaniac`, `Wimp`, `Nimble`, `FastLearner`, `Undergrounder`, `DislikesMen`, `DislikesWomen`, `GreatMemory`, `Tough`, `Gourmand`, `QuickSleeper`, `SpeedOffset`, `NaturalMood`, `Nerves`, `Neurotic`, `Industriousness`, `PsychicSensitivity`, `ShootingAccuracy`, `Beauty`, `Immunity`, `Sickly`, `Asexual`, `Bisexual`, `Recluse`.

**MO 1.6 directly verified original XML**:
- `DankPyon_Fighter` — commonality `0.2`, weapon combat/accuracy/dodge/damage and pain/mood-related stats.
- `DankPyon_Veteran` — commonality `0.1`, stronger combat, movement and tending modifiers as well as pain/mood stats.
- Both **provisionally retained**; fighters and battle veterans are historically plausible roles. The separate `1.6/Patches/Core/Add_Trait_Thoughts.xml` disables several death/corpse/banishment-related Thoughts and attempts two overlapping `nullifyingTraits` edits. Review those particular social penalties if Culture later changes them; **do not** silently nerf or duplicate MO patches now. `MayRequire` Ideology factors are outside the no-DLC supported test profile.

## Vanilla background audit — decisions and targeted candidates

### Historical source filters are not final character generation

The full 784-background Core registry has a **prioritization** breakdown of `BODY_MODIFICATION=6`, `DRUG_REFERENCES=7`, `TECHNOLOGY_SF_REVIEW=50`, `RELIGION_INSTITUTION_REVIEW=12`, `MANUAL_REVIEW=752`. Overlap is possible. These counts came from lexical/metadata flags, not verified historical judgments. A prior naive `king` substring in `Cooking` was corrected; never treat automatic labels as direct approval or deletion authority.

| BackstoryDef | Original metadata and cultural risk | Assessment / production implication |
|---|---|---|
| `FerventResearcher72` | Adult, `Offworld;Pirate`, forces `Transhumanist=0`; retained English narrative about transcending humanity, committees and advanced technology | **Exclude from ordinary period generation** rather than merely delete its forced trait; owner main already has a narrowly targeted XML exclusion, **not runtime accepted**. Preserve Def for save references. |
| `MechanoidHacker93` | Forces `DrugDesire=1`; explicitly identifies mechanoid-era specialization | **High-priority exclusion candidate**; inspect final description, categories and pawn references before patching. |
| `VideoGamer16` | Forces `DrugDesire=2`; refers to modern electronic gaming | **High-priority exclusion candidate**; confirm authentic description and eligibility before patching. |
| `TestSubject90` | Forces `BodyPurist=0`, `Offworld;Outlander;Pirate;Cult` | **Review:** experiments themselves existed historically; whether this is SF experimentation requires the full narrative, not its ID alone. |
| `HunterOfTheKing53` | Forces `BodyPurist=0`; `Offworld` source category | **Review** monarchy/occupation and forced-trait rationale; a king/hunter reference is not intrinsically foreign to Japanese antiquity. |
| `DischargedSoldier53` | Forces `DrugDesire=2` and `TooSmart=0` | **Review** service/discharge institution and forced drug desire separately; do not automatically eliminate former soldiers. |
| `SpaceBartender0` | Original possessions include `PsychiteTea=3` | **Review** narration AND incompatible starting substance; SF identifier alone does not prove final displayed story is SF after an installed rewrite. |

**Existing Culture source** `Patches/TranshumanistGeneration.xml`: `commonality=0`; `FerventResearcher72` ordinary generation categories replaced with unused `AMJ_CultureExcluded`; a narrow forced `Transhumanist` entry removal remains as defense against direct assignment. Other generators and third-party background overrides are not covered; no new changes to the XML are authorized by this document. The prior runtime XPath failure and correction must not be treated as blanket loaded-game PASS.

**Individual SF-candidate continuation (2026-10-17):** [SFBackgroundIndividualAudit.md](SFBackgroundIndividualAudit.md) checks all **50** original technology-flagged IDs against previously preserved English narrative comments and original 1.6/1.7 metadata, and identifies **26** further concrete false-negative/other-flag examples including AI programming, genetic engineering, robotics, and `VideoGamer16`. The 50 flag is **not exhaustive** and the archived English comments are **not yet proven identical to authentic 1.6/1.7 description text**. These classifications are proposed dispositions, not further gameplay changes.

**Not yet audited to a final Japanese historical verdict:** all 784 authentic long background narratives, every non-obvious SF entry outside the old 50, every job/work-disable mismatch, category inheritance and special pawn-generation paths. Do not say the complete backgrounds have been culturally approved or disabled. Review **authentic original text** only for candidate decisions; do not re-fetch all 784 merely to raise audit counts.

## Non-Vanilla / medieval existing background content

These are **optional source cohorts**, not a single installed pool, not Culture dependencies and not 214 mutually generated pawn types.

| Source | Reviewed source | Observed group breakdown | AMJ implication |
|---|---|---|---|
| Backstories Anthology 1.6 snapshot of Medieval + Tribal | 162 backgrounds (Medieval 107, Tribal 55), original authored XML at pinned GitHub revision | European-context 61; folklore/fantasy 15; generic-role 40; individual 45; concrete drug-content conflict 1 | Western medieval labels do not make them Japanese. No default bulk import, conversion or forced-trait wipe. |
| Medieval Backstories Patch 1.5/1.6 GTF content | 52 backgrounds (including 11 from a Royalty-conditional folder) | era/institution 11; European-context 11; general role 16; individual 14 | Inspect original narrative, active DLC and filters before optional compatibility. The `Space` part of a repurposed defName is not proof of SF in-game text. |
| Historical Backstories | 500 original BackstoryDef, 23 original TraitDef nodes (not 23 necessarily live usable traits) | Original backgrounds expose 8 broad categories and no explicit forcedTraits in original XML | Large optional pack; neither its apparent period theme nor zero original forcedTraits guarantees Japan compatibility. |
| Shavius's Medieval Flavour Pack | 27 actual 1.6 BackstoryDefs, five with explicit forcedTraits; none of the 27 explicitly forces `Transhumanist` | European knights, lords, convent and religious context | Optional period/culture inspection, no blanket adoption or requirement. |
| Medieval Backstories — Core & Royalty Overhaul | 0 new BackstoryDef/TraitDef; changes translated DefInjected and WTL classification; English file has 746 matching Vanilla IDs and 38 nonmatching IDs | Rewriting baseline SF prose may help, but not proven Japan-specific and still leaves TraitDef behavior | **Prior art only**, not proof that installing it automatically solves Culture's generation mismatch; do not require it. |

### Concrete optional-mod review cases

| Example | Source facts | Preliminary Japanese-context decision |
|---|---|---|
| `UB_Tribal_VisionGuide` | `DrugDesire=1` forced; `PsychiteTea=1` starting item | **Content conflict candidate:** revise or exclude only if optional compatibility is adopted and historically suitable consumable roles are implemented. |
| `UB_Medieval_BelovedKing`, `UB_Medieval_BelovedQueen` | European-institution review; forced `Kind`, manual/animal/plant work restrictions | **Cultural rewrite candidate**, not automatic generic ruler or Japanese emperor. Check jurisdiction, household and period. |
| `UB_Medieval_CharitableNun`, `UB_Medieval_ChivalrousKnight`, `UB_Medieval_CorruptSquire` | Latin-European religious or knightly institutions | **Cultural rewrite/replacement candidates** only on close narrative review; medieval Europe is not medieval Japan. |
| `UB_Medieval_Carpenter`, `UB_Medieval_CropFarmer` | Generic trades present in local period | **Usable concept candidates**; original descriptions and work restrictions still need review. |
| `GTF_PrivateDetective66`, `GTF_Mathematician6`, `GTF_Taxonomist0` | Modern/professional institution or university/scholarly context in original description | **Chronology/context review**; distinguish historically applicable arithmetic/scholarship from historically nonexistent offices. |
| `GTF_SpacePirate55`, `GTF_SpaceRaider71`, `GTF_SpaceSmuggler6` | Repurposed `Space*` identifiers with non-SF medieval display/story per source audit | **Do not reject on ID alone**; classify actual period narrative and available category. |
| `GTF_Missionary99`, `GTF_MedievalKnight26`, `GTF_Artilleryman28` | Religion, Western chivalry, artillery context | **Region-and-period-specific review**; the late-Sengoku period and earlier periods cannot share an indiscriminate rule. |
| `SH_MED_ReligiousKnight`, `SH_MED_LandlessKnight`, `VA_MED_ConventChild` | European knightly/religious subject matter | **Optional-content cultural rewrite or defer**, without changing Vanilla/MO source. |

### Spawn categories, forced traits and unintended consequences

- **Vanilla source:** 784 backstories, of which 119 explicitly force traits (125 assignments); 90 have starting possessions; 3 explicitly prohibit traits. For any proposed exclusion, identify whether it can actually be generated by the intended MO faction/pawn kinds before adding more XML.
- **MO original:** two trait defs, **zero new BackstoryDefs**. Faction backstory filters contain `Tribal` and `Pirate` in their original XML; some patched medieval filters do not align with their PawnKind categories. This is a compatibility question, **not** proof all flagged backgrounds appear naturally.
- **Optional Anthology:** 62 source backstories force traits, 59 disable work, one specifies initial possessions. A superficial title rewrite can silently alter skills, Pawn generation or addiction behavior.
- Preserve unchanged existing saved pawns' backstories and traits, the `BodyPurist`/prosthetics relationship, independently useful mental-health/drug emergency responses and default life-stage diversity.
- Do not force a general global `BackstoryDef` deletion, replace all mandatory Traits, make a new DLC dependency or build new CI/native test matrices just because an audit cohort is large.

## Priority and disposition (audited recommendation, not new gameplay authorization)

1. **Already approved and implemented as source:** `Transhumanist` new-generation suppression plus `FerventResearcher72` ordinary-category exclusion. A scoped Vanilla 1.6 native acceptance now PASS; separate MO-specific runtime acceptance is not established.
2. **Next high-confidence period conflicts for targeted original-text decision:** `MechanoidHacker93`, `VideoGamer16`; inspect authentic source, item effects, spawn eligibility and any direct assignments, then decide whether their story itself is excluded or rewritten. Do not substitute mere Trait deletion.
3. **Shared content/trait interactions:** `TestSubject90`, `HunterOfTheKing53`, `DischargedSoldier53`, `SpaceBartender0`, with `BodyPurist` / `DrugDesire` effects separated from the background narrative and from the unimplemented drug replacement design.
4. **Western-medium/fantasy optional packs:** use pinned 162/52 triage and original 27/500 metadata only if an actual supported install/compatibility use case requires it. Never build broad optional Mod dependencies for audit completeness.
5. **Remaining general traits and background long text:** keep the good default variety and return to them **only for a documented gameplay/cultural conflict**. `TECHNOLOGY_SF_REVIEW=50` is an underinclusive review queue, not 50 approved exclusions (see [individual audit](SFBackgroundIndividualAudit.md)).

**Production follow-up (2026-10-17):** [30 adult SF background category restrictions](../Patches/AnachronisticAdultBackstories.xml) are now implemented as separate source after the [individual audit](SFBackgroundIndividualAudit.md); the old 50-flag scope is not exhaustive. The remaining 12 category-less Solid child X backgrounds are now addressed through [`SolidChildBioGatePatch.cs`](../Source/AMJ.Culture/SolidChildBioGatePatch.cs) compiled in the new 1.6 [DLL](../Assemblies/AMJ.Culture.dll): block the fixed biography selection, not a nonexistent childhood category. Build-only PASS followed by a focused actual Vanilla 1.6 [native PASS](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005480093): all 31 adult loaded categories, all 12 child IDs retained, one blocked and one allowed named bio, one ordinary generated pawn, ERROR=0. Not an exhaustive childhood sample, MO/1.7 check or old-save load. The detailed production decision belongs to [Design.md](Design.md).

**Second-wave correction (2026-10-17):** The previously accepted one allowed Solid-Bio representative was `FallenProdigy40`, whose preserved English historical description actually depicts a glitterworld academy. The previous PASS established *blacklist preservation*, not the historical fitness of all 784 backgrounds. The [24-addition historical-fit decision and actual source change](SFBackgroundSecondWave.md) expands ordinary category exclusions to 43 adult backgrounds and the selected Solid childhood blacklist to 24, for 67 total. Old Vanilla 1.6 native evidence remains valid only for its old 43-entry behavior; the additional 24 are **not** game-tested yet.

**Implemented reusable-historical-occupation slice (2026-10-17):** Fifteen previously preserved, not generation-blocked Vanilla BackstoryDefs now have complete historically grounded English source titles/descriptions and native Japanese `DefInjected` text. The first five are `SpaceMerchant97`, `SpaceSmuggler6`, `SpacePirate55`, `SpaceHunter79`, `SpaceStationCook63`; the further ten are `Computer80`, `IndustrialOrphan13`, `SpaceHumanitarian9`, `SpaceMarine16`, `SpaceRaider71`, `SpaceTactician28`, `SpacerOrphan77`, `FactoryDrone58`, `FactoryWorker58`, `SpaceyachtPilot89`. Owner [Design](Design.md#中世日本へ転用可能なvanilla人物背景の改稿2026-10-17) records the exact matched skill/work/category effects and no-Def-deletion rule. Their original IDs retain no SF reference in the new bilingual copy. **Static sources present; actual language-loaded 1.6 and MO behavior not tested.** Do not count these fifteen as added exclusions, and do not claim all 784 backgrounds audited.

**32/32 reusable original-background conversions (2026-10-17):** The remaining 17 specific `R` candidates are now also authored in English and Japanese (source [HistoricalOccupationBackstories.xml](../Patches/HistoricalOccupationBackstories.xml) and [Japanese language overrides](../Languages/Japanese/DefInjected/BackstoryDef/HistoricalOccupationBackstories.xml)). The six biographies originally granting `BlocksVacstone`, `ComponentIndustrial`, `Penoxycyline`, `MealNutrientPaste`, `MedicineUltratech`, or `MedicineIndustrial` have controlled new-Pawn-only starting grants using generic metal, herbal medicine, or simple meals, without deleting old ThingDefs or saved inventory ([item patch](../Patches/HistoricalOccupationStartingPossessions.xml)). Original metadata and original `PossessionThingDefCountClass` custom key-as-element deserializer inform these edits. All 32 originally proposed `R` narratives now have owner XML changes, not a claim that English/Japanese load precedence, 1.6/1.7 behavior, MO or saved-pawn loads passed.

**Audit output vs implementation:** This file records candidate decisions and already approved constraints. It does not create extra feature approval, does not prove all 784 narratives are culturally fit, does not assert MO 1.7 compatibility and cannot be used as a runtime-test PASS. The owner `Docs/Design.md` and actual patch code remain canonical for accepted behavior.
