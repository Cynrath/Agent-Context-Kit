# TASK-0026: v0.4 Generated File Preview Refinement

## Purpose
Refine the local Web UI generated file preview so expected agent/context files are easier to audit, and record the user-requested continuous progress rule as a project hard rule.

## Scope
- Add a continuous progress hard rule to project agent instructions.
- Improve the `ackit webui` generated file preview section.
- Show expected generated/context files with category, status, size, and preview text.
- Include missing expected files as explicit missing entries instead of only showing files that exist.
- Keep previews capped, encoded, local-only, and self-contained.
- Add focused tests and update Web UI documentation.

## Out of scope
- New CLI command.
- Hosted/server Web UI.
- JavaScript interactivity.
- External CSS, fonts, images, CDNs, telemetry, or remote calls.
- Generating missing files from the Web UI.
- Overwriting existing generated/context files.
- Changing scanner rules or generated markdown templates.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Replacing TODO package URLs.

## Affected files
- `AGENTS.md`
- `src/AgentContextKit.Core/Generation.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/WEB_UI_PROTOTYPE.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0026-v040-generated-file-preview-refinement.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact because output is local-only. Preview labels should support English and Turkish where practical.

## Audit/security impact
Improves local auditability of agent/context files. Must keep repository-controlled text HTML-encoded and must not expose content through remote services.

## Architecture impact
No new service boundary expected. Refine `WebUiGenerator` helpers while keeping IO and rendering in Core and CLI output separate.

## CLI impact
No command syntax change. Behavior remains:

```powershell
ackit webui [--output <repo-relative.html>] [--lang en|tr] [--json]
```

## Testing impact
Add focused tests for generated file preview status, categories, size metadata, missing file entries, and HTML encoding.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL/tag/push/publish decisions.

## Acceptance criteria
- `AGENTS.md` contains the continuous progress hard rule.
- Generated file preview lists expected agent/context files, including missing files.
- Existing expected files show category, present status, size, and encoded preview text.
- Missing expected files show missing status and no file read is required.
- Preview remains self-contained and uses no remote assets.
- Existing Web UI overwrite behavior is unchanged.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit webui --json` creates a local ignored validation file.
- Static Web UI checks confirm preview status/category/size labels.
- `ackit scan --ci` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Check current .NET guidance before coding.
4. Add the continuous progress hard rule to `AGENTS.md`.
5. Refine generated file preview rendering.
6. Add focused tests.
7. Update Web UI docs, roadmap, project map, changelog, handoff, context pack, and next steps.
8. Run build/test and local CLI validation.
9. Run `ackit scan --ci`.
10. Run `scripts/verify-release.ps1`.
11. Run `git diff --check`.
12. Run real-name grep.
13. Update completion notes.
14. Commit implementation.
15. Continue with the next roadmap task without asking for permission.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0026-validation.html --json`
4. `Select-String -Path .ackit/webui/task-0026-validation.html -Pattern "Generated File Preview|Category|Status|Size|Missing"`
5. `Select-String -Path .ackit/webui/task-0026-validation.html -Pattern '<script|@import|href="http|src="http|url\(http'`
6. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- Preview metadata can become noisy if too many expected files are listed.
- Missing entries can be misread as failures unless docs describe them as audit hints.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0026 implementation commit. Do not run destructive git commands.

## Completion notes
Implemented generated file preview refinement and recorded the continuous progress hard rule in `AGENTS.md`.

Generated file preview now includes:
- Expected agent/context files.
- Category values such as Codex, Claude, Cursor, Copilot, and Documentation.
- Present/missing status values.
- Size metadata for present files.
- Capped, HTML-encoded preview text.
- Missing-file hints without generating or overwriting files.

Verification completed:
- Context7 `.NET` docs were checked before coding for current file/encoding guidance.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed with 46/46 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0026-validation-final2.html --json` exited `0`, created the ignored local Web UI validation file, and reported zero risk findings.
- Static Web UI checks found `Generated File Preview`, `Category`, `Status`, `Size`, `Present`, `Missing`, `Codex`, and `Documentation`.
- Static Web UI checks found no remote asset/script/import references.
- `.ackit/webui/` is ignored by git.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` exited `0` and reported no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed; it reported known public-release blockers in report-only mode.
- `git diff --check` passed.
- Real-name grep found no matches.

Public release remains blocked by maintainer-only TODO URL selection, release tag creation, push, and NuGet publish approval.
