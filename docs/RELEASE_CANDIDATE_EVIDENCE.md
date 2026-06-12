# Release Candidate Evidence

## Status
Local evidence preparation includes TASK-0084 through TASK-0092 baseline/config hardening, dependency cleanup, the manual hosted evidence workflow design, and a conditional local contract freeze. This document does not approve a release candidate or 1.0 GA.

## Local Evidence Matrix
| Area | Local Evidence | Status | Remaining Blocker |
| --- | --- | --- | --- |
| Baseline policy | schema/fingerprint fixtures, explicit update, integrity checks, new/existing CI tests, JSON/SARIF/report/Web UI parity, manual three-OS workflow design | Ready locally | successful hosted workflow run |
| Config compatibility | published `0.2.0-alpha.1` config fixture plus read-only `config-check` human/JSON/exit/privacy contract tests | Ready locally | hosted predecessor-config smoke and RC sign-off |
| CLI/JSON/SARIF | contract tests, additive schema rules, exit-code parity, conditional local freeze | Ready locally | maintainer sign-off and machine-readable schema decision |
| Performance | disposable 2,000-file benchmark with 30-second tripwire plus manual three-OS workflow design | Initial local evidence | successful hosted run, memory, cancellation, mixed corpus |
| Security response | policy, Critical regression tests, privacy boundaries, clean dated dependency review | Partial | private GitHub reporting channel |
| Runtime support | .NET 10, three-OS workflow policy, and manual RC workflow | Documented | fresh hosted RC run and final support window |
| Supply chain | metadata/artifact/package install gates | Partial | signing/SBOM/provenance/recovery decisions |
| Migration/localization | upgrade/rollback guide, fixtures, read-only config diagnostics, and explicit no-auto-migration policy | Partial | complete EN/TR parity gate and hosted evidence |

## Local Commands
```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
powershell -ExecutionPolicy Bypass -File scripts/measure-scan-performance.ps1 -FileCount 2000 -MaxSeconds 30 -FailOnThreshold
powershell -ExecutionPolicy Bypass -File scripts/check-release-candidate-evidence.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-candidate-workflow.ps1 -FailOnIssues
```

## Dated Local Result
Local evidence recorded on 2026-06-12:
- Published-config and baseline-schema fixture tests passed: 2/2.
- The final disposable 2,000-file scan benchmark rerun completed in 2.936 seconds against a 30-second tripwire.
- NuGet vulnerability review reported no vulnerable direct or transitive packages.
- The initial NuGet deprecation review reported `xunit` `2.9.3` as Legacy. TASK-0091 migrated the executable test project to `xunit.v3` `3.2.2` and `xunit.runner.visualstudio` `3.1.5` using the official xUnit v3 migration requirements.
- Disposable migration smoke and the repository suite both passed 169/169 tests; the post-migration vulnerability and deprecation reviews reported no findings.
- Local Windows reproduction of the manual RC workflow steps passed isolated predecessor/source installs, config immutability, `config-check`, baseline, SARIF parse, and final scan.
- TASK-0092 reconciled the CLI, exit-code, config schema `1`, JSON schema `2`, baseline schema `1`, SARIF `2.1.0`, generated-file, privacy, and upgrade contracts into a conditional local freeze.

The former xUnit test-tooling warning is resolved locally. Remaining supply-chain decisions are signing, SBOM, provenance, recovery policy, and release-date revalidation.

## Maintainer-Only Evidence
- Enable and verify private vulnerability reporting.
- Push reviewed source and obtain green Windows/Ubuntu/macOS CI, source-package smoke, baseline upgrade smoke, and benchmark evidence.
- Decide signing, SBOM, provenance, and package recovery/deprecation policy.
- Select/version a release candidate, create tag/GitHub pre-release, publish NuGet only in a dedicated approved release task.
- Record dependency vulnerability/deprecation review with the release date.
- Complete and approve `docs/MAINTAINER_RC_DECISION.md`; the current decision remains NO-GO for RC publication.

## Decision Rule
Do not call the project 1.0 RC-ready while any P0 gap in `docs/V100_GAP_ANALYSIS.md` remains open. P1 gaps require completion or an explicit dated maintainer risk acceptance before 1.0 GA.
