# AgentContextKit Agent Rules

## Default Workflow
- Read `README.md`, `docs/PRODUCT_SPEC.md`, `docs/ARCHITECTURE.md`, and the active task before changing code.
- Do not code before a task file exists under `docs/tasks/`.
- Continuous progress hard rule: when the user says to continue, do not ask whether to continue; proceed through the next documented task in order with task docs, implementation, verification, and commit.
- Update `.codex/SESSION_HANDOFF.md` after major steps.
- Run relevant tests before reporting completion.
- Prefer safe, minimal, production-ready changes.

## Safety
- Keep the MVP offline-first and local-only.
- Do not upload repository content.
- Do not add telemetry, LLM calls, or remote services without a documented decision.
- Do not overwrite existing user files by default.
- Do not delete files or run destructive git commands unless explicitly requested.
- Treat secret/PII/brand leakage as a release-blocking concern.

## Code Standards
- Public APIs, classes, and methods use English names.
- Keep CLI parsing/output separate from Core business logic.
- Keep IO, scanning, rendering, reporting, and config responsibilities separate.
- Use nullable reference types.
- Keep dependencies minimal.
- Add focused tests for behavior changes.

## Git Discipline
- Check `git status` before edits.
- Never push, publish, force-push, or create remotes from an agent session.
- Use small logical commits when practical.
- Do not commit `.env`, dumps, uploads, `bin/`, `obj/`, `node_modules`, backups, or generated junk.

## Repository Health
- README: yes
- LICENSE: yes
- SECURITY: yes
- Tests: yes
- CI: yes
- Agent instructions: yes

## Stack
- .NET: .sln/.slnx/*proj/Program.cs
- .NET CLI / .NET Tool: PackAsTool/ToolCommandName
- GitHub Actions: .github/workflows

## Release Status
- Current release: `v0.2.0-alpha.1` published on GitHub and NuGet as a pre-release.
- Previous release: `v0.1.0-alpha.2` published and verified; pushed, released, and published.
- NuGet global tool install verification: completed.
- GitHub Release page: completed.
- Published-package smoke workflow installs `AgentContextKit` `0.2.0-alpha.1`.
- Source-package smoke workflow installs the local `AgentContextKit` `0.2.0-alpha.1` package.

## Risk Summary
- No risk findings in the latest local scan.

## Recommended Checks
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- doctor`
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Handoff
If context is low or work pauses, update:
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- The active task file completion notes
