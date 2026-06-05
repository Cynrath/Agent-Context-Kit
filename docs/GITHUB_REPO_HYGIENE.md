# GitHub Repository Hygiene

This document tracks GitHub-facing hygiene for AgentContextKit.

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

## Issue And PR Templates
The repository includes:
- Bug report template.
- Feature request template.
- Security hardening template.
- Documentation improvement template.
- Pull request template.

Templates must not ask for real secrets, API keys, private repository contents, or production config.

## Actions Monitoring
Current workflows:
- `ci`: source restore, build, tests, and scan on Ubuntu and Windows.
- `cross-platform-smoke`: published NuGet global tool smoke on Windows, Ubuntu, and macOS.
- `cross-platform-source-smoke`: current-branch package smoke on Windows, Ubuntu, and macOS.

Maintainers should check Actions after every push and before any release tag, GitHub Release, or NuGet publish.

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
