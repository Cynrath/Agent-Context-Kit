# TASK-0128 Hosted Release-Candidate Evidence Hardening

## Purpose
Make hosted candidate evidence exact-SHA, privacy-safe, permission-minimal, and suitable for future GO/NO-GO decisions.

## Current State
The manual RC workflow exists, but its dedicated reviewed hosted evidence remains open.

## Scope
Harden workflow inputs/concurrency/permissions/output summaries, static gates, and execute read-only hosted evidence for an exact reviewed commit.

## Out Of Scope
Publishing, tagging, release editing, settings changes, or artifact upload unless explicitly safe and required.

## Affected Files
RC workflow/scripts/tests/docs/evidence/queue/handoff files.

## Implementation
Require exact commit/version inputs, verify master ancestry/state, preserve no-upload boundaries, add negative static checks, dispatch and record results.

## Security/Privacy Boundary
No raw findings, secrets, local paths, package credentials, SARIF upload, or repository write permissions.

## Compatibility
No product contract change.

## Acceptance Criteria
Workflow gate passes and exact commit completes Windows/Ubuntu/macOS evidence with documented results.

## Tests
Static permission/input checks, invalid SHA/version tests, local evidence scripts.

## Validation
Full local gates and hosted run inspection.

## Rollback
Revert workflow hardening; retain previous evidence as historical.

## Completion Evidence
Local hardening implemented: exact SHA/candidate/predecessor inputs, current origin/master validation, per-commit/version concurrency, read-only permissions, no-upload boundaries, and positive/negative input tests. First hosted run `27478415124` passed all Windows evidence and all non-performance Ubuntu/macOS checks; those two runners exposed a null `$env:TEMP` portability defect before benchmark execution. Portable temp fallback is pending repeat validation and dispatch without changing the 2,000-file/30-second threshold.

## Commit
`ci: harden hosted release candidate evidence`

## Push
Normal push after validation.

## Hosted Checks
Standard 8/8 plus dedicated RC workflow jobs.
