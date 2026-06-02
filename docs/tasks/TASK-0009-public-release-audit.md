# TASK-0009: Public Release Audit

## Purpose
Add a final local-only public release audit that checks tracked-file hygiene, package metadata posture, and maintainer-only blockers without pushing, tagging, publishing, or choosing release URLs automatically.

## Scope
- Add a public release audit script.
- Add a public release audit report document.
- Update release/docs/handoff references.
- Run the audit in report-only and failing modes.
- Run standard build/test/scan/release verification after changes.

## Out of scope
- GitHub push.
- NuGet publish.
- Remote repository creation.
- Release tag creation.
- Replacing TODO package URLs.
- Choosing a public repository URL.
- Deleting temp folders.
- Broad source rewrites.
- Automatic redaction.

## Affected files
- `scripts/audit-public-release.ps1`
- `docs/PUBLIC_RELEASE_AUDIT.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/SOURCE_HYGIENE.md`
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0009-public-release-audit.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
README and README.tr gain a link to the audit document. Audit doc is English to match release docs.

## Audit/security impact
Improves public-release auditability by checking tracked forbidden files, placeholder package URLs, pseudonym package metadata, dirty working tree state, and release tag presence. Script must not mutate files, push, publish, tag, delete, redact, or create remotes.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run audit script in report-only and failing modes, then run build/test/scan/release verification.

## OSS/release impact
Gives maintainers a single local audit command before public release and records the current blocked state.

## Acceptance criteria
- `scripts/audit-public-release.ps1` exists and is local-only.
- `docs/PUBLIC_RELEASE_AUDIT.md` exists.
- Audit script reports known TODO package URL blockers.
- Audit script exits `0` by default while blockers remain.
- Audit script exits non-zero with `-FailOnIssues` while blockers remain.
- Audit script checks for tracked forbidden release artifacts.
- Audit script confirms package author metadata uses `Cynrath`.
- Build, tests, scan, and release verification pass.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add `scripts/audit-public-release.ps1`.
4. Add `docs/PUBLIC_RELEASE_AUDIT.md`.
5. Update documentation index, release checklist, release validation, release blockers, source hygiene, README, README.tr, changelog, handoff, and next steps.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`.
7. Run a wrapped `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues` and record the expected non-zero result.
8. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
9. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
10. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
11. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
12. Update completion notes.
13. Commit implementation.
14. Continue with the next task if more non-blocked work remains.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- Audit script can become stale if package metadata moves to another file.
- `-FailOnIssues` intentionally fails while maintainer-only blockers remain.
- Tracked-file artifact rules may need tuning for future supported fixture files.

## Rollback plan
Revert the TASK-0009 implementation commit. Do not delete temp folders or run destructive git commands.

## Completion notes
Completed.

- Added `scripts/audit-public-release.ps1`.
- Added `docs/PUBLIC_RELEASE_AUDIT.md`.
- Updated documentation index, release checklist, release validation, release blockers, source hygiene, project map, README, README.tr, changelog, handoff, and next steps.
- Verified report-only audit exits `0`.
- Verified `-FailOnIssues` exits `1` while placeholder URLs, missing release tag, and uncommitted changes remain.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 18/18.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan` passed with no risk findings.
- `powershell -NoProfile -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.
- The release verification script reported known blockers in non-failing mode, then completed pack and temporary tool install validation.
- Temporary package/tool folders were left under the user temp directory for inspection.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction was performed.
