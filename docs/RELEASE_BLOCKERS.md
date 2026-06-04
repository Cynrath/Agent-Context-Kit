# Release Blockers

This document lists known blockers that must be resolved before any public push, tag, NuGet publish, or release announcement.

## Current Status
Public release is blocked.

The local v0.1.0-alpha.1 package can be built and installed from a temporary local feed, but the package metadata still contains placeholder public URLs.

## Blocking Items
- `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj` has a TODO `RepositoryUrl`.
- `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj` has a TODO `PackageProjectUrl`.
- No public repository URL has been intentionally selected by the maintainer in this session.
- Recommended final public repository URL for maintainer review: `https://github.com/Cynrath/agent-context-kit`.
- Current local `origin` is `https://github.com/Cynrath/Agent-Context-Kit.git`; the casing/name difference is a maintainer-only decision and must not be changed automatically.
- No public release tag has been created.
- No NuGet publish approval has been given.

## Non-Blocking Local Validation
The following checks can pass while public release is still blocked:
- `dotnet restore`
- `dotnet build -c Release`
- `dotnet test -c Release`
- `ackit scan`
- `ackit doctor`
- `scripts/check-package-metadata.ps1` in report-only mode
- `dotnet pack`
- temporary `dotnet tool install --tool-path`
- `scripts/verify-release.ps1`
- source archive hygiene checks for local ZIP/RAR sharing

These checks prove local package health. They do not approve publication.

## Guard Script
Run the blocker check in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1
```

Run it as a failing release gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
```

While TODO package URLs remain, `-FailOnBlockers` is expected to return a non-zero exit code.

## Public Release Audit
Run the broader public release audit:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1
```

Run it as a failing gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
```

See [PUBLIC_RELEASE_AUDIT.md](PUBLIC_RELEASE_AUDIT.md).

## Public Release Gate Orchestration
Run all public release gates in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
```

Run all public release gates as failing checks:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

See [PUBLIC_RELEASE_GATES.md](PUBLIC_RELEASE_GATES.md).

## Package Metadata Gate
Run the package metadata review:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
```

Run it as a failing gate before public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
```

While TODO package URLs remain, `-FailOnIssues` is expected to return a non-zero exit code. See [NUGET_METADATA.md](NUGET_METADATA.md).

## Required Manual Resolution
Before public release:
1. Select the real public repository URL.
2. Decide whether the final public URL should use `https://github.com/Cynrath/agent-context-kit` or the current origin casing/name.
3. Replace TODO `RepositoryUrl` and `PackageProjectUrl` with the selected URL.
4. Run the package metadata script with `-FailOnIssues` and confirm it exits `0`.
5. Run the full release validation script.
6. Run the audit script with `-FailOnIssues` and confirm it exits `0`.
7. Run the blocker script with `-FailOnBlockers` and confirm it exits `0`.
8. Run the public release gate orchestration script with `-FailOnIssues` and confirm it exits `0`.
9. Review package README rendering.
10. Review `SECURITY.md`, `CONTRIBUTING.md`, and release notes.
11. Push, tag, and publish only as an explicit maintainer action.

See [MAINTAINER_RELEASE_HANDOFF.md](MAINTAINER_RELEASE_HANDOFF.md) for the manual handoff sequence.

## References
- [Create a NuGet package using MSBuild](https://learn.microsoft.com/en-us/nuget/create-packages/creating-a-package-msbuild)
- [Package authoring best practices](https://learn.microsoft.com/nuget/create-packages/package-authoring-best-practices#package-metadata)
- [dotnet pack command](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-pack)
