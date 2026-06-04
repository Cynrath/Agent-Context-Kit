# Changelog

All notable changes to AgentContextKit will be documented in this file.

This project follows Semantic Versioning where practical before `1.0.0`.

## [Unreleased]
- No unreleased changes yet.

## [0.1.0-alpha.1] - 2026-06-04
### Added
- Initial offline-first .NET CLI tool package with command name `ackit`.
- CLI commands: `init`, `scan`, `scan --ci`, `report`, `webui`, `prompt-pack`, `context-export`, `generate`, `task`, `redact-check`, `doctor`, `version`, and `help`.
- Repository scanner for docs, tests, CI, Docker, generated agent files, package metadata, and stack signals.
- Sample-aware main stack detection for `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions` without treating `samples/` stacks as the main product stack.
- Pattern-based secret, PII, brand, risky path, and risky extension scanning.
- JSON output with schema/tool metadata, generated timestamps, repository metadata, summaries, and CI mode fields.
- Task-first development document generation under `docs/tasks`.
- Agent instruction generation for Codex, Claude, Cursor, and GitHub Copilot.
- Offline static HTML report generation with safe repository-relative output handling.
- Offline static Web UI prototype generation for local scan review.
- Local-only dry-run prompt pack generation and explicit-approval context export manifests.
- English and Turkish output/template foundation.
- Config schema documentation and generated-file conventions.
- Focused xUnit test coverage and GitHub Actions CI.
- Local release verification, package metadata, public release audit, release blocker, public gate, and v1.0 readiness scripts.
- v1.0 final local readiness review documentation and gate script.
- Source archive hygiene docs and WinRAR exclude guidance for local ZIP/RAR sharing.
- OSS readiness, governance, privacy, support, security, package, release, and maintainer handoff documentation.

### Changed
- Public package and docs metadata use the `Cynrath` persona.
- Package URLs point to `https://github.com/Cynrath/agent-context-kit`.
- Public release blockers now track tag, push approval, and NuGet publish approval rather than package URL placeholders.

### Fixed
- Added NuGet package README metadata for local pack readiness.
- Refined self-scan stack accuracy so sample ASP.NET Core, Minimal API, TypeScript, and Tailwind CSS signals are not reported as the main repository stack.
