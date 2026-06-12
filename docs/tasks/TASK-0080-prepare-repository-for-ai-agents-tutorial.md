# TASK-0080 Prepare A Repository For AI Coding Agents Tutorial

## Purpose
Provide a practical, security-first workflow for adopting AgentContextKit in an existing repository before AI coding agents begin making changes.

## Scope
- Add a tutorial covering baseline inspection, local config, report-only risk review, agent instruction generation, task-first workflow, CI gating, and ongoing maintenance.
- Explain safe behavior for existing files, findings, allowlists, generated artifacts, and multiple agent targets.
- Define a repository-ready checklist and rollback path.
- Link the tutorial from README, first-five-minutes, documentation index, examples, project map, roadmap, queue, and Codex handoff docs.

## Out Of Scope
- No automatic redaction, deletion, upload, remote AI call, provider configuration, or API key handling.
- No CI workflow activation, Code Scanning upload, push, tag, NuGet publish, GitHub Release edit, or repository setting change.
- No runtime feature or scanner rule change.

## Affected Files
- `docs/PREPARE_REPOSITORY_FOR_AI_AGENTS.md`
- `README.md`
- `README.tr.md`
- `docs/FIRST_FIVE_MINUTES.md`
- `docs/EXAMPLES.md`
- `docs/AI_WORKFLOW.md`
- `docs/DOCUMENTATION_INDEX.md`
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
None. The tutorial recommends local review and maintainer-approved CI/repository changes only.

## SEO/i18n Impact
The English tutorial is linked from both README files and includes Turkish language-option guidance. No translated page is added in this task.

## Audit/Security Impact
The workflow keeps repository content local, treats High/Critical findings as review blockers, forbids broad Critical suppression, preserves existing files, and requires human review before generated context is committed or shared.

## Acceptance Criteria
- The tutorial covers preflight git/docs/tests/CI/security inspection before generation.
- `init`, `scan`, config review, `generate`, `task`, `doctor`, and `scan --ci` are sequenced with clear exit expectations.
- Existing-file, allowlist, artifact, remote-call, and secret-handling boundaries are explicit.
- A concise readiness checklist and rollback procedure are included.
- Documentation, build/test, hygiene, and release gates pass.

## Tests
Run documentation link/search checks, build/test/scan/doctor, JSON/SARIF/sample smoke, hygiene scans, `git diff --check`, and release gates.

## Risks
- Teams may treat generated instructions as authoritative without review.
- Broad ignore/allowlist settings can create false negatives.
- Running generation in a dirty worktree can make user changes harder to distinguish.

## Rollback
Revert the TASK-0080 documentation commit. In a target repository, remove only newly generated files after reviewing `git status`; never use destructive bulk cleanup.

## Completion Notes
- Task created after TASK-0079.
- Added `docs/PREPARE_REPOSITORY_FOR_AI_AGENTS.md` with baseline inspection, config, report-only risk review, agent instruction generation, task-first work, CI checks, local artifact boundaries, readiness criteria, maintenance, and rollback.
- Linked the adoption guide from both README files, first-five-minutes, examples, AI workflow, and documentation index.
- Static checks confirmed the guide contains no destructive git command or token-like example and all linked local docs exist.
- Validation passed: restore, Release build with 0 warnings/errors, 127/127 tests, clean `scan --ci`, doctor PASS, JSON parse, SARIF parse, sample smoke, hygiene scans, diff check, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; the post-commit rerun passed with no blocking items.
