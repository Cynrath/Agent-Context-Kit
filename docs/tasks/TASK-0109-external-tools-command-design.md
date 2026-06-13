# TASK-0109 External Tools Command Design

## Purpose
Design a future `ackit external-tools` command family without implementing it.

## Background
Users may need discovery and explanation, but probing PATH or executing binaries creates a new trust boundary.

## Current State
Command names exist only in the interoperability backlog.

## Scope
- Design list, check, explain, and optional doctor extension behavior.
- Specify PATH probing, version-probe, exit, output, platform, and privacy boundaries.

## Out Of Scope
- No CLI parser change, command implementation, binary execution, PATH probing, install, network, or doctor behavior change.
- No stable contract promise.

## Affected Files
- docs/EXTERNAL_TOOLS_COMMAND_DESIGN.md
- docs/CLI_CONTRACT.md
- docs/CLI_REFERENCE.md
- docs/INTEROPERABILITY_DESIGN.md
- docs/ROADMAP.md
- queue and .codex files

## Data And Privacy Boundary
Future probes must never pass repository paths/content and must sanitize executable/version output.

## Offline And Default-Network Policy
AgentContextKit remains local-only and performs no external installation, subprocess invocation, repository upload, telemetry, or network call in this task. Any future networked behavior requires explicit opt-in and a separate reviewed task.

## Database Impact
None.

## Admin Impact
None locally. Repository settings, hosted workflows, releases, package ownership, and security controls remain maintainer-only.

## Permission Impact
None. No token scope, workflow permission, filesystem privilege, or remote API permission is added.

## SEO And I18n Impact
Documentation wording and links may change. No hosted site metadata or runtime localization contract changes unless explicitly listed in scope.

## Security Impact
Future probes must never pass repository paths/content and must sanitize executable/version output. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- Default doctor remains unchanged and non-failing when tools are absent.
- Auto-install and network are prohibited.
- Design status is unmistakable in CLI docs.

## Validation Steps
- CLI contract/reference consistency search, docs gates, full validation.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Documented designs can be mistaken for shipped commands.
- PATH probing can execute malicious shadow binaries if later implemented carelessly.

## Rollback Plan
Remove design sections; published CLI contract remains unchanged.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Designed list/check/explain and optional doctor discovery semantics, PATH/version trust boundaries, conceptual output, and non-core exit behavior.
- CLI docs explicitly mark every command as non-shipped; no parser or process code changed.
