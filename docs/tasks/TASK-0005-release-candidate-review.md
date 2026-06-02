# TASK-0005: v0.1.0-alpha.1 Release Candidate Review

## Purpose
Create a local release candidate review workflow for `0.1.0-alpha.1` without pushing, publishing, or creating remotes.

## Scope
- Add a safe PowerShell release verification script.
- Add a release candidate review report document.
- Run the verification script locally.
- Record known release blockers and manual actions.
- Keep all validation local and offline-first.

## Out of scope
- GitHub push.
- NuGet publish.
- Remote repository creation.
- Permanent global tool install.
- Tag creation.
- Deleting temporary files automatically.

## Affected files
- `scripts/verify-release.ps1`
- `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/RELEASE_CHECKLIST.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0005-release-candidate-review.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
None.

## Audit/security impact
The script must not publish, push, delete repository files, mutate source files, redact content, or create a permanent global tool installation. It may create temporary package/tool folders under the user temp directory.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
The release verification script runs restore, build, tests, scan, doctor, pack, temporary tool install, and installed tool smoke checks.

## OSS/release impact
Provides a repeatable local release candidate gate and a clear report for maintainers.

## Acceptance criteria
- `scripts/verify-release.ps1` exists and is safe/local-only.
- `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md` exists.
- The script passes locally.
- The RC report records command results, known blockers, and manual actions.
- No push, publish, remote creation, tag creation, deletion, or automatic redaction occurs.

## Implementation steps
1. Create `scripts/verify-release.ps1`.
2. Make the script fail fast on command errors.
3. Make the script create temp package/tool folders outside the repo.
4. Make the script run restore/build/test.
5. Make the script run `ackit scan` and `ackit doctor`.
6. Make the script run `dotnet pack`.
7. Make the script install the package into temp `--tool-path`.
8. Make the script run installed `ackit --help` and `ackit scan --json`.
9. Add `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md`.
10. Update release validation/checklist docs to mention the script.
11. Run the script.
12. Update task completion notes and handoff.
13. Commit the completed task.

## Test steps
1. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
2. `git status --short --branch`

## Risks
- PowerShell execution policy can block script execution on some machines; use explicit `-ExecutionPolicy Bypass` for local validation.
- The script creates temp folders and does not delete them automatically to avoid destructive behavior.
- `RepositoryUrl` and `PackageProjectUrl` remain TODO placeholders, so publish remains blocked.

## Rollback plan
Revert the TASK-0005 implementation commit. Temporary folders can be manually removed after confirming their paths are outside the repository.

## Completion notes
In progress.
