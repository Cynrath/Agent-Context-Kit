# TASK-0125 v0.2.0-alpha.2 Post-Publish Verification

## Purpose
Verify the published alpha.2 tool, sync public install/status references, and prove the final post-publish commit across all platforms.

## Current State
Begins only after NuGet, exact tag, and GitHub pre-release exist.

## Scope
Pin published smoke and README installs to alpha.2; update status docs/tasks/context; reinstall global tool; run disposable full CLI smoke; commit/push; wait for final 8/8.

## Out Of Scope
New product features, changing the published package, or moving the release tag.

## Affected Files
Published smoke workflow, README files, changelog/release/validation/roadmap/queue/handoff/task/context docs.

## Implementation Steps
Wait for package availability; uninstall/install alpha.2; run version/help/full disposable smoke including synthetic secret exit 2; update docs/workflow; validate; commit/push; monitor final checks.

## Security/Privacy Boundary
Synthetic secret material exists only in a disposable local file, is not printed or committed, and is deleted before final scan.

## Backward Compatibility
Post-publish docs/workflow pins change only; released CLI contracts remain immutable.

## Database Impact
None.

## Admin Impact
None beyond observing hosted checks.

## Permission Impact
Normal Git push only.

## SEO/I18n Impact
English/Turkish install and release status remain aligned.

## Audit/Security Impact
Global install, full smoke, fake-secret enforcement, final clean scan, and cross-platform results become release evidence.

## Acceptance Criteria
Global alpha.2 installs; full smoke passes; fake secret exits 2; final scan passes; docs/workflow are synced; final HEAD is pushed and Actions pass 8/8.

## Tests
Full local suite plus disposable installed-tool smoke and hosted cross-platform matrices.

## Validation
Verify clean Git state, HEAD/origin equality, exact tag SHA, GitHub pre-release, NuGet availability, and final 8/8.

## Rollback
Revert post-publish docs/workflow commit if incorrect; published package/tag remain immutable and fixes use a new version.

## Completion Evidence
Pending.

## Commit
Dedicated post-publish verification/docs sync commit.

## Push
Push normally to `master` after validation.

## Hosted Checks
Final post-publish HEAD must pass 2 CI, 3 published smoke, and 3 source smoke jobs.
