# TASK-0022: v0.3 Public Release Hardening

## Purpose
Add a single local public release gate workflow that orchestrates existing package metadata, audit, and blocker checks.

## Scope
- Add `scripts/check-public-release-gates.ps1`.
- Add public release gate documentation.
- Keep report-only mode non-failing while blockers remain visible.
- Add failing mode for final public release gates.
- Update release docs, documentation index, roadmap, changelog, project map, handoff, and next steps.

## Out of scope
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Replacing TODO package URLs.
- Changing package version.
- Mutating package metadata.

## Affected files
- `scripts/check-public-release-gates.ps1`
- `docs/PUBLIC_RELEASE_GATES.md`
- `docs/PUBLIC_RELEASE_AUDIT.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0022-v030-public-release-hardening.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. New public release gate docs are English.

## Audit/security impact
Improves release auditability by making public release gates repeatable from one local script. Script must not push, publish, tag, create remotes, redact, delete, or mutate files.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run public gate script in report-only mode and wrapped failing mode, then run standard validation.

## OSS/release impact
Clarifies final public release gates while keeping maintainer-only blockers unresolved.

## Acceptance criteria
- `scripts/check-public-release-gates.ps1` exists.
- `docs/PUBLIC_RELEASE_GATES.md` exists.
- Gate script runs package metadata, public audit, and release blocker checks.
- Gate script exits `0` in report-only mode while blockers remain.
- Gate script exits non-zero with `-FailOnIssues` while TODO URLs and missing tag remain.
- Docs link to the public release gate workflow.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan --ci` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add public release gate script.
4. Add public release gate docs.
5. Update release docs, documentation index, roadmap, changelog, project map, handoff, and next steps.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1`.
7. Run wrapped `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues` and record expected failure.
8. Run build/test.
9. Run `ackit scan --ci`.
10. Run `scripts/verify-release.ps1`.
11. Run `git diff --check`.
12. Run real-name grep.
13. Update completion notes.
14. Commit implementation.
15. Continue with the next v0.3 task.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
7. `git diff --check`
8. Real-name grep

## Risks
- Gate orchestration can become stale if underlying script names or flags change.
- Failing mode intentionally fails until maintainer-only public release blockers are resolved.

## Rollback plan
Revert the TASK-0022 implementation commit. Do not run destructive git commands.

## Completion notes
Implemented `scripts/check-public-release-gates.ps1` and `docs/PUBLIC_RELEASE_GATES.md`.

Verification completed:
- `scripts/check-public-release-gates.ps1` exited `0` in report-only mode and printed known public-release blockers.
- Wrapped `scripts/check-public-release-gates.ps1 -FailOnIssues` exited `1` as expected while TODO URLs, missing tag, and uncommitted changes remain.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed with 42/42 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` exited `0` and reported no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.

Public release remains blocked by maintainer-only TODO URL selection, release tag creation, push, and NuGet publish approval.
