# TASK-0006: Documentation Completion And Agent Context Pack

## Purpose
Complete the documentation surface for the v0.1.0-alpha.1 candidate and create the missing generated agent/context workflow files required by the original project scope.

## Scope
- Add missing user-facing and maintainer-facing documentation.
- Add missing AI agent instruction files.
- Add missing generated workflow/context files.
- Update README files with a complete documentation index.
- Keep all generated writes safe and non-destructive.
- Run verification after documentation changes.

## Out of scope
- GitHub push.
- NuGet publish.
- Remote repository creation.
- Tag creation.
- Automatic redaction.
- Deleting or overwriting existing files.

## Affected files
- `docs/CLI_REFERENCE.md`
- `docs/EXAMPLES.md`
- `docs/TROUBLESHOOTING.md`
- `docs/FAQ.md`
- `docs/SUPPORT.md`
- `docs/PRIVACY.md`
- `docs/MAINTAINERS.md`
- `docs/GOVERNANCE.md`
- `docs/PROJECT_MAP.md`
- `docs/AI_WORKFLOW.md`
- `docs/SECURITY_NOTES.md`
- `docs/tasks/TASK-0001.md`
- `CLAUDE.md`
- `.cursor/rules/project.mdc`
- `.github/copilot-instructions.md`
- `.codex/HANDOFF.md`
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0006-documentation-completion.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
README and README.tr gain a documentation index. New deeper docs are primarily English for OSS readiness; Turkish README links to them.

## Audit/security impact
Security notes and privacy docs reinforce offline-first behavior, no upload, and report-only redaction. No sensitive paths or secrets should be written into docs.

## Architecture impact
No runtime architecture change.

## CLI impact
No command behavior change.

## Testing impact
Run restore/build/test/scan/doctor and release verification script after docs are added.

## OSS/release impact
Improves project trust, contributor onboarding, support expectations, governance clarity, and AI-agent consistency.

## Acceptance criteria
- All listed missing docs exist.
- All generated agent/context files exist.
- README and README.tr include a documentation index.
- `ackit scan` reports no risk findings.
- `ackit doctor` reports all checks PASS.
- `scripts/verify-release.ps1` passes.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Add user docs: CLI reference, examples, troubleshooting, FAQ.
4. Add OSS maintainer docs: support, privacy, maintainers, governance.
5. Run `ackit generate --target all --lang en` or manually create any missing generated files if safe generation skips too much.
6. Inspect generated files for public-release hygiene.
7. Update README and README.tr documentation index.
8. Update changelog, handoff, and next steps.
9. Run `dotnet restore AgentContextKit.sln`.
10. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
11. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
12. Run `dotnet run --project src/AgentContextKit.Cli -- scan`.
13. Run `dotnet run --project src/AgentContextKit.Cli -- doctor`.
14. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
15. Update completion notes.
16. Commit implementation.
17. Continue with the next task if more release gaps remain.

## Test steps
1. `dotnet restore AgentContextKit.sln`
2. `dotnet build AgentContextKit.sln -c Release --no-restore`
3. `dotnet test AgentContextKit.sln -c Release --no-build`
4. `dotnet run --project src/AgentContextKit.Cli -- scan`
5. `dotnet run --project src/AgentContextKit.Cli -- doctor`
6. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- Generated docs may include local paths if produced from scan output; inspect before commit.
- More docs can become stale if not maintained.
- Governance/support docs are early and should be revisited after public launch.

## Rollback plan
Revert the TASK-0006 implementation commit. Do not delete files manually unless explicitly requested.

## Completion notes
In progress.
