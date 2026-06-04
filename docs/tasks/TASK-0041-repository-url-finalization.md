# TASK-0041: Repository URL Finalization

## Status
Completed.

## Purpose
Finalize the public repository URL and align release documentation with the selected public metadata before the first public GitHub release.

## Scope
- Confirm local `origin` with `git remote -v`.
- Keep or align `origin` to `https://github.com/Cynrath/agent-context-kit.git` if it differs only by casing/name.
- Confirm package metadata uses `https://github.com/Cynrath/agent-context-kit`.
- Remove stale TODO URL blocker language from release docs and Codex handoff docs.
- Keep push, remote creation, tag push, and NuGet publish outside this task.

## Affected Files
- `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/PUBLIC_RELEASE_GATES.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/OSS_READINESS.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/PACKAGING.md`
- `docs/DOCUMENTATION_INDEX.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None. This project has no database or migrations.

## Admin Impact
None. There is no admin UI.

## Permission Impact
None. No authentication or authorization behavior changes.

## SEO/i18n Impact
README and README.tr text may be updated. No runtime SEO surface exists.

## Audit/Security Impact
Reduces release risk by ensuring public package metadata points to the intended repository and by removing misleading blocker records.

## Acceptance Criteria
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `Authors` is `Cynrath`.
- `Company` is `Cynrath`.
- Package URL TODO blockers are removed from active release docs.
- Missing release tag, push approval, and NuGet publish approval remain documented as maintainer-controlled gates.

## Tests
- `git remote -v`
- `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues`
- `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`
- Run a targeted active-doc scan for stale package URL blocker wording.

## Risks
- Historical task files can still mention earlier TODO URL states; active release docs must represent the current state.
- Remote URL casing is a public repository decision; only the explicitly approved target URL may be used.

## Rollback
- Revert this task's documentation changes.
- If the git remote was aligned incorrectly, set it back with `git remote set-url origin <previous-url>`.

## Completion Notes
Completed during final public release preparation.

- `git remote -v` confirmed `origin` is `https://github.com/Cynrath/agent-context-kit.git`.
- Package metadata already used `https://github.com/Cynrath/agent-context-kit` for `RepositoryUrl` and `PackageProjectUrl`.
- Active release docs, package docs, README files, Codex handoff docs, and release gate docs were updated to remove stale URL blocker wording.
- `scripts/check-package-metadata.ps1 -FailOnIssues` passed.
- Release blockers now focus on release tag, push approval, and NuGet publish approval.
