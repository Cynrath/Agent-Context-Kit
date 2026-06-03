# TASK-0016: v0.2 NuGet Metadata Hardening Review

## Purpose
Add a dedicated NuGet package metadata review workflow so package readiness can be checked separately from broader release blockers.

## Scope
- Add a package metadata review script.
- Add a NuGet metadata documentation page.
- Keep TODO repository URLs as explicit blockers.
- Update packaging/release docs and handoff.
- Run metadata checks and standard validation.

## Out of scope
- NuGet publish.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- Replacing TODO package URLs.
- Adding package icon assets.
- Changing package version.

## Affected files
- `scripts/check-package-metadata.ps1`
- `docs/NUGET_METADATA.md`
- `docs/PACKAGING.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0016-v020-nuget-metadata-hardening.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. NuGet metadata docs are English.

## Audit/security impact
Improves release auditability. Script must not publish, push, tag, create remotes, redact, delete, or mutate package metadata.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run metadata script in report-only and failing modes, then run build/test/scan/release verification.

## OSS/release impact
Clarifies which package metadata is ready and which metadata remains blocked by maintainer-only URL selection.

## Acceptance criteria
- `scripts/check-package-metadata.ps1` exists and is local-only.
- `docs/NUGET_METADATA.md` exists.
- Metadata script validates required package fields.
- Metadata script reports TODO `RepositoryUrl` and `PackageProjectUrl`.
- Metadata script exits `0` by default while blockers remain.
- Metadata script exits non-zero with `-FailOnIssues` while TODO URLs remain.
- Docs link to metadata review.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add metadata check script.
4. Add NuGet metadata docs.
5. Update packaging, release checklist, documentation index, project map, changelog, handoff, and next steps.
6. Run `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1`.
7. Run wrapped `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues` and record expected failure.
8. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
9. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
10. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
11. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
12. Update completion notes.
13. Commit implementation.
14. Continue with the next v0.2 task.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- Script can become stale if package metadata moves to shared props.
- `-FailOnIssues` intentionally fails until maintainer-selected public URLs replace TODO values.
- Metadata review does not validate NuGet.org rendering after publish.

## Rollback plan
Revert the TASK-0016 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
