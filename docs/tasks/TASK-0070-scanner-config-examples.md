# TASK-0070 Scanner Config Examples

## Purpose
Add safe copy-ready `.ackit/config.yml` examples for common scanner modes without introducing real secrets or Critical suppression examples.

## Scope
- Add minimal, strict, and CI-oriented config examples under `docs/examples/config/`.
- Link examples from configuration, scanner rules, and sample gallery docs.
- Keep examples safe, local-only, and documentation-only.

## Affected Files
- `docs/examples/config/minimal-config.yml`
- `docs/examples/config/strict-config.yml`
- `docs/examples/config/ci-config.yml`
- `docs/CONFIGURATION.md`
- `docs/SCANNER_RULES.md`
- `docs/SAMPLE_GALLERY.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
No remote permissions or workflows are changed.

## SEO/i18n Impact
None.

## Audit/Security Impact
Examples explain that allowlists suppress only non-Critical findings and must not be used to hide secrets, production config, private data, or release blockers.

## Acceptance Criteria
- Config examples contain no real secrets, tokens, private emails, or local machine paths.
- Examples avoid Critical suppression guidance.
- Docs explain `safeDomains`, `ignoredPaths`, and `ignoredFindingIds` boundaries.
- Validation and hygiene checks pass.

## Tests
Covered by PROJECT-CONTROL validation and post-commit public release gate.

## Risks
Users may over-copy allowlists without review. Examples keep comments narrow and conservative.

## Rollback
Revert the docs commit.

## Completion Notes
- Added minimal, strict, and CI config examples locally.
- Pre-commit validation passed: restore, Release build, 83/83 tests, `scan --ci`, `doctor`, `scan --json`, global `ackit version/help`, SARIF generation/parse, sample smoke, hygiene scans, `git diff --check`, config/generated gate, v0.2 readiness gate, v1.0 documentation release gate, and `verify-release.ps1`.
- `check-public-release-gates.ps1 -FailOnIssues` reported expected dirty-working-tree failures before commit and must be rerun after commit.
- Post-commit `check-public-release-gates.ps1 -FailOnIssues` passed. Expected warning remains that current `HEAD` is after `v0.2.0-alpha.1`, so remote tag verification is manual.
