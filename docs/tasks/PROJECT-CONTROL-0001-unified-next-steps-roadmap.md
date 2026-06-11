# PROJECT-CONTROL-0001 Unified Next-Steps Roadmap

## Purpose
Create a central next-task roadmap and execute safe local-only post-release planning tasks without asking for per-task continuation.

## Scope
- Read current git, GitHub Release, and GitHub Actions status.
- Create `docs/NEXT_TASKS.md` and `docs/PROJECT_EXECUTION_QUEUE.md`.
- Complete TASK-0066 through TASK-0069 as local documentation tasks.
- Keep all remote-write work documented as maintainer-only.

## Affected Files
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `docs/tasks/PROJECT-CONTROL-0001-unified-next-steps-roadmap.md`
- `docs/tasks/TASK-0066-github-release-body-polish.md`
- `docs/tasks/TASK-0067-github-labels-and-repo-settings.md`
- `docs/tasks/TASK-0068-codeql-code-scanning-decision.md`
- `docs/tasks/TASK-0069-issue-tracker-bootstrap-plan.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None.

## Admin Impact
Maintainer-only GitHub admin work is documented but not performed.

## Permission Impact
No new repository permissions are granted. Code Scanning remains documentation-only.

## SEO/i18n Impact
No SEO impact. Public release language is normalized to published pre-release wording.

## Audit/Security Impact
Remote-write guardrails are centralized. Generated artifacts and local SARIF outputs remain ignored/local-only.

## Acceptance Criteria
- Central queue docs exist.
- TASK-0066 through TASK-0069 have task files and local docs.
- Remote-write work is marked maintainer-only.
- Build/test/scan/doctor/hygiene/release gates pass before commit.

## Tests
Run restore, build, tests, scan, doctor, JSON scan, SARIF parse, sample smoke, hygiene scans, diff checks, and release gates.

## Risks
- Queue can drift from actual repository state.
- Maintainer-only work can be mistaken for agent-executable work.
- GitHub Release body can remain stale until a maintainer edits it.

## Rollback
Revert the project-control commit. No remote state is changed.

## Completion Notes
- Started from `master` aligned with `origin/master`.
- GitHub Release `v0.2.0-alpha.1` exists as a published pre-release.
- Hosted `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` are green for commit `e0a0fa9`.
- Created central queue docs and completed TASK-0066 through TASK-0069 locally.
- Pre-commit validation passed: restore, Release build, 83/83 tests, `scan --ci`, `doctor`, `scan --json`, global `ackit version/help`, global SARIF generation/parse, sample smoke, hygiene scans, `git diff --check`, config/generated gate, v0.2 readiness gate, v1.0 documentation release gate, and `verify-release.ps1`.
- `check-public-release-gates.ps1 -FailOnIssues` reported expected dirty-working-tree failures before commit and must be rerun after commit.
