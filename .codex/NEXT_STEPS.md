# Next Steps

1. Keep `RepositoryUrl` and `PackageProjectUrl` as TODO until the maintainer selects the real public repository URL.
2. After the URL is selected, replace both package URL fields.
3. Create the release tag only after explicit maintainer approval.
4. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`.
5. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
7. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
