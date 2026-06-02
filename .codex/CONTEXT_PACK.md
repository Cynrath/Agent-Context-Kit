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

## Source Hygiene
- Empty SDK scaffold file `src/AgentContextKit.Core/Class1.cs` has been removed.
- Tests live in `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`.
