# Next Steps

1. Create TASK-0009 for final public-release audit without resolving maintainer-only blockers.
2. Keep `RepositoryUrl` and `PackageProjectUrl` as TODO until the maintainer selects the real public repository URL.
3. After the URL is selected, replace both package URL fields and run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
4. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` after blocker resolution.
5. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
