# Next Steps

TASK-0040 public release final cleanup is complete locally; remaining release actions are maintainer-only.

1. Decide the final public repository URL. Recommended review URL: `https://github.com/Cynrath/agent-context-kit`.
2. Decide whether current `origin` casing/name `https://github.com/Cynrath/agent-context-kit.git` should be kept or aligned.
3. Replace TODO `RepositoryUrl` and `PackageProjectUrl` only after that URL is selected.
4. Run `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues` after replacing TODO package URLs.
5. Create the release tag only after explicit maintainer approval.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`.
8. Run `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues`.
9. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
10. Follow `docs/MAINTAINER_RELEASE_HANDOFF.md` for push and NuGet publish.
11. Use `docs/SOURCE_ARCHIVE.md` and `winrar_exclude.txt` before sharing a local ZIP/RAR.
12. Do not push, publish, redact, tag, delete, or create remotes without explicit maintainer action.
