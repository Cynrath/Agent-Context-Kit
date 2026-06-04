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

## GitHub Repository Metadata
Repository description:

```text
Offline-first CLI for generating safe AI coding agent context, task-first workflows, repo hygiene reports, and multi-agent instruction files.
```

GitHub topics:

```text
ai-tools, coding-agents, codex, developer-tools, dotnet, cli, repository-scanner, agents-md, open-source, security
```

## Remaining Step: Codex For OSS Form
Use `docs/CODEX_FOR_OSS_APPLICATION.md`.

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
