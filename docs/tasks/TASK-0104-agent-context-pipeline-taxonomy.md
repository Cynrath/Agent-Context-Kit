# TASK-0104 Agent Context Pipeline Taxonomy

## Purpose
Standardize product language around inspect, harden, generate, review, optional enrichment, validate, handoff, and release decision.

## Background
AgentContextKit spans readiness, context, safety, and release workflows; inconsistent terminology makes the product boundary unclear.

## Current State
The concepts exist across product and tutorial docs but are not expressed as one pipeline.

## Scope
- Document eight stages with goals, commands, outputs, privacy/external boundaries, done criteria, and failure criteria.
- Align product, workflow, tutorial, and positioning docs.

## Out Of Scope
- No runtime pipeline engine, new command, automation, or external-tool execution.
- No README expansion beyond concise links.

## Affected Files
- docs/AGENT_CONTEXT_PIPELINE.md
- docs/ECOSYSTEM_POSITIONING.md
- docs/PRODUCT_SPEC.md
- docs/README_POSITIONING_NOTES.md
- docs/AI_WORKFLOW.md
- tutorial docs
- queue/index/map and .codex files

## Data And Privacy Boundary
Optional enrichment is outside the default trust boundary and never precedes local hygiene review.

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
Optional enrichment is outside the default trust boundary and never precedes local hygiene review. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- All eight stages are documented consistently.
- Release decision remains maintainer-controlled and does not imply readiness.

## Validation Steps
- Terminology search, docs links, scanner hygiene, docs/release gates.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Taxonomy can become marketing language detached from commands.
- Duplicated tutorial content can drift.

## Rollback Plan
Revert terminology docs; CLI behavior is unchanged.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added the eight-stage product taxonomy with commands, outputs, privacy/external boundaries, done criteria, and failure criteria.
- Linked product, workflow, tutorial, and ecosystem positioning docs without changing runtime behavior.
