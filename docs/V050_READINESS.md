# v0.5 Readiness

AgentContextKit v0.5 local readiness is tracked separately from public release approval and separately from any future live LLM provider approval.

This page consolidates the local review after these completed work areas:
- Optional LLM integration architecture with explicit consent gates.
- Provider-neutral `ILLMProvider` abstraction.
- Local-only dry-run prompt pack generation with `ackit prompt-pack`.
- Local-only context export approval manifests with `ackit context-export`.

## Local Readiness Review
Run the v0.5 readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1
```

Run it as a failing gate for missing v0.5 assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues
```

`-FailOnIssues` fails only for missing v0.5 readiness assets. It reports public-release blockers separately because those require maintainer-only decisions.

## Safety Boundary
v0.5 readiness does not approve live LLM use. The current product state remains:
- No LLM API calls.
- No provider SDK dependency.
- No API key read, storage, generation, or validation.
- No repository content upload.
- Prompt packs and context export manifests are local-only generated artifacts.

## Expected Public Blockers
Public release remains blocked until:
- `RepositoryUrl` is replaced with the real public repository URL.
- `PackageProjectUrl` is replaced with the real public project URL.
- A release tag is created after explicit maintainer approval.
- Push and NuGet publish are explicitly approved.

## Required Validation
Use these commands before treating v0.5 local readiness as reviewed:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- prompt-pack --output .ackit/prompt-packs/v050-readiness.md --json
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- context-export --prompt-pack .ackit/prompt-packs/v050-readiness.md --approve --output .ackit/context-exports/v050-readiness.json --json
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

Use failing public gates only after maintainer-only public release blockers are resolved:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

## Next Roadmap Step
After v0.5 local readiness is consolidated, the next product line is v1.0 stabilization:
- Stable CLI and generated file conventions.
- Stable config format.
- Complete documentation and validation gates.
- Green CI and release handoff review.
