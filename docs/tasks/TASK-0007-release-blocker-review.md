# TASK-0007: Release Blocker Review And Guard Script

## Purpose
Make the remaining public-release blockers explicit, add a safe local guard script, and keep the v0.1.0-alpha.1 release path auditable without pushing, tagging, publishing, or choosing a public repository URL automatically.

## Scope
- Add a release blocker document.
- Add a PowerShell blocker check script.
- Wire the blocker check into local release verification in non-failing review mode.
- Update release, packaging, README, changelog, and handoff docs.
- Run focused validation after changes.

## Out of scope
- GitHub push.
- NuGet publish.
- Remote repository creation.
- Release tag creation.
- Selecting a real public repository URL.
- Deleting temporary verification folders.
- Automatic redaction.

## Affected files
- `docs/RELEASE_BLOCKERS.md`
- `scripts/check-release-blockers.ps1`
- `scripts/verify-release.ps1`
- `docs/RELEASE_VALIDATION.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md`
- `docs/PACKAGING.md`
- `docs/DOCUMENTATION_INDEX.md`
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0007-release-blocker-review.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
README and README.tr gain a release blocker link. The blocker document is English to match the current release docs; Turkish README links to it.

## Audit/security impact
Improves release auditability by making unresolved metadata blockers machine-checkable. The script must not mutate repository files, push, publish, tag, delete, redact, or create remotes.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run build/test/scan and both blocker-check modes. `-FailOnBlockers` is expected to return non-zero while placeholder URLs remain.

## OSS/release impact
Prevents accidental public release with placeholder package URLs and gives maintainers a clear manual gate before NuGet publication.

## Acceptance criteria
- `docs/RELEASE_BLOCKERS.md` exists and lists known blockers.
- `scripts/check-release-blockers.ps1` exists and is local-only.
- Blocker script reports TODO `RepositoryUrl` and `PackageProjectUrl`.
- Blocker script exits `0` by default when blockers exist.
- Blocker script exits non-zero with `-FailOnBlockers` while blockers exist.
- `scripts/verify-release.ps1` runs blocker review in non-failing mode.
- Release docs link to the blocker document and script.
- Build, tests, scan, and release verification still pass.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add `docs/RELEASE_BLOCKERS.md`.
4. Add `scripts/check-release-blockers.ps1`.
5. Update `scripts/verify-release.ps1` to run the blocker check without `-FailOnBlockers`.
6. Update release validation, checklist, release candidate, packaging, documentation index, README, README.tr, changelog, handoff, and next steps.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`.
8. Run a wrapped `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers` and record the expected non-zero result.
9. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
10. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
11. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
12. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
13. Update completion notes.
14. Commit implementation.
15. Continue with the next task if more release gaps remain.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- The blocker script could become stale if package metadata moves to a shared props file.
- `-FailOnBlockers` intentionally fails until maintainer-selected public URLs replace TODO values.
- Release verification remains local-only and does not prove NuGet.org rendering.

## Rollback plan
Revert the TASK-0007 implementation commit. Do not remove temp folders or rewrite history unless explicitly requested.

## Completion notes
Pending.
