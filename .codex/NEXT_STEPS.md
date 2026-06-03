# Next Steps

1. Continue v0.2 product work with TASK-0012 for better risk scanner precision.
2. Maintainer must select the real public repository URL before any public release.
3. Replace `RepositoryUrl` and `PackageProjectUrl` only after that URL is selected.
4. Create the release tag only after explicit maintainer approval.
5. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
8. Follow `docs/MAINTAINER_RELEASE_HANDOFF.md` for push and NuGet publish.
9. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
