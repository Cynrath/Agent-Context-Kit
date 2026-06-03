# Next Steps

1. Continue v0.2 product work with TASK-0014 for expanded generated docs.
2. Keep public-release blockers unresolved until maintainer selects the real public repository URL.
3. Maintainer must select the real public repository URL before any public release.
4. Replace `RepositoryUrl` and `PackageProjectUrl` only after that URL is selected.
5. Create the release tag only after explicit maintainer approval.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
8. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
9. Follow `docs/MAINTAINER_RELEASE_HANDOFF.md` for push and NuGet publish.
10. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
