# Next Steps

TASK-0063 README screenshots, Web UI preview assets, and visual documentation polish is committed locally.

1. Keep package URLs at `https://github.com/Cynrath/agent-context-kit`.
2. Treat `v0.1.0-alpha.2` GitHub Release, NuGet publish, global tool install, `ackit --help`, Web UI smoke, and Codex for OSS submission as complete.
3. Read-only GitHub CLI validation for TASK-0063 confirmed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs on `master` succeeded before local edits.
4. Published NuGet `0.1.0-alpha.2` does not include `ackit sarif`; current source includes it and the next alpha package should include it.
5. TASK-0063 adds public visual asset policy, Web UI preview guidance, README text preview, and a safe generic flow diagram.
6. Screenshots are intentionally not committed until sanitized assets are available.
7. Generated `.ackit/`, SARIF, local reports, Web UI, screenshot captures from local paths, package, archive, `bin/`, `obj/`, and `node_modules` artifacts must not be committed.
8. SARIF output must stay local by default, use repository-relative paths, omit raw scanner match values, and avoid absolute local path leakage.
9. Keep label creation, branch protection, repository settings, push, tag, GitHub Release, Code Scanning upload, and NuGet publish as maintainer-only actions.
10. TASK-0063 validation passed: restore, Release build, 83/83 tests, self-scan, doctor, JSON scan, SARIF generation/parse, sample smoke, installed `ackit` checks, hygiene scans, `git diff --check`, and v1.0 documentation release gate.
11. Post-commit public release gate passed with no blocking items; only the expected post-release `HEAD` warning and manual remote tag verification note remained.
12. Push is maintainer-only after the local commit.
