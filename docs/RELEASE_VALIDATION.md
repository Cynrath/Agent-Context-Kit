# Release Validation

This checklist validates local release readiness without publishing.

## PROJECT-CONTROL-0102 Pre-Version Evidence
On 2026-06-13, TASK-0116–0122 validation passed with a zero-warning Release build, 186/186 tests, clean source scan, doctor PASS, sample smoke, JSON/SARIF/locale/link contracts, local package install smoke, and all requested readiness/security/supply-chain gates. The unchanged 2,000-file/30-second performance tripwire completed in 3.961 seconds standalone and 2.785 seconds through the RC gate.

## v0.2.0-alpha.2 Candidate Evidence
On 2026-06-13, TASK-0123 prepared source/package/CLI metadata and source-package smoke as `0.2.0-alpha.2` while leaving published-package smoke and public README install commands on `0.2.0-alpha.1`. Release build completed with zero warnings and zero errors; 186/186 tests passed; source scan was clean; doctor passed; JSON and SARIF parsed; sample, contract, localization, documentation, readiness, security, supply-chain, and release gates passed. Candidate `.nupkg` and `.snupkg` archives were inspected and removed after a full temporary installed-tool smoke. The 2,000-file performance tripwire completed in 3.704 seconds through the RC evidence gate and 3.685 seconds standalone, below the unchanged 30-second threshold.

## Required Commands
```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli -- config-check --json
dotnet run --project src/AgentContextKit.Cli -- scan
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/release-validation.sarif
dotnet run --project src/AgentContextKit.Cli -- report --json
dotnet run --project src/AgentContextKit.Cli -- webui --json
dotnet run --project src/AgentContextKit.Cli -- prompt-pack --output .ackit/prompt-packs/release-validation.md --json
dotnet run --project src/AgentContextKit.Cli -- context-export --prompt-pack .ackit/prompt-packs/release-validation.md --approve --output .ackit/context-exports/release-validation.json --json
dotnet run --project src/AgentContextKit.Cli -- doctor
powershell -ExecutionPolicy Bypass -File scripts/measure-scan-performance.ps1 -FileCount 2000 -MaxSeconds 30 -FailOnThreshold
powershell -ExecutionPolicy Bypass -File scripts/check-release-candidate-evidence.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-candidate-workflow.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-json-contract-assets.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-localization-parity.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/test-local-markdown-links.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-local-markdown-links.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-workflow.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/prepare-release.ps1 -Version <version> -CommitSha <sha> -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/verify-published-package.ps1 -Version <published-version>
powershell -ExecutionPolicy Bypass -File scripts/check-security-supply-chain-evidence.ps1 -RunDependencyReview -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-rc-local-readiness.ps1 -RunDependencyReview -FailOnIssues
```

Hosted RC evidence is manual-only. After a maintainer push, dispatch `.github/workflows/release-candidate-evidence.yml` and record the three OS results as described in `docs/RC_HOSTED_EVIDENCE.md`.

The normative local evidence matrix and dated results are maintained in `docs/RELEASE_CANDIDATE_EVIDENCE.md`.

The conditional local contract freeze and maintainer GO/NO-GO conditions are maintained in `docs/RELEASE_CANDIDATE_CONTRACT_FREEZE.md` and `docs/MAINTAINER_RC_DECISION.md`.

Machine-readable command JSON, baseline, and SARIF profile assets are indexed in `docs/schemas/README.md` and validated by `scripts/check-json-contract-assets.ps1`.

Repository-local Markdown targets are checked without network access by `scripts/check-local-markdown-links.ps1`; its positive/negative smoke cases are in `scripts/test-local-markdown-links.ps1`. External URLs and same-document anchors are intentionally not validated by this local gate.

English/Turkish human output, known argument errors, exit decisions, and JSON semantic invariance are defined in [LOCALIZATION.md](LOCALIZATION.md) and validated by `tests/AgentContextKit.Tests/LocalizationParityTests.cs` plus `scripts/check-localization-parity.ps1`.

Security reporting and supply-chain maintainer evidence fields are defined in [SECURITY_SUPPLY_CHAIN_EVIDENCE.md](SECURITY_SUPPLY_CHAIN_EVIDENCE.md) and [MAINTAINER_SECURITY_SUPPLY_CHAIN_HANDOFF.md](MAINTAINER_SECURITY_SUPPLY_CHAIN_HANDOFF.md), with local structure/dependency review through `scripts/check-security-supply-chain-evidence.ps1`. This gate does not prove remote settings or artifact publication.

The consolidated local RC decision is defined in [RC_LOCAL_READINESS.md](RC_LOCAL_READINESS.md). `scripts/check-rc-local-readiness.ps1` orchestrates the existing local evidence gates and intentionally reports `LOCAL READY / REMOTE NO-GO` while hosted and maintainer-only evidence remains open.

Hosted standard workflow evidence is recorded in [HOSTED_VALIDATION_STATUS.md](HOSTED_VALIDATION_STATUS.md). It proves current CI and source/published package smoke on commit `37d5220`; it does not substitute for the unrun manual RC evidence workflow.

Private vulnerability reporting status is recorded in [PRIVATE_VULNERABILITY_REPORTING_STATUS.md](PRIVATE_VULNERABILITY_REPORTING_STATUS.md). The read-only GitHub endpoint returned disabled on 2026-06-13; enablement and notification ownership remain P0 maintainer actions.

Published package/release supply-chain status is recorded in [PUBLISHED_SUPPLY_CHAIN_STATUS.md](PUBLISHED_SUPPLY_CHAIN_STATUS.md). The exact `0.2.0-alpha.1` package has a valid NuGet.org repository signature but no observed author signature, package/release SBOM, or accessible GitHub package attestation. The NuGet owner profile also differs from the project persona and requires maintainer disposition.

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-published-supply-chain-status.ps1 -FailOnIssues
```

Release-candidate dependency review:

```powershell
dotnet list AgentContextKit.sln package --vulnerable --include-transitive
dotnet list AgentContextKit.sln package --deprecated
```

The 2026-06-12 post-migration review found no vulnerable or deprecated direct/transitive packages. TASK-0091 replaced Legacy `xunit` `2.9.3` with `xunit.v3` `3.2.2`, updated the Visual Studio runner to `3.1.5`, and preserved 169/169 passing tests.

The `sarif` command is available in current source and in the published NuGet `0.2.0-alpha.1` global tool.

## v0.2.0-alpha.1 Published Package Validation
Use temporary directories outside the repository:

```powershell
$pkg = Join-Path $env:TEMP "ackit-v020-alpha1-nupkg"
$tools = Join-Path $env:TEMP "ackit-v020-alpha1-tools"
New-Item -ItemType Directory -Force -Path $pkg,$tools | Out-Null
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release -o $pkg
dotnet tool install AgentContextKit --tool-path $tools --add-source $pkg --version 0.2.0-alpha.1 --ignore-failed-sources
& (Join-Path $tools "ackit.exe") version
& (Join-Path $tools "ackit.exe") --help
& (Join-Path $tools "ackit.exe") sarif --output .ackit/reports/task-0064-local.sarif
Get-Content .ackit/reports/task-0064-local.sarif | ConvertFrom-Json
```

Do not commit the package, temporary tool install, or generated SARIF output.

## Scripted Validation
```powershell
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

The script creates temporary package/tool folders under the user temp directory and leaves them in place for inspection. It also runs `scripts/check-release-blockers.ps1` in report-only mode, so local validation can keep public release follow-up status visible.

## SARIF Output Validation
Generate and parse a local SARIF report:

```powershell
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/release-validation.sarif
Get-Content .ackit/reports/release-validation.sarif | ConvertFrom-Json
```

The output is local-only and ignored by git when written under `.ackit/reports/`. It should use SARIF `2.1.0`, repository-relative artifact URIs, stable `ACKIT` rule IDs, and no raw secret match values.

`docs/examples/github-actions-sarif-upload.yml` shows a non-active GitHub Code Scanning upload example. Do not copy it into `.github/workflows/` until a maintainer has intentionally approved Code Scanning upload and repository permissions.

See [GITHUB_ACTIONS_USAGE.md](GITHUB_ACTIONS_USAGE.md) for CI command ordering, published-tool versus source-package smoke guidance, and SARIF upload criteria.
See [CODE_SCANNING_DECISION.md](CODE_SCANNING_DECISION.md) for the documentation-only default and opt-in criteria.

## Sample Smoke Validation
Run sample smoke checks from the repository root:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/test-samples.ps1 -NoBuild
```

The script scans committed sample repositories from their own directories because the CLI scans the current working directory. It does not generate or commit `.ackit/` output.

## Local Web UI And Report Smoke
Generate local-only preview artifacts under ignored `.ackit/` paths:

```powershell
dotnet run --project src/AgentContextKit.Cli -- report --output .ackit/reports/task-0063-report.html
dotnet run --project src/AgentContextKit.Cli -- webui --output .ackit/webui/task-0063-webui.html
Test-Path .ackit/reports/task-0063-report.html
Test-Path .ackit/webui/task-0063-webui.html
```

Do not commit generated HTML. If a README screenshot is needed, sanitize a screenshot first using `docs/VISUAL_ASSETS.md` and `docs/WEB_UI_PREVIEW.md`.

## Scanner Rule Catalog Validation
Scanner tests cover stable `ACKIT` rule ID mapping, additive JSON `ruleId` output, SARIF rule metadata, config-driven `safeDomains`, `ignoredPaths`, and `ignoredFindingIds`, and the rule that Critical findings remain reportable.

See [SCANNER_RULES.md](SCANNER_RULES.md) and [CONFIGURATION.md](CONFIGURATION.md).

## v0.2 Readiness Review
Run the v0.2 local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1
```

Use it as a failing gate for missing v0.2 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1 -FailOnIssues
```

See [V020_READINESS.md](V020_READINESS.md).

## v0.3 Readiness Review
Run the v0.3 local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1
```

Use it as a failing gate for missing v0.3 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1 -FailOnIssues
```

See [V030_READINESS.md](V030_READINESS.md).

## v0.4 Readiness Review
Run the v0.4 local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1
```

Use it as a failing gate for missing v0.4 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues
```

See [V040_READINESS.md](V040_READINESS.md).

## v0.5 Readiness Review
Run the v0.5 local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1
```

Use it as a failing gate for missing v0.5 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues
```

See [V050_READINESS.md](V050_READINESS.md).

## v1.0 Local Contract Gates
Run the stable CLI contract check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1
```

Use it as a failing local gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues
```

See [CLI_CONTRACT.md](CLI_CONTRACT.md).

Run the config/generated convention check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1
```

Use it as a failing local gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues
```

See [CONFIG_GENERATED_CONVENTIONS.md](CONFIG_GENERATED_CONVENTIONS.md).

Run the documentation/release gate freeze check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1
```

Use it as a failing local gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues
```

See [V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md](V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md).

## Historical v1.0 Asset Readiness Review
Run the historical v1.0 asset and current gap-analysis presence check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1
```

Use it as a failing local gate for missing historical assets or the maintained 1.0 gap analysis:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1 -FailOnIssues
```

See [V100_READINESS.md](V100_READINESS.md) and [V100_GAP_ANALYSIS.md](V100_GAP_ANALYSIS.md). Passing this script does not mean the product is ready for 1.0 GA.

## Release Blocker Review
Report current blockers:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1
```

Use the blocker check as a failing gate before public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
```

If the working tree is clean, package metadata is final, and the release tag exists locally, the failing gate should return `0`. See [RELEASE_BLOCKERS.md](RELEASE_BLOCKERS.md).

## Package Metadata Review
Run package metadata review in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
```

Use it as a failing gate before public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
```

With the final package URLs in metadata, the failing gate should return `0`. See [NUGET_METADATA.md](NUGET_METADATA.md).

## Public Release Audit
Run the final public release audit in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1
```

Use it as a failing gate before public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
```

See [PUBLIC_RELEASE_AUDIT.md](PUBLIC_RELEASE_AUDIT.md).

## Public Release Gate Orchestration
Run all public release gates in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
```

Run all public release gates as failing checks before future public release announcements or follow-up release work:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

See [PUBLIC_RELEASE_GATES.md](PUBLIC_RELEASE_GATES.md).

## Package Validation
```powershell
$stamp = Get-Date -Format "yyyyMMddHHmmss"
$pkg = Join-Path $env:TEMP "ackit-nupkg-$stamp"
$tools = Join-Path $env:TEMP "ackit-tools-$stamp"
New-Item -ItemType Directory -Force -Path $pkg, $tools | Out-Null

dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
dotnet tool install AgentContextKit --tool-path $tools --add-source $pkg --version 0.2.0-alpha.1 --ignore-failed-sources
& (Join-Path $tools "ackit.exe") version
& (Join-Path $tools "ackit.exe") --help
& (Join-Path $tools "ackit.exe") scan --json
```

## Published NuGet Smoke Test
The `AgentContextKit` version `0.2.0-alpha.1` published global tool has been smoke-tested from NuGet:

- `ackit version` returned `AgentContextKit 0.2.0-alpha.1`.
- `ackit --help` worked.
- `ackit webui` created `.ackit/webui/index.html`.
- `ackit init --lang tr` created `.ackit/config.yml`.
- `ackit scan --ci` completed with no risk findings.
- `ackit generate --target all --lang tr` created agent/context files.
- `ackit task "Demo smoke test görevi" --lang tr` created a task file.
- `ackit report --output .ackit/reports/smoke.html` created a local HTML report.
- `ackit webui --output .ackit/webui/index.html` created a local static Web UI.
- A fake `OPENAI_API_KEY` in `.env.test` was detected by `redact-check` as Critical and returned exit code `2`.
- After `.env.test` was removed, `ackit scan --ci` reported no risk findings.
- `ackit scan --json`, `ackit doctor --json`, `ackit prompt-pack`, and `ackit context-export` worked.
- `context-export` created a local manifest and did not call a remote LLM provider.

The published `0.2.0-alpha.1` smoke test includes `ackit sarif`.

`ackit doctor` can fail on a clean minimal console app because README, LICENSE, SECURITY, tests, CI, `.gitignore`, and package metadata are intentionally absent. That is expected health reporting, not a smoke-test failure.

## Cross-Platform Published-Package Smoke Workflow
`.github/workflows/cross-platform-smoke.yml` verifies the published global tool on Windows, Ubuntu, and macOS.

The workflow:
- Installs .NET 10 with `actions/setup-dotnet`.
- Installs `AgentContextKit` version `0.2.0-alpha.1` as a NuGet global tool.
- Adds the global tool path using `%USERPROFILE%\.dotnet\tools` on Windows and `~/.dotnet/tools` on Linux/macOS.
- Creates a clean console app, initializes git, and runs the installed-tool smoke commands.
- Verifies fake secret detection returns exit code `2`, deletes the fake secret, and confirms the final `ackit scan --ci` has no risk findings.
- Runs as post-release validation only; it does not publish NuGet packages or create tags.

Latest recorded hosted result:
- Workflow: `cross-platform-smoke`.
- Commit: `6d38f11`.
- Branch: `master`.
- Status: Success.
- Windows, Ubuntu, and macOS jobs succeeded.
- NuGet global tool install, `ackit version`, `ackit --help`, DemoApp smoke flow, expected fake-secret `redact-check` failure, and final `scan --ci` all completed successfully.

The workflow installs the current published package `0.2.0-alpha.1` and exercises `ackit sarif`.

## Cross-Platform Source Smoke Workflow
`.github/workflows/cross-platform-source-smoke.yml` verifies the current branch and local package before future publication.

The workflow:
- Uses `actions/checkout@v6` and `actions/setup-dotnet@v5`.
- Runs restore, Release build, and Release tests.
- Packs `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj` into a temporary package directory.
- Installs `AgentContextKit` version `0.2.0-alpha.1` from that temporary package source into a temporary tool path.
- Runs `ackit version`, `ackit --help`, a clean demo app smoke flow, expected fake-secret `redact-check` failure, fake secret cleanup, and final `ackit scan --ci`.
- Does not push, tag, create GitHub Releases, or publish NuGet packages.

Hosted validation status:
- Workflow: `cross-platform-source-smoke`.
- Commit: `6d38f11`.
- Branch: `master`.
- Status: Success.
- Windows, Ubuntu, and macOS jobs succeeded.
- Source restore/build/test, local pack/install, DemoApp smoke flow, expected fake-secret `redact-check` failure, and final `scan --ci` completed successfully.
- Source/package smoke is the correct workflow class for testing next-version commands before future publication.

## CI Workflow
Latest recorded hosted result:
- Workflow: `ci`.
- Commit: `6d38f11`.
- Branch: `master`.
- Status: Success.
- Ubuntu and Windows jobs succeeded.
- Restore, Release build, Release tests, and repository `scan --ci` completed successfully.

## GitHub Actions Node 24 Readiness
The local workflow files are prepared for the GitHub Actions Node 24 JavaScript action runtime:

- `ci.yml` uses `actions/checkout@v6` and `actions/setup-dotnet@v5`.
- `cross-platform-smoke.yml` uses `actions/setup-dotnet@v5`.
- `cross-platform-source-smoke.yml` uses `actions/checkout@v6` and `actions/setup-dotnet@v5`.
- Both workflows set read-only `contents: read` permissions.
- Windows jobs now target `windows-2025` explicitly instead of relying on the moving `windows-latest` label.
- `FORCE_JAVASCRIPT_ACTIONS_TO_NODE24=true` is not set because the selected official action majors already run on Node 24.

Hosted workflow validation is complete for the latest TASK-0056 push. Future workflow changes still require hosted validation after a maintainer push. This task does not push, tag, create GitHub Releases, or publish NuGet packages.

## Manual Release Gates
- Run `scripts/check-package-metadata.ps1 -FailOnIssues` and confirm it exits `0`.
- Run `scripts/audit-public-release.ps1 -FailOnIssues` and confirm it exits `0`.
- Run `scripts/check-release-blockers.ps1 -FailOnBlockers` and confirm it exits `0`.
- Run `scripts/check-public-release-gates.ps1 -FailOnIssues` and confirm it exits `0`.
- Confirm `RepositoryUrl` points to the real public repository.
- Confirm `PackageProjectUrl` points to the real public project/repository page.
- Confirm package README renders correctly.
- Confirm license and security policy are current.
- Confirm no secrets, dumps, backups, uploads, `bin/`, `obj/`, or generated package outputs are committed.
- Confirm no permanent global tool install is required for validation.
- Confirm GitHub Actions latest `master` run is green.
- Confirm GitHub Release page exists for the current release tag.
- Confirm NuGet package availability and global tool install for `AgentContextKit` version `0.2.0-alpha.1`.
- Confirm the published NuGet global tool smoke test remains documented and reproducible.
- Confirm Codex for OSS form submission remains recorded; keep `docs/CODEX_FOR_OSS_APPLICATION.md` as the submitted application pack/reference.

See [MAINTAINER_RELEASE_HANDOFF.md](MAINTAINER_RELEASE_HANDOFF.md) for published release status and alpha.2 follow-up guidance.

## Current-Source Baseline Validation
TASK-0086 adds a current-source-only baseline workflow; it is not in published NuGet `0.2.0-alpha.1`.

```powershell
dotnet run --project src/AgentContextKit.Cli -- baseline --output .ackit-baseline.json
dotnet run --project src/AgentContextKit.Cli -- scan --baseline .ackit-baseline.json --ci
dotnet run --project src/AgentContextKit.Cli -- baseline --output .ackit-baseline.json --update --json
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/baseline.sarif --baseline .ackit-baseline.json --json
dotnet run --project src/AgentContextKit.Cli -- report --output .ackit/reports/baseline.html --baseline .ackit-baseline.json --json
dotnet run --project src/AgentContextKit.Cli -- webui --output .ackit/webui/baseline.html --baseline .ackit-baseline.json --json
```

Validate that the first command refuses an existing file without `--update`, baseline JSON contains no raw matches/messages/absolute paths, existing Critical findings remain visible, and only new High/Critical findings affect baseline-aware CI exits. Parse SARIF, confirm result properties contain no raw match, and confirm report/Web UI include existing/new status. Use a disposable repository for secret-pattern smoke tests and remove generated artifacts after validation.
