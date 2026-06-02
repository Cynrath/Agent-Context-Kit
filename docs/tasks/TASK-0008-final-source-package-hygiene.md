# TASK-0008: Final Source And Package Hygiene

## Purpose
Remove non-functional scaffolding leftovers and align source/documentation names before any public release action, without changing CLI behavior.

## Scope
- Remove the unused empty Core marker class file.
- Rename the generic test file to a descriptive name.
- Add a source hygiene document.
- Update current project map, documentation index, changelog, and handoff references.
- Run local validation after cleanup.

## Out of scope
- GitHub push.
- NuGet publish.
- Remote repository creation.
- Release tag creation.
- Replacing TODO package URLs.
- Broad source rewrites.
- CLI behavior changes.
- Automatic redaction.

## Affected files
- `src/AgentContextKit.Core/Class1.cs`
- `tests/AgentContextKit.Tests/UnitTest1.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/SOURCE_HYGIENE.md`
- `docs/PROJECT_MAP.md`
- `docs/DOCUMENTATION_INDEX.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0008-final-source-package-hygiene.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. New source hygiene doc is English to match release/development docs.

## Audit/security impact
Reduces release noise by removing unused scaffolding and stale generic file naming. No secret handling, authorization, or runtime security behavior changes.

## Architecture impact
No architecture change. CLI/Core/Tests boundaries remain unchanged.

## CLI impact
No command behavior change.

## Testing impact
Run build/test/scan and local release verification after file cleanup.

## OSS/release impact
Improves repository presentation and package confidence before public release.

## Acceptance criteria
- Empty `src/AgentContextKit.Core/Class1.cs` is removed.
- Tests are preserved under a descriptive test file name.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Current docs no longer point to removed source files.
- Work is committed.
- No push, publish, tag, remote creation, deletion outside this scoped cleanup, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Remove `src/AgentContextKit.Core/Class1.cs`.
4. Rename `tests/AgentContextKit.Tests/UnitTest1.cs` to `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`.
5. Add `docs/SOURCE_HYGIENE.md`.
6. Update current project map and documentation index.
7. Update changelog, handoff, and next steps.
8. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
9. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
10. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
11. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
12. Update completion notes.
13. Commit implementation.
14. Continue with the next task if more release gaps remain.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
4. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- Renaming the test file can leave stale references in generated docs if not updated.
- Removing the marker class is safe only because it has no references and the Core project has other source files.
- This does not resolve public release blockers for TODO package URLs.

## Rollback plan
Revert the TASK-0008 implementation commit. Do not run broad cleanup commands or destructive git operations.

## Completion notes
Completed.

- Removed unused empty `src/AgentContextKit.Core/Class1.cs`.
- Renamed `tests/AgentContextKit.Tests/UnitTest1.cs` to `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`.
- Added `docs/SOURCE_HYGIENE.md`.
- Updated current project map with hidden agent/config files and the renamed test file.
- Updated documentation index, README, README.tr, changelog, handoff, and next steps.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 18/18.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan` passed with no risk findings.
- `powershell -NoProfile -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.
- The release verification script reported known blockers in non-failing mode, then completed pack and temporary tool install validation.
- Temporary package/tool folders were left under the user temp directory for inspection.
- No push, publish, tag, remote creation, deletion outside the scoped cleanup, overwrite of unrelated files, or automatic redaction was performed.
