# TASK-0071 SARIF Code Scanning Opt-In Workflow Design

## Purpose
Design a minimal, privacy-first GitHub Code Scanning opt-in workflow without activating SARIF upload.

## Scope
- Document the recommended opt-in workflow shape.
- Refine the documentation-only SARIF upload example.
- Keep upload manual through `workflow_dispatch`.
- Keep `security-events: write` scoped to the upload job.
- Validate SARIF JSON before upload.

## Affected Files
- `docs/SARIF_UPLOAD_WORKFLOW_DESIGN.md`
- `docs/examples/github-actions-sarif-upload.yml`
- `docs/CODE_SCANNING_DECISION.md`
- `docs/GITHUB_ACTIONS_USAGE.md`
- `docs/SARIF_OUTPUT.md`
- `docs/SECURITY_MODEL.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
Activating the workflow and granting Code Scanning permissions remain maintainer-only GitHub actions.

## Permission Impact
The design requires job-level `contents: read` and `security-events: write`. No permission is granted by this task.

## SEO/i18n Impact
None.

## Audit/Security Impact
The workflow remains manual, validates SARIF before upload, uses repository-relative output, and does not upload local HTML/Web UI/context artifacts.

## Acceptance Criteria
- No active workflow is added under `.github/workflows/`.
- Example uses `workflow_dispatch` only.
- Upload permission is job-scoped.
- Published package version is explicit.
- SARIF is parsed before upload.
- Privacy and rollback guidance are documented.

## Tests
Run repository validation, hygiene checks, YAML text checks, and release gates.

## Risks
- A maintainer can copy the example without reviewing permissions or alert ownership.
- GitHub action majors can change and require later review.
- Code Scanning availability can depend on repository settings and GitHub plan behavior.

## Rollback
Revert the docs commit. No remote workflow or security setting is changed.

## Completion Notes
- Started from clean `master` aligned with `origin/master`.
- Added a manual published-package upload design with job-level `security-events: write`.
- Refined the documentation-only example to use `workflow_dispatch`, one upload job, SARIF existence/JSON validation, and no automatic push or pull-request trigger.
- No active workflow, SARIF upload, Code Scanning setting, or repository permission was changed.
- Pre-commit validation passed: workflow guardrail text checks, restore, Release build, 83/83 tests, `scan --ci`, `doctor`, `scan --json`, global `ackit version/help`, SARIF generation/parse, sample smoke, hygiene scans, `git diff --check`, config/generated gate, v0.2 readiness gate, v1.0 documentation release gate, and `verify-release.ps1`.
- `check-public-release-gates.ps1 -FailOnIssues` reported expected dirty-working-tree failures before commit and must be rerun after commit.
- Post-commit `check-public-release-gates.ps1 -FailOnIssues` passed. Expected warning remains that current `HEAD` is after `v0.2.0-alpha.1`, so remote tag verification is manual.
