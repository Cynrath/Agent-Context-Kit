# Project Execution Queue

| Order | Status | Task | Priority | Blocking Status | Expected Files | Validation Required | Remote Write Required? | Done Criteria |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 1 | Done locally | TASK-0066 GitHub Release body polish documentation | High | GitHub Release edit is maintainer-only | `docs/RELEASE_BODY_V020_ALPHA1.md`, release docs, queue docs | Docs review, release gates | Yes, to edit GitHub Release body | Corrected release body draft exists with published pre-release wording. |
| 2 | Done locally | TASK-0067 GitHub labels and manual repo settings checklist | High | Label/settings writes are maintainer-only | `docs/GITHUB_LABELS.md`, `docs/GITHUB_SETTINGS_CHECKLIST.md`, triage/maintainer docs | Docs review, release gates | Yes, for labels, branch protection, repo settings | Manual checklist and label table are ready. |
| 3 | Done locally | TASK-0068 CodeQL / Code Scanning decision document | High | Code Scanning activation is maintainer-only | `docs/CODE_SCANNING_DECISION.md`, SARIF/security/Actions docs | Docs review, release gates | Yes, for SARIF upload activation and Code Scanning permissions | Decision records documentation-only default and opt-in path. |
| 4 | Done locally | TASK-0069 GitHub issue tracker bootstrap plan | Medium | Issue creation is maintainer-only | `docs/ISSUE_BACKLOG.md`, triage/onboarding docs | Docs review, release gates | Yes, to create GitHub issues | Initial issue backlog is ready to copy into GitHub. |
| 5 | Done locally | TASK-0070 scanner config examples and sample configs | Medium | None for docs-only examples | `docs/examples/config/*.yml`, config/scanner docs | Build/test/scan/doctor/hygiene/gates | No | Safe config examples exist without critical suppression or secrets. |
| 6 | Done locally | TASK-0071 SARIF GitHub Code Scanning opt-in workflow design | Medium | Activation is maintainer-only | docs example workflow/design docs | Build/test/scan/doctor/hygiene/gates | Yes, to activate workflow/upload | Opt-in workflow design is documented, not active. |
| 7 | Done locally | TASK-0072 JSON schema stability and contract tests | Medium | None | JSON docs/tests | Build/test/scan/doctor/hygiene/gates | No | Schema behavior is covered by tests. |
| 8 | Done locally | TASK-0073 CLI exit code contract hardening | Medium | None | exit code docs/tests | Build/test/scan/doctor/hygiene/gates | No | Exit code behavior is documented and tested. |
| 9 | Done locally | TASK-0074 scanner fixture coverage expansion | Medium | None | scanner fixtures/tests/docs | Build/test/scan/doctor/hygiene/gates | No | Fixture coverage increases without false-negative risk. |
| 10 | Done locally | TASK-0075 safe suppression audit log | Medium | None | config/scanner docs/code/tests | Build/test/scan/doctor/hygiene/gates | No | Suppression behavior has auditable local output. |
| 11 | Done locally | TASK-0076 README command examples and copy-paste polish | Low | None | README files | Build/test/scan/doctor/hygiene/gates | No | README examples are concise and current. |
| 12 | Queued | TASK-0077 sanitized screenshot capture plan | Low | Screenshots require manual sanitization | visual asset docs | Docs review/hygiene/gates | No | Screenshot plan avoids local paths and private data. |
| 13 | Queued | TASK-0078 docs site / GitHub Pages planning | Low | GitHub Pages activation is maintainer-only | docs planning file | Docs review/hygiene/gates | Yes, to enable Pages | Docs site decision and risks are documented. |
| 14 | Queued | TASK-0079 tutorial: first 5 minutes with ackit | Low | None | tutorial docs | Build/test/scan/doctor/hygiene/gates | No | Beginner tutorial exists and uses published package. |
| 15 | Queued | TASK-0080 tutorial: prepare a repo for AI coding agents | Low | None | tutorial docs | Build/test/scan/doctor/hygiene/gates | No | AI agent prep tutorial exists. |
| 16 | Queued | TASK-0081 v0.2.0-alpha.2 scope planning | Medium | Version decision needed before publish | roadmap/release docs | Docs review/hygiene/gates | No | Next alpha scope is documented. |
| 17 | Queued | TASK-0082 v0.3 roadmap decision | Medium | Product decision | roadmap docs | Docs review/hygiene/gates | No | v0.3 direction is decided. |
| 18 | Queued | TASK-0083 1.0 readiness gap analysis | Medium | None | readiness docs | Full local gates | No | 1.0 gaps are listed with owners and validation. |

## Remote-Write Guardrail
The following actions are not performed from an agent session without explicit maintainer approval:
- GitHub Release body edits.
- GitHub label creation or editing.
- Branch protection changes.
- Repository settings changes.
- GitHub Code Scanning or SARIF upload activation.
- GitHub issue creation.
- Push, tag creation, GitHub Release creation, NuGet publish.
