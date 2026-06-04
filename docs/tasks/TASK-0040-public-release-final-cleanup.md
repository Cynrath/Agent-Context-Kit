# TASK-0040: Public Release Final Cleanup

## Purpose
Prepare AgentContextKit for the first public GitHub release with local-only cleanup, source archive hygiene, self-scan accuracy audit, package metadata review, and blocker reporting.

## Scope
- Verify source archive hygiene for GitHub-ready source and local ZIP/RAR sharing.
- Update `winrar_exclude.txt` and document source archive rules.
- Clean empty local artifact directories when they are safe local artifacts.
- Audit self-scan stack detection for sample-driven false positives.
- Prevent `samples/`, docs, generated output, templates, and fixtures from producing main repository stack signals where practical.
- Keep sample files available for risk scanning.
- Add focused tests for main stack detection and sample exclusion behavior.
- Document public GitHub URL casing/name decision as maintainer-only.
- Re-run local release gates in report-only mode and document expected maintainer-only blockers.
- Update release, archive, OSS, architecture, roadmap, project map, changelog, context, next steps, handoff, and task notes.

## Out of scope
- GitHub push.
- Release tag creation.
- Remote creation, deletion, or URL changes.
- NuGet publish.
- Replacing TODO `RepositoryUrl` or `PackageProjectUrl`.
- Live LLM provider calls.
- Provider SDKs, HTTP clients, telemetry, upload, or API key handling.
- Automatic redaction.
- Treating restore-required `obj` cleanup behavior as a build bug.

## Affected files
- `docs/tasks/TASK-0040-public-release-final-cleanup.md`
- `src/AgentContextKit.Core/Scanning.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `.gitignore`
- `winrar_exclude.txt`
- `docs/SOURCE_ARCHIVE.md`
- `docs/ARCHITECTURE.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/OSS_READINESS.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB impact
None. No database, migrations, EF Core, or persisted application data are in scope.

## Admin impact
None. There is no admin UI or admin permission surface in this CLI project.

## Permission impact
No runtime permission model change. Release actions remain maintainer-only and local checks must not push, tag, publish, or mutate remotes.

## SEO/i18n impact
No public SEO surface. Documentation remains English. CLI localization behavior should not change.

## Audit/security impact
High release relevance. This task improves source archive hygiene, scanner accuracy, public metadata review, and blocker clarity while preserving offline/local-only behavior.

## Architecture impact
Small Core behavior change expected in `StackDetector`: main repository stack detection should ignore sample/generated/test-fixture style paths for stack signals while risk scanning still scans those files.

## CLI impact
No CLI syntax change expected. `ackit scan` output should become more accurate for the AgentContextKit repository by avoiding sample-derived main stack signals.

## Testing impact
Add focused xUnit coverage for sample path stack exclusion and .NET CLI/tool detection.

## OSS/release impact
Improves first public GitHub release readiness. Public release remains blocked until maintainer selects the final public repo URL, updates package URLs, creates the tag, pushes, and approves NuGet publishing.

## Acceptance criteria
- Task file exists before implementation.
- `.gitignore` is reviewed for `.git`, `.ackit`, `bin`, `obj`, test, coverage, package, archive, logs, and secret patterns.
- `winrar_exclude.txt` includes the approved source archive exclude patterns.
- `docs/SOURCE_ARCHIVE.md` documents GitHub source package vs local ZIP/RAR archive rules.
- `docs/SOURCE_ARCHIVE.md` includes a WinRAR exclude example.
- `.cursor/rules/project.mdc` is documented as an intentional AI instruction file.
- Empty local artifact directories are cleaned only when verified as safe local artifacts.
- `ackit scan` for this repository no longer reports sample-derived ASP.NET Core, Minimal API, TypeScript, or Tailwind signals as main repository stacks.
- Main repository scan reports `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions` where appropriate.
- Sample files remain eligible for risk scanning.
- Focused tests cover sample stack exclusion and .NET tool detection.
- Package metadata is reviewed: `Authors=Cynrath`, `Company=Cynrath` or blank, `PackageId=AgentContextKit`, `ToolCommandName=ackit`, `PackageLicenseExpression=MIT`.
- TODO `RepositoryUrl` and `PackageProjectUrl` remain documented as blockers until maintainer selects the final public URL.
- Current origin URL casing/name is documented as maintainer-only if it differs from the recommended final public URL.
- Real-name grep finds no matches for prohibited real-name terms.
- Required local validation commands are run and documented.
- Public release gate scripts in report-only mode complete and report expected TODO URL/tag/approval blockers.
- Build/test pass before implementation commit.
- No push, tag, remote change, NuGet publish, upload, provider call, API key handling, destructive git command, or unrelated file overwrite occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Re-read required project docs and stack detector/test code before implementation.
4. Update source archive docs and WinRAR exclude rules.
5. Inspect and safely clean empty local artifact directories when applicable.
6. Update stack detector to keep sample/generated/fixture paths out of main stack signals.
7. Add `.NET CLI / .NET Tool` detection.
8. Add focused tests.
9. Update architecture, project map, roadmap, OSS, release blocker, maintainer handoff, changelog, context, next steps, and session handoff docs.
10. Run required validation commands in order.
11. Update task completion notes.
12. Commit implementation.
13. Stop before maintainer-only push/tag/publish/remote actions.

## Test steps
1. `dotnet restore AgentContextKit.sln`
2. `dotnet build AgentContextKit.sln -c Release --no-restore`
3. `dotnet test AgentContextKit.sln -c Release --no-build`
4. `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
5. `dotnet run --project src/AgentContextKit.Cli -- scan`
6. `dotnet run --project src/AgentContextKit.Cli -- doctor`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1`
9. `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`
10. `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`
11. `git diff --check`
12. Real-name grep

## Risks
- Hiding sample stack signals from main scan can reduce visibility for sample projects until a separate sample stack report is implemented.
- Stack detection path filtering can be too broad if future real projects are intentionally placed under excluded path roots.
- Source archive docs can be mistaken for GitHub push requirements unless the distinction is explicit.
- Public release remains blocked by maintainer-only decisions.

## Rollback plan
Revert the TASK-0040 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
