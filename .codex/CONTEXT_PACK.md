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
- Codex for OSS application pack: yes, `docs/CODEX_FOR_OSS_APPLICATION.md`

## Risk Summary
- No risk findings in the latest local scan.
- Package URLs point to `https://github.com/Cynrath/agent-context-kit`.
- GitHub repository is public.
- Current release tag `v0.1.0-alpha.2` is pushed.
- GitHub Actions latest `master` run is green.
- Read-only GitHub CLI validation on 2026-06-05 confirmed `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` succeeded for commit `8dac9237c27ba912d056344155f1c9f901557bf5`.
- Repository description and topics are set.
- GitHub Release page for `v0.1.0-alpha.2` is completed.
- NuGet package `AgentContextKit` version `0.1.0-alpha.2` is published.
- NuGet global tool install verification is completed for `0.1.0-alpha.2`.
- NuGet global tool smoke test is completed, including `ackit --help` and Web UI generation.
- Cross-platform CI smoke workflow succeeded on commit `868dff3` for Windows, Ubuntu, and macOS.
- Codex for OSS form submission is completed per maintainer-provided status.
- Alpha.2 hardening and publication verification are complete through TASK-0056.
- TASK-0057 added GitHub issue templates, a pull request template, maintainer guide, contributor onboarding, support matrix, repository hygiene, and issue triage docs.
- TASK-0057 pre-commit validation passed: restore, Release build, 67/67 tests, `scan --ci`, `doctor`, `scan --json`, installed `ackit` version/help, hygiene scans, `git diff --check`, and v1.0 documentation release gate.
- Latest self-scan main stacks: `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.

## Hard Rules
- No remote upload.
- No LLM API in MVP.
- No hosted Web UI in MVP.
- No overwrite by default.
- No automatic redaction.
- No GitHub push or further NuGet publish from agent sessions.

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

## Public Release State
- `RepositoryUrl` is `https://github.com/Cynrath/agent-context-kit`.
- `PackageProjectUrl` is `https://github.com/Cynrath/agent-context-kit`.
- Current local `origin` is `https://github.com/Cynrath/agent-context-kit.git`.
- GitHub repository public: yes.
- `master` pushed: yes.
- `v0.1.0-alpha.2` pushed: yes.
- GitHub Actions latest `master` run: success.
- Latest read-only Actions check: `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` succeeded on commit `8dac9237c27ba912d056344155f1c9f901557bf5`.
- Repository description: set.
- Repository topics: set.
- GitHub Release page: completed.
- NuGet publish: completed.
- NuGet global tool install verification: completed.
- NuGet smoke test: completed.
- Cross-platform smoke workflow: completed successfully on Windows, Ubuntu, and macOS.
- Codex for OSS form submission is completed per maintainer-provided status.
- Current release publication: TASK-0056 records `v0.1.0-alpha.2` tag push, GitHub Release, NuGet publish, global tool install verification, and Web UI smoke evidence.
- `docs/MAINTAINER_RELEASE_HANDOFF.md` contains the current published release state and future release guidance.
- GitHub contributor workflow docs were added in TASK-0057: `docs/MAINTAINER_GUIDE.md`, `docs/SUPPORT_MATRIX.md`, `docs/CONTRIBUTOR_ONBOARDING.md`, `docs/GITHUB_REPO_HYGIENE.md`, and `docs/ISSUE_TRIAGE.md`.

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
- TASK-0046 synced post-push repository status after `master` and `v0.1.0-alpha.1` were pushed.
- TASK-0047 syncs NuGet publish verification and Codex for OSS submission readiness after `AgentContextKit` version `0.1.0-alpha.1` was published and globally installed.
- TASK-0048 records NuGet global tool smoke test verification in a clean demo app and keeps Codex for OSS submission as the remaining follow-up.
- TASK-0049 prepares a Windows/Ubuntu/macOS GitHub Actions smoke workflow for the published NuGet global tool and documents alpha.2 preparation without tagging or publishing.
- TASK-0050 records the successful cross-platform smoke workflow result and adds non-blocking CI/scanner-noise backlog items for TASK-0051 and TASK-0052.
- TASK-0051 through TASK-0054 are implemented locally for alpha.2 hardening: scanner allowlist/fixture-noise reduction, Node 24 workflow readiness, Turkish output polish, and alpha.2 release preparation.
- TASK-0051 implemented conservative scanner noise reduction with safe technical domains, fixture-only placeholder email handling, and preserved Critical secret detection.
- TASK-0052 updated local workflow files for Node 24-ready official actions and explicit `windows-2025`; hosted workflow validation remains manual after push.
- TASK-0053 updated Turkish human CLI output to natural UTF-8 text while preserving JSON schema behavior.
- TASK-0054 completed alpha.2 release preparation docs without changing version, tagging, publishing, creating GitHub releases, or pushing.
- TASK-0055 implements alpha.2 release decision work: source metadata/version bump, source/current-branch smoke workflow, README command cleanup, local alpha.2 package smoke, and release doc sync.
- TASK-0055 validation passed locally: restore/build/test, scan, doctor, JSON scan, local alpha.2 package smoke, package metadata gate, documentation gate, maintainer identity scan, tracked artifact scan, exact token/local-path scan, and `git diff --check`.
- TASK-0056 completed alpha.2 publish verification and refreshed agent/public release docs for the published `v0.1.0-alpha.2` state.
- TASK-0057 is completed for GitHub repo hygiene, issue templates, PR template, maintainer guide, support matrix, contributor onboarding, and issue triage docs. The post-commit public release gate rerun passed with only the expected post-release `HEAD` warning.
