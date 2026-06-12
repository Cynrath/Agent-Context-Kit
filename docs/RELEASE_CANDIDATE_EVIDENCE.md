# Release Candidate Evidence

## Status
Local evidence preparation is in progress after TASK-0084 through TASK-0087 baseline/config hardening. This document does not approve a release candidate or 1.0 GA.

## Local Evidence Matrix
| Area | Local Evidence | Status | Remaining Blocker |
| --- | --- | --- | --- |
| Baseline policy | schema/fingerprint fixtures, explicit update, integrity checks, new/existing CI tests, JSON/SARIF/report/Web UI parity | Ready locally | hosted/package upgrade smoke |
| Config compatibility | published `0.2.0-alpha.1` config fixture plus read-only `config-check` human/JSON/exit/privacy contract tests | Ready locally | hosted predecessor-config smoke and RC sign-off |
| CLI/JSON/SARIF | contract tests, additive schema rules, exit-code parity | Ready locally | RC freeze/sign-off and machine-readable schema decision |
| Performance | disposable 2,000-file benchmark with 30-second tripwire | Initial local evidence | hosted three-OS, memory, cancellation, mixed corpus |
| Security response | policy, Critical regression tests, privacy boundaries | Partial | private GitHub reporting channel and dated dependency review |
| Runtime support | .NET 10 and current three-OS workflow policy | Documented | fresh hosted RC evidence and final support window |
| Supply chain | metadata/artifact/package install gates | Partial | signing/SBOM/provenance/recovery decisions |
| Migration/localization | upgrade/rollback guide and fixtures | Partial | CLI config migration and EN/TR parity gate |

## Local Commands
```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
powershell -ExecutionPolicy Bypass -File scripts/measure-scan-performance.ps1 -FileCount 2000 -MaxSeconds 30 -FailOnThreshold
powershell -ExecutionPolicy Bypass -File scripts/check-release-candidate-evidence.ps1 -FailOnIssues
```

## Dated Local Result
Local evidence recorded on 2026-06-12:
- Published-config and baseline-schema fixture tests passed: 2/2.
- The final disposable 2,000-file scan benchmark rerun completed in 2.936 seconds against a 30-second tripwire.
- NuGet vulnerability review reported no vulnerable direct or transitive packages.
- NuGet deprecation review reported `xunit` `2.9.3` as Legacy and identified `xunit.v3` as the alternative.

The xUnit finding affects test tooling rather than the shipped runtime package, but it still requires a migration or explicit dated maintainer risk acceptance before a 1.0 release candidate.

## Maintainer-Only Evidence
- Enable and verify private vulnerability reporting.
- Push reviewed source and obtain green Windows/Ubuntu/macOS CI, source-package smoke, baseline upgrade smoke, and benchmark evidence.
- Decide signing, SBOM, provenance, and package recovery/deprecation policy.
- Select/version a release candidate, create tag/GitHub pre-release, publish NuGet only in a dedicated approved release task.
- Record dependency vulnerability/deprecation review with the release date.

## Decision Rule
Do not call the project 1.0 RC-ready while any P0 gap in `docs/V100_GAP_ANALYSIS.md` remains open. P1 gaps require completion or an explicit dated maintainer risk acceptance before 1.0 GA.
