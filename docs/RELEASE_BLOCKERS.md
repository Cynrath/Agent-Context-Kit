# Release Blockers

This document lists known blockers that remain after the first public GitHub push and tag push.

## Current Status
GitHub publication is complete for the initial alpha source state.

- GitHub repository public: yes, `https://github.com/Cynrath/agent-context-kit`.
- `master` pushed: yes.
- `v0.1.0-alpha.1` tag pushed: yes.
- `master` and `v0.1.0-alpha.1` point to `aee808244bf33d00808e7e70db6235132c2d3829`.
- Package metadata final URL: yes.
- Codex for OSS application pack: ready, `docs/CODEX_FOR_OSS_APPLICATION.md`.
- GitHub Actions latest `master` run: success.
- Repository description: set.
- Repository topics: set.

## Blocking Items
- GitHub Release page for `v0.1.0-alpha.1` is pending.
- NuGet publish is pending.
- NuGet install verification is pending until publish completes.
- Codex for OSS form submission is pending.

## Resolved Items
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `Authors` is `Cynrath`.
- `Company` is `Cynrath`.
- `PackageId` is `AgentContextKit`.
- `ToolCommandName` is `ackit`.
- `PackageLicenseExpression` is `MIT`.
- Public GitHub repository exists.
- `master` is pushed.
- `v0.1.0-alpha.1` is pushed.
- GitHub Actions latest `master` run is green for `aee808244bf33d00808e7e70db6235132c2d3829`.
- Repository description matches the maintained release text.
- Repository topics include the maintained topic set.

## Local Validation
The following checks validate local source and package readiness. They do not create GitHub releases or publish NuGet packages:

```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

Remote tag verification can be checked with:

```powershell
git ls-remote origin "refs/heads/master" "refs/tags/v0.1.0-alpha.1" "refs/tags/v0.1.0-alpha.1^{}"
```

The local scripts do not create or mutate remote GitHub state.

## GitHub Release Page
Create a GitHub Release for tag `v0.1.0-alpha.1`.

Suggested title:

```text
AgentContextKit v0.1.0-alpha.1
```

Suggested summary:

```text
Initial alpha release of AgentContextKit, an offline-first .NET global tool for safe AI coding agent context, repository hygiene checks, task-first docs, and public release readiness.
```

## NuGet Publish
Publish only after GitHub Actions are green and the GitHub Release page is prepared:

```powershell
$pkg = Join-Path $env:TEMP "ackit-final-nupkg"
New-Item -ItemType Directory -Force -Path $pkg | Out-Null
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
dotnet nuget push (Join-Path $pkg "AgentContextKit.0.1.0-alpha.1.nupkg") --source https://api.nuget.org/v3/index.json --api-key $env:NUGET_API_KEY
```

Do not commit API keys or paste secrets into public logs.

## References
- [Create a NuGet package using MSBuild](https://learn.microsoft.com/en-us/nuget/create-packages/creating-a-package-msbuild)
- [Package authoring best practices](https://learn.microsoft.com/nuget/create-packages/package-authoring-best-practices#package-metadata)
- [dotnet pack command](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-pack)
