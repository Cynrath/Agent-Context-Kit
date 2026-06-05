# TASK-0059 - Scanner SARIF Output, GitHub Code Scanning Readiness, And CI Integration Examples

## Purpose
Add privacy-first SARIF 2.1.0 output for AgentContextKit scanner findings so maintainers can produce standard security/reporting artifacts for future GitHub Code Scanning and CI review workflows.

## Scope
- Add a new `ackit sarif --output <repo-relative.sarif> [--lang en|tr] [--json]` command.
- Run the existing repository scan and risk scanner for SARIF generation.
- Generate SARIF 2.1.0 JSON with stable tool metadata, rule IDs, severity mapping, and file-level locations.
- Keep `ackit scan --json` behavior unchanged.
- Keep SARIF output path repository-relative and privacy-first.
- Add tests for empty findings, fake secret masking, path privacy, severity mapping, and CLI smoke.
- Add SARIF docs and a non-active GitHub Actions upload example under `docs/examples/`.
- Update README, documentation index, security/release/maintainer docs, roadmap, project map, and Codex handoff files.

## Out Of Scope
- Do not upload SARIF to GitHub Code Scanning.
- Do not add an active `.github/workflows` SARIF upload workflow.
- Do not push, tag, create a GitHub Release, publish NuGet, change repository settings, change branch protection, or create labels.
- Do not add external dependencies unless unavoidable.
- Do not expose absolute local paths, user profile paths, raw secrets, or private repository content in SARIF output.

## Affected Files
- `src/AgentContextKit.Core/Abstractions.cs`
- `src/AgentContextKit.Core/Models.cs`
- `src/AgentContextKit.Core/Scanning.cs`
- `src/AgentContextKit.Core/Generation.cs` or a new Core SARIF file
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `README.md`
- `README.tr.md`
- `docs/SARIF_OUTPUT.md`
- `docs/examples/github-actions-sarif-upload.yml`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/SECURITY_MODEL.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `docs/MAINTAINER_GUIDE.md`
- `docs/ISSUE_TRIAGE.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## CLI Impact
- New command:

```text
ackit sarif --output <repo-relative.sarif> [--lang en|tr] [--json]
```

- Preferred default behavior decision: require `--output` for explicitness in the first SARIF MVP.
- Recommended local output path: `.ackit/reports/ackit.sarif`.
- Human output should be concise and include created path, finding count, and critical/high count.
- JSON command output should summarize the command result and output path without printing the SARIF payload to stdout.

## Security / Privacy Impact
- SARIF artifact locations must use repository-relative paths with `/` separators.
- SARIF must not contain absolute drive paths such as Windows drive roots or user folders.
- SARIF messages must not include raw secret matches.
- Secret-like findings must map to a stable rule ID while keeping sensitive values masked or omitted.
- Local `.ackit/` output remains ignored and local-only.

## CI Impact
- Add an example SARIF upload workflow under `docs/examples/` only.
- Do not activate GitHub Code Scanning upload in `.github/workflows`.
- Example workflow must document `security-events: write` permission for upload.

## Acceptance Criteria
- `ackit sarif --output .ackit/reports/ackit.sarif` creates parseable SARIF 2.1.0 JSON.
- SARIF contains `version`, `$schema`, `runs[0].tool.driver.name`, `informationUri`, `version`, `rules`, and `results`.
- Critical/High map to `error`, Medium maps to `warning`, and Low/Info map to `note`.
- Stable rule IDs are used:
  - `ACKIT001`: SecretLike
  - `ACKIT002`: PiiOrBrandLike
  - `ACKIT003`: GeneratedOrBuildArtifact
  - `ACKIT004`: LocalPathOrPrivateLocation
  - `ACKIT005`: RepositoryHygiene
  - `ACKIT999`: GeneralFinding
- SARIF paths are repository-relative and normalized to `/`.
- SARIF output does not include raw fake secret values or absolute local paths.
- Existing scan, doctor, JSON output, report, Web UI, prompt-pack, and context-export behavior remains intact.

## Test Steps
- `git status --short`
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`
- `dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0059.sarif`
- `Test-Path .ackit/reports/task-0059.sarif`
- `Get-Content .ackit/reports/task-0059.sarif | ConvertFrom-Json`
- `ackit version`
- `ackit --help`
- `git diff --check`
- Maintainer identity scan
- Tracked artifact scan including `*.sarif`
- Exact fake token/local-path scan
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`
- `scripts/check-public-release-gates.ps1 -FailOnIssues`

## Risks
- SARIF schema shape can be too minimal for some consumers if required fields are omitted.
- Scanner messages can accidentally include raw matches if not sanitized.
- Absolute path leakage can occur if repository path normalization is incomplete.
- Active GitHub Code Scanning upload could surprise maintainers if added to workflows; keep upload example in docs only.

## Rollback Plan
- Revert the TASK-0059 commits.
- Remove the `sarif` CLI command and Core SARIF renderer.
- Remove SARIF-specific tests and docs.
- Restore README, documentation index, roadmap, project map, and handoff files to the previous state.

## Completion Notes
- `git fetch origin` completed.
- `git status -sb` showed local `master` aligned with `origin/master`, with only the new TASK-0059 task file untracked at observation time.
- Recent commits:
  - `8d5893b docs: add repository settings and label guidance`
  - `c0f1eb2 docs: add GitHub contributor workflow and support docs`
  - `8dac923 docs: verify alpha2 publish and refresh agent context`
  - `a348e5a chore: prepare alpha2 source smoke and version bump`
  - `b1b34c3 docs: add alpha2 release decision task`
- GitHub CLI was available for read-only workflow inspection.
- Latest read-only workflow results for `Cynrath/agent-context-kit`:
  - `ci`: success for `docs: add repository settings and label guidance`.
  - `cross-platform-smoke`: success for `docs: add repository settings and label guidance`.
  - `cross-platform-source-smoke`: success for `docs: add repository settings and label guidance`.
- Implemented `ISarifReportWriter` and `SarifReportWriter` with SARIF 2.1.0 output, stable `ACKIT` rules, severity mapping, repository-relative artifact URIs, and omitted raw match values.
- Added `ackit sarif --output <repo-relative.sarif> [--lang en|tr] [--json]`.
- Added focused tests for empty SARIF reports, fake secret omission, path privacy, severity mapping, and CLI JSON smoke.
- Added `docs/SARIF_OUTPUT.md` and `docs/examples/github-actions-sarif-upload.yml` as a non-active GitHub Code Scanning upload example.
- Updated README, CLI docs, JSON/exit-code docs, security model, release validation, maintainer guide, issue triage, roadmap, project map, and Codex handoff files.
- `dotnet restore AgentContextKit.sln` passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed: 72/72.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci` passed with no risk findings.
- `dotnet run --project src/AgentContextKit.Cli -- doctor` passed.
- `dotnet run --project src/AgentContextKit.Cli -- scan --json` passed and reported `riskSummary.total = 0`.
- `dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0059.sarif` created a local ignored SARIF report with 0 findings.
- `Get-Content .ackit/reports/task-0059.sarif | ConvertFrom-Json` parsed successfully with SARIF `2.1.0`, schema `https://json.schemastore.org/sarif-2.1.0.json`, driver `AgentContextKit`, and tool version `0.1.0-alpha.2`.
- The generated SARIF report was checked for exact local path and token markers; no matches were found.
- Published global tool checks passed: `ackit version` returned `AgentContextKit 0.1.0-alpha.2`, and `ackit --help` ran. The published package does not include the new local TASK-0059 `sarif` command until a future package publication.
- `git diff --check` passed.
- Maintainer identity scan returned no matches.
- Tracked artifact scan, including `*.sarif`, returned no matches.
- Exact fake token/local path scan returned no matches.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` passed with only the expected dirty working tree warning.
- `scripts/check-public-release-gates.ps1 -FailOnIssues` failed before commit only because the working tree had uncommitted changes; package metadata was clean and the post-release `HEAD` warning is expected for documentation/source sync commits after `v0.1.0-alpha.2`.
- Post-commit `scripts/check-public-release-gates.ps1 -FailOnIssues` passed with no blocking items. The expected warning remains that current `HEAD` is after `v0.1.0-alpha.2` and remote tag verification is manual.
- No push, tag, NuGet publish, GitHub Release, GitHub Code Scanning upload, repository settings change, label change, or branch protection change was performed.
