# PROJECT-CONTROL-0102 Alpha.2 Automated Release

## Purpose
Deliver `v0.2.0-alpha.2` from TASK-0116 through TASK-0125, including validated code/docs changes, normal pushes, exact-commit hosted checks, OIDC NuGet publication, GitHub pre-release creation, and post-publish verification.

## Current State
`master` and `origin/master` are aligned at `8dadd16`; `v0.2.0-alpha.1` is published and the target tag does not exist.

## Scope
Coordinate TASK-0116 through TASK-0125 in order and preserve evidence for every local and hosted gate.

## Out Of Scope
Force push, history rewrite, moving an existing tag, mutating an immutable NuGet version, API-key credentials, or deletion of user work.

## Affected Files
Task/docs files, focused Core/CLI/tests, release scripts, workflows, version metadata, release notes, README files, and Codex handoff files.

## Implementation Steps
Create all tasks; complete hardening; validate; add release automation; prepare alpha.2; push; wait for exact-commit checks; publish; verify; sync post-publish state.

## Security/Privacy Boundary
No secret value may be printed, persisted, committed, or passed outside the short-lived OIDC publish step. Repository content remains local except normal Git/GitHub release artifacts.

## Backward Compatibility
JSON v2, SARIF 2.1.0, config v1, baseline v1, rule IDs, and documented exit codes remain compatible.

## Database Impact
None.

## Admin Impact
Uses the preconfigured `nuget-release` GitHub environment only during authorized publication.

## Permission Impact
Release job alone receives `contents: write` and `id-token: write`; validation remains read-only.

## SEO/I18n Impact
Release documentation and localized README text are synchronized; no SEO surface is introduced.

## Audit/Security Impact
Exact commit, package identity, tag target, hosted checks, and publication evidence are recorded without raw findings or credentials.

## Acceptance Criteria
TASK-0116–0125 complete; alpha.2 is published and installable; pre/post publication hosted checks pass 8/8; working tree is clean and `HEAD == origin/master`.

## Tests
All task-specific tests plus full build/test/scan/doctor/sample/contract/performance/release gates.

## Validation
Validate local artifacts, hosted Actions, NuGet availability, GitHub Release, exact tag SHA, and clean final Git state.

## Rollback
Before publication, revert normal commits. After publication, never overwrite the immutable version; fix forward with a new version.

## Completion Evidence
Pending.

## Commit
Use small logical commits; final release and post-publish commits must identify their exact SHA.

## Push
Normal push to `master` is authorized after successful validation.

## Hosted Checks
Require two CI jobs, three published-package smoke jobs, and three source-package smoke jobs for the exact release and post-publish commits.
