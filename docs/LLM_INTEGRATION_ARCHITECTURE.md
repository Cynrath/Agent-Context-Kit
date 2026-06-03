# Optional LLM Integration Architecture

AgentContextKit remains offline-first and local-only today. v0.5 may add optional LLM-assisted workflows later, but this architecture requires explicit consent, dry-run review, and provider isolation before any repository context can leave the local machine.

## Current State
- No LLM API calls are implemented.
- No OpenAI or other LLM SDK dependency is included.
- No API key is read, stored, generated, or validated.
- No repository content is uploaded.
- Existing `scan`, `generate`, `report`, and `webui` commands remain local-only.

## Reference Inputs
- OpenAI's current API guidance recommends the Responses API for new API-backed projects and supports tools/function calling for agentic workflows.
- OpenAI API keys are secrets and should not be exposed in client-side code; future server-side or CLI usage must load them from an environment variable, OS secret store, or another explicit key-management mechanism.
- .NET guidance supports dependency injection and interface-based dependency boundaries, so provider implementations should be isolated behind Core abstractions instead of hard-coded in CLI command handlers.

## Design Goals
- Keep the default product fully offline.
- Make remote behavior opt-in per command or explicit configuration.
- Show exactly what context would be exported before export.
- Minimize data sent to providers.
- Keep provider code replaceable and testable.
- Keep CLI parsing separate from provider business logic.
- Avoid SDK lock-in until the provider abstraction is stable.

## Non-Goals
- No live LLM call in this task.
- No API key setup flow in this task.
- No hosted service.
- No background upload.
- No telemetry.
- No automatic redaction.
- No provider-specific prompt tuning in this task.

## Proposed Boundaries

### Core
Future Core abstractions should own provider-neutral behavior:
- `ILLMProvider`: provider-neutral request/response boundary.
- `ILLMProviderFactory`: resolves a configured provider only after consent and configuration checks.
- `IPromptPackBuilder`: builds dry-run prompt/context packs from scan results and generated docs.
- `IContextExportReview`: records what would be exported and whether the user approved it.
- `ILLMAuditWriter`: writes local audit records for approved exports and provider calls.

### CLI
The CLI should only parse options, print dry-run review output, request explicit confirmation, and map Core results to exit codes. It must not build prompts, filter secrets, or directly call provider SDKs.

### Providers
Provider implementations should be optional adapters. A provider adapter may translate Core requests into a concrete API shape, but it must not own scanning, redaction, task generation, or policy decisions.

## Consent Gates
Future LLM-backed commands must pass all gates before any remote call:
1. `ackit scan --ci` or equivalent local risk review reports no critical/high export blockers.
2. A dry-run context pack is generated locally.
3. The user sees the exact files, snippets, token estimate when available, and risk summary.
4. The user explicitly approves export for that run.
5. Provider configuration is loaded from approved secret storage.
6. A local audit record is written before and after the provider call.

If any gate fails, the command exits without remote network access.

## Data Minimization
Future context export should prefer:
- Repository metadata over full file contents.
- Summaries over raw source files.
- Explicit task scope over whole-repository context.
- Redacted snippets over raw secrets, tokens, local paths, or personal data.
- Small allowlists over broad default inclusion.

Generated prompt packs should include a manifest with included paths, excluded paths, risk summary, redaction status, and user approval status.

## Secrets And Configuration
- API keys must not be stored in repository files.
- `.env`, dumps, generated prompt packs, and audit logs containing sensitive content must stay ignored by default unless a maintainer explicitly changes the policy.
- Provider configuration should support environment variables and OS/user secret stores before any file-based option.
- The CLI must never print full API keys.
- Missing credentials should produce actionable local errors without stack traces.

## Audit And Security
Future implementation should write local audit entries for:
- command name and timestamp
- provider name
- approved export manifest path
- risk summary at approval time
- files/snippets included
- files/snippets excluded
- user approval mode
- provider request ID when available
- result status and error category

Audit logs must avoid storing raw secrets and should be easy to delete manually.

## Failure Behavior
- Public users must not see internal stack traces.
- Provider/network failures should be categorized as configuration, authentication, rate limit, safety/policy, timeout, or unknown.
- A failed provider call must not mutate repository files unless the user explicitly approved a local output path.
- Partial outputs should be marked incomplete.

## Rollback Strategy
Because the current task is documentation-only, rollback is the TASK-0030 implementation commit. Future runtime implementation should keep LLM code in optional adapters so it can be disabled by configuration without affecting offline commands.

## Future Task Split
Recommended v0.5 implementation order:
1. Add provider-neutral `ILLMProvider` request/response models with fake/in-memory tests.
2. Add dry-run prompt pack generation with no remote calls.
3. Add user-approved context export manifests.
4. Add provider adapters only after explicit maintainer approval and documentation updates.
