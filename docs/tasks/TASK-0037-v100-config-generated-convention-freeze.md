# TASK-0037: v1.0 Config And Generated File Convention Freeze

## Purpose
Define and locally verify the v1.0 configuration and generated-file conventions so default config, ignored artifact paths, generated output paths, skip behavior, and documentation stay aligned before public release.

## Scope
- Add a config/generated-file convention document.
- Add a local-only convention check script.
- Verify `.ackit/config.yml` defaults documented in `docs/CONFIGURATION.md` match `AckitConfig.Default` and generated default config output.
- Verify generated artifact directories are ignored by `.gitignore` and default config.
- Verify generated output path conventions are documented for reports, Web UI, prompt packs, context export manifests, agent files, and task files.
- Keep maintainer-only public release actions separate from local convention readiness.
- Update roadmap, documentation index, project map, changelog, context pack, next steps, and session handoff docs.

## Out of scope
- Changing the runtime config schema unless a documentation/script inconsistency blocks the convention freeze.
- Replacing TODO package URLs.
- Creating or pushing a release tag.
- GitHub push.
- Remote repository creation.
- NuGet publish.
- Live LLM provider calls.
- Provider SDKs, HTTP clients, telemetry, upload, or API key handling.
- Automatic redaction or deletion.

## Affected files
- `docs/CONFIG_GENERATED_CONVENTIONS.md`
- `scripts/check-config-generated-conventions.ps1`
- `docs/CONFIGURATION.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0037-v100-config-generated-convention-freeze.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact. Documentation should remain local-release oriented.

## Audit/security impact
Improves release auditability by freezing local artifact paths and generated-file behavior. Must preserve offline-first, skip-existing, and ignored-generated-artifact defaults.

## Architecture impact
No runtime architecture change expected. Script should inspect local docs/source/config behavior only.

## CLI impact
No CLI syntax change expected.

## Testing impact
No xUnit test expected unless runtime code changes. Script validation and existing build/test/scan checks are required.

## OSS/release impact
Clarifies stable config and generated-file conventions for v1.0. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `docs/CONFIG_GENERATED_CONVENTIONS.md` exists.
- `scripts/check-config-generated-conventions.ps1` exists and is local-only.
- `scripts/check-config-generated-conventions.ps1 -FailOnIssues` exits 0 when config/generated-file docs and source conventions are aligned.
- The script verifies default ignore paths in source, generated config output, `.gitignore`, and docs.
- The script verifies generated output path conventions for report, Web UI, prompt-pack, context-export, task files, and agent/context files.
- The convention document records skip-existing behavior, repository-relative output behavior, ignored artifact paths, and generated file ownership.
- Documentation index references the convention document.
- Roadmap references the convention freeze.
- Project map and changelog include the new doc/script/task.
- Context pack, next steps, and session handoff are updated.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan --ci` reports no risk findings.
- `scripts/check-cli-contract.ps1 -FailOnIssues` passes.
- `scripts/check-v050-readiness.ps1 -FailOnIssues` passes.
- `scripts/verify-release.ps1` passes.
- `git diff --check` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, remote calls, API key handling, upload, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Read required project docs and current .NET/PowerShell guidance before implementation.
4. Add config/generated-file convention documentation.
5. Add local convention check script.
6. Update config docs if needed for convention clarity.
7. Update roadmap, documentation index, project map, changelog, context pack, next steps, and session handoff.
8. Run the convention script in report-only and failing modes.
9. Run build/test and local scan validation.
10. Run `scripts/check-cli-contract.ps1 -FailOnIssues`.
11. Run `scripts/check-v050-readiness.ps1 -FailOnIssues`.
12. Run `scripts/verify-release.ps1`.
13. Run `git diff --check`.
14. Run real-name grep.
15. Update completion notes.
16. Commit implementation.
17. Continue with the next documented v1.0 task without asking for permission.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
6. `powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues`
7. `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues`
8. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
9. `git diff --check`
10. Real-name grep

## Risks
- The convention script can become stale if generated paths move.
- Freezing conventions too early can require a documented pre-v1.0 breaking-change task later.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0037 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
