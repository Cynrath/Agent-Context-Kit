# TASK-0112 Docs Quality Toolchain Decision

## Purpose
Record a dependency-free current decision for documentation lint, prose, links, and future site generation.

## Background
The documentation set is large, but adding Node/Python site tooling now would create maintenance and network costs.

## Current State
The docs site is deferred; individual quality tools are cataloged but not decisioned together.

## Scope
- Keep Markdown canonical.
- Evaluate markdownlint-cli2, Vale, Lychee, MkDocs, Docusaurus, and Starlight as optional future tools.
- Define local-file-first link checking and activation criteria.

## Out Of Scope
- No dependency, package manifest, config, workflow, site generator, hosted URL check, or Pages setting.
- No generated site.

## Affected Files
- docs/DOCS_QUALITY_TOOLCHAIN_DECISION.md
- docs/DOCS_SITE_PLAN.md
- docs/DOCUMENTATION_INDEX.md
- docs/ROADMAP.md
- queue and .codex files

## Data And Privacy Boundary
No analytics, external fonts, telemetry, or network link crawling in default validation.

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
No analytics, external fonts, telemetry, or network link crawling in default validation. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- Current default is no docs toolchain dependency.
- Markdown remains canonical.
- Each candidate has a narrow future role and activation gate.

## Validation Steps
- Docs inventory/link review, scan/hygiene, docs gates.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- Manual quality checks scale poorly.
- Deferred tooling can allow link/prose drift.

## Rollback Plan
Revert decision docs.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Kept Markdown canonical with no dependency; assigned narrow future roles to markdownlint-cli2, Vale, Lychee, MkDocs, Docusaurus, and Starlight.
- External URL checks and site activation remain optional/maintainer-controlled.
