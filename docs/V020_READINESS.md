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
`v0.2.0-alpha.1` is published on GitHub and NuGet as a pre-release. This repository does not automate future pushes, tags, NuGet publishes, GitHub Releases, or SARIF uploads.

`0.2.0-alpha.1` published content includes `ackit sarif`, SARIF 2.1.0 output, scanner rule catalog hardening, configurable allowlists, additive JSON `ruleId`, expanded scanner patterns, sample gallery and demo scenario docs, Web UI preview docs, and visual asset guidance.

## Planned v0.2.0-alpha.2
The next package scope is frozen in `docs/V020_ALPHA2_SCOPE.md` as a compatibility-preserving hardening release. It includes culture-invariant scanner precision, expanded fixture coverage, and sanitized human/JSON suppression audit output while retaining CLI commands, JSON schema `2`, config schema `1`, exit codes, and SARIF visible-findings-only behavior.

Version metadata remains `0.2.0-alpha.1` until a separate release-preparation task runs local package smoke and updates source-package workflow pins.

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
