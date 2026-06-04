# Public Release Gates

This page documents the local public release gate orchestration command after the first GitHub push and tag push.

The gate script wraps existing checks:
- `scripts/check-package-metadata.ps1`
- `scripts/audit-public-release.ps1`
- `scripts/check-release-blockers.ps1`

## Current Post-Push State
- GitHub repository public: yes.
- `master` pushed: yes.
- `v0.1.0-alpha.1` tag pushed: yes.
- `master` and `v0.1.0-alpha.1` point to `aee808244bf33d00808e7e70db6235132c2d3829`.
- Package metadata URLs are final and should pass the metadata gate.
- NuGet publish is pending.
- GitHub Release page is pending.
- GitHub Actions latest `master` run is green for the pushed release commit.
- Repository description and topics are set.

Remote tag verification is a manual or git-remote check. The local gate scripts do not push, create releases, or publish packages.

## Report-Only Mode
Run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
```

Report-only mode keeps underlying checks non-failing and exits `0` when the scripts complete, while still printing notes.

## Failing Gate Mode
Run before GitHub Release page creation or NuGet publish:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

In post-push documentation sync state, this gate should pass when the working tree is clean, package metadata is final, no tracked artifacts are present, and the release tag exists locally.

## Maintainer-Only Actions
This script does not:
- Push commits.
- Create or push tags.
- Create GitHub Releases.
- Create remotes.
- Publish NuGet packages.
- Replace package metadata URLs.
- Redact or delete files.

Follow [MAINTAINER_RELEASE_HANDOFF.md](MAINTAINER_RELEASE_HANDOFF.md) for the remaining post-push release steps.
