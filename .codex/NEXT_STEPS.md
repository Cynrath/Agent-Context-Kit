# Next Steps

1. Add baseline repository files: `global.json`, `.gitignore`, `.editorconfig`.
2. Scaffold the .NET 10 solution and three projects.
3. Add OSS docs: README, license, security, contributing, code of conduct, changelog, AGENTS.
4. Add product docs under `docs/`.
5. Implement Core models and service interfaces.
6. Implement MVP services: repository scanner, stack detector, risk scanners, template renderer, task generator, generators, doctor.
7. Implement CLI commands: `init`, `scan`, `generate`, `task`, `redact-check`, `doctor`, `version`, `help`.
8. Add templates for English and Turkish generated docs.
9. Add unit tests for stack detection, risk scanning, template fallback, task generation, overwrite safety, config fallback, and doctor checks.
10. Add GitHub Actions CI.
11. Run `dotnet restore`, `dotnet build -c Release`, and `dotnet test -c Release`.
12. Update `.codex/SESSION_HANDOFF.md` and final task completion notes.
