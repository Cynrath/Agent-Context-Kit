# TASK-0045: Final Public Release Handoff

## Status
Completed.

## Purpose
Produce the final local handoff for the first public GitHub release and NuGet alpha package, including exact commands, validation results, remaining external actions, and rollback notes.

## Scope
- Run the final validation sequence in the requested order.
- Record release blocker before/after state.
- Record GitHub readiness, NuGet readiness, Codex application pack location, and remaining manual actions.
- Commit completed docs and implementation changes after successful build/test.
- Prepare local annotated tag only after the final reviewed commit and passing gates.
- Do not push, publish, force push, create a remote, or expose secrets.

## Affected Files
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/OSS_READINESS.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md`
- `docs/tasks/TASK-0041-repository-url-finalization.md`
- `docs/tasks/TASK-0042-public-github-readiness.md`
- `docs/tasks/TASK-0043-release-tag-and-package-readiness.md`
- `docs/tasks/TASK-0044-codex-for-oss-application-pack.md`
- `docs/tasks/TASK-0045-final-public-release-handoff.md`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/i18n Impact
Public release handoff docs may update public-facing installation and release wording. No runtime SEO or i18n behavior changes.

## Audit/Security Impact
This is the final local release safety checkpoint before public push/publish. It must verify no tracked artifacts, prohibited maintainer identity terms, stale package URL blocker wording, high/critical scan findings, or dirty working tree remain.

## Acceptance Criteria
- Final validation commands are run and results are recorded.
- Build passes with 0 errors.
- Tests pass.
- `ackit scan --ci` passes with no risk findings.
- `ackit doctor` reports PASS.
- Real-name grep returns no matches.
- Tracked artifact grep returns no matches.
- Package metadata has final public URLs.
- Local tag `v0.1.0-alpha.1` exists if gates pass and points at the final commit.
- Push and NuGet publish remain manual unless explicitly approved outside this local preparation.

## Tests
- `git status --short`
- `git remote -v`
- `dotnet restore AgentContextKit.sln`
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- doctor`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
- `powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1`
- `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`
- `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`
- `git diff --check`
- Run the prohibited maintainer identity term scan with `rg` and safe excludes.
- `git ls-files | rg -n ".(rar|zip|nupkg|snupkg|bak|tmp|binlog|trx)$|(^|/)(bin|obj|TestResults|coverage|publish|out)/"`

## Risks
- Final gates that require a tag can only pass after the final commit is tagged.
- Push and NuGet publish require external credentials and explicit maintainer control.

## Rollback
- For docs-only changes, revert the related commits.
- For an incorrect local tag, delete it only after explicit approval with `git tag -d v0.1.0-alpha.1`.
- Public push and NuGet publish rollback are outside this local preparation task and must be handled by the maintainer.

## Completion Notes
Completed for local handoff preparation.

- Final release handoff docs now include GitHub description/topics, local validation commands, tag commands, push commands, NuGet publish commands, and post-publish install checks.
- `docs/CODEX_FOR_OSS_APPLICATION.md` is ready for OSS support/application use.
- Build and tests passed before the implementation commit.
- Final post-commit validation and local tag creation are intentionally performed after the reviewed commit so the tag points at the final release commit.
- Push, tag push, NuGet publish, remote creation, force push, and secret handling remain outside this local preparation.
