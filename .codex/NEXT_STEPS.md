# Next Steps

PROJECT-CONTROL-0001 is active: use `docs/NEXT_TASKS.md` and `docs/PROJECT_EXECUTION_QUEUE.md` as the central local task queue.

1. Current published release is `v0.2.0-alpha.1`.
2. Local `master` is aligned with `origin/master` at the start of PROJECT-CONTROL-0001.
3. Read-only GitHub CLI validation confirmed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs on `master` succeeded for commit `e0a0fa9`.
4. GitHub Release `v0.2.0-alpha.1` exists as a pre-release; its body has candidate-era wording, so `docs/RELEASE_BODY_V020_ALPHA1.md` is the maintainer-ready replacement draft.
5. Published NuGet `0.2.0-alpha.1` includes `ackit sarif`.
6. TASK-0066 through TASK-0085 are completed locally; continue with TASK-0086 baseline-aware scan and CI policy after TASK-0085 validation and commit.
7. `docs/V100_GAP_ANALYSIS.md` is the source of truth for 1.0 readiness; historical v1.0 scripts validate asset presence, not GA completion.
7. Remote writes remain maintainer-only: push, tag, GitHub Release edits, label creation, repo settings, branch protection, Code Scanning upload, issue creation, and NuGet publish.
8. Generated `.ackit/`, SARIF, local reports, Web UI, packages, archives, `bin/`, `obj/`, `TestResults`, and coverage artifacts must not be committed.
9. PROJECT-CONTROL-0001 pre-commit validation passed: restore, build, 83/83 tests, scan, doctor, JSON scan, SARIF parse, sample smoke, hygiene scans, diff checks, config/v0.2/v1.0 gates, and `verify-release.ps1`.
10. `check-public-release-gates.ps1 -FailOnIssues` must be rerun after commit because it correctly fails on dirty working trees before commit.
11. If local branch remains equal to origin, do not suggest a push command. If ahead after commit, report `local ahead, maintainer push required`.
