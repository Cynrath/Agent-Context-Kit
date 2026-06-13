# PROJECT-CONTROL-0102 Alpha.2 Automated Release

## Purpose
Deliver `v0.2.0-alpha.2` from TASK-0116 through TASK-0125, including validated code/docs changes, normal pushes, exact-commit hosted checks, OIDC NuGet publication, GitHub pre-release creation, and post-publish verification.

## Current State
Completed. NuGet `0.2.0-alpha.2`, exact tag `v0.2.0-alpha.2`, and the GitHub pre-release are published from package commit `f540479a92cbe66097f6796553828ee49ddd5512`; TASK-0125 post-publish synchronization and hosted validation are complete.

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
TASK-0116 through TASK-0125 are complete. Exact package commit `f540479a92cbe66097f6796553828ee49ddd5512` passed the eight standard hosted jobs. Release run `27470659578` validated and packed the exact commit and published NuGet `0.2.0-alpha.2` through OIDC Trusted Publishing. Its post-publish verifier exposed an Ubuntu temporary-directory assumption after the immutable publish; recovery did not republish. Exact tag `v0.2.0-alpha.2` and the GitHub pre-release were completed at the package commit with validated `.nupkg` and `.snupkg` assets. Global installation and full published-package smoke passed locally. Post-publish commit `ead65120928835419fb91bf695e845721620c394` passed all eight standard hosted jobs.

## Commit
Use small logical commits; final release and post-publish commits must identify their exact SHA.

## Push
Normal push to `master` is authorized after successful validation.

## Hosted Checks
Require two CI jobs, three published-package smoke jobs, and three source-package smoke jobs for the exact release and post-publish commits.
