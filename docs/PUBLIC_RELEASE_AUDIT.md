# Public Release Audit

This document records the local-only public release audit workflow after the first GitHub push and tag push.

## Current Status
GitHub source publication is complete for `0.1.0-alpha.1`.

- GitHub repository public: yes.
- `master` pushed: yes.
- `v0.1.0-alpha.1` tag pushed: yes.
- `master` and `v0.1.0-alpha.1` point to `aee808244bf33d00808e7e70db6235132c2d3829`.
- Package URL blockers are resolved.
- GitHub Release page is pending.
- NuGet publish is pending.

## Audit Command
Run report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1
```

Run as a failing gate before GitHub Release page creation or NuGet publish:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
```

For post-push documentation sync commits, `HEAD` may be newer than the release tag. The audit requires the release tag to exist locally and reports `HEAD` not being tagged as a warning, not a release issue.

## Gate Orchestration
Run package metadata, public release audit, and release blocker checks together:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
```

Use failing mode before GitHub Release page creation or NuGet publish:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

See [PUBLIC_RELEASE_GATES.md](PUBLIC_RELEASE_GATES.md).

## What The Audit Checks
- Tracked forbidden build/dependency/test artifact paths.
- Tracked package, temporary, dump, and backup file extensions.
- Tracked secret-like config files such as `.env` and `secrets.json`.
- Tracked environment-specific appsettings files.
- Dirty working tree state.
- Local release tag presence.
- Package `Authors` and `Company` metadata use `Cynrath`.
- `RepositoryUrl` and `PackageProjectUrl` are not placeholders.
- Package README and license metadata are present.

Remote tag push, GitHub Actions status, GitHub Release page status, repository topics, and NuGet package availability are external checks and must be verified through GitHub/NuGet or maintainer-controlled commands.

## Required Manual Follow-Up
Before NuGet publish and release announcement:
1. Confirm GitHub Actions latest `master` run is green.
2. Confirm repository description and topics.
3. Create the GitHub Release page for `v0.1.0-alpha.1`.
4. Run `scripts/audit-public-release.ps1 -FailOnIssues`.
5. Run `scripts/check-release-blockers.ps1 -FailOnBlockers`.
6. Run `scripts/check-public-release-gates.ps1 -FailOnIssues`.
7. Run `scripts/verify-release.ps1`.
8. Publish to NuGet only after explicit maintainer approval.

See [MAINTAINER_RELEASE_HANDOFF.md](MAINTAINER_RELEASE_HANDOFF.md) for copy-paste-ready maintainer-only commands.

## Safety
The audit script is read-only. It does not push, tag, publish, redact, delete files, create remotes, or modify package metadata.
