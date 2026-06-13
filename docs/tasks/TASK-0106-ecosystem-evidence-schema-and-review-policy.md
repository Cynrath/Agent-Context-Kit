# TASK-0106 Ecosystem Evidence Schema And Review Policy

## Purpose
Prevent ecosystem documentation from degrading into stale or unsupported claims.

## Background
Tool capabilities, licenses, network behavior, and installation methods change independently.

## Current State
TASK-0101 introduces evidence records, but the reusable field schema and staleness policy need an explicit contract.

## Scope
- Define required evidence fields, confidence levels, reviewer role, last-reviewed and stale-after dates.
- Apply the policy to comparison/evidence docs.

## Out Of Scope
- No runtime JSON schema, generated validator, scraper, scheduled network job, or automated endorsement.
- No remote issue creation.

## Affected Files
- docs/ECOSYSTEM_EVIDENCE_SCHEMA.md
- docs/RELATED_TOOLS_EVIDENCE.md
- docs/RELATED_TOOLS_REVIEW_POLICY.md
- docs/RELATED_TOOLS_COMPARISON_MATRIX.md
- queue and .codex files

## Data And Privacy Boundary
Evidence stores public project metadata and review notes only, never local repository content or credentials.

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
Evidence stores public project metadata and review notes only, never local repository content or credentials. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- Every schema field requested in the brief is defined.
- High/medium/low confidence and stale handling are actionable.
- Unverified claims cannot appear as confirmed recommendations.

## Validation Steps
- Schema-to-matrix field review, docs gates, hygiene.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Review dates may give false precision if source scope is unclear.
- Manual policy can still drift without ownership.

## Rollback Plan
Revert evidence-policy docs.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added documentation schema version 1 with required evidence, confidence, reviewer-role, review-date, stale-after, privacy, and recommendation fields.
- Linked the schema to the evidence register, matrix, and review policy without adding runtime automation.
