# AMJ Culture — production source and per-version builds

**Latest compilation-only status (2026-10-17):** The new-pawn excluded-chemical addiction/tolerance filter is in `Source/AMJ.Culture/InitialDrugAddictionGenerationPatch.cs` and both per-version product assemblies. Source commit: `009d36c75d0d9f557ab63563723e78efc6cfbc8c`; licensed 1.6 [build #39034597068](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39034597068) DLL `5e2f9a930210c6223b7a21259f8f689f39241d248887b2b2a3b419a9b252f77d`; licensed 1.7 [build #39034604350](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39034604350) DLL `a36d5a5774feb7b0304f40a378099b7d8fd170a16885b63146bfbdb576366234`. Existing 1.6/1.7 chair/background runtime PASS pertains to **older binaries**, not these newly compiled ones. One consolidated game-load, pawn generation and old-save test is still required for this new functionality before release. See the current [Design](../Docs/Design.md#initial-addiction-on-newly-generated-pawns--targeted-c-source-2026-10-17).

## Historical chair adapter and prior runtime evidence

**Implementation state:** Culture has version-selected 1.6 and 1.7 assemblies. The 1.7 DLL (SHA-256 `96a55e102c6ab0155f4c5336466afe6d398dbcefd915486bd31bd957cdf84e58`) was built from original RimWorld 1.7.4977 in [Project #39015931527](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39015931527); genuine Vanilla 1.7 gameplay acceptance [#39016905072](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39016905072) is PASS: table-free chair/Comfort, table-first, standing fallback, loaded 43+24 backgrounds and normal Pawn in one Pickle scenario with ERROR0, game exit0, original game unchanged. The existing RimWorld 1.6 assembly is
9,216 bytes (SHA-256
`0d944eb47a1e648acdbc13ca75948e075e5ccfd71fefe1d4196c54d5a6c5be03`);
the generic chair adapter source is unchanged. Versioned binaries load via `LoadFolders.xml`; no root `Assemblies` binary is shipped. With the previous 8,704-byte
DLL (SHA-256 `2fa6ab6429bd9f81d48633bea0998b47c7255fbb8658b73addc697d8ddf24010`),
[Vanilla run #39005539686](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005539686)
proved free Stool eating/Comfort, table-adjacent seat precedence, and no-seat
standing fallback in one authentic 1.6 game (Pickle 1/1, ERROR0).
[Original MO run #39007512785](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39007512785)
proved table-free eating and Comfort using `DankPyon_RusticStool` (Pickle 1/1,
ERROR0, exit0) **only with two original MO/SYR clips temporarily WAV-converted
in the disposable clone**. The current 9,216-byte DLL was not tested in that
MO run, and original-MP3 ERROR-zero, MO with the latest DLL, save reload, Japanese rendered narrative and dining-room mood remain open.


The adapter patches only `Toils_Ingest.CarryIngestibleToChewSpot`. During a
normal ingest Job it uses Vanilla travel if a valid table-adjacent chair exists,
otherwise attempts a reservable `isSittable` chair without requiring an
adjacent `SurfaceType.Eat`. A table without a usable adjacent chair does not
block table-free seating. If none exists, original travel runs. It uses
Vanilla sit/spot reservations and does not alter room or food mechanics.

Build against **licensed RimWorld 1.6** managed assemblies and Harmony, without
committing upstream binaries:

```bash
dotnet build Source/AMJ.Culture/AMJ.Culture.csproj -c Release \
  -p:RimWorldManagedDir=/path/to/RimWorldLinux_Data/Managed \
  -p:HarmonyDllPath=/path/to/Harmony/Assemblies/0Harmony.dll \
  -p:CultureOutputDir=/path/to/isolated/Culture/Assemblies
```

The current DLL is staged and `About/About.xml` declares the Harmony
requirement. The focused original RimWorld 1.6 chair/Comfort, table-precedence
and standing-fallback acceptance has completed with zero ERRORs. Other furniture
and dining-room mood regression remain unverified and should be checked only
when an actual compatibility or release requirement calls for them. The prior
1.7 Stool experiment is **not** a production 1.7 PASS.

Enza textures, construction balance and new furniture are separate work.
A focused original-game Vanilla 1.6 acceptance is complete. Do not claim MO, 1.7 or unrelated Backstory functionality was covered by the chair test, and do not publish without applicable release gates.
