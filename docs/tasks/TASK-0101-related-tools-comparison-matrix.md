# TASK-0101 Related Tools Comparison Matrix

## Purpose
Turn the initial related-project catalog into a normalized, evidence-backed comparison matrix.

## Background
TASK-0100 established the initial ecosystem catalog. A reviewable matrix is needed before any workflow or adapter recommendation.

## Current State
The initial catalog exists, but evidence dates, confidence, package/install, network, output, platform, and capability fields are not normalized.

## Scope
- Create the comparison matrix, evidence register, and review policy.
- Re-check official license, offline, CLI, output, network, and platform claims.
- Classify every recommendation as docs-only, workflow-example, adapter-candidate-later, or not-recommended.

## Out Of Scope
- No dependency, installation, execution, adapter, benchmark, endorsement, or compatibility guarantee.
- No remote write or hosted upload.

## Affected Files
- docs/RELATED_PROJECTS.md
- docs/RELATED_TOOLS_COMPARISON_MATRIX.md
- docs/RELATED_TOOLS_EVIDENCE.md
- docs/RELATED_TOOLS_REVIEW_POLICY.md
- docs/OFFLINE_OSS_ECOSYSTEM.md
- queue/index/map and .codex handoff files

## Data And Privacy Boundary
Record source-disclosure, local-path, token, graph, SARIF, SBOM, and hosted-upload risks without processing repository content.

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
Record source-disclosure, local-path, token, graph, SARIF, SBOM, and hosted-upload risks without processing repository content. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- All requested projects have normalized rows or an explicit needs-verification marker.
- CodeQL remains terms-scoped; Graphify, Trivy, Semgrep, Gitingest, Repomix, and Code2Prompt boundaries stay accurate.
- Official evidence URLs and review date are present.

## Validation Steps
- Review links and claims against official sources.
- Run repository scan, docs gates, hygiene, and final full validation.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- External projects change quickly; stale evidence can mislead.
- Wide matrices may reduce readability.

## Rollback Plan
Revert the documentation commit; no runtime or remote state changes.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added a normalized three-part matrix covering identity/runtime, offline/network/output capability, and privacy/recommendation decisions.
- Added official-source evidence and a dated confidence/staleness review policy.
- Preserved needs-verification markers and terms-scoped treatment for CodeQL.
