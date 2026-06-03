# TASK-0035: v1.0 Stabilization Plan

## Purpose
Create a concrete v1.0 stabilization plan that turns the current roadmap bullets into release-trackable local work without pushing, tagging, publishing, or resolving maintainer-only public-release blockers.

## Scope
- Add a v1.0 stabilization planning document.
- Define local readiness themes for stable CLI behavior, config format, generated file conventions, documentation completeness, test reliability, CI health, privacy/security gates, and packaging handoff.
- Keep maintainer-only public release decisions explicitly separate from local stabilization work.
- Update roadmap, documentation index, project map, changelog, context pack, next steps, and session handoff docs.

## Out of scope
- Replacing TODO package URLs.
- Creating or pushing a release tag.
- GitHub push.
- Remote repository creation.
- NuGet publish.
- Live LLM provider calls.
- Provider SDKs, HTTP clients, telemetry, upload, or API key handling.
- Runtime CLI behavior changes unless a documentation consistency fix is required.
- Automatic redaction or deletion.

## Affected files
- `docs/V100_STABILIZATION_PLAN.md`
- `docs/ROADMAP.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0035-v100-stabilization-plan.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact. Documentation should stay clear and local-release oriented. No new localization surface is expected.

## Audit/security impact
Improves release auditability by making v1.0 readiness criteria explicit. Must continue to preserve offline-first, no-upload, no-provider-call, and no-secret-handling boundaries.

## Architecture impact
No runtime architecture change expected. This task is documentation/planning only.

## CLI impact
No CLI syntax change.

## Testing impact
No xUnit test expected. Existing build/test/scan/readiness checks should remain passing.

## OSS/release impact
Clarifies the path to v1.0. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `docs/V100_STABILIZATION_PLAN.md` exists.
- The plan defines v1.0 stabilization themes and local acceptance gates.
- The plan separates local stabilization from maintainer-only public release actions.
- Roadmap references the v1.0 stabilization plan.
- Documentation index references the v1.0 stabilization plan.
- Project map and changelog include the new doc/task.
- Context pack, next steps, and session handoff are updated.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan --ci` reports no risk findings.
- `scripts/check-v050-readiness.ps1 -FailOnIssues` passes.
- `scripts/verify-release.ps1` passes.
- `git diff --check` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, remote calls, API key handling, upload, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Read required project docs before implementation.
4. Add `docs/V100_STABILIZATION_PLAN.md`.
5. Update roadmap, documentation index, project map, changelog, context pack, next steps, and session handoff.
6. Run build/test and local scan validation.
7. Run `scripts/check-v050-readiness.ps1 -FailOnIssues`.
8. Run `scripts/verify-release.ps1`.
9. Run `git diff --check`.
10. Run real-name grep.
11. Update completion notes.
12. Commit implementation.
13. Continue with the next documented v1.0 task without asking for permission.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
4. `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues`
5. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
6. `git diff --check`
7. Real-name grep

## Risks
- A planning document can drift from implementation if follow-up tasks are not created from it.
- Public-release blockers remain intentionally unresolved.
- This does not replace manual maintainer review for release URLs, tag, push, or NuGet publish.

## Rollback plan
Revert the TASK-0035 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
