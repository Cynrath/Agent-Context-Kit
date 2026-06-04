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
- Pushing the final public repository.
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
Improves first public GitHub release readiness. Public release remains blocked until the release tag points at the reviewed commit, the maintainer pushes, and NuGet publishing is approved.

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
- `RepositoryUrl` and `PackageProjectUrl` point at the selected public repository URL.
- Current origin URL is documented.
- Real-name grep finds no matches for prohibited real-name terms.
- Required local validation commands are run and documented.
- Public release gate scripts in report-only mode complete and report expected tag/approval blockers.
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
Completed.

- Added `docs/SOURCE_ARCHIVE.md` with GitHub source package vs local ZIP/RAR archive guidance.
- Updated `winrar_exclude.txt` with source archive exclude patterns and made it trackable.
- Reviewed `.gitignore`; build, test, coverage, logs, archives, local secrets, local `.ackit` artifacts, package files, and binlogs are covered.
- Removed the empty local `.ackit` artifact directory after verifying it was inside the repository and empty.
- Kept non-empty `bin/` and `obj/` directories because they contain current restore/build artifacts; deleting `obj/` requires a later `dotnet restore` before `--no-restore` build.
- Updated `StackDetector` to ignore `samples/`, docs, generated output, templates, and fixture-style paths for main repository stack signals.
- Preserved risk scanning for sample files.
- Added `.NET CLI / .NET Tool` detection from `PackAsTool` and `ToolCommandName`.
- Added focused tests for .NET tool detection, sample stack exclusion, and sample risk scanning.
- Updated architecture, project map, roadmap, release blocker, public gate, maintainer handoff, OSS, source hygiene, changelog, context, next steps, and session handoff docs.
- Package metadata reviewed: `Authors=Cynrath`, `Company=Cynrath`, `PackageId=AgentContextKit`, `ToolCommandName=ackit`, `PackageLicenseExpression=MIT`.
- Final public repo URL documented as `https://github.com/Cynrath/agent-context-kit`.
- Current `origin` documented as `https://github.com/Cynrath/agent-context-kit.git`.
- `dotnet restore AgentContextKit.sln`: passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore`: passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build`: passed, 59/59 tests.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`: passed with no risk findings.
- `dotnet run --project src/AgentContextKit.Cli -- scan`: passed; main stacks are `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.
- `dotnet run --project src/AgentContextKit.Cli -- doctor`: passed.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`: passed.
- `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1`: completed in report-only mode with expected missing tag and maintainer approval blockers.
- `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`: completed in report-only mode with expected blockers.
- `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`: completed in report-only mode with expected blockers.
- No push, tag, remote creation/change, NuGet publish, upload, provider call, API key handling, automatic redaction, or destructive git command was performed.
