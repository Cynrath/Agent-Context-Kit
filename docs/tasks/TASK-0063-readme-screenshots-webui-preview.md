# TASK-0063 README Screenshots, Web UI Preview, And Visual Documentation Polish

## Purpose
Prepare safe public visual documentation for AgentContextKit after the `v0.1.0-alpha.2` release and the current-source SARIF/scanner expansion work.

## Scope
- Add README preview guidance without committing unsanitized screenshots.
- Document public screenshot and visual asset rules.
- Document local-only Web UI preview usage and screenshot hygiene.
- Add a small safe diagram asset if it contains no local paths, private data, or sensitive values.
- Sync documentation index, roadmap, project map, OSS readiness, release validation, demo, sample, Web UI, and report docs.
- Run local report/Web UI smoke checks and release validation.

## Out Of Scope
- No push.
- No tag.
- No NuGet publish.
- No GitHub Release creation.
- No GitHub Code Scanning upload.
- No committed local `.ackit/` output, SARIF output, HTML report, generated Web UI file, archive, or screenshot from a local machine.

## Affected Files
- `README.md`
- `README.tr.md`
- `docs/VISUAL_ASSETS.md`
- `docs/WEB_UI_PREVIEW.md`
- `docs/assets/diagrams/ackit-flow.svg`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/OSS_READINESS.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/GITHUB_REPO_HYGIENE.md`
- `docs/DEMO_SCENARIOS.md`
- `docs/SAMPLE_GALLERY.md`
- `docs/WEB_UI_PROTOTYPE.md`
- `docs/HTML_REPORTS.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## Documentation Impact
Public docs will explain how to generate local Web UI and report previews, why generated HTML remains local-only, and how future screenshots can be safely sanitized before commit.

## Asset/Privacy Impact
Only sanitized, repository-generic visual assets may be committed. Generated Web UI/report HTML and screenshots that include local paths, private names, machine-specific data, raw finding matches, or sensitive values remain uncommitted.

## Security/Privacy Impact
The task reduces accidental public exposure risk by documenting visual asset rules and screenshot review checklists before README screenshots are added.

## Acceptance Criteria
- `docs/VISUAL_ASSETS.md` exists and defines commit-safe and forbidden visual assets.
- `docs/WEB_UI_PREVIEW.md` exists and documents local-only Web UI preview usage.
- README files contain a compact preview section and links to visual asset, Web UI, sample, and demo docs.
- Documentation index and project map link the new docs and optional diagram.
- Roadmap lists TASK-0064, TASK-0065, and TASK-0066 follow-ups.
- Local Web UI/report smoke outputs are generated under `.ackit/` and not committed.
- Build, tests, scan, doctor, SARIF parse, sample smoke, hygiene scans, and documentation gates are run before commit.

## Test Steps
```powershell
git status --short
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- doctor
dotnet run --project src/AgentContextKit.Cli -- scan --json
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0063.sarif
powershell -ExecutionPolicy Bypass -File scripts/test-samples.ps1 -NoBuild
git diff --check
powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

## Risks
- README preview wording could imply screenshots exist before sanitized assets are available.
- Committed screenshots could leak local paths if captured directly from generated Web UI output.
- Documentation gates may flag dirty-tree state before commit.

## Rollback Plan
Revert this task commit to remove the new visual docs, README preview section, diagram asset, and related documentation links.

## Completion Notes
- Started from clean `master` aligned with `origin/master` after fast-forwarding to `a856aac`.
- Read-only GitHub CLI checks reported latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs as successful.
- Added `docs/VISUAL_ASSETS.md`, `docs/WEB_UI_PREVIEW.md`, README preview sections, documentation links, and `docs/assets/diagrams/ackit-flow.svg`.
- Local report and Web UI smoke generated ignored `.ackit` outputs successfully, then removed those task-specific local artifacts after Test-Path validation.
- Validation passed: restore, Release build, 83/83 tests, `scan --ci`, `doctor`, `scan --json`, SARIF generation/parse, sample smoke, installed `ackit version`, installed `ackit --help`, `git diff --check`, real-name scan, tracked artifact scan, exact fake-token/local-path scan, and v1.0 documentation release gate.
- Pre-commit public release gate reported no package metadata issue and failed only because the working tree had uncommitted changes.
- Post-commit public release gate passed with no blocking items; only the expected post-release `HEAD` warning and manual remote tag verification note remained.
