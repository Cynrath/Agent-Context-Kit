# Project Execution Queue

## Completed Local Execution
| Status | Task range | Scope | Validation | Remote state |
| --- | --- | --- | --- | --- |
| Done locally | TASK-0066 through TASK-0099 | Post-release docs, scanner/config/output hardening, tutorials, baseline/config product work, RC evidence, security/supply-chain state audits | Full local build/test/scan/doctor/sample/readiness/release gates per task | Remote actions remain separately controlled |

## Maintainer-Gated Release/Security Track
| Order | Status | Action | Priority | Blocking status | Expected evidence | Validation required | Remote write required? | Done criteria |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| M1 | Maintainer action required | Manual hosted RC evidence workflow | P0 | Blocks RC | Exact candidate run URLs and Windows/Ubuntu/macOS results | Hosted predecessor/config/baseline/SARIF/performance workflow | Yes | Green run for exact reviewed candidate |
| M2 | Maintainer action required | Private vulnerability reporting and notification ownership | P0 | Blocks RC | `enabled: true`, visible report entry, date, owner, primary/backup notification owners | Read-only API/UI verification after setting change | Yes | Verified private channel and response ownership |
| M3 | Maintainer action required | NuGet owner identity alignment | P1 | Blocks RC unless explicitly accepted | `Cyranth`/`Cynrath` alignment or dated intentional exception | NuGet package/profile review | Yes | Public owner identity has an owned disposition |
| M4 | Maintainer decision required | Author signing | P1 | Complete or accept risk | Sign/defer decision, evidence, lifecycle owner, review date | Exact package verification | Possibly | Author-signing control or dated accepted risk |
| M5 | Maintainer decision required | SBOM publication | P1 | Complete or accept risk | Format, digest, privacy review, location, owner or defer record | Exact candidate/package review | Possibly | Published SBOM or dated accepted risk |
| M6 | Maintainer decision required | Provenance/attestation | P1 | Complete or accept risk | Workflow, subject digest, verification, owner or defer record | Exact artifact attestation review | Yes if attesting | Verified attestation or dated accepted risk |
| M7 | Maintainer decision required | Package recovery ownership | P1 | Required decision | Owner, threshold, communication, unlist/deprecate/successor procedure | Tabletop/document review | Yes only during incident/action | Accepted recovery procedure and review date |
| M8 | Maintainer decision required | Candidate version and release approval | P0/P1 dependent | Waits for required evidence | Version, commit, metadata, notes, package diff, GO/NO-GO | Full local and hosted release gates | Yes | Dedicated approved release-preparation task |

## Local-Only Ecosystem/Product Intelligence Track
| Order | Status | Task | Priority | Blocking status | Expected files | Validation required | Remote write required? | Done criteria |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 35 | Done locally | TASK-0100 offline OSS ecosystem catalog and roadmap reset | Medium | None | ecosystem/positioning/workflow docs, queue reset, README links | Official-source research plus full local gates | No | Initial catalog and split roadmap exist without dependencies |
| 36 | Queued | TASK-0101 related tools comparison matrix | Medium | None | normalized comparison/evidence docs | Source/license/offline review plus docs gates | No | Higher-confidence matrix with review dates and evidence links |
| 37 | Queued | TASK-0102 offline workflow examples with external tools | Medium | Tools installed manually for optional validation | disposable example docs/scripts only if approved | No-upload disposable smoke and hygiene | No | Safe copy-ready workflows with no auto-install/network default |
| 38 | Queued | TASK-0103 optional interoperability design, no dependency | Medium | Requires TASK-0101/0102 evidence | architecture/ADR/schema proposal | Threat model, license/privacy/output contract review | No | Opt-in adapter boundary designed without implementation |
| 39 | Queued | TASK-0104 agent context pipeline taxonomy | Low | None | taxonomy/product/docs updates | Docs review and scanner hygiene | No | Product categories and handoff stages are consistently named |
| 40 | Queued | TASK-0105 README ecosystem positioning section | Low | Waits for comparison/taxonomy review | concise README/docs updates | Docs/hygiene/release gates | No | Public positioning is concise, accurate, and stable |

## Guardrail
Maintainer-gated work does not block local-only product/docs progress, but no local task may claim release-ready, 1.0-ready, active security controls, signing, SBOM, provenance, or hosted RC completion without exact evidence.
