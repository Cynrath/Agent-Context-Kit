# TASK-0020: v0.3 HTML Report Generation

## Purpose
Add offline static HTML report generation for scan results.

## Scope
- Add `ackit report`.
- Generate a single self-contained HTML report from local scan results.
- Use a safe default output path under `.ackit/reports/`.
- Skip existing report files by default.
- Reject unsafe output paths outside the repository.
- Add `.ackit/reports/` to `.gitignore`.
- Add HTML report docs and focused tests.
- Update CLI, JSON, roadmap, changelog, project map, handoff, and next steps docs.

## Out of scope
- Web server.
- JavaScript-heavy dashboard.
- External CSS, images, fonts, CDNs, or telemetry.
- Automatic browser opening.
- Automatic overwrite or cleanup.
- NuGet publish.
- GitHub push.
- Release tag creation.
- Replacing TODO package URLs.

## Affected files
- `.gitignore`
- `src/AgentContextKit.Core/Abstractions.cs`
- `src/AgentContextKit.Core/Generation.cs`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `README.md`
- `docs/HTML_REPORTS.md`
- `docs/CLI_REFERENCE.md`
- `docs/JSON_OUTPUT.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0020-v030-html-report-generation.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. Report labels support the existing English/Turkish language option at a basic level. Generated reports are local artifacts, not public SEO pages.

## Audit/security impact
Improves local reviewability. Report generation must HTML-encode repository content, stay offline, avoid external assets, skip existing files by default, and prevent path traversal outside the repository.

## Architecture impact
Adds a Core HTML report generator and CLI command wrapper. Scanner behavior remains unchanged.

## CLI impact
Adds `ackit report [--output <repo-relative.html>] [--lang en|tr] [--json]`.

## Testing impact
Add focused tests for report creation, no-overwrite behavior, unsafe output rejection, and CLI JSON output.

## OSS/release impact
Adds a local report artifact workflow. Public release blockers remain maintainer-only.

## Acceptance criteria
- `ackit report` is documented.
- `ackit report` creates `.ackit/reports/scan-report.html` by default.
- `ackit report --output <path>` writes a repo-relative HTML file.
- Existing report files are skipped by default.
- Unsafe output paths are rejected.
- HTML report content is encoded and self-contained.
- `.ackit/reports/` is ignored by git.
- `ackit report --json` returns generated file metadata and risk summary.
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
3. Add Core HTML report generator interface and implementation.
4. Add CLI `report` command and JSON output.
5. Add tests.
6. Add HTML report docs.
7. Update README, CLI reference, JSON docs, documentation index, roadmap, changelog, project map, handoff, and next steps.
8. Run build/test.
9. Run `ackit report --json` in a temporary repository or ignored output path.
10. Run `ackit scan --ci`.
11. Run `scripts/verify-release.ps1`.
12. Run `git diff --check`.
13. Run real-name grep.
14. Update completion notes.
15. Commit implementation.
16. Continue with the next v0.3 task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- report --json`
4. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
5. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
6. `git diff --check`
7. Real-name grep

## Risks
- Generated reports can be mistaken for source files if output is not ignored or reviewed.
- HTML encoding mistakes could expose malformed content.
- Default report path can be skipped if a previous local report exists.

## Rollback plan
Revert the TASK-0020 implementation commit. Do not run destructive git commands.

## Completion notes
Implemented `ackit report`, `HtmlReportGenerator`, and `docs/HTML_REPORTS.md`.

Verification completed:
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed with 42/42 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- report --output .ackit/reports/task-0020-validation.html --json` created an ignored local HTML report and returned riskSummary `0`.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` exited `0` and reported no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed and the installed temporary tool showed `report` in help output.

Generated local reports remain under ignored `.ackit/reports/`.
Public release remains blocked by maintainer-only TODO URL selection, release tag creation, push, and NuGet publish approval.
