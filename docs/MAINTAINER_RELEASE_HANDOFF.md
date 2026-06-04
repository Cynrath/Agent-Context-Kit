# Maintainer Release Handoff

This handoff lists the remaining maintainer-only actions for a public release.

Codex must not run these steps unless the maintainer explicitly asks for each public action. The current automated work stops before URL selection, tag creation, push, and NuGet publish.

## Current Blocked State
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- Recommended final public repository URL for review: `https://github.com/Cynrath/agent-context-kit`.
- Current local `origin` is `https://github.com/Cynrath/agent-context-kit.git`; decide intentionally whether to keep that casing/name or align the public URL before changing package metadata.
- `HEAD` has no release tag.
- No push or NuGet publish has been approved.

## Manual Step 1: Select Public URL
Choose the real public repository URL.

Example placeholder:

```powershell
$repoUrl = "https://github.com/Cynrath/agent-context-kit"
```

Do not use the placeholder value in a public package.

## Manual Step 2: Update Package Metadata
Update `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`:

```xml
<PackageProjectUrl>https://github.com/<owner>/agent-context-kit</PackageProjectUrl>
<RepositoryUrl>https://github.com/<owner>/agent-context-kit</RepositoryUrl>
```

Then run:

```powershell
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

The audit will still fail until a release tag exists.

## Manual Step 3: Commit Metadata Change
After review:

```powershell
git status --short --branch
git add src/AgentContextKit.Cli/AgentContextKit.Cli.csproj docs/RELEASE_BLOCKERS.md docs/PUBLIC_RELEASE_AUDIT.md
git commit -m "chore: set public release repository urls"
```

Adjust staged docs only if they were intentionally updated.

## Manual Step 4: Create Release Tag
Only after explicit approval:

```powershell
git tag -a v0.1.0-alpha.1 -m "AgentContextKit v0.1.0-alpha.1"
```

Then re-run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

## Manual Step 5: Push
Only after explicit approval:

```powershell
git push origin master
git push origin v0.1.0-alpha.1
```

If the default branch is not `master`, replace it intentionally.

## Manual Step 6: Publish NuGet Package
Use the verified package from the latest `scripts/verify-release.ps1` temp package directory, or pack again intentionally:

```powershell
$pkg = Join-Path $env:TEMP "ackit-final-nupkg"
New-Item -ItemType Directory -Force -Path $pkg | Out-Null
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
```

Publish only with an approved NuGet API key. Prefer a secure environment variable or a configured credential provider.

```powershell
dotnet nuget push (Join-Path $pkg "AgentContextKit.0.1.0-alpha.1.nupkg") --source https://api.nuget.org/v3/index.json --api-key $env:NUGET_API_KEY
```

Do not commit API keys or paste secrets into public logs.

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
