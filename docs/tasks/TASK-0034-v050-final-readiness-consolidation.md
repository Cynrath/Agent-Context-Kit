# TASK-0034: v0.5 Final Readiness Consolidation

## Purpose
Consolidate v0.5 local readiness after optional LLM architecture documentation, provider-neutral abstraction, dry-run prompt pack generation, and user-approved context export manifest support.

## Scope
- Add a local-only v0.5 readiness script.
- Add v0.5 readiness documentation.
- Verify required v0.5 task files, LLM architecture docs, CLI docs, JSON/config docs, generated artifact ignore rules, provider abstraction, prompt-pack generation, and context-export manifest assets.
- Report public-release blockers separately from v0.5 local readiness issues.
- Update product spec command inventory, release validation, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff docs.

## Out of scope
- Calling any LLM provider.
- Adding provider SDKs, HTTP clients, hosted services, telemetry, or API key handling.
- Uploading prompt packs or repository content.
- Replacing TODO package URLs.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Automatic redaction or deletion.

## Affected files
- `scripts/check-v050-readiness.ps1`
- `docs/V050_READINESS.md`
- `docs/PRODUCT_SPEC.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0034-v050-final-readiness-consolidation.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
No runtime permission change. Readiness checks are local-only and must not mutate repository content.

## SEO/i18n impact
No SEO impact. Documentation and script output should stay clear and local-review oriented. Product spec command inventory should include v0.5 commands.

## Audit/security impact
High relevance. The readiness review must confirm v0.5 remains offline-first: no provider adapter, no remote calls, no API key handling, no uploads, and generated prompt/export artifacts ignored by default.

## Architecture impact
No runtime architecture change expected. Adds a PowerShell readiness guard and documentation only.

## CLI impact
No CLI syntax change. The readiness script should validate existing `prompt-pack` and `context-export` assets.

## Testing impact
No xUnit test expected unless implementation touches product code. Script validation and existing build/test/release checks are required.

## OSS/release impact
Strengthens local readiness review. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `scripts/check-v050-readiness.ps1` exists and is local-only.
- `scripts/check-v050-readiness.ps1 -FailOnIssues` exits 0 when v0.5 assets are present.
- The script checks TASK-0030 through TASK-0034 and core v0.5 docs/assets.
- The script verifies `prompt-pack` and `context-export` are present in CLI/help/docs-relevant files.
- The script verifies `.ackit/prompt-packs/` and `.ackit/context-exports/` are ignored/default-configured.
- The script reports public-release blockers separately from local v0.5 readiness issues.
- `docs/V050_READINESS.md` documents usage, failing gate behavior, expected blockers, required validation, and next roadmap step.
- Product spec command inventory includes `prompt-pack` and `context-export`.
- Release validation and documentation index mention v0.5 readiness.
- Roadmap marks v0.5 final readiness consolidated.
- Project map and changelog include the new script/doc/task.
- Context pack, next steps, and session handoff are updated.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit prompt-pack --json` creates a local ignored validation file.
- `ackit context-export --approve --json` creates a local ignored validation manifest.
- `ackit scan --ci` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- `git diff --check` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, remote calls, API key handling, upload, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Read required project docs and current .NET/PowerShell guidance before implementation.
4. Add the v0.5 readiness script by following the existing v0.4 readiness pattern.
5. Add v0.5 readiness documentation.
6. Update product spec, release validation, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff.
7. Run the v0.5 readiness script in report-only and failing modes.
8. Run build/test and local CLI validation.
9. Run `ackit prompt-pack --json`.
10. Run `ackit context-export --approve --json`.
11. Run `ackit scan --ci`.
12. Run `scripts/verify-release.ps1`.
13. Run `git diff --check`.
14. Run real-name grep.
15. Update completion notes.
16. Commit implementation.
17. Continue with the next roadmap task without asking for permission.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1`
2. `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- prompt-pack --output .ackit/prompt-packs/task-0034-validation.md --json`
6. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- context-export --prompt-pack .ackit/prompt-packs/task-0034-validation.md --approve --output .ackit/context-exports/task-0034-validation.json --json`
7. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
8. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
9. `git diff --check`
10. Real-name grep

## Risks
- The readiness script can become stale if v0.5 assets move or are renamed.
- Public-release blockers remain intentionally unresolved.
- This does not approve live LLM provider use; it only validates local dry-run and approval-manifest assets.
- This does not replace manual maintainer review for release URLs, tag, push, or NuGet publish.

## Rollback plan
Revert the TASK-0034 implementation commit. Do not run destructive git commands.

## Completion notes
Completed in TASK-0034.

- Added `scripts/check-v050-readiness.ps1` with local-only v0.5 asset and content checks.
- Added `docs/V050_READINESS.md` with report-only and failing gate usage, safety boundary, expected public blockers, required validation, and v1.0 next-step context.
- Updated product spec current command inventory to include `prompt-pack` and `context-export`.
- Updated release validation, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff docs.
- Kept public-release blockers separate from local v0.5 readiness issues.
- Checked current .NET CLI guidance through Context7 `/dotnet/docs` and PowerShell script guidance through Microsoft Learn before implementation.

Verification:

- `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1` passed and reported no v0.5 asset issues.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues` exited 0 with public blockers reported separately.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 56/56 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- prompt-pack --output .ackit/prompt-packs/task-0034-validation.md --json` created the ignored local prompt pack with risk summary 0.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- context-export --prompt-pack .ackit/prompt-packs/task-0034-validation.md --approve --output .ackit/context-exports/task-0034-validation.json --json` created the ignored local approval manifest with risk summary 0.
- `git check-ignore -v .ackit/prompt-packs/task-0034-validation.md` confirmed the prompt-pack validation artifact is ignored.
- `git check-ignore -v .ackit/context-exports/task-0034-validation.json` confirmed the context-export validation artifact is ignored.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` passed with no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.
- `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1` completed in report-only mode with known public blockers.
- `git diff --check` passed.
- Real-name grep found no matches.
