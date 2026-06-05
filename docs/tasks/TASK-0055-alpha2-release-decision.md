# TASK-0055 - Alpha.2 Release Decision, Source Smoke Workflow, and Version Bump

## Purpose
Prepare `AgentContextKit` for `v0.1.0-alpha.2` without pushing, tagging, creating a GitHub Release, or publishing a NuGet package.

TASK-0056 supersedes the pre-publication parts of this task after `v0.1.0-alpha.2` was pushed, released on GitHub, published on NuGet, and install-verified.

## Scope
- Bump source/package metadata to `0.1.0-alpha.2`.
- Keep the existing cross-platform published-package smoke workflow pinned to the already published `0.1.0-alpha.1` package.
- Add a separate cross-platform source smoke workflow that packs the current branch and smoke-tests the local package on Windows, Ubuntu, and macOS.
- Sync release, packaging, roadmap, OSS readiness, README, changelog, and Codex handoff docs.
- Run local build, test, scanner, doctor, JSON scan, local package smoke, and hygiene checks.

## Affected Files
- `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `scripts/check-package-metadata.ps1`
- `scripts/check-release-blockers.ps1`
- `.github/workflows/cross-platform-source-smoke.yml`
- `.github/workflows/cross-platform-smoke.yml`
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/OSS_READINESS.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `docs/PACKAGING.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/NUGET_METADATA.md`
- `docs/JSON_OUTPUT.md`
- `docs/HTML_REPORTS.md`
- `docs/WEB_UI_PROTOTYPE.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None. This repository is an offline-first CLI/global tool and has no database schema or migrations.

## Admin Impact
None. There is no admin UI or role management surface.

## Permission Impact
No permission model changes. The new workflow uses read-only repository contents permissions and local temporary package/tool paths.

## SEO / i18n Impact
- README text is clarified for the current published release versus alpha.2 source preparation.
- Turkish CLI polish from previous work remains documented.
- No SEO-facing web surface is added.

## Audit / Security Impact
- No telemetry, upload, remote LLM call, or secret handling is added.
- The source smoke workflow must verify the fake-secret failure path without committing real secrets or exact secret assignment examples.
- Hygiene scans must remain clean for maintainer identity terms, tracked artifacts, and exact token/local-path patterns.

## Acceptance Criteria
- Task documentation exists before implementation.
- `ackit --help` replaces stale `ackit help` examples.
- README install commands remain pinned to the then-current published package until alpha.2 publication. TASK-0056 updates them to `0.1.0-alpha.2`.
- Source metadata and CLI runtime version report `0.1.0-alpha.2`.
- `PackageReleaseNotes` describes alpha.2 scanner noise, allowlist, Node 24 readiness, Turkish CLI output, and cross-platform validation improvements.
- A new `cross-platform-source-smoke` workflow packs and installs the current source package version on Windows, Ubuntu, and macOS.
- Existing `cross-platform-smoke` remained the published-package smoke workflow for `0.1.0-alpha.1` until TASK-0056 updated it to `0.1.0-alpha.2`.
- Changelog and release docs clearly separate published alpha.1 from alpha.2 preparation.
- Local package smoke installs the packed alpha.2 package from a temporary source and `ackit version` reports `AgentContextKit 0.1.0-alpha.2`.
- No push, tag, GitHub Release creation, or NuGet publish is performed.

## Tests
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`
- Local `dotnet pack` and temporary tool-path install smoke for `0.1.0-alpha.2`
- Maintainer identity scan
- Tracked artifact scan
- Exact token/local-path pattern scan
- `git diff --check`

## Validation Results
- `dotnet restore AgentContextKit.sln`: passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore`: passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build`: passed, 67/67 tests.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`: passed with no risk findings.
- `dotnet run --project src/AgentContextKit.Cli -- doctor`: passed.
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`: passed and reported `toolVersion` `0.1.0-alpha.2`.
- Local `dotnet pack`: created `AgentContextKit.0.1.0-alpha.2.nupkg`.
- Temporary tool-path install from the local package source: passed.
- Installed `ackit version`: returned `AgentContextKit 0.1.0-alpha.2`.
- Installed `ackit --help`: passed.
- Local demo app source smoke: passed for init, scan, generate, task, report, Web UI, expected fake-secret failure, fake secret cleanup, and final scan.
- Package metadata gate: passed.
- Documentation/release gate: passed with the expected dirty working tree warning.
- Maintainer identity scan: no matches.
- Tracked artifact scan: no matches.
- Exact token/local-path pattern scan: no matches.
- `git diff --check`: passed with an LF normalization warning for the edited project file.

## Risks
- Source and package versions can drift if `Program.cs`, tests, and project metadata are not updated together.
- The source smoke workflow can accidentally test the published package if local package source/tool-path arguments are wrong.
- Hosted GitHub Actions validation cannot be completed locally; it remains a maintainer post-push check.

## Rollback
- Revert the task commit and implementation commit.
- Remove `.github/workflows/cross-platform-source-smoke.yml`.
- Restore CLI/package version metadata and version assertions to the previous published version.
- Keep published `0.1.0-alpha.1` release artifacts unchanged.

## Implementation Notes
- Source/package metadata and CLI runtime version are prepared as `0.1.0-alpha.2`.
- Existing published-package smoke workflow remained pinned to `0.1.0-alpha.1` until alpha.2 publication verification.
- Added `cross-platform-source-smoke` for current-branch package validation on Windows, Ubuntu, and macOS.
- README quick smoke examples initialize git before running `ackit init` and `ackit --help` is used in command lists.
- Public install examples are updated by TASK-0056 after alpha.2 publication verification.

## Status
- Completed locally.
