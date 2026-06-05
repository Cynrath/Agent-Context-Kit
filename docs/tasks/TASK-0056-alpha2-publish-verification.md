# TASK-0056 - Alpha.2 Publish Verification, Docs Sync, Agent Refresh

## Purpose
Record the completed `v0.1.0-alpha.2` GitHub/NuGet publication, update current-release documentation, refresh agent instruction files, and move published-package smoke coverage from `0.1.0-alpha.1` to `0.1.0-alpha.2`.

## Scope
- Mark `v0.1.0-alpha.2` as published and verified.
- Update README install examples to `0.1.0-alpha.2`.
- Keep `v0.1.0-alpha.1` as historical release context only.
- Update published-package smoke workflow to install `AgentContextKit` `0.1.0-alpha.2`.
- Keep source-package smoke workflow as current branch validation.
- Refresh `AGENTS.md`, `CLAUDE.md`, Cursor, and Copilot instructions if stale release, blocker, version, or stack data is present.
- Add local-only Web UI/report artifact guidance.
- Generate local report/Web UI smoke outputs without committing them.
- Run build, test, scan, doctor, installed-tool checks, stale wording scans, and hygiene gates.

## Affected Files
- `README.md`
- `README.tr.md`
- `AGENTS.md`
- `CLAUDE.md`
- `.cursor/rules/project.mdc`
- `.github/copilot-instructions.md`
- `.github/workflows/cross-platform-smoke.yml`
- `.github/workflows/cross-platform-source-smoke.yml`
- `CHANGELOG.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/PUBLIC_RELEASE_AUDIT.md`
- `docs/PUBLIC_RELEASE_GATES.md`
- `docs/OSS_READINESS.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/PACKAGING.md`
- `docs/NUGET_METADATA.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/SOURCE_HYGIENE.md`
- `docs/HTML_REPORTS.md`
- `docs/WEB_UI_PROTOTYPE.md`
- `docs/SECURITY_NOTES.md`
- `docs/TROUBLESHOOTING.md`
- `docs/tasks/TASK-0055-alpha2-release-decision.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `.codex/HANDOFF.md`
- `scripts/audit-public-release.ps1`
- `scripts/check-release-blockers.ps1`
- `scripts/check-v100-documentation-release-gates.ps1`

## DB Impact
None. This repository has no database schema or migrations.

## Admin Impact
None. There is no admin UI.

## Permission Impact
No application permission changes. Workflow permissions remain read-only for repository contents.

## SEO / i18n Impact
- English and Turkish README current-release wording is updated.
- No website SEO surface is added.

## Audit / Security Impact
- Public artifact guidance is tightened for local `.ackit/reports/` and `.ackit/webui/` outputs.
- No telemetry, upload, provider call, API key handling, tag creation, GitHub Release creation, NuGet publish, or push is performed by this task.
- Exact secret/local-path hygiene scans must remain clean.

## Acceptance Criteria
- Task file exists before implementation.
- README and README.tr state `v0.1.0-alpha.2` is current, published, and verified.
- NuGet install examples use `0.1.0-alpha.2`.
- Published-package smoke workflow installs `AgentContextKit` `0.1.0-alpha.2`.
- Source-package smoke workflow remains separate and validates the current branch/local package.
- Agent instruction files use the main repository stack only: `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.
- Stale public release blocker language is removed from active agent/handoff docs.
- Web UI/report docs say local outputs can include local paths and should not be shared as public release artifacts.
- Local report and Web UI smoke outputs are generated and remain untracked.
- Build, tests, scan, doctor, installed-tool checks, stale wording scans, hygiene scans, and documentation gates pass.

## Tests
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`
- `ackit version`
- `ackit --help`
- `dotnet run --project src/AgentContextKit.Cli -- report --output .ackit/reports/alpha2-post-publish.html`
- `dotnet run --project src/AgentContextKit.Cli -- webui --output .ackit/webui/alpha2-post-publish.html`
- Stale release wording scan
- Maintainer identity scan
- Tracked artifact scan
- Exact token/local-path scan
- `git diff --check`
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`

## Implementation Notes
- README and README.tr now show `v0.1.0-alpha.2` as the current published and verified release.
- NuGet install examples now use `0.1.0-alpha.2`.
- Published-package smoke workflow now installs `AgentContextKit` `0.1.0-alpha.2`.
- Source-package smoke workflow remains separate for current branch/local package validation.
- AGENTS, CLAUDE, Cursor, and Copilot instructions now show the main repository stack only: `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.
- Active public release blocker wording was removed from agent and handoff docs.
- Local report/Web UI docs now warn that generated outputs can include local paths and should not be shared as public release artifacts.
- Release gate scripts now check the current release tag `v0.1.0-alpha.2`.

## Validation Results
- `dotnet restore AgentContextKit.sln`: passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore`: passed with 0 warnings and 0 errors after rerunning without parallel test file lock.
- `dotnet test AgentContextKit.sln -c Release --no-build`: passed, 67/67 tests.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`: passed with no risk findings.
- `dotnet run --project src/AgentContextKit.Cli -- doctor`: passed.
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`: passed and reported `toolVersion` `0.1.0-alpha.2`.
- `ackit version`: returned `AgentContextKit 0.1.0-alpha.2`.
- `ackit --help`: passed.
- `ackit report --output .ackit/reports/alpha2-post-publish.html`: created the ignored local report with risk findings `0`.
- `ackit webui --output .ackit/webui/alpha2-post-publish.html`: created the ignored local Web UI with risk findings `0`.
- `.ackit/reports/alpha2-post-publish.html` and `.ackit/webui/alpha2-post-publish.html` are ignored by `.gitignore`.
- Stale release wording scan: no active matches after historical wording cleanup.
- Maintainer identity scan: no matches.
- Tracked artifact scan: no matches.
- Exact token/local-path scan: no matches.
- `git diff --check`: passed.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`: passed after updating the current release expectation to `v0.1.0-alpha.2`.

## Risks
- Some historical task and release-candidate files intentionally mention alpha.1; stale scans must distinguish active docs from archived history.
- Local Web UI/report outputs can include local paths and must remain ignored.
- Published workflow updates require hosted GitHub Actions validation after push.

## Rollback
- Revert the documentation/workflow commit.
- Restore published-package smoke workflow to the previous package version only if alpha.2 publication is invalidated.
- Keep the already pushed tag/GitHub Release/NuGet publication as maintainer-managed external state.

## Status
- Completed locally.
