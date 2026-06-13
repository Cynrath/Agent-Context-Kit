# External Tool Contracts

Status: design-only profile vocabulary.

## Profile Fields
| Field | Requirement |
| --- | --- |
| `toolId` | Stable lowercase AgentContextKit profile identifier, not an executable path |
| `displayName` | Human-readable upstream name |
| `evidenceId` | Link to `docs/RELATED_TOOLS_EVIDENCE.md` |
| `licenseStatus` | Reviewed license/terms summary and date |
| `executableNames` | Platform-specific candidate names; discovery is opt-in |
| `versionProbe` | Bounded no-network arguments and parser |
| `supportedVersions` | Explicit reviewed range or `unknown/experimental` |
| `networkPolicy` | `none`, `prepared-cache`, or `explicit-opt-in`; never implicit |
| `inputPolicy` | Repository root, disposable lab, or explicit file profile |
| `outputPolicy` | Repository-relative ignored directory, formats, size/count limits |
| `exitPolicy` | External exit meanings preserved without ACKIT severity mapping |
| `privacyClass` | Source bundle, graph, findings, SBOM, index, or diagnostics |
| `parserProfile` | Optional versioned parser; never generic JSON trust |
| `platforms` | Evidence-backed Windows/Linux/macOS support |
| `staleAfter` | Date after which the profile cannot be recommended |

## Result Envelope
A future operation result should contain profile ID, detected version, resolved executable path in local human output only, start/end status, external exit code, timeout/cancellation status, repository-relative output paths, byte counts, sanitized diagnostics, and network policy. Machine-readable output must not include user profile paths, environment values, raw secrets, or source snippets.

## Rule And Tool Identity
External tool identity and upstream rule IDs remain intact. Use a namespace such as `gitleaks/<rule>` or `semgrep/<rule>`. Never rewrite an external ID into `ACKIT####` without a reviewed one-to-one semantic mapping and tests.

## Stability
External profiles are experimental until upstream version ranges, output fixtures, privacy behavior, and all three supported operating systems have evidence. Removing a stale profile is allowed without changing the core CLI contract when the profile has never been declared stable.

## Import Contract
Any future `parserProfile` must satisfy `docs/EXTERNAL_OUTPUT_IMPORT_BOUNDARY.md`: explicit format/version, namespaced identity, bounded resources, repository-relative paths, sanitized summaries, and failure isolation. Generic JSON trust is prohibited.
