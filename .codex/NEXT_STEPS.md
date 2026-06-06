# Next Steps

TASK-0062 v0.2.0-alpha scanner expansion, configurable allowlist, and rule catalog hardening is committed locally.

1. Keep package URLs at `https://github.com/Cynrath/agent-context-kit`.
2. Treat `v0.1.0-alpha.2` GitHub Release, NuGet publish, global tool install, `ackit --help`, Web UI smoke, and Codex for OSS submission as complete.
3. Read-only GitHub CLI validation on 2026-06-06 confirmed `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` succeeded for latest `master` after `docs: add sample gallery and demo scenarios`.
4. Published NuGet `0.1.0-alpha.2` does not include `ackit sarif`; current source includes it and the next alpha package should include it.
5. TASK-0062 adds central scanner rule catalog hardening, config-driven `safeDomains`, `ignoredPaths`, and `ignoredFindingIds`, expanded scanner patterns, and `docs/SCANNER_RULES.md`.
6. Generated `.ackit/`, SARIF, local reports, Web UI, package, archive, `bin/`, `obj/`, and `node_modules` artifacts must not be committed.
7. Security fixture docs must avoid exact sensitive token-like prefixes.
8. SARIF output must stay local by default, use repository-relative paths, omit raw scanner match values, and avoid absolute local path leakage.
9. Keep label creation, branch protection, repository settings, push, tag, GitHub Release, Code Scanning upload, and NuGet publish as maintainer-only actions.
10. TASK-0062 validation passed: restore, Release build, 83/83 tests, self-scan, doctor, JSON scan, SARIF generation/parse, sample smoke, installed `ackit` checks, hygiene scans, `git diff --check`, config/generated convention gate, v0.2 readiness gate, and v1.0 documentation release gate.
11. Post-commit public release gate passed with no blocking items; only the expected post-release `HEAD` warning and manual remote tag verification note remained.
12. Push is maintainer-only if the maintainer wants these changes on GitHub.
13. Do not create GitHub releases, publish NuGet packages, upload SARIF, redact, delete, force push, or create remotes from the agent session.
