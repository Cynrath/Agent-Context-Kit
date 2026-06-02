# AgentContextKit

Offline-first repository context and safety tooling for AI-assisted development.

AgentContextKit is a .NET CLI (`ackit`) for developers who use Codex, Claude Code, Cursor, GitHub Copilot, Gemini CLI, and similar AI coding agents. It analyzes a repository, detects stack signals, generates safe agent instruction/workflow files, and reports secret/PII/brand leakage risks before public release or AI context export.

## Problem
AI coding agents often work with incomplete, stale, or unsafe project context. That can lead to wrong file edits, leaked production settings, weak task planning, missing tests, inconsistent agent instructions, and accidental exposure when a private project becomes public.

## Solution
AgentContextKit creates a local, repeatable workflow:
- Scan repository structure and stack signals.
- Produce a concise project map.
- Generate AI agent instruction files.
- Create task-first development documents.
- Report risky files and secret-like content.
- Keep generated output safe by skipping existing files by default.

## Who Should Use It
- Developers using AI-assisted coding tools.
- Open source maintainers.
- Freelancers, agencies, and small teams.
- Teams preparing private repositories for public release.
- Developers who want consistent Codex/Claude/Cursor/Copilot context files.

## Why Offline-first
The MVP does not call remote AI APIs and does not upload repository contents. This keeps private code local, reduces data leakage risk, and makes the tool usable in restricted environments.

## Features
- `ackit init`: create `.ackit/config.yml` without overwriting existing config.
- `ackit scan`: detect stack, docs, tests, CI, Docker, agent files, and risky paths.
- `ackit generate`: generate context and workflow files for supported agent targets.
- `ackit task`: create structured task files under `docs/tasks`.
- `ackit redact-check`: report secret/PII/brand/local path risks.
- `ackit doctor`: report OSS and repository health checks.
- `--json`: emit machine-readable JSON for automation-friendly command output.
- English and Turkish output/template foundation.

## Quick Start
```powershell
dotnet restore
dotnet build -c Release
dotnet run --project src/AgentContextKit.Cli -- --help
dotnet run --project src/AgentContextKit.Cli -- scan
dotnet run --project src/AgentContextKit.Cli -- scan --json
dotnet run --project src/AgentContextKit.Cli -- task "Add permission checks" --lang en
```

## CLI Commands
```text
ackit init [--lang en|tr] [--json]
ackit scan [--lang en|tr] [--json]
ackit generate [--target codex|claude|cursor|copilot|all] [--lang en|tr] [--json]
ackit task "<title>" [--lang en|tr] [--json]
ackit redact-check [--profile public-release] [--lang en|tr] [--json]
ackit doctor [--lang en|tr] [--json]
ackit version
ackit help
```

## Generated Files
Depending on the command and selected target, AgentContextKit can generate:
- `AGENTS.md`
- `CLAUDE.md`
- `.cursor/rules/project.mdc`
- `.github/copilot-instructions.md`
- `docs/PROJECT_MAP.md`
- `docs/AI_WORKFLOW.md`
- `docs/SECURITY_NOTES.md`
- `docs/DEVELOPMENT_STANDARD.md`
- `docs/tasks/TASK-0001.md`
- `.codex/HANDOFF.md`
- `.codex/CONTEXT_PACK.md`

## Safety Behavior
- Existing files are skipped by default.
- No automatic secret redaction in the MVP.
- No remote upload.
- No GitHub push or NuGet publish is performed by the tool.
- Risk reports are severity based: Critical, High, Medium, Low, Info.

## Localization
Default language is English. Turkish is supported with `--lang tr`. Unknown language values fall back to English.

## Configuration And JSON
See [docs/CONFIGURATION.md](docs/CONFIGURATION.md) for `.ackit/config.yml` and [docs/JSON_OUTPUT.md](docs/JSON_OUTPUT.md) for machine-readable output.

## Documentation
Start with [docs/DOCUMENTATION_INDEX.md](docs/DOCUMENTATION_INDEX.md).

Key docs:
- [CLI Reference](docs/CLI_REFERENCE.md)
- [Examples](docs/EXAMPLES.md)
- [Configuration](docs/CONFIGURATION.md)
- [JSON Output](docs/JSON_OUTPUT.md)
- [Troubleshooting](docs/TROUBLESHOOTING.md)
- [Architecture](docs/ARCHITECTURE.md)
- [Security Model](docs/SECURITY_MODEL.md)
- [Packaging](docs/PACKAGING.md)
- [Release Validation](docs/RELEASE_VALIDATION.md)
- [Release Blockers](docs/RELEASE_BLOCKERS.md)

## Roadmap
See [docs/ROADMAP.md](docs/ROADMAP.md).

## Packaging
Local package validation is documented in [docs/PACKAGING.md](docs/PACKAGING.md) and [docs/RELEASE_VALIDATION.md](docs/RELEASE_VALIDATION.md). NuGet publishing is a manual maintainer action and is not part of the MVP automation.

Public release blockers are tracked in [docs/RELEASE_BLOCKERS.md](docs/RELEASE_BLOCKERS.md).

## Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md).

## Security
See [SECURITY.md](SECURITY.md). Please do not include secrets, private repository contents, or production configuration in public issues.

## License
MIT. See [LICENSE](LICENSE).
