# TASK-0024: v0.4 Local Web UI Prototype

## Purpose
Start v0.4 by adding a local-only Web UI prototype that makes scan results easier to review without starting a server or introducing frontend dependencies.

## Scope
- Add a CLI command for generating a local static Web UI prototype.
- Generate a self-contained HTML file from the current local scan result.
- Include a scan result dashboard, generated/context file preview, risk finding browser, and task preview.
- Keep output local, offline, repository-relative, and non-overwriting by default.
- Add focused tests and user documentation.
- Update config defaults so generated Web UI files are ignored by scans and git.

## Out of scope
- ASP.NET app hosting.
- Local server lifecycle management.
- Node, React, Vue, Tailwind, npm, or bundler setup.
- Remote calls, telemetry, analytics, CDNs, fonts, or external assets.
- Browser automation or live file editing from the generated UI.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Replacing TODO package URLs.

## Affected files
- `src/AgentContextKit.Core/Abstractions.cs`
- `src/AgentContextKit.Core/Generation.cs`
- `src/AgentContextKit.Core/Configuration.cs`
- `src/AgentContextKit.Core/Models.cs`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `.gitignore`
- `README.md`
- `README.tr.md`
- `docs/PRODUCT_SPEC.md`
- `docs/CLI_REFERENCE.md`
- `docs/CONFIGURATION.md`
- `docs/JSON_OUTPUT.md`
- `docs/ARCHITECTURE.md`
- `docs/WEB_UI_PROTOTYPE.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0024-v040-local-web-ui-prototype.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact because output is local-only. UI labels should support English and Turkish through the existing language option where practical.

## Audit/security impact
Improves local reviewability of risk findings. Generated HTML must encode repository-controlled text, must not use remote assets, must not execute repository content as script, must not overwrite existing files, and must reject paths outside the repository.

## Architecture impact
Add a Core generator behind a small interface and wire it through the CLI service record. Keep scanning, rendering, IO, and CLI output responsibilities separated.

## CLI impact
Add a local Web UI generation command. Proposed command:

```powershell
ackit webui [--output <repo-relative.html>] [--lang en|tr] [--json]
```

Default output path:

```text
.ackit/webui/index.html
```

## Testing impact
Add unit tests for HTML encoding, no-overwrite behavior, unsafe output path rejection, config defaults, and CLI JSON output.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL/tag/push/publish decisions.

## Acceptance criteria
- `ackit webui` is listed in help and CLI docs.
- `ackit webui` generates `.ackit/webui/index.html` by default.
- `ackit webui --output <path>` supports repository-relative `.html`/`.htm` output.
- Existing Web UI files are skipped, not overwritten.
- Unsafe output paths outside the repository are rejected.
- Generated UI is self-contained and uses no external assets.
- Repository-controlled text in generated HTML is encoded.
- Generated UI includes scan dashboard, health, stack signals, risk findings, generated/context file preview, and task preview sections.
- `.ackit/webui/` is ignored by git and default scan config.
- `ackit webui --json` emits schema version 2 metadata, repository metadata, risk summary, and generated file metadata.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan --ci` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add Web UI generator interface and implementation.
4. Wire `ackit webui` into CLI help, command routing, JSON output, and service construction.
5. Add `.ackit/webui/` ignore behavior.
6. Add focused tests.
7. Add Web UI prototype docs.
8. Update README, CLI, JSON, config, architecture, documentation index, roadmap, project map, changelog, handoff, and next steps.
9. Run build/test and local CLI validation.
10. Run `ackit scan --ci`.
11. Run `scripts/verify-release.ps1`.
12. Run `git diff --check`.
13. Run real-name grep.
14. Update completion notes.
15. Commit implementation.
16. Continue with the next roadmap task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0024-validation.html --json`
4. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
5. `powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1 -FailOnIssues`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
7. `git diff --check`
8. Real-name grep

## Risks
- The prototype can be mistaken for a hosted Web UI if docs are vague.
- Large repositories can produce long HTML output unless preview lists are capped.
- Static UI can drift from CLI output if shared summaries are not kept simple.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0024 implementation commit. Do not run destructive git commands.

## Completion notes
Implemented `ackit webui`, `IWebUiGenerator`, `.ackit/webui/` ignore behavior, focused tests, and Web UI prototype documentation.

Verification completed:
- Context7 `.NET` docs were checked before coding for current encoding/JSON guidance.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed with 46/46 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --output .ackit/webui/task-0024-validation.html --json` exited `0`, created the local Web UI prototype, and reported zero risk findings.
- In-app browser `file` + `://` navigation was blocked by browser security policy. Local static checks confirmed required sections exist, no remote asset/script/import references exist, and `.ackit/webui/` is ignored by git.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` exited `0` and reported no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1 -FailOnIssues` exited `0`; it reported expected public-release blockers and warned about uncommitted implementation changes.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed; it reported known public-release blockers in report-only mode.
- `git diff --check` passed.
- Real-name grep found no matches.

Public release remains blocked by maintainer-only TODO URL selection, release tag creation, push, and NuGet publish approval.
