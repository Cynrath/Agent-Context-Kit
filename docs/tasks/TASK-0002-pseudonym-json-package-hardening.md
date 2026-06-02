# TASK-0002: Pseudonym, JSON Output, And Package Hardening

## Purpose
Continue the AgentContextKit MVP by replacing maintainer real-name metadata with the public pseudonym `Cynrath`, adding machine-readable JSON output for CI/script usage, and validating local NuGet tool packaging without publishing.

## Scope
- Replace real-name metadata with `Cynrath`.
- Keep license and package metadata OSS-friendly while avoiding real-name exposure.
- Add `--json` output for core reporting commands where practical.
- Keep default human CLI output unchanged.
- Add tests for JSON output behavior and package metadata expectations.
- Run local `dotnet pack` and install the package into a temporary `--tool-path` only.

## Out of scope
- GitHub push.
- NuGet publish.
- Remote repository creation.
- Permanent global tool installation.
- Automatic redaction.
- Web UI or LLM integration.

## Affected files
- `LICENSE`
- `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/UnitTest1.cs`
- `CHANGELOG.md`
- `README.md`
- `README.tr.md`
- `docs/PRODUCT_SPEC.md`
- `docs/ROADMAP.md`
- `docs/RELEASE_CHECKLIST.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0002-pseudonym-json-package-hardening.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
README and docs already use project-level wording. CLI localization remains en/tr. JSON field names use English stable machine-readable names.

## Audit/security impact
Replacing the real name with `Cynrath` reduces public personal information exposure. Local pack/install verification must not publish packages or install permanent global tools.

## Architecture impact
JSON output should be implemented in the CLI layer using structured DTOs or anonymous objects derived from Core models. Core business logic should remain console-agnostic.

## CLI impact
Add `--json` support for:
- `scan`
- `redact-check`
- `doctor`
- optionally `init`, `generate`, and `task` if straightforward.

## Testing impact
Add tests for package metadata pseudonym and JSON serialization expectations. Existing 10 tests must continue passing.

## OSS/release impact
Local package validation improves release confidence. `RepositoryUrl` can remain TODO until a real remote URL is intentionally chosen.

## Acceptance criteria
- No real-name maintainer text remains in tracked source/docs.
- `Cynrath` is used in license/package maintainer metadata.
- `ackit scan --json` emits valid JSON.
- `ackit doctor --json` emits valid JSON.
- `ackit redact-check --json` emits valid JSON and preserves exit code behavior.
- `dotnet restore`, `dotnet build -c Release`, and `dotnet test -c Release` pass.
- `dotnet pack` succeeds locally.
- Tool can be installed to a temporary `--tool-path` from the local package and `ackit --help` works.

## Test steps
1. Run a repository text search for real-name maintainer variants.
2. `dotnet restore AgentContextKit.sln`
3. `dotnet build AgentContextKit.sln -c Release --no-restore`
4. `dotnet test AgentContextKit.sln -c Release --no-build`
5. `dotnet run --project src/AgentContextKit.Cli -- scan --json`
6. `dotnet run --project src/AgentContextKit.Cli -- doctor --json`
7. `dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release -o <temp-nupkg>`
8. `dotnet tool install AgentContextKit --tool-path <temp-tools> --add-source <temp-nupkg> --version 0.1.0-alpha.1`
9. `<temp-tools>/ackit --help`

## Risks
- JSON output shape is early and may need stabilization before `1.0.0`.
- Temporary tool installation can fail if NuGet cache contains conflicting prerelease packages; use an isolated temp source and exact version.
- Local pack creates generated files under temp paths or ignored package folders only.

## Rollback plan
Revert the implementation commit for TASK-0002. If a temporary tool path is created during verification, remove it manually after confirming it is outside the repository.

## Completion notes
Completed.

- Replaced maintainer metadata and license copyright with `Cynrath`.
- Added `--json` output for `init`, `scan`, `generate`, `task`, `redact-check`, and `doctor`.
- Added CLI JSON and package metadata tests.
- Added package readme metadata so local `dotnet pack` is warning-free.
- Verified local `dotnet pack` and temporary `dotnet tool install --tool-path`.
- No push, publish, remote creation, permanent global install, deletion, or automatic redaction was performed.
