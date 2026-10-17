# Culture chair-only dining adapter

**Implementation state:** RimWorld 1.6 production C# and matching compiled
`Assemblies/AMJ.Culture.dll` are on main. The current production assembly
(8,704 bytes; SHA-256
`2fa6ab6429bd9f81d48633bea0998b47c7255fbb8658b73addc697d8ddf24010`)
includes the unmodified chair adapter plus independently owned Backstory
logic; it was compiled against licensed original RimWorld 1.6 references
in [build-only #39004690614](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39004690614).
[Real-game #39005539686](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005539686)
PASS: in one original 1.6 session the standalone Vanilla Stool meal/Comfort,
table-adjacent stool priority, and no-chair standing fallback all succeeded
(Pickle 1/1, ERROR 0, original files unchanged). MO, dining-room mood
regression and 1.7 gameplay are not verified.


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
