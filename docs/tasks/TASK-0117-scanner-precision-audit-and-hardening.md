# TASK-0117 Scanner Precision Audit And Hardening

## Purpose
Audit scanner precision and fix only demonstrated false positives/negatives without weakening Critical secret detection.

## Current State
Scanner has stable rule IDs, safe-domain handling, fixture-noise controls, and 178 tests.

## Scope
Review token/domain/path/secret boundaries; add narrowly scoped fixes and positive/negative regression tests where evidence warrants.

## Out Of Scope
Broad allowlists, blanket ignores, severity reduction for real secrets, or schema changes.

## Affected Files
Scanner Core files, focused tests, scanner/security docs, and task evidence.

## Implementation Steps
Audit rules and fixtures; reproduce defects; patch minimally; test both matching and non-matching cases; document unchanged areas.

## Security/Privacy Boundary
Raw secret-like matches must not enter JSON, SARIF, docs, logs, or committed fixtures.

## Backward Compatibility
Rule IDs, JSON v2, SARIF, config v1, and exit behavior remain stable.

## Database Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/I18n Impact
No expected impact.

## Audit/Security Impact
Improves signal quality while retaining Critical coverage.

## Acceptance Criteria
Every code change has positive and negative tests; synthetic Critical patterns still block; no speculative code churn.

## Tests
Focused scanner tests plus full suite and CLI scan gates.

## Validation
Run scan human/CI/JSON and verify sanitized findings.

## Rollback
Revert focused scanner/test/docs changes.

## Completion Evidence
Completed locally on 2026-06-13. Email, phone, and IP scanning now evaluates all distinct candidates; generic token assignment rejects hyphenated metadata such as `id-token`; Windows path detection requires a real boundary; plain numeric run IDs are not phone findings. Human/JSON/Web UI output omits raw matches while JSON retains `match: null` compatibility. Positive and negative tests pass and Critical patterns remain active.

## Commit
Include with compatible product hardening.

## Push
Normal push after full validation.

## Hosted Checks
All exact-commit CI and smoke jobs must pass.
