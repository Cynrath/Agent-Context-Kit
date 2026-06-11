# TASK-0065 v0.2.0-alpha.1 Publish Verification And Docs Sync

## Purpose
Sync repository documentation after `v0.2.0-alpha.1` was pushed, released as a GitHub pre-release, published to NuGet, and verified as a global tool.

## Published Release Facts
- Git tag `v0.2.0-alpha.1` is pushed.
- GitHub Release `v0.2.0-alpha.1` exists and is marked pre-release.
- NuGet package `AgentContextKit` version `0.2.0-alpha.1` is published.
- Global tool install is verified.
- `ackit version` returns `AgentContextKit 0.2.0-alpha.1`.
- `ackit --help` includes `sarif`.
- Published package now includes `ackit sarif`.
- Previous published package: `0.1.0-alpha.2`.

## Scope
- Update README install commands to `0.2.0-alpha.1`.
- Update active docs from candidate/unreleased language to published/verified language.
- Update release validation, packaging, maintainer handoff, changelog, roadmap, support, SARIF, scanner/config, and agent instruction files.
- Update published-package smoke workflow to install `0.2.0-alpha.1`.
- Validate global installed `ackit sarif`, source commands, build/test, sample smoke, hygiene scans, and release gates.

## Out Of Scope
- No push.
- No tag creation.
- No NuGet publish.
- No GitHub Release creation or edit.
- No GitHub Code Scanning upload.
- No committed generated `.ackit/`, SARIF, local HTML report/Web UI, package, archive, or release helper artifacts.

## Affected Files
- `README.md`
- `README.tr.md`
- `.github/workflows/cross-platform-smoke.yml`
- `.github/workflows/cross-platform-source-smoke.yml`
- `CHANGELOG.md`
- Release, packaging, support, SARIF, config, scanner, and maintainer docs.
- Agent instruction files.
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## Versioning Impact
Current published release becomes `v0.2.0-alpha.1`. `0.1.0-alpha.2` remains historical/previous release only.

## Package Impact
Published NuGet install commands now use:

```powershell
dotnet tool install --global AgentContextKit --version 0.2.0-alpha.1
```

## SARIF Impact
`ackit sarif` is now part of the published NuGet global tool and should be validated from the global install.

## Documentation Impact
Active docs should no longer call `0.2.0-alpha.1` a candidate/unreleased package. Candidate language may remain only in historical task files.

## Acceptance Criteria
- README install commands use `0.2.0-alpha.1`.
- Active docs state `v0.2.0-alpha.1` is published and verified.
- Published package docs state `ackit sarif` is available.
- Published smoke workflow installs `0.2.0-alpha.1`.
- Global installed `ackit sarif` generates parseable SARIF.
- Build, tests, scan, doctor, source SARIF, sample smoke, hygiene checks, and release gates pass or expected warnings are documented.

## Test Steps
```powershell
gh release view v0.2.0-alpha.1 --repo Cynrath/agent-context-kit
dotnet tool list -g
ackit version
ackit --help
ackit sarif --output .ackit/reports/task-0065-global.sarif
Get-Content .ackit/reports/task-0065-global.sarif | ConvertFrom-Json
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- doctor
dotnet run --project src/AgentContextKit.Cli -- scan --json
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0065-source.sarif
Get-Content .ackit/reports/task-0065-source.sarif | ConvertFrom-Json
powershell -ExecutionPolicy Bypass -File scripts/test-samples.ps1 -NoBuild
git diff --check
```

## Risks
- Docs can accidentally keep stale candidate/unreleased wording.
- Published smoke workflow can remain pinned to the previous package.
- Generated SARIF or release helper artifacts can be accidentally staged.

## Rollback Plan
Revert this docs sync commit if the publication evidence is invalidated. Do not delete or rewrite the already published NuGet package or pushed tag from an agent session.

## Completion Notes
- Started from clean `master` aligned with `origin/master`.
- `v0.2.0-alpha.1` tag exists locally after fetch.
- `gh release view` confirmed GitHub Release `v0.2.0-alpha.1` exists and is pre-release.
- Read-only GitHub CLI checks reported latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs as successful for commit `33649c3`.
- README, README.tr, active release docs, packaging docs, SARIF/config/scanner docs, agent instruction files, issue templates, and Codex handoff files were synced to published `0.2.0-alpha.1`.
- Published-package smoke workflow now installs `AgentContextKit` `0.2.0-alpha.1` and validates `ackit sarif` output parsing.
- The tracked generated helper `artifacts/release-notes-v0.2.0-alpha.1.md` was removed from source control, and `artifacts/` was added to `.gitignore`.
- Local ignored `artifacts/nupkg/AgentContextKit.0.2.0-alpha.1.nupkg` was removed after it produced a non-blocking scanner artifact finding.
- Global installed `ackit version` returned `AgentContextKit 0.2.0-alpha.1`; `ackit --help` included `sarif`; global `ackit sarif` generated parseable SARIF.
- `dotnet restore`, Release build, and Release tests passed with 83/83 tests.
- Source `scan --ci`, `doctor`, `scan --json`, source `sarif` generation/parse, and sample smoke passed after generated package cleanup.
- Maintainer identity, tracked artifact, exact fake-token/local-path, and `git diff --check` hygiene checks returned no findings.
- `check-config-generated-conventions.ps1 -FailOnIssues`, `check-v020-readiness.ps1 -FailOnIssues`, `check-v100-documentation-release-gates.ps1 -FailOnIssues`, and `verify-release.ps1` completed successfully; dirty-tree warnings were expected before commit.
- Post-commit `check-public-release-gates.ps1 -FailOnIssues` passed. Expected warning remains that current `HEAD` is after `v0.2.0-alpha.1`, so remote tag verification is manual.
- GitHub Release body still contains some pre-publication wording, but this task does not edit GitHub Releases; maintainer can polish it manually if desired.
