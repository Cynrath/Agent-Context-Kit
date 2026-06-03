# Public Release Audit

This document records the final local-only public release audit workflow.

## Current Status
Public release is blocked.

The codebase passes local build, test, scan, package, and temporary tool install validation, but maintainer-only public release blockers remain:
- `RepositoryUrl` is still a TODO placeholder.
- `PackageProjectUrl` is still a TODO placeholder.
- Current HEAD has no release tag.
- Push, tag, and NuGet publish have not been explicitly approved.

## Latest Local Result
TASK-0009 validation passed locally.

Results:
- Report-only audit exited `0`.
- Failing audit gate exited `1` as expected while blockers remain.
- Build passed with 0 warnings and 0 errors.
- Tests passed, 18/18.
- `ackit scan` reported no risk findings.
- `scripts/verify-release.ps1` passed.
- Release blocker review reported known blockers in non-failing mode.

## Audit Command
Run report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1
```

Run as a failing gate before public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
```

While maintainer-only blockers remain, `-FailOnIssues` is expected to return a non-zero exit code.

## Gate Orchestration
Run package metadata, public release audit, and release blocker checks together:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
```

Use failing mode only after maintainer-only blockers are resolved:

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
- Release tag presence at `HEAD`.
- Package `Authors` and `Company` metadata use `Cynrath`.
- `RepositoryUrl` and `PackageProjectUrl` are not placeholders.
- Package README and license metadata are present.

## Required Manual Resolution
Before public release:
1. Select the real public repository URL.
2. Replace TODO package URL metadata.
3. Commit the URL metadata change.
4. Create the release tag intentionally.
5. Run `scripts/audit-public-release.ps1 -FailOnIssues`.
6. Run `scripts/check-release-blockers.ps1 -FailOnBlockers`.
7. Run `scripts/verify-release.ps1`.
8. Push, tag, and publish only after explicit maintainer approval.

See [MAINTAINER_RELEASE_HANDOFF.md](MAINTAINER_RELEASE_HANDOFF.md) for copy-paste-ready maintainer-only commands.

## Safety
The audit script is read-only. It does not push, tag, publish, redact, delete files, create remotes, or modify package metadata.
