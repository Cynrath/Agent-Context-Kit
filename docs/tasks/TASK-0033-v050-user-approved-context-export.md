# TASK-0033: v0.5 User-Approved Context Export

## Purpose
Add a local-only context export manifest workflow that records explicit user approval for a dry-run prompt pack without uploading content or calling an LLM provider.

## Scope
- Add a local `ackit context-export` command.
- Require an explicit approval flag before creating an export manifest.
- Read a repository-relative prompt pack path.
- Generate a repository-relative JSON manifest under an ignored `.ackit/context-exports/` path by default.
- Include source prompt pack path, file size, approval mode, timestamp, risk summary, and explicit no-remote-call notes.
- Support `--prompt-pack <repo-relative.md>`, `--output <repo-relative.json>`, `--approve`, `--lang en|tr`, and `--json`.
- Reject paths outside the repository.
- Skip existing manifest files by default.
- Add focused Core/CLI tests and update docs.

## Out of scope
- Calling any LLM provider.
- OpenAI or other provider SDKs.
- API key setup, reading, storage, or validation.
- Sending prompt packs anywhere.
- Provider adapter implementation.
- Hosted/server Web UI.
- Telemetry or analytics.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Automatic redaction or deletion.

## Affected files
- `src/AgentContextKit.Core/Abstractions.cs`
- `src/AgentContextKit.Core/Generation.cs`
- `src/AgentContextKit.Core/Models.cs`
- `src/AgentContextKit.Core/Configuration.cs`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `.gitignore`
- `README.md`
- `README.tr.md`
- `docs/CLI_REFERENCE.md`
- `docs/EXAMPLES.md`
- `docs/JSON_OUTPUT.md`
- `docs/CONFIGURATION.md`
- `docs/LLM_INTEGRATION_ARCHITECTURE.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0033-v050-user-approved-context-export.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
No runtime permission change. The command requires explicit local approval and writes only inside the repository.

## SEO/i18n impact
No SEO impact. CLI/user-facing output should support English/Turkish where practical.

## Audit/security impact
High relevance. The manifest records local approval only and must clearly state that no remote call or content upload occurred.

## Architecture impact
Adds a Core context-export manifest generator while keeping CLI parsing/output separate from generation logic.

## CLI impact
Adds:

```powershell
ackit context-export --prompt-pack <repo-relative.md> --approve [--output <repo-relative.json>] [--lang en|tr] [--json]
```

## Testing impact
Add focused tests for required approval, required prompt pack path, safe output handling, existing-file skip behavior, JSON metadata, and no remote/provider dependency.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `ackit context-export` exists in CLI help.
- Command fails without `--approve`.
- Command fails without `--prompt-pack`.
- Prompt pack and output paths must stay inside the repository.
- Existing manifest files are skipped by default.
- Manifest JSON includes source prompt pack path, source size, approval mode, generated timestamp, repository metadata, risk summary, and no-remote-call note.
- `--json` emits schema version, command name, repository metadata, risk summary, and manifest file metadata.
- No provider SDK, HTTP client, API key handling, content upload, or remote call is added.
- `.gitignore` ignores `.ackit/context-exports/`.
- Docs and roadmap are updated.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit context-export --json` creates a local ignored validation manifest when explicitly approved.
- `ackit scan --ci` reports no risk findings.
- `scripts/check-v040-readiness.ps1 -FailOnIssues` still passes.
- `scripts/verify-release.ps1` passes.
- `git diff --check` passes.
- Real-name grep finds no matches.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, remote calls, API key handling, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Read required project docs and current .NET guidance before implementation.
4. Add Core context export manifest generator and model.
5. Add CLI command and JSON output.
6. Add focused tests.
7. Update docs, roadmap, project map, changelog, context pack, next steps, and session handoff.
8. Run build/test and local validation.
9. Run `ackit prompt-pack --json` to create a local validation source pack.
10. Run `ackit context-export --approve --json`.
11. Run `ackit scan --ci`.
12. Run `scripts/check-v040-readiness.ps1 -FailOnIssues`.
13. Run `scripts/verify-release.ps1`.
14. Run `git diff --check`.
15. Run real-name grep.
16. Update completion notes.
17. Commit implementation.
18. Continue with the next roadmap task without asking for permission.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- prompt-pack --output .ackit/prompt-packs/task-0033-source.md --json`
4. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- context-export --prompt-pack .ackit/prompt-packs/task-0033-source.md --approve --output .ackit/context-exports/task-0033-validation.json --json`
5. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
6. `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues`
7. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
8. `git diff --check`
9. Real-name grep

## Risks
- Users can still manually copy prompt packs elsewhere after local approval.
- The approval flag can be misunderstood as provider approval if docs are vague.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0033 implementation commit. Do not run destructive git commands.

## Completion notes
Completed in TASK-0033.

- Added local-only `ackit context-export` command with required `--prompt-pack` and `--approve`.
- Added `IContextExportManifestGenerator`, `ContextExportSpec`, and `ContextExportManifestGenerator`.
- Default output is `.ackit/context-exports/context-export-manifest.json`; `.ackit/context-exports/` is ignored by git and default config.
- Manifest JSON includes source prompt pack path/size, approval mode, generated timestamp, repository metadata, risk summary, and no-remote/no-upload/no-API-key safety fields.
- Existing manifests are skipped by default, unsafe paths are rejected, and missing approval/prompt-pack inputs return exit code 1.
- Added focused generator, CLI JSON, approval-required, skip, unsafe path, and config tests.
- Updated README, CLI reference, examples, JSON/config docs, optional LLM architecture, roadmap, project map, changelog, context pack, next steps, and session handoff.
- Checked current .NET JSON/path guidance through Context7 before implementation.

Verification:

- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 56/56 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- prompt-pack --output .ackit/prompt-packs/task-0033-source.md --json` created the ignored local source prompt pack with risk summary 0.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- context-export --prompt-pack .ackit/prompt-packs/task-0033-source.md --approve --output .ackit/context-exports/task-0033-validation.json --json` created the ignored local approval manifest with risk summary 0.
- Static manifest checks found approval mode, source prompt pack path, no-remote-call, and no-API-key fields.
- `git check-ignore -v .ackit/context-exports/task-0033-validation.json` confirmed the validation artifact is ignored.
- Missing `--approve` validation returned exit code 1 as expected.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` passed with no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues` exited 0 with public blockers reported separately.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed and installed help showed `context-export`.
- `git diff --check` passed.
- Real-name grep found no matches.
