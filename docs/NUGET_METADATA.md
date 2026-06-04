# NuGet Metadata

AgentContextKit package metadata is defined in `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`.

## Current Status
Local package metadata is ready for local pack validation:
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `Authors` is `Cynrath`.
- `Company` is `Cynrath`.

Public publish remains pending until:
- GitHub Actions latest `master` run is green.
- GitHub Release page for `v0.1.0-alpha.1` is prepared.
- NuGet publish approval has been given.

This review follows Microsoft Learn NuGet package authoring guidance for package ID, version, authors, description, project URL, README, repository metadata, tags, release notes, and license expression:
- https://learn.microsoft.com/nuget/create-packages/package-authoring-best-practices#package-metadata
- https://learn.microsoft.com/nuget/reference/msbuild-targets#pack-target

## Metadata Review
Run report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
```

Run as a failing gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
```

With the current package metadata, `-FailOnIssues` should return exit code `0`.

The script is local-only and read-only. It does not pack, push, publish, tag, redact, delete, create remotes, or change package metadata.

## Required Fields
- `PackAsTool`: `true`
- `ToolCommandName`: `ackit`
- `PackageId`: `AgentContextKit`
- `Version`: `0.1.0-alpha.1`
- `Authors`: `Cynrath`
- `Company`: `Cynrath`
- `PackageReadmeFile`: `README.md`
- `PackageLicenseExpression`: `MIT`
- `RepositoryType`: `git`
- `RepositoryUrl`: `https://github.com/Cynrath/agent-context-kit`
- `PackageProjectUrl`: `https://github.com/Cynrath/agent-context-kit`
- `PackageRequireLicenseAcceptance`: `false`
- `Description`: non-empty
- `PackageTags`: includes `ai`, `coding-agent`, `security`, `cli`, and `oss`
- `PackageReleaseNotes`: non-empty
- `README.md`: present and explicitly packed into the package root

## Publish Blockers
Before public publish:
1. Run `scripts/check-package-metadata.ps1 -FailOnIssues`.
2. Run `scripts/audit-public-release.ps1 -FailOnIssues`.
3. Run `scripts/check-release-blockers.ps1 -FailOnBlockers`.
4. Run `scripts/check-public-release-gates.ps1 -FailOnIssues`.
5. Run `scripts/verify-release.ps1`.
6. Confirm GitHub Actions latest `master` run is green.
7. Confirm GitHub Release page for `v0.1.0-alpha.1` exists or is ready.
8. Publish only after explicit maintainer approval.
