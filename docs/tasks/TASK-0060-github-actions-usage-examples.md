# TASK-0060 - GitHub Actions Usage Examples, SARIF Availability Wording, And CI Docs Polish

## Purpose
Clarify that `ackit sarif` is available in current source after `v0.1.0-alpha.2`, but is not included in the published NuGet `0.1.0-alpha.2` package. Add stronger documentation-only GitHub Actions examples for CI scanning, SARIF, published-tool smoke, and source-package smoke workflows.

## Scope
- Update README and docs wording for published package versus current source command availability.
- Keep `AgentContextKit` `0.1.0-alpha.2` as the published NuGet package.
- Add CI usage documentation for `scan --ci`, `doctor`, SARIF output, smoke tests, privacy, and failure interpretation.
- Add documentation-only workflow examples under `docs/examples/`.
- Update documentation index, project map, roadmap, OSS readiness, release validation, maintainer guide, GitHub repo hygiene, and Codex handoff files.

## Out Of Scope
- Do not add active SARIF upload to `.github/workflows`.
- Do not push, tag, publish NuGet, create GitHub Releases, change repository settings, create labels, or change branch protection.
- Do not change runtime code or package version.
- Do not upload SARIF or repository content.

## Affected Files
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `docs/CLI_REFERENCE.md`
- `docs/CLI_CONTRACT.md`
- `docs/SARIF_OUTPUT.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/MAINTAINER_GUIDE.md`
- `docs/GITHUB_ACTIONS_USAGE.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/OSS_READINESS.md`
- `docs/GITHUB_REPO_HYGIENE.md`
- `docs/examples/github-actions-scan-ci.yml`
- `docs/examples/github-actions-sarif-upload.yml`
- `docs/examples/github-actions-published-tool-smoke.yml`
- `docs/examples/github-actions-source-package-smoke.yml`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## Documentation Impact
- Published package examples must not imply that NuGet `0.1.0-alpha.2` contains `ackit sarif`.
- Source/development examples may use `dotnet run --project src/AgentContextKit.Cli -- sarif --output ...`.
- SARIF upload examples must remain documentation-only and reviewed before activation.

## CI Impact
- Add copy-paste-ready example workflow snippets only under `docs/examples/`.
- Keep real workflows unchanged.
- Clearly distinguish published global tool smoke from source package smoke.

## Security / Privacy Impact
- SARIF docs must emphasize repository-relative paths, omitted raw scanner matches, and local-only output.
- GitHub Code Scanning upload requires explicit maintainer approval and `security-events: write`.
- CI examples must avoid secrets, private repository content, and public artifact upload by default.

## Acceptance Criteria
- README and docs clarify that `ackit sarif` is source/current-branch functionality and will be included in the next alpha package.
- NuGet `0.1.0-alpha.2` quick verification does not list `ackit sarif`.
- Documentation-only workflow examples exist for scan CI, SARIF upload, published-tool smoke, and source-package smoke.
- `docs/GITHUB_ACTIONS_USAGE.md` covers CI purpose, command order, privacy, failure interpretation, SARIF upload guidance, and smoke strategies.
- Real `.github/workflows` files are not changed to upload SARIF.
- Local validation and hygiene checks pass.

## Test Steps
- `git status --short`
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`
- `dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0060.sarif`
- `Test-Path .ackit/reports/task-0060.sarif`
- `Get-Content .ackit/reports/task-0060.sarif | ConvertFrom-Json`
- `ackit version`
- `ackit --help`
- `git diff --check`
- Maintainer identity scan.
- Tracked artifact scan including `*.sarif`.
- Exact fake token/local path scan.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`
- `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues`

## Risks
- Docs can accidentally suggest a command exists in the published NuGet package before it does.
- SARIF upload examples can be copied into real workflows without privacy review.
- Example workflows can drift from real current workflows if not documented as examples only.

## Rollback Plan
- Revert the TASK-0060 commit.
- Remove newly added example workflow docs and `docs/GITHUB_ACTIONS_USAGE.md`.
- Restore README, SARIF, release validation, maintainer, roadmap, project map, OSS readiness, and Codex handoff wording to the TASK-0059 state.

## Completion Notes
- `git fetch origin` completed.
- `git status -sb` showed local `master` aligned with `origin/master`.
- Recent commits:
  - `aaaad5f feat: add SARIF report output`
  - `8d5893b docs: add repository settings and label guidance`
  - `c0f1eb2 docs: add GitHub contributor workflow and support docs`
  - `8dac923 docs: verify alpha2 publish and refresh agent context`
  - `a348e5a chore: prepare alpha2 source smoke and version bump`
- GitHub CLI was available for read-only workflow inspection.
- Read-only GitHub Actions results for `Cynrath/agent-context-kit` showed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs succeeded after `feat: add SARIF report output`.
- Latest workflow-dispatch reruns for `cross-platform-smoke` and `cross-platform-source-smoke` also succeeded.
- Clarified README, Turkish README, CLI docs, SARIF docs, release validation, maintainer guide, OSS readiness, roadmap, project map, GitHub repo hygiene, changelog, product spec, and Codex handoff files so `ackit sarif` is source/current-branch functionality after `v0.1.0-alpha.2` and not part of the published NuGet `0.1.0-alpha.2` global tool.
- Added `docs/GITHUB_ACTIONS_USAGE.md`.
- Added documentation-only workflow examples:
  - `docs/examples/github-actions-scan-ci.yml`
  - `docs/examples/github-actions-sarif-upload.yml`
  - `docs/examples/github-actions-published-tool-smoke.yml`
  - `docs/examples/github-actions-source-package-smoke.yml`
- `git status --short` showed only expected TASK-0060 documentation changes before commit.
- `dotnet restore AgentContextKit.sln` passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed: 72/72.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci` passed with no risk findings.
- `dotnet run --project src/AgentContextKit.Cli -- doctor` passed.
- `dotnet run --project src/AgentContextKit.Cli -- scan --json` passed and reported `riskSummary.total = 0`.
- `dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0060.sarif` created a local ignored SARIF report with 0 findings.
- `Get-Content .ackit/reports/task-0060.sarif | ConvertFrom-Json` parsed successfully with SARIF `2.1.0`, schema `https://json.schemastore.org/sarif-2.1.0.json`, driver `AgentContextKit`, and tool version `0.1.0-alpha.2`.
- Published global tool checks passed: `ackit version` returned `AgentContextKit 0.1.0-alpha.2`, and `ackit --help` ran without `sarif` in the command list, which is expected for the published package.
- `git diff --check` passed.
- Maintainer identity scan returned no matches.
- Tracked artifact scan, including `*.sarif`, returned no matches.
- Exact fake token/local path scan returned no matches.
- The generated TASK-0060 SARIF report was checked for exact local path and token markers; no matches were found.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` passed with only the expected dirty working tree warning.
- `scripts/check-public-release-gates.ps1 -FailOnIssues` failed before commit only because the working tree had uncommitted changes; package metadata was clean and the post-release `HEAD` warning is expected for documentation/source sync commits after `v0.1.0-alpha.2`.
- Post-commit `scripts/check-public-release-gates.ps1 -FailOnIssues` passed with no blocking items. The expected warning remains that current `HEAD` is after `v0.1.0-alpha.2` and remote tag verification is manual.
- No push, tag, NuGet publish, GitHub Release, GitHub Code Scanning upload, repository settings change, label change, or branch protection change was performed.
