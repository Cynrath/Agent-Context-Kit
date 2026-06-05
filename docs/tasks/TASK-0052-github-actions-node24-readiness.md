# TASK-0052: GitHub Actions Node 24 Readiness

## Status
Completed.

## Purpose
Prepare GitHub Actions workflows for the Node 24 JavaScript action runtime and document non-blocking hosted runner image warnings.

## Scope
- Inspect `.github/workflows/ci.yml` and `.github/workflows/cross-platform-smoke.yml`.
- Review official action versions for Node 24 support.
- Upgrade official GitHub actions when a current stable major is available and compatible.
- Evaluate `FORCE_JAVASCRIPT_ACTIONS_TO_NODE24=true` and use it only if it is appropriate for local workflow hygiene.
- Keep `windows-latest` or switch to an explicit runner only if the target label is stable and clear.
- Document hosted CI manual validation expectations after a future push.

## Out Of Scope
- Running GitHub-hosted workflows from this agent session.
- Pushing workflow changes.
- Adding new third-party CI actions.

## Affected Files
- `.github/workflows/ci.yml`
- `.github/workflows/cross-platform-smoke.yml`
- `docs/RELEASE_VALIDATION.md`
- `docs/ROADMAP.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None. The project has no database or migrations.

## Admin Impact
None. There is no admin UI.

## Permission Impact
None. Workflow token permissions may be tightened to read-only if compatible with existing CI steps.

## SEO/i18n Impact
None.

## Audit/Security Impact
Reduces CI maintenance risk by moving official actions toward Node 24-compatible versions and documenting runner-image warning handling.

## Acceptance Criteria
- Workflow files use current official action majors where locally safe.
- Node 24 readiness is documented with official-source rationale.
- `windows-latest` redirect/image notices are treated as non-blocking unless an explicit stable runner label is chosen.
- Workflow syntax remains simple and readable.
- Hosted GitHub Actions validation is left as a manual post-push step.

## Tests
- Local YAML inspection.
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- Hosted workflow validation after a maintainer push.

## Risks
- New major action versions can require newer hosted/self-hosted runner versions.
- Hosted runner labels can change independently of this repository.

## Rollback
- Revert the workflow readiness commit.
- If hosted CI fails after a manual push, restore the previous action major versions and rerun CI.

## Completion Notes
- Reviewed official action/readiness sources for Node 24 migration and hosted runner labels.
- Updated `ci.yml` to `actions/checkout@v6`, `actions/setup-dotnet@v5`, read-only `contents: read`, and `windows-2025`.
- Updated `cross-platform-smoke.yml` to `actions/setup-dotnet@v5`, read-only `contents: read`, and `windows-2025`.
- Left `FORCE_JAVASCRIPT_ACTIONS_TO_NODE24=true` unset because the selected official action majors are Node 24-ready.
- Documented that hosted GitHub Actions validation is manual after a maintainer push.
