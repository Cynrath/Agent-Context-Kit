# TASK-0017: v0.2 Final Readiness Consolidation

## Purpose
Consolidate v0.2 readiness into a single local review workflow after stack detection, risk scanner precision, JSON schema, generated docs, samples, and NuGet metadata hardening work.

## Scope
- Add a v0.2 readiness document.
- Add a local-only v0.2 readiness check script.
- Keep public-release blockers unresolved.
- Update roadmap, release validation, documentation index, changelog, project map, handoff, and next steps.
- Run standard validation.

## Out of scope
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Replacing TODO package URLs.
- Changing package version.
- Runtime CLI behavior changes.

## Affected files
- `scripts/check-v020-readiness.ps1`
- `docs/V020_READINESS.md`
- `docs/ROADMAP.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0017-v020-final-readiness-consolidation.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. New readiness docs are English.

## Audit/security impact
Improves release auditability by providing a single local checklist for v0.2 assets and known public-release blockers. Script must not publish, push, tag, create remotes, redact, delete, or mutate repository files.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run the v0.2 readiness script, build, test, scan, metadata check, release blocker check, release verification, and real-name grep.

## OSS/release impact
Clarifies that v0.2 local readiness can be reviewed while public release remains blocked by maintainer-only URL, tag, push, and publish actions.

## Acceptance criteria
- `scripts/check-v020-readiness.ps1` exists and is local-only.
- `docs/V020_READINESS.md` exists.
- Readiness script verifies expected v0.2 task docs, sample repos, metadata script, and core release docs exist.
- Readiness script reports TODO package URLs and missing release tag as public blockers without mutating files.
- Readiness script exits `0` by default.
- Readiness script exits non-zero with `-FailOnIssues` only for missing v0.2 readiness assets.
- Docs link to v0.2 readiness review.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add v0.2 readiness script.
4. Add v0.2 readiness docs.
5. Update roadmap, release validation, documentation index, project map, changelog, handoff, and next steps.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1`.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1 -FailOnIssues`.
8. Run `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1`.
9. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
10. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
11. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
12. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
13. Run `git diff --check`.
14. Run real-name grep.
15. Update completion notes.
16. Commit implementation.
17. Continue with the next roadmap task.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1 -FailOnIssues`
3. `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1`
4. `dotnet build AgentContextKit.sln -c Release --no-restore`
5. `dotnet test AgentContextKit.sln -c Release --no-build`
6. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- Readiness script can become stale if v0.2 scope changes.
- Public-release blockers remain intentionally unresolved.
- This task validates local readiness, not NuGet.org rendering or remote release state.

## Rollback plan
Revert the TASK-0017 implementation commit. Do not run destructive git commands.

## Completion notes
Implemented `scripts/check-v020-readiness.ps1` and `docs/V020_READINESS.md`.

Verification completed:
- `scripts/check-v020-readiness.ps1` exited `0`, reported no v0.2 readiness asset issues, and reported expected public-release blockers.
- `scripts/check-v020-readiness.ps1 -FailOnIssues` exited `0` because all v0.2 readiness assets exist.
- `scripts/check-package-metadata.ps1` exited `0` in report-only mode and reported TODO package URL blockers.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed with 31/31 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan` reported no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.

Public release remains blocked by maintainer-only TODO URL selection, release tag creation, push, and NuGet publish approval.
