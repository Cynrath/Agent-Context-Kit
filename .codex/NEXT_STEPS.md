# Next Steps

TASK-0059 scanner SARIF output, GitHub Code Scanning readiness, and CI integration examples is completed locally.

1. Keep package URLs at `https://github.com/Cynrath/agent-context-kit`.
2. Treat `v0.1.0-alpha.2` GitHub Release, NuGet publish, global tool install, `ackit --help`, Web UI smoke, and Codex for OSS submission as complete.
3. Read-only GitHub CLI validation on 2026-06-05 confirmed `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` succeeded for latest `master` after TASK-0058.
4. TASK-0059 adds `ackit sarif --output <repo-relative.sarif> [--json]` for privacy-first SARIF 2.1.0 scanner output.
5. SARIF output must stay local by default, use repository-relative paths, omit raw scanner match values, and avoid absolute local path leakage.
6. `docs/examples/github-actions-sarif-upload.yml` is documentation-only; do not activate GitHub Code Scanning upload without maintainer approval.
7. Keep label creation, branch protection, repository settings, push, tag, GitHub Release, Code Scanning upload, and NuGet publish as maintainer-only actions.
8. Pre-commit validation passed: restore, Release build, 72/72 tests, repository `scan --ci`, `doctor`, `scan --json`, SARIF generation/JSON parse, installed `ackit` checks, SARIF local-path/token scan, hygiene scans, `git diff --check`, and v1.0 documentation/release gate.
9. Public release gate was run before commit and failed only because the working tree was dirty; the post-commit rerun passed with no blocking items.
10. Push is the next maintainer-only action if the maintainer wants the TASK-0059 commit on GitHub.
11. Do not create GitHub releases, publish NuGet packages, upload SARIF, redact, delete, force push, or create remotes from the agent session.
