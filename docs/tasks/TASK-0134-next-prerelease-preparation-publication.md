# TASK-0134 Next Prerelease Preparation Publication And Verification

## Purpose
Prepare, publish, and verify the selected prerelease only if all exact-version/commit GO conditions are genuinely satisfied.

## Current State
Blocked by default until TASK-0133 selects a version and all required P0/P1 decisions/evidence are recorded.

## Scope
Conditional version bump, changelog/release notes, pack/inspect/install smoke, exact-SHA checks, OIDC publish, exact tag/pre-release, post-publish sync and final hosted checks.

## Out Of Scope
Publishing through API keys, bypassing blockers, force operations, tag movement, version reuse, or fabricated approval.

## Affected Files
Version metadata, workflows/scripts, changelog/release notes, README/status/handoff/queue/task files.

## Implementation
Evaluate GO packet first. If GO, execute the immutable release sequence. If blocked, complete preparation analysis and record exact blockers without attempting publication.

## Security/Privacy Boundary
OIDC-only publish; least-privilege jobs; no secret output; generated package/SBOM/SARIF/HTML remains temporary unless explicitly reviewed as a release asset.

## Compatibility
Must match TASK-0133 scope and preserve declared contracts.

## Acceptance Criteria
Either full release/post-release verification succeeds, or a truthful blocked result identifies unmet external/human conditions while all independent work is complete.

## Tests
Complete local gates, package/install smoke, hosted RC evidence, standard 8/8, release recovery verification, and post-publish 8/8 if published.

## Validation
Exact SHA/tag/release/package/global install and Actions verification.

## Rollback
Before publish, revert ordinary commits. After publish, fix forward with a new version; never move/replace immutable artifacts.

## Completion Evidence
Pending.

## Commit
Dedicated preparation and post-publish commits if GO; documentation-only blocker closure if NO-GO.

## Push
Normal push after validation.

## Hosted Checks
Exact candidate 8/8, release workflow, verify-existing recovery run, and final post-publish 8/8 when publication occurs.
