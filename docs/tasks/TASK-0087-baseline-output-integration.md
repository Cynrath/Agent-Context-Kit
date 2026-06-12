# TASK-0087 Baseline Output Integration

## Purpose
Extend the explicit baseline workflow from TASK-0086 to SARIF, HTML report, and Web UI outputs with consistent, additive, privacy-safe metadata.

## Scope
- Accept `--baseline <repo-relative.json>` on `sarif`, `report`, and `webui`.
- Reuse the validated Core baseline store/classifier; do not duplicate policy logic in generators.
- Add existing/new counts and sanitized finding classification to command JSON output.
- Add baseline status, fingerprint, and occurrence to SARIF result properties.
- Add visible baseline summaries/status labels to local HTML report and Web UI output.
- Keep output unchanged when no baseline option is supplied.
- Add focused Core/CLI/output contract tests and synchronize active documentation.

## Out Of Scope
- No baseline support for prompt packs, context export, generation, doctor, or redact-check.
- No automatic baseline creation/update, config-driven baseline activation, remote baseline storage, Code Scanning upload, version bump, push, tag, release, or NuGet publish.

## Affected Files
- Core output interfaces and SARIF/HTML/Web UI generators.
- CLI option handling and JSON mapping.
- focused tests.
- CLI, JSON, SARIF, reports, Web UI, architecture, security, roadmap, queue, changelog, and Codex handoff docs.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. Baseline files and generated outputs remain local and repository-relative.

## SEO/i18n Impact
Human HTML labels remain concise English in the current prototype. JSON/SARIF property names remain language-independent.

## Audit/Security Impact
Output integrations must not copy raw matches into baseline metadata. SARIF baseline properties contain only status, fingerprint, and occurrence. Existing findings remain visible and are not suppressed.

## Acceptance Criteria
- `sarif`, `report`, and `webui` accept an explicit validated baseline path.
- Their JSON responses expose a consistent additive baseline summary.
- SARIF remains valid 2.1.0 and records sanitized per-result baseline properties.
- HTML report and Web UI show existing/new counts and per-finding status without hiding findings.
- Invocations without `--baseline` retain their previous output shape/behavior.
- Missing/invalid baselines return the TASK-0086 stable error contract.
- Full local validation passes.

## Tests
Add focused generator and CLI tests, then run restore/build/test, scan/doctor, JSON/SARIF/sample smoke, hygiene scans, contract/config/readiness/release gates, and post-commit public gates.

## Risks
- Generator-specific implementations can drift from the CLI baseline policy.
- SARIF property changes can accidentally expose unsafe match content.
- HTML status mapping can mismatch finding order if classification is recomputed independently.

## Rollback
Revert the TASK-0087 commit. TASK-0086 scan/baseline behavior remains independently usable.

## Completion Notes
- Task created after TASK-0086 commit `6435f6d` and successful post-commit public gate.
- Added optional `BaselineEvaluation` input to SARIF, HTML report, and Web UI generators with centralized scan-alignment validation.
- Added `--baseline` support to `sarif`, `report`, and `webui`; all command JSON responses reuse the shared sanitize-only baseline DTO.
- SARIF results add `baselineStatus`, `baselineFingerprint`, and `baselineOccurrence` properties without raw matches.
- HTML report and Web UI add existing/new metrics and per-finding Baseline status while keeping all findings visible.
- Added focused Core/CLI/output tests; 162/162 tests currently pass.
- Disposable output smoke passed: SARIF parsed with `existing` status and no raw token; report and Web UI contained baseline summaries/status columns.
- Final validation passed: restore, Release build with 0 warnings/errors, 162/162 tests, clean scan, doctor PASS, JSON/SARIF parse, disposable baseline-output smoke, sample smoke, hygiene scans, CLI/config/v0.2/v1.0 gates, and local package verification.
- The pre-commit public release gate reported only the expected dirty-working-tree blockers; package metadata remained clean and the gate must be rerun after commit.
