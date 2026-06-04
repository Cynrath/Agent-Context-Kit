# TASK-0043: Release Tag And Package Readiness

## Status
Completed.

## Purpose
Prepare `v0.1.0-alpha.1` for local release validation, package verification, and local annotated tag readiness without pushing tags or publishing packages.

## Scope
- Fill `CHANGELOG.md` for `0.1.0-alpha.1`.
- Update `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md`.
- Update `docs/RELEASE_CHECKLIST.md`, `docs/PACKAGING.md`, release blocker docs, and maintainer handoff docs.
- Run restore, build, test, scan, doctor, release verification, public gates, audit, blocker checks, pack, and local tool install validation.
- Create local annotated tag `v0.1.0-alpha.1` only after the final reviewed commit and passing gates.
- Do not push the tag.
- Do not publish to NuGet without a safe explicit maintainer publish step.

## Affected Files
- `CHANGELOG.md`
- `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/PACKAGING.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/PUBLIC_RELEASE_GATES.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/i18n Impact
Release notes and README package guidance may change. No runtime SEO or i18n behavior changes.

## Audit/Security Impact
Ensures package metadata, source hygiene, and public release gates are validated before a public tag or NuGet publish.

## Acceptance Criteria
- `CHANGELOG.md` has a dated `0.1.0-alpha.1` section.
- Local package validation succeeds with `dotnet pack`.
- Temporary local tool install works and `ackit --help` plus `ackit scan --json` run successfully.
- `scripts/verify-release.ps1` passes.
- Public release gates pass after the final local tag, or remaining blockers are external-only and documented.
- Local tag `v0.1.0-alpha.1` exists only if it points to the reviewed final commit.

## Tests
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release`
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
- `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1`
- `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`
- `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`

## Risks
- Creating a tag before the final commit would make the tag point at the wrong commit.
- NuGet publish requires maintainer approval and a secret API key; this task must not expose or commit secrets.

## Rollback
- Delete an incorrect local tag only if explicitly approved: `git tag -d v0.1.0-alpha.1`.
- Revert documentation changes through git if release notes need correction.

## Completion Notes
Completed during final public release preparation.

- `CHANGELOG.md` now has a dated `0.1.0-alpha.1` section for 2026-06-04.
- `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md`, `docs/RELEASE_CHECKLIST.md`, `docs/PACKAGING.md`, `docs/RELEASE_BLOCKERS.md`, `docs/PUBLIC_RELEASE_GATES.md`, and `docs/MAINTAINER_RELEASE_HANDOFF.md` were refreshed for final URL metadata and release tag readiness.
- `dotnet restore AgentContextKit.sln` passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 59/59 tests.
- `dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build` passed.
- Temporary `dotnet tool install --tool-path` passed.
- Installed `ackit --help` and `ackit scan --json` passed.
- Local tag creation is intentionally deferred until after the final reviewed commit so `v0.1.0-alpha.1` points at the correct commit.
