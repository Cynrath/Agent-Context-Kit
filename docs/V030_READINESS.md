# v0.3 Readiness

AgentContextKit v0.3 local readiness is tracked separately from public release approval.

This page consolidates the local review after these completed work areas:
- CI mode with `ackit scan --ci`.
- Exit code standardization.
- Offline static HTML reports with `ackit report`.
- Example workflow documentation.
- Public release gate orchestration.

## Local Readiness Review
Run the v0.3 readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1
```

Run it as a failing gate for missing v0.3 assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1 -FailOnIssues
```

`-FailOnIssues` fails only for missing v0.3 readiness assets. It reports public-release blockers separately because those require maintainer-only decisions.

## Expected Public Blockers
Public release remains blocked until:
- `RepositoryUrl` is replaced with the real public repository URL.
- `PackageProjectUrl` is replaced with the real public project URL.
- A release tag is created after explicit maintainer approval.
- Push and NuGet publish are explicitly approved.

## Required Validation
Use these commands before treating v0.3 local readiness as reviewed:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- report --output .ackit/reports/v030-readiness.html --json
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

Use failing public gates only after maintainer-only public release blockers are resolved:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

## Next Roadmap Step
After v0.3 local readiness is consolidated, the next product line is v0.4:
- Local Web UI prototype.
- Scan result dashboard.
- Generated file preview.
- Risk finding browser.
- Task preview.
