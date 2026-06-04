# Release Candidate Review: 0.1.0-alpha.1

## Status
Local release candidate preparation is complete for the reviewed working tree. Package URL blockers are resolved; public release remains blocked until the final reviewed commit is tagged and maintainer-controlled push/NuGet publish actions are approved.

## Scope
This review validates the local package and CLI without pushing, tag-pushing, publishing, or creating remotes.

## Required Local Gate
```powershell
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

The script runs:
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `ackit scan`
- `ackit doctor`
- `scripts/check-release-blockers.ps1` in report-only mode
- `dotnet pack`
- temporary `dotnet tool install --tool-path`
- installed `ackit --help`
- installed `ackit scan --json`

## Current Known Blockers
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- Local tag `v0.1.0-alpha.1` must point at the reviewed commit before public tag push.
- Public push and NuGet publish require explicit maintainer action.

See [RELEASE_BLOCKERS.md](RELEASE_BLOCKERS.md) for the active blocker list and guard script.

## Manual Actions Before Public Release
- Review package README rendering.
- Review SECURITY and CONTRIBUTING docs.
- Run `ackit redact-check --profile public-release`.
- Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
- Push only after explicit maintainer action outside this automated flow.
- Publish to NuGet only after explicit maintainer action.

## Verification Result
Current local release candidate validation:

- `dotnet restore AgentContextKit.sln`: passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore`: passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build`: passed, 59/59 tests.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`: passed with no risk findings.
- `dotnet run --project src/AgentContextKit.Cli -- doctor`: passed, all checks PASS.
- `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues`: passed.
- `dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build`: passed.
- Temporary `dotnet tool install --tool-path`: passed.
- Installed `ackit --help`: passed.
- Installed `ackit scan --json`: passed and reported `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.

Full post-commit release gates are rerun after the final release preparation commit so the local tag can point at the reviewed commit.
