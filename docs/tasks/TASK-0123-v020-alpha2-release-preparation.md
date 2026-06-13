# TASK-0123 v0.2.0-alpha.2 Release Preparation

## Purpose
Prepare and push the exact alpha.2 release commit after TASK-0116–0122 pass.

## Current State
Source/package/CLI metadata and source-package smoke are being moved to `0.2.0-alpha.2`; published-package smoke must remain pinned to `0.2.0-alpha.1` until publication.

## Scope
Bump source/CLI/source-smoke/release verification to `0.2.0-alpha.2`; update changelog/release notes/handoff/queue; pack, inspect, and temporary-install the candidate.

## Out Of Scope
Changing published-package smoke or public README install commands before NuGet publication.

## Affected Files
Csproj, CLI version, source smoke workflow, release scripts/docs, CHANGELOG, release notes, roadmap/queue/handoff/context.

## Implementation Steps
Bump controlled references; build/test; pack to temp; inspect metadata/content; install temp tool; run command/smoke checks; commit and push.

## Security/Privacy Boundary
Candidate packages and generated reports stay temporary/ignored; no credentials are used.

## Backward Compatibility
Schemas, command names, exit codes, config/baseline formats, and rule IDs remain stable.

## Database Impact
None.

## Admin Impact
None until hosted publication.

## Permission Impact
Normal Git push only.

## SEO/I18n Impact
English/Turkish release wording remains aligned.

## Audit/Security Impact
Package metadata/content and exact release SHA are recorded.

## Acceptance Criteria
Candidate reports alpha.2, local package installs and passes smoke, published smoke stays alpha.1, clean commit is pushed.

## Tests
Full validation plus package metadata/content/install smoke.

## Validation
Run preparation and verification scripts for alpha.2.

## Rollback
Before publication, revert the release-preparation commit; never reuse alpha.2 after publication.

## Completion Evidence
Release build passed with zero warnings and zero errors; 186/186 tests passed. Source scan was clean, doctor passed, JSON/SARIF parsed, samples and all contract/readiness/security/documentation gates passed, and the unchanged 2,000-file/30-second tripwire completed in 3.704 seconds through the RC gate and 3.685 seconds standalone. Candidate `.nupkg` and `.snupkg` entries were inspected. A temporary install reported `AgentContextKit 0.2.0-alpha.2` and completed init, scan, generate, task, report, Web UI, SARIF, prompt-pack, context-export, fake-secret exit `2`, cleanup, and final clean scan. Commit `63ef69c` passed the eight standard hosted jobs. Its first manual release dispatch stopped safely before package/tag/release creation after exposing a Windows PowerShell 5.1 link-check compatibility gap; the focused fix and regression case are included in the replacement release commit.

## Commit
Dedicated alpha.2 release-preparation commit.

## Push
Push normally to `master` after green local validation.

## Hosted Checks
Wait for exact release SHA 8/8 before dispatching publication.
