# AMJ Culture Coordination

Authoritative coordination file: `main:Docs/Coordination.md`. Confirmed scope and acceptance conditions belong in `Docs/Design.md`; the actual Thought modification belongs in `Patches/AteWithoutTable.xml`.

## CULTURE-1-7-IMPLEMENTATION-20261017

**Status: 1.7 PRODUCT DLL BUILT / VERSION-SPLIT PACKAGE STAGED; RUNTIME ACCEPTANCE OPEN.** RimWorld 1.7.4977 original-managed [build #39015931527](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39015931527) compiled exact Culture source `bd07ceb34619a992a934ca0a7add7be1f1ae8a8b` into 9,216-byte DLL SHA-256 `96a55e102c6ab0155f4c5336466afe6d398dbcefd915486bd31bd957cdf84e58`. 1.6 version stays its original 9,216-byte DLL SHA-256 `0d944eb47a1e648acdbc13ca75948e075e5ccfd71fefe1d4196c54d5a6c5be03`. Product layout/design is recorded in owner `Docs/Design.md`. No 1.7 gameplay PASS claimed from build alone; perform **one** focused real 1.7 acceptance after package integration, not PR-triggered tests or a matrix. No Steam publication.

## CULTURE-CHARACTER-BACKGROUNDS-20261017

**Status: 67 BACKGROUND RULES IMPLEMENTED / NEW 1.6 DLL BUILD PASS / SECOND-WAVE NATIVE ACCEPTANCE OPEN.** Culture now includes 43 adult and 24 Solid-child ordinary-generation restrictions; the updated 9,216-byte `AMJ.Culture.dll` SHA-256 `0d944eb47a1e648acdbc13ca75948e075e5ccfd71fefe1d4196c54d5a6c5be03` was compiled by existing isolated [build-only #39007502577](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39007502577). Formal second-wave decisions in `Docs/SFBackgroundSecondWave.md` and `Docs/Design.md`; implementation in `Patches/AnachronisticAdultBackstories.xml`, `Patches/TranshumanistGeneration.xml`, `Source/AMJ.Culture/SolidChildBioGatePatch.cs`. No game started for the second wave; MO, 1.7 and old-save load remain unverified.

[Actual isolated original 1.6 acceptance #39005480093](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005480093): loaded adult categories **31/31**; preserved childhood Def IDs **12/12**; real `Transhumanist` private commonality zero; Harmony installed; one excluded `MechanoidNerd10` Solid Bio rejected, one then-unblocklisted `FallenProdigy40` retained (subsequently found to have anachronistic glitterworld history, now added to the exclusion list); a new normal player pawn generated without banned histories; Pickle **1/1**, Unity/raw log ERROR **0**, exit 0, original game hashes unchanged. Tested source exact `18636756e1a722466009a013ad17d6e8997ce40d`, matching the **previous** 8,704-byte production DLL. Initial [#39005233520](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005233520) was a test-only C# compile failure and did not start RimWorld; fixed by private-field reflection without production source changes.

The first-wave test applies only to its old 31 adult / 12 child list and does not accept the added second-wave 12+12 changes. Do not infer that all twelve initially blocked children were individually selected in gameplay, that the shuffled fallback was deliberately forced during the test, that a saved pawn was reloaded, or that MO/1.7 passed. This is not a reason to repeat native tests, add a broad matrix or claim Culture feature/release completeness. All required exact expected outcomes, implementation boundaries and evidence belong to `Docs/Design.md`.

## CULTURE-CHAIR-ONLY-MEAL-20261017

**Status: CHAIR SOURCE UNCHANGED / THREE-BRANCH VANILLA NATIVE PASS ON PREVIOUS 8,704-BYTE DLL.**
The generic `isSittable` meal route is unchanged since [Culture PR #9](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/pull/9). Culture currently packages the new 9,216-byte DLL SHA-256 `0d944eb47a1e648acdbc13ca75948e075e5ccfd71fefe1d4196c54d5a6c5be03`, rebuilt solely for an additional background rule without changing the chair adapter. The actual table-priority/native seat PASS below refers to the superseded 8,704-byte DLL; do not call that an exact new-binary test. Canonical scope and complete evidence remain in `Docs/Design.md`.

**Latest actual 1.6 result:** [Project run #39005539686](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005539686) SUCCESS, 1.6.4871 rev598, Culture source `1f806e962abcc5c2437b85fed81121519492c948`, matching DLL. One-game test covers table-free Vanilla Stool eating with Comfort gain and meal completion; a farther table-adjacent Stool preferred to the closer table-free Stool; and standing food consumption with no chair after removing furniture. Pickle 1/1, runtime and raw log ERROR0, exit0, original game unchanged. The first attempt [#39005451249](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005451249) was blocked before launching RimWorld due to obsolete expected binary hash, fixed by [Project PR #588](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/pull/588). The prior [one-chair PASS #39002811535](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39002811535) used the earlier 7,680-byte DLL, now superseded.

**MO furniture check (separate snapshot):** [Project #39007512785](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39007512785)
SUCCESS, original staged Medieval Overhaul 1.6.2.2 + VEF + SYR, authentic
RimWorld 1.6.4871 rev598, Culture source `6902a96870e3ed32f05b2d633b9dbfbcfd970e69`,
8,704-byte production DLL SHA-256
`2fa6ab6429bd9f81d48633bea0998b47c7255fbb8658b73addc697d8ddf24010`.
A single original `DankPyon_RusticStool` was used without an eating table;
Comfort increased and a normal meal finished; Pickle 1/1, Unity/raw ERROR0,
exit0, originals unchanged. Only the **disposable clone** converted two known
MO/SYR unsupported MP3 files to WAV; the original MP3 profile is **not**
ERROR-zero certified. The newly compiled **9,216-byte main DLL** above was
not exercised by this MO run, although the chair C# source is unchanged.
The real-game test-only [Culture PR #25](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/pull/25)
and manual [Project PR #591](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/pull/591)
are merged, no new chair production code.

**Not automatically scheduled:** Other MO/third-party furniture, dining-room positive Thoughts, original MP3 ERROR-zero or RimWorld 1.7 compatibility remain untested by this narrow check. Do not expand tests merely for more PASS counts; no PR-triggered native workflow or Steam Workshop authorization follows. Independent Culture Background status is maintained above and must not be conflated with this seat acceptance.

## CULTURE-NO-TABLE-MINIMAL-20261016

**Historical initial prototype only (2026-10-16):** `main` exists and holds the standalone five-file Culture 1.6 prototype plus `LICENSE` at `390b24d4a9bbd1e24d6544f3fdcff9ac1ff6ba3d`. The original topic history is an ancestor of `main` but the branch has been removed. No Culture PR was recorded for the initial bootstrap; do not characterize it as a PR merge. Future changes must use topic→PR→main.

**Historical first slice:** `Patches/AteWithoutTable.xml` replaced existing `ThoughtDef=AteWithoutTable` `baseMoodEffect` with `0` without changing ingest Jobs. A later chair-meal implementation and Harmony DLL are now on main; see the active section above. The initial source-only check was not a loaded-game PASS. Source XML structure/scope check PASS; this is not a real loaded-game PASS.

**NEXT — specify the remaining Culture gameplay, not a native-test campaign:** Follow owner `Docs/Design.md` and Project execution priorities: assess historical psychoactive-drug supply/use and related Traits/Needs, while resolving the known `Transhumanist` natural/forced-Backstory generation paths. Culture-wide drug/Backstory/Trait specification and implementation remain incomplete; chair-only meal C#/DLL are present and have the scoped native PASS recorded above. The dedicated broader 1.6 Vanilla/MO integration and 1.7 compatibility checks remain DEFERRED, not PASSED or waived; the focused Vanilla Stool meal PASS above is established. Before this existing patch or a later coherent feature is accepted for release, require focused actual loaded-Def and gameplay evidence, preservation of table/seat/room positives and independent penalties, exact source/provider identity and strict ERROR-zero logs. No test-only work solely because the runner is available.

**Separate OPEN:** historically suitable enza (円座), actual Comfort and seat-specific meal destination. Its scope is independent of this minimal Thought patch.

Tracking: [Culture issue #1](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/issues/1). Project cross-owner source: [CultureCandidate](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md).
