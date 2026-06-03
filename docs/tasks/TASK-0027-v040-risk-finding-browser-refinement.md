# TASK-0027: v0.4 Risk Finding Browser Refinement

## Purpose
Refine the local Web UI risk finding browser so findings are easier to review, prioritize, and act on during local audits.

## Scope
- Improve the `ackit webui` risk finding browser section.
- Sort findings by severity, category, and path for deterministic review.
- Add a local finding ID for each displayed item.
- Show severity, category, path, message, optional match, and recommended action.
- Keep the no-findings empty state clear.
- Keep output local, static, encoded, and non-overwriting.
- Add focused tests and update Web UI documentation.

## Out of scope
- JavaScript filters or dynamic browser-side search.
- New CLI command.
- Hosted/server Web UI.
- External CSS, fonts, images, CDNs, telemetry, or remote calls.
- Changing scanner rules or severity assignment.
- Automatic redaction or file mutation.
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
- `docs/tasks/TASK-0027-v040-risk-finding-browser-refinement.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact because output is local-only. Browser labels should support English and Turkish where practical.

## Audit/security impact
Improves local auditability of risk findings. Must keep finding data HTML-encoded and must not redact, mutate, upload, or publish data.

## Architecture impact
No new service boundary expected. Refine `WebUiGenerator` finding rendering only.

## CLI impact
No command syntax change. Behavior remains:

```powershell
ackit webui [--output <repo-relative.html>] [--lang en|tr] [--json]
```

## Testing impact
Add focused tests for finding ID, match display, deterministic severity order, and recommended actions.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL/tag/push/publish decisions.

## Acceptance criteria
- Risk finding browser includes a deterministic review queue.
- Findings are ordered by severity descending, then category, then path.
- Each visible finding has a stable local ID.
- Optional match text is shown when available and encoded.
- Recommended action is shown for each severity.
- No-findings output remains clear.
- Existing Web UI overwrite behavior is unchanged.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit webui --json` creates a local ignored validation file.
- Static Web UI checks confirm risk browser labels.
- `ackit scan --ci` reports no risk findings for this repository.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Check current .NET guidance before coding.
4. Refine risk finding browser rendering.
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
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0027-validation.html --json`
4. `Select-String -Path .ackit/webui/task-0027-validation.html -Pattern "Risk Finding Browser|Review Queue|Finding ID|Recommended Action|No risk findings"`
5. `Select-String -Path .ackit/webui/task-0027-validation.html -Pattern '<script|@import|href="http|src="http|url\(http'`
6. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- Recommended actions can be mistaken for automatic remediation unless docs stay clear.
- Large finding sets can still require scrolling because the output remains static.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0027 implementation commit. Do not run destructive git commands.

## Completion notes
Not implemented yet.
