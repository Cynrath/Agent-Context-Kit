# External Tools Command Design

Status: future design only. These commands are not implemented or included in the published CLI.

## Goals
Provide reviewed discovery and explanation without installing, updating, executing workflows, reading repository content, or changing default doctor behavior.

## Proposed Commands
| Command | Design behavior | No-go behavior |
| --- | --- | --- |
| `ackit external-tools list` | Print known profile IDs, evidence status, stale state, and design recommendation | No PATH scan, install, version probe, or network |
| `ackit external-tools check` | With explicit opt-in, resolve candidate executable paths and run profile-safe bounded version probes | No repository argument, no workflow execution, no auto-update |
| `ackit external-tools explain <tool>` | Print reviewed license/offline/privacy/output notes and documentation links | No endorsement or compatibility guarantee |
| `ackit doctor --external-tools` | Optional separate section reporting profile discovery only | Missing tools do not fail normal doctor or repository health |

## Default Doctor Contract
`ackit doctor` without `--external-tools` remains unchanged. Optional tools are not repository requirements. Absence, unsupported version, or stale evidence is informational in the explicit external mode unless a future user-selected workflow declares a local prerequisite.

## PATH Probing Boundary
- Disabled unless `check` or `doctor --external-tools` is explicit.
- Resolve and display a sanitized executable path in human output; machine output should avoid user-profile roots where possible.
- Do not invoke through a shell or accept arbitrary command strings.
- Detect multiple candidates and require explicit selection before any future workflow execution.
- Treat version output as untrusted, bounded text.
- Never inherit provider credentials or forward repository content to a probe.

## Conceptual Output
Human output: profile, discovery state, reviewed recommendation, evidence date/staleness, detected version when explicitly probed, platform support, and privacy warning.

Future JSON must use the existing command envelope but requires a schema/version task. It must not include environment values, raw process output, usernames, machine names, or absolute workspace paths.

## Exit Design
- `0`: requested design/discovery operation completed, including tools not installed.
- `1`: invalid profile, unsafe executable resolution, malformed version output, timeout, or unsupported explicit check.
- `2`: reserved for AgentContextKit critical risk semantics; external-tool absence must not use it.

This exit mapping is design-only and not part of the current stable CLI contract.

## Security Review Before Implementation
Threat model, fake process runner, timeout/cancellation, executable-path confirmation, environment minimization, output sanitization, cross-platform tests, and no-network tests are mandatory.
