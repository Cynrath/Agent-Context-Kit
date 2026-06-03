# TASK-0025: v0.4 Scan Result Dashboard Refinement

## Purpose
Refine the local Web UI scan result dashboard so a developer can quickly understand repository readiness, blocking risk level, severity distribution, and the next local verification commands.

## Scope
- Improve the `ackit webui` dashboard section.
- Add a local readiness score derived from existing scan health and risk data.
- Add a review status summary based on critical/high risk findings.
- Add risk severity breakdown cards or table.
- Add recommended local check commands based on detected stack signals.
- Keep output local, static, encoded, and non-overwriting.
- Add focused tests and update Web UI documentation.

## Out of scope
- New CLI command.
- Hosted/server Web UI.
- JavaScript interactivity.
- External CSS, fonts, images, CDNs, telemetry, or remote calls.
- Changing scanner rules or risk severities.
- Database, admin, auth, permissions, or SEO features.
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
- `docs/tasks/TASK-0025-v040-scan-result-dashboard-refinement.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact because output is local-only. Dashboard labels should support English and Turkish where practical.

## Audit/security impact
Improves local auditability by making blocking risk status and severity counts visible at the top of the Web UI. Must continue to HTML-encode repository-controlled text and avoid remote assets.

## Architecture impact
No new service boundary expected. Refine `WebUiGenerator` helpers while keeping CLI/Core separation.

## CLI impact
No command syntax change. Behavior remains:

```powershell
ackit webui [--output <repo-relative.html>] [--lang en|tr] [--json]
```

## Testing impact
Add focused tests for dashboard readiness/status text, severity breakdown, and recommended checks.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL/tag/push/publish decisions.

## Acceptance criteria
- `ackit webui` dashboard includes a readiness score.
- Dashboard includes a review status derived from findings.
- Dashboard shows critical, high, medium, low, and info counts.
- Dashboard shows recommended local verification commands.
- Dashboard remains self-contained and uses no remote assets.
- Repository-controlled text remains encoded.
- Existing Web UI overwrite behavior is unchanged.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit webui --json` creates a local ignored validation file.
- `ackit scan --ci` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Check current .NET guidance before coding.
4. Refine `WebUiGenerator` dashboard rendering.
5. Add recommended check rendering.
6. Add focused tests.
7. Update Web UI docs, roadmap, project map, changelog, handoff, context pack, and next steps.
8. Run build/test and local CLI validation.
9. Run `ackit scan --ci`.
10. Run `scripts/verify-release.ps1`.
11. Run `git diff --check`.
12. Run real-name grep.
13. Update completion notes.
14. Commit implementation.
15. Continue with the next roadmap task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0025-validation.html --json`
4. `Select-String -Path .ackit/webui/task-0025-validation.html -Pattern "Readiness Score|Review Status|Recommended Checks"`
5. `Select-String -Path .ackit/webui/task-0025-validation.html -Pattern '<script|@import|href="http|src="http|url\(http'`
6. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- A score can be misread as public-release approval unless docs are clear.
- The dashboard can become noisy if too many recommendations are shown.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0025 implementation commit. Do not run destructive git commands.

## Completion notes
Not implemented yet.
