# Release Candidate Local Readiness

## Decision
**LOCAL READY / REMOTE NO-GO** as of 2026-06-12.

The current source tree has complete local release-candidate evidence for its documented contract, localization, security regression, dependency, package, and repository-hygiene checks. This is not a release approval. Hosted evidence, remote security settings, supply-chain decisions, candidate version selection, and maintainer sign-off remain incomplete.

Standard hosted `ci`, published-package smoke, and source-package smoke are green for commit `37d52200fead0ce5c53571205d324b9b7ff6c75b`. The dedicated manual RC evidence workflow remains unrun, so the remote decision stays NO-GO. See `docs/HOSTED_VALIDATION_STATUS.md`.

## Locally Verified Evidence
| Area | Evidence | Local Status |
| --- | --- | --- |
| Build and tests | .NET 10 restore and zero-warning Release build; 178/178 tests | VERIFIED LOCAL |
| CLI and config contracts | help/exit/config convention gates and read-only config diagnostics | VERIFIED LOCAL |
| Machine-readable contracts | command JSON schema `2`, baseline schema `1`, SARIF `2.1.0` profile, sanitized golden fixtures | VERIFIED LOCAL |
| Baseline behavior | deterministic sanitized fingerprints, integrity checks, existing/new classification, output parity | VERIFIED LOCAL |
| Localization | English/Turkish human output, known errors, exits, and language-independent JSON semantics | VERIFIED LOCAL |
| Security regression | Critical token coverage, unsuppressible Critical policy, clean self-scan and hygiene checks | VERIFIED LOCAL |
| Dependencies | direct/transitive vulnerability and deprecation reviews clean after xUnit v3 migration | VERIFIED LOCAL |
| Performance tripwire | disposable 2,000-file scan completed in 3.495 seconds against a 30-second local threshold | VERIFIED LOCAL |
| Package | local pack, isolated tool install, help, scan JSON, and package metadata verification | VERIFIED LOCAL |
| Samples and outputs | sample smoke, doctor, JSON/SARIF parse, and local release verification | VERIFIED LOCAL |

## Remaining Maintainer Evidence
| Area | Required Evidence | Status |
| --- | --- | --- |
| Hosted RC workflow | Standard CI/source/published smoke are green for `37d5220`; dedicated predecessor/config/baseline/performance workflow is still required | PENDING MAINTAINER |
| Private vulnerability reporting | Enabled setting, verified entry point, date, owner, and non-sensitive reference | PENDING MAINTAINER |
| Security notification ownership | Primary and backup owner confirmation | PENDING MAINTAINER |
| Final contract acceptance | Candidate-specific CLI/config/JSON/baseline/SARIF/localization review | PENDING MAINTAINER |
| Signing | Sign or defer decision with lifecycle/recovery evidence or accepted risk | PENDING MAINTAINER |
| SBOM | Publish or defer decision tied to the exact candidate commit | PENDING MAINTAINER |
| Provenance | Attest or defer decision tied to the exact package digest | PENDING MAINTAINER |
| Package recovery | Accepted unlist/deprecate/successor ownership and communication procedure | PENDING MAINTAINER |
| Version and release plan | Candidate version, metadata, package diff, notes, rollback, and post-publish smoke approval | PENDING MAINTAINER |

## Open Gap Boundary
`docs/V100_GAP_ANALYSIS.md` remains the source of truth. A local pass does not close a P0 gap that requires hosted or remote evidence, and it does not dispose a P1 risk without a dated maintainer decision.

The current release-candidate decision in `docs/MAINTAINER_RC_DECISION.md` remains **NO-GO for release-candidate publication**.

## Local Gate
```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-rc-local-readiness.ps1 -RunDependencyReview -FailOnIssues
```

The gate checks this decision boundary and invokes existing release-candidate, workflow, documentation, readiness, contract, localization, and security/supply-chain evidence checks. Use `-SkipBenchmark` only for an intermediate edit loop; final evidence requires the benchmark.

## Remote Boundary
This document and its gate do not push, dispatch hosted workflows, change GitHub settings, handle credentials or certificates, create private reports, sign packages, generate or publish SBOM/provenance, select a version, tag, create a release, or publish NuGet packages.
