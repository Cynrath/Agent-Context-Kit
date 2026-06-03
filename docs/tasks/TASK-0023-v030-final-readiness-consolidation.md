# TASK-0023: v0.3 Final Readiness Consolidation

## Purpose
Consolidate v0.3 local readiness after CI mode, exit code standardization, HTML reports, example workflows, and public release hardening.

## Scope
- Add a v0.3 readiness script.
- Add a v0.3 readiness document.
- Update roadmap, documentation index, release validation, project map, changelog, handoff, and next steps.
- Keep public-release blockers unresolved.

## Out of scope
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Replacing TODO package URLs.
- Runtime CLI behavior changes.

## Affected files
- `scripts/check-v030-readiness.ps1`
- `docs/V030_READINESS.md`
- `docs/ROADMAP.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0023-v030-final-readiness-consolidation.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. New readiness docs are English.

## Audit/security impact
Improves release auditability by collecting v0.3 local readiness assets and public-release blockers in one local check. Script must not push, publish, tag, create remotes, redact, delete, or mutate files.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run v0.3 readiness script, public release gate script, build/test, scan, and release verification.

## OSS/release impact
Clarifies that v0.3 local readiness is separate from maintainer-only public release approval.

## Acceptance criteria
- `scripts/check-v030-readiness.ps1` exists.
- `docs/V030_READINESS.md` exists.
- Readiness script verifies expected v0.3 task docs, report docs, exit code docs, workflow docs, gate docs, and relevant scripts exist.
- Readiness script reports TODO package URLs and missing release tag as public blockers without mutating files.
- Readiness script exits `0` by default.
- Readiness script exits non-zero with `-FailOnIssues` only for missing v0.3 readiness assets.
- Docs link to v0.3 readiness review.
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
3. Add v0.3 readiness script.
4. Add v0.3 readiness docs.
5. Update roadmap, documentation index, release validation, project map, changelog, handoff, and next steps.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1`.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1 -FailOnIssues`.
8. Run `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1`.
9. Run build/test.
10. Run `ackit scan --ci`.
11. Run `scripts/verify-release.ps1`.
12. Run `git diff --check`.
13. Run real-name grep.
14. Update completion notes.
15. Commit implementation.
16. Continue with the next roadmap task.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1 -FailOnIssues`
3. `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1`
4. `dotnet build AgentContextKit.sln -c Release --no-restore`
5. `dotnet test AgentContextKit.sln -c Release --no-build`
6. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- Readiness script can become stale if v0.3 scope changes.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0023 implementation commit. Do not run destructive git commands.

## Completion notes
Implemented `scripts/check-v030-readiness.ps1` and `docs/V030_READINESS.md`.

Verification completed:
- `scripts/check-v030-readiness.ps1` exited `0`, reported no v0.3 readiness asset issues, and reported expected public-release blockers.
- `scripts/check-v030-readiness.ps1 -FailOnIssues` exited `0` because all v0.3 readiness assets exist.
- `scripts/check-public-release-gates.ps1` exited `0` in report-only mode and printed known public-release blockers.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed with 42/42 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` exited `0` and reported no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.

Public release remains blocked by maintainer-only TODO URL selection, release tag creation, push, and NuGet publish approval.
