# TASK-0062 - v0.2.0-alpha Scanner Expansion, Configurable Allowlist, And Rule Catalog Hardening

## Purpose
Harden AgentContextKit scanner behavior for the v0.2.0-alpha track by centralizing the risk rule catalog, adding a narrow configurable allowlist foundation, expanding risk detection, and keeping SARIF/JSON behavior stable and privacy-first.

## Scope
- Add a central scanner rule catalog for stable `ACKIT` rule IDs, category, default severity, description, and recommendation text.
- Preserve existing SARIF rule ID mapping:
  - `ACKIT001` SecretLike
  - `ACKIT002` PiiOrBrandLike
  - `ACKIT003` GeneratedOrBuildArtifact
  - `ACKIT004` LocalPathOrPrivateLocation
  - `ACKIT005` RepositoryHygiene
  - `ACKIT999` GeneralFinding
- Make SARIF rule metadata use the central catalog.
- Extend `.ackit/config.yml` parsing with narrow `safeDomains`, `ignoredPaths`, and `ignoredFindingIds` fields.
- Keep Critical secret-like findings unsuppressible through config allowlists by default.
- Expand scanner coverage for private key/certificate indicators, environment/production config, database artifacts, archives/packages, local path leakage, and provider-token-like patterns.
- Add focused tests and update scanner/config/SARIF docs.

## Out Of Scope
- Do not push, tag, publish NuGet, create GitHub Releases, or upload SARIF to GitHub Code Scanning.
- Do not change the published NuGet `0.1.0-alpha.2` behavior.
- Do not commit generated `.ackit/` reports, SARIF files, local Web UI output, packages, archives, `bin/`, `obj/`, or `node_modules`.
- Do not add real secrets, real credentials, production config, private keys, or exact raw token/local path examples to public docs or samples.
- Do not make config suppressions broad enough to hide Critical secret findings silently.

## Affected Files
- `src/AgentContextKit.Core/Models.cs`
- `src/AgentContextKit.Core/Configuration.cs`
- `src/AgentContextKit.Core/Scanning.cs`
- `src/AgentContextKit.Core/Sarif.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/SCANNER_RULES.md`
- `docs/CONFIGURATION.md`
- `docs/SECURITY_MODEL.md`
- `docs/SARIF_OUTPUT.md`
- `docs/JSON_OUTPUT.md`
- `docs/CLI_REFERENCE.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `docs/SAMPLE_GALLERY.md`
- `docs/DEMO_SCENARIOS.md`
- `docs/MAINTAINER_GUIDE.md`
- `docs/DOCUMENTATION_INDEX.md`
- `README.md`
- `README.tr.md`
- `samples/security-fixture-repo/README.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## Security / Privacy Impact
- Critical secret-like findings must remain visible even if a user configures `ignoredFindingIds`.
- Config allowlists are local-only analysis controls; they never redact, delete, upload, publish, or mutate repository content.
- SARIF output must continue to omit raw scanner match values and use repository-relative artifact paths.
- Scanner messages must avoid printing raw secret values in SARIF and documentation.

## Config Impact
- `safeDomains` adds narrow repo-local domain allowlisting for domain-like Low findings.
- `ignoredPaths` adds report-suppression paths for risk findings while existing `ignorePaths` continues to exclude files from scanning/enumeration.
- `ignoredFindingIds` adds non-Critical rule suppression for known accepted findings.
- Unknown fields remain ignored.
- Config absence preserves current behavior.

## SARIF Impact
- SARIF continues to use stable `ACKIT` rule IDs and SARIF 2.1.0.
- SARIF driver rules should come from the central rule catalog.
- SARIF result messages remain safe and do not include raw match values.
- JSON command output remains schema version 2; any additions must be additive only.

## CLI Impact
- Human scan output should remain compatible.
- Existing commands and exit codes should not change.
- Published `0.1.0-alpha.2` still does not include `ackit sarif`; current source does.

## Acceptance Criteria
- Central rule catalog exists and is used by SARIF.
- `safeDomains`, `ignoredPaths`, and `ignoredFindingIds` are parsed from `.ackit/config.yml`.
- Config allowlists can suppress safe domain-like, ignored path, and non-Critical ignored rule findings.
- Critical secret-like findings are not suppressed by config allowlists.
- Expanded scanner rules have focused tests and preserve existing behavior.
- Docs explain rule IDs, severity, SARIF mapping, config allowlist behavior, and Critical suppression boundaries.
- Validation passes without committing generated artifacts or sensitive literal examples.

## Test Steps
- `git status --short`
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`
- `dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0062.sarif`
- `Test-Path .ackit/reports/task-0062.sarif`
- `Get-Content .ackit/reports/task-0062.sarif | ConvertFrom-Json`
- `powershell -ExecutionPolicy Bypass -File scripts/test-samples.ps1 -NoBuild`
- `ackit version`
- `ackit --help`
- `git diff --check`
- Maintainer identity scan.
- Tracked artifact scan including `*.sarif`.
- Exact fake token/local path scan.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`
- `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues`

## Risks
- Config allowlists could create false negatives if they are too broad.
- Expanded token patterns can increase false positives in docs/tests if patterns are not narrowly scoped.
- SARIF rule catalog changes can accidentally break stable rule IDs.
- Docs can accidentally include sensitive exact token/local path literals.

## Rollback Plan
- Revert the TASK-0062 commit.
- Remove new config fields from docs if runtime support is reverted.
- Restore scanner, SARIF, model, and tests to TASK-0061 state.
- Re-run restore/build/test, scan, doctor, sample smoke, and release gates.

## Completion Notes
- `git fetch origin` completed.
- `git status -sb` showed local `master` aligned with `origin/master`.
- Recent commits:
  - `1a6b2f5 docs: add sample gallery and demo scenarios`
  - `9beb2fe docs: add GitHub Actions usage examples`
  - `aaaad5f feat: add SARIF report output`
  - `8d5893b docs: add repository settings and label guidance`
  - `c0f1eb2 docs: add GitHub contributor workflow and support docs`
- GitHub CLI was available for read-only workflow inspection.
- Read-only GitHub Actions results for `Cynrath/agent-context-kit` showed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs succeeded after `docs: add sample gallery and demo scenarios`.
- Implemented central `RiskRuleCatalog` with stable `ACKIT` rule IDs, default severity context, descriptions, and recommendations.
- Updated SARIF rule metadata to use the central rule catalog, including rule help text.
- Added additive JSON `ruleId` on scanner finding objects.
- Added `.ackit/config.yml` fields `safeDomains`, `ignoredPaths`, and `ignoredFindingIds`.
- Added non-Critical suppression behavior for configured safe domains, ignored paths, and ignored finding IDs.
- Critical findings remain reportable even when `ignoredPaths` or `ignoredFindingIds` are configured.
- Expanded scanner coverage for additional package artifacts, AWS access key-like values, GitHub token-like families, bearer token-like values, Unix home paths, and file URI paths.
- Added `docs/SCANNER_RULES.md` and updated scanner/config/SARIF/JSON/CLI/sample docs.
- Initial parallel `dotnet run --project src/AgentContextKit.Cli -- scan --ci` collided with another `dotnet run` build process and hit a temporary Debug build file lock; rerunning the same command sequentially passed.
- `dotnet restore AgentContextKit.sln` passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 83/83 tests.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci` passed with no risk findings and main stacks `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.
- `dotnet run --project src/AgentContextKit.Cli -- doctor` passed.
- `dotnet run --project src/AgentContextKit.Cli -- scan --json` returned `riskSummary.total` 0.
- `dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0062.sarif` created an ignored local SARIF file with 0 findings.
- `Test-Path .ackit/reports/task-0062.sarif` and `ConvertFrom-Json` validation passed; SARIF version was `2.1.0`, driver was `AgentContextKit`, rule count was 6, and result count was 0.
- `scripts/test-samples.ps1 -NoBuild` passed for all sample repositories.
- Published global tool checks passed: `ackit version` returned `AgentContextKit 0.1.0-alpha.2`, and `ackit --help` worked. Published `0.1.0-alpha.2` still does not include source-only `sarif`.
- `git diff --check` passed.
- Maintainer identity scan returned no matches.
- Tracked artifact scan including `*.sarif` returned no matches.
- Exact fake token/local path scan returned no matches.
- Generated local SARIF exact token/local path scan returned no matches.
- New scanner literal self-noise scan returned no matches.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` passed with only the expected dirty working tree warning.
- `scripts/check-config-generated-conventions.ps1 -FailOnIssues` passed with only the expected dirty working tree warning.
- `scripts/check-v020-readiness.ps1 -FailOnIssues` passed with no v0.2 readiness asset issues; it reported the expected post-release `HEAD`/dirty tree notes.
- `scripts/check-public-release-gates.ps1 -FailOnIssues` failed pre-commit only because the working tree had uncommitted changes; package metadata was clean and the post-release `HEAD` warning remains expected.
- After commit, `scripts/check-public-release-gates.ps1 -FailOnIssues` passed with no blocking items; only the expected post-release `HEAD` warning and manual remote tag verification note remained.
- No push, tag, NuGet publish, GitHub Release, GitHub Code Scanning upload, repository settings change, label change, or branch protection change was performed.
