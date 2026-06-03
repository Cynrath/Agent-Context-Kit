# TASK-0019: v0.3 Exit Code Standardization

## Purpose
Standardize CLI exit code usage and documentation after adding CI mode.

## Scope
- Centralize CLI exit code constants.
- Preserve current command behavior.
- Add a dedicated exit code documentation page.
- Update CLI reference, JSON docs, documentation index, roadmap, changelog, project map, handoff, and next steps.
- Add focused tests for command error exit behavior.

## Out of scope
- Changing public release blockers.
- Adding new commands.
- HTML reports.
- NuGet publish.
- GitHub push.
- Release tag creation.
- Replacing TODO package URLs.

## Affected files
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/EXIT_CODES.md`
- `docs/CLI_REFERENCE.md`
- `docs/JSON_OUTPUT.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0019-v030-exit-code-standardization.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. New exit code docs are English.

## Audit/security impact
Improves automation clarity by making failure semantics explicit. Does not add remote calls or automatic redaction.

## Architecture impact
Small CLI-layer maintainability change. Core scanner behavior remains unchanged.

## CLI impact
No intended behavior change. Existing exit codes are documented and routed through named constants.

## Testing impact
Add focused tests for unknown command and invalid `task` invocation exit code behavior.

## OSS/release impact
Improves CI and public release validation documentation. Public release blockers remain maintainer-only.

## Acceptance criteria
- CLI exit code constants exist.
- Existing `scan`, `scan --ci`, `redact-check`, `doctor`, invalid command, and invalid task exit behavior is preserved.
- `docs/EXIT_CODES.md` exists.
- CLI and JSON docs link or summarize exit code behavior.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan --ci` reports no risk findings in this repository.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Centralize exit code constants in the CLI.
4. Add focused tests for invalid command/task exit behavior.
5. Add `docs/EXIT_CODES.md`.
6. Update CLI reference, JSON docs, documentation index, roadmap, changelog, project map, handoff, and next steps.
7. Run build/test.
8. Run `ackit scan --ci`.
9. Run `scripts/verify-release.ps1`.
10. Run `git diff --check`.
11. Run real-name grep.
12. Update completion notes.
13. Commit implementation.
14. Continue with the next v0.3 task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
4. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
5. `git diff --check`
6. Real-name grep

## Risks
- Standardization could accidentally alter exit behavior if constants are wired incorrectly.
- Docs can drift if future commands add new exit behavior without updating the matrix.

## Rollback plan
Revert the TASK-0019 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
