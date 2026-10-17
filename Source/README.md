# Culture chair-only dining adapter

**Implementation state:** RimWorld 1.6 source and compiled matching binary are
staged. **No actual loaded-game meal result is claimed**. The packaged
`Assemblies/AMJ.Culture.dll` was built against licensed isolated original 1.6
managed assemblies, source `aab06d25fd35c0eecdc3c818e06c90bf6a43fbca`, in the existing manual
[Project build-only run #38995487716](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/38995487716).
DLL SHA-256: `609f8d2b2d4f9bc48f6ee86474f2b0e15c5d7abb2f342bd855dac526250a4dbb`.

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

The matching DLL is staged and `About/About.xml` declares the Harmony
requirement. Before promotion, run one focused real-game acceptance: chair-only ingest/Comfort, table precedence, normal fallback,
mood/room effects, and complete strict ERROR-free log. The prior 1.7
stool experiment is established technical evidence, **not** a production PASS.

Enza textures, construction balance and new furniture are separate work.
Do not claim runtime acceptance or publish before the actual loaded-game check.
