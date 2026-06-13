# TASK-0124 v0.2.0-alpha.2 Hosted Validation And Publication

## Purpose
Publish alpha.2 only after exact-release-commit hosted validation succeeds.

## Current State
Target tag/release/package do not exist at task creation.

## Scope
Confirm 8/8 checks; dispatch `release.yml`; publish via OIDC; verify NuGet; create exact tag and GitHub pre-release with package assets.

## Out Of Scope
Force-moving tags, republishing an existing package, API keys, or publication from another commit.

## Affected Files
No required local product files; task/handoff evidence may be updated after hosted completion.

## Implementation Steps
Collect exact SHA checks; dispatch inputs; watch run; handle idempotent partial states; verify package/tag/release.

## Security/Privacy Boundary
Only OIDC short-lived credentials are used; no secret value is exposed.

## Backward Compatibility
Publishes the already validated immutable package.

## Database Impact
None.

## Admin Impact
Uses authorized GitHub Release and NuGet publication operations.

## Permission Impact
Release job scoped `contents: write` and `id-token: write` only.

## SEO/I18n Impact
Release notes are public; no runtime localization change.

## Audit/Security Impact
Hosted run, exact SHA, NuGet state, tag target, and release URL are evidence.

## Acceptance Criteria
Pre-release Actions 8/8; release workflow succeeds; package exists; tag targets release SHA; GitHub pre-release and assets exist.

## Tests
Hosted release workflow includes the full local/package smoke suite.

## Validation
Use `gh run view`, NuGet package query/install, `git rev-list`, and `gh release view`.

## Rollback
NuGet is immutable. If GitHub completion fails after publish, resume only missing tag/release steps; never republish.

## Completion Evidence
Pre-publication exact commit `f540479a92cbe66097f6796553828ee49ddd5512` passed the required eight standard hosted jobs. Workflow run `27470659578` used NuGet OIDC Trusted Publishing and successfully published `AgentContextKit` `0.2.0-alpha.2`, then stopped before tag/GitHub Release creation because the Linux verifier assumed `$env:TEMP`. The immutable package was detected without republishing; exact tag `v0.2.0-alpha.2` was pushed at `f540479a92cbe66097f6796553828ee49ddd5512`, and the GitHub pre-release was created with the validated `.nupkg` and `.snupkg` assets.

## Commit
Publication itself does not require a new code commit.

## Push
Release workflow operates on the already pushed exact commit.

## Hosted Checks
8/8 exact commit plus successful `release.yml` run are mandatory.
