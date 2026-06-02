# Next Steps

1. Maintainer must select the real public repository URL.
2. Replace `RepositoryUrl` and `PackageProjectUrl` only after that URL is selected.
3. Create the release tag only after explicit maintainer approval.
4. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`.
5. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
7. Follow `docs/MAINTAINER_RELEASE_HANDOFF.md` for push and NuGet publish.
8. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
