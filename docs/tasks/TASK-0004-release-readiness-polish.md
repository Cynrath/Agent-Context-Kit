# TASK-0004: Release Readiness Polish

## Purpose
Improve v0.1.0-alpha.1 release readiness by documenting package validation, tightening NuGet metadata, reducing Git line-ending noise, and recording the `.slnx` decision without publishing or pushing.

## Scope
- Add package/release validation documentation.
- Add `.gitattributes` for consistent repository text handling.
- Review NuGet metadata against official package authoring guidance.
- Add package release notes, copyright, repository type, and project URL metadata.
- Keep `RepositoryUrl` and `PackageProjectUrl` as TODO placeholders until the real public remote is intentionally selected.
- Document that `AgentContextKit.sln` is the primary solution and `AgentContextKit.slnx` is kept as a synced .NET 10 solution artifact for now.
- Add tests for package metadata fields.
- Run restore/build/test/pack and temporary tool-path install verification.

## Out of scope
- GitHub push.
- NuGet publish.
- Remote repository creation.
- Permanent global tool install.
- Adding package icon assets.
- Deleting `AgentContextKit.slnx`.

## Affected files
- `.gitattributes`
- `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`
- `tests/AgentContextKit.Tests/UnitTest1.cs`
- `docs/PACKAGING.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/DECISIONS.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/THIRD_PARTY_NOTICES.md`
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0004-release-readiness-polish.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
README files mention package validation docs. CLI output is unchanged.

## Audit/security impact
No publish or push is performed. Local package validation uses temporary paths only. `.gitattributes` reduces accidental line-ending churn in future commits.

## Architecture impact
No runtime architecture change. Package metadata changes affect NuGet packaging only.

## CLI impact
No command behavior change expected.

## Testing impact
Add tests that verify package metadata fields exist and still use `Cynrath`.

## OSS/release impact
Improves trust signals for package consumers and release reviewers. Keeps release blocked on explicit manual remote/publish decisions.

## Acceptance criteria
- `.gitattributes` exists.
- `docs/PACKAGING.md` exists.
- `docs/RELEASE_VALIDATION.md` exists.
- NuGet metadata includes package release notes, repository type, package project URL, copyright, README, license, tags, and tool command name.
- Tests pass.
- `dotnet pack` succeeds without warnings.
- Temporary `dotnet tool install --tool-path` succeeds from local package.
- Installed temporary `ackit --help` works.
- No push or publish occurs.

## Implementation steps
1. Add `.gitattributes` with LF normalization for source/docs/config files.
2. Add `docs/PACKAGING.md`.
3. Add `docs/RELEASE_VALIDATION.md`.
4. Update `docs/DECISIONS.md` with the `.slnx` decision.
5. Update release checklist and README references.
6. Update CLI `.csproj` package metadata.
7. Extend metadata tests.
8. Run `dotnet restore AgentContextKit.sln`.
9. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
10. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
11. Run local `dotnet pack` into a temp folder.
12. Install the package into a temp `--tool-path`.
13. Run installed `ackit --help`.
14. Update handoff/task completion notes.
15. Commit the completed task.

## Test steps
1. `dotnet restore AgentContextKit.sln`
2. `dotnet build AgentContextKit.sln -c Release --no-restore`
3. `dotnet test AgentContextKit.sln -c Release --no-build`
4. `dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o <temp-nupkg>`
5. `dotnet tool install AgentContextKit --tool-path <temp-tools> --add-source <temp-nupkg> --version 0.1.0-alpha.1 --ignore-failed-sources`
6. `<temp-tools>/ackit --help`

## Risks
- Placeholder URLs must not be treated as release-ready for NuGet publish.
- `.gitattributes` may alter line-ending behavior in future checkouts.
- Keeping both `.sln` and `.slnx` may confuse some contributors; docs must mark `.sln` as primary.

## Rollback plan
Revert the TASK-0004 implementation commit. Remove only temporary package/tool folders outside the repository if needed.

## Completion notes
Completed.

- Added `.gitattributes` for repository line-ending consistency.
- Added `docs/PACKAGING.md` and `docs/RELEASE_VALIDATION.md`.
- Documented the `.sln` primary / `.slnx` preserved-secondary decision in `docs/DECISIONS.md`.
- Added NuGet metadata for copyright, release notes, repository type, package project URL, and license acceptance.
- Extended metadata tests.
- Verified restore/build/test.
- Verified local pack into a temporary folder.
- Verified temporary `dotnet tool install --tool-path`.
- Verified installed temporary `ackit --help`.
- Verified `ackit scan` and `ackit doctor` report clean status.
- No push, NuGet publish, remote creation, permanent global tool install, deletion, or automatic redaction was performed.
