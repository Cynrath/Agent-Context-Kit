# TASK-0081 v0.2.0-alpha.2 Scope Planning

## Purpose
Freeze a small, compatible `v0.2.0-alpha.2` scope from the hardening work completed after the published `v0.2.0-alpha.1` package.

## Scope
- Separate package-affecting scanner/audit changes from repository-only documentation work.
- Define compatibility boundaries for CLI commands, exit codes, JSON schema, config schema, and SARIF.
- Record explicit out-of-scope items and release acceptance gates.
- Prepare release checklist, readiness, roadmap, OSS status, maintainer handoff, queue, and Codex handoff docs without changing version metadata.

## Out Of Scope
- No source/package/CLI version bump.
- No tag, push, GitHub Release, NuGet publish, global tool update, or published-package workflow change.
- No new CLI command, breaking JSON/config change, Code Scanning activation, Pages activation, screenshot capture, or remote repository setting change.

## Affected Files
- `docs/V020_ALPHA2_SCOPE.md`
- `CHANGELOG.md`
- `docs/V020_READINESS.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/OSS_READINESS.md`
- `docs/ROADMAP.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. Future release publication remains maintainer-only.

## SEO/i18n Impact
No new localized CLI behavior is planned. Release docs must keep English/Turkish README version references aligned when a later release task changes the version.

## Audit/Security Impact
The planned package preserves Critical finding visibility, adds sanitized local suppression audit output, avoids raw matches in audit/SARIF, and keeps allowlists limited to non-Critical findings.

## Acceptance Criteria
- Alpha.2 has a concise package scope, repository-doc scope, compatibility matrix, exclusions, and release gates.
- Version metadata remains `0.2.0-alpha.1` in this task.
- Published-package smoke remains pinned to the published alpha.1 package until alpha.2 is actually published.
- A separate future release-preparation task is required for bump/pack/tag/publish steps.
- Documentation, build/test, hygiene, and release gates pass.

## Tests
Run version/workflow pin checks, build/test/scan/doctor, JSON/SARIF/sample smoke, hygiene scans, `git diff --check`, and release gates.

## Risks
- Scope creep could turn a patch alpha into a breaking feature release.
- Updating published-package smoke before NuGet publication would make CI fail.
- Additive JSON fields can still break consumers that incorrectly reject unknown properties.

## Rollback
Revert the TASK-0081 planning commit. No runtime, version, workflow package pin, or remote release state is changed.

## Completion Notes
- Task created after TASK-0080.
- Added `docs/V020_ALPHA2_SCOPE.md` with package-affecting scanner/audit work, repository-only docs, compatibility matrix, exclusions, release preparation gates, publication gates, and rollback.
- Confirmed csproj, CLI runtime, source-package smoke, and published-package smoke remain pinned to `0.2.0-alpha.1`; no version or workflow package pin changed.
- Updated changelog, v0.2 readiness, release checklist, maintainer handoff, OSS readiness, roadmap, documentation index, project map, queue, and Codex handoff files.
- Validation passed: restore, Release build with 0 warnings/errors, 127/127 tests, clean `scan --ci`, doctor PASS, JSON parse, SARIF parse, sample smoke, version-pin checks, hygiene scans, diff check, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; the post-commit rerun passed with no blocking items.
