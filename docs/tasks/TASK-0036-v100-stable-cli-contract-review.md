# TASK-0036: v1.0 Stable CLI Contract Review

## Purpose
Define and locally verify the stable v1.0 CLI contract for commands, options, exit behavior, JSON output references, and documentation alignment before any public release action.

## Scope
- Add a stable CLI contract document.
- Add a local-only CLI contract check script.
- Verify required commands and options are present in CLI help.
- Verify release-critical CLI docs reference the stable command surface.
- Keep public-release blockers and maintainer-only actions separate from CLI contract readiness.
- Update roadmap, documentation index, project map, changelog, context pack, next steps, and session handoff docs.

## Out of scope
- Changing CLI command syntax unless an inconsistency blocks contract documentation.
- Replacing TODO package URLs.
- Creating or pushing a release tag.
- GitHub push.
- Remote repository creation.
- NuGet publish.
- Live LLM provider calls.
- Provider SDKs, HTTP clients, telemetry, upload, or API key handling.
- Automatic redaction or deletion.

## Affected files
- `docs/CLI_CONTRACT.md`
- `scripts/check-cli-contract.ps1`
- `docs/ROADMAP.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0036-v100-stable-cli-contract-review.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
No SEO impact. CLI contract docs should mention English/Turkish language option behavior where relevant.

## Audit/security impact
Improves release auditability by making the local command surface explicit and script-checkable. Must not call remote services or expose secrets.

## Architecture impact
No runtime architecture change expected. Script should inspect local help/docs only.

## CLI impact
No CLI syntax change expected.

## Testing impact
No xUnit test expected unless CLI code changes. Script validation and existing build/test/scan checks are required.

## OSS/release impact
Clarifies the stable CLI contract path for v1.0. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `docs/CLI_CONTRACT.md` exists.
- `scripts/check-cli-contract.ps1` exists and is local-only.
- `scripts/check-cli-contract.ps1 -FailOnIssues` exits 0 when CLI help/docs are aligned.
- The script verifies required commands in `ackit --help`.
- The script verifies command references in CLI reference, JSON output docs, release validation docs, and README.
- The contract document lists commands, stability rules, exit behavior, JSON docs dependency, localization option behavior, and non-goals.
- Roadmap references the stable CLI contract review.
- Documentation index references the CLI contract doc.
- Project map and changelog include the new doc/script/task.
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
3. Read required project docs and current .NET/PowerShell guidance before implementation.
4. Add stable CLI contract documentation.
5. Add local CLI contract check script.
6. Update roadmap, documentation index, project map, changelog, context pack, next steps, and session handoff.
7. Run the CLI contract script in report-only and failing modes.
8. Run build/test and local scan validation.
9. Run `scripts/check-v050-readiness.ps1 -FailOnIssues`.
10. Run `scripts/verify-release.ps1`.
11. Run `git diff --check`.
12. Run real-name grep.
13. Update completion notes.
14. Commit implementation.
15. Continue with the next documented v1.0 task without asking for permission.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
6. `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- The script can become stale if help text or docs move.
- This does not freeze implementation forever; it records the current v1.0 target contract.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0036 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
