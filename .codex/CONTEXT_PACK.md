# AgentContextKit Context Pack

## Project
AgentContextKit (`ackit`) is an offline-first .NET 10 CLI for AI-assisted repository context generation, task-first workflow docs, and secret/PII/brand leakage risk reporting.

## Architecture
- CLI project: `src/AgentContextKit.Cli`
- Core project: `src/AgentContextKit.Core`
- Tests: `tests/AgentContextKit.Tests`

## Current MVP
- Safe local scanning.
- Agent instruction generation.
- Task file generation.
- Pattern-based redact checks.
- English/Turkish localization foundation.

## Hard Rules
- No remote upload.
- No LLM API in MVP.
- No Web UI in MVP.
- No overwrite by default.
- No automatic redaction.
- No GitHub push or NuGet publish from agent sessions.

## Verification
Use:
```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

## Public Release Blockers
- `RepositoryUrl` and `PackageProjectUrl` are TODO placeholders until the maintainer selects the real public URL.
- `scripts/audit-public-release.ps1 -FailOnIssues` must exit `0` before any public release.
- `scripts/check-release-blockers.ps1 -FailOnBlockers` must exit `0` before any public release.
- `docs/MAINTAINER_RELEASE_HANDOFF.md` contains the maintainer-only public release sequence.

## Source Hygiene
- Empty SDK scaffold file `src/AgentContextKit.Core/Class1.cs` has been removed.
- Tests live in `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`.

## v0.2 Progress
- TASK-0011 completed stack detection expansion with .NET SDK, ASP.NET Core, Razor, Blazor WebAssembly, Worker Service, Minimal API, package manager, TypeScript, and Tailwind CSS signals.
- TASK-0012 completed risk scanner precision improvements for environment samples, private key files, private key blocks, IP filtering, and configured keyword token boundaries.
- TASK-0013 completed JSON schema version 2 with generated timestamp, repository metadata, and summary fields.
