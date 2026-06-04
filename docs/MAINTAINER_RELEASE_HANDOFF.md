# Maintainer Release Handoff

This handoff lists the remaining maintainer-only actions after the first public GitHub push and tag push.

Codex must not push commits, create GitHub releases, or publish NuGet packages unless the maintainer explicitly asks for each public action.

## Current Post-Push State
- GitHub repository public: yes, `https://github.com/Cynrath/agent-context-kit`.
- `master` pushed: yes.
- `v0.1.0-alpha.1` tag pushed: yes.
- `master` and `v0.1.0-alpha.1` point to `aee808244bf33d00808e7e70db6235132c2d3829`.
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageId` is `AgentContextKit`.
- `ToolCommandName` is `ackit`.
- `Authors` and `Company` are `Cynrath`.
- NuGet publish is pending.
- GitHub Release page is pending.
- Codex for OSS application pack is ready in `docs/CODEX_FOR_OSS_APPLICATION.md`.
- GitHub Actions latest `master` run is green for `aee808244bf33d00808e7e70db6235132c2d3829`.
- Repository description is set.
- Repository topics are set.

## GitHub Repository Metadata
Repository description:

```text
Offline-first CLI for generating safe AI coding agent context, task-first workflows, repo hygiene reports, and multi-agent instruction files.
```

GitHub topics:

```text
ai-tools, coding-agents, codex, developer-tools, dotnet, cli, repository-scanner, agents-md, open-source, security
```

## Manual Step 1: Recheck GitHub Actions
Latest `master` workflow run was verified as `success` for `aee808244bf33d00808e7e70db6235132c2d3829`. Recheck before NuGet publish:

```text
https://github.com/Cynrath/agent-context-kit/actions
```

If Actions are failing in a later run, fix CI before NuGet publish.

## Manual Step 2: Create GitHub Release
Create a release for tag `v0.1.0-alpha.1`:

```text
https://github.com/Cynrath/agent-context-kit/releases/new?tag=v0.1.0-alpha.1
```

Suggested release title:

```text
AgentContextKit v0.1.0-alpha.1
```

Suggested release summary:

```text
Initial alpha release of AgentContextKit, an offline-first .NET global tool for safe AI coding agent context, repository hygiene checks, task-first docs, and public release readiness.
```

## Manual Step 3: Publish NuGet Package
Run local validation first:

```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

Pack and publish only with an approved NuGet API key stored outside the repository:

```powershell
$pkg = Join-Path $env:TEMP "ackit-final-nupkg"
New-Item -ItemType Directory -Force -Path $pkg | Out-Null
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
dotnet nuget push (Join-Path $pkg "AgentContextKit.0.1.0-alpha.1.nupkg") --source https://api.nuget.org/v3/index.json --api-key $env:NUGET_API_KEY
```

Do not commit API keys or paste secrets into public logs.

## Manual Step 4: Verify NuGet Install
After NuGet indexing completes:

```powershell
dotnet tool install --global AgentContextKit --version 0.1.0-alpha.1
ackit --help
ackit scan --ci
```

Use a clean shell or temporary tool path if a previous global install exists.

## Manual Step 5: Submit Codex For OSS Form
Use `docs/CODEX_FOR_OSS_APPLICATION.md`.

Form-ready sections are included for:
- Why this repository is a good fit.
- How API credits would be used.
- Additional notes.

## Final Checklist
- GitHub Actions latest `master` run is green.
- Repository description is set.
- Repository topics are set.
- GitHub Release page exists for `v0.1.0-alpha.1`.
- NuGet package is published.
- NuGet global tool install is verified.
- Codex for OSS form is submitted or saved for submission.
