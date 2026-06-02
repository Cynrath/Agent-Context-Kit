# Contributing

Thanks for considering a contribution. AgentContextKit aims to stay small, safe, and useful for real developer workflows.

## Development Rules
- Read `AGENTS.md`, `docs/PRODUCT_SPEC.md`, and the relevant task file first.
- Create or update a task under `docs/tasks/` before changing code.
- Keep changes small and reviewable.
- Do not add new dependencies unless they are clearly necessary.
- Preserve offline-first behavior.
- Do not introduce remote uploads, telemetry, or LLM calls without a documented decision.
- Add or update tests for behavior changes.

## Local Checks
```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release
```

## Pull Request Expectations
- Clear summary and scope.
- Linked task document.
- Tests or clear reason why tests were not added.
- Security impact noted.
- No secrets, dumps, backups, uploads, generated junk, `bin/`, `obj/`, or `node_modules`.

## Commit Style
Use small conventional-style commits when practical:
- `docs: add product spec and roadmap`
- `feat: add scan command`
- `test: cover secret scanner`
- `chore: update CI`
