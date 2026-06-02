# TASK-0010: Maintainer Release Handoff

## Purpose
Document the exact maintainer-only release handoff steps that remain after local validation, without performing push, tag, publish, remote creation, or package URL replacement automatically.

## Scope
- Add a maintainer release handoff document.
- Link it from release and documentation indexes.
- Update README files, changelog, handoff, and next steps.
- Run lightweight documentation validation after changes.

## Out of scope
- GitHub push.
- NuGet publish.
- Remote repository creation.
- Release tag creation.
- Replacing TODO package URLs.
- Running destructive commands.
- Automatic redaction.

## Affected files
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/RELEASE_BLOCKERS.md`
- `docs/PUBLIC_RELEASE_AUDIT.md`
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0010-maintainer-release-handoff.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
README and README.tr gain a link to the maintainer release handoff. Handoff doc is English to match release docs.

## Audit/security impact
Reduces release risk by separating maintainer-only actions from automated/local agent actions and keeping publish/tag steps explicit.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run `ackit scan`, audit report-only mode, blocker report-only mode, and `git diff --check` after docs change.

## OSS/release impact
Provides the final handoff for maintainers to complete public release safely.

## Acceptance criteria
- `docs/MAINTAINER_RELEASE_HANDOFF.md` exists.
- Release docs link to the handoff.
- README and README.tr link to the handoff.
- Handoff clearly marks push/tag/publish/URL changes as maintainer-only.
- `ackit scan` reports no risk findings.
- Audit and blocker scripts still report the known maintainer-only blockers.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add `docs/MAINTAINER_RELEASE_HANDOFF.md`.
4. Update release docs, README files, changelog, handoff, and next steps.
5. Run `git diff --check`.
6. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
7. Run `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`.
8. Run `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`.
9. Update completion notes.
10. Commit implementation.

## Test steps
1. `git diff --check`
2. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
3. `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`
4. `powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1`

## Risks
- Handoff commands use placeholders and must not be copied without replacing values intentionally.
- NuGet/GitHub permissions are outside this local repository.
- Public package rendering still needs maintainer review.

## Rollback plan
Revert the TASK-0010 implementation commit. Do not run destructive git operations.

## Completion notes
Pending.
