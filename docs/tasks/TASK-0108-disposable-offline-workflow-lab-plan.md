# TASK-0108 Disposable Offline Workflow Lab Plan

## Purpose
Define a safe disposable sample-only lab for future external workflow smoke tests without running tools now.

## Background
Docs-only examples eventually need validation, but testing against private repositories or installing tools in the default pipeline is unacceptable.

## Current State
Sample repositories exist; no external-tool evidence-capture lab contract exists.

## Scope
- Define disposable clone, no-secret/no-PAT rules, network isolation modes, output paths, evidence template, cleanup, and failure handling.
- Connect the lab to sample gallery and demo scenarios.

## Out Of Scope
- No tool installation, network call, smoke execution, generated output, container image, or committed evidence artifact.
- No private repository use.

## Affected Files
- docs/DISPOSABLE_EXTERNAL_WORKFLOW_LAB.md
- docs/examples/external-tools/README.md
- docs/SAMPLE_GALLERY.md
- docs/DEMO_SCENARIOS.md
- queue and .codex files

## Data And Privacy Boundary
Only synthetic public samples may be used; outputs stay under ignored `.ackit/external/` and are removed after review.

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
Only synthetic public samples may be used; outputs stay under ignored `.ackit/external/` and are removed after review. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- Offline and network-required modes are isolated.
- Evidence capture excludes source payloads and secrets.
- Cleanup and failure handling are explicit.

## Validation Steps
- Plan review against privacy threat model and offline policy; docs gates.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Cleanup commands can remove unintended files if run outside the disposable root.
- Future tools may create outputs outside configured paths.

## Rollback Plan
Revert lab-plan docs.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added a synthetic disposable lab plan with network-off/preparation separation, output containment, evidence template, failure handling, and scoped cleanup.
- No external tool was installed, downloaded, or executed.
