# TASK-0039: v1.0 Final Local Readiness Consolidation

## Purpose
Consolidate v1.0 final local readiness after stable CLI contract review, config/generated convention freeze, and documentation/release gate freeze.

## Scope
- Add a local-only v1.0 final readiness script.
- Add v1.0 final readiness documentation.
- Verify TASK-0035 through TASK-0039 assets and v1.0 docs/scripts.
- Run and document all local v1.0 gates.
- Keep public-release blockers separate from local readiness issues.
- Update release validation, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff docs.

## Out of scope
- Replacing TODO package URLs.
- Creating or pushing a release tag.
- GitHub push.
- Remote repository creation.
- NuGet publish.
- Live LLM provider calls.
- Provider SDKs, HTTP clients, telemetry, upload, or API key handling.
- Runtime CLI behavior changes.
- Automatic redaction or deletion.

## Affected files
- `scripts/check-v100-readiness.ps1`
- `docs/V100_READINESS.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0039-v100-final-local-readiness-consolidation.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact. Documentation should remain release and maintainer oriented.

## Audit/security impact
High relevance. The final local readiness gate must preserve explicit separation between local readiness and maintainer-only public release approval.

## Architecture impact
No runtime architecture change expected. Adds a PowerShell readiness guard and documentation only.

## CLI impact
No CLI syntax change.

## Testing impact
No xUnit test expected. Script validation and existing build/test/scan checks are required.

## OSS/release impact
Consolidates v1.0 local readiness. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `scripts/check-v100-readiness.ps1` exists and is local-only.
- `docs/V100_READINESS.md` exists.
- `scripts/check-v100-readiness.ps1 -FailOnIssues` exits 0 when v1.0 local readiness assets are present.
- The script checks TASK-0035 through TASK-0039 and v1.0 docs/scripts.
- The script reports public-release blockers separately from local v1.0 readiness issues.
- The readiness doc documents usage, expected public blockers, required validation, and maintainer-only release boundary.
- Release validation and documentation index mention v1.0 readiness.
- Roadmap marks v1.0 final local readiness consolidated.
- Project map and changelog include the new doc/script/task.
- Context pack, next steps, and session handoff are updated.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan --ci` reports no risk findings.
- `scripts/check-cli-contract.ps1 -FailOnIssues` passes.
- `scripts/check-config-generated-conventions.ps1 -FailOnIssues` passes.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` passes.
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
4. Add v1.0 final readiness script.
5. Add v1.0 final readiness documentation.
6. Update release validation, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff.
7. Run the v1.0 readiness script in report-only and failing modes.
8. Run build/test and local scan validation.
9. Run `scripts/check-cli-contract.ps1 -FailOnIssues`.
10. Run `scripts/check-config-generated-conventions.ps1 -FailOnIssues`.
11. Run `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`.
12. Run `scripts/check-v050-readiness.ps1 -FailOnIssues`.
13. Run `scripts/verify-release.ps1`.
14. Run `git diff --check`.
15. Run real-name grep.
16. Update completion notes.
17. Commit implementation.
18. Continue with maintainer-only blocker documentation or next task without asking for permission.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
6. `powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues`
7. `powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues`
8. `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`
9. `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues`
10. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
11. `git diff --check`
12. Real-name grep

## Risks
- Final local readiness can be misread as public release approval if docs are vague.
- Public-release blockers remain intentionally unresolved.
- This does not replace manual maintainer review for release URLs, tag, push, or NuGet publish.

## Rollback plan
Revert the TASK-0039 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
