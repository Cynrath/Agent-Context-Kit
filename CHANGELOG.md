# Changelog

All notable changes to AgentContextKit will be documented in this file.

This project follows Semantic Versioning where practical before `1.0.0`.

## [Unreleased]
### Added
- Initial repository foundation.
- .NET 10 solution structure.
- CLI/Core/Tests projects.
- MVP CLI commands: `init`, `scan`, `generate`, `task`, `redact-check`, `doctor`, `help`, and `version`.
- JSON output via `--json` for automation-friendly CLI usage.
- Config and JSON output documentation.
- Config schema fields for `schemaVersion`, `ignorePaths`, and `riskExtensions`.
- JSON output metadata with `schemaVersion` and `toolVersion`.
- Config-driven ignore path and risky extension scanner behavior.
- Packaging and release validation docs.
- `.gitattributes` for repository line-ending consistency.
- Local release verification script and release candidate review document.
- Completed documentation index, CLI reference, examples, troubleshooting, FAQ, support, privacy, maintainers, and governance docs.
- Generated missing agent/context workflow files for Claude, Cursor, Copilot, project map, AI workflow, security notes, task template, and Codex handoff.
- Release blocker document and local blocker guard script.
- Core repository scanner, stack detector, risk scanner, template renderer, task generator, and doctor checks.
- English/Turkish localization and template foundation.
- xUnit test coverage for MVP behaviors.
- GitHub Actions CI.
- OSS readiness documents.
### Changed
- Replaced maintainer real-name metadata with the public pseudonym `Cynrath`.
### Fixed
- Added NuGet package readme metadata for local pack readiness.

## [0.1.0-alpha.1] - Planned
### Added
- CLI skeleton with `init`, `scan`, `generate`, `task`, `redact-check`, `doctor`, `help`, and `version`.
- Offline repository scanner.
- Pattern-based risk scanner.
- English/Turkish localization foundation.
- Markdown context and workflow generation.
- Focused unit tests.
- GitHub Actions CI.
