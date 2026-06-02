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
```
