# TASK-0090 Hosted Baseline Upgrade And Performance Workflow Design

## Purpose
Add a manual, cross-platform GitHub Actions workflow that can produce fresh release-candidate evidence for predecessor config compatibility, current-source package behavior, baseline policy, and the synthetic performance tripwire.

## Scope
- Add a `workflow_dispatch`-only Windows/Ubuntu/macOS workflow.
- Install published predecessor `AgentContextKit` `0.2.0-alpha.1` in an isolated tool path.
- Pack current source with a run-unique prerelease package version and install it in a separate tool path.
- Verify predecessor scan JSON, unchanged predecessor config, current-source `config-check`, baseline creation/classification, SARIF parse, and final clean scan.
- Run the existing 2,000-file/30-second synthetic performance tripwire on each OS.
- Add a local static workflow gate and maintainer execution/interpretation docs.

## Out Of Scope
- No workflow dispatch, push, artifact upload, SARIF upload, Code Scanning activation, tag, release, NuGet publish, branch protection, or repository setting change.
- No production SLA, memory benchmark, cancellation implementation, or release-candidate approval.

## Affected Files
- `.github/workflows/release-candidate-evidence.yml`
- `scripts/check-release-candidate-workflow.ps1`
- `docs/RC_HOSTED_EVIDENCE.md`
- GitHub Actions, upgrade, performance, release evidence, roadmap, queue, release validation, and Codex handoff docs.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
The workflow uses only `contents: read`. Manual dispatch and observing hosted results require maintainer GitHub access. No security-event or artifact-write permission is granted.

## SEO/i18n Impact
None. Workflow and maintainer evidence docs are English and machine-oriented.

## Audit/Security Impact
The workflow uses only synthetic repositories and committed sanitized fixtures. It must not upload artifacts or log raw secret values, local user paths, customer data, or private source.

## Acceptance Criteria
- Workflow is manual-only and has a three-OS matrix.
- Published predecessor and current source use separate tool paths.
- Current source package version is unique per run so NuGet cannot satisfy the candidate install from the public predecessor package.
- Config content hash is unchanged after predecessor/current-source checks.
- `config-check`, baseline-aware scan, SARIF parse, and performance tripwire run on each OS.
- Local static gate rejects missing safety/evidence markers.
- Hosted execution remains a documented maintainer action.

## Tests
Validate workflow syntax where local tooling permits, run the static workflow gate, then run restore/build/test, source scan/doctor, JSON/SARIF/sample smoke, hygiene, contract/readiness/evidence/release gates, local package verification, and post-commit public gates.

## Risks
- GitHub runner timing can vary and the generous threshold is not an SLA.
- NuGet availability can block predecessor install independently of source quality.
- A future package version change may require workflow/docs updates.
- Static YAML checks cannot replace an actual hosted run.

## Rollback Plan
Revert the TASK-0090 commit. No hosted run or remote configuration is changed locally.

## Completion Notes
- Task created after TASK-0089 commit `1284646` and successful post-commit public gate.
- Added manual-only `.github/workflows/release-candidate-evidence.yml` with `contents: read`, Windows/Ubuntu/macOS matrix, isolated predecessor/source tool paths, run-unique candidate package version, config hash check, `config-check`, baseline/SARIF, and performance steps.
- Added `scripts/check-release-candidate-workflow.ps1`; the static safety/evidence gate passed.
- Local Windows reproduction passed predecessor/source package installation, predecessor JSON scan, current-source config/baseline/SARIF checks, config immutability, and final scan.
- The first local reproduction found fixture self-noise; the predecessor config fixture now ignores only its own non-Critical config keyword matches. Critical suppression remains blocked.
- No local YAML parser/actionlint was available; hosted syntax and three-OS behavior remain a maintainer push/manual-dispatch action.
- Full validation passed: restore, Release build, 169/169 tests, clean scan, doctor PASS, JSON/SARIF parse, sample smoke, hygiene, workflow/CLI/config/v0.2/v1.0/RC evidence gates, and local package verification.
- The pre-commit public release gate failed only on the expected dirty working tree; rerun it after commit.
