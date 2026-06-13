# Ecosystem Evidence Schema

Version: documentation schema `1`. This is not a runtime JSON schema and does not authorize automated network collection.

## Record Fields
| Field | Required | Meaning |
| --- | --- | --- |
| `projectId` | Yes | Stable lowercase catalog key |
| `displayName` | Yes | Official project/tool name |
| `repository` | Yes | Official repository URL |
| `package` | When applicable | Official package and CLI names; keep package and command distinct |
| `license` | Yes | SPDX identifier when asserted, otherwise exact terms-scoped/unknown wording |
| `licenseEvidence` | Yes | Official license URL or repository path |
| `licenseLastChecked` | Yes | ISO date of license review |
| `offlineEvidence` | Yes | Official source supporting a specific local workflow |
| `cliEvidence` | When CLI exists | Official command/install/help source |
| `outputEvidence` | Yes | Official source for claimed formats/artifacts |
| `networkBoundaryEvidence` | Yes | Official source or explicit unresolved marker for default/optional network behavior |
| `privacyRisk` | Yes | Source, path, finding, graph, index, SBOM, telemetry, credential, or hosted-upload exposure |
| `integrationRecommendation` | Yes | `docs-only`, `workflow-example`, `adapter-candidate-later`, or `not-recommended` |
| `lastReviewed` | Yes | ISO review date |
| `reviewerRole` | Yes | Role, not a private personal identity |
| `staleAfter` | Yes | ISO date after which recommendation is suspended pending review |
| `confidence` | Yes | `high`, `medium`, or `low` |
| `notes` | Yes | Open verification items, terms, mode, and platform caveats |

## Confidence Contract
- `high`: direct official evidence supports the exact claim and mode.
- `medium`: core behavior is official, but platform/version/mode details need a disposable lab.
- `low`: evidence is incomplete or ambiguous; state `Needs verification` and do not recommend an adapter.

## Staleness Contract
On or after `staleAfter`, the row remains historical but cannot support a new workflow/adapter/release claim. Re-review license first, then default network behavior, CLI/output contract, privacy, supported platforms, and recommendation.

## Example Markdown Record
```text
projectId: example-tool
displayName: Example Tool
repository: official repository URL
package: package / CLI
license: Needs verification
licenseEvidence: official license path
licenseLastChecked: YYYY-MM-DD
offlineEvidence: official local-workflow source
cliEvidence: official CLI source
outputEvidence: official output source
networkBoundaryEvidence: Needs verification
privacyRisk: full-source output
integrationRecommendation: docs-only
lastReviewed: YYYY-MM-DD
reviewerRole: maintainer-support documentation review
staleAfter: YYYY-MM-DD
confidence: low
notes: no integration recommendation until verified
```

## Validation Policy
Evidence records are manually reviewed Markdown. Do not scrape repositories in default CI, call hosted APIs, install packages, or infer capabilities from source language/stars. A future machine-readable schema must be a separate docs-only task before any automation.
