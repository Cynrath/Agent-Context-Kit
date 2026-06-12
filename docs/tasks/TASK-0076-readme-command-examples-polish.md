# TASK-0076 README Command Examples And Copy-Paste Polish

## Purpose
Make English and Turkish README command examples concise, current, and safe to run without guessing the working directory or build configuration.

## Scope
- Clarify published-tool versus current-source commands.
- State when commands must run from a target repository root.
- Use explicit solution, project, Release, and no-build/no-restore arguments for source flows.
- Group common scan, CI, report, SARIF, generation, and health-check commands.
- Link suppression audit documentation without claiming it exists in the published alpha package.

## Affected Files
- `README.md`
- `README.tr.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `docs/ROADMAP.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/i18n Impact
English and Turkish README examples remain aligned. No CLI localization strings change.

## Audit/Security Impact
Examples keep generated reports local, require explicit context-export approval, and do not include secrets or remote-write commands.

## Acceptance Criteria
- Published install and verification commands use `0.2.0-alpha.1`.
- Source commands are copy-paste-ready from repository root.
- Common workflows are grouped without duplicating the full CLI reference.
- Local artifact and source-only suppression audit boundaries are clear.
- Documentation and release gates pass.

## Tests
Run README command/version searches, build/test/scan/doctor, hygiene checks, and release gates.

## Risks
- README can become too long if it duplicates detailed reference docs.
- Source-only features can be mistaken for published-package features.

## Rollback
Revert the README/task documentation commit.

## Completion Notes
- Task created after TASK-0075.
- English and Turkish README command blocks now separate published-tool, repository-root scan, local artifact, context-generation, and source-build workflows.
- Published install examples remain pinned to `0.2.0-alpha.1`; current-source suppression audit behavior is identified without claiming it exists in that package.
- Validation passed: restore, Release build with 0 warnings/errors, 127/127 tests, clean `scan --ci`, doctor PASS, JSON parse, SARIF parse, sample smoke, hygiene scans, diff check, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; the post-commit rerun passed with no blocking items.
