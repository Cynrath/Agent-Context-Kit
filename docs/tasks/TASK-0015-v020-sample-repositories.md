# TASK-0015: v0.2 Sample Repositories

## Purpose
Add safe sample repositories that demonstrate AgentContextKit stack detection and generated-doc workflows without adding secrets, risky files, or public-release blockers.

## Scope
- Add a samples index.
- Add a safe .NET Minimal API sample.
- Add a safe Node/TypeScript/Tailwind tooling sample.
- Add sample usage documentation.
- Validate sample scans.
- Update documentation index, examples, roadmap, project map, changelog, and handoff.

## Out of scope
- Building sample projects in the main solution.
- Adding external package restore requirements for samples.
- Adding real secrets or risky sample env files.
- GitHub push.
- NuGet publish.
- Release tag creation.
- Replacing TODO package URLs.
- Remote repository creation.

## Affected files
- `samples/README.md`
- `samples/dotnet-minimal-api/README.md`
- `samples/dotnet-minimal-api/Sample.MinimalApi.csproj`
- `samples/dotnet-minimal-api/Program.cs`
- `samples/node-tooling/README.md`
- `samples/node-tooling/package.json`
- `samples/node-tooling/tsconfig.json`
- `samples/node-tooling/tailwind.config.js`
- `samples/node-tooling/src/index.ts`
- `docs/SAMPLES.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/EXAMPLES.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0015-v020-sample-repositories.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. Sample docs are English.

## Audit/security impact
Samples must not include `.env`, `.env.example`, private key files, tokens, passwords, dumps, backups, uploads, or generated package artifacts.

## Architecture impact
Samples are not added to `AgentContextKit.sln`. They are repository fixtures for scan/generate examples only.

## CLI impact
No command behavior change.

## Testing impact
Run root scan and sample directory scans. Run build/test/release verification after adding samples.

## OSS/release impact
Improves v0.2 onboarding and provides concrete scan examples.

## Acceptance criteria
- `samples/README.md` exists.
- .NET Minimal API sample scans as .NET/ASP.NET Core/Minimal API.
- Node tooling sample scans as Node/TypeScript/Tailwind.
- Samples contain no risk findings.
- Root repository scan contains no risk findings.
- Docs link to samples.
- `dotnet build` passes.
- `dotnet test` passes.
- `scripts/verify-release.ps1` passes.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add safe sample directories and files.
4. Add `docs/SAMPLES.md`.
5. Update docs, roadmap, project map, changelog, handoff, and next steps.
6. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
7. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
8. Run root `ackit scan`.
9. Run sample scans for both sample directories.
10. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
11. Update completion notes.
12. Commit implementation.
13. Continue with the next v0.2 task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
4. `Push-Location samples/dotnet-minimal-api; dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan; Pop-Location`
5. `Push-Location samples/node-tooling; dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan; Pop-Location`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- Sample files can change root stack detection output by design.
- Sample package manifests must avoid values that scanner treats as secret-like.
- Future sample additions can accidentally add risky files if not scanned.

## Rollback plan
Revert the TASK-0015 implementation commit. Do not run destructive git commands.

## Completion notes
Completed.

- Added `samples/README.md`.
- Added safe .NET Minimal API sample.
- Added safe Node/TypeScript/Tailwind tooling sample.
- Added `docs/SAMPLES.md`.
- Updated documentation index, examples, roadmap, project map, changelog, context pack, handoff, and next steps.
- Updated current generated stack sections after root scan started detecting sample stack signals.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 31/31.
- Root `ackit scan` passed with no risk findings.
- `samples/dotnet-minimal-api` scan detected `.NET`, `ASP.NET Core`, and `ASP.NET Core Minimal API` with no risk findings.
- `samples/node-tooling` scan detected `Node`, `TypeScript`, and `Tailwind CSS` with no risk findings.
- `powershell -NoProfile -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.
- Installed temporary `ackit scan --json` emitted schema version `2` with `riskSummary.total` equal to `0`.
- Release verification reported known public-release blockers in non-failing mode.
- Temporary package/tool folders were left under the user temp directory for inspection.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction was performed.
