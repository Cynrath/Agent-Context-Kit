# v0.2 Readiness

AgentContextKit v0.2 local readiness is tracked separately from public release approval.

This page consolidates the local review after these completed work areas:
- Expanded stack detection.
- Risk scanner precision.
- Scanner rule catalog hardening and narrow config allowlists.
- JSON schema version 2 and summary output.
- Expanded generated agent/context docs.
- Safe sample repositories.
- NuGet package metadata hardening.

## Local Readiness Review
Run the v0.2 readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1
```

Run it as a failing gate for missing v0.2 assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1 -FailOnIssues
```

`-FailOnIssues` fails only for missing v0.2 readiness assets. It reports public-release blockers separately because those require maintainer-only decisions.

## Current Public Release State
`v0.1.0-alpha.2` is published on GitHub and NuGet. Current v0.2 readiness work is source-side hardening for future alpha releases and does not push, tag, publish NuGet packages, create GitHub Releases, or upload SARIF.

## Required Validation
Use these commands before treating v0.2 local readiness as reviewed:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

Use these before future public release announcements:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
```

## Next Roadmap Step
After v0.2 local readiness is consolidated, the next product line is v0.3:
- CI mode.
- Exit code standardization.
- HTML report generation.
- More tests and example workflows.
