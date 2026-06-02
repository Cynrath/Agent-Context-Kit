# Source Hygiene

This document records repository hygiene rules that should stay true before public release.

## Current Status
- No empty SDK scaffold source files should remain.
- Test source files should use descriptive names.
- Build outputs must stay out of git.
- Temporary release package/tool folders must stay under the user temp directory.
- Public package URL placeholders are tracked separately in [RELEASE_BLOCKERS.md](RELEASE_BLOCKERS.md).

## Source File Rules
- Keep CLI entry and command routing in `src/AgentContextKit.Cli`.
- Keep scanner, generation, configuration, and doctor behavior in `src/AgentContextKit.Core`.
- Keep tests in `tests/AgentContextKit.Tests`.
- Do not keep unused placeholder classes such as default SDK `Class1.cs`.
- Prefer descriptive test file names over generated names such as `UnitTest1.cs`.

## Package Hygiene Rules
- Do not commit `bin/`, `obj/`, `.nupkg`, temporary tool installs, dumps, backups, or upload folders.
- Do not publish while `RepositoryUrl` or `PackageProjectUrl` contain placeholders.
- Do not use permanent global tool installs for release validation.
- Keep `PackageReadmeFile`, license expression, package tags, and release notes reviewed before publish.

## Verification
Use:

```powershell
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

Before any public release, also run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
```
