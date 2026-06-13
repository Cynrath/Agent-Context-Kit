# TASK-0118 Suppression Audit Polish

## Purpose
Harden suppression audit accuracy and sanitization for configured non-Critical suppressions.

## Current State
Human/JSON output records sanitized suppression metadata; Critical rules cannot be suppressed.

## Scope
Audit deduplication, reasons, paths, counts, and Critical rejection; patch only confirmed gaps with paired tests.

## Out Of Scope
Critical suppression, raw-match output, new schema versions, or broad ignore defaults.

## Affected Files
Suppression/scanner Core code, CLI projection, tests, and suppression docs.

## Implementation Steps
Trace suppression records; test ignored ID/path/domain behavior; fix deterministic sanitized audit output if needed.

## Security/Privacy Boundary
Audit records contain rule/severity/category/repository-relative path/reason only, never matched content.

## Backward Compatibility
Existing additive JSON fields and config keys remain compatible.

## Database Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/I18n Impact
Keep human labels localized where changed.

## Audit/Security Impact
Makes suppression decisions reviewable without leaking sensitive values.

## Acceptance Criteria
Counts and reasons are deterministic; Critical suppression stays rejected; raw values are absent; positive/negative tests pass.

## Tests
Focused suppression and config validation tests plus full suite.

## Validation
Run scan human/JSON with safe fixtures and inspect sanitized output.

## Rollback
Revert suppression-specific changes.

## Completion Evidence
Completed locally on 2026-06-13. Identical suppression records are deduplicated in scanner and configured safe-domain audit output. Repeated safe-domain emails produce one sanitized audit record; Critical suppression tests remain green.

## Commit
Include with product hardening if changed.

## Push
Normal push after validation.

## Hosted Checks
All exact-commit CI and smoke jobs must pass.
