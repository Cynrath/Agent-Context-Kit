# TASK-0029: v0.4 Final Readiness Consolidation

## Purpose
Consolidate v0.4 local readiness after the offline Web UI prototype and its dashboard, generated-file, risk-finding, and task-preview refinements.

## Scope
- Add a local-only v0.4 readiness script.
- Add v0.4 readiness documentation.
- Verify required v0.4 task files, Web UI docs, CLI/Web UI implementation files, and related validation assets.
- Report public-release blockers separately from v0.4 local readiness issues.
- Update release validation, roadmap, project map, changelog, context pack, next steps, and session handoff docs.

## Out of scope
- Hosted/server Web UI.
- JavaScript interactivity.
- External CSS, fonts, images, CDNs, telemetry, or remote calls.
- Optional LLM integration.
- Changing package URLs.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Automatic redaction or deletion.

## Affected files
- `scripts/check-v040-readiness.ps1`
- `docs/V040_READINESS.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0029-v040-final-readiness-consolidation.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact. Documentation and script output should stay clear and local-review oriented.

## Audit/security impact
Improves local release auditability for v0.4 assets. Must not push, publish, tag, upload, delete, or mutate repository content beyond documented local files.

## Architecture impact
No runtime architecture change expected. Adds a PowerShell readiness guard and documentation only.

## CLI impact
No CLI syntax change. The readiness script should validate existing CLI/Web UI assets.

## Testing impact
No xUnit test expected unless implementation touches product code. Script validation and existing build/test/release checks are required.

## OSS/release impact
Strengthens local readiness review. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `scripts/check-v040-readiness.ps1` exists and is local-only.
- `scripts/check-v040-readiness.ps1 -FailOnIssues` exits 0 when v0.4 assets are present.
- The script checks TASK-0024 through TASK-0029 and core Web UI docs/assets.
- Public-release blockers are reported separately from local v0.4 readiness issues.
- `docs/V040_READINESS.md` documents usage, failing gate behavior, expected blockers, validation commands, and next roadmap step.
- Release validation and documentation index mention v0.4 readiness.
- Roadmap marks v0.4 final readiness consolidated.
- Project map and changelog include the new script/doc/task.
- Context pack, next steps, and session handoff are updated.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit webui --json` creates a local ignored validation file.
- `ackit scan --ci` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- `git diff --check` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Read required project docs and current .NET/PowerShell guidance before implementation.
4. Add the v0.4 readiness script by following the existing v0.3 readiness pattern.
5. Add v0.4 readiness documentation.
6. Update release validation, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff.
7. Run the v0.4 readiness script in report-only and failing modes.
8. Run build/test and local CLI validation.
9. Run `ackit scan --ci`.
10. Run `scripts/verify-release.ps1`.
11. Run `git diff --check`.
12. Run real-name grep.
13. Update completion notes.
14. Commit implementation.
15. Continue with the next roadmap task without asking for permission.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0029-validation.html --json`
6. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- The readiness script can become stale if v0.4 assets move or are renamed.
- Public-release blockers remain intentionally unresolved.
- This does not replace manual maintainer review for release URLs, tag, push, or NuGet publish.

## Rollback plan
Revert the TASK-0029 implementation commit. Do not run destructive git commands.

## Completion notes
Completed in TASK-0029.

- Added `scripts/check-v040-readiness.ps1` with local-only v0.4 asset checks.
- Added `docs/V040_READINESS.md` with report-only and failing gate usage, expected public blockers, required validation, and v0.5 next-step context.
- Updated release validation, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff docs.
- Kept public-release blockers separate from local v0.4 readiness issues.
- Checked current .NET CLI guidance through Context7 `/dotnet/docs` and PowerShell script guidance through Microsoft Learn before implementation.

Verification:

- `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1` passed and reported no v0.4 asset issues.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues` exited 0 with public blockers reported separately.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 46/46 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0029-validation-final.html --json` created the ignored local Web UI with risk summary 0 and TASK-0029 shown as completed.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` passed with no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed. Public release blockers remain maintainer-only TODO package URLs and missing release tag.
- `git diff --check` passed.
- Real-name grep found no matches.
