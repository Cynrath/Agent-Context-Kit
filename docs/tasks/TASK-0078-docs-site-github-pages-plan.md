# TASK-0078 Docs Site And GitHub Pages Plan

## Purpose
Decide whether AgentContextKit should add a hosted documentation site now and define a safe, low-maintenance GitHub Pages path without enabling it.

## Scope
- Inspect current repository docs, site tooling, workflows, and read-only GitHub Pages state.
- Compare repository Markdown, native GitHub Pages/Jekyll, and external static-site generator options.
- Record the recommended current decision, future activation triggers, information architecture, security constraints, and maintainer-only steps.
- Sync documentation index, repository settings, roadmap, queue, and Codex handoff files.

## Out Of Scope
- No GitHub Pages activation or repository settings change.
- No Pages deployment workflow.
- No Node, Python, Ruby, or static-site generator dependency.
- No custom domain, DNS, analytics, external scripts, or remote asset hosting.
- No push, tag, NuGet publish, GitHub Release edit, or Code Scanning upload.

## Affected Files
- `docs/DOCS_SITE_PLAN.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/GITHUB_SETTINGS_CHECKLIST.md`
- `docs/GITHUB_REPO_HYGIENE.md`
- `docs/ISSUE_BACKLOG.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None in this task. Future Pages enablement is a maintainer-only GitHub repository setting.

## Permission Impact
No permission change. A future Actions-based deployment would require separately reviewed Pages permissions.

## SEO/i18n Impact
The plan defines English-first navigation, preservation of Turkish README access, canonical repository links, and a future decision for localized site content. No hosted SEO surface is created now.

## Audit/Security Impact
The plan prohibits generated `.ackit` output, private paths, raw findings, secrets, analytics, unreviewed third-party scripts, and remote content uploads. Repository Markdown remains the source of truth.

## Acceptance Criteria
- Read-only Pages and Actions state is recorded without remote mutation.
- A decision document compares practical options and records the recommended current state.
- Future activation triggers, navigation, URL/base-path, accessibility, privacy, validation, and rollback requirements are documented.
- Remote-write steps are explicitly maintainer-only.
- No site tooling or deployment workflow is added.
- Documentation, hygiene, and release gates pass.

## Tests
Run docs/config searches, build/test/scan/doctor, hygiene scans, `git diff --check`, and release gates.

## Risks
- Enabling Pages before link/base-path validation can publish broken navigation.
- A generator can add dependency and maintenance overhead disproportionate to the current project size.
- Hosted docs can drift from repository Markdown or expose generated local artifacts.

## Rollback
Revert the TASK-0078 documentation commit. No remote Pages state or runtime behavior is changed.

## Completion Notes
- Task created after TASK-0077.
- Read-only inspection found no Pages workflow or site generator configuration; the Pages API returned `404`, so no configured Pages site was observed.
- Added a decision plan that keeps repository Markdown canonical, defers Pages, compares hosting options, and defines future activation triggers, information architecture, privacy, accessibility, base-path, permission, and rollback requirements.
- No Pages setting, workflow, custom domain, analytics, site generator, or dependency was added.
- Validation passed: restore, Release build with 0 warnings/errors, 127/127 tests, clean `scan --ci`, doctor PASS, JSON parse, SARIF parse, sample smoke, Pages tooling/workflow absence check, hygiene scans, diff check, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; the post-commit rerun passed with no blocking items.
