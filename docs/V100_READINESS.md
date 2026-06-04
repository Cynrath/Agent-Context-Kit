# v1.0 Final Local Readiness

This page consolidates v1.0 local readiness. It confirms that the local docs, tasks, release gates, CLI contract, config conventions, and validation scripts are present and linked. It does not approve public release.

## Local Readiness Scope
The v1.0 local readiness review covers:
- TASK-0035 stabilization planning.
- TASK-0036 stable CLI contract review.
- TASK-0037 config and generated-file convention freeze.
- TASK-0038 documentation and release gate freeze.
- TASK-0039 final local readiness consolidation.
- Release validation documentation and the documentation index.
- Context pack, next steps, and session handoff continuity.

## Usage
Run the review in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1
```

Run it as a failing local gate for missing local readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1 -FailOnIssues
```

Public release blockers are reported separately. They do not cause `-FailOnIssues` to fail because they require maintainer-only decisions.

## Expected Public Release Blockers
The local v1.0 readiness gate is expected to report these blockers until a maintainer explicitly resolves them:
- `RepositoryUrl` is still a TODO placeholder.
- `PackageProjectUrl` is still a TODO placeholder.
- Current HEAD has no release tag.
- Push, tag, and NuGet publish have not been explicitly approved.

These blockers are tracked by `docs/RELEASE_BLOCKERS.md`, `docs/PUBLIC_RELEASE_AUDIT.md`, `docs/PUBLIC_RELEASE_GATES.md`, and `docs/MAINTAINER_RELEASE_HANDOFF.md`.

## Required Validation
Run these local checks before treating v1.0 local readiness as complete:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
git diff --check
rg -n "<maintainer-real-name-pattern>" .
```

Use the local maintainer real-name pattern without committing that pattern to documentation. The grep is expected to exit with no matches.

## Maintainer-Only Boundary
This readiness review does not:
- Replace package metadata URLs.
- Create or push release tags.
- Push commits.
- Create remotes.
- Publish NuGet packages.
- Upload repository content.
- Call LLM providers.
- Read, store, generate, or validate API keys.
- Redact, overwrite, or delete files.

Follow `docs/MAINTAINER_RELEASE_HANDOFF.md` only after explicit maintainer approval.
