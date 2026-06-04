# Maintainer Release Handoff

This handoff lists the remaining maintainer-only actions for a public release.

Codex must not push commits, push tags, create remotes, or publish NuGet packages unless the maintainer explicitly asks for each public action.

## Current Ready State
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `origin` is `https://github.com/Cynrath/agent-context-kit.git`.
- `PackageId` is `AgentContextKit`.
- `ToolCommandName` is `ackit`.
- `Authors` and `Company` are `Cynrath`.
- Local tag `v0.1.0-alpha.1` must point at the reviewed release commit before tag push.
- No push or NuGet publish has been approved.

## GitHub Repository Metadata
Suggested repository description:

```text
Offline-first CLI for generating safe AI coding agent context, task-first workflows, repo hygiene reports, and multi-agent instruction files.
```

Suggested GitHub topics:

```text
ai-tools, coding-agents, codex, developer-tools, dotnet, cli, repository-scanner, agents-md, open-source, security
```

Codex for OSS application material is prepared in `docs/CODEX_FOR_OSS_APPLICATION.md`.

## Manual Step 1: Final Local Validation
Run from the repository root:

```powershell
git status --short --branch
git remote -v
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- doctor
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

If the audit fails only because no release tag exists, create the local tag in Manual Step 2 and rerun the failing gates.

## Manual Step 2: Create Local Release Tag
Only after the final release commit is reviewed:

```powershell
git tag -a v0.1.0-alpha.1 -m "AgentContextKit v0.1.0-alpha.1"
```

Then re-run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

## Manual Step 3: Push Repository And Tag
Only after explicit maintainer approval:

```powershell
git push -u origin master
git push origin v0.1.0-alpha.1
```

If the default branch is not `master`, replace it intentionally.

## Manual Step 4: Publish NuGet Package
Pack from the reviewed commit:

```powershell
$pkg = Join-Path $env:TEMP "ackit-final-nupkg"
New-Item -ItemType Directory -Force -Path $pkg | Out-Null
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
```

Publish only with an approved NuGet API key stored outside the repository:

```powershell
dotnet nuget push (Join-Path $pkg "AgentContextKit.0.1.0-alpha.1.nupkg") --source https://api.nuget.org/v3/index.json --api-key $env:NUGET_API_KEY
```

Do not commit API keys or paste secrets into public logs.

## Post-Publish Install Check
```powershell
dotnet tool install --global AgentContextKit --version 0.1.0-alpha.1
ackit --help
ackit scan --ci
```

## Final Checklist
- Real package URLs are set.
- Source archive hygiene reviewed with `docs/SOURCE_ARCHIVE.md` if sharing a local ZIP/RAR.
- Package metadata gate exits `0`.
- Audit gate exits `0`.
- Blocker gate exits `0`.
- Public release gate orchestration exits `0`.
- Release verification passes.
- Package README renders correctly.
- `SECURITY.md`, `CONTRIBUTING.md`, and `CHANGELOG.md` are reviewed.
- Tag points to the reviewed commit.
- Push and publish are explicitly approved.
