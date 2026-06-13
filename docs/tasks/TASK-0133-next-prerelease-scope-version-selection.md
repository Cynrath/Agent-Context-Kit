# TASK-0133 Next Prerelease Scope And Version Selection

## Purpose
Select the smallest SemVer-compatible prerelease version and freeze its evidence-backed scope.

## Current State
Current release is `0.2.0-alpha.2`; TASK-0126–0132 may add release/security automation without intended breaking CLI changes.

## Scope
Review actual diff, open blockers, compatibility, changelog, package contract, and select/defer the next version with GO prerequisites.

## Out Of Scope
Version bump, tag, publish, release creation, or claiming GO before all required evidence exists.

## Affected Files
Scope/version decision, roadmap, changelog, release checklist, queue/handoff and TASK-0134 plan.

## Implementation
Use SemVer and real changes; prefer the smallest prerelease increment; document exact inclusion/exclusion and blocker disposition.

## Security/Privacy Boundary
No credential or private evidence in planning docs.

## Compatibility
Explicitly record CLI/schema/config/baseline/SARIF/package compatibility.

## Acceptance Criteria
One version is selected or publication is deferred with concrete unmet conditions; no metadata bump occurs here.

## Tests
Version/reference consistency and documentation gates.

## Validation
Diff review, contract gates, blocker/decision register review.

## Rollback
Revert planning decision before release preparation.

## Completion Evidence
Pending.

## Commit
`docs: select next prerelease scope`

## Push
Normal push after validation.

## Hosted Checks
Standard 8/8 after push if the planning commit changes active release docs/workflows.
