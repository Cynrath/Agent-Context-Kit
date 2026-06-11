<div align="center">

# AgentContextKit

**Offline-first repository context and safety tooling for AI-assisted development.**

Analyze a repository, generate clean agent context files, create task-first workflow docs, and catch secret/PII/brand leakage risks before a project is shared with AI agents or released publicly.

<p>
  <a href="https://github.com/Cynrath/agent-context-kit/actions/workflows/ci.yml"><img alt="CI" src="https://github.com/Cynrath/agent-context-kit/actions/workflows/ci.yml/badge.svg"></a>
  <a href="https://github.com/Cynrath/agent-context-kit/actions/workflows/cross-platform-smoke.yml"><img alt="Cross-platform smoke" src="https://github.com/Cynrath/agent-context-kit/actions/workflows/cross-platform-smoke.yml/badge.svg"></a>
  <a href="https://github.com/Cynrath/agent-context-kit/actions/workflows/cross-platform-source-smoke.yml"><img alt="Current-source smoke" src="https://github.com/Cynrath/agent-context-kit/actions/workflows/cross-platform-source-smoke.yml/badge.svg"></a>
</p>

<p>
  <a href="https://www.nuget.org/packages/AgentContextKit"><img alt="NuGet" src="https://img.shields.io/nuget/v/AgentContextKit?label=NuGet&logo=nuget"></a>
  <a href="https://www.nuget.org/packages/AgentContextKit"><img alt="NuGet downloads" src="https://img.shields.io/nuget/dt/AgentContextKit?label=downloads&logo=nuget"></a>
  <a href="LICENSE"><img alt="License" src="https://img.shields.io/github/license/Cynrath/agent-context-kit"></a>
  <a href="https://dotnet.microsoft.com/"><img alt=".NET 10" src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white"></a>
</p>

<p>
  <a href="#-quick-start"><strong>Quick Start</strong></a> ·
  <a href="#-preview"><strong>Preview</strong></a> ·
  <a href="#-what-it-does"><strong>Features</strong></a> ·
  <a href="#-cli-command-map"><strong>CLI</strong></a> ·
  <a href="#-safety-model"><strong>Safety</strong></a> ·
  <a href="#-documentation-map"><strong>Docs</strong></a>
</p>

</div>

---

## Project Status

| Area | Status |
| --- | --- |
| Current release | `v0.1.0-alpha.2` published on GitHub and NuGet |
| Package | `AgentContextKit` global tool install verified |
| Next package candidate | `0.2.0-alpha.1` prepared locally; not pushed, tagged, released, or published yet |
| Runtime | .NET 10 |
| Platforms | Windows, Ubuntu, macOS via GitHub Actions smoke flows |
| Privacy model | Offline-first; no repository upload and no remote AI API calls in the MVP |
| Source note | Current `master` is prepared as `0.2.0-alpha.1` and includes `ackit sarif` |

> [!IMPORTANT]
> The published NuGet package `0.1.0-alpha.2` does **not** include `ackit sarif` yet. Current source is prepared as the `0.2.0-alpha.1` package candidate. Use the source command shown below until `0.2.0-alpha.1` is published.

---

## Preview

The Web UI dashboard shows readiness score, stack signals, health checks, findings, generated context files, and task previews.

![AgentContextKit flow](docs/assets/diagrams/ackit-flow.svg)

Screenshots are intentionally not committed yet until sanitized assets are available. See [Web UI Preview](docs/WEB_UI_PREVIEW.md), [Visual Assets](docs/VISUAL_ASSETS.md), [Sample Gallery](docs/SAMPLE_GALLERY.md), and [Demo Scenarios](docs/DEMO_SCENARIOS.md).

---

## Why AgentContextKit

AI coding agents are powerful, but they often receive incomplete, stale, or unsafe project context. That can lead to wrong edits, weak task planning, inconsistent agent instructions, missed test expectations, and accidental exposure when a private repository becomes public.

AgentContextKit gives teams a repeatable local workflow before they hand a repository to Codex, Claude Code, Cursor, GitHub Copilot, Gemini CLI, or similar coding agents.

| Problem | AgentContextKit helps by |
| --- | --- |
| Agent context is scattered | Generating consistent agent instruction and workflow files |
| Repository structure is unclear | Producing a concise project map and stack signal summary |
| Work starts without task discipline | Creating structured task files under `docs/tasks` |
| Public release has leakage risk | Reporting secret-like, PII-like, brand, and local-path findings |
| CI needs machine-readable checks | Supporting JSON output and `scan --ci` severity gates |
| Review artifacts need to stay local | Creating offline HTML, Web UI, prompt pack, and context export artifacts |

---

## What It Does

| Capability | Command | Output |
| --- | --- | --- |
| Initialize config | `ackit init` | `.ackit/config.yml` |
| Scan repository | `ackit scan` | Stack, docs, tests, CI, Docker, agent files, risky paths |
| Fail CI on risk | `ackit scan --ci` | Non-zero exit on high or critical findings |
| Generate SARIF | `ackit sarif` | Privacy-first SARIF 2.1.0 report from current source and the `0.2.0-alpha.1` candidate |
| Build HTML report | `ackit report` | Offline static scan report |
| Build Web UI prototype | `ackit webui` | Offline static review UI |
| Prepare prompt pack | `ackit prompt-pack` | Local dry-run prompt pack; no remote call |
| Export approved context manifest | `ackit context-export` | Local approval manifest |
| Generate agent files | `ackit generate` | Codex, Claude, Cursor, Copilot context files |
| Create task docs | `ackit task` | Structured task Markdown files |
| Check sensitive content | `ackit redact-check` | Secret/PII/brand/local-path risk report |
| Check repository health | `ackit doctor` | OSS and repo hygiene diagnostics |

---

## Quick Start

### Install from NuGet

```powershell
dotnet tool install --global AgentContextKit --version 0.1.0-alpha.2
ackit --help
ackit version
ackit scan --ci
```

### Run from source

```powershell
dotnet restore
dotnet build -c Release
dotnet run --project src/AgentContextKit.Cli -- --help
dotnet run --project src/AgentContextKit.Cli -- scan
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- scan --json
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/ackit.sarif
dotnet run --project src/AgentContextKit.Cli -- report --json
dotnet run --project src/AgentContextKit.Cli -- webui --json
dotnet run --project src/AgentContextKit.Cli -- prompt-pack --json
dotnet run --project src/AgentContextKit.Cli -- context-export --prompt-pack .ackit/prompt-packs/prompt-pack.md --approve --json
dotnet run --project src/AgentContextKit.Cli -- task "Add permission checks" --lang en
```

### Try it on a sample

```powershell
Push-Location samples/dotnet-console
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
Pop-Location
```

See [Sample Gallery](docs/SAMPLE_GALLERY.md) and [Demo Scenarios](docs/DEMO_SCENARIOS.md) for guided examples.

<details>
<summary><strong>Installed-tool smoke test</strong></summary>

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

</details>

---

## CLI Command Map

```text
ackit init [--lang en|tr] [--json]
ackit scan [--lang en|tr] [--json] [--ci]
ackit sarif --output <repo-relative.sarif> [--lang en|tr] [--json]
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

> [!NOTE]
> `ackit sarif` is part of the current source command surface and the `0.2.0-alpha.1` package candidate; it is not in the published `0.1.0-alpha.2` NuGet global tool.

---

## Generated Files

Depending on the command and selected target, AgentContextKit can generate:

| Area | Files |
| --- | --- |
| Agent instructions | `AGENTS.md`, `CLAUDE.md`, `.cursor/rules/project.mdc`, `.github/copilot-instructions.md` |
| Project workflow | `docs/PROJECT_MAP.md`, `docs/AI_WORKFLOW.md`, `docs/SECURITY_NOTES.md`, `docs/DEVELOPMENT_STANDARD.md` |
| Task tracking | `docs/tasks/TASK-0001.md` |
| Codex handoff | `.codex/HANDOFF.md`, `.codex/CONTEXT_PACK.md` |
| Reports | `.ackit/reports/scan-report.html`, `.ackit/reports/ackit.sarif` |
| Review UI | `.ackit/webui/index.html` |
| Prompt review | `.ackit/prompt-packs/prompt-pack.md` |
| Context approval | `.ackit/context-exports/context-export-manifest.json` |

---

## Safety Model

AgentContextKit is designed to make repository review safer before public release or AI-context export.

| Behavior | Default |
| --- | --- |
| Remote AI/API calls | No remote AI calls in the MVP |
| Repository upload | No upload of repository contents |
| Existing generated files | Skipped by default |
| Secret redaction | No automatic redaction in the MVP |
| Risk severity | Critical, High, Medium, Low, Info |
| SARIF content | Repository-relative locations; raw secret matches are not written |
| Prompt packs | Local dry-run artifacts only |
| Context exports | Local approval manifest only |
| Publishing | No GitHub push or NuGet publish is performed by the tool |

The scanner uses stable rule IDs and a narrow safe technical allowlist for common platform/package domains, known non-Critical paths, and clearly non-real fixture placeholders. Configured scanner allowlists can suppress non-Critical noise, but Critical findings remain reportable.

> [!CAUTION]
> Static reports, Web UI files, prompt packs, and context export manifests are local artifacts. Review them before sharing because they can include repository metadata or local paths.

---

## Localization

Default language is English. Turkish is supported with `--lang tr`. Unknown language values fall back to English.

```powershell
ackit init --lang tr
ackit scan --lang tr
ackit generate --target all --lang tr
ackit task "Yetki kontrollerini ekle" --lang tr
```

---

## Configuration And Automation

| Topic | Documentation |
| --- | --- |
| `.ackit/config.yml` | [Configuration](docs/CONFIGURATION.md) |
| Machine-readable output | [JSON Output](docs/JSON_OUTPUT.md) |
| CI usage | [GitHub Actions Usage](docs/GITHUB_ACTIONS_USAGE.md) |
| Scanner behavior | [Scanner Rules](docs/SCANNER_RULES.md) |
| Exit behavior | [Exit Codes](docs/EXIT_CODES.md) |
| SARIF report | [SARIF Output](docs/SARIF_OUTPUT.md) |

---

## Documentation Map

Start with [Documentation Index](docs/DOCUMENTATION_INDEX.md).

| Category | Links |
| --- | --- |
| Usage | [CLI Reference](docs/CLI_REFERENCE.md), [Examples](docs/EXAMPLES.md), [Example Workflows](docs/EXAMPLE_WORKFLOWS.md) |
| Demo | [Sample Gallery](docs/SAMPLE_GALLERY.md), [Demo Scenarios](docs/DEMO_SCENARIOS.md), [Web UI Preview](docs/WEB_UI_PREVIEW.md) |
| Reports | [HTML Reports](docs/HTML_REPORTS.md), [SARIF Output](docs/SARIF_OUTPUT.md), [Web UI Prototype](docs/WEB_UI_PROTOTYPE.md), [Visual Assets](docs/VISUAL_ASSETS.md) |
| Operations | [Configuration](docs/CONFIGURATION.md), [JSON Output](docs/JSON_OUTPUT.md), [Troubleshooting](docs/TROUBLESHOOTING.md) |
| Engineering | [Architecture](docs/ARCHITECTURE.md), [Source Hygiene](docs/SOURCE_HYGIENE.md), [Security Model](docs/SECURITY_MODEL.md) |
| Packaging | [Packaging](docs/PACKAGING.md), [Release Validation](docs/RELEASE_VALIDATION.md), [Maintainer Release Handoff](docs/MAINTAINER_RELEASE_HANDOFF.md) |
| Maintainers | [Contributor Onboarding](docs/CONTRIBUTOR_ONBOARDING.md), [Support Matrix](docs/SUPPORT_MATRIX.md), [Maintainer Guide](docs/MAINTAINER_GUIDE.md) |
| GitHub setup | [GitHub Labels](docs/GITHUB_LABELS.md), [GitHub Settings Checklist](docs/GITHUB_SETTINGS_CHECKLIST.md), [GitHub Repo Hygiene](docs/GITHUB_REPO_HYGIENE.md), [Issue Triage](docs/ISSUE_TRIAGE.md) |
| Release readiness | [Public Release Audit](docs/PUBLIC_RELEASE_AUDIT.md), [Release Blockers](docs/RELEASE_BLOCKERS.md) |

---

## Roadmap

See [Roadmap](docs/ROADMAP.md).

Public release blockers are tracked in [Release Blockers](docs/RELEASE_BLOCKERS.md).

---

## Packaging

Local package validation is documented in [Packaging](docs/PACKAGING.md) and [Release Validation](docs/RELEASE_VALIDATION.md). The `0.1.0-alpha.2` package is published as a NuGet global tool. Current source is prepared as the `0.2.0-alpha.1` package candidate.

```powershell
dotnet tool install --global AgentContextKit --version 0.1.0-alpha.2
ackit version
```

---

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) and [Contributor Onboarding](docs/CONTRIBUTOR_ONBOARDING.md). Please use the GitHub issue and pull request templates, and do not include secrets or private repository data in public reports.

---

## Security

See [SECURITY.md](SECURITY.md). Please do not include secrets, private repository contents, production configuration, or private customer data in public issues.

---

## License

MIT. See [LICENSE](LICENSE).
