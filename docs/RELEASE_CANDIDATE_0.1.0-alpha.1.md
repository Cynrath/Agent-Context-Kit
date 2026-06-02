# Release Candidate Review: 0.1.0-alpha.1

## Status
Local release candidate review passed. Public release remains blocked until placeholder package URLs are replaced by maintainer-selected real public URLs.

## Scope
This review validates the local package and CLI without publishing, pushing, tagging, or creating remotes.

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
- `RepositoryUrl` is still a TODO placeholder.
- `PackageProjectUrl` is still a TODO placeholder.
- No public remote or release tag has been created from this session.

See [RELEASE_BLOCKERS.md](RELEASE_BLOCKERS.md) for the active blocker list and guard script.

## Manual Actions Before Public Release
- Choose the real public repository URL.
- Update `RepositoryUrl` and `PackageProjectUrl`.
- Review package README rendering.
- Review SECURITY and CONTRIBUTING docs.
- Run `ackit redact-check --profile public-release`.
- Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
- Push and tag only after explicit maintainer action outside this automated flow.
- Publish to NuGet only after explicit maintainer action.

## Verification Result
Passed during the TASK-0005 local validation run.

Command:
```powershell
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

Results:
- Restore: passed.
- Build: passed with 0 warnings and 0 errors.
- Tests: passed, 18/18.
- `ackit scan`: passed, no risk findings.
- `ackit doctor`: passed, all checks PASS.
- Release blocker review: reported placeholder URL blockers in non-failing mode.
- `dotnet pack`: passed.
- Temporary `dotnet tool install --tool-path`: passed.
- Installed `ackit --help`: passed.
- Installed `ackit scan --json`: passed.

Temporary package/tool artifacts were left under the user temp directory for inspection.
