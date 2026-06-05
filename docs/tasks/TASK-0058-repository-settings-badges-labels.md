# TASK-0058 - Repository Settings, Badges, Labels, And Public Presentation Hardening

## Purpose
Improve the public GitHub presentation and maintainer workflow after `v0.1.0-alpha.2` by adding README badges, GitHub label guidance, repository settings checklist, and current Actions status documentation.

## Scope
- Read current git/remote status.
- Read GitHub Actions workflow status with GitHub CLI if available.
- Add or update README badges for CI, cross-platform smoke workflows, NuGet, license, and .NET 10.
- Add `docs/GITHUB_LABELS.md` with recommended labels, colors, descriptions, usage guidance, and optional non-executed `gh` commands.
- Add `docs/GITHUB_SETTINGS_CHECKLIST.md` with repository metadata, topics, branch protection, security settings, release settings, and manual verification checklist.
- Link the new docs from README, documentation index, maintainer docs, roadmap, OSS readiness, release validation, and Codex handoff files.
- Record future task planning for SARIF, Actions examples, sample gallery, scanner expansion, and README preview assets.
- Run build, tests, scan, doctor, JSON scan, installed tool checks, hygiene scans, and release documentation gates.

## Affected Files
- `README.md`
- `README.tr.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/GITHUB_LABELS.md`
- `docs/GITHUB_SETTINGS_CHECKLIST.md`
- `docs/GITHUB_REPO_HYGIENE.md`
- `docs/ISSUE_TRIAGE.md`
- `docs/MAINTAINER_GUIDE.md`
- `docs/SUPPORT_MATRIX.md`
- `docs/OSS_READINESS.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/ROADMAP.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `.github/ISSUE_TEMPLATE/bug_report.yml`
- `.github/ISSUE_TEMPLATE/feature_request.yml`
- `.github/ISSUE_TEMPLATE/security_hardening.yml`
- `.github/ISSUE_TEMPLATE/docs_improvement.yml`
- `docs/PROJECT_MAP.md`
- `docs/tasks/TASK-0058-repository-settings-badges-labels.md`

## DB Impact
None. This repository has no database schema or migrations.

## Admin Impact
None. There is no application admin UI.

## Permission Impact
No application permission changes. GitHub label, branch protection, repository settings, and security settings changes are documented as maintainer-only manual actions and are not executed.

## SEO / i18n Impact
- README and README.tr public presentation improves with compact badges and links.
- No hosted website SEO surface changes.
- English and Turkish README content should stay aligned.

## Audit / Security Impact
- Do not create GitHub labels, mutate branch protection, change repository settings, push, tag, publish NuGet packages, or create GitHub Releases.
- Do not include secrets, private repository content, production config, or local machine paths in docs.
- Keep public examples generic and safe.

## Acceptance Criteria
- README.md and README.tr.md include compact current badges for workflows, NuGet, license, and .NET 10.
- Install command remains `dotnet tool install --global AgentContextKit --version 0.1.0-alpha.2`.
- `ackit --help`, `ackit version`, and Windows/Ubuntu/macOS GitHub Actions coverage remain visible.
- `docs/GITHUB_LABELS.md` contains the requested label set with color, description, and usage guidance.
- `docs/GITHUB_SETTINGS_CHECKLIST.md` contains repository description, topics, branch protection recommendations, security settings, release settings, and manual verification checklist.
- Documentation index and maintainer docs link the new docs.
- Roadmap includes TASK-0059 through TASK-0063 planning entries.
- GitHub Actions status is recorded from read-only `gh` evidence or documented as manual validation if unavailable.
- Build, tests, scan, doctor, JSON scan, installed tool checks, hygiene scans, and release gates pass.

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
- Badge URLs can become stale if workflow file names or package ID change.
- Label plans can diverge from actual GitHub labels unless maintainers apply them intentionally.
- Branch protection recommendations may need adjustment after the solo bootstrap phase.

## Rollback
- Revert the TASK-0058 commit.
- Remove the added badge lines and GitHub settings/labels docs.
- Restore affected documentation links and handoff notes to the previous state.

## Current Remote And Actions Observations
- `git fetch origin` completed.
- `git status -sb` showed local `master` aligned with `origin/master`, with only the new TASK-0058 task file untracked at observation time.
- Recent commits:
  - `c0f1eb2 docs: add GitHub contributor workflow and support docs`
  - `8dac923 docs: verify alpha2 publish and refresh agent context`
  - `a348e5a chore: prepare alpha2 source smoke and version bump`
  - `b1b34c3 docs: add alpha2 release decision task`
  - `5e1aef8 docs: prepare alpha2 release plan`
- GitHub CLI was available for read-only workflow inspection.
- Latest read-only workflow results for `Cynrath/agent-context-kit`:
  - `ci`: success for `docs: add GitHub contributor workflow and support docs`.
  - `cross-platform-smoke`: success for `docs: add GitHub contributor workflow and support docs`.
  - `cross-platform-source-smoke`: success for `docs: add GitHub contributor workflow and support docs`.

## Implementation Notes
- Added compact README badges for CI, published-package smoke, source-package smoke, NuGet version, NuGet downloads, license, and .NET 10.
- Added `docs/GITHUB_LABELS.md` with label names, colors, descriptions, usage rules, and optional maintainer-only `gh label create` examples.
- Added `docs/GITHUB_SETTINGS_CHECKLIST.md` with repository metadata, topics, branch protection, security settings, release settings, and manual verification checklist.
- Aligned issue template labels with the recommended `type:*` and `status: needs-triage` label scheme.
- Linked the new docs from README, README.tr, documentation index, project map, GitHub repo hygiene, issue triage, maintainer guide, support matrix, OSS readiness, release validation, roadmap, and Codex handoff files.
- Did not create GitHub labels, change branch protection, mutate repository settings, push, tag, create a GitHub Release, publish NuGet, or handle API keys.

## Validation Notes
- `git status --short` showed only expected TASK-0058 documentation/template changes.
- `dotnet restore AgentContextKit.sln` passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed: 67/67.
- Initial parallel `dotnet run` validation hit a transient `VBCSCompiler` Debug output file lock. `dotnet build-server shutdown` completed, and the CLI checks were rerun sequentially.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci` passed with no risk findings after removing numeric workflow run IDs from the task notes.
- `dotnet run --project src/AgentContextKit.Cli -- doctor` passed.
- `dotnet run --project src/AgentContextKit.Cli -- scan --json` passed with risk summary 0.
- `ackit version` returned `AgentContextKit 0.1.0-alpha.2`.
- `ackit --help` worked.
- Maintainer identity term scan returned no matches.
- Tracked artifact scan returned no matches.
- Exact fake token/local-path scan returned no matches.
- `git diff --check` passed.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` passed with only the expected dirty working tree warning.
- `scripts/check-public-release-gates.ps1 -FailOnIssues` failed before commit only because public release gates intentionally reject uncommitted working tree changes; rerun after commit is required.
- After the first TASK-0058 commit, `scripts/check-public-release-gates.ps1 -FailOnIssues` passed with no blocking items. It kept the expected warning that `HEAD` is a post-release documentation commit rather than the `v0.1.0-alpha.2` tag, and remote tag verification remains manual.

## Status
- Completed.
