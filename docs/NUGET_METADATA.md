# NuGet Metadata

AgentContextKit package metadata is defined in `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`.

## Current Status
Local package metadata is mostly ready for local pack validation, but public publish remains blocked:
- `RepositoryUrl` is still a TODO placeholder.
- `PackageProjectUrl` is still a TODO placeholder.
- No public release tag exists.
- No NuGet publish approval has been given.

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

While TODO package URLs remain, `-FailOnIssues` is expected to return a non-zero exit code.

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
- `PackageRequireLicenseAcceptance`: `false`
- `Description`: non-empty
- `PackageTags`: includes `ai`, `coding-agent`, `security`, `cli`, and `oss`
- `PackageReleaseNotes`: non-empty
- `README.md`: present and explicitly packed into the package root

## Publish Blockers
Before public publish:
1. Select the real public repository URL.
2. Replace `RepositoryUrl` and `PackageProjectUrl`.
3. Run `scripts/check-package-metadata.ps1 -FailOnIssues`.
4. Run `scripts/audit-public-release.ps1 -FailOnIssues`.
5. Run `scripts/check-release-blockers.ps1 -FailOnBlockers`.
6. Run `scripts/verify-release.ps1`.
7. Publish only after explicit maintainer approval.
