# TASK-0032: v0.5 Dry-Run Prompt Pack Generation

## Purpose
Add local-only dry-run prompt pack generation so users can review exactly what would be sent to a future LLM provider without making remote calls.

## Scope
- Add a local `ackit prompt-pack` command.
- Generate a repository-relative Markdown prompt pack under an ignored `.ackit/prompt-packs/` path by default.
- Include scan summary, stack signals, repository health, risk summary, generated/context file status, latest task summary, and explicit "no remote call" notes.
- Support `--output <repo-relative.md>`, `--lang en|tr`, and `--json`.
- Reject output paths outside the repository.
- Skip existing output files by default.
- Add focused Core/CLI tests and update docs.

## Out of scope
- Calling any LLM provider.
- OpenAI or other provider SDKs.
- API key setup, reading, storage, or validation.
- User-approved context export implementation.
- Provider adapter implementation.
- Prompt optimization for a specific model.
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
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `.gitignore`
- `README.md`
- `README.tr.md`
- `docs/CLI_REFERENCE.md`
- `docs/EXAMPLES.md`
- `docs/LLM_INTEGRATION_ARCHITECTURE.md`
- `docs/JSON_OUTPUT.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0032-v050-dry-run-prompt-pack-generation.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
No runtime permission change. The command must not export data remotely and must write only inside the repository.

## SEO/i18n impact
No SEO impact. CLI/user-facing output should support English/Turkish where practical.

## Audit/security impact
High relevance. The prompt pack is a local review artifact and must make clear that it is not approval to call a provider. It must not bypass risk scanning, redaction review, or public-release blockers.

## Architecture impact
Adds a Core prompt-pack generator and keeps CLI parsing/output separate from generation logic.

## CLI impact
Adds:

```powershell
ackit prompt-pack [--output <repo-relative.md>] [--lang en|tr] [--json]
```

## Testing impact
Add focused tests for default output generation, safe output path handling, existing-file skip behavior, JSON metadata, and no remote/provider dependency.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `ackit prompt-pack` exists in CLI help.
- Default output is local and ignored under `.ackit/prompt-packs/`.
- Custom output paths must stay inside the repository.
- Existing prompt-pack files are skipped by default.
- Markdown output includes scan summary, stacks, health, risk summary, generated/context file status, latest task summary, and explicit no-remote-call notes.
- `--json` emits schema version, command name, repository metadata, risk summary, and prompt pack file metadata.
- No provider SDK, HTTP client, API key handling, or remote call is added.
- `.gitignore` ignores `.ackit/prompt-packs/`.
- Docs and roadmap are updated.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit prompt-pack --json` creates a local ignored validation file.
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
4. Add Core prompt-pack generator and result model.
5. Add CLI command and JSON output.
6. Add focused tests.
7. Update docs, roadmap, project map, changelog, context pack, next steps, and session handoff.
8. Run build/test and local validation.
9. Run `ackit prompt-pack --json`.
10. Run `ackit scan --ci`.
11. Run `scripts/check-v040-readiness.ps1 -FailOnIssues`.
12. Run `scripts/verify-release.ps1`.
13. Run `git diff --check`.
14. Run real-name grep.
15. Update completion notes.
16. Commit implementation.
17. Continue with the next roadmap task without asking for permission.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- prompt-pack --output .ackit/prompt-packs/task-0032-validation.md --json`
4. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
5. `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
7. `git diff --check`
8. Real-name grep

## Risks
- Prompt packs can expose sensitive context if later copied to a provider manually.
- The first implementation can become too broad if it tries to include full source content.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0032 implementation commit. Do not run destructive git commands.

## Completion notes
Completed in TASK-0032.

- Added local-only `ackit prompt-pack` command with `--output`, `--lang`, and `--json`.
- Added `IPromptPackGenerator` and `PromptPackGenerator` for safe repository-relative Markdown output.
- Default output is `.ackit/prompt-packs/prompt-pack.md`; `.ackit/prompt-packs/` is ignored by git and default config.
- Prompt packs include scan summary, stacks, repository health, generated/context file status, latest task summary, and explicit no-remote/no-API-key notes.
- Existing prompt pack files are skipped by default, and unsafe output paths are rejected.
- Added focused generator, CLI JSON, skip, unsafe path, and config tests.
- Updated README, CLI reference, examples, JSON/config docs, optional LLM architecture, roadmap, project map, changelog, context pack, next steps, and session handoff.
- Checked current .NET file path guidance through Context7 before implementation.

Verification:

- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 51/51 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- prompt-pack --output .ackit/prompt-packs/task-0032-validation-final.md --json` created the ignored local prompt pack with risk summary 0 and TASK-0032 shown as completed.
- Static prompt-pack checks found dry-run/no-remote/no-API-key notes and expected sections.
- `git check-ignore -v .ackit/prompt-packs/task-0032-validation-final.md` confirmed the validation artifact is ignored.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` passed with no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues` exited 0 with public blockers reported separately.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed and installed help showed `prompt-pack`.
- `git diff --check` passed.
- Real-name grep found no matches.
