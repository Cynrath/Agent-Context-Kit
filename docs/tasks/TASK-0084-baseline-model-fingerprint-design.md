# TASK-0084 Baseline Model And Fingerprint Design

## Purpose
Create the safe, deterministic core model required for future baseline-aware CI without changing current scan behavior or exposing raw finding matches.

## Scope
- Define a versioned baseline manifest and entry model in Core.
- Define deterministic finding fingerprint generation from stable rule ID, normalized repository-relative path, and non-secret location metadata.
- Normalize separators and path casing so equivalent Windows/Linux/macOS finding locations produce the same fingerprint.
- Reject absolute, traversal, empty, and otherwise unsafe baseline paths.
- Add focused tests for determinism, cross-platform normalization, privacy, ordering, schema defaults, and invalid input.
- Document the schema, privacy boundary, compatibility rules, and future integration points.

## Out Of Scope
- No baseline CLI command, file read/write integration, config field, scan classification, CI exit change, JSON/SARIF/report integration, or migration workflow.
- No raw match, message, secret value, username, machine name, absolute path, timestamp, or repository root in fingerprints.
- No version bump, push, tag, GitHub Release change, NuGet publish, or remote write.

## Affected Files
- `src/AgentContextKit.Core/Models.cs` or a focused Core baseline file.
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs` or focused baseline tests.
- `docs/BASELINE_MODEL.md`
- `docs/ARCHITECTURE.md`
- `docs/SECURITY_MODEL.md`
- `docs/V030_ROADMAP_DECISION.md`
- `docs/V100_GAP_ANALYSIS.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `CHANGELOG.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. The model remains local-only and performs no remote access.

## SEO/i18n Impact
No localized CLI output changes. Baseline JSON field names remain English and language-independent.

## Audit/Security Impact
Fingerprints must be one-way SHA-256 identifiers over sanitized metadata only. Critical findings remain visible; this task does not suppress or accept any finding.

## Acceptance Criteria
- Baseline schema has an explicit independent schema version.
- Equivalent relative paths produce identical fingerprints across path separator and drive-casing variants after repository-relative normalization.
- Different rule IDs, paths, or location metadata produce different fingerprints.
- Fingerprint input and baseline entries exclude raw matches/messages/secrets and machine-specific absolute roots.
- Invalid or escaping paths are rejected.
- Entry ordering can be made deterministic.
- Existing CLI/JSON/SARIF/exit behavior is unchanged.
- Full local validation passes.

## Tests
Add focused xUnit coverage, then run restore/build/test, scan/doctor, JSON/SARIF/sample smoke, hygiene scans, contract/config/readiness/release gates, and public release gate checks.

## Risks
- Case normalization can collapse distinct files on case-sensitive repositories if applied too broadly.
- Including mutable line numbers can make baselines noisy; excluding all location metadata can merge distinct findings.
- A future scanner integration may require a documented fingerprint schema increment if canonicalization changes.

## Rollback
Revert the TASK-0084 commit. No existing command, package version, baseline file, or remote state is changed.

## Completion Notes
- Task created after TASK-0083 gap analysis.
- Added `BaselineSchema`, `BaselineLocation`, `BaselineEntry`, `BaselineManifest`, and `BaselineFingerprint` in a focused Core module.
- Fingerprint v1 uses SHA-256 over algorithm ID, normalized rule ID, repository-relative path, and optional positive line/column metadata; severity and raw finding content are excluded.
- Path normalization handles separator and Unicode variants, preserves case-sensitive repository identity, and rejects absolute, URI-like, traversal, control-character, and empty paths.
- Added 9 focused test methods covering determinism, path case, Unicode, severity independence, unsafe paths, location validation, privacy, deterministic ordering, and duplicate rejection.
- Added baseline model, architecture, security, roadmap, 1.0 gap, queue, index, map, changelog, and Codex handoff documentation.
- Validation passed: restore, Release build with 0 warnings/errors, 136/136 tests, clean scan, doctor PASS, JSON/SARIF parse, sample smoke, hygiene scans, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; rerun it after commit.
- The post-commit public release gate passed with no blocking items; only the expected post-release HEAD warning and manual remote-tag verification note remain.
