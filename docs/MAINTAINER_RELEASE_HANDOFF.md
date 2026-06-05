# Maintainer Release Handoff

This handoff records the completed `v0.1.0-alpha.1` GitHub and NuGet release state and the remaining Codex for OSS submission follow-up.

Codex must not push commits, create GitHub releases, publish NuGet packages, or handle API keys unless the maintainer explicitly asks for each public action.

## Current Published State
- GitHub repository public: yes, `https://github.com/Cynrath/agent-context-kit`.
- `master` pushed: yes.
- `v0.1.0-alpha.1` tag pushed: yes.
- `master` and `v0.1.0-alpha.1` point to `aee808244bf33d00808e7e70db6235132c2d3829`.
- GitHub Actions latest `master` run is green for `aee808244bf33d00808e7e70db6235132c2d3829`.
- Repository description is set.
- Repository topics are set.
- GitHub Release page: completed, `https://github.com/Cynrath/agent-context-kit/releases/tag/v0.1.0-alpha.1`.
- NuGet publish: completed.
- NuGet global tool install verification: completed.
- NuGet global tool smoke test: completed.
- Cross-platform CI smoke validation: completed on commit `868dff3` for Windows, Ubuntu, and macOS.
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageId` is `AgentContextKit`.
- `ToolCommandName` is `ackit`.
- `Authors` and `Company` are `Cynrath`.
- Codex for OSS application pack is ready in `docs/CODEX_FOR_OSS_APPLICATION.md`.

## Verified Install
Maintainer verification evidence:

```powershell
dotnet tool install --global AgentContextKit --version 0.1.0-alpha.1
ackit version
ackit --help
```

Expected version output:

```text
AgentContextKit 0.1.0-alpha.1
```

If the tool is already installed, use:

```powershell
dotnet tool update --global AgentContextKit --version 0.1.0-alpha.1
```

## Verified NuGet Smoke Test
Completed smoke test evidence:

- Clean test app: external `ackit-smoke-test/DemoApp` directory outside this repository.
- `ackit init --lang tr` created `.ackit/config.yml`.
- `ackit scan --ci` reported no risk findings.
- `ackit generate --target all --lang tr` created `AGENTS.md`, `CLAUDE.md`, `.cursor/rules/project.mdc`, `.github/copilot-instructions.md`, docs, and `.codex` files.
- `ackit task "Demo smoke test görevi" --lang tr` worked.
- `ackit report --output .ackit/reports/smoke.html` worked.
- `ackit webui --output .ackit/webui/index.html` worked.
- Fake `OPENAI_API_KEY` in `.env.test` was detected by `redact-check` as Critical with exit code `2`.
- After `.env.test` was deleted, `ackit scan --ci` reported no risk findings.
- `ackit scan --json`, `ackit doctor --json`, `ackit prompt-pack`, and `ackit context-export` worked.
- `context-export` did not call a remote LLM provider.

`ackit doctor` reported expected health failures on the minimal demo app because README, LICENSE, SECURITY, tests, CI, `.gitignore`, and package metadata were absent. This is correct repository-health behavior, not a tool issue.

## Cross-Platform CI Smoke Test
`.github/workflows/cross-platform-smoke.yml` verified the published NuGet global tool on commit `868dff3`:

- `windows-latest`
- `ubuntu-latest`
- `macos-latest`

The workflow installed .NET 10, installed `AgentContextKit` version `0.1.0-alpha.1` globally, added the platform-specific `.dotnet/tools` path, created a clean demo app, ran the installed-tool smoke flow, verified fake secret detection returned exit code `2`, deleted the fake secret, and finished with `ackit scan --ci`.

This workflow is alpha.2 preparation only. It does not create tags, publish NuGet packages, or mutate release metadata.

## Next Alpha.2 Work
Alpha.2 preparation has started locally after the completed `v0.1.0-alpha.1` release.

Implemented locally:
- Scanner safe technical allowlist and fixture-noise reduction.
- GitHub Actions Node 24 readiness and explicit Windows runner labels.
- Turkish human CLI output polish.
- Alpha.2 release preparation docs.

Not performed:
- Version bump.
- New tag.
- GitHub Release.
- NuGet publish.
- Push.

## GitHub Actions Node 24 Readiness
The local workflow files have been prepared for Node 24-compatible official actions:

- `ci.yml`: `actions/checkout@v6`, `actions/setup-dotnet@v5`, `windows-2025`, read-only `contents: read`.
- `cross-platform-smoke.yml`: `actions/setup-dotnet@v5`, `windows-2025`, read-only `contents: read`.
- `FORCE_JAVASCRIPT_ACTIONS_TO_NODE24=true` is not required for the current local workflow because the selected official action majors are Node 24-ready.

Manual validation required after the next maintainer push:
- Confirm `ci` succeeds on Ubuntu and Windows.
- Confirm `cross-platform-smoke` succeeds on Windows, Ubuntu, and macOS.
- Confirm no Node.js 20 runtime warning remains.
- Confirm no `windows-latest` redirect notice remains.

## GitHub Repository Metadata
Repository description:

```text
Offline-first CLI for generating safe AI coding agent context, task-first workflows, repo hygiene reports, and multi-agent instruction files.
```

GitHub topics:

```text
ai-tools, coding-agents, codex, developer-tools, dotnet, cli, repository-scanner, agents-md, open-source, security
```

## Codex For OSS Form
The Codex for OSS form has been submitted per maintainer-provided status. Keep `docs/CODEX_FOR_OSS_APPLICATION.md` as the submitted application pack/reference.

Form-ready sections are included for:
- Why this repository is a good fit.
- How API credits would be used.
- Additional notes.

## Future Release Checklist
- Update version and release notes intentionally.
- Run restore/build/test/scan/doctor.
- Run release gates.
- Push release commits and tag only after review.
- Create GitHub Release.
- Publish NuGet with a secure API key outside the repository.
- Verify install from NuGet.
