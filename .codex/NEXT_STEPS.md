# Next Steps

TASK-0064 v0.2.0-alpha.1 release preparation is committed locally.

1. Keep package URLs at `https://github.com/Cynrath/agent-context-kit`.
2. Treat `v0.1.0-alpha.2` GitHub Release, NuGet publish, global tool install, `ackit --help`, Web UI smoke, and Codex for OSS submission as complete.
3. Read-only GitHub CLI validation for TASK-0063 confirmed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs on `master` succeeded before local edits.
4. Published NuGet `0.1.0-alpha.2` does not include `ackit sarif`; current source is being prepared as `0.2.0-alpha.1` and includes it.
5. TASK-0064 release decision: next package candidate is `0.2.0-alpha.1` because SARIF, scanner rule catalog, config allowlists, additive JSON `ruleId`, expanded scanner patterns, sample gallery, Web UI preview, and visual asset guidance exceed a patch-style alpha.
6. README published install commands stay pinned to `0.1.0-alpha.2` until `0.2.0-alpha.1` is published.
7. Generated `.ackit/`, SARIF, local reports, Web UI, screenshot captures from local paths, package, archive, `bin/`, `obj/`, and `node_modules` artifacts must not be committed.
8. SARIF output must stay local by default, use repository-relative paths, omit raw scanner match values, and avoid absolute local path leakage.
9. Keep label creation, branch protection, repository settings, push, tag, GitHub Release, Code Scanning upload, and NuGet publish as maintainer-only actions.
10. TASK-0064 validation passed: local package pack/install smoke for `0.2.0-alpha.1`, restore, Release build, 83/83 tests, self-scan, doctor, JSON scan, SARIF generation/parse, sample smoke, installed published `ackit` checks, hygiene scans, `git diff --check`, config/generated gate, v0.2 readiness gate, v1.0 documentation release gate, and `scripts/verify-release.ps1`.
11. Post-commit public release gate passed with no blocking items; only the expected post-release `HEAD` warning and manual remote tag verification note remained.
12. Push is maintainer-only after the local commit.
