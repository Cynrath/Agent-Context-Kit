# TASK-0001: AgentContextKit Foundation

## Purpose
Create the initial production-grade open source foundation for AgentContextKit, a .NET 10 CLI named `ackit` that helps AI-assisted developers analyze repositories, generate safe agent context files, create task-first workflow docs, and detect secret/PII/brand leakage risks.

## Scope
- Initialize local git repository if missing.
- Create required handoff/task/decision docs before implementation.
- Scaffold .NET 10 solution with CLI, Core, and Tests projects.
- Add OSS readiness documents and baseline project docs.
- Implement MVP CLI command skeletons with useful local behavior.
- Implement Core models, service interfaces, scanning, risk detection, template rendering, task generation, and doctor checks.
- Add English/Turkish localization foundation.
- Add focused unit tests and CI workflow.

## Out of scope
- NuGet publish.
- GitHub remote creation or push.
- Web UI.
- LLM API integration.
- Automatic redaction or destructive cleanup.
- Large dependency-heavy reporting features.

## Affected files
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/**`
- `AgentContextKit.sln`
- `global.json`
- `.gitignore`
- `.editorconfig`
- `src/AgentContextKit.Cli/**`
- `src/AgentContextKit.Core/**`
- `tests/AgentContextKit.Tests/**`
- `.github/workflows/ci.yml`

## Architecture impact
The initial architecture separates CLI argument parsing/output from Core business logic. Core exposes interfaces for scanning, stack detection, risk scanning, config, template rendering, task file generation, agent instruction generation, clock, and filesystem abstraction.

## CLI impact
The `ackit` command will support initial MVP commands:
- `init`
- `scan`
- `generate`
- `task`
- `redact-check`
- `doctor`
- `version` / `--version`
- `help` / `--help`

## Security impact
The first release is offline-first and does not upload code. Secret/PII/brand scanning is report-only. Existing files are skipped by default and are not overwritten.

## Localization impact
English and Turkish are supported through a small localization/template abstraction. Unknown language input falls back to English.

## Testing impact
Add unit tests for stack detection, secret scanning, ignored folders, template fallback, task slug generation, overwrite safety, redact-check severity, config fallback, and doctor missing-file checks.

## OSS/release impact
Create README, README.tr, MIT license, SECURITY, CONTRIBUTING, CODE_OF_CONDUCT, CHANGELOG, roadmap, release checklist, and third-party notices for an OSS-ready `0.1.0-alpha.1` foundation.

## Acceptance criteria
- `AgentContextKit.sln` exists.
- CLI/Core/Tests projects exist.
- `ackit --help` or `dotnet run --project src/AgentContextKit.Cli -- --help` shows command help.
- `ackit scan` via `dotnet run` performs minimal repo analysis.
- `ackit task` creates a task file under `docs/tasks`.
- `ackit redact-check` detects a basic secret-like pattern.
- Existing generated files are not overwritten by default.
- English/Turkish localization foundation exists.
- OSS docs and task/handoff docs exist.
- Build/test results are recorded.

## Test steps
1. `dotnet restore`
2. `dotnet build -c Release`
3. `dotnet test -c Release`
4. `dotnet run --project src/AgentContextKit.Cli -- --help`
5. `dotnet run --project src/AgentContextKit.Cli -- scan`
6. `dotnet run --project src/AgentContextKit.Cli -- doctor`

## Risks
- Installed .NET SDK reports a workload exception during `dotnet --info`; build/test may still work.
- Regex-based scanning can produce false positives and false negatives.
- MVP templates may be intentionally concise and need hardening in later versions.
- Public release readiness still requires manual review before publish.

## Rollback plan
No remote or publish actions are performed. To roll back this task manually, inspect `git status`, remove newly created files only after confirming they are from this task, or reset to a known commit if one exists and the user explicitly requests that destructive action.

## Completion notes
Completed.

- Created the .NET 10 solution and CLI/Core/Tests projects.
- Added OSS readiness docs, product docs, handoff docs, and CI.
- Implemented MVP Core services and CLI commands.
- Added 10 focused xUnit tests.
- Verified restore/build/test and CLI command behavior.
- No remote, push, publish, deletion, or automatic redaction was performed.
