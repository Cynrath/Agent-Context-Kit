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

## v0.2.0-alpha
- Stronger stack detector with .NET SDK, ASP.NET Core, Razor, Blazor WebAssembly, Worker Service, Minimal API, package manager, TypeScript, and Tailwind CSS signals.
- Better risk scanner with environment sample precision, key-file detection, broader private key block detection, IP filtering, and configured keyword token boundaries.
- `.ackit/config.yml` brand/PII keyword support improvements through token-boundary matching.
- JSON schema stabilization and expanded fields with schema version 2 metadata and summary fields.
- More generated docs with expanded agent/context templates that include health, risk, and recommended checks.
- NuGet package metadata hardening with a local metadata review gate.
- Sample repositories for safe .NET Minimal API and Node/TypeScript/Tailwind stack detection.
- Final local readiness consolidation.

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
- Stable CLI.
- NuGet global tool release.
- Stable config format.
- Stable generated file conventions.
- Complete documentation.
- Reliable test suite.
- Green CI.
