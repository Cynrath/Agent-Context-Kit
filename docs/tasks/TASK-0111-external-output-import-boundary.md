# TASK-0111 External Output Import Boundary

## Purpose
Design safe future import boundaries for external SARIF, JSON, SBOM, and graph outputs.

## Background
External artifacts use different schemas, identifiers, sensitivity levels, and path conventions.

## Current State
Interoperability design mentions imports but lacks profile-specific normalization and size/safety rules.

## Scope
- Define format profiles, namespaces, size limits, path normalization, summary-only policies, failure isolation, and mapping restrictions.
- Align SARIF, JSON, and security docs.

## Out Of Scope
- No parser, import command, schema package, artifact read, finding merge, or rule mapping implementation.
- No SBOM publication approval.

## Affected Files
- docs/EXTERNAL_OUTPUT_IMPORT_BOUNDARY.md
- docs/EXTERNAL_TOOL_CONTRACTS.md
- docs/INTEROPERABILITY_DESIGN.md
- docs/SARIF_OUTPUT.md
- docs/JSON_OUTPUT.md
- docs/SECURITY_MODEL.md
- docs/ROADMAP.md
- queue and .codex files

## Data And Privacy Boundary
Reject absolute/traversal paths and raw secret ingestion; prefer sanitized counts and metadata summaries.

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
Reject absolute/traversal paths and raw secret ingestion; prefer sanitized counts and metadata summaries. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- External rule IDs remain namespaced.
- No external finding becomes an ACKIT core finding without explicit mapping policy.
- SARIF/JSON/SBOM/graph boundaries cover requested controls.

## Validation Steps
- Contract consistency review, scanner/hygiene, docs/security gates.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Summary extraction can still leak component or architecture names.
- Format variants may exceed the design.

## Rollback Plan
Revert import-boundary docs.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added universal input limits plus SARIF, JSON, SBOM, and graph summary boundaries with namespaced IDs, path containment, sanitized summaries, and failure isolation.
- No parser, import command, or finding mapping was implemented.
