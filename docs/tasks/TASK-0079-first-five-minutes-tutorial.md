# TASK-0079 First Five Minutes With Ackit Tutorial

## Purpose
Give a new user one short, copy-paste-ready path from NuGet install to a scanned repository with generated agent context and a task file.

## Scope
- Add a first-five-minutes tutorial for the published `0.2.0-alpha.1` global tool.
- Use a disposable synthetic .NET console repository and explicit PowerShell commands.
- Explain expected generated files, local-only artifacts, `scan` versus `scan --ci`, and minimal-repository doctor behavior.
- Verify the documented flow with the installed global tool.
- Link the tutorial from README, examples, documentation index, project map, roadmap, queue, and Codex handoff docs.

## Out Of Scope
- No package install/publish mutation beyond using the already installed verified tool.
- No remote repository, push, tag, GitHub Release, issue, Pages, or Code Scanning action.
- No real project, secret, personal data, screenshot, generated HTML commit, or source-code feature change.

## Affected Files
- `docs/FIRST_FIVE_MINUTES.md`
- `README.md`
- `README.tr.md`
- `docs/EXAMPLES.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ISSUE_BACKLOG.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. The tutorial performs local filesystem and local git initialization only.

## SEO/i18n Impact
The English tutorial is linked from both README files and explains how to switch generated output to Turkish with `--lang tr`. No translated tutorial page is added in this task.

## Audit/Security Impact
The flow uses synthetic data, keeps generated reports under ignored `.ackit/` paths, performs no remote calls, and warns users to review generated files before committing.

## Acceptance Criteria
- The tutorial installs or verifies `AgentContextKit` `0.2.0-alpha.1` and creates a disposable demo repository.
- Commands cover `init`, report-only scan, `generate`, `task`, and final `scan --ci`.
- Expected files and exit behavior are explained without treating minimal-repository doctor gaps as a tool error.
- Local reports/Web UI are clearly optional and uncommitted.
- The exact tutorial flow passes with the installed global tool.
- Documentation, build/test, hygiene, and release gates pass.

## Tests
Run the tutorial in a timestamped temporary directory, then run repository build/test/scan/doctor, hygiene checks, `git diff --check`, and release gates.

## Risks
- A long tutorial can duplicate README and CLI reference content.
- Reusing a fixed demo path can make reruns fail or mix artifacts.
- Users may mistake expected doctor health gaps in a minimal repository for a CLI failure.

## Rollback
Revert the TASK-0079 documentation commit. Temporary tutorial smoke output remains outside the repository and is not committed.

## Completion Notes
- Task created after TASK-0078.
- Added `docs/FIRST_FIVE_MINUTES.md` and linked it from both README files, examples, and the documentation index.
- Published-tool smoke passed in a timestamped temporary repository: version/help, `init`, report-only scan, `generate --target all`, task creation, final `scan --ci`, report, Web UI, and SARIF all succeeded; expected files were present.
- Minimal-demo `ackit doctor` returned exit code `1` for documented repository-health gaps, confirming the tutorial warning is accurate rather than a tool failure.
- Validation passed: restore, Release build with 0 warnings/errors, 127/127 tests, clean repository `scan --ci`, doctor PASS, JSON parse, SARIF parse, sample smoke, tutorial link checks, hygiene scans, diff check, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; the post-commit rerun passed with no blocking items.
