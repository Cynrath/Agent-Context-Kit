# AgentContextKit

[![ci](https://github.com/Cynrath/agent-context-kit/actions/workflows/ci.yml/badge.svg)](https://github.com/Cynrath/agent-context-kit/actions/workflows/ci.yml)
[![cross-platform-smoke](https://github.com/Cynrath/agent-context-kit/actions/workflows/cross-platform-smoke.yml/badge.svg)](https://github.com/Cynrath/agent-context-kit/actions/workflows/cross-platform-smoke.yml)
[![cross-platform-source-smoke](https://github.com/Cynrath/agent-context-kit/actions/workflows/cross-platform-source-smoke.yml/badge.svg)](https://github.com/Cynrath/agent-context-kit/actions/workflows/cross-platform-source-smoke.yml)
[![NuGet](https://img.shields.io/nuget/v/AgentContextKit?label=NuGet)](https://www.nuget.org/packages/AgentContextKit)
[![NuGet downloads](https://img.shields.io/nuget/dt/AgentContextKit?label=downloads)](https://www.nuget.org/packages/AgentContextKit)
[![License](https://img.shields.io/github/license/Cynrath/agent-context-kit)](LICENSE)
[![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)

Offline-first repository context and safety tooling for AI-assisted development.

AgentContextKit is a .NET CLI (`ackit`) for developers who use Codex, Claude Code, Cursor, GitHub Copilot, Gemini CLI, and similar AI coding agents. It analyzes a repository, detects stack signals, generates safe agent instruction/workflow files, and reports secret/PII/brand leakage risks before public release or AI context export.

Public repository URL: `https://github.com/Cynrath/agent-context-kit`

Current release: `v0.1.0-alpha.2` is published on GitHub and NuGet, and the global tool install has been verified.

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
- `ackit scan --ci`: fail automated checks on high or critical risk findings.
- `ackit report`: create an offline static HTML scan report.
- `ackit webui`: create an offline static Web UI prototype for scan review.
- `ackit prompt-pack`: create a local dry-run prompt pack for future LLM context review without remote calls.
- `ackit context-export`: create a local approval manifest for a reviewed prompt pack without uploading content.
- `ackit generate`: generate context and workflow files for supported agent targets.
- `ackit task`: create structured task files under `docs/tasks`.
- `ackit redact-check`: report secret/PII/brand/local path risks.
- `ackit doctor`: report OSS and repository health checks.
- `--json`: emit machine-readable JSON for automation-friendly command output.
- English and Turkish output/template foundation.

## Quick Start
From source:

```powershell
dotnet restore
dotnet build -c Release
dotnet run --project src/AgentContextKit.Cli -- --help
dotnet run --project src/AgentContextKit.Cli -- scan
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- scan --json
dotnet run --project src/AgentContextKit.Cli -- report --json
dotnet run --project src/AgentContextKit.Cli -- webui --json
dotnet run --project src/AgentContextKit.Cli -- prompt-pack --json
dotnet run --project src/AgentContextKit.Cli -- context-export --prompt-pack .ackit/prompt-packs/prompt-pack.md --approve --json
dotnet run --project src/AgentContextKit.Cli -- task "Add permission checks" --lang en
```

Install from NuGet:

```powershell
dotnet tool install --global AgentContextKit --version 0.1.0-alpha.2
ackit --help
ackit version
ackit scan --ci
```

Quick installed-tool verification:

```powershell
$smoke = Join-Path $env:TEMP "ackit-smoke-test"
New-Item -ItemType Directory -Force -Path $smoke | Out-Null
Push-Location $smoke
dotnet new console -n DemoApp
Push-Location DemoApp
git init
ackit init --lang tr
ackit scan --ci
ackit generate --target all --lang tr
ackit task "Demo smoke test task" --lang en
ackit report --output .ackit/reports/smoke.html
ackit webui --output .ackit/webui/index.html
ackit prompt-pack --output .ackit/prompt-packs/smoke.md --json
ackit context-export --prompt-pack .ackit/prompt-packs/smoke.md --approve --output .ackit/context-exports/smoke.json --json
Pop-Location
Pop-Location
```

`ackit doctor` can report missing README, LICENSE, SECURITY, tests, CI, `.gitignore`, or package metadata in a minimal demo app. That is expected repository-health output, not a tool failure.

Cross-platform published-package smoke coverage is tracked by `.github/workflows/cross-platform-smoke.yml`. It installs `AgentContextKit` `0.1.0-alpha.2` as a global tool on Windows, Ubuntu, and macOS, then runs the installed-tool smoke flow against a clean demo app.
Current-source smoke coverage is tracked by `.github/workflows/cross-platform-source-smoke.yml`. It packs the current branch locally and installs the package from the workflow's temporary package source without publishing it.
Tested on Windows, Ubuntu, and macOS via GitHub Actions.

## CLI Commands
```text
ackit init [--lang en|tr] [--json]
ackit scan [--lang en|tr] [--json] [--ci]
ackit report [--output <repo-relative.html>] [--lang en|tr] [--json]
ackit webui [--output <repo-relative.html>] [--lang en|tr] [--json]
ackit prompt-pack [--output <repo-relative.md>] [--lang en|tr] [--json]
ackit context-export --prompt-pack <repo-relative.md> --approve [--output <repo-relative.json>] [--lang en|tr] [--json]
ackit generate [--target codex|claude|cursor|copilot|all] [--lang en|tr] [--json]
ackit task "<title>" [--lang en|tr] [--json]
ackit redact-check [--profile public-release] [--lang en|tr] [--json]
ackit doctor [--lang en|tr] [--json]
ackit version
ackit --help
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
- `.ackit/reports/scan-report.html`
- `.ackit/webui/index.html`
- `.ackit/prompt-packs/prompt-pack.md`
- `.ackit/context-exports/context-export-manifest.json`

## Safety Behavior
- Existing files are skipped by default.
- No automatic secret redaction in the MVP.
- No remote upload.
- Static reports and Web UI files are local-only and self-contained; they can include local repository paths and should not be shared as public release artifacts.
- Prompt packs are local dry-run artifacts and do not call remote LLM providers.
- Context export manifests record local approval only and do not upload content.
- No GitHub push or NuGet publish is performed by the tool.
- Risk reports are severity based: Critical, High, Medium, Low, Info.
- The scanner uses a narrow safe technical allowlist for common platform/package domains and clearly non-real fixture placeholders while keeping Critical secret patterns reportable.

## Localization
Default language is English. Turkish is supported with `--lang tr`. Unknown language values fall back to English.

## Configuration And JSON
See [docs/CONFIGURATION.md](docs/CONFIGURATION.md) for `.ackit/config.yml` and [docs/JSON_OUTPUT.md](docs/JSON_OUTPUT.md) for machine-readable output.

## Documentation
Start with [docs/DOCUMENTATION_INDEX.md](docs/DOCUMENTATION_INDEX.md).

Key docs:
- [CLI Reference](docs/CLI_REFERENCE.md)
- [Examples](docs/EXAMPLES.md)
- [Example Workflows](docs/EXAMPLE_WORKFLOWS.md)
- [Configuration](docs/CONFIGURATION.md)
- [JSON Output](docs/JSON_OUTPUT.md)
- [Exit Codes](docs/EXIT_CODES.md)
- [HTML Reports](docs/HTML_REPORTS.md)
- [Web UI Prototype](docs/WEB_UI_PROTOTYPE.md)
- [Troubleshooting](docs/TROUBLESHOOTING.md)
- [Architecture](docs/ARCHITECTURE.md)
- [Source Hygiene](docs/SOURCE_HYGIENE.md)
- [Security Model](docs/SECURITY_MODEL.md)
- [Packaging](docs/PACKAGING.md)
- [Contributor Onboarding](docs/CONTRIBUTOR_ONBOARDING.md)
- [Support Matrix](docs/SUPPORT_MATRIX.md)
- [GitHub Labels](docs/GITHUB_LABELS.md)
- [GitHub Settings Checklist](docs/GITHUB_SETTINGS_CHECKLIST.md)
- [Maintainer Guide](docs/MAINTAINER_GUIDE.md)
- [GitHub Repo Hygiene](docs/GITHUB_REPO_HYGIENE.md)
- [Issue Triage](docs/ISSUE_TRIAGE.md)
- [Maintainer Release Handoff](docs/MAINTAINER_RELEASE_HANDOFF.md)
- [Public Release Audit](docs/PUBLIC_RELEASE_AUDIT.md)
- [Release Validation](docs/RELEASE_VALIDATION.md)
- [Release Blockers](docs/RELEASE_BLOCKERS.md)

## Roadmap
See [docs/ROADMAP.md](docs/ROADMAP.md).

## Packaging
Local package validation is documented in [docs/PACKAGING.md](docs/PACKAGING.md) and [docs/RELEASE_VALIDATION.md](docs/RELEASE_VALIDATION.md). The `0.1.0-alpha.2` package is published as a NuGet global tool.

Public release blockers are tracked in [docs/RELEASE_BLOCKERS.md](docs/RELEASE_BLOCKERS.md).

## Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md) and [docs/CONTRIBUTOR_ONBOARDING.md](docs/CONTRIBUTOR_ONBOARDING.md). Please use the GitHub issue and pull request templates, and do not include secrets or private repository data in public reports.

## Security
See [SECURITY.md](SECURITY.md). Please do not include secrets, private repository contents, or production configuration in public issues.

## License
MIT. See [LICENSE](LICENSE).
