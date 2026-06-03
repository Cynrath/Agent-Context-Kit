# TASK-0028: v0.4 Task Preview Refinement

## Purpose
Refine the local Web UI task preview so task files are easier to scan by task ID, title, status, size, and capped preview text.

## Scope
- Improve the `ackit webui` task preview section.
- Show latest task files with task ID, title, status, size, and path.
- Derive task status from completion notes where practical.
- Keep capped, encoded preview text.
- Keep output local, static, encoded, and non-overwriting.
- Add focused tests and update Web UI documentation.

## Out of scope
- New CLI command.
- Hosted/server Web UI.
- JavaScript interactivity.
- External CSS, fonts, images, CDNs, telemetry, or remote calls.
- Editing task files from the Web UI.
- Changing task generation templates.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Replacing TODO package URLs.

## Affected files
- `src/AgentContextKit.Core/Generation.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/WEB_UI_PROTOTYPE.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0028-v040-task-preview-refinement.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact because output is local-only. Task preview labels should support English and Turkish where practical.

## Audit/security impact
Improves local auditability of task status. Must keep task content HTML-encoded and must not upload, mutate, or publish data.

## Architecture impact
No new service boundary expected. Refine `WebUiGenerator` task rendering only.

## CLI impact
No command syntax change. Behavior remains:

```powershell
ackit webui [--output <repo-relative.html>] [--lang en|tr] [--json]
```

## Testing impact
Add focused tests for task ID, title, status, size, and encoded preview text.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL/tag/push/publish decisions.

## Acceptance criteria
- Task preview includes a table of latest task files.
- Task preview shows task ID, title, status, size, and path.
- Status is derived from completion notes when possible.
- Task preview text remains capped and encoded.
- Existing Web UI overwrite behavior is unchanged.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit webui --json` creates a local ignored validation file.
- Static Web UI checks confirm task preview labels.
- `ackit scan --ci` reports no risk findings for this repository.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Check current .NET guidance before coding.
4. Refine task preview rendering.
5. Add focused tests.
6. Update Web UI docs, roadmap, project map, changelog, handoff, context pack, and next steps.
7. Run build/test and local CLI validation.
8. Run `ackit scan --ci`.
9. Run `scripts/verify-release.ps1`.
10. Run `git diff --check`.
11. Run real-name grep.
12. Update completion notes.
13. Commit implementation.
14. Continue with the next roadmap task without asking for permission.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0028-validation.html --json`
4. `Select-String -Path .ackit/webui/task-0028-validation.html -Pattern "Task Preview|Task ID|Title|Task Status|Size"`
5. `Select-String -Path .ackit/webui/task-0028-validation.html -Pattern '<script|@import|href="http|src="http|url\(http'`
6. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- Status inference is intentionally simple and can be imperfect for older task files.
- Large task files still require scrolling because output remains static.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0028 implementation commit. Do not run destructive git commands.

## Completion notes
Not implemented yet.
