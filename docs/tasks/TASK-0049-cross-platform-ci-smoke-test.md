# TASK-0049: Cross-Platform CI Smoke Test

## Status
Completed.

## Purpose
Add a GitHub Actions smoke test workflow that verifies the published `AgentContextKit` `0.1.0-alpha.1` NuGet global tool on Windows, Ubuntu, and macOS, then document alpha.2 preparation without publishing a new package, creating a new tag, or pushing changes.

## Scope
- Add `.github/workflows/cross-platform-smoke.yml`.
- Install .NET 10 SDK in the workflow.
- Install `AgentContextKit` version `0.1.0-alpha.1` as a NuGet global tool.
- Add platform-specific `.dotnet/tools` PATH handling for Windows and Linux/macOS.
- Run a clean demo app smoke test on `windows-latest`, `ubuntu-latest`, and `macos-latest`.
- Verify redact-check returns exit code `2` for a fake secret, then remove the fake secret and confirm the final scan has no risk findings.
- Document cross-platform smoke coverage and alpha.2 preparation.
- Keep Turkish ASCII fallback localization polish as a v0.2 backlog item, not an alpha.1 blocker.
- Run local restore, build, test, scan, doctor, maintainer identity scan, and tracked artifact scan.

## Affected Files
- `docs/tasks/TASK-0049-cross-platform-ci-smoke-test.md`
- `.github/workflows/cross-platform-smoke.yml`
- `README.md`
- `README.tr.md`
- `docs/OSS_READINESS.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/ROADMAP.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- Related release/project map docs if needed for task tracking consistency.

## DB Impact
None. This project has no database or migrations.

## Admin Impact
None. There is no admin UI.

## Permission Impact
None. No authentication or authorization behavior changes.

## SEO/i18n Impact
README and README.tr gain cross-platform CI smoke test notes. Turkish CLI ASCII fallback polish remains a future localization backlog item.

## Audit/Security Impact
Adds CI coverage for installed NuGet tool behavior across Windows, Ubuntu, and macOS, including fake secret detection and no remote LLM provider call during `context-export`.

## Acceptance Criteria
- Cross-platform smoke workflow exists and uses a Windows, Ubuntu, and macOS matrix.
- Workflow installs .NET 10 SDK.
- Workflow installs `AgentContextKit` version `0.1.0-alpha.1` as a global tool.
- Workflow adds the global tool path correctly on Windows and Linux/macOS.
- Workflow runs version/help, demo app creation, git init, init, scan, generate, task, report, webui, prompt-pack, and context-export on every OS.
- Workflow verifies fake secret detection returns exit code `2`.
- Workflow deletes the fake secret before final `ackit scan --ci`.
- Release docs record cross-platform CI smoke coverage and alpha.2 preparation.
- No push, tag, NuGet publish, or remote creation is performed.
- Local restore, build, tests, scan, doctor, identity scan, and tracked artifact scan pass.

## Tests
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- Run the prohibited maintainer identity term scan with `rg` and safe excludes.
- `git ls-files | rg -n "\.(rar|zip|nupkg|snupkg|bak|tmp|binlog|trx)$|(^|/)(bin|obj|TestResults|coverage|publish|out)/"`

## Risks
- The new workflow can only be fully validated by GitHub Actions after push; local validation checks repository syntax and release docs but does not execute the hosted OS matrix.
- NuGet availability or GitHub-hosted runner images can have transient external failures.
- Turkish CLI ASCII fallback wording remains non-blocking and tracked for v0.2 localization polish.

## Rollback
- Revert this documentation and workflow commit.
- Do not unpublish NuGet, move the release tag, create a new tag, or push from this task.

## Completion Notes
Completed.

- Added `.github/workflows/cross-platform-smoke.yml` with a `windows-latest`, `ubuntu-latest`, and `macos-latest` matrix.
- Workflow installs .NET 10, installs `AgentContextKit` version `0.1.0-alpha.1` as a NuGet global tool, and adds the correct platform-specific `.dotnet/tools` path.
- Workflow creates a clean demo app, initializes git, and runs version/help, init, scan, generate, task, report, webui, prompt-pack, and context-export.
- Workflow writes the fake secret with string concatenation so the repository source does not contain a full token-like value.
- Workflow expects `redact-check` exit code `2`, removes `.env.test`, and finishes with `ackit scan --ci`.
- Updated README, README.tr, release validation, release checklist, OSS readiness, maintainer handoff, roadmap, changelog, project map, and Codex handoff/context docs.
- Documented that this is alpha.2 preparation only and does not create a tag or publish a package.
- Hosted Windows/Ubuntu/macOS execution is pending until the workflow commit is pushed to GitHub.
- `dotnet restore AgentContextKit.sln`: passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore`: passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build`: passed, 59/59 tests.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`: passed with no risk findings.
- `dotnet run --project src/AgentContextKit.Cli -- doctor`: passed.
- Prohibited maintainer identity term scan returned no matches.
- Tracked artifact scan returned no matches.
- `git diff --check`: passed.
- Source scan for full token-like fake secrets and exact local Windows paths returned no matches.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`: passed with the expected dirty working tree warning before commit.
