# TASK-0067 GitHub Labels And Repo Settings Checklist

## Purpose
Prepare a single manual checklist for GitHub labels, milestones, settings, branch protection, issue templates, topics, and release hygiene.

## Scope
- Clarify recommended labels.
- Mark GitHub metadata writes as maintainer-only.
- Update issue triage and maintainer guidance.

## Affected Files
- `docs/GITHUB_LABELS.md`
- `docs/GITHUB_SETTINGS_CHECKLIST.md`
- `docs/ISSUE_TRIAGE.md`
- `docs/MAINTAINER_GUIDE.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
GitHub label/settings changes require maintainer action.

## Permission Impact
No permissions are changed locally.

## SEO/i18n Impact
None.

## Audit/Security Impact
Security-sensitive issues remain routed through `SECURITY.md`; public labels should not disclose vulnerabilities.

## Acceptance Criteria
- Label set matches the recommended public taxonomy.
- Repo settings checklist has branch protection, topics, issue templates, release settings, and security settings.
- No GitHub metadata write is performed.

## Tests
Covered by docs and release gates in PROJECT-CONTROL-0001.

## Risks
Manual GitHub settings can drift from docs.

## Rollback
Revert the local docs commit.

## Completion Notes
- Manual label/settings checklist updated locally.
- No GitHub labels, milestones, branch protection, or repository settings were changed.
