# TASK-0107 External Tool Privacy Threat Model

## Purpose
Model privacy and trust risks from external prompt, graph, SARIF, SBOM, index, and report outputs.

## Background
Complementary tools can produce richer artifacts than AgentContextKit and may expose substantially more repository information.

## Current State
Privacy warnings exist per tool but are not consolidated into threats, mitigations, and residual risks.

## Scope
- Document all requested threat categories, mitigations, trust boundaries, abuse cases, and review responsibilities.
- Link the threat model from privacy, security, and interoperability docs.

## Out Of Scope
- No external scan, secret processing, artifact normalization, upload, redaction engine, or security certification.
- No claim that mitigations eliminate risk.

## Affected Files
- docs/EXTERNAL_TOOL_PRIVACY_THREAT_MODEL.md
- docs/EXTERNAL_OUTPUT_PRIVACY.md
- docs/SECURITY_MODEL.md
- docs/PRIVACY.md
- docs/INTEROPERABILITY_DESIGN.md
- queue and .codex files

## Data And Privacy Boundary
This task is the privacy control definition; all examples remain synthetic and local-only.

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
This task is the privacy control definition; all examples remain synthetic and local-only. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- All listed threats and mitigations are covered.
- External findings never inherit ACKIT stable IDs without mapping policy.
- Residual risk and maintainer/user responsibilities are explicit.

## Validation Steps
- Threat-category coverage search, scanner/hygiene, docs/security gates.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Threat lists can create a false sense of completeness.
- Tool telemetry/defaults can change.

## Rollback Plan
Revert threat-model documentation.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added assets, trust zones, requested threats, mandatory mitigations, abuse cases, residual risk, and responsibility boundaries.
- Linked privacy, security, output, and interoperability docs; no external artifact was processed.
