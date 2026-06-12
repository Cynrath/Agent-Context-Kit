# TASK-0072 JSON Schema Stability And Contract Tests

## Purpose
Harden the existing JSON schema version 2 contract with focused CLI tests without changing the public output shape.

## Scope
- Add a shared contract test for successful JSON commands.
- Verify common envelope fields: `schemaVersion`, `toolVersion`, `generatedAtUtc`, and `command`.
- Verify scanner finding fields and stable `ruleId` behavior.
- Document additive-change and schema-version rules.

## Affected Files
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/JSON_OUTPUT.md`
- `docs/CLI_CONTRACT.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/i18n Impact
JSON field names remain language-independent. Human localization output is unchanged.

## Audit/Security Impact
Finding contract tests verify stable rule IDs and required safe metadata fields. Tests do not assert or expose real secrets.

## Acceptance Criteria
- All successful JSON command classes expose the common schema v2 envelope.
- Scan finding objects expose `ruleId`, `severity`, `category`, `path`, `message`, and `match`.
- Existing JSON output remains backward-compatible.
- Tests and release gates pass.

## Tests
Run focused/full tests, CLI scans, SARIF parse, hygiene checks, and release gates.

## Risks
- Overly strict tests can block legitimate additive fields. Tests should require stable fields without rejecting additional properties.

## Rollback
Revert the contract test/docs commit. Runtime JSON output is not changed by this task.

## Completion Notes
- Added shared schema v2 envelope assertions across all successful JSON command classes.
- Added scanner finding contract coverage for stable rule IDs, required metadata fields, match-field presence, and risk summary counters.
- Documented additive compatibility, schema-version bump, localization, and unknown-field handling rules.
- Focused JSON tests passed 23/23; full Release tests passed 85/85 with 0 build warnings and 0 build errors.
- Repository scan, doctor, JSON scan, global-tool checks, SARIF parse, sample smoke, hygiene scans, config conventions, v0.2 readiness, v1.0 documentation gate, and local release verification passed.
- The pre-commit public release gate failed only because the working tree contained the task changes, which is the expected local gate behavior before commit.
- The post-commit public release gate passed with no blockers; only the expected post-release `HEAD` warning and manual remote-tag verification note remain.
