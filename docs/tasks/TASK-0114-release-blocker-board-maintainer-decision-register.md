# TASK-0114 Release Blocker Board And Maintainer Decision Register

## Purpose
Consolidate maintainer-gated RC/security blockers and decisions without closing them.

## Background
Hosted evidence, reporting, identity, signing, SBOM, provenance, recovery, candidate, and approval evidence is spread across several documents.

## Current State
The local state remains `LOCAL READY / REMOTE NO-GO`; private reporting is disabled and several P0/P1 decisions are pending.

## Scope
- Create a blocker board and decision register with owner, evidence, remote-write, options, accepted risk, review date, impact, and done criteria.
- Link existing evidence/status/handoff docs.

## Out Of Scope
- No blocker closure, setting change, workflow dispatch, release approval, signing, SBOM/provenance publication, owner change, tag, or publish.
- No fabricated due date or owner identity.

## Affected Files
- docs/RELEASE_BLOCKER_BOARD.md
- docs/MAINTAINER_DECISION_REGISTER.md
- security/supply-chain and RC status docs
- queue and .codex files

## Data And Privacy Boundary
The board stores control/evidence metadata only and must not include tokens, private reports, certificates, or recovery secrets.

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
The board stores control/evidence metadata only and must not include tokens, private reports, certificates, or recovery secrets. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- All ten requested blockers are present.
- Remote-write requirements and accepted-risk paths are explicit.
- LOCAL READY / REMOTE NO-GO remains authoritative.

## Validation Steps
- Cross-check against existing status/evidence docs and security gates.
- Full release verification.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- A board can become stale or be mistaken for approval.
- Accepted-risk placeholders may be misread as accepted decisions.

## Rollback Plan
Revert board/register links; original evidence docs remain authoritative.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added ten explicit P0/P1 blocker rows and a pending decision register with owner roles, evidence, remote-write needs, accepted-risk paths, review placeholders, release impact, and done criteria.
- Preserved verified disabled/mismatch/missing evidence and the `LOCAL READY / REMOTE NO-GO` boundary; no blocker was closed.
