# Culture chair-only dining adapter

**Implementation state:** scoped 1.6 C# source candidate. This branch does not
contain an assembled runtime DLL and does not claim any real-game result.
`About/About.xml` remains unchanged until a matching, compiled DLL is staged.

The adapter patches only `Toils_Ingest.CarryIngestibleToChewSpot`. During a
normal ingest Job it uses Vanilla travel if a reachable eating table exists,
otherwise attempts a reservable `isSittable` chair without requiring an
adjacent `SurfaceType.Eat`. If none exists, original travel runs. It uses
Vanilla sit/spot reservations and does not alter room or food mechanics.

Build against **licensed RimWorld 1.6** managed assemblies and Harmony, without
committing upstream binaries:

```bash
dotnet build Source/AMJ.Culture/AMJ.Culture.csproj -c Release \
  -p:RimWorldManagedDir=/path/to/RimWorldLinux_Data/Managed \
  -p:HarmonyDllPath=/path/to/Harmony/Assemblies/0Harmony.dll \
  -p:CultureOutputDir=/path/to/isolated/Culture/Assemblies
```

On successful compile, stage the matching `Assemblies/AMJ.Culture.dll`, add
Harmony to `About/About.xml`, and only then run one focused real-game
acceptance: chair-only ingest/Comfort, table precedence, normal fallback,
mood/room effects, and complete strict ERROR-free log. The prior 1.7
stool experiment is established technical evidence, **not** a production PASS.

Enza textures, construction balance and new furniture are separate work.
Do not ship a source-only branch as a functional Mod.
