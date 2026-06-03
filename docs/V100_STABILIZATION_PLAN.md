# v1.0 Stabilization Plan

This plan turns the v1.0 roadmap into local, reviewable work. It does not approve public release, remote publishing, live LLM provider use, repository upload, or package metadata URL replacement.

## Current Boundary
AgentContextKit is locally validated through v0.5:
- Offline repository scanning and risk reporting.
- Agent/context/task generation.
- JSON output and CI mode.
- Offline HTML report and Web UI prototype.
- Local dry-run prompt packs and context export approval manifests.
- v0.2, v0.3, v0.4, and v0.5 readiness scripts.

Public release is still blocked by maintainer-only decisions:
- Select the real public repository URL.
- Replace TODO `RepositoryUrl` and `PackageProjectUrl`.
- Create a release tag after explicit approval.
- Push and publish only after maintainer approval.

## Stabilization Themes

### Stable CLI Contract
Review all commands, options, exit codes, JSON fields, and help text before v1.0. Breaking changes should be made before the contract is declared stable.

Local acceptance gates:
- `docs/CLI_REFERENCE.md`, `docs/JSON_OUTPUT.md`, and `docs/EXIT_CODES.md` agree with `ackit --help`.
- `scan --ci` exit behavior is documented and tested.
- Existing commands remain offline-first by default.
- Public users do not see stack traces or internal implementation details for expected input errors.

### Stable Config Format
Review `.ackit/config.yml` defaults, documented fields, ignore path behavior, and risk scanner settings.

Local acceptance gates:
- `docs/CONFIGURATION.md` matches `AckitConfig.Default`.
- Generated default config contains all documented generated-artifact ignore paths.
- Config changes remain backward-compatible or are explicitly documented before v1.0.

### Stable Generated File Conventions
Review generated paths, skip-existing behavior, local artifact directories, and task file conventions.

Local acceptance gates:
- Generated files remain repository-relative.
- Existing files are skipped by default.
- `.ackit/reports/`, `.ackit/webui/`, `.ackit/prompt-packs/`, and `.ackit/context-exports/` remain ignored.
- Agent instruction, project map, AI workflow, security notes, and task templates stay consistent.

### Documentation Freeze
Review README, Turkish README, usage docs, architecture docs, security/privacy docs, release docs, and maintainer handoff.

Local acceptance gates:
- Documentation index references every release-critical doc.
- README quick start matches supported commands.
- Safety boundaries are repeated where users could misunderstand local artifacts as public release approval.
- Maintainer-only steps are never presented as agent-run steps.

### Test And CI Reliability
Review xUnit coverage and GitHub Actions behavior for the stable command surface.

Local acceptance gates:
- `dotnet build AgentContextKit.sln -c Release --no-restore` passes.
- `dotnet test AgentContextKit.sln -c Release --no-build` passes.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci` passes on this repository.
- CI remains local validation only and does not publish packages.

### Security And Privacy Gates
Review scanner behavior, source hygiene docs, public release audit scripts, and optional LLM safety boundaries.

Local acceptance gates:
- No critical/high risk findings in local self-scan.
- Real-name grep finds no matches.
- No provider SDK, API key handling, HTTP client, telemetry, upload, or live provider call is introduced without a new documented task and explicit maintainer approval.
- Public release gates report known blockers until maintainer-only actions are complete.

### Packaging Handoff
Review NuGet package metadata, local pack/install validation, and maintainer release handoff.

Local acceptance gates:
- `scripts/verify-release.ps1` passes.
- Package metadata TODO URLs remain explicit blockers until maintainer selects the real public URL.
- Temporary tool install validates `ackit --help` and `ackit scan --json`.
- No push, tag, remote creation, or NuGet publish occurs from agent sessions.

## Proposed v1.0 Task Sequence
1. TASK-0036: stable CLI contract review.
2. TASK-0037: config and generated file convention freeze.
3. TASK-0038: documentation and release gate freeze.
4. TASK-0039: v1.0 final local readiness consolidation.

## Required Local Validation
Run these before considering the stabilization plan reviewed:

```powershell
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

Public release gates should remain report-only until maintainer-only blockers are resolved:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
```

## Rollback
If this plan proves too broad, replace it with smaller v1.0 task-specific planning docs. Do not run destructive git commands.
