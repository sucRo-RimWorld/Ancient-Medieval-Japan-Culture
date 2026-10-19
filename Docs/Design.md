# AMJ Culture – Characters — independent Mod design (extraction candidate)

**Author scope decision (2026-10-19):** Split only **backgrounds and traits** from the existing AMJ Culture. Other Culture subfeatures remain in the original Mod, with no broader decomposition authorized. The identity and game effect of all existing original Vanilla/MO BackstoryDef and TraitDef records must remain stable.

## Scope

- Vanilla/MO historically inappropriate **ordinary background generation** restrictions: adult **77** (76 XML + FerventResearcher72), children **58** (55 original Solid biographies gated via Harmony + 3 Shuffled XML); existing unique/nonordinary references remain.
- **89** author-approved additional historical background rewrites and pre-existing **34** bilingual rewrites (total **123** existing DefNames rewritten, not newly generated backgrounds), matching English XML plus approved Japanese DefInjected.
- Existing **23** historical starting-possession XML substitutions (keep their original item source and Def identities).
- Trait generation such as **Transhumanist commonality 0**, with its related forced adult background restriction; trait redrafting/new culturally fitting TraitDefs remain **future work**, not approved as complete.
- Harmony 1.6/1.7 Solid childhood generation patch and a **separate** minimal bootstrap, assembled into `AMJ.Culture.Characters.dll` per game version.

## Explicit exclusions — stay in Culture

- `Greedy` / `Jealous` barracks-related ThoughtWorker behavior, TraitDef contextual descriptions, thoughts and Japanese Thought translations: owning effect is the Culture **shared lodging** custom. Never copy SharedLodgingTraitPatch or SharedLodgingTraitThoughts here.
- New Pawn chemical addiction/tolerance generation, DrugDesire/psychoactive supplies and chemical balance: owning effect is Culture **drugs**. Never copy InitialDrugAddictionGenerationPatch.
- Entrance/no-shoes, chair meals, bedding, social Thought rules, other daily-life rules and their assemblies/assets.
- Neither Mod is a mandatory runtime dependency of the other. Harmony is required. Medieval Overhaul remains optional.

## Transition and save rules

**Source split is not runtime migration.** The old Culture `1.6/Assemblies/AMJ.Culture.dll`, `1.7/Assemblies/AMJ.Culture.dll` and its six original background-related XML patches **still contain** these rules until a coordinated cutover. Do not co-install this candidate with old Culture: duplicate Harmony gates and duplicate XML rewrites otherwise occur. Before releasing the extracted Mod, actually build its two versioned DLLs, remove all six owned XML files and the Solid-child C# patch from Culture, rebuild both Culture DLLs so no stale patch remains, and verify the new two-Mod combination and each separately in a bounded original-game acceptance. Revisit existing Culture title/About wording and generation claims at cutover; preserve valid unrelated gameplay/Thought/drug/entrance behavior. No direct-save migration and no Retroactive Pawn replacement. Recommend a new game; do not promise old save compatibility or mid-save removal safety.

**Recorded accepted evidence belongs to the prior Culture package, not the extracted package:** Vanilla 1.6/1.7 loaded-game + original MO compatibility acceptance (Project runs #39195995302, #39196173048, #39204533172, #39201144280); Japanese 89 full loaded-text match #39208562796. These provide a baseline for a **single cutover check**, not an automatically transferable PASS.

## Repository provisioning

The linked GitHub integration does not expose a create-repository operation. The independently rooted **`candidate/culture-characters-source` branch** of `Ancient-Medieval-Japan-Culture` stores complete extractable source and approved prose in the **root layout of the future** `Ancient-Medieval-Japan-Culture-Characters` repository. This candidate branch is **not to be merged into the Culture main branch** (its tree intentionally excludes the other Culture features). Once a new repository exists, copy this exact candidate root tree into its main branch via approved GitHub Git data operations; then implement a coordinated cutover with old Culture to prevent duplicate operation. No release or upload is approved by source staging.
