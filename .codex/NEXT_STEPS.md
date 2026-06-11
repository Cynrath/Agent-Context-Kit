# Next Steps

TASK-0065 v0.2.0-alpha.1 publish verification and docs sync is in progress.

1. Keep package URLs at `https://github.com/Cynrath/agent-context-kit`.
2. Treat `v0.2.0-alpha.1` GitHub pre-release, NuGet publish, global tool install, `ackit --help`, and `ackit sarif` availability as complete.
3. Read-only GitHub CLI validation for TASK-0065 confirmed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs on `master` succeeded for commit `33649c3`.
4. Published NuGet `0.2.0-alpha.1` includes `ackit sarif`.
5. README published install commands are pinned to `0.2.0-alpha.1`.
6. Published-package smoke workflow installs `AgentContextKit` `0.2.0-alpha.1` and validates SARIF generation.
7. Generated `.ackit/`, SARIF, local reports, Web UI, screenshot captures from local paths, package, archive, `bin/`, `obj/`, and `node_modules` artifacts must not be committed.
8. SARIF output must stay local by default, use repository-relative paths, omit raw scanner match values, and avoid absolute local path leakage.
9. Keep label creation, branch protection, repository settings, push, tag, GitHub Release edits, Code Scanning upload, and NuGet publish as maintainer-only actions.
10. TASK-0065 pre-commit validation passed: global tool version/help/SARIF parse, restore, Release build, 83/83 tests, source scan/doctor/JSON/SARIF parse, sample smoke, hygiene scans, `git diff --check`, config/generated gate, v0.2 readiness gate, v1.0 documentation release gate, and `scripts/verify-release.ps1`.
11. Post-commit `scripts/check-public-release-gates.ps1 -FailOnIssues` passed. Expected warning remains: current `HEAD` is after `v0.2.0-alpha.1`, so remote tag verification is manual.
12. Push is maintainer-only after the local commit.
