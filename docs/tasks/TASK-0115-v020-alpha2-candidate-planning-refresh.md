# TASK-0115 v0.2.0-alpha.2 Candidate Planning Refresh

## Purpose
Refresh alpha.2 scope planning without changing version metadata or authorizing a release.

## Background
The current published release is `v0.2.0-alpha.1`; product hardening and ecosystem docs have progressed while maintainer gates remain open.

## Current State
Earlier alpha.2 scope documents exist, but candidate planning needs current product, offline, and maintainer-gate boundaries.

## Scope
- Create a current planning document covering scanner precision, suppression audit, baseline/config polish, and ecosystem docs.
- Align roadmap, changelog planning, handoff, queue, and Codex context.

## Out Of Scope
- No version bump, tag, package, release notes finalization, publish, workflow pin change, or release approval.
- No claim that alpha.2 is selected or ready.

## Affected Files
- docs/V020_ALPHA2_PLAN.md
- docs/ROADMAP.md
- docs/CHANGELOG.md
- docs/NEXT_TASKS.md
- docs/PROJECT_EXECUTION_QUEUE.md
- docs/MAINTAINER_RELEASE_HANDOFF.md
- Codex handoff files

## Data And Privacy Boundary
No dependency expansion, default network behavior, external execution, or new data upload is allowed in the proposed scope.

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
No dependency expansion, default network behavior, external execution, or new data upload is allowed in the proposed scope. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- Plan clearly says planning-only.
- P0/P1 maintainer decisions remain prerequisites.
- Scope excludes dependency/network/external execution expansion.

## Validation Steps
- Version/reference search, docs/release/security gates, full build/test/scan validation.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Planning can be confused with release authorization.
- A docs/CHANGELOG planning file may drift from root CHANGELOG unless its role is explicit.

## Rollback Plan
Revert planning docs; current published release remains unchanged.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added a planning-only alpha.2 scope, compatibility intent, exclusions, blocker prerequisites, and future release-task boundary.
- Added a docs-only planning changelog that explicitly defers to root `CHANGELOG.md` for product release history.
- No version, workflow package pin, tag, package, release, or publish action changed.
