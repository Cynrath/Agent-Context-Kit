# TASK-0057 - GitHub Repo Hygiene, Templates, Maintainer Guide, Support Matrix

## Purpose
Strengthen the open source repository surface after `v0.1.0-alpha.2` publication by adding GitHub issue templates, a pull request template, maintainer/contributor support docs, and current Actions result documentation.

## Scope
- Try to read GitHub Actions status with `gh` in read-only mode.
- Document Actions result evidence or manual validation requirements.
- Add GitHub issue templates for bugs, features, security hardening, and docs.
- Add a pull request template.
- Add maintainer, support matrix, contributor onboarding, repository hygiene, and issue triage docs.
- Link the new docs from README, documentation index, project map, roadmap, OSS readiness, maintainer handoff, and Codex handoff files.
- Run build, tests, scan, doctor, JSON scan, installed-tool checks, stale/hygiene scans, and release documentation gates.

## Affected Files
- `.github/ISSUE_TEMPLATE/bug_report.yml`
- `.github/ISSUE_TEMPLATE/feature_request.yml`
- `.github/ISSUE_TEMPLATE/security_hardening.yml`
- `.github/ISSUE_TEMPLATE/docs_improvement.yml`
- `.github/ISSUE_TEMPLATE/config.yml`
- `.github/pull_request_template.md`
- `docs/MAINTAINER_GUIDE.md`
- `docs/SUPPORT_MATRIX.md`
- `docs/CONTRIBUTOR_ONBOARDING.md`
- `docs/GITHUB_REPO_HYGIENE.md`
- `docs/ISSUE_TRIAGE.md`
- `README.md`
- `README.tr.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/OSS_READINESS.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/RELEASE_VALIDATION.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None. This repository has no database schema or migrations.

## Admin Impact
None. There is no admin UI.

## Permission Impact
No application permission changes. GitHub Actions documentation stays read-only. No workflow dispatch, rerun, push, tag, GitHub Release, or NuGet publish is performed.

## SEO / i18n Impact
- README and README.tr receive short links to contributor/support docs.
- No web SEO surface changes.

## Audit / Security Impact
- Issue templates must not ask reporters to paste secrets, private repository contents, production config, or API keys.
- Security hardening issues must route vulnerability disclosure to `SECURITY.md`.
- Hygiene scans must remain clean for maintainer identity terms, tracked artifacts, and exact token/local-path patterns.

## Acceptance Criteria
- All requested issue templates exist and collect useful reproduction/context fields.
- Security hardening template warns against sharing real secrets and points vulnerability disclosure to `SECURITY.md`.
- Pull request template includes summary, related task/issue, change type, safety/security impact, localization impact, generated files impact, tests run, and checklist.
- Maintainer guide covers release flow, version bump, tag, GitHub Release, NuGet publish, post-publish verification, Actions validation, and rollback notes.
- Support matrix documents Windows, Ubuntu, macOS, .NET 10, unsupported older .NET versions, and shell notes.
- Contributor onboarding documents clone/build/test/local CLI/scanner/task-first/test/artifact workflow.
- GitHub repo hygiene doc covers description, topics, branch protection, release checklist, templates, Actions monitoring, and NuGet ownership notes.
- Issue triage doc covers label recommendations, issue type routing, severity mapping, duplicate/invalid closure, and discussion/backlog conversion.
- Active release docs record `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` as passing per maintainer-provided status or read-only `gh` evidence.
- Build, tests, scanner, doctor, installed-tool checks, stale/hygiene scans, and release documentation gates pass.

## Tests
- `git status --short`
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`
- `ackit version`
- `ackit --help`
- `git diff --check`
- Maintainer identity scan
- Tracked artifact scan
- Exact token/local-path scan
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`
- `scripts/check-public-release-gates.ps1 -FailOnIssues`

## Risks
- GitHub CLI may be unavailable or unauthenticated; in that case hosted Actions status remains documented from maintainer-provided evidence.
- Issue templates can accidentally solicit sensitive data; keep template wording explicit and conservative.
- New docs can become stale if release process changes; keep maintainer handoff as the current release source of truth.

## Rollback
- Revert the TASK-0057 commit.
- Remove the added GitHub templates and maintainer/support docs.
- Restore README/doc index/project map/roadmap/handoff links to the previous state.

## Implementation Notes
- GitHub CLI was available and authenticated for read-only workflow inspection.
- `gh run list` confirmed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs succeeded on `master`.
- `gh run view` confirmed commit `8dac9237c27ba912d056344155f1c9f901557bf5` had successful hosted validation:
  - `ci`: `build (windows-2025)` and `build (ubuntu-latest)` succeeded.
  - `cross-platform-smoke`: `smoke (windows-2025)`, `smoke (ubuntu-latest)`, and `smoke (macos-latest)` succeeded.
  - `cross-platform-source-smoke`: `source smoke (windows-2025)`, `source smoke (ubuntu-latest)`, and `source smoke (macos-latest)` succeeded.
- Added GitHub issue templates for bug reports, feature requests, security hardening requests, and documentation improvements.
- Added GitHub issue template configuration with vulnerability disclosure routed to `SECURITY.md`.
- Added pull request template with safety, localization, generated-file, validation, and side-effect checklist items.
- Added maintainer, support matrix, contributor onboarding, GitHub repo hygiene, and issue triage docs.
- Linked the new docs from README, README.tr, documentation index, project map, roadmap, OSS readiness, maintainer handoff, release validation, and Codex handoff files.
- No push, tag creation, GitHub Release creation, NuGet publish, remote mutation, or API key handling was performed.

## Validation Notes
- `git status --short` showed only expected TASK-0057 documentation/template changes.
- `dotnet restore AgentContextKit.sln` passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed: 67/67.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci` passed with no risk findings.
- `dotnet run --project src/AgentContextKit.Cli -- doctor` passed.
- `dotnet run --project src/AgentContextKit.Cli -- scan --json` passed with risk summary 0 after a sequential rerun.
- `ackit version` returned `AgentContextKit 0.1.0-alpha.2`.
- `ackit --help` worked.
- Maintainer identity term scan returned no matches.
- Tracked artifact scan returned no matches.
- Exact fake token/local-path scan returned no matches.
- `git diff --check` passed.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` passed with only the expected dirty working tree warning.
- `scripts/check-public-release-gates.ps1 -FailOnIssues` failed before commit only because public release gates intentionally reject uncommitted working tree changes.
- After the first TASK-0057 commit, `scripts/check-public-release-gates.ps1 -FailOnIssues` passed with no blocking items. It kept the expected warning that `HEAD` is a post-release documentation commit rather than the `v0.1.0-alpha.2` tag, and remote tag verification remains manual.

## Status
- Completed.
