# TASK-0014: v0.2 Expanded Generated Docs

## Purpose
Improve generated agent/context documentation so future `ackit generate` output gives coding agents richer, safer, task-first project context.

## Scope
- Expand agent instruction templates.
- Add repository health, risk summary, and recommended check values to generated docs.
- Improve generated AI workflow, security notes, development standard, handoff, and context pack templates.
- Update current generated repository docs to the new standard.
- Add tests for expanded generated content.
- Update docs and handoff.

## Out of scope
- Web UI.
- LLM integration.
- Automatic redaction.
- Remote upload.
- GitHub push.
- NuGet publish.
- Release tag creation.
- Replacing TODO package URLs.
- New dependencies.

## Affected files
- `src/AgentContextKit.Core/Generation.cs`
- `src/AgentContextKit.Core/Templates.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `AGENTS.md`
- `CLAUDE.md`
- `.cursor/rules/project.mdc`
- `.github/copilot-instructions.md`
- `docs/AI_WORKFLOW.md`
- `docs/SECURITY_NOTES.md`
- `docs/DEVELOPMENT_STANDARD.md`
- `.codex/HANDOFF.md`
- `.codex/CONTEXT_PACK.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0014-v020-expanded-generated-docs.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Generated English/Turkish templates are updated. Existing project docs remain primarily English where they already are.

## Audit/security impact
Generated docs will more explicitly preserve offline-first behavior, report-only redaction, release blockers, and required verification.

## Architecture impact
No boundary changes. Template rendering remains in Core and command output remains in CLI.

## CLI impact
`ackit generate` output content changes for newly generated files. Existing safe skip behavior remains unchanged.

## Testing impact
Add focused tests for expanded generated AGENTS content. Run build/test/scan/release verification.

## OSS/release impact
Improves agent onboarding and public repository clarity for v0.2.

## Acceptance criteria
- Generated AGENTS template includes repository health.
- Generated AGENTS template includes risk summary.
- Generated AGENTS template includes recommended checks.
- Generated handoff/context pack templates include richer project context.
- Current generated docs are updated to the expanded standard.
- Existing files are still skipped by default.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add health/risk/check values to generator template values.
4. Expand templates.
5. Add tests for expanded generated docs.
6. Update current generated docs.
7. Update roadmap, project map, changelog, handoff, and next steps.
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
- Richer generated docs can become stale if templates are not maintained.
- Updating current generated docs changes project-facing instructions and should stay aligned with AGENTS rules.
- Recommended checks are heuristic and may not fit every future stack perfectly.

## Rollback plan
Revert the TASK-0014 implementation commit. Do not run destructive git commands.

## Completion notes
Completed.

- Added generated template values for repository health, risk summary, and recommended checks.
- Expanded AGENTS, Claude, Cursor, Copilot, AI workflow, security notes, development standard, handoff, and context pack templates.
- Updated current generated docs to include health, risk, and recommended checks.
- Added focused test coverage for expanded generated AGENTS content.
- Fixed a template source self-scan false positive by converting escaped newline strings to raw multiline strings.
- Updated roadmap, project map, changelog, context pack, handoff, and next steps.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 31/31.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan` passed with no risk findings after template string cleanup.
- `powershell -NoProfile -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.
- Installed temporary `ackit scan --json` emitted schema version `2` with `riskSummary.total` equal to `0`.
- Release verification reported known public-release blockers in non-failing mode.
- Temporary package/tool folders were left under the user temp directory for inspection.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction was performed.
