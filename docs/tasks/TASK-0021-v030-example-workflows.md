# TASK-0021: v0.3 Example Workflows

## Purpose
Add copy-paste-ready workflow examples that combine scan, CI mode, HTML reports, doctor checks, release validation, and sample repository checks.

## Scope
- Add a dedicated example workflows document.
- Update existing examples and AI workflow docs.
- Update documentation index, README, roadmap, changelog, project map, handoff, and next steps.
- Keep workflows local-only and offline-first.

## Out of scope
- New CLI commands.
- New runtime behavior.
- New GitHub Actions workflow files.
- NuGet publish.
- GitHub push.
- Release tag creation.
- Replacing TODO package URLs.

## Affected files
- `docs/EXAMPLE_WORKFLOWS.md`
- `docs/EXAMPLES.md`
- `docs/AI_WORKFLOW.md`
- `docs/DOCUMENTATION_INDEX.md`
- `README.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0021-v030-example-workflows.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. New workflow docs are English.

## Audit/security impact
Improves safe usage by documenting local-only workflows and public release gates. Does not add remote calls or automatic redaction.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run standard validation and self-scan to ensure docs do not introduce risk findings.

## OSS/release impact
Improves onboarding and maintainer release readiness. Public release blockers remain maintainer-only.

## Acceptance criteria
- `docs/EXAMPLE_WORKFLOWS.md` exists.
- Workflows include local development, CI gate, HTML report review, public release preflight, and sample scanning.
- Examples use current commands including `scan --ci` and `report`.
- Docs state that public release actions remain maintainer-only.
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
3. Add example workflows document.
4. Update examples, AI workflow, docs index, README, roadmap, changelog, project map, handoff, and next steps.
5. Run build/test.
6. Run `ackit scan --ci`.
7. Run `scripts/verify-release.ps1`.
8. Run `git diff --check`.
9. Run real-name grep.
10. Update completion notes.
11. Commit implementation.
12. Continue with the next v0.3 task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
4. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
5. `git diff --check`
6. Real-name grep

## Risks
- Workflow docs can become stale as commands evolve.
- Public release preflight examples can be mistaken for approval unless maintainer-only gates remain explicit.

## Rollback plan
Revert the TASK-0021 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
