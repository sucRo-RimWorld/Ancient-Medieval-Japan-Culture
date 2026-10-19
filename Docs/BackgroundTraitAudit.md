# Culture background / trait historical-fit audit — 2026-10-17

**Stage 1 definition inventory completed (2026-10-17):** [authoritative owner status, exact screen categories and evidence limits](Stage1VanillaMOBackgroundTraitInventory.md), with a [row for each of the 784+43+2 definitions](Data/VanillaMOBackgroundTraitStage1Screen.csv). All flagged changes remain Stage 2 decisions requiring affected original Core 1.6/1.7 text and generation-route checks. The 18 custom Solid_Rare definitions are not automatically exempted as unique-only without route proof. This is not a claim of individual completed historical adjudication or current source text verification.

## Player-facing purpose and evidence standard

**Author-approved work order (2026-10-17):** Complete the review of ordinarily obtainable Vanilla **and** Medieval Overhaul backgrounds/traits and correct identified conflicts in their existing definitions, generation routes or related effects **before** implementing any *new* Japanese-period-specific background or trait. Japanese historical rewrites of existing Vanilla stories are corrective work in this first phase, not approval to add new Defs. Restrict further audits to reachable ordinary generation (unique-pawn-only assignments remain out of scope), preserve previously fixed/accepted source, and defer proposed Sengoku battle-experience/corpse-familiarity careers or traits until this phase is complete. Do not preemptively change corpse/grief/execution Thoughts or duplicate MO's existing traits.

**Scope decision (2026-10-17):** Backgrounds and traits demonstrably limited to a specific unique/predefined pawn, with no ordinary Vanilla/MO generation path, are **out of scope** for this audit and its corrective patches. Exempt only after checking generation reachability. A `Solid` background or fixed named biography is not sufficient evidence of unique-only use: named Solid biographies may also be selected during ordinary pawn generation. Retain reachable Solid backgrounds and their forced traits in scope; leave uncertain cases unmodified until their route is established. Preserve unique pawns, existing saves, all Def identities, and previously implemented rules unless an actual unique-only misclassification is proved.

**2026-10-17 Shuffled-only implementation:** Core 1.6/1.7 original metadata confirms ordinary Shuffled and category eligibility for `JoywireArtist8`, `VRDesigner87` (adult), `OrganFarm67`, `VatgrownSoldier8` (child). Their SF material is documented in archived official Ludeon English notes, but modern licensed-Core narrative identity is not established. Owner XML now adds 2 adults and 2 Shuffled children without changing forced traits, previously approved 34 rewrites, fixed Solid childhoods or production DLLs. New total **adult48 + Solid child24 + Shuffled child2 = 74**. Actual loaded-game acceptance outstanding. Other Solid candidates stay pending until ordinary-generation reachability is established; do not treat `Solid` as proof of unique-only or normal eligibility.

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

**Targeted forced-Trait + sleeping thought implementation (2026-10-17):** Exact original Core metadata confirms `TestSubject90` and `HunterOfTheKing53` force `BodyPurist=0` in SF histories; both, plus `SpaceBartender0` (space station, `PsychiteTea ×3`), now have ordinary adult categories restricted without deleting any Trait/ThingDef or saved history. `DischargedSoldier53` retains its `DrugDesire=2;TooSmart=0` forced Traits and the actor's intoxication interest but is rewritten as expelled period infantry; `ApprenticeOracle83` retains existing child slot/Cult category and skills with shrine-ritual childhood in place of cryosleep. Adult no-generation total 46, children 24, total 70; complete rewrite total 34. For `Greedy`/`Jealous` the formal [Design](Design.md) now implements special treatment of assigned Barracks under original Impressiveness thresholds (not deletion/weight edits) and English/Japanese correction of private-room wording; ordinary private bedrooms remain Vanilla. New 1.6 and 1.7 production assemblies were compiled successfully and packaged (details/hashes in [Design.md](Design.md)); **actual loaded-game behavior of the 70 restrictions, two historical rewrites and shared lodging thoughts remains outstanding**. Do not turn this into a drug/effects or Shared Sleeping rewrite.

**Audit output vs implementation:** This file records candidate decisions and already approved constraints. It does not create extra feature approval, does not prove all 784 narratives are culturally fit, does not assert MO 1.7 compatibility and cannot be used as a runtime-test PASS. The owner `Docs/Design.md` and actual patch code remain canonical for accepted behavior.

## 第2段階・第1修正群 — 既存SF背景13件の通常生成制限（2026-10-17）

**実装正本：** `Patches/AnachronisticAdultBackstories.xml`。第1段階の829定義台帳と、第1段階で取得済みの正規Vanilla Core 1.6/1.7 schema v2 CSV（Project Actions #38797500765、原ファイルのSHA一致）を再利用。追加のゲーム起動・Coreの再抽出は行っていない。

**原文の証拠範囲：** [Ludeon公式の旧英語原文コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml) にある各該当IDの `desc` 直前コメントを個別確認した。以下の内容要約はこの旧原文に基づく。**配布中の正規Core 1.6/1.7 `description` 本文と完全一致したとの証明ではない**（既取得の正規CSVはメタデータのみ）。厳密な現行原文同一性とロード後カテゴリは後の受け入れ対象。名前だけでSF判定したものではない。

| 背景ID | 旧英語原文にある具体的な時代不整合 | 既存Core 1.6/1.7の通常カテゴリ |
|---|---|---|
| `ArmyScientist35` | 惑星軍の兵器システム研究 | Offworld / Pirate |
| `CloneFarmer58` | クローン農場で生産された人間と宇宙への逃亡 | Offworld / Pirate |
| `DoomsdayPariah18` | 惑星を滅ぼす古代装置と宇宙船 | Offworld / Cult |
| `Gnomebiologist96` | グリッターワールド大学の異星生物学教授 | Offworld / Pirate |
| `LazyProgrammer3` | ソフトウェア開発、企業、知財侵害 | Offworld |
| `MilitaryEngineer45` | 軍事ドローン、メカノイドの装置の修理 | Offworld / Pirate |
| `NavyScientist52` | 星間戦争、アーキテクト遺物、宇宙海軍研究所 | Offworld / Scientist / Researcher |
| `RugbyPlayer71` | VRゲームからの切断による人格変化 | Offworld / Pirate |
| `StarfighterPilot79` | 宇宙戦闘機、宇宙海賊 | Offworld / Pirate |
| `Technician9` | IT技術者、冷凍睡眠装置、AI | Offworld / Pirate |
| `TestSubject92` | 量子物理学等の未来的実験施設 | Offworld / Cult |
| `UrbworldRebel56` | オンライン集合意識とそれへの反抗 | Offworld / Pirate |
| `WhiteHatHacker82` | コンピュータからのオンラインセキュリティ活動 | Offworld / Outlander / Pirate |

**生成経路・副作用：** 13件とも既取得の正規メタデータで `slot=Adulthood`、`spawnCategories` 存在、1.6/1.7完全一致。対象は `Solid_Adult` 11件、`Shuffled` 2件（`NavyScientist52`、`UrbworldRebel56`）。通常カテゴリ選択への制限のみで、`BackstoryDef` は維持する。 `RugbyPlayer71` の強制 `Brawler=0`、`DoomsdayPariah18` のAnomaly条件付き所持品、`NavyScientist52` のOdyssey条件付き所持品は削除・変更しない。直接割り当てられた固有Pawnと既存セーブも対象外。 `Solid_Child` のカテゴリ未設定候補をこの成人XMLへ混入させない。

**合計：** 第1段階74件（成人48＝47+FerventResearcher72、Solid幼少24、Shuffled幼少2）＋今回成人13件＝**87件**（成人61、幼少26）の新規通常生成制限。歴史改稿34件、全43 Vanilla / 2 MO TraitDef、既存MO死亡Thought調整、DLL、所持品置換6件は変更しない。

**改稿優先で保留する候補：** `Architect28`（建築）、`BountyHunter41`（追捕）、`Castaway81`（漂流）、`CharityWorker36`（救恤）、`CorporateBuilder58`（建設）、`ExpertHandyman8`（修繕）、`FurnitureBuilder83`（木工）、`LanguageAnalyst27`（語学・文書）、`MasterChef48`（調理）、`MedicSoldier54`（軍医）、`MilitaryOfficer17`（軍人）、`Minister88`（政治職）、`NinjaAssassin31`（間諜）、`Novelist7`（著述）、`PlanetaryDiplomat7`（外交）、`PrisonerOfWar2`（捕虜）、`ReligiousHierarch16`（宗教職）、`Sculptor63`（彫刻）、`StationSecurity38`（警備）などはSF原文のまま採用できないが、前近代に転用可能な役割があるため**自動一括除外しない**。本文と技能・作業禁止・強制特性が合う日本語改稿を作成し、必要な史実・編集承認後に英訳・実装する。13件も将来改稿可能であり永久削除ではない。

**次の段階内作業：** なお残る該当SF候補、通常経路が確認できる `Solid_Child` 候補、Traitの時代不整合（`BodyPurist` / `DrugDesire` 等）を原文・使用経路と対で処理する。固有Pawn専用定義は除外。新規日本独自BackstoryDef/TraitDefは第3段階まで作らない。実ゲームはここでは起動しない。

## 第2段階・第2修正群 — 一次分類で見逃されたSF背景3件の生成制限（2026-10-17）

**製品ソース：** `Patches/AnachronisticAdultBackstories.xml`。元の第1段階台帳で暫定的に時代不整合なし、または現代制度要確認とされた成人3件について、[公式アーカイブの英語原文コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)と第1段階で取得済みの正規1.6/1.7 Core schema-v2メタデータ（Project #38797500765）を個別突合した。現行ライセンスCore長文が同じであることは**未立証**であり、この証拠水準は第1修正群と同じ。

| Def | 旧英語原文の具体的不整合 | 元の成人生成カテゴリ | 既存付随条件 |
|---|---|---|---|
| `AlcoholicTrucker93` | 宇宙貨物運送会社、ナノ遺伝子治療後の肉体変容 | `Offworld` | Caring / Social / Intellectual / Artistic / Cleaning 無効 |
| `BloodgameSurvivor6` | 企業による星系規模の配信型殺人娯楽番組と武装カメラドローン | `Offworld;Pirate` | Crafting 無効 |
| `MedicalScientist16` | 異星人と星間船で行う遺伝子研究 | `Offworld;Pirate` | 研究・医療技能を維持 |

3件とも `BackstoryDefs/Solid/Solid_Adult.xml` 内の `Adulthood`、カテゴリ明示あり。取得済み正規メタデータは1.6・1.7で同一。既存Defの削除・技能/作業制限/特性改変・旧セーブ変換は行わず、通常のカテゴリ選択のみ `AMJ_CultureExcluded` へ制限する。一般的な運送人・戦闘生存者・医者の職業そのものを禁止する判断ではない。将来、妥当な日本語原文が承認された場合は、同じDefの史実改稿で再利用する余地を残す。

**合計更新：** 先行第1段階74件＋第2段階初回13件＋今回3件＝**90件**（成人64、幼少26）。改稿34件、初期所持品置換6件、MO特性と既存Thought補正は変更しない。既存XMLの成人カテゴリ制限は63件、独立 `FerventResearcher72` 1件。ロード済みカテゴリ・実ゲーム発生率・MO 1.7は未検証。新規背景/特性を追加しない。

**改稿優先で保留：** `EnergyResearcher89`（学問・実験）、`MindwipedAssassin50`（暗殺・諜報）、`MuffaloResearcher67`（畜産・医術）、`ProstituteIdol28`（性産業史・創作と表示の整理）などの職能まで機械的に消すことは避ける。現在の未来的な原文と歴史的に移せる仕事内容を区別し、日本語本文の承認手順と効果整合が揃ったものから改稿する。

## 第2段階・第3修正群 — 生成経路が確定したShuffled背景2件（2026-10-17）

**実装理由と判定：** 前回の90件に加えて、既存Vanilla 1.6/1.7 Coreの取得済みv2原XMLメタデータ（Project #38797500765）と公式の[旧英語原文コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)を対象IDだけ照合。両バージョンで原ファイル／各項目は同一。ただし現在のライセンスCore本文との一致までは確認していない。以下2件はいずれも通常カテゴリを持つ**Shuffled**であり、カテゴリ未指定Solid幼少期の推測を混入させていない。

| 元Def | 原の経路・技能等 | 時代不整合／処理 |
|---|---|---|
| `TestSubject15` | 幼少期、`BackstoryDefs/Shuffled/Offworld_Specific_Child.xml`、カテゴリ `Offworld;Pirate;Madman;Cult`、`Shooting +4`、Social / Caring / Firefighting制限、強制Trait・所持品なし | 旧原文は産業惑星の密閉施設で出生時から超兵士用に行う人体実験。古代〜中世日本の一般的児童経歴へ**原文のまま**入れない。既存 `Patches/AnachronisticShuffledChildBackstories.xml` のカテゴリ置換を追加。 |
| `IllegalShipwright76` | 成人期、`BackstoryDefs/Shuffled/Pirate_Adult.xml`、カテゴリ `Pirate`、`Construction +7`、`ManualSkilled`必須、強制Trait・所持品なし | 旧原文は違法なテラワット級エネルギー兵器を貨物宇宙船に偽装搭載する造船者。既存 `Patches/AnachronisticAdultBackstories.xml` にカテゴリ置換を追加。歴史的な**船大工・舟大工**は有用であり、将来日本語史実改稿で同じDefを再使用し得るが、当面未来兵装を通常経歴に出さない。 |

**数値・ソース:** 既存成人64＋今回1＝**65**（成人XML64＋独立 `FerventResearcher72` 1）、既存幼少26＋今回1＝**27**（Solidゲート24＋Shuffled子3）。計 **90→92件**を通常生成制限。既存34件の英日改稿、六つの初期所持品置換、MOの2Traitと死亡・捕虜Thoughtの調整、Culture DLLは変更なし。元のDef名・技能・作業制限・強制Trait・既存セーブ参照、手動割当は維持する。1.6/1.7の実ゲーム最終ロード・MO併用・保存済みPawnのロードは今回**未検証**。

**保留の歴史的転用：** `ChildStar74`（映像番組の子役→芸能の子弟）、`DeepSpaceMiner3`（宇宙鉱山→採掘職とOdyssey所持品）、`Architect28`（AI支援設計→歴史的な建築工事）などは、原の職業効果・作業タグが妥当であることを確認した上で日本語原稿を作成し、作者の歴史記述承認後に英語化する。これらをDef名やSF単語だけで一括禁止しない。カテゴリ不明のSolid幼少期・固有Pawn専用定義は、証拠なしにゲートへ追加しない。

## 第2段階・第4修正群 — 改稿承認待ちの現代専業背景2件を通常生成制限（2026-10-17）

**既存ゲームソースへの変更：** `Patches/AnachronisticAdultBackstories.xml` の成人対象カテゴリを既存64件から66件へ追加した。正規Core 1.6／1.7の**取得済み**メタデータと、Ludeon公式の[過去の英語原文コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)を対象ID単位で照合。現行ライセンスCoreの長文本文と旧コメントの完全一致は証明していない。

| Def | 検証済み通常生成経路と機械的効果 | 元のSF/現代固有本文 | Phase 2判断 |
|---|---|---|---|
| `VideoProducer83` | `Solid_Adult`、`Offworld`、`Intellectual+2/Artistic+7/Crafting+4`、ManualDumb・Social禁止、強制Traitなし | 映像制作、シットコムの放送を職業とする | 映像放送という独立した業態は中世にない。舞台芸能・絵巻・物語制作のような既存職能と元のSocial禁止や技能配分との整合が未確定のため、歴史改稿まで普通のPawnの新規選択を制限する。 |
| `CosmeticSurgeon36` | `Solid_Adult`、`Offworld;Pirate`、`Intellectual+3/Medicine+8/Artistic+6`、Cooking禁止、強制 `Abrasive=0` | 化粧整形外科の商業診療、現代美容手術 | 当時の中世日本の医師・薬師一般を禁止するものではない。医師との整合ある転用が判断できるまでは、現代的な美容整形職を通常生成しない。 |

両IDは元Coreの `Adulthood`、生成カテゴリあり。1.6/1.7メタデータの対象行は同一。定義や技能、強制Traitは削除・変更しない。既存Pawnやセーブ中の参照も保持する。今回のカテゴリ置換は歴史改稿が承認された場合に解除・再利用できる暫定的生成処理であり、完全削除ではない。

**今回の計数：** 成人 **67**＝成人XML66＋独立 `FerventResearcher72` 1、幼少27＝Solid名前ゲート24＋Shuffled 3、合計 **94件** の通常生成制限。既存34件の史実改稿は未変更。新規日本独自Def・ビルド・ネイティブゲーム起動は行わない。現在の正規Core長文、実ゲーム読み込み、MO併用を別途最終受入れで確認する。

### 第2段階・既存Traitの強制経路の結論（文章承認を除く）

再棚卸しを行わず第1段階の正規Core 1.6／1.7メタデータで **強制Traitを含む対象背景6件**を特定した。各対象行は両バージョンで一致。

| TraitDef | 元から強制する既存背景 | 技術的な現況・判断 |
|---|---|---|
| `Transhumanist` | `FerventResearcher72` の `Transhumanist=0` | 既存成人カテゴリ除外と `commonality=0` を維持。人工義体への欲求を再導入する通常生成経路を今回作らない。Def自体はセーブ互換のため保持。 |
| `BodyPurist` | `HunterOfTheKing53`、`TestSubject90` の `BodyPurist=0` | 後者は既にSF成人カテゴリ制限済み。前者は動物に対する恐怖を含む狩猟背景として史実改稿可能性があり、義体化願望とは逆の特性。共通効果・元強制付与をこの段階で機械的に削らない。本文適合は最終一括確認へ。 |
| `DrugDesire` | `DischargedSoldier53` `=2`、Solid幼少 `MechanoidHacker93` `=1`、`VideoGamer16` `=2` | 幼少2件は既存ゲート24件に含まれる。成人は既存歴史的改稿済み。ドラッグの全面停止を行わず、娯楽・緊急心情対処・依存・離脱の既存効果と強制付与を維持。嗜好品・薬物の変更が確定した時に関連仕様を照合する。 |

`BodyPurist`、`DrugDesire` の強制付与を全削除すると、セーブ済みPawn・意図された強制特性・DrugPolicyのプレイ面に副作用が出る。よってPhase 2の文章以外の技術判断は**元のTrait効果・強制経路維持**とする。元のTraitの日本語表示や歴史語義の改稿案は他の背景本文と一括確認する。既に修正した `Greedy` / `Jealous` の共同寝所効果、MOの戦士・古参兵Thought処理は変更しない。新規日本独自特性は第3段階まで追加しない。

## 第2段階・第5修正群 — 追加のSF研究・技術職5件の通常生成制限（2026-10-17）

元の保存済み**正規Vanilla Core 1.6.4871 / 1.7.4977 schema-v2メタデータ**（Project source audit #38797500765）を対象行だけ参照し、5件とも `BackstoryDefs/Solid/Solid_Adult.xml` の `Adulthood`、`spawnCategories` 明示、1.6/1.7同一であることを確認。物語の時代不整合は[Ludeonのアーカイブ英語原文コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)で個別に確認。現在の配布Core長文本文とアーカイブが文字列一致することは**未立証**であり、断定しない。

| DefName | 元カテゴリ | 旧原文で時代不整合を起こす要素 | 扱い |
|---|---|---|---|
| `NeuroScientist19` | Offworld | 全脳マッピングと改造人類の脳研究 | 元の医療・知性技能を保持し、史実的医療役への改稿まで通常抽選を止める |
| `SoftwareDeveloper84` | Offworld; Pirate | 自動化技術で教育・労働から離れるソフトウェア職 | 技術職自体の一般否定ではなく、現代の自動化経歴を除外 |
| `ExiledResearcher44` | Offworld; Pirate; ImperialCommon; Cult | ロボット研究と技術忌避の星間帝国による破門 | 史実上の追放・技術師職を今後改稿できるよう元Defを維持 |
| `EnergyResearcher89` | Offworld; Pirate | 遠距離即時通信装置研究と研究施設の大爆発 | 当時の伝令・烽火・学術は別途転用できるが元の技術説明は出さない |
| `MadScientist31` | Offworld; Pirate | 遺伝子研究所での違法な人体実験と惑星刑務所 | 元の技能・作業禁止を保存し、日本史に転用可能な医術・禁忌の話は最終原稿判断へ |

対象5件は原Core上で**強制Trait、開始所持品、禁止Traitを持たない**。既存の別々のSF背景制限と重複なし。新しいカテゴリ変換は `Patches/AnachronisticAdultBackstories.xml` に追記しただけで、各 `BackstoryDef` の削除・値変更、強制Traitや旧セーブへの変換は行わない。一般の学究・医師・技術師という職能は他の既存Defで維持する。この制限は、日本語の史実改稿が一括承認された場合に対象Defを復帰できる暫定措置である。

**新計数：成人**72**（成人生成カテゴリのXML71＋独立 `FerventResearcher72` 1）、幼少**27**（Solid幼少ゲート24＋Shuffled幼少カテゴリ3）、生成制限総数**99**。歴史的改稿済み34件、別途作者の一括確認待ち日本語改稿原稿35件はいずれも変更なし。新規日本独自BackstoryDef/TraitDefは作っていない。1.6/1.7現在の長文本文一致、ロード済み生成挙動、旧セーブ読込、MO 1.7対応は未検証。個別の実ゲーム起動・CI発火は行わない。

## 第2段階・第6修正群 — Solid幼少期8件とSolid_Rare成人期4件（2026-10-17）

**生成経路の確証と制約：** [RimWorld Wiki の人物生成・Named Bio解説](https://rimworldwiki.com/wiki/Backstories)と[開発者作成Rare人物一覧](https://rimworldwiki.com/wiki/List_of_Player-created_Pawns)を照合。Solid（名前・幼少・成人がセットの既定人物）も**通常の新規Pawn生成で抽選され得る**。Rare9組は「固有名前付き＝全員通常生成不能」ではなく抽選率が低い群。一般経路は `PawnBioAndNameGenerator.TryGetRandomUnusedSolidBioFor` にあり、選ばれる可否は成人側のカテゴリや性別・仕事条件にも依存する。**今回の12件が無条件に生成されることや、各名前付き組の個別生成回数は未立証**。このルートに到達した場合にのみ拒否する最小実装とした。元の名付きPawn・既存セーブ・原Defは削除しない。

**幼少期8件：** 既存 `Source/AMJ.Culture/SolidChildBioGatePatch.cs` の `ExcludedChildhoods`（元24件）に追加した。いずれも保存済みの本物のCore 1.6/1.7 schema v2メタデータで `BackstoryDefs/Solid/Solid_Child.xml`・`slot=Childhood`、両バージョンの属性完全一致。以下の叙述根拠は[Ludeon公式アーカイブ英語原文コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)であり、現在配布Coreの原文本文と完全一致したとは主張しない。

| 既存幼少背景 | 旧公式英語原文の時代不整合 | 保存する既存機械的効果 |
|---|---|---|
| `CloneFarmed21` | クローン子どもの人工子宮育成と仮想現実 | 元技能・作業制限 |
| `VatgrownAssassin20` | 科学者による人工培養の暗殺人間 | 元の強制 `Psychopath=0`、技能、Caring/Artistic/Cooking/PlantWorkの制限 |
| `VatgrownScientist91` | 物理・化学に特化した人工培養研究者 | 元技能・作業制限 |
| `JoywireAddict76` | 快楽回路インプラントの依存と入手行動 | 元技能・作業制限 |
| `LabGrownChild22` | 人体実験施設の試験的な実験室出生 | 元技能・作業制限 |
| `DreadedBaby56` | 宇宙船事故の幼児が核動力を発明したという設定 | 元技能、既存 `Cult` カテゴリを削除しない |
| `GameFanatic86` | VRゲームの熟練と犯罪科学者による人体実験 | 元技能・作業制限 |
| `ReclusiveProdigy96` | 遺伝子改造と神経機能拡張の児童研究 | 元の強制 `TooSmart=0`、技能・作業制限、既存 `Cult` カテゴリを削除しない |

幼少期Defには `spawnCategories` を後付けせず、通常Named Solid人物が**実際に選出された時だけ**幼少 `defName` を見て `false` を返す既存Harmony Postfixに統合した。無関係なShuffled幼少、既存の設定済み人物、手動の背景割当、昔のセーブは変更しない。特性 `Psychopath`／`TooSmart` は削除しない。

**Rare成人期4件：** Core 1.6/1.7元メタデータで `BackstoryDefs/Solid/Solid_Rare.xml` に存在し、元の`spawnCategories` を保持する現代専業の `JoeCustomAdulthood`（game developer; Offworld/Scientist）、`LiamCustomAdulthood`（simulation developer; Offworld/Scientist）、`OskarCustomAdulthood`（mod designer; Outlander）、`TynanCustomAdulthood`（game developer; Pirate）のみ、成人向けカテゴリ制限XMLへ追加した。Feyのゲーム運営、Mateの大道芸、Nathanの採掘等ほかのRare人物は名称だけで排除しない。Liamの `Industriousness=1`、Tynanの `Industriousness=-2`、元スキル・仕事制限・定義・旧セーブ参照は維持する。この4件は時代不整合を元の正式な英語タイトルでも確認できるため、アーカイブの長文欠落によって職業判断を捏造してはいない。

**正本の計数・製品の状態を区別：** 既存99件（成人72、幼少27）→**ソース変更対象111件**（成人76＝通常カテゴリ75＋独立 `FerventResearcher72` 1、幼少35＝Solidゲート32＋Shuffledカテゴリ3）。ただし**1.6/1.7パッケージ済みのDLLは今回のC#変更より前の版**なので、現時点のゲーム実装に含まれる新しい幼少8件のゲートは**ビルド・両版DLL統合待ち**。今回の新規XMLの成人4件は製品XMLを更新すれば有効となり得るが、ロード後動作は未検証。**ゲームで111件が適用済みと報告しないこと**。既存34件の史実改稿と作者一括承認待ち日本語原稿35件は不変。1.6/1.7ネイティブゲーム受け入れと現在の原本文確認は最後に一括実施する。余計な自動テスト・公開・新規第3段階Defなし。


### 第6修正群の製品バイナリ収録追記（2026-10-17）

第6修正群時点で残っていた「新Solid幼少期8件の製品DLL未収録」は、1.6／1.7実ゲーム正規Managed参照を使った**ビルド専用**Project #39041617611／#39041895169が両方SUCCESSとなり、該当バイナリのGitBlob作成・Culture本体の版別Assemblies置換により解消した。元ソースは共通 `366888bccc897b25ee714e1092c00cc3f7080ab2`（Solid幼少期32件）、1.6 SHA-256 `688c32a5ab5b1dbefe73ccffc2ebcb2287156cf565018c7e1ed60e0167161b9d`、1.7 SHA-256 `7fb340337eaf9c998e3beb866b21ebcc92e9135201b6ca07c287ae150e8fb004`。**ソース111件が製品のロード済みゲームで有効と検証した意味ではない。** ゲーム生成／MO／旧セーブ確認と一括未承認日本語原稿35件は保留を維持する。


## 第2段階・第7修正群 — Solid幼少期23件と通常生成時の時代不整合な所持品17件（2026-10-17）

**確認対象：** 第1段階の原Vanilla Core 1.6.4871／1.7.4977 schema-v2定義を使用し、未対応のSF・現代制度フラグ77件を既存111制限・実装済み34改稿・一括承認待ち35原稿から差し引いて個別再確認した。旧公式英語原文コメントは[Ludeonの旧原文データ](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)を参照。現在配布中の原RimWorld 1.6/1.7本文と完全一致は確認できないため「旧原文の判定」であることを維持する。原定義のslot、元パス、強制Trait、既存所持品は正規両版メタデータで一致する。

### 未修正Solid幼少期の追加23件

以下は元の `BackstoryDefs/Solid/Solid_Child.xml` の通常Named Solid選択経路に接続しうる幼少期23件で、原の宇宙移住・星間軍隊／航行・現代電子／動画・人体改造等を**史実改稿なしで**通常選択させない。既存 `Source/AMJ.Culture/SolidChildBioGatePatch.cs` の集合へ追加し **32→55件**とする。

| 不整合分類 | 元DefName |
|---|---|
| クローン／人工人体・現代の遺伝子研究 | `AbandonedChild30`、`CaravanChild53`、`ChildScientist30`、`VatgrownSlavegirl8` |
| 宇宙船・冷凍睡眠・星間移住・軍事／未来兵器 | `Colonist22`、`CrimeBossChild39`、`FeralChild85`、`MidworldCadet83`、`PilotFan16`、`ShipChild46`、`SocialPariah3`、`StarforceCadet79`、`VoyagerChild94` |
| 量子物理・宇宙軍需の技術的英才教育 | `AspiringPhysicist47`、`StudentEngineer34` |
| 電子計算機・オンライン言論・現代のVR/ゲーム/動画 | `DiplomatsChild97`、`LogicalChild2`、`MidworldGeek18`、`ProfessionalGamer76`、`RebelWriter82`、`VideoGamer55`、`VidtubeStar98` |
| 工業化学工場・産業惑星での労働 | `ToxicChild96` |

※上の分類は生成を制限した理由であり、子どもの性格、性自認、被害経験、出身身分、文筆・武芸等の歴史的職能そのものを不適切と評価するものではない。すべての元Def、仕事制限・技能、固有人物／旧セーブを維持する。元の `LogicalChild2` が強制する `TooSmart=0` と `ToxicChild96` の `Nerves=-1`（元メタデータの degree `-1`）を触らない。将来作者が古代〜中世日本の同じ強制Trait・仕事・技能に合う日本語改稿を承認した場合、当該幼少期の通常抽選を復帰できる。単なるSFキーワードを含むもの全てを機械的に禁止する判断ではない。既存34件の歴史改稿と35件の一括日本語未承認原稿は変更しない。

### 出生背景から直接付与される不自然な初期アイテム17件

`Patches/HistoricalOccupationStartingPossessions.xml` 内の最初の6件は維持し、既存Vanilla XMLに該当する以下17件の**通常生成時**の所持品だけを変更する。Def・WorkDisables・skills・forcedTraits・派閥抽選カテゴリ・旧セーブに存在する実物アイテムは消さない。ダミーDefや新規アイテムは使わず、実際の元Coreで使用されている `Bow_Short`、`PlantPot`、`MealSimple`、`Cloth`、`Steel`、`MeleeWeapon_Spear`、`MedicineHerbal`、`Silver` を使用する。

| 元の種類 | 対象Def・変更内容 |
|---|---|
| 合成嗜好品 | `Smuggler23`：`Flake 20→Silver 20`。通常生成から禁止薬物を新たに供給しない |
| 工業銃器 | `Bodyguard58`（`Gun_Autopistol`）、`BushSniper94` / `OverwatchSniper42`（`Gun_SniperRifle`）、`Gunfighter51`（`Gun_Revolver`）の各1丁を `Bow_Short 1` に置換 |
| 近現代の耕作／携帯食 | `CropFarmer17`：`SunLamp 1→PlantPot 1`、`MercenaryChef44`：`MealSurvivalPack 2→MealSimple 2` |
| 時代不整合の衣服 | `Pirate56`：`Apparel_GasMask 1→Cloth 10`（Biotech時のみ）、`Rancher43`：`Apparel_CowboyHat 1→Cloth 10`、`Sheriff52`：`Apparel_Duster 1→Cloth 10` |
| 高度兵器／異常テクノロジー | `SteamworldTinker38` / `MadScientist22`：`Apparel_PackTurret 1→Steel 10`（Anomaly）、`ExplosivesExpert26`：`AncientMiningCharge 1→Steel 10`（Odyssey）、`Assassin20`：`NerveSpiker 1→MeleeWeapon_Spear 1`（Anomaly） |
| 現代の臓器移植素材 | `PirateDoctor0` / `UnethicalDoctor29`：`Kidney 1→MedicineHerbal 2`、`ProstheticSurgeon0`：`AestheticNose 1→MedicineHerbal 2`（Royalty） |

Anomaly／Royalty／Odyssey／Biotech限定元アイテムは、元XPathの存在を確かめてから `PatchOperationConditional` の内部で置換する。元DLCがない構成に不在XPathを強制しない。これは「初期所持品から持ち込まれる未来技術と合成薬」を止める限定した手段であり、Vanilla戦利品・装備テーブル・プレイヤーの古いアイテムは対象外。元の数値に機械的換算率を仮定せず、一品程度の携帯品として既存素材に置き換えた。Silver以外の価値等価性、DLC 1.7 XMLロードとバランスは実ゲーム未確認。

### 件数と残作業

追加前は通常生成制限 **111件**（成人76・幼少35）、追加後はソース上 **134件**（成人76＝成人カテゴリ75＋独立1、幼少58＝Solidゲート55＋Shuffled3）。初期所持品パッチは従来6件→**23件**（今回17件）となる。このうち新規Solid 23件は**C#ソースのみ変更済み**。既存の1.6/1.7 DLLはまだSolid55件を収録していないので、**次の完全ソース両版ビルド／実物DLL統合まで134件を製品動作と扱わない。**

未処理フラグの残り54件は、一般的な医師・役人・武人・旅商・芸能・工芸・学芸等に転用可能な背景が多く、無条件除外は不適切。生成カテゴリ／副作用を比較し、期間相当の日本語改稿が必要なものと、内容を維持できるものへ判定を続ける。既存35件の一括承認も最後のまま。第3段階の独自背景・特性は追加しない。ネイティブゲーム検証は作者の承認済み本文を反映した後の一括受入れまで実施しない。


### 第7修正群・元ゲーム1.6／1.7の製品DLL実物統合追記（2026-10-17）

本節は直上第7修正群の「新規23件C#はビルド待ち」を**後続で訂正**する。共通正規Cultureソース `228d86be846ee75444f3331957a824f866ea9918` の55件の名前付きSolid幼少期制限を、正規RimWorld 1.6／1.7ゲーム参照で別々にビルドした。Project #39043583308／#39043586558 両方SUCCESS、生成DLL 25,600バイト、元コードの他の玄関・薬物関連C#を含む。1.6 GitBlob `f99c8a134bc76630d3f9969138d3862c30e5fd50` / SHA-256 `527629e8353ec878940c99b1ddb869f3d4a8747ec436107c466efc12355083ba`、1.7 GitBlob `e638bbc36c1275c6e6d71024da0c9faf71215854` / SHA-256 `b87b7bf518cf8b3f76b9116de27be6f3a95abd4d5dd9d2aacb59490116cbb2e3`。製品へのバイナリ実物格納は同一PRで行う。**134背景＋初期所持品23の製品実装が実ゲームで期待通り動くという受入れではない**。両版でのloaded Def、特性・Pawn生成、MO併用、旧セーブの最終検証は別途必要。


## 第2段階・残る時代不整合可能性54件の史実転用原稿（2026-10-17）

第1段階CSVの背景 `EXPLICIT_TECH_OR_SF_ARCHIVE` 132件＋`POSSIBLE_MODERN_INSTITUTION_ARCHIVE` 68件＝**200件**について、既存 `AnachronisticAdultBackstories`、`AnachronisticShuffledChildBackstories`、`SolidChildBioGatePatch`、`HistoricalOccupationBackstories` と既存一括レビュー原稿を突合した。先行で未処理だったフラグ付き77件は、正規元Coreのゲーム版1.6／1.7メタデータで確認できるSolid幼少期の生成制限23件（PR #86）と、歴史的に転用可能な**追加54件の日本語改稿案**に分けた。結果、**200件のフラグ付き母集団について、制限・既存実装済み改稿・一括レビュー未承認案のいずれにも属さない項目が0件**となった。これは形式的な候補分類の完成であり、レビュー未承認分の本文実装や現行ゲームでの正常動作を証明しない。

追加の日本語案54件は [Stage2JapaneseReviewDraft.md](Stage2JapaneseReviewDraft.md) の36〜89番。先行35件と合わせ合計89件。成人と幼少の元Def／元 `slot`／元技能／仕事制限／強制Trait／元アイテムの整合は個別に残した。原のSF・近現代の学校／医療／政治／武器／娯楽／科学を、史実職能へ置き換えるため、**ゲーム文面に反映前に作者が日本語を一括承認**すること。改稿を承認するまで、該当する未来語がゲーム中に残る可能性があり、`Keep` と確定したわけではない。背景の内容が現代技術を示すという理由だけで、女性・男性の生き方や児童の階層、薬に関わる被害者等の存在を否定しない。

未立証範囲は(a)旧公式アーカイブ英語文と現行配布Core 1.6/1.7本文の完全一致、(b)Stage 1の「旧アーカイブに明白な衝突なし」550件を全員分再校閲したか、(c)文章未取得16件や固有Rare人物の元長文と通常抽選実績、(d)全134生成制限・23所持品調整の実ゲーム確認。これらを実施済みと報告しない。既存134件生成制限のC#/XML/両版DLL、34件の史実英日改稿、製品のTrait実装には本追加原稿による変化はない。


## 第2段階・第8修正群 — 旧英語原文未収録16件と名前付きRare18件の生成経路整理（2026-10-18）

**入力と判断の確度:** Stage1原台帳の `ARCHIVAL_DESCRIPTION_NOT_FOUND` **16件**と `SPECIAL_NAMED_RARE_ROUTE` **18件**を、過去に正規ゲームから取得済みの `1.6/core-character-defs.csv`／`1.7/core-character-defs.csv`（Project #38797500765、各Defのslot／元ファイル／英語title／カテゴリ／技能・強制特性）で対象ごとに照合。両版の元定義メタデータが一致する。**元の現行長文description全文はこのCSVにはなく、欠落した旧英語原文も創作しない。** `Solid`／`Shuffled Special`／`Solid_Rare` の違いを基準に、追加制限すべきか否かを判定した。

### アーカイブ本文未取得16件

| 生成経路 | 対象Def | 技術上の扱い |
|---|---|---|
| `Shuffled/Special.xml`（8） | `Child27` (Child)、`ChildTribal` (ChildTribal)、`Colonist97` (AdultColonist)、`ColonyChild59` (AdultColonist)、`Newborn79` (Newborn)、`TribeChild19` (AdultTribal)、`TribeMember57` (AdultTribal)、`VatgrownChild11` (VatGrown) | 特定生成目的の特殊カテゴリであり、一般のOffworld/Outlander/Tribal等の成人カテゴリと同一視しない。元の仕組みを壊しかねないため、**元Specialを変更しない**。特に `VatgrownChild11` はSF語を含むがBiotech等の特殊出生・成長処理との衝突を避ける。一般Pawn生成でSFを選択する証拠は未取得。 |
| `Solid/Solid_Child.xml`（4） | `MonkeyChild67`（動物と野外）、`MusicalKid14`（音楽）、`OldMoneyHeir46`（富家の跡取り）、`StewardsAssisant28`（家政の手伝い） | 既存タイトル・元技能が歴史的に存在し得る。元の長文本文不明を理由に削除／ゲート追加しない。将来現行本文が不整合と確定した場合のみ別対応。 |
| `Solid/Solid_Adult.xml`（4） | `CivilServant25`（役所、`Civil;Pirate;Offworld;Outlander;Trader`）、`RimworldExile32`（追放、`Offworld;Outlander;Pirate;Slave;Trader`）、`Streamer52`（ストリーマー、`Offworld;Outlander;Pirate;Slave;Trader`）、`TravelingBard93`（旅の吟遊芸人、同5カテゴリ） | `CivilServant25`、`RimworldExile32`、`TravelingBard93` は役人／追放者／芸能者への史実転用が可能なため、**長文未取得時点では維持**。`Streamer52` は元の英語の職業 `Streamer` が映像配信時代の職能を表す強い兆候があり、原文詳細の承認・日本史改稿まで**通常カテゴリのみを暫定制限**する。元の `Social+6;Animals+5`、DefID、仕事制限、旧セーブは不変。原長文の内容まで確定したとは主張しない。 |

**実装：** `Patches/AnachronisticAdultBackstories.xml` の成人カテゴリ制限XPathを **75→76** へ増やし `Streamer52` だけ `AMJ_CultureExcluded` とする。現在のVanilla 1.6/1.7の元カテゴリXPathを確認済みのため、新たなC#やDLLは不要。その他15件は通常生成を強制排除する根拠が弱く、現行の値を維持する。特殊カテゴリの維持は「VatGrownを中世日本の通常出生として認める」という意味ではない。元の特殊システムを勝手に破壊しないための境界である。

### 開発者由来・名前付きRare18件

元の `BackstoryDefs/Solid/Solid_Rare.xml` は開発者由来**9組の成人・幼少期**。通常の名前付きSolid選出に到達し得る（絶対的な個人固定のみとは判定しない）が、Rare全員の元の長文はアーカイブに載っていない。元title／カテゴリ・技能・強制Trait以外の文脈は不明。

- **成人4件は既に生成制限済み**：`JoeCustomAdulthood` (game developer)、`LiamCustomAdulthood` (simulation developer)、`OskarCustomAdulthood` (mod designer)、`TynanCustomAdulthood` (game developer)。カテゴリ選択を制限しても、個別に割り当てた背景やセーブ済みのDefは削除しない。元の `LiamCustomAdulthood` の `Industriousness=1` と `TynanCustomAdulthood` の `Industriousness=-2` を維持。
- **成人5件は原titleだけで禁止しない**：`CynapseCustomAdulthood` (hermit)、`FeyCustomAdulthood` (game master)、`MateCustomAdulthood` (busker)、`NathanCustomAdulthood` (digger)、`WillCustomAdulthood` (labyrinth maker)。このうち`FeyCustomAdulthood` は現代ゲームの主催者か伝統的遊戯／芸能の世話役か未確定。題名だけで一律制限する根拠とはしない。
- **幼少期9件は原titleと成人ペアの条件を区別して保持**：`CynapseCustomChildhood` (tinkerer)、`FeyCustomChildhood` (wargame fanatic)、`JoeCustomChildhood` (drummer)、`LiamCustomChildhood` (sim addict)、`MateCustomChildhood` (curious kid)、`NathanCustomChildhood` (cave child)、`OskarCustomChildhood` (art slave)、`TynanCustomChildhood` (punk)、`WillCustomChildhood` (maze child)。`LiamCustomChildhood` のsim依存と `FeyCustomChildhood` のwar gameは要原文だが、幼少期には成人カテゴリがなく、通常生成が成人ペアで遮断されている場合がある。重複したC#ゲートを証拠なく増やさず、名前付きSolid経路で確認する。旧Rare人物の元設定は保存する。

**現行の状態と残留する証拠限界：** 正規元Core 1.6/1.7の**メタデータとして**16＋18件を個別分類したので、欠落長文による無根拠な削除をしない判断は確定。未取得の**配布中Coreの生のdescription長文**、Rare実抽選の個別経路（通常出現頻度）については未立証として分離する。新規のStoryDef・TraitDef、英日歴史本文、強制Trait・職務効果は変更しない。現在の生成制限は **成人77（成人カテゴリXML76＋独立`FerventResearcher72`1）、幼少58（Solidゲート55＋Shuffled3）、合計135件**。元の34件史実改稿、23件の生成時所持品置換、最終確認待ち日本語原稿89件は不変。この追加はXMLだけなのでビルド済み1.6/1.7 DLLの再製造は不要だが、ロード後の効果は未確認。Steamやネイティブの繰り返しテストは行わず、後続の一括受入れへ渡す。


## 第2段階・第9修正群 — 原稿日本語の品質指摘による全面改稿（2026-10-18）

**以前の89件原稿は無承認・差し戻し。** title/shortをもっともらしい架空役名で埋めてしまい、`description` も技能やWorkDisablesの由来を説明するための不自然な創作が目立った。ユーザーの指摘を受け、元Core 1.6/1.7の定義属性を再照合したうえで、日本語タイトル・説明を全件再構成。原の効果を文章だけで改変しない。**87件のdescriptionすべて変更**、原稿数89→87。未承認でゲームには投入しない。

| 元Def | 元属性と修正理由 | 暫定措置 |
|---|---|---|
| `AddictionCounsel60` | `Solid_Adult`、Offworld／Outlander。Social+8・Medicine+3・Artistic+3・Crafting+3で、依存症専門職に相当する独立職の根拠がない。寺での困窮者救済と専門カウンセラーは別物。 | 通常生成除外、Def／能力維持、作者確認用の無理な日本語原稿は廃止 |
| `Architect28` | `Shuffled/Offworld_Glitterworld_Adult`、Offworld。Intellectualを必須としManualDumb不可、Construction+5・Artistic+7・Crafting−3、元TextBook所持。中世の番匠・棟梁を、この近代的な非肉体労働専門職に安易に読み替えるのは不適切。 | 通常生成除外、元の所持品等もDef内に残す |
| `BountyHunter41` | `Solid_Adult`、Offworld／Pirate、Shooting+8・Melee+5。`追捕使`は官職、元の旧英語本文は富裕な依頼主のために秘密裏に殺害する職歴であり、盗賊追跡を語っていない。 | **雇われの刺客**。弓・近接戦闘の技能を維持し、「賞金稼ぎ」「盗賊狩り」「秘密の依頼主」創作は新しい日本語本文に残さない |
| `Castaway81` | 難破後の野外生存という役割。 | **難破の生存者**とする |
| `CharityWorker36` | `Shuffled/Offworld_Glitterworld_Adult`、Offworld。Violent不可、Caring／Social必須、Social+6。中世にも僧侶の施しや病人の救済の実例がある。一般的な「世話役」へのすり替えはしない。 | **施しの僧**とする |
| `ExpertHandyman8` | Construction+7・Intellectual+4・Crafting+3。実際に修理する多能工であり、直訳の「修繕職」は日本語の職業名として不自然。 | **腕利きの職人** |
| `FurnitureBuilder83` | 木を扱って家具を作る仕事。指物は室町以降に専門職の例があり、単に「家具職人」で意味は通る。 | **家具職人** |
| `GangSoldier39` | 戦闘の古参。原の武器・料理・手当ての技能を維持。 | **古参の野盗** |

**実装：** `Patches/AnachronisticAdultBackstories.xml` に2件のカテゴリ制限追加（XML通常成人76→78）。ほかの既存制限の変更なし。新しい集計は成人79（XML78＋独立1）＋幼少58＝**137背景**。23件の初期所持品差替え、34件の製品反映済み英日改稿は維持。 `Docs/Stage2JapaneseReviewDraft.md` は87件に再番号付けされ、同じ原Defに対する短い自然な日本語を全件差し替え。**旧原稿のtitle・説明文を部分的に修正しただけで承認できる状態とせず、全件新規確認の対象として提示する。**

**未実施：** 作者の日本語文章承認、英語化・ゲーム文面反映、現行Core原文完全照合、Loaded Pawnの通常生成・旧セーブ・MO互換ゲーム検証、Steam公開。歴史用語の確認は一般的な史料ベースで、元Coreの現在の原文が必ず同一とは断言しない。


### BountyHunter41個別語彙・原文整合性の再確認（2026-10-18、作者指摘）

上記の旧タイトル「盗賊狩り」は誤った転用だった。旧公式英語原文は裕福な依頼者のために内密の殺害を行う（glitterworldのbounty hunter）という趣旨であり、「盗賊を追う」／「誰から受注したか滅多に話さない」という叙述は原文の直訳でもなく、自然な中世日本の人物紹介でもない。原ゲームがSFのbounty hunterという職能を持つことと、ゲーム側に日本中世の賞金稼ぎという制度を新設することを混同しない。

作者から刀／剣の語彙指摘もあり、原稿1件のtitle／shortTitle／descriptionを **「雇われの刺客」／「刺客」／「[PAWN_nameDef]は有力者に雇われ、敵対する者を密かに討ってきた。弓と剣に長け、狙った相手を逃すことは少なかった。」**に訂正した。ここで「剣」は一文上の広い刀剣表現であり、史実の鎌倉武士の具体的な佩刀の呼称「太刀」と同一とは主張しない。元の `Shooting+8;Melee+5` を維持。これは**まだ未承認の文章案**であり、ゲームの元Def、C#/XML、英語版、初期所持品、他の86原稿を変更していない。

## Phase 2 — final approved Japanese texts and generation adjustment (2026-10-19)

Author-approved Japanese originals: 89, including reopened AddictionCounsel60 and Architect28. [The approved canonical text](Stage2JapaneseReviewDraft.md) is implemented through English Core title/short/description patches and Japanese DefInjected; historical names no longer need to resemble original modern careers. All original mechanical effects, forced traits and item Defs remain. Two adult category-only exclusions removed: restricted total 135 (adult 77, child 58). Existing 34 bilingual rewrites and 23 possession substitutions unchanged. Current exact Culture commit `21a9f70279a8859167f6765fcaac2bfdc95b47f8`: real **Vanilla** 1.6/1.7 loaded-game PASS ([1.6 #39195995302](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39195995302), [1.7 #39196173048](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39196173048)), Pickle 1/1 each, ERROR0 and exit0. Current acceptance tests verify 89 loaded English titles, nonempty descriptions, all 77/58 restricted original Defs, the two category restorations, a normal Pawn and Harmony Solid gate; the full description bodies, Japanese glyph rendering, every original starting possession/forced Trait or old-save compatibility. **Original MO loaded-game acceptance:** 1.6.4871 + original MO 1.6.2.2 + VEF/SYR [#39204533172](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39204533172) **PASS** (Pickle 1/1, ERROR0); 1.7.4977 + staged original **MO 1.6.2.2** + VEF/SYR [#39201144280](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39201144280) **PASS** (Pickle 1/1, runtime/FMOD ERROR0). Both validated all 89 loaded titles/nonempty descriptions, 135 restricted original backgrounds, two reopened categories, pawn generation and Harmony Solid gate with existing MO assertions. Initial 1.6 [#39200901738](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39200901738) failed only the added 90s Quickstart readiness wait; corrected via Project PR #738 (merge `48dd1d2faf1b188af6a761154df43563c15ad286`), retested PASS. **Follow-up source and native Japanese acceptance (2026-10-19):** Licensed 1.6.4871 original-game isolated Japanese [#39208562796](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39208562796) **PASS** 1/1, Unity errors0, exit0, verifying all **89 Japanese loaded title/titleShort/full description strings** exactly match approved DefInjected; 356 corresponding XML keys also match the approved Japanese draft. Previous Japanese-mode failures were caused by using the wrong installed language folder name, not a demonstrated Culture translation defect. Genuine Core 1.6 and 1.7 metadata agree for all 89 originals (including **13** with forced traits and **9** with possessions). All **267** Stage 2 XML XPath operations affect only the three text fields, never mechanics. Existing **23** historical starting-possession XPaths have exact source-item matches in both original Core 1.6/1.7 metadata (23/23 each), including overlap `Sheriff52: Apparel_Duster → Cloth=10` and `Smuggler23: Flake → Silver=20`. These are **static** matches, not generated-Pawn grant verifications. **Scope limits remain:** 1.7 MO test used original 1.6 MO package rather than native 1.7 MO; Japanese screen glyph rendering in the isolated Linux host, per-Pawn forced traits/items, old-save reload and Steam publication are **not** validated. New game recommended.


**Author closure of background Phase 2 (2026-10-19): COMPLETE.** The remaining screenshot/Japanese-glyph visual inspection was **explicitly skipped** by the author and is **not a PASS**. Source texts and actual loaded Japanese title/titleShort/full descriptions (89/89) have passed; scoped original 1.6/1.7 and MO compatibility game tests have passed, plus static original Core trait/possession integrity checks. Targeted item/trait per-Pawn assignments, actual old-save reload and native MO 1.7 remain unverified but **not Phase 2 blockers**. New game recommended. Background Phase 2 closed independently of still-open Culture Trait work and Steam release.
