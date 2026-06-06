# Next Steps

TASK-0060 GitHub Actions usage examples, SARIF availability wording, and CI docs polish is completed locally.

1. Keep package URLs at `https://github.com/Cynrath/agent-context-kit`.
2. Treat `v0.1.0-alpha.2` GitHub Release, NuGet publish, global tool install, `ackit --help`, Web UI smoke, and Codex for OSS submission as complete.
3. Read-only GitHub CLI validation on 2026-06-06 confirmed `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` succeeded for latest `master` after TASK-0059.
4. Published NuGet `0.1.0-alpha.2` does not include `ackit sarif`; current source includes it and the next alpha package should include it.
5. TASK-0060 adds GitHub Actions usage docs and documentation-only workflow examples under `docs/examples/`.
6. SARIF output must stay local by default, use repository-relative paths, omit raw scanner match values, and avoid absolute local path leakage.
7. `docs/examples/github-actions-sarif-upload.yml` is documentation-only; do not activate GitHub Code Scanning upload without maintainer approval.
8. Keep label creation, branch protection, repository settings, push, tag, GitHub Release, Code Scanning upload, and NuGet publish as maintainer-only actions.
9. Pre-commit validation passed: restore, Release build, 72/72 tests, repository `scan --ci`, `doctor`, `scan --json`, SARIF generation/JSON parse, installed `ackit` checks, SARIF local-path/token scan, hygiene scans, `git diff --check`, and v1.0 documentation/release gate.
10. Public release gate was run before commit and failed only because the working tree was dirty; the post-commit rerun passed with no blocking items.
11. Push is the next maintainer-only action if the maintainer wants the TASK-0060 commit on GitHub.
12. Do not create GitHub releases, publish NuGet packages, upload SARIF, redact, delete, force push, or create remotes from the agent session.
