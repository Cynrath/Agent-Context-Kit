# TASK-0110 Workflow Command Design

## Purpose
Design a future guidance-only `ackit workflow` command family without executing external tools.

## Background
Reviewed examples can be surfaced by a future CLI, but invocation must remain manual and explicit.

## Current State
Per-tool Markdown examples exist after TASK-0102; no command-level guidance contract exists.

## Scope
- Design list and show commands for reviewed workflows.
- Specify guidance output, versioning, localization, and manual-copy boundaries.

## Out Of Scope
- No command implementation, subprocess, install, network, file generation, or automatic clipboard use.
- No external tool validation.

## Affected Files
- docs/WORKFLOW_COMMAND_DESIGN.md
- docs/CLI_CONTRACT.md
- docs/CLI_REFERENCE.md
- docs/EXTERNAL_TOOL_WORKFLOWS.md
- docs/ROADMAP.md
- queue and .codex files

## Data And Privacy Boundary
Guidance must omit credentials, hosted upload, private paths, and raw repository content.

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
Guidance must omit credentials, hosted upload, private paths, and raw repository content. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- All requested workflow profiles are covered.
- Output is reviewed guidance only and clearly marked design-only.
- Unknown profiles and exit behavior are defined conceptually.

## Validation Steps
- CLI docs consistency, example link review, full docs gates.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Examples may drift from external CLIs.
- Users may assume printed commands are universally safe.

## Rollback Plan
Revert design docs.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Designed list/show behavior for reviewed guidance profiles, canonical docs sourcing, localization parity, conceptual exit behavior, and explicit non-execution rules.
- No CLI code or external tool behavior changed.
