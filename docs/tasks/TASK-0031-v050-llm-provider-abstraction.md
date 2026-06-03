# TASK-0031: v0.5 LLM Provider Abstraction

## Purpose
Add provider-neutral LLM request/response abstractions for future optional integrations without adding provider SDKs, remote calls, API key handling, or CLI command changes.

## Scope
- Add Core provider-neutral LLM interfaces and models.
- Keep models immutable or init-only where practical.
- Include request metadata for dry-run/audit-friendly usage.
- Include response metadata for provider name, request ID, token counts when available, and warnings.
- Add focused tests using fake/in-memory providers only.
- Update optional LLM architecture docs and project docs.

## Out of scope
- OpenAI or other provider adapters.
- OpenAI SDK or HTTP client implementation.
- API key setup or storage.
- Remote LLM calls.
- Prompt pack generation.
- User-approved context export implementation.
- CLI command syntax changes.
- Hosted/server Web UI.
- Telemetry or analytics.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Automatic redaction or deletion.

## Affected files
- `src/AgentContextKit.Core/Abstractions.cs`
- `src/AgentContextKit.Core/Models.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/LLM_INTEGRATION_ARCHITECTURE.md`
- `docs/ARCHITECTURE.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0031-v050-llm-provider-abstraction.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
No runtime permission change. Future provider calls still require explicit consent gates and approved secret handling.

## SEO/i18n impact
No SEO impact. Public API names should be English.

## Audit/security impact
Defines future audit-friendly request/response boundaries. Must not weaken offline-first behavior or introduce remote access.

## Architecture impact
Adds future Core extension points while keeping CLI/Core separation intact.

## CLI impact
No CLI syntax or behavior change.

## Testing impact
Add focused tests proving provider-neutral models can be used with a fake provider and do not require network/provider dependencies.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- Provider-neutral `ILLMProvider` abstraction exists in Core.
- Request/response models exist in Core and do not depend on provider SDK packages.
- Request model supports messages, metadata, dry-run/export-review identifiers, and cancellation-friendly provider calls.
- Response model supports text output, provider/model names, request ID, token counts when available, and warnings.
- Tests use fake provider behavior only.
- No new package dependency is added.
- No CLI behavior changes.
- Optional LLM architecture and architecture docs mention the abstraction.
- Roadmap marks `ILLMProvider` abstraction completed.
- Project map, changelog, context pack, next steps, and session handoff are updated.
- `dotnet build` passes.
- `dotnet test` passes.
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
4. Add provider-neutral Core interfaces and models.
5. Add focused fake-provider tests.
6. Update optional LLM architecture and related docs.
7. Run build/test and local validation.
8. Run `ackit scan --ci`.
9. Run `scripts/check-v040-readiness.ps1 -FailOnIssues`.
10. Run `scripts/verify-release.ps1`.
11. Run `git diff --check`.
12. Run real-name grep.
13. Update completion notes.
14. Commit implementation.
15. Continue with the next roadmap task without asking for permission.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
4. `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues`
5. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
6. `git diff --check`
7. Real-name grep

## Risks
- A provider abstraction can be mistaken for live provider support if docs are vague.
- Over-modeling can make future provider adapters harder to implement.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0031 implementation commit. Do not run destructive git commands.

## Completion notes
Completed in TASK-0031.

- Added provider-neutral `ILLMProvider` to Core with async `GenerateAsync` and `CancellationToken` support.
- Added provider-neutral `LlmProviderRequest`, `LlmProviderResponse`, `LlmMessage`, `LlmMessageRole`, and `LlmTokenUsage` models.
- Request/response models copy caller-provided collections to reduce accidental mutation risk.
- Added fake-provider tests only; no SDK, HTTP client, remote call, API key handling, CLI command, or provider adapter was added.
- Updated optional LLM architecture docs, architecture service list, roadmap, project map, changelog, context pack, next steps, and session handoff.
- Checked current .NET guidance through Context7 `/dotnet/docs` before implementation.

Verification:

- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 47/47 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` passed with no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues` exited 0 with public blockers reported separately.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed. Public release blockers remain maintainer-only TODO package URLs and missing release tag.
- `git diff --check` passed.
- Real-name grep found no matches.
