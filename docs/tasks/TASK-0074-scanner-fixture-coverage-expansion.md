# TASK-0074 Scanner Fixture Coverage Expansion

## Purpose
Expand scanner regression fixtures so supported secret, PII, artifact, local-path, allowlist, and suppression boundaries remain precise across future rule changes.

## Scope
- Add table-driven scanner fixtures for representative positive and negative cases.
- Verify expected category, severity, and stable rule ID.
- Verify fixture-like values are ignored only in fixture-like paths.
- Verify Critical findings cannot be suppressed by configured allowlists.
- Document the fixture coverage matrix and safe test-data conventions.

## Affected Files
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `src/AgentContextKit.Core/Scanning.cs`
- `docs/SCANNER_FIXTURES.md`
- `docs/SCANNER_RULES.md`
- `docs/SECURITY_MODEL.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `docs/ROADMAP.md`
- `CHANGELOG.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/i18n Impact
None. Scanner categories and rule IDs remain language-independent.

## Audit/Security Impact
Fixtures use synthetic values assembled in tests to avoid committing real-looking live credentials. Critical suppression resistance remains mandatory.

## Acceptance Criteria
- Representative supported rule families have table-driven positive fixtures.
- Safe documentation and fixture examples have negative fixtures.
- Expected category, severity, and rule ID are asserted.
- Critical findings remain visible despite `ignoredPaths` or `ignoredFindingIds`.
- Full tests and release gates pass with no new self-scan findings.

## Tests
Run focused scanner tests, full Release tests, repository scan/doctor/JSON, SARIF parse, sample smoke, hygiene scans, and release gates.

## Risks
- Over-broad negative fixtures can hide real findings.
- Literal token prefixes in source can create self-scan noise.
- Platform-specific paths can make fixtures unstable.
- Case-insensitive regex behavior can vary by process culture unless explicitly culture-invariant.

## Rollback
Revert the scanner precision, fixture test, and documentation commit.

## Completion Notes
- Added table-driven positive fixtures for secret/local-path content and repository path risks, asserting severity, category, and stable rule ID.
- Added negative fixtures for placeholder emails, technical domains, .NET namespaces, documentation IP ranges, date-like values, and wildcard safe-domain boundaries.
- Fixed locale-sensitive scanner matching by applying culture-invariant semantics to case-insensitive regexes.
- Added narrow built-in safe technical entries for Shields.io badge hosts and `System.IO` after culture-invariant self-scan exposed legitimate technical noise.
- Focused scanner tests passed 58/58; full Release tests passed 125/125 with 0 build warnings and 0 build errors.
- Repository scan, doctor, JSON scan, global-tool checks, SARIF parse, sample smoke, hygiene scans, CLI/config/v0.2/v1.0 gates, and local release verification passed.
- The pre-commit public release gate failed only because the working tree contained the task changes, which is expected before commit.
- The post-commit public release gate passed with no blockers; only the expected post-release `HEAD` warning and manual remote-tag verification note remain.
- Read-only GitHub Actions status was successful for the latest pushed predecessor (`3ecf005`) across CI, published-package smoke, and source-package smoke. Hosted validation for this local commit requires maintainer push.
