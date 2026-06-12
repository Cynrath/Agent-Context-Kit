# TASK-0073 CLI Exit Code Contract Hardening

## Purpose
Harden the documented CLI exit-code contract with focused regression tests while preserving current runtime behavior.

## Scope
- Verify success aliases and invalid invocation exits.
- Verify scan CI, redact-check, doctor, and generated-command exit behavior.
- Verify JSON and human-readable modes return the same process exit code.
- Clarify automation guidance in exit-code documentation.

## Affected Files
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/EXIT_CODES.md`
- `docs/CLI_CONTRACT.md`
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
No localization text changes. Exit codes remain language-independent.

## Audit/Security Impact
Tests preserve non-zero automation exits for high/critical findings and invalid invocations. No real secrets are used.

## Acceptance Criteria
- Exit codes `0`, `1`, and `2` are covered for their documented command classes.
- JSON and human modes are verified to use identical exit decisions.
- Invalid required-option cases remain exit code `1`.
- Full tests and release gates pass.

## Tests
Run focused/full tests, scan, doctor, SARIF parse, sample smoke, hygiene scans, and release gates.

## Risks
- Test fixtures can accidentally trigger unrelated scanner findings. Use isolated, synthetic values with explicit expected severity.

## Rollback
Revert the test/docs commit. Runtime CLI behavior is not changed by this task.

## Completion Notes
- Added success coverage for help/version aliases and empty invocation.
- Added invalid-invocation coverage for missing task title, SARIF output, context-export approval, and prompt-pack input.
- Added human/JSON exit parity coverage for high-risk CI scan, non-critical redact findings, and failed doctor health checks.
- Documented language/output-format independence and compatibility review requirements.
- Focused exit/JSON tests passed 34/34; full Release tests passed 96/96 with 0 build warnings and 0 build errors.
- Repository scan, doctor, JSON scan, global-tool checks, SARIF parse, sample smoke, hygiene scans, CLI/config/v0.2/v1.0 gates, and local release verification passed.
- The pre-commit public release gate failed only because the working tree contained the task changes, which is expected before commit.
- The post-commit public release gate passed with no blockers; only the expected post-release `HEAD` warning and manual remote-tag verification note remain.
