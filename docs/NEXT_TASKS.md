# Next Tasks

This file is the unified local execution queue for AgentContextKit after the `v0.2.0-alpha.1` publication. It prevents one-off task prompts by keeping the next safe local tasks in order.

Remote-write work such as GitHub Release edits, label creation, branch protection, repository settings, Code Scanning upload activation, tags, pushes, and NuGet publishing remains maintainer-only.

## Immediate Polish
1. TASK-0066 GitHub Release body polish documentation - completed locally.
2. TASK-0067 GitHub labels and manual repo settings checklist - completed locally.
3. TASK-0068 CodeQL / Code Scanning decision document - completed locally.
4. TASK-0069 GitHub issue tracker bootstrap plan - completed locally.

## Product Hardening
5. TASK-0070 scanner config examples and sample configs - completed locally.
6. TASK-0071 SARIF GitHub Code Scanning opt-in workflow design - completed locally.
7. TASK-0072 JSON schema stability and contract tests - completed locally.
8. TASK-0073 CLI exit code contract hardening - completed locally.
9. TASK-0074 scanner fixture coverage expansion - completed locally.
10. TASK-0075 safe suppression audit log - completed locally.

## Documentation / Presentation
11. TASK-0076 README command examples and copy-paste polish - completed locally.
12. TASK-0077 sanitized screenshot capture plan - completed locally; manual capture remains open.
13. TASK-0078 docs site / GitHub Pages planning - queued.
14. TASK-0079 tutorial: first 5 minutes with ackit - queued.
15. TASK-0080 tutorial: prepare a repo for AI coding agents - queued.

## Next Package Planning
16. TASK-0081 v0.2.0-alpha.2 scope planning - queued.
17. TASK-0082 v0.3 roadmap decision - queued.
18. TASK-0083 1.0 readiness gap analysis - queued.

## Current Remote State
- Current published release: `v0.2.0-alpha.1`.
- GitHub Release: published pre-release.
- NuGet package: `AgentContextKit` `0.2.0-alpha.1`.
- Published global tool includes `ackit sarif`.
- Latest checked hosted workflows on `master`: `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` succeeded.

## Execution Rule
Work through local-only queued tasks in order. Do not ask for per-task continuation. Stop only for genuine blockers, failed validation that requires a product decision, or maintainer-only remote writes.
