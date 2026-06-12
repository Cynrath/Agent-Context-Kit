# GitHub Repository Hygiene

This document tracks GitHub-facing hygiene for AgentContextKit.

Detailed manual settings are tracked in `docs/GITHUB_SETTINGS_CHECKLIST.md`. Recommended issue and PR labels are tracked in `docs/GITHUB_LABELS.md`.

## Repository Metadata
Recommended description:

```text
Offline-first CLI for generating safe AI coding agent context, task-first workflows, repo hygiene reports, and multi-agent instruction files.
```

Recommended topics:

```text
ai-tools, coding-agents, codex, developer-tools, dotnet, cli, repository-scanner, agents-md, open-source, security
```

## Branch Protection
Recommended `master` protection:
- Require pull request review for non-maintainer changes.
- Require `ci` before merge.
- Require `cross-platform-source-smoke` before release-prep merges.
- Do not require `cross-platform-smoke` for every PR if NuGet availability noise becomes disruptive; keep it required before release announcements.
- Block force pushes and branch deletion.

Do not configure branch protection from an agent session. Treat branch protection as a maintainer-only GitHub settings action.

## Badges
README badge set:
- `ci`
- `cross-platform-smoke`
- `cross-platform-source-smoke`
- NuGet version
- NuGet downloads
- License
- .NET 10

Keep badges compact so the public pitch remains visible near the top of the README.

## Visual Assets
README may link sanitized diagrams and screenshots, but generated `.ackit/` Web UI/report HTML is local-only and must not be committed as a public artifact.

Use:
- `docs/VISUAL_ASSETS.md` for screenshot and image policy.
- `docs/WEB_UI_PREVIEW.md` for Web UI screenshot workflow.
- `docs/SCREENSHOT_CAPTURE_PLAN.md` for the approved disposable-demo capture and review process.
- `docs/assets/diagrams/` for safe diagrams.
- `docs/assets/screenshots/` for future sanitized screenshots; its README is policy guidance, not an approved screenshot.

## Issue And PR Templates
The repository includes:
- Bug report template.
- Feature request template.
- Security hardening template.
- Documentation improvement template.
- Pull request template.

Templates must not ask for real secrets, API keys, private repository contents, or production config.

Issue templates should use the label plan in `docs/GITHUB_LABELS.md`, starting new public issues with `status: needs-triage`.

## Labels
Use `docs/GITHUB_LABELS.md` as the source of truth for:
- `type:*` labels.
- `status:*` labels.
- `priority:*` labels.
- `area:*` labels.
- `good first issue`.
- `good first issue` for small contributor-friendly tasks.

Label creation and edits are maintainer-only GitHub metadata actions. Optional `gh label create` examples are documentation only.

## Actions Monitoring
Current workflows:
- `ci`: source restore, build, tests, and scan on Ubuntu and Windows.
- `cross-platform-smoke`: published NuGet global tool smoke on Windows, Ubuntu, and macOS.
- `cross-platform-source-smoke`: current-branch package smoke on Windows, Ubuntu, and macOS.

Maintainers should check Actions after every push and before any release tag, GitHub Release, or NuGet publish.

Documentation-only examples live under `docs/examples/`:
- `github-actions-scan-ci.yml`
- `github-actions-sarif-upload.yml`
- `github-actions-published-tool-smoke.yml`
- `github-actions-source-package-smoke.yml`

Do not treat docs examples as active workflows until they are copied into `.github/workflows/` after review. `docs/GITHUB_ACTIONS_USAGE.md` explains published-tool versus source-package smoke and SARIF upload criteria.

Latest read-only GitHub CLI observation for TASK-0060:
- `ci`: success on `master`.
- `cross-platform-smoke`: success on `master`.
- `cross-platform-source-smoke`: success on `master`.
- Latest checked commit: `aaaad5f`.

Latest read-only GitHub CLI observation for TASK-0063:
- `ci`: success on `master`.
- `cross-platform-smoke`: success on `master`.
- `cross-platform-source-smoke`: success on `master`.
- Latest checked commit before local TASK-0063 edits: `a856aac`.

## Release Checklist
Use these docs together:
- `docs/RELEASE_CHECKLIST.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/PACKAGING.md`
- `docs/SOURCE_HYGIENE.md`

## NuGet Ownership
- Package owners must be maintainer-controlled accounts.
- API keys must stay outside the repository.
- NuGet publish is a maintainer-only action.
- Published `.nupkg` and `.snupkg` files are not source artifacts and must not be committed.

## Local Artifact Hygiene
- `.ackit/` output is ignored and local-only.
- Web UI and report files may include local paths.
- Local ZIP/RAR working archives should follow `docs/SOURCE_ARCHIVE.md`.
- GitHub source packages should contain tracked source files only.
