# Roadmap

## v0.1.0-alpha
- Solution foundation.
- CLI skeleton.
- `init`, `scan`, `generate`, `task`, `redact-check`, `doctor`.
- English/Turkish localization foundation.
- Markdown template generation.
- Basic JSON output.
- Config schema docs.
- JSON output docs.
- Basic tests.
- README, OSS docs, AGENTS.
- GitHub Actions.
- Public repository metadata finalization for `https://github.com/Cynrath/agent-context-kit`.
- Source archive hygiene and local release gate preparation.
- Codex for OSS application pack.

## v0.2.0-alpha
- Stronger stack detector with .NET SDK, ASP.NET Core, Razor, Blazor WebAssembly, Worker Service, Minimal API, package manager, TypeScript, and Tailwind CSS signals.
- Better risk scanner with environment sample precision, key-file detection, broader private key block detection, IP filtering, and configured keyword token boundaries.
- `.ackit/config.yml` brand/PII keyword support improvements through token-boundary matching.
- JSON schema stabilization and expanded fields with schema version 2 metadata and summary fields.
- More generated docs with expanded agent/context templates that include health, risk, and recommended checks.
- NuGet package metadata hardening with a local metadata review gate.
- Sample repositories for safe .NET Minimal API and Node/TypeScript/Tailwind stack detection.
- Further Turkish generated-template and documentation localization polish beyond the current CLI output pass.
- Final local readiness consolidation.

## v0.1.0-alpha.2 Preparation
- Run the published NuGet global tool smoke workflow on Windows, Ubuntu, and macOS before considering an alpha.2 package.
- Use cross-platform CI smoke results to identify path separator, shell, report output, Web UI output, prompt-pack, and context-export portability issues.
- Reduce scanner fixture/domain-like noise while preserving Critical secret detection.
- Prepare GitHub Actions for Node 24-compatible official action majors and explicit Windows runner labels.
- Polish Turkish human CLI output with UTF-8 text while preserving JSON schema behavior.
- Bump source/package metadata to `0.1.0-alpha.2` only for reviewed source preparation.
- Add cross-platform source smoke coverage that packs the current branch and installs the local package before publication.
- Keep alpha.2 preparation local and CI-driven until a maintainer explicitly approves a push, new tag, GitHub Release, and NuGet publish.

## CI And Scanner Backlog
- TASK-0051 scanner allowlist and fixture-noise reduction:
  - Treat the internal placeholder email fixture (`private` + `[at]` + `example.internal`) as non-secret test data.
  - Reduce domain-like noise for framework/package strings such as `Microsoft[dot]NET`.
  - Reduce domain-like noise for package registry references such as `api[dot]nuget[dot]org`.
- TASK-0052 GitHub Actions Node 24 readiness:
  - Update official actions to Node 24-compatible majors where safe.
  - Use an explicit Windows runner label to avoid `windows-latest` redirect noise.
  - Hosted validation remains manual after a maintainer push.
- TASK-0053 Turkish localization polish:
  - Replace visible Turkish ASCII fallback CLI wording with natural UTF-8 Turkish text.
  - Keep JSON output schema stable and language-independent.
- TASK-0054 alpha.2 release preparation:
  - Document alpha.2 readiness and remaining manual release actions.
- TASK-0055 alpha.2 release decision:
  - Bump source/package metadata to `0.1.0-alpha.2`.
  - Add current-branch source smoke workflow coverage.
  - Keep published-package smoke pinned to `0.1.0-alpha.1` until alpha.2 is published.

## v0.3.0-beta
- HTML report generation. Started with offline static `ackit report`.
- CI mode. Started with `ackit scan --ci` and GitHub Actions integration.
- Exit code standardization. Started with centralized CLI constants and an exit code matrix.
- Public release hardening. Started with public release gate orchestration.
- More tests.
- Example workflows. Started with local development, CI, HTML report, public release preflight, and sample scanning flows.
- Final local readiness consolidation.

## v0.4.0-beta
- Local Web UI prototype. Started with offline static `ackit webui`.
- Scan result dashboard. Refined with readiness score, review status, severity breakdown, and recommended checks.
- Generated file preview. Refined with expected file categories, present/missing status, size metadata, and capped previews.
- Risk finding browser. Refined with deterministic review queue, finding IDs, match display, and recommended actions.
- Task preview. Refined with task ID, title, inferred status, size metadata, paths, and capped previews.
- Final local readiness consolidation.

## v0.5.0-beta
- Optional LLM integration architecture. Documented with consent gates, provider boundaries, data minimization, and no live provider calls.
- `ILLMProvider` abstraction. Added provider-neutral request/response models and fake-provider tests.
- Dry-run prompt pack generation. Added local-only `ackit prompt-pack` Markdown output with JSON metadata.
- User-approved context export. Added local-only `ackit context-export` approval manifests with JSON metadata.
- Final local readiness consolidation.

## v1.0.0
- Stabilization plan. Added local v1.0 stabilization themes, acceptance gates, and follow-up task sequence.
- Stable CLI contract review. Added local command surface contract and contract check script.
- Config and generated file convention freeze. Added local conventions and convention check script.
- Documentation and release gate freeze. Added local release-critical docs/gates check.
- Final local readiness consolidation. Added local v1.0 readiness review docs and script.
- Public release final cleanup. Added source archive hygiene, package URL blocker clarification, and sample-aware self-scan stack accuracy.
- First public alpha release handoff. Added final repository URL docs, Codex for OSS application pack, release tag readiness, source archive hygiene, GitHub Release completion, NuGet publication, and global tool install verification.
- Stable CLI.
- NuGet global tool release.
- Stable config format.
- Stable generated file conventions.
- Complete documentation.
- Reliable test suite.
- Green CI.

## Post-v1.0
- Optional sample stack reporting that lists `samples/*` stacks separately from the main repository stack.
