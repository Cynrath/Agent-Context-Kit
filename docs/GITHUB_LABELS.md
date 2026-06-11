# GitHub Labels

This document defines the recommended public issue and pull request label set for AgentContextKit.

Labels are maintainer-only GitHub metadata. Do not run the optional commands from an agent session unless the maintainer explicitly asks for metadata changes.

## Recommended Label Set
| Label | Color | Description | Use When |
| --- | --- | --- | --- |
| `type: bug` | `d73a4a` | Reproducible incorrect behavior. | A command, output, workflow, or package behaves incorrectly. |
| `type: feature` | `a2eeef` | New capability or workflow improvement. | The request adds a user-visible workflow or command behavior. |
| `type: docs` | `0075ca` | Documentation change. | README, docs, examples, release notes, or templates need updates. |
| `type: security` | `b60205` | Non-sensitive security hardening or vulnerability-process work. | Scanner precision, safe defaults, privacy, or release hygiene improves without disclosing a vulnerability. |
| `type: maintenance` | `cfd3d7` | Internal cleanup, tests, chores, or dependency/process work. | Code or docs are reorganized with no user-facing feature change. |
| `status: needs-triage` | `fbca04` | Needs maintainer review. | A new issue or PR has not been classified yet. |
| `status: accepted` | `0e8a16` | Accepted for implementation. | Maintainer agrees the issue should be worked. |
| `status: blocked` | `b60205` | Cannot move without an external action. | Work needs maintainer-only GitHub/NuGet action, external decision, or unavailable evidence. |
| `priority: high` | `d93f0b` | Important near-term work. | CI breakage, release-blocking bug, or major user workflow failure. |
| `priority: medium` | `fbca04` | Normal planned work. | Useful improvement or false positive reduction without urgent release impact. |
| `priority: low` | `0e8a16` | Polish or backlog work. | Wording, small docs, cleanup, or low-risk follow-up. |
| `area: cli` | `0052cc` | CLI commands and UX. | Command parsing, help, localization output, exit codes, or command behavior. |
| `area: scanner` | `1d76db` | Stack or risk scanner. | Stack detection, risk findings, allowlists, JSON scan output, or redact checks. |
| `area: sarif` | `5319e7` | SARIF output or Code Scanning integration. | SARIF generation, parsing, upload examples, or Code Scanning decisions. |
| `area: docs` | `0075ca` | Documentation surface. | README, docs, task files, generated guidance, or release notes. |
| `area: ci` | `5319e7` | GitHub Actions and validation. | CI, smoke tests, release gates, or workflow runtime changes. |
| `good first issue` | `7057ff` | Small contributor-friendly task. | Low-risk task with clear acceptance criteria and minimal repo knowledge needed. |

## Labeling Rules
- Use exactly one `type:*` label for most issues and PRs.
- Use one or more `area:*` labels when the affected subsystem is clear.
- Use at most one `priority:*` label after triage.
- Keep `status: needs-triage` on new public issues until a maintainer reviews them.
- Remove `status: needs-triage` when applying `status: accepted` or `status: blocked`.
- Do not use public labels to imply a vulnerability disclosure. Sensitive vulnerability reports go through `SECURITY.md`.

## Optional GitHub CLI Commands
The following examples are documentation only. They create or update GitHub labels if a maintainer chooses to run them.

```powershell
gh label create "type: bug" --repo Cynrath/agent-context-kit --color "d73a4a" --description "Reproducible incorrect behavior."
gh label create "type: feature" --repo Cynrath/agent-context-kit --color "a2eeef" --description "New capability or workflow improvement."
gh label create "type: docs" --repo Cynrath/agent-context-kit --color "0075ca" --description "Documentation change."
gh label create "type: security" --repo Cynrath/agent-context-kit --color "b60205" --description "Non-sensitive security hardening or vulnerability-process work."
gh label create "type: maintenance" --repo Cynrath/agent-context-kit --color "cfd3d7" --description "Internal cleanup, tests, chores, or dependency/process work."
gh label create "status: needs-triage" --repo Cynrath/agent-context-kit --color "fbca04" --description "Needs maintainer review."
gh label create "status: accepted" --repo Cynrath/agent-context-kit --color "0e8a16" --description "Accepted for implementation."
gh label create "status: blocked" --repo Cynrath/agent-context-kit --color "b60205" --description "Cannot move without an external action."
gh label create "priority: high" --repo Cynrath/agent-context-kit --color "d93f0b" --description "Important near-term work."
gh label create "priority: medium" --repo Cynrath/agent-context-kit --color "fbca04" --description "Normal planned work."
gh label create "priority: low" --repo Cynrath/agent-context-kit --color "0e8a16" --description "Polish or backlog work."
gh label create "area: cli" --repo Cynrath/agent-context-kit --color "0052cc" --description "CLI commands and UX."
gh label create "area: scanner" --repo Cynrath/agent-context-kit --color "1d76db" --description "Stack or risk scanner."
gh label create "area: sarif" --repo Cynrath/agent-context-kit --color "5319e7" --description "SARIF output or Code Scanning integration."
gh label create "area: docs" --repo Cynrath/agent-context-kit --color "0075ca" --description "Documentation surface."
gh label create "area: ci" --repo Cynrath/agent-context-kit --color "5319e7" --description "GitHub Actions and validation."
gh label create "good first issue" --repo Cynrath/agent-context-kit --color "7057ff" --description "Small contributor-friendly task."
```

If a label already exists, use `gh label edit` with the same `--color` and `--description` values.
