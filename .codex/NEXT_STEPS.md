# Next Steps

PROJECT-CONTROL-0001 is active: use `docs/NEXT_TASKS.md` and `docs/PROJECT_EXECUTION_QUEUE.md` as the central local task queue.

1. Current published release is `v0.2.0-alpha.1`.
2. Local `master` is aligned with `origin/master` at the start of PROJECT-CONTROL-0001.
3. Read-only GitHub CLI validation confirmed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs on `master` succeeded for commit `e0a0fa9`.
4. GitHub Release `v0.2.0-alpha.1` exists as a pre-release; its body has candidate-era wording, so `docs/RELEASE_BODY_V020_ALPHA1.md` is the maintainer-ready replacement draft.
5. Published NuGet `0.2.0-alpha.1` includes `ackit sarif`.
6. TASK-0066 through TASK-0087 are committed locally. TASK-0088 release-candidate evidence assets are implemented; finish full validation, commit, rerun the public gate, then continue with TASK-0089 config diagnostics CLI integration.
7. `docs/V100_GAP_ANALYSIS.md` is the source of truth for 1.0 readiness; historical v1.0 scripts validate asset presence, not GA completion.
8. Remote writes remain maintainer-only: push, tag, GitHub Release edits, label creation, repo settings, branch protection, Code Scanning upload, issue creation, and NuGet publish.
9. Generated `.ackit/`, SARIF, local reports, Web UI, packages, archives, `bin/`, `obj/`, `TestResults`, and coverage artifacts must not be committed.
10. TASK-0088 local evidence: fixture tests 2/2, final 2,000-file benchmark rerun 2.936 seconds, no vulnerable packages, and a Legacy warning for test dependency `xunit` `2.9.3`. Full validation passed with 164/164 tests.
11. `check-public-release-gates.ps1 -FailOnIssues` must be rerun after commit because it correctly fails on dirty working trees before commit.
12. Local branch is ahead of origin; do not push from the agent session. Report `local ahead, maintainer push required`.
