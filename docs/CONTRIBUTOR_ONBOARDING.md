# Contributor Onboarding

This guide gets a contributor from clone to validated local change.

## Prerequisites
- Git.
- .NET 10 SDK.
- PowerShell for release and documentation gate scripts.

## Clone And Inspect
```powershell
git clone https://github.com/Cynrath/agent-context-kit.git
cd agent-context-kit
git status --short --branch
```

Read:
- `README.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ARCHITECTURE.md`
- `docs/DEVELOPMENT_STANDARD.md`
- The active task under `docs/tasks/`

## Build And Test
```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
```

## Run The CLI From Source
```powershell
dotnet run --project src/AgentContextKit.Cli -- --help
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- doctor
```

## Task-First Workflow
Before implementation, create or update a task file under `docs/tasks/`.

Each task should record:
- Purpose and scope.
- Affected files.
- Security and audit impact.
- Acceptance criteria.
- Tests.
- Risks and rollback.

Use `docs/NEXT_TASKS.md` and `docs/PROJECT_EXECUTION_QUEUE.md` to find the current local task order. Use `docs/ISSUE_BACKLOG.md` for copy-ready public issue ideas after maintainer review.

## Local Artifact Rules
- `.ackit/` reports, Web UI output, prompt packs, and context exports are local-only.
- Do not commit archives, packages, logs, `bin/`, `obj/`, test results, coverage, or generated junk.
- Do not paste secrets, production config, or private repository content into issues, PRs, docs, or fixtures.

## Before Opening A PR
Run:

```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- doctor
git diff --check
```

If your change affects release docs or public readiness, also run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

## PR Expectations
- Explain the change and the task/issue it addresses.
- List tests run.
- Call out security, localization, generated-file, and release workflow impact.
- Keep public examples redacted and minimal.
