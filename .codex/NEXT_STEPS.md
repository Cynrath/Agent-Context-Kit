# Next Steps

1. Resolve public-release blockers only after the maintainer selects the real public repository URL.
2. Replace `RepositoryUrl` and `PackageProjectUrl` with the maintainer-selected URL.
3. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
4. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
5. Create TASK-0008 for final source/package hygiene review before any public action.
6. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
