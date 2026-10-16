# AMJ Culture Coordination

Authoritative coordination file: `main:Docs/Coordination.md`. Confirmed scope and acceptance conditions belong in `Docs/Design.md`; the actual Thought modification belongs in `Patches/AteWithoutTable.xml`.

## CULTURE-NO-TABLE-MINIMAL-20261016

**Status: SOURCE ON MAIN / NATIVE VALIDATION OPEN (2026-10-16).** `main` exists and holds the standalone five-file Culture 1.6 prototype plus `LICENSE` at `390b24d4a9bbd1e24d6544f3fdcff9ac1ff6ba3d`. The original topic history is an ancestor of `main` but the branch has been removed. No Culture PR was recorded for the initial bootstrap; do not characterize it as a PR merge. Future changes must use topic→PR→main.

**Implemented:** `Patches/AteWithoutTable.xml` replaces only existing `ThoughtDef=AteWithoutTable` `baseMoodEffect` with `0`. No Harmony, meal-Job, furniture, seat, Comfort, room reward, other Thought, or dependency changes. Source XML structure/scope check PASS; this is not a real loaded-game PASS.

**NEXT — one bounded native 1.6 acceptance:** On the established isolated Linux host, run the exact Culture `main` source and original Vanilla 1.6 (then MO 1.6 if Vanilla passes). Confirm loaded `AteWithoutTable` stage value `0` and actual meal without table has no negative mood effect; demonstrate an ordinary table and an impressive dining room still provide their original benefits when normally applicable; observe unaffected food/filth penalties. Preserve original sources, player saves/ModsConfig, strict native ERROR-zero logs and exact game/owner/optional-provider identities. This native scenario has not yet been implemented or run; do not substitute source parsing or the retired stool experiment. RimWorld 1.7 remains separately unverified and is not declared supported.

**Separate OPEN:** historically suitable enza (円座), actual Comfort and seat-specific meal destination. Its scope is independent of this minimal Thought patch.

Tracking: [Culture issue #1](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/issues/1). Project cross-owner source: [CultureCandidate](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/CultureCandidate.md).
