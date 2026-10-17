# AMJ Culture Coordination

Authoritative coordination file: `main:Docs/Coordination.md`. Confirmed scope and acceptance conditions belong in `Docs/Design.md`; the actual Thought modification belongs in `Patches/AteWithoutTable.xml`.

## CULTURE-CHAIR-ONLY-MEAL-20261017

**Status: IMPLEMENTED / DLL ON MAIN; RUNTIME ACCEPTANCE QUEUED (2026-10-17).**
Culture [PR #9](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/pull/9) was merged to `main` at `832e2ba3d2e342372a67520feeb91d1397453af2`. The production C# adapter `Source/AMJ.Culture/TablelessChairMealPatch.cs` and compiled 1.6 `Assemblies/AMJ.Culture.dll` (7,680 bytes; SHA-256 `609f8d2b2d4f9bc48f6ee86474f2b0e15c5d7abb2f342bd855dac526250a4dbb`) plus Harmony requirement are packaged. Exact build-only [Project #38995487716](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/38995487716) succeeded against isolated owned 1.6 managed assemblies. Formal gameplay specification is `Docs/Design.md`.

**Player behavior implemented in source:** ordinary existing valid `isSittable` chairs are candidate eating destinations without a table; if a functional seat adjacent to an Eat surface is available, keep Vanilla selection; otherwise use normal sit/spot reservation and travel; without a chair, fall back to the original ingest travel. `AteWithoutTable=0` remains separate. No new enza/other furniture, assets, zones, or global `HasEatSurface` rewrite. Prior enza-only draft PR #6 is closed unmerged.

**Not yet verified in game:** the single focused original 1.6 Vanilla chair/Comfort/ingestion acceptance [Project #38995788200](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/38995788200) has been manually requested and was queued behind another job at handoff. Do **not** report runtime PASS before checking final Pickle and complete ERROR-zero log. If it fails, fix the concrete gameplay defect and rerun the affected check only. No 1.7 claim, broad rerun, auto-trigger, or Workshop release. Test program uses source pinned at `55f47961ccffba6725e42079908839cebced50e5` (same production DLL and C# as main).

## CULTURE-NO-TABLE-MINIMAL-20261016

**Historical initial prototype only (2026-10-16):** `main` exists and holds the standalone five-file Culture 1.6 prototype plus `LICENSE` at `390b24d4a9bbd1e24d6544f3fdcff9ac1ff6ba3d`. The original topic history is an ancestor of `main` but the branch has been removed. No Culture PR was recorded for the initial bootstrap; do not characterize it as a PR merge. Future changes must use topic→PR→main.

**Historical first slice:** `Patches/AteWithoutTable.xml` replaced existing `ThoughtDef=AteWithoutTable` `baseMoodEffect` with `0` without changing ingest Jobs. A later chair-meal implementation and Harmony DLL are now on main; see the active section above. The initial source-only check was not a loaded-game PASS. Source XML structure/scope check PASS; this is not a real loaded-game PASS.

**NEXT — specify the remaining Culture gameplay, not a native-test campaign:** Follow owner `Docs/Design.md` and Project execution priorities: assess historical psychoactive-drug supply/use and related Traits/Needs, while resolving the known `Transhumanist` natural/forced-Backstory generation paths. Culture-wide specification and implementation are incomplete; only the minimal no-table XML behavior has source. For now, the dedicated Culture 1.6 Vanilla/MO gameplay harness and 1.7 compatibility checks are DEFERRED, not PASSED or waived. Before this existing patch or a later coherent feature is accepted for release, require focused actual loaded-Def and gameplay evidence, preservation of table/seat/room positives and independent penalties, exact source/provider identity and strict ERROR-zero logs. No test-only work solely because the runner is available.

**Separate OPEN:** historically suitable enza (円座), actual Comfort and seat-specific meal destination. Its scope is independent of this minimal Thought patch.

Tracking: [Culture issue #1](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/issues/1). Project cross-owner source: [CultureCandidate](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md).
