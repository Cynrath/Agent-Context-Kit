# TASK-0030: v0.5 Optional LLM Integration Architecture

## Purpose
Define the optional LLM integration architecture for v0.5 without adding remote calls, API keys, SDK dependencies, telemetry, or hosted services.

## Scope
- Add a local-first optional LLM integration architecture document.
- Document provider boundaries, consent gates, data minimization, dry-run behavior, and audit requirements.
- Record an architecture decision for deferring live provider calls until explicit maintainer approval.
- Update product/architecture docs where needed to clarify that MVP remains offline-first.
- Update roadmap, project map, documentation index, changelog, context pack, next steps, and session handoff.

## Out of scope
- Implementing `ILLMProvider`.
- Adding OpenAI or other LLM SDK packages.
- Calling any remote LLM API.
- Reading or storing API keys.
- Hosted/server Web UI.
- Telemetry or analytics.
- Exporting repository content.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- NuGet publish.
- Automatic redaction or deletion.

## Affected files
- `docs/LLM_INTEGRATION_ARCHITECTURE.md`
- `docs/DECISIONS.md`
- `docs/PRODUCT_SPEC.md`
- `docs/ARCHITECTURE.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/tasks/TASK-0030-v050-optional-llm-integration-architecture.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
No runtime permission change. The architecture must require explicit user consent before any future context export or remote provider call.

## SEO/i18n impact
No SEO impact. Documentation should use clear English terms and avoid implying that remote LLM features exist today.

## Audit/security impact
High relevance. The design must keep secret/PII/brand leakage as release-blocking, require dry-run review before export, avoid silent uploads, and keep audit logs/local review artifacts in scope for future implementation.

## Architecture impact
Defines future provider boundaries and trust model. No runtime architecture change in this task.

## CLI impact
No command syntax change. Future CLI commands/options may be proposed but not implemented.

## Testing impact
Documentation-only task unless implementation scope changes. Existing build/test/release checks are still required.

## OSS/release impact
No public release approval impact. Public release remains blocked by maintainer-only URL, tag, push, and publish decisions.

## Acceptance criteria
- `docs/LLM_INTEGRATION_ARCHITECTURE.md` exists.
- The architecture states that current MVP remains offline-first and local-only.
- The architecture defines consent gates before context export and remote provider calls.
- The architecture documents data minimization, redaction review, provider abstraction boundaries, configuration/secrets handling, audit/security requirements, and rollback strategy.
- `docs/DECISIONS.md` records the decision to defer live LLM calls and SDK dependencies.
- Product and architecture docs mention optional future LLM integration without changing current behavior.
- Documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff are updated.
- Current official OpenAI/.NET guidance is checked before writing integration-facing architecture.
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
3. Read required project docs and current official OpenAI/.NET guidance before implementation.
4. Add the optional LLM integration architecture document.
5. Record the architecture decision.
6. Update product/architecture docs and indexes.
7. Update roadmap, project map, changelog, context pack, next steps, and session handoff.
8. Run build/test and local validation.
9. Run `ackit scan --ci`.
10. Run `scripts/check-v040-readiness.ps1 -FailOnIssues`.
11. Run `scripts/verify-release.ps1`.
12. Run `git diff --check`.
13. Run real-name grep.
14. Update completion notes.
15. Commit implementation.
16. Continue with the next roadmap task without asking for permission.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
4. `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues`
5. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
6. `git diff --check`
7. Real-name grep

## Risks
- Architecture docs can be misread as implemented remote behavior if wording is unclear.
- Future provider integrations can leak sensitive repository context without strict consent and review gates.
- Public-release blockers remain intentionally unresolved.

## Rollback plan
Revert the TASK-0030 implementation commit. Do not run destructive git commands.

## Completion notes
Completed in TASK-0030.

- Added `docs/LLM_INTEGRATION_ARCHITECTURE.md` for future optional LLM provider work.
- Documented current offline-only state, consent gates, data minimization, provider boundaries, secrets handling, audit/security requirements, failure behavior, rollback, and future task split.
- Added ADR-0010 to defer live LLM calls, SDK dependencies, API key handling, and context export until explicit maintainer approval.
- Updated product spec, architecture, documentation index, roadmap, project map, changelog, context pack, next steps, and session handoff docs.
- Checked official OpenAI docs fallback for Responses API and API-key handling guidance, and Context7 `/dotnet/docs` for DI/interface guidance before writing the architecture.

Verification:

- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 46/46 tests.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` passed with no risk findings.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues` exited 0 with public blockers reported separately.
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed. Public release blockers remain maintainer-only TODO package URLs and missing release tag.
- `git diff --check` passed.
- Real-name grep found no matches.
