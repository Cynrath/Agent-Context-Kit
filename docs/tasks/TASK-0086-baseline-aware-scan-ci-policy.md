# TASK-0086 Baseline-Aware Scan And CI Policy

## Purpose
Add explicit local baseline file creation/loading and baseline-aware scan/CI classification while preserving existing default scan behavior.

## Scope
- Add safe JSON serialization/loading for baseline schema v1 under a repository-relative path.
- Add an explicit baseline creation/update command or option that never runs implicitly.
- Add opt-in baseline-aware `scan` classification for existing versus new findings.
- Keep all findings visible, including baseline Critical findings.
- Make CI exit decisions depend on new High/Critical findings only when baseline mode is explicitly requested.
- Add human/JSON output metadata and focused tests for create/load/classify/exit/privacy/error behavior.
- Document migration, review, and rollback workflow.

## Out Of Scope
- No SARIF/HTML/Web UI baseline metadata; TASK-0087 owns those outputs.
- No automatic baseline update, wildcard acceptance, Critical suppression, remote baseline storage, Code Scanning activation, or config-driven implicit baseline.
- No version bump, push, tag, GitHub Release change, NuGet publish, or remote write.

## Affected Files
- Core baseline serialization/classification services and interfaces.
- CLI parsing/help/output/exit policy.
- focused tests.
- baseline, CLI, JSON, exit-code, security, architecture, roadmap, queue, changelog, and Codex docs.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. Baselines are local repository files and require explicit commands/options.

## SEO/i18n Impact
Human output must support English/Turkish where added. JSON field names remain English and language-independent.

## Audit/Security Impact
Baseline files must omit raw matches/messages/absolute paths. Critical findings remain visible. Baseline creation and replacement require explicit user intent and reviewable repository-relative output.

## Acceptance Criteria
- Existing `ackit scan` and `ackit scan --ci` behavior is unchanged without a baseline option.
- Baseline creation is explicit, deterministic, sanitized, and refuses overwrite unless an explicit update flag is supplied.
- Baseline-aware scan classifies every finding as existing or new.
- Baseline-aware CI returns non-zero only for new High/Critical findings, while still reporting existing Critical findings.
- Missing/invalid/incompatible baseline files fail safely with documented exit behavior.
- Human/JSON parity and focused tests pass.
- Full local validation passes.

## Tests
Add Core and CLI tests, then run restore/build/test, scan/doctor, JSON/SARIF/sample smoke, hygiene scans, contract/config/readiness/release gates, and public release gate checks.

## Risks
- A baseline can be misread as suppression if output does not keep existing findings visible.
- Fingerprints without source locations can collide for multiple same-rule findings in one file.
- New CLI options can break help/contract gates if docs and tests drift.

## Rollback
Revert the TASK-0086 commit and remove any locally generated baseline file after review. Existing default scan behavior remains the fallback.

## Completion Notes
- Task created after TASK-0085 config diagnostics.
- Added `BaselineStore` with repository-relative JSON path enforcement, explicit update requirement, schema/algorithm validation, duplicate detection, and fingerprint integrity checks.
- Added `BaselineClassifier` with deterministic occurrence metadata and complete existing/new classification without storing raw matches or messages.
- Added current-source `ackit baseline` and opt-in `ackit scan --baseline <path>` workflows.
- Preserved default scan/CI behavior; baseline-aware CI evaluates only new High/Critical findings while all findings remain visible.
- Added additive baseline JSON metadata, stable `ACKITBASE` errors, CLI/Core tests, contract gate updates, and security/architecture/user documentation.
- Final validation passed: restore, Release build with 0 warnings/errors, 154/154 tests, clean scan, doctor PASS, JSON/SARIF parse, sample smoke, hygiene scans, CLI/config/v0.2/v1.0 gates, and local package verification.
- The pre-commit public release gate reported only the expected dirty-working-tree blockers; package metadata remained clean and the gate must be rerun after commit.
