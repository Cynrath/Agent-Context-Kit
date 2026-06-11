# TASK-0068 CodeQL And Code Scanning Decision

## Purpose
Record the decision for CodeQL and SARIF upload / GitHub Code Scanning.

## Scope
- Add a Code Scanning decision document.
- Keep default state documentation-only.
- Document opt-in requirements and privacy constraints.

## Affected Files
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
Enabling Code Scanning requires maintainer action.

## Permission Impact
Active upload workflows require `security-events: write`; no permission is granted in this task.

## SEO/i18n Impact
None.

## Audit/Security Impact
SARIF remains local-only by default and omits raw secret matches.

## Acceptance Criteria
- Default decision is documentation-only.
- Opt-in workflow is deferred.
- Permission and privacy requirements are explicit.
- No SARIF upload or active workflow is created.

## Tests
Covered by docs and release gates in PROJECT-CONTROL-0001.

## Risks
Users may copy example workflows without reviewing permissions.

## Rollback
Revert the local docs commit.

## Completion Notes
- Code Scanning decision added locally.
- Code Scanning remains documentation-only by default; no SARIF upload workflow was activated.
