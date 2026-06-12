# Security And Supply-Chain Evidence Register

## Status
Local evidence register prepared on 2026-06-12. Remote security settings, signing, SBOM publication, provenance attestations, and final release decisions are **PENDING MAINTAINER**. This document is not release approval and must not be used to claim those controls are active.

## Status Vocabulary
- `VERIFIED LOCAL`: reproduced from local source, tests, package inspection, or documented policy.
- `PROPOSED`: recommended decision that the maintainer has not accepted.
- `PENDING MAINTAINER`: requires a dated maintainer decision or remote evidence.
- `ACCEPTED RISK`: explicit dated owner acceptance with scope and review date.
- `VERIFIED MAINTAINER`: completed remote/credentialed action with non-sensitive evidence metadata.

## Evidence Register
| Area | Current Status | Local Evidence | Maintainer Evidence Required | RC Effect |
| --- | --- | --- | --- | --- |
| Private vulnerability reporting | PENDING MAINTAINER | `SECURITY.md` and response targets prohibit sensitive public reports | Enable the repository setting; record verification date, maintainer, repository URL, and non-sensitive confirmation that the private report entry point is visible | P0 blocker |
| Security notification ownership | PENDING MAINTAINER | Incident fields and response targets are documented | Record who receives repository security notifications and the backup owner; do not record reporter data | P0 blocker |
| Dependency vulnerability/deprecation review | VERIFIED LOCAL on 2026-06-12 | Direct/transitive vulnerability and deprecation reviews were clean after xUnit v3 migration; full suite passes 178/178 | Rerun on final candidate date and record commands, date, commit, sources reached, and result | Required fresh evidence |
| NuGet author signing | PENDING MAINTAINER | Package metadata/content/install gates exist; current package is not claimed as author-signed | Choose sign or defer. If signing, record certificate lifecycle owner, timestamp policy, verification command/result, and recovery plan without private certificate data. If deferred, record ACCEPTED RISK and review date | P1 decision |
| SBOM | PENDING MAINTAINER | Dependency manifests and local dependency review exist | Choose GitHub SPDX export, release-workflow SBOM, or defer. Record format, candidate commit, generation method, hash, publication location, privacy review, and owner | P1 decision |
| Build/package provenance | PENDING MAINTAINER | Source/package smoke and package verification are local; no attestation is claimed | Choose GitHub artifact attestation or defer. Record workflow/run, subject digest, verification command/result, permissions review, publication location, and owner | P1 decision |
| Bad-package recovery | PROPOSED | Successor/unlist/deprecate policy is documented; immutable package replacement is prohibited | Accept the owner, decision threshold, communication path, unlist/deprecate authority, successor version process, and post-incident review date | Required decision |

## Recommended Defaults
- **Private reporting:** enable and verify before any 1.0 release candidate. GitHub documents private vulnerability reporting as a secure repository disclosure channel: <https://docs.github.com/code-security/security-advisories/working-with-repository-security-advisories/configuring-private-vulnerability-reporting-for-a-repository>.
- **Signing:** do not claim author signing until certificate issuance, timestamping, verification, rotation, and recovery are owned. Microsoft documents `dotnet nuget sign` and signature verification: <https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-sign>.
- **SBOM:** prefer SPDX-compatible export tied to the exact candidate commit, with privacy/content review before publication. GitHub documents repository SBOM export: <https://docs.github.com/code-security/supply-chain-security/understanding-your-software-supply-chain/exporting-a-software-bill-of-materials-for-your-repository>.
- **Provenance:** prefer a dedicated release workflow with least-privilege attestation permissions and digest verification; do not add it implicitly to ordinary CI. GitHub documents artifact attestations and required workflow permissions: <https://docs.github.com/actions/security-for-github-actions/using-artifact-attestations/using-artifact-attestations-to-establish-provenance-for-builds>.
- **Recovery:** publish a fixed successor; never replace immutable package content. Unlisting/deprecation and public communication remain explicit maintainer actions.

## Maintainer Evidence Record
Complete this metadata-only block in a dedicated maintainer/release task. Do not include secrets, private report content, certificate files/private identifiers, customer data, or machine-local paths.

```text
Status: PENDING MAINTAINER
Candidate version:
Candidate commit:
Decision date:
Maintainer: Cynrath

Private vulnerability reporting: ENABLED / NOT ENABLED
Verification URL or repository setting reference:
Security notification primary/backup owner:

Dependency review date and result:
Package signing decision: SIGN / DEFER
Signing evidence or accepted-risk reference:
SBOM decision: PUBLISH / DEFER
SBOM format, digest, and publication reference:
Provenance decision: ATTEST / DEFER
Attestation run, subject digest, and verification reference:
Bad-package recovery owner and accepted procedure:

Open risks:
Next review date:
```

## Completion Rule
Change an item to `VERIFIED MAINTAINER` only when the exact candidate commit/version, decision date, owner, and non-sensitive evidence reference are recorded. A proposal, checklist, local script result, or repository document is not remote evidence.
