# AgentContextKit Context Pack

## Project
AgentContextKit (`ackit`) is an offline-first .NET 10 CLI for AI-assisted repository context generation, task-first workflow docs, and secret/PII/brand leakage risk reporting.

## Architecture
- CLI project: `src/AgentContextKit.Cli`
- Core project: `src/AgentContextKit.Core`
- Tests: `tests/AgentContextKit.Tests`

## Current MVP
- Safe local scanning.
- Sample-aware main repository stack detection.
- Agent instruction generation.
- Task file generation.
- Pattern-based redact checks.
- Offline static HTML report, Web UI, dry-run prompt pack, and local context export manifest generation.
- English/Turkish localization foundation.

## Repository Health
- README: yes
- LICENSE: yes
- SECURITY: yes
- Tests: yes
- CI: yes
- Agent instructions: yes

## Risk Summary
- No risk findings in the latest local scan.
- Public release remains blocked by TODO package URLs and missing release tag.
- Latest self-scan main stacks: `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.

## Hard Rules
- No remote upload.
- No LLM API in MVP.
- No hosted Web UI in MVP.
- No overwrite by default.
- No automatic redaction.
- No GitHub push or NuGet publish from agent sessions.

## Verification
Use:
```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- report --json
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- webui --json
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- prompt-pack --json
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- context-export --prompt-pack .ackit/prompt-packs/prompt-pack.md --approve --json
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

## Public Release Blockers
- `RepositoryUrl` and `PackageProjectUrl` are TODO placeholders until the maintainer selects the real public URL.
- Recommended public URL for maintainer review: `https://github.com/Cynrath/agent-context-kit`.
- Current local `origin` is `https://github.com/Cynrath/agent-context-kit.git`; casing/name alignment is maintainer-only.
- `scripts/check-package-metadata.ps1 -FailOnIssues` must exit `0` before any public release.
- `scripts/audit-public-release.ps1 -FailOnIssues` must exit `0` before any public release.
- `scripts/check-release-blockers.ps1 -FailOnBlockers` must exit `0` before any public release.
- `docs/MAINTAINER_RELEASE_HANDOFF.md` contains the maintainer-only public release sequence.

## Source Hygiene
- Empty SDK scaffold file `src/AgentContextKit.Core/Class1.cs` has been removed.
- Tests live in `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`.
- Source archive hygiene is documented in `docs/SOURCE_ARCHIVE.md`.
- `winrar_exclude.txt` contains the local ZIP/RAR exclude list.
- `.cursor/rules/project.mdc` is an intentional AI instruction file.

## v0.2 Progress
- TASK-0011 completed stack detection expansion with .NET SDK, ASP.NET Core, Razor, Blazor WebAssembly, Worker Service, Minimal API, package manager, TypeScript, and Tailwind CSS signals.
- TASK-0012 completed risk scanner precision improvements for environment samples, private key files, private key blocks, IP filtering, and configured keyword token boundaries.
- TASK-0013 completed JSON schema version 2 with generated timestamp, repository metadata, and summary fields.
- TASK-0014 completed expanded generated agent/context docs with repository health, risk summary, and recommended checks.
- TASK-0015 completed safe sample repositories for .NET Minimal API and Node/TypeScript/Tailwind stack detection.
- TASK-0016 completed NuGet package metadata review script and documentation.
- TASK-0017 completed v0.2 local readiness consolidation script and documentation.
- TASK-0018 completed `ackit scan --ci` with high/critical exit codes, scan JSON CI metadata, tests, docs, and GitHub Actions integration.
- TASK-0019 completed exit code standardization with CLI constants, focused tests, and `docs/EXIT_CODES.md`.
- TASK-0020 completed offline static HTML report generation with `ackit report`, tests, docs, safe output handling, and ignored `.ackit/reports/`.
- TASK-0021 completed example workflow documentation for local development, CI, HTML reports, public release preflight, and sample scans.
- TASK-0022 completed public release gate orchestration script and documentation.
- TASK-0023 completed v0.3 local readiness consolidation script and documentation.
- TASK-0024 completed offline static Web UI prototype generation with `ackit webui`, tests, docs, safe output handling, and ignored `.ackit/webui/`.
- TASK-0025 completed Web UI scan dashboard refinement with readiness score, review status, severity breakdown, recommended checks, tests, and docs.
- TASK-0026 completed generated file preview refinement with expected file category, present/missing status, size metadata, capped previews, continuous progress hard rule, tests, and docs.
- TASK-0027 completed risk finding browser refinement with deterministic review queue, finding IDs, match display, recommended actions, tests, and docs.
- TASK-0028 completed task preview refinement with task ID, title, inferred status, size metadata, paths, capped previews, tests, and docs.
- TASK-0029 completed v0.4 local readiness consolidation script and documentation.
- TASK-0030 completed optional LLM integration architecture with consent gates, provider boundaries, data minimization, and no live provider calls.
- TASK-0031 completed provider-neutral `ILLMProvider` request/response abstractions with fake-provider tests and no live provider calls.
- TASK-0032 completed local-only `ackit prompt-pack` dry-run Markdown generation with JSON metadata and no remote provider calls.
- TASK-0033 completed local-only `ackit context-export` approval manifest generation with JSON metadata and no remote provider calls.
- TASK-0034 completed v0.5 local readiness consolidation script and documentation.
- TASK-0035 completed v1.0 stabilization planning with local acceptance gates and follow-up task sequence.
- TASK-0036 completed stable CLI contract documentation and local contract check script.
- TASK-0037 completed config/generated-file convention documentation and local convention check script.
- TASK-0038 completed documentation/release gate freeze documentation and local gate check script.
- TASK-0039 completed v1.0 final local readiness review documentation and local readiness check script.
- TASK-0040 completed public release final cleanup with source archive hygiene, sample-aware stack detection, package URL blocker clarification, and local gate reports.
