# TASK-0047: NuGet Publish Verification

## Status
Completed.

## Purpose
Record that `AgentContextKit` version `0.1.0-alpha.1` has been published to NuGet and verified as a global .NET tool, then prepare the Codex for OSS submission materials as the remaining follow-up.

## Scope
- Update README install instructions from pending publication to active NuGet install.
- Mark GitHub Release, NuGet publish, and NuGet install verification as completed in release handoff docs.
- Remove NuGet publish and GitHub Release blockers from active release blocker docs.
- Keep Codex for OSS form submission as the remaining non-release follow-up.
- Update Codex handoff/context docs.
- Run local build, test, scan, doctor, real-name scan, artifact scan, and release blocker gates.

## Affected Files
- `docs/tasks/TASK-0047-nuget-publish-verification.md`
- `README.md`
- `README.tr.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/OSS_READINESS.md`
- `docs/CODEX_FOR_OSS_APPLICATION.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- Related active release docs or gate scripts if they still mention stale NuGet/GitHub Release follow-up state.

## DB Impact
None. This project has no database or migrations.

## Admin Impact
None. There is no admin UI.

## Permission Impact
None. No authentication or authorization behavior changes.

## SEO/i18n Impact
README and README.tr install wording changes from pending publication to active package installation.

## Audit/Security Impact
Improves release audit accuracy by recording completed NuGet publish and install verification while preserving local-only agent safety boundaries.

## Acceptance Criteria
- README install command is active, not described as after publication.
- Maintainer handoff shows GitHub Release completed.
- Maintainer handoff shows NuGet publish completed.
- Maintainer handoff shows NuGet install verification completed.
- Release blockers no longer list GitHub Release or NuGet publish as blockers.
- OSS readiness says `v0.1.0-alpha.1` is published on GitHub and NuGet.
- Codex for OSS application pack says GitHub and NuGet are published and includes the package install command.
- Changelog records NuGet publish verification.
- Local build, tests, scan, doctor, identity scan, artifact scan, and release gates pass.

## Tests
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- Run the prohibited maintainer identity term scan with `rg` and safe excludes.
- `git ls-files | rg -n "\.(rar|zip|nupkg|snupkg|bak|tmp|binlog|trx)$|(^|/)(bin|obj|TestResults|coverage|publish|out)/"`
- `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues`
- `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues`
- `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers`

## Risks
- NuGet indexing can lag after publish; the maintainer has already verified global tool install for this task.
- Future docs commits are newer than the release tag; local gate scripts should treat this as valid post-release documentation sync.

## Rollback
- Revert this documentation sync commit.
- Do not unpublish NuGet or move the pushed release tag from an agent session.

## Completion Notes
Completed.

- Updated README and README.tr so NuGet install is an active command for `AgentContextKit` version `0.1.0-alpha.1`.
- Updated maintainer handoff, blocker, OSS readiness, release checklist, release candidate, packaging, NuGet metadata, public gate, public audit, roadmap, and Codex for OSS docs to reflect completed GitHub Release, NuGet publish, and global tool install verification.
- Verified GitHub Release page through the GitHub API: `https://github.com/Cynrath/agent-context-kit/releases/tag/v0.1.0-alpha.1`.
- Verified NuGet package version `0.1.0-alpha.1` through the NuGet flat-container index.
- Verified Codex for OSS form-ready text lengths remain below 500 characters.
- Removed stale active-doc language that described GitHub Release or NuGet publish as not yet completed.
- `dotnet restore AgentContextKit.sln`: passed.
- `dotnet build AgentContextKit.sln -c Release --no-restore`: passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build`: passed, 59/59 tests.
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`: passed with no risk findings and main stacks `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.
- `dotnet run --project src/AgentContextKit.Cli -- doctor`: passed.
- Initial parallel `scan --ci` and `doctor` attempt hit a transient Debug build file lock; both commands passed when rerun sequentially.
- Prohibited maintainer identity term scan returned no matches.
- Tracked artifact scan returned no matches.
- Stale active release wording scan returned no matches.
- `scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues`: passed with the expected dirty working tree warning before commit.
- Post-commit `scripts/check-public-release-gates.ps1 -FailOnIssues`: passed.
- Post-commit `scripts/audit-public-release.ps1 -FailOnIssues`: passed.
- Post-commit `scripts/check-release-blockers.ps1 -FailOnBlockers`: passed.
- Post-commit release gates reported the expected warning that `HEAD` is a documentation sync commit after `v0.1.0-alpha.1`; remote tag verification remains manual.
