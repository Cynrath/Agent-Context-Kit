# AgentContextKit Agent Rules

## Default Workflow
- Read `README.md`, `docs/PRODUCT_SPEC.md`, `docs/ARCHITECTURE.md`, and the active task before changing code.
- Do not code before a task file exists under `docs/tasks/`.
- Update `.codex/SESSION_HANDOFF.md` after major steps.
- Run relevant tests before reporting completion.
- Prefer safe, minimal, production-ready changes.

## Safety
- Keep the MVP offline-first and local-only.
- Do not upload repository content.
- Do not add telemetry, LLM calls, or remote services without a documented decision.
- Do not overwrite existing user files by default.
- Do not delete files or run destructive git commands unless explicitly requested.
- Treat secret/PII/brand leakage as a release-blocking concern.

## Code Standards
- Public APIs, classes, and methods use English names.
- Keep CLI parsing/output separate from Core business logic.
- Keep IO, scanning, rendering, reporting, and config responsibilities separate.
- Use nullable reference types.
- Keep dependencies minimal.
- Add focused tests for behavior changes.

## Git Discipline
- Check `git status` before edits.
- Never push, publish, force-push, or create remotes from an agent session.
- Use small logical commits when practical.
- Do not commit `.env`, dumps, uploads, `bin/`, `obj/`, `node_modules`, backups, or generated junk.

## Handoff
If context is low or work pauses, update:
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- The active task file completion notes
