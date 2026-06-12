# Published Supply-Chain Status

## Audit Scope
Read-only audit performed on 2026-06-13 for:

- NuGet package `AgentContextKit` `0.2.0-alpha.1`;
- GitHub Release `v0.2.0-alpha.1`;
- the exact downloaded package digest;
- package signature, archive entries, release assets, and accessible GitHub artifact attestations.

This is a published-state audit, not release approval and not a maintainer decision to sign, publish an SBOM, attest provenance, or change package ownership.

## Package Identity
| Field | Verified value |
| --- | --- |
| Package ID | `AgentContextKit` |
| Version | `0.2.0-alpha.1` |
| Listed | `true` |
| Published | `2026-06-11T08:54:44.08Z` |
| Package SHA-256 | `d6d0804f5dfca7c03f2b8ea173de74e3b0e9e8eb73ceec5dda9ac18cae1988ce` |
| Package metadata author | `Cynrath` |
| Repository URL | `https://github.com/Cynrath/agent-context-kit` |
| Repository commit | `d309a6fee8623297a6004838960fba842b7c4b53` |

The public NuGet Gallery owner profile is `Cyranth`, while package metadata and the public project persona use `Cynrath`. This identity mismatch is a maintainer-only account/package ownership action and must be resolved or explicitly documented before a 1.0 release candidate.

## Signature Result
`dotnet nuget verify --all --verbosity detailed` completed successfully.

- Signature type: `Repository`.
- Repository signer: `NuGet.org Repository by Microsoft`.
- Repository certificate SHA-256: `1F4B311D9ACC115C8DC8018B5A49E00FCE6DA8E2855F9F014CA6F34570BC482D`.
- Repository timestamp: `2026-06-11T08:55:09Z` (displayed locally as 11:55:09 Europe/Istanbul).
- No author signature was observed by the verifier.

NuGet documentation states that packages uploaded to NuGet.org are automatically repository-signed. Repository signing verifies the repository/upload surface and must not be described as AgentContextKit author signing.

References:
- <https://learn.microsoft.com/dotnet/core/tools/dotnet-nuget-verify>
- <https://learn.microsoft.com/nuget/reference/signed-packages-reference>

## SBOM And Provenance Result
The package contained 13 archive entries. No entry name matched SBOM, SPDX, CycloneDX, provenance, in-toto, Sigstore, or attestation conventions.

The GitHub Release is a published pre-release with no manually uploaded assets. GitHub-generated source archives are not project-uploaded SBOM or provenance artifacts.

`gh attestation verify` queried the exact package SHA-256 against `Cynrath/agent-context-kit` and returned HTTP 404. Therefore, no accessible GitHub SLSA provenance attestation was found for this exact package digest during this audit. This conclusion is limited to the inspected repository, digest, and public/authenticated CLI query.

GitHub verification reference:
- GitHub Docs, "REST API endpoints for artifact attestations": <https://docs.github.com/>

## Current Decision Boundary
| Area | Verified published state | Maintainer action required |
| --- | --- | --- |
| NuGet owner identity | Public owner profile `Cyranth`; project persona/author `Cynrath` | Align the profile/package owner identity or record an intentional, reviewed exception |
| Author signing | No author signature observed | Choose author signing or record dated accepted risk |
| Repository signing | Valid NuGet.org repository signature | Preserve as repository evidence; do not relabel as author signing |
| SBOM | Not present in package or GitHub Release assets | Publish an exact-candidate SBOM or record dated accepted risk |
| Provenance | No accessible GitHub attestation for package digest | Attest an exact-candidate artifact or record dated accepted risk |
| Recovery | Procedure documented but not accepted by a named owner | Record owner, threshold, communication path, and review date |

## Reproduction
Use an ignored local path and remove the package after inspection:

```powershell
$package = ".ackit/audits/AgentContextKit.0.2.0-alpha.1.nupkg"
Invoke-WebRequest `
  -Uri "https://api.nuget.org/v3-flatcontainer/agentcontextkit/0.2.0-alpha.1/agentcontextkit.0.2.0-alpha.1.nupkg" `
  -OutFile $package
Get-FileHash -Algorithm SHA256 $package
dotnet nuget verify $package --all --verbosity detailed
gh release view v0.2.0-alpha.1 --repo Cynrath/agent-context-kit --json assets,isDraft,isPrerelease,publishedAt,url
gh attestation verify $package --repo Cynrath/agent-context-kit --format json
```

The attestation command is expected to remain non-zero until an attestation exists for the exact digest. Do not treat that expected absence as a CLI defect.

## Remote Boundary
TASK-0099 downloaded and inspected public artifacts only. It did not sign, publish, attest, upload, edit a release, modify NuGet ownership, unlist/deprecate a package, push, tag, or publish a package.
