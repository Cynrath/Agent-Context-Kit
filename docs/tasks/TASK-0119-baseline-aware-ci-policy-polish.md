# TASK-0119 Baseline-Aware CI Policy Polish

## Purpose
Audit baseline-aware CI decisions for deterministic, compatible classification and exit behavior.

## Current State
Baseline v1 distinguishes existing/new findings and CI blocks new High/Critical findings.

## Scope
Review missing/invalid baselines, duplicate findings, path normalization, severity changes, and exit parity; patch confirmed gaps only.

## Out Of Scope
Baseline schema/fingerprint changes, silent baseline creation, or acceptance of new Critical findings.

## Affected Files
Baseline policy/store, CLI behavior, tests, and baseline docs.

## Implementation Steps
Audit classifier and CLI paths; reproduce edge cases; apply compatible fixes; add positive/negative tests.

## Security/Privacy Boundary
Baseline and outputs remain sanitized and repository-relative.

## Backward Compatibility
Baseline schema 1 and fingerprint algorithm v1 remain unchanged.

## Database Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/I18n Impact
Localized human output remains semantically aligned.

## Audit/Security Impact
Prevents baseline policy from hiding newly introduced severe findings.

## Acceptance Criteria
New High/Critical findings block; existing findings classify consistently; malformed/missing baseline errors stay structured; paired tests pass.

## Tests
Focused baseline/exit parity tests plus contract and full suite.

## Validation
Run baseline create/update/scan CI/JSON/SARIF scenarios.

## Rollback
Revert baseline-specific changes.

## Completion Evidence
Completed locally on 2026-06-13. Baseline classification now treats a matching fingerprint with increased severity as `new`; equal or lower severity remains `existing`. Baseline schema 1 and fingerprint algorithm v1 are unchanged. Core and CLI CI tests prove Critical escalation returns exit code `2`.

## Commit
Include with compatible product hardening if changed.

## Push
Normal push after validation.

## Hosted Checks
All exact-commit CI and smoke jobs must pass.
