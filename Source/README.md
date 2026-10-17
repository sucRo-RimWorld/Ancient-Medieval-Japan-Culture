# AMJ Culture C# — source integration

The XML seat Def alone does not make a pawn choose an enza for a table-free meal. This project adds the narrowly scoped ordinary Ingest Job travel Toil adapter.

Build only against the legally owned RimWorld **1.6** `Assembly-CSharp.dll` and the compatible Harmony `0Harmony.dll`; these third-party binaries are **not** committed. Example:

```bash
dotnet build Source/AMJ.Culture/AMJ.Culture.csproj -c Release \
  -p:RimWorldManagedDir=/path/to/RimWorldLinux_Data/Managed \
  -p:HarmonyDllPath=/path/to/Harmony/Assemblies/0Harmony.dll \
  -p:CultureOutputDir=/path/to/isolated/Culture/Assemblies
```

The 1.6 `Assemblies/AMJ.Culture.dll` was compiled against the isolated owned 1.6 Managed assemblies from Culture source commit `f374022c74c079f2f0985e516ad1cce6b25ce939` (Project Actions #38989898749). Artifact DLL SHA-256 `345aa69bfed2a634410382f2a24ddc804953445c018b6ec9b2ee49d73df62b7b`. The production source and binary are on this feature branch but **actual loaded-game meal/Comfort/room acceptance and final art acceptance are still open**; do not publish or claim runtime PASS. The stored circle texture is a **provisional** source graphic, not accepted final art; see owner `Docs/Design.md`. Do not rerun the retired one-Pawn stool test fixture.
