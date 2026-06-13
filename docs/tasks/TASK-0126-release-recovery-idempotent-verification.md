# TASK-0126 Release Recovery And Idempotent Verification

## Purpose
Add a safe `verify-existing` release operation that proves an immutable published release without credentials or remote mutation.

## Current State
Release run `27470659578` published alpha.2 through OIDC but failed during verification; exact tag/release were completed through safe recovery.

## Scope
Split `publish` and `verify-existing` jobs/permissions, verify NuGet/tool/tag/release/assets/metadata/digests, and add positive/negative/idempotency tests and workflow gates.

## Out Of Scope
Republishing alpha.2, moving its tag, editing its release, API keys, or granting write permissions to verification.

## Affected Files
`.github/workflows/release.yml`, release scripts/tests/docs, queue/handoff files.

## Implementation
Add operation/version/automation/release SHA inputs; isolate jobs; add a read-only verifier and static/behavior tests; dispatch against alpha.2 after standard checks pass.

## Security/Privacy Boundary
`verify-existing` receives no `id-token: write`, no `contents: write`, no NuGet login, and performs no tag/release/package mutation.

## Compatibility
Existing published artifacts remain immutable; publish behavior remains exact-master and OIDC-only.

## Acceptance Criteria
Static gate proves permission separation; local tests pass; hosted `verify-existing` completes green for alpha.2 and can be rerun idempotently.

## Tests
Positive exact-state verification, wrong SHA/version/asset negative cases, no-write marker checks, repeated invocation.

## Validation
Full local gates, standard 8/8, then manual `verify-existing` run with automation HEAD and alpha.2 release SHA.

## Rollback
Revert workflow/script changes; existing release remains untouched.

## Completion Evidence
Local implementation complete: publish and verify-existing jobs are permission-isolated; static workflow checks and positive/negative/idempotency fixtures pass. Hosted 8/8 and the read-only alpha.2 verification dispatch remain the completion evidence after push.

## Commit
`ci: add read-only release recovery verification`

## Push
Normal `master` push after local validation.

## Hosted Checks
Standard 8/8 plus green `release.yml` `verify-existing` run.
