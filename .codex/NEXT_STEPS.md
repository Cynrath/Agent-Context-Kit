# Next Steps

TASK-0039 v1.0 final local readiness consolidation is complete locally; remaining release actions are maintainer-only.

1. Keep public-release blockers unresolved until maintainer selects the real public repository URL.
2. Maintainer must select the real public repository URL before any public release.
3. Replace `RepositoryUrl` and `PackageProjectUrl` only after that URL is selected.
4. Create the release tag only after explicit maintainer approval.
5. Run `powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1 -FailOnIssues` before any v1.0 local readiness handoff.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues`.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues`.
8. Run `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`.
9. Run `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues`.
10. Run `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues` after replacing TODO package URLs.
11. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`.
12. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
13. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
14. Follow `docs/MAINTAINER_RELEASE_HANDOFF.md` for push and NuGet publish.
15. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
