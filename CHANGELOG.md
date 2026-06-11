# Changelog

All notable changes to AgentContextKit will be documented in this file.

This project follows Semantic Versioning where practical before `1.0.0`.

## [Unreleased]

## [0.2.0-alpha.1] - 2026-06-11
### Added
- Added `ackit sarif` source command for SARIF 2.1.0 output.
- Added scanner rule catalog with stable `ACKIT` rule IDs.
- Added additive JSON `ruleId` field.
- Added config allowlist foundation: `safeDomains`, `ignoredPaths`, `ignoredFindingIds`.
- Added expanded scanner patterns.
- Added sample gallery and demo scenarios.
- Added Web UI preview and visual asset guidance.
- Added `ackit sarif --output <repo-relative.sarif>` documentation and GitHub Code Scanning readiness notes for the published `0.2.0-alpha.1` package.
- Added documentation-only GitHub Actions examples for scan CI, SARIF upload, published-tool smoke, and source-package smoke.
- Added GitHub Actions usage guidance for CI command order, privacy, failure interpretation, and SARIF upload decisions.
- Added sample repository gallery and demo scenario docs for onboarding.
- Added safe sample repositories for .NET console, generic empty repository health gaps, and security fixture wording.
- Added a local sample smoke helper script.
- Added a central scanner rule catalog with stable `ACKIT` rule IDs, default severity context, and SARIF help metadata.
- Added configurable `safeDomains`, `ignoredPaths`, and `ignoredFindingIds` scanner allowlist fields for narrow non-Critical noise suppression.
- Added scanner coverage for additional package artifacts, provider-token-like values, bearer token-like values, and Unix home path leakage.

### Changed
- Published NuGet `0.2.0-alpha.1` now includes `ackit sarif`.
- JSON finding objects now include additive `ruleId` metadata.
- SARIF rule metadata now uses the centralized scanner rule catalog.
- Scanner documentation and security model are updated for v0.2.0-alpha.

### Security
- Critical findings cannot be silently suppressed by config allowlist.
- SARIF output avoids raw secret matches and absolute local paths.

## [0.1.0-alpha.2] - 2026-06-05
### Added
- Added a cross-platform source smoke workflow that packs the current branch and installs `AgentContextKit` `0.1.0-alpha.2` from a temporary local package source on Windows, Ubuntu, and macOS.
- Added alpha.2 hardening tasks for scanner noise reduction, GitHub Actions Node 24 readiness, Turkish CLI output polish, and release preparation.
- Published `v0.1.0-alpha.2` on GitHub and NuGet and verified global tool installation.

### Changed
- Reduced scanner noise with a conservative safe technical domain allowlist and fixture-only placeholder email handling.
- Added safe technical allowlist coverage for common platform/package domains while preserving Critical secret detection.
- Reduced fixture placeholder noise without suppressing real source/docs email or secret findings.
- Prepared GitHub Actions workflows for Node 24-ready official action majors and explicit Windows runner labeling.
- Polished Turkish human CLI output while preserving JSON schema fields.
- Bumped source/package metadata and CLI runtime version to `0.1.0-alpha.2`.
- Updated the published-package smoke workflow to install `AgentContextKit` `0.1.0-alpha.2`.
- Recorded successful cross-platform GitHub Actions smoke validation for the published NuGet global tool.
- Synced post-push GitHub release status docs after `master` and `v0.1.0-alpha.1` were pushed.
- Verified NuGet publication and global tool install for `AgentContextKit` version `0.1.0-alpha.1`.
- Verified NuGet global tool smoke test in a clean demo app.

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
- Public release blockers track the completed GitHub Release and NuGet publication state, with Codex for OSS submission as the remaining follow-up.

### Fixed
- Added NuGet package README metadata for local pack readiness.
- Refined self-scan stack accuracy so sample ASP.NET Core, Minimal API, TypeScript, and Tailwind CSS signals are not reported as the main repository stack.
