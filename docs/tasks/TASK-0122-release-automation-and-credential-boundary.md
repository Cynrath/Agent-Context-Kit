# TASK-0122 Release Automation And Credential Boundary

## Purpose
Add exact-commit, idempotent GitHub release automation using NuGet Trusted Publishing only.

## Current State
Publication is manual; Trusted Publishing is configured for `Cyranth`, `Cynrath/agent-context-kit`, `release.yml`, and `nuget-release`.

## Scope
Add workflow_dispatch release workflow, prepare/verify scripts, package inspection, installed-tool smoke, OIDC publish, tag/release sequencing, and docs.

## Out Of Scope
API keys, repository secrets, fork/PR publishing, automatic release triggers, force-moving tags, or overwriting NuGet versions.

## Affected Files
`.github/workflows/release.yml`, release scripts/docs/tests/gates, package metadata where required.

## Implementation Steps
Validate inputs/SHA/version/state; run all gates; pack/inspect/install/smoke; OIDC login; publish or safely resume; verify NuGet; tag exact SHA; create pre-release/assets.

## Security/Privacy Boundary
`NuGet/login@v1` output is short-lived and used only by `dotnet nuget push`; it is never echoed, written, or retained.

## Backward Compatibility
Workflow does not alter CLI contracts; scripts accept explicit version/SHA.

## Database Impact
None.

## Admin Impact
Uses the existing protected `nuget-release` environment.

## Permission Impact
Default `contents: read`; release job alone gets `contents: write` and `id-token: write`.

## SEO/I18n Impact
None beyond release documentation.

## Audit/Security Impact
Concurrency, exact-SHA checks, idempotency, permission scoping, and immutable-version behavior are enforced.

## Acceptance Criteria
Static/local workflow gates pass; workflow is manual-only; OIDC is the sole credential; publish failure prevents tag/release; partial success resumes safely.

## Tests
PowerShell script tests, workflow static checks, local package/install smoke, and full repository validation.

## Validation
Review YAML, run scripts in non-publish mode, and pass release/readiness/security gates.

## Rollback
Remove workflow/scripts/docs before publication; after publication retain immutable evidence and fix forward.

## Completion Evidence
Completed locally on 2026-06-13. Added manual exact-SHA `release.yml`, scoped validation/release permissions, per-version concurrency, OIDC-only `NuGet/login@v1` for `Cyranth`, idempotent package/tag/release state handling, short-retention validated package transfer, package preparation/static gates, and disposable installed-package verification. Local pack produced `.nupkg` and `.snupkg`; full installed-tool smoke and synthetic-secret exit `2` passed without exposing the value.

## Commit
Dedicated release-automation commit.

## Push
Normal push after local validation.

## Hosted Checks
Standard workflows must pass before the release workflow is dispatched.
