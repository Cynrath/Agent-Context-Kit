# TASK-0013: v0.2 JSON Schema Expansion

## Purpose
Stabilize and expand machine-readable JSON output for automation by adding common metadata and summary fields.

## Scope
- Bump JSON output schema version from `1` to `2`.
- Add common `generatedAtUtc` metadata to JSON commands.
- Add repository metadata to scan, doctor, and redact-check JSON.
- Add risk summaries to scan and redact-check JSON.
- Add check summaries to doctor JSON.
- Add generated file summaries to generate JSON.
- Update tests and JSON docs.
- Keep command exit-code behavior unchanged.

## Out of scope
- CLI command renames.
- Human output rewrites.
- New output formats.
- Remote upload.
- GitHub push.
- NuGet publish.
- Release tag creation.
- Replacing TODO package URLs.

## Affected files
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/JSON_OUTPUT.md`
- `docs/CLI_REFERENCE.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0013-v020-json-schema-expansion.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. JSON field names stay English and stable.

## Audit/security impact
No risk-scanning behavior changes. JSON summaries make automation easier without exposing additional secret values beyond existing finding fields.

## Architecture impact
JSON shaping remains in the CLI layer. Core models remain independent from console/JSON output.

## CLI impact
JSON output schema version changes to `2`. Human output and exit codes remain unchanged.

## Testing impact
Update existing JSON tests and add focused coverage for summary fields.

## OSS/release impact
Improves CI/script integration readiness for v0.2.

## Acceptance criteria
- JSON commands include `schemaVersion: 2`.
- JSON commands include `generatedAtUtc`.
- `scan --json` includes `repositoryName` and `riskSummary`.
- `redact-check --json` includes `repositoryPath`, `repositoryName`, and `riskSummary`.
- `doctor --json` includes `repositoryPath`, `repositoryName`, and `checkSummary`.
- `generate --json` includes `fileSummary`.
- Existing exit codes remain unchanged.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Update JSON schema version and common metadata.
4. Add repository, risk, check, and file summary DTO helpers.
5. Update JSON command outputs.
6. Update tests.
7. Update docs and handoff.
8. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
9. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
10. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
11. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
12. Update completion notes.
13. Commit implementation.
14. Continue with the next v0.2 task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
4. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- Consumers expecting schema version `1` must handle schema version `2`.
- Adding fields can expose more repository metadata, but only local path/name values already present in existing scan output.
- Timestamp values make JSON output non-deterministic unless consumers ignore them.

## Rollback plan
Revert the TASK-0013 implementation commit. Do not run destructive git commands.

## Completion notes
Completed.

- Bumped JSON output schema version from `1` to `2`.
- Added `generatedAtUtc` to JSON command outputs.
- Added `repositoryName` to scan output.
- Added repository metadata to doctor and redact-check JSON.
- Added `riskSummary` to scan and redact-check JSON.
- Added `checkSummary` to doctor JSON.
- Added `fileSummary` to generate JSON.
- Kept human output and exit-code behavior unchanged.
- Added CLI clock access for JSON metadata while keeping JSON shaping in the CLI layer.
- Updated JSON tests and added generate summary coverage.
- Updated JSON output docs, CLI reference, roadmap, project map, changelog, context pack, handoff, and next steps.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 30/30.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan` passed with no risk findings.
- `powershell -NoProfile -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.
- Installed temporary `ackit scan --json` emitted schema version `2`, `generatedAtUtc`, `repositoryName`, and `riskSummary`.
- Release verification reported known public-release blockers in non-failing mode.
- Temporary package/tool folders were left under the user temp directory for inspection.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction was performed.
