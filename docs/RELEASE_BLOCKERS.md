# Release Blockers

This document lists known blockers that must be resolved before any public push, tag push, NuGet publish, or release announcement.

## Current Status
Public release is blocked.

The package URL blockers are resolved locally. The selected public repository URL is `https://github.com/Cynrath/agent-context-kit`, and the local `origin` is `https://github.com/Cynrath/agent-context-kit.git`.

Public release remains blocked until the final reviewed commit is tagged, the repository is pushed by the maintainer, and NuGet publish is explicitly approved.

## Blocking Items
- `v0.1.0-alpha.1` must point at the final reviewed commit before any public push.
- No public release tag has been pushed.
- No push approval has been given.
- No NuGet publish approval has been given.

## Resolved Items
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `Authors` is `Cynrath`.
- `Company` is `Cynrath`.
- `PackageId` is `AgentContextKit`.
- `ToolCommandName` is `ackit`.
- `PackageLicenseExpression` is `MIT`.

## Non-Blocking Local Validation
The following checks can pass while public release is still blocked:
- `dotnet restore`
- `dotnet build -c Release`
- `dotnet test -c Release`
- `ackit scan`
- `ackit doctor`
- `scripts/check-package-metadata.ps1 -FailOnIssues`
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

If the working tree is clean and `v0.1.0-alpha.1` points at `HEAD`, this gate should exit `0`.

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

With the final repository URLs in package metadata, this gate should exit `0`. See [NUGET_METADATA.md](NUGET_METADATA.md).

## Required Manual Resolution
Before public release:
1. Confirm the public repository exists at `https://github.com/Cynrath/agent-context-kit`.
2. Confirm `origin` remains `https://github.com/Cynrath/agent-context-kit.git`.
3. Run the package metadata script with `-FailOnIssues` and confirm it exits `0`.
4. Run the full release validation script.
5. Ensure local tag `v0.1.0-alpha.1` points at the reviewed commit.
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
