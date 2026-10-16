# AMJ Culture Coordination

The authoritative owner-level status/handoff log is `main:Docs/Coordination.md`.
Confirmed specifications and implementation results belong in `Docs/Design.md`, XML/source and the affected test record; Project retains only cross-owner status pointers.

## CULTURE-NO-TABLE-MINIMAL-20261016

**Status: SOURCE STAGED / RUNTIME OPEN.** The minimum XML patch changes only `AteWithoutTable` stage `baseMoodEffect` to `0`, preserving original ThoughtDef identity and all seating/meal/room systems. Source-only static XML structure check passed. Native RimWorld 1.6 Vanilla/MO loaded Def, neutral no-table meal, impressive dining room and other penalties are UNTESTED; RimWorld 1.7 compatibility is also UNTESTED. No Workshop release is approved.

**Next:** once this owner source is actually committed through a PR, run one focused isolated native 1.6 acceptance test with the exact owner commit and actual game/provider identity, then 1.7 compatibility if 1.6 passes. Do not repeat the retired Project stool proof.

**Separate OPEN:** historically suitable enza (円座), Comfort and seat-specific meal destination. Do not blend that task into this minimal Thought patch.
