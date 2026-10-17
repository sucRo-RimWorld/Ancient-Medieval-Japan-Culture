# AMJ Culture Coordination

Authoritative coordination file: `main:Docs/Coordination.md`. Confirmed scope and acceptance conditions belong in `Docs/Design.md`; the actual Thought modification belongs in `Patches/AteWithoutTable.xml`.

## CULTURE-CHAIR-ONLY-MEAL-20261017

**Status: IMPLEMENTED / REAL 1.6 TABLE-FREE CHAIR MEAL PASS; FULL ERROR-ZERO ACCEPTANCE RECHECK QUEUED.** Source and compiled RimWorld 1.6 `Assemblies/AMJ.Culture.dll` (SHA-256 `609f8d2b2d4f9bc48f6ee86474f2b0e15c5d7abb2f342bd855dac526250a4dbb`) were merged by [Culture PR #9](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/pull/9), main `832e2ba3d2e342372a67520feeb91d1397453af2`. Implementation and accepted intended behavior are respectively `Source/AMJ.Culture/TablelessChairMealPatch.cs` and `Docs/Design.md`.

**Actual first native result:** [Project 1.6 run #38995788200](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/38995788200) loaded the exact production DLL (SHA above) in original 1.6.4871 rev598. One Vanilla `Stool` **without an adjacent table** was actually chosen for a normal Ingest Job, Comfort increased and meal consumption finished; Pickle 1/1 passed, exit 0, original game unchanged. **The workflow was correctly FAIL overall**: one independent ERROR from `Patches/TranshumanistGeneration.xml` invalid `FerventResearcher72/forcedTraits` PatchOperationRemove XPath. No seat-Job error observed. This did not verify other furniture, MO, table-first precedence, fallback, impressive dining room or 1.7.

**Concrete ERROR correction:** [Culture PR #15](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/pull/15), owner main `2bcfdc9343b3ea5c94c216893a74cdf2a7d40f30`, removed only the failing third XPath, retained `Transhumanist` normal commonality 0 and `FerventResearcher72` ordinary spawn-category exclusion. The owner Design now explicitly **does not claim** control of direct externally forced background assignment. No meal code/DLL change.

**Current sole retry:** [Project run #39002811535](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39002811535) was manually triggered after author approved actual game testing, pinned exactly to corrected Culture main `2bcfdc9343b3ea5c94c216893a74cdf2a7d40f30`, and was **queued** at this update because the isolated runner was occupied by another project job. Check the final one-scenario Pickle report, full native ERROR log, source/provider identity and preservation of original game before promoting clean runtime acceptance. No duplicate dispatch, broad test expansion, PR-triggered game launches or Steam release without separate approval. Existing 1.7 proof-of-concept is not Culture production acceptance.

## CULTURE-NO-TABLE-MINIMAL-20261016

**Historical initial prototype only (2026-10-16):** `main` exists and holds the standalone five-file Culture 1.6 prototype plus `LICENSE` at `390b24d4a9bbd1e24d6544f3fdcff9ac1ff6ba3d`. The original topic history is an ancestor of `main` but the branch has been removed. No Culture PR was recorded for the initial bootstrap; do not characterize it as a PR merge. Future changes must use topic→PR→main.

**Historical first slice:** `Patches/AteWithoutTable.xml` replaced existing `ThoughtDef=AteWithoutTable` `baseMoodEffect` with `0` without changing ingest Jobs. A later chair-meal implementation and Harmony DLL are now on main; see the active section above. The initial source-only check was not a loaded-game PASS. Source XML structure/scope check PASS; this is not a real loaded-game PASS.

**NEXT — specify the remaining Culture gameplay, not a native-test campaign:** Follow owner `Docs/Design.md` and Project execution priorities: assess historical psychoactive-drug supply/use and related Traits/Needs, while resolving the known `Transhumanist` natural/forced-Backstory generation paths. Culture-wide specification and implementation are incomplete; only the minimal no-table XML behavior has source. For now, the dedicated Culture 1.6 Vanilla/MO gameplay harness and 1.7 compatibility checks are DEFERRED, not PASSED or waived. Before this existing patch or a later coherent feature is accepted for release, require focused actual loaded-Def and gameplay evidence, preservation of table/seat/room positives and independent penalties, exact source/provider identity and strict ERROR-zero logs. No test-only work solely because the runner is available.

**Separate OPEN:** historically suitable enza (円座), actual Comfort and seat-specific meal destination. Its scope is independent of this minimal Thought patch.

Tracking: [Culture issue #1](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/issues/1). Project cross-owner source: [CultureCandidate](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md).
