# Next Steps

1. Continue v0.4 product work with TASK-0025 for scan result dashboard refinement.
2. Keep public-release blockers unresolved until maintainer selects the real public repository URL.
3. Maintainer must select the real public repository URL before any public release.
4. Replace `RepositoryUrl` and `PackageProjectUrl` only after that URL is selected.
5. Create the release tag only after explicit maintainer approval.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1 -FailOnIssues` for local v0.2 readiness review.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues` after replacing TODO package URLs.
8. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`.
9. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
10. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
11. Follow `docs/MAINTAINER_RELEASE_HANDOFF.md` for push and NuGet publish.
12. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
