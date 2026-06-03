# TASK-0018: v0.3 CI Mode

## Purpose
Add a minimal CI mode for repository scanning so automated checks can fail on high or critical risk findings while preserving existing human scan behavior.

## Scope
- Add `--ci` support to `ackit scan`.
- Preserve default `ackit scan` exit code behavior.
- Return non-zero from `ackit scan --ci` when high or critical findings exist.
- Include CI mode and exit code metadata in scan JSON output.
- Update CLI, JSON, release validation, CI workflow, roadmap, changelog, project map, handoff, and next steps docs.
- Add focused tests for CI mode behavior.

## Out of scope
- HTML reports.
- Full exit code standardization for every command.
- New dependencies.
- Remote services.
- NuGet publish.
- GitHub push.
- Release tag creation.
- Replacing TODO package URLs.

## Affected files
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `.github/workflows/ci.yml`
- `README.md`
- `docs/CLI_REFERENCE.md`
- `docs/JSON_OUTPUT.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0018-v030-ci-mode.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. CLI output remains English-first with existing Turkish command support unchanged.

## Audit/security impact
Improves automation safety by allowing scan findings to fail CI without adding remote calls or automatic redaction.

## Architecture impact
Small CLI-layer change. Core scanner behavior remains unchanged.

## CLI impact
`ackit scan --ci` becomes supported. `ackit scan` without `--ci` remains report-only and exits `0`.

## Testing impact
Add focused CLI tests for default scan exit behavior, high finding CI failure, critical finding CI failure, and JSON CI metadata.

## OSS/release impact
GitHub Actions can run `ackit scan --ci` after build/test. Public release blockers remain maintainer-only.

## Acceptance criteria
- `ackit scan --ci` is documented.
- `ackit scan --ci` exits `0` when no high/critical findings exist.
- `ackit scan --ci` exits `1` when high findings exist and no critical findings exist.
- `ackit scan --ci` exits `2` when critical findings exist.
- `ackit scan` without `--ci` preserves existing `0` exit behavior.
- `ackit scan --ci --json` includes `ciMode` and `exitCode`.
- Existing JSON output remains valid.
- GitHub Actions runs `ackit scan --ci`.
- `dotnet build` passes.
- `dotnet test` passes.
- Self-scan reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add CLI parsing for `--ci`.
4. Update `scan` exit code calculation.
5. Add scan JSON `ciMode` and `exitCode` fields.
6. Add focused tests.
7. Update docs and GitHub Actions workflow.
8. Run targeted CI mode commands.
9. Run build/test/scan/release verification.
10. Run `git diff --check`.
11. Run real-name grep.
12. Update completion notes.
13. Commit implementation.
14. Continue with the next v0.3 task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
4. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci --json`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
7. `git diff --check`
8. Real-name grep

## Risks
- CI mode can fail builds when repositories contain intentional high/critical findings.
- JSON schema additions require docs and tests to stay aligned.
- `scan --ci` should not be confused with public release approval.

## Rollback plan
Revert the TASK-0018 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
