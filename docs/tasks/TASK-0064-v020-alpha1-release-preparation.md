# TASK-0064 v0.2.0-alpha.1 Release Preparation

## Purpose
Prepare AgentContextKit source and package metadata locally for the next alpha package candidate, `0.2.0-alpha.1`, without publishing, tagging, pushing, creating a GitHub Release, or uploading SARIF.

## Release Decision
Next package candidate: `0.2.0-alpha.1`.

Rationale:
- `ackit sarif` is a new CLI command after the published `0.1.0-alpha.2` package.
- SARIF 2.1.0 output is a new package-visible feature.
- Scanner rule catalog and stable `ACKIT` rule IDs are a new feature set.
- Config allowlist fields add a new scanner configuration surface.
- JSON output adds an additive `ruleId` field.
- Expanded scanner patterns, sample gallery, demo scenarios, Web UI preview docs, and visual asset guidance make this a better minor alpha than another `0.1.0` alpha.

The published `0.1.0-alpha.2` release remains the current public NuGet install version until `0.2.0-alpha.1` is explicitly published later by a maintainer.

## Scope
- Bump source/package candidate version to `0.2.0-alpha.1`.
- Update package release notes.
- Update source version output and tests.
- Update release docs to distinguish published `0.1.0-alpha.2` from source candidate `0.2.0-alpha.1`.
- Validate local package install from a temporary package source.
- Validate source build, tests, scan, doctor, SARIF, sample smoke, hygiene scans, and release gates.

## Out Of Scope
- No push.
- No tag.
- No NuGet publish.
- No GitHub Release creation.
- No GitHub Code Scanning upload.
- No committed generated `.ackit/` output, SARIF output, local HTML report/Web UI output, package files, archives, or temp tool output.

## Affected Files
- `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- Release gate scripts with hardcoded source/current version expectations, if present.
- README and release/package/docs files that mention current published version, source candidate version, or SARIF availability.
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## Versioning Impact
Source and local package candidate version become `0.2.0-alpha.1`. Published install commands remain `0.1.0-alpha.2` until the maintainer publishes the new package.

## Package Impact
Local `dotnet pack` should produce `AgentContextKit.0.2.0-alpha.1.nupkg` in a temporary package directory only. The package artifact must not be committed.

## SARIF Impact
`ackit sarif` becomes part of the `0.2.0-alpha.1` package candidate surface and must be validated from the local temporary tool install.

## Scanner/Config Impact
Scanner rule catalog, stable rule IDs, additive JSON `ruleId`, config allowlist fields, and expanded scanner patterns are documented as part of the `0.2.0-alpha.1` candidate.

## Documentation Impact
Docs must consistently state:
- Published NuGet package: `0.1.0-alpha.2`.
- Current source / next package candidate: `0.2.0-alpha.1`.
- `ackit sarif`: current source and `0.2.0-alpha.1` candidate feature.
- Future install commands for `0.2.0-alpha.1` are only valid after publication.

## Acceptance Criteria
- Task file exists before implementation changes.
- Source/package version reports `0.2.0-alpha.1`.
- Tests expect `0.2.0-alpha.1` where source/package version is asserted.
- Package release notes mention SARIF output, scanner rule catalog, configurable allowlist, expanded scanner rules, additive JSON `ruleId`, sample gallery, and Web UI preview documentation.
- Local temporary package install verifies `ackit version`, `ackit --help`, and `ackit sarif`.
- Build, tests, scan, doctor, SARIF parse, sample smoke, hygiene checks, and release/documentation gates pass or are documented with expected post-release warnings.

## Test Steps
```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release -o <temp-package-dir>
dotnet tool install AgentContextKit --tool-path <temp-tool-dir> --add-source <temp-package-dir> --version 0.2.0-alpha.1 --ignore-failed-sources
<temp-tool-dir>\ackit version
<temp-tool-dir>\ackit --help
<temp-tool-dir>\ackit sarif --output .ackit/reports/task-0064-local.sarif
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- doctor
dotnet run --project src/AgentContextKit.Cli -- scan --json
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0064.sarif
powershell -ExecutionPolicy Bypass -File scripts/test-samples.ps1 -NoBuild
git diff --check
```

## Risks
- Updating README install commands too early could tell users to install an unpublished package.
- Hardcoded script/test versions can drift from package metadata.
- Local package artifacts can accidentally appear in the worktree if temp paths are inside the repository.
- SARIF/local report/Web UI artifacts can include local context if committed.

## Rollback Plan
Revert the TASK-0064 commit to restore the previous source candidate state and docs. Delete any temporary package/tool directories outside the repository if needed.

## Completion Notes
- Started from clean `master` aligned with `origin/master` after fetch.
- Read-only GitHub CLI checks reported latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs as successful for commit `6d38f11`.
- Release decision recorded: prepare `0.2.0-alpha.1` locally as the next package candidate.
- Source/package metadata, CLI runtime version, package metadata gate expectation, tests, source smoke workflow, docs, and agent instruction files were updated for the `0.2.0-alpha.1` candidate.
- Published install commands remain pinned to `0.1.0-alpha.2` until a maintainer publishes `0.2.0-alpha.1`.
- Local package validation passed from a temporary package source: `ackit version` returned `AgentContextKit 0.2.0-alpha.1`, `ackit --help` listed `sarif`, `ackit sarif` generated parseable SARIF 2.1.0, DemoApp smoke passed, fake secret `redact-check` returned exit code `2`, and final `scan --ci` passed.
- Source validation passed: restore, Release build, 83/83 tests, `scan --ci`, `doctor`, `scan --json` with `toolVersion` `0.2.0-alpha.1`, SARIF generation/parse, sample smoke, published global `ackit version`/`--help`, and `git diff --check`.
- Hygiene passed: real-name scan, tracked artifact scan, and exact fake-token/local-path scan found no matches.
- Release/documentation gates passed where applicable: config/generated convention gate, v0.2 readiness asset gate, v1.0 documentation release gate, and `scripts/verify-release.ps1`.
- Pre-commit public release gate failed only because the working tree had uncommitted changes. Package metadata gate passed.
- Post-commit public release gate passed with no blocking items; only the expected post-release `HEAD` warning and manual remote tag verification note remained.
