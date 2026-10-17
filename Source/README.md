# Culture chair-only dining adapter

**Implementation state:** Culture has version-selected 1.6 and 1.7 assemblies. The 1.7 DLL (SHA-256 `96a55e102c6ab0155f4c5336466afe6d398dbcefd915486bd31bd957cdf84e58`) was built from original RimWorld 1.7.4977 in [Project #39015931527](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39015931527); native gameplay acceptance is pending. The existing RimWorld 1.6 assembly is
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
MO run, and original-MP3 ERROR-zero, 1.7 and dining-room mood remain open.


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
