# TASK-0061 - Sample Repository Gallery, Demo Scenarios, And Onboarding Examples

## Purpose
Add a safe sample repository gallery and demo scenarios so users can quickly understand AgentContextKit stack detection, repository health checks, scanner behavior, generated local reports, Web UI output, and current source-only SARIF behavior.

## Scope
- Add `docs/SAMPLE_GALLERY.md`.
- Add `docs/DEMO_SCENARIOS.md`.
- Add missing safe sample repositories under `samples/`.
- Strengthen existing sample READMEs without breaking sample structure.
- Update README, documentation index, roadmap, OSS readiness, release validation, maintainer guide, GitHub Actions usage, SARIF docs, project map, and Codex handoff files.
- Optionally add a small sample validation helper if it stays simple and local-only.

## Out Of Scope
- Do not push, tag, publish NuGet, create GitHub Releases, or upload SARIF/content.
- Do not add generated `.ackit/` outputs, local reports, Web UI files, SARIF files, ZIP/RAR archives, package artifacts, `bin/`, `obj/`, `node_modules`, or restore/build artifacts.
- Do not add real secrets, real credentials, private keys, production config, or exact sensitive token prefixes.
- Do not change runtime scanner behavior unless validation reveals a docs-blocking issue.

## Affected Files
- `docs/SAMPLE_GALLERY.md`
- `docs/DEMO_SCENARIOS.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/OSS_READINESS.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/MAINTAINER_GUIDE.md`
- `docs/GITHUB_ACTIONS_USAGE.md`
- `docs/SARIF_OUTPUT.md`
- `docs/SAMPLES.md`
- `samples/README.md`
- `scripts/test-samples.ps1`
- `samples/dotnet-console/*`
- `samples/dotnet-minimal-api/README.md`
- `samples/node-tooling/README.md`
- `samples/generic-empty-repo/*`
- `samples/security-fixture-repo/*`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## Documentation Impact
- The sample gallery should explain each sample path, stack, expected detected stack, expected health gaps, suggested commands, expected generated files, and expected risk behavior.
- Demo scenarios should distinguish published package commands from current source commands.
- SARIF docs must keep the published package versus current source availability distinction.

## Sample Repository Impact
- Add safe, minimal samples only.
- Existing samples must remain small and artifact-free.
- Samples may be scanner fixtures rather than fully buildable projects, but buildable samples are preferred where simple.

## Security / Privacy Impact
- Samples must not contain real secrets.
- Security fixture examples must avoid exact raw secret prefixes. Use split notation in documentation rather than writing sensitive-looking prefixes directly.
- Local report, Web UI, SARIF, and `.ackit/` outputs remain ignored local artifacts and must not be committed.

## Acceptance Criteria
- `docs/SAMPLE_GALLERY.md` documents all five requested samples.
- `docs/DEMO_SCENARIOS.md` documents published package, source, sample gallery, Web UI, and SARIF demos.
- `samples/dotnet-console`, `samples/generic-empty-repo`, and `samples/security-fixture-repo` exist with safe minimal content.
- Existing `samples/dotnet-minimal-api` and `samples/node-tooling` README files include expected behavior notes.
- README files link to the sample gallery and demo scenarios.
- Validation passes without committing generated artifacts or sensitive fixture literals.

## Test Steps
- `git status --short`
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`
- `dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0061.sarif`
- `Test-Path .ackit/reports/task-0061.sarif`
- `Get-Content .ackit/reports/task-0061.sarif | ConvertFrom-Json`
- `ackit version`
- `ackit --help`
- `git diff --check`
- Sample smoke scans from sample directories.
- Maintainer identity scan.
- Tracked artifact scan including `*.sarif`.
- Exact fake token/local path scan.
- `powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`
- `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues`

## Risks
- Sample fixture wording can accidentally trigger scanner or hygiene false positives.
- Demo docs can imply generated `.ackit/` artifacts should be committed.
- Published package examples can accidentally include source-only `ackit sarif`.
- Sample projects can grow into dependency/build artifacts if guardrails are unclear.

## Rollback Plan
- Revert the TASK-0061 commit.
- Remove added sample directories and demo/gallery docs.
- Restore sample README, documentation index, roadmap, release docs, and Codex handoff files to TASK-0060 state.

## Completion Notes
- `git fetch origin` completed.
- `git status -sb` showed local `master` aligned with `origin/master`.
- Recent commits:
  - `9beb2fe docs: add GitHub Actions usage examples`
  - `aaaad5f feat: add SARIF report output`
  - `8d5893b docs: add repository settings and label guidance`
  - `c0f1eb2 docs: add GitHub contributor workflow and support docs`
  - `8dac923 docs: verify alpha2 publish and refresh agent context`
- GitHub CLI was available for read-only workflow inspection.
- Read-only GitHub Actions results for `Cynrath/agent-context-kit` showed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs succeeded after `docs: add GitHub Actions usage examples`.
- Existing samples before TASK-0061 were `samples/dotnet-minimal-api` and `samples/node-tooling`.
- Added `docs/SAMPLE_GALLERY.md`, `docs/DEMO_SCENARIOS.md`, `samples/dotnet-console`, `samples/generic-empty-repo`, `samples/security-fixture-repo`, and `scripts/test-samples.ps1`.
- Updated README files, sample docs, documentation index, roadmap, OSS readiness, release validation, maintainer guide, GitHub Actions usage, SARIF docs, project map, changelog, and Codex handoff files.
- `dotnet restore AgentContextKit.sln` passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 72/72 tests.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci` passed with no risk findings; main repository stacks remained `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.
- `dotnet run --project src/AgentContextKit.Cli -- doctor` passed.
- `dotnet run --project src/AgentContextKit.Cli -- scan --json` returned `riskSummary.total` 0.
- `dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/task-0061.sarif` produced an ignored local SARIF file with 0 findings; JSON parse confirmed SARIF 2.1.0 and `AgentContextKit` tool version `0.1.0-alpha.2`.
- Published global tool checks passed: `ackit version` returned `AgentContextKit 0.1.0-alpha.2`, and `ackit --help` worked. The published package help did not list source-only `sarif`, which is expected for `0.1.0-alpha.2`.
- `scripts/test-samples.ps1 -NoBuild` passed for all sample repositories. Sample-specific stack detection behaved as expected without polluting the root repository stack.
- Maintainer identity scan returned no matches.
- Tracked artifact scan including `*.sarif` returned no matches.
- Exact fake token/local path scan returned no matches in tracked source and in the generated local SARIF file.
- `git diff --check` passed.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues` passed with only the expected dirty working tree warning.
- `scripts/check-public-release-gates.ps1 -FailOnIssues` failed pre-commit only because the working tree had uncommitted changes; package metadata was clean and the post-release `HEAD` warning remains expected.
- After commit, `scripts/check-public-release-gates.ps1 -FailOnIssues` passed with no blocking items; only the expected post-release `HEAD` warning and manual remote tag verification note remained.
- No push, tag, NuGet publish, GitHub Release, SARIF upload, repository settings change, label change, or branch protection change was performed.
