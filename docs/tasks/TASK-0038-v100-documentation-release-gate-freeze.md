# TASK-0038: v1.0 Documentation And Release Gate Freeze

## Purpose
Define and locally verify the v1.0 documentation and release gate freeze so release-critical docs, validation scripts, public blockers, and maintainer-only actions remain aligned before final v1.0 readiness consolidation.

## Scope
- Add a documentation/release gate freeze document.
- Add a local-only documentation/release gate check script.
- Verify documentation index coverage for release-critical docs.
- Verify release validation docs reference readiness, CLI contract, config/generated conventions, public gates, and maintainer handoff.
- Verify public release blockers remain explicit until maintainer-only URL, tag, push, and publish actions are approved.
- Verify release gate scripts exist and are documented.
- Update roadmap, project map, changelog, context pack, next steps, and session handoff docs.

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
- `docs/V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md`
- `scripts/check-v100-documentation-release-gates.ps1`
- `docs/RELEASE_VALIDATION.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0038-v100-documentation-release-gate-freeze.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact. Documentation should remain release and maintainer oriented.

## Audit/security impact
Improves release auditability by freezing release gate documentation and preserving explicit blocker separation.

## Architecture impact
No runtime architecture change expected. Script should inspect local docs/scripts only.

## CLI impact
No CLI syntax change.

## Testing impact
No xUnit test expected. Script validation and existing build/test/scan checks are required.

## OSS/release impact
Clarifies final pre-v1.0 local release gate expectations. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `docs/V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md` exists.
- `scripts/check-v100-documentation-release-gates.ps1` exists and is local-only.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` exits 0 when release docs/scripts are aligned.
- The script verifies release-critical docs exist and are referenced from `docs/DOCUMENTATION_INDEX.md`.
- The script verifies release validation references v0.2 through v0.5 readiness, CLI contract, config/generated conventions, public gates, release blockers, public audit, package metadata, and maintainer handoff.
- The script verifies public-release blockers remain explicit in blocker docs.
- The script verifies release gate scripts exist.
- Roadmap references the documentation/release gate freeze.
- Project map and changelog include the new doc/script/task.
- Context pack, next steps, and session handoff are updated.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan --ci` reports no risk findings.
- `scripts/check-cli-contract.ps1 -FailOnIssues` passes.
- `scripts/check-config-generated-conventions.ps1 -FailOnIssues` passes.
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
4. Add documentation/release gate freeze documentation.
5. Add local documentation/release gate check script.
6. Update release validation and documentation index if needed for freeze clarity.
7. Update roadmap, project map, changelog, context pack, next steps, and session handoff.
8. Run the documentation/release gate script in report-only and failing modes.
9. Run build/test and local scan validation.
10. Run `scripts/check-cli-contract.ps1 -FailOnIssues`.
11. Run `scripts/check-config-generated-conventions.ps1 -FailOnIssues`.
12. Run `scripts/check-v050-readiness.ps1 -FailOnIssues`.
13. Run `scripts/verify-release.ps1`.
14. Run `git diff --check`.
15. Run real-name grep.
16. Update completion notes.
17. Commit implementation.
18. Continue with the next documented v1.0 task without asking for permission.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
6. `powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues`
7. `powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues`
8. `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues`
9. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
10. `git diff --check`
11. Real-name grep

## Risks
- The gate script can become stale if release docs are renamed.
- Public-release blockers remain intentionally unresolved.
- This does not replace manual maintainer review for release URLs, tag, push, or NuGet publish.

## Rollback plan
Revert the TASK-0038 implementation commit. Do not run destructive git commands.

## Completion notes
Completed in TASK-0038.

- Added `docs/V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md` with release-critical docs, release gate scripts, public release blockers, local freeze validation, and safety boundaries.
- Added `scripts/check-v100-documentation-release-gates.ps1` to verify release-critical docs, documentation index coverage, release validation references, public blocker docs, public gate docs, audit docs, and maintainer handoff notes.
- Updated release validation, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff docs.
- No CLI syntax change, runtime behavior change, provider call, SDK, HTTP client, API key handling, upload, push, tag, publish, remote creation, or automatic redaction was added.

Verification:

- `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1` passed and reported no documentation/release gate issues.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` exited 0.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 56/56 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` passed with no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues` exited 0.
- `powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues` exited 0.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues` exited 0 with public blockers reported separately.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.
- `git diff --check` passed.
- Real-name grep found no matches.
