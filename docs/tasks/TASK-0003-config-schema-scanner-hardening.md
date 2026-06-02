# TASK-0003: Config Schema, JSON Schema, And Scanner Hardening

## Purpose
Harden the AgentContextKit MVP by documenting and implementing a clearer `.ackit/config.yml` schema, stabilizing JSON output metadata, and improving scanner behavior without adding heavy dependencies or remote services.

## Scope
- Add public docs for configuration and JSON output.
- Add `schemaVersion` and `toolVersion` to JSON command output.
- Add config support for:
  - `schemaVersion`
  - `defaultLanguage`
  - `brandKeywords`
  - `piiKeywords`
  - `ignorePaths`
  - `riskExtensions`
- Keep unknown config fields non-fatal.
- Keep unknown language fallback to English.
- Apply config-driven ignore paths during repository scanning and risk scanning.
- Apply config-driven risky extension findings.
- Add safer domain/email/IP-like PII signals with conservative severity.
- Add `--profile public-release` metadata to JSON output for `redact-check`.
- Update generated default `.ackit/config.yml`.
- Add tests for config parsing, ignore paths, custom risk extensions, JSON schema metadata, and redact-check profile output.
- Run restore/build/test and CLI checks.

## Out of scope
- Full YAML parser dependency.
- JSON Schema draft file validation.
- SARIF output.
- Automatic redaction.
- Deleting, moving, or rewriting repository files.
- GitHub push or NuGet publish.
- Web UI or LLM integration.

## Affected files
- `src/AgentContextKit.Core/Models.cs`
- `src/AgentContextKit.Core/Configuration.cs`
- `src/AgentContextKit.Core/Scanning.cs`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/UnitTest1.cs`
- `docs/CONFIGURATION.md`
- `docs/JSON_OUTPUT.md`
- `docs/SECURITY_MODEL.md`
- `docs/ARCHITECTURE.md`
- `README.md`
- `README.tr.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0003-config-schema-scanner-hardening.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
English/Turkish README command docs will mention config and JSON output. JSON field names stay English and stable.

## Audit/security impact
Config-driven ignore paths reduce accidental reporting of explicitly excluded generated or vendor paths. Custom risky extensions and PII/domain/IP detection improve public-release review coverage. Scanner remains report-only.

## Architecture impact
Core remains responsible for config, scanning, and risk models. CLI remains responsible for output shape, command options, and exit codes. No new runtime dependency should be added.

## CLI impact
- `ackit scan --json` includes `schemaVersion` and `toolVersion`.
- `ackit doctor --json` includes `schemaVersion` and `toolVersion`.
- `ackit redact-check --profile public-release --json` includes `profile`, `schemaVersion`, and `toolVersion`.
- Existing human-readable output remains compatible.

## Testing impact
Add tests for:
- Config reader parses `schemaVersion`.
- Config reader parses `ignorePaths`.
- Config reader parses `riskExtensions`.
- Repository scanner excludes configured ignore paths.
- Risk scanner flags configured risky extensions.
- Brand/PII scanner flags configured keywords.
- JSON output includes `schemaVersion` and `toolVersion`.
- Redact-check JSON includes requested profile and keeps critical exit code.

## OSS/release impact
This improves public release hygiene and CI integration without publishing anything. It also prepares the project for future JSON schema stabilization.

## Acceptance criteria
- `docs/CONFIGURATION.md` exists.
- `docs/JSON_OUTPUT.md` exists.
- Default `.ackit/config.yml` output includes schema, ignore path, and risky extension examples.
- `AckitConfig` stores schema version, ignore paths, and risk extensions.
- Config parser handles multiline YAML-style lists and inline lists for the new fields.
- Configured ignore paths are excluded from repository files and risk findings.
- Configured risky extensions produce risk findings.
- `scan --json`, `doctor --json`, and `redact-check --json` include schema metadata.
- All tests pass.
- `ackit scan` on this repository reports no risk findings.

## Implementation steps
1. Inspect current config, scanning, CLI JSON, and test files.
2. Add `docs/CONFIGURATION.md` and `docs/JSON_OUTPUT.md`.
3. Update README, architecture, security model, roadmap/changelog as needed.
4. Extend `AckitConfig` with schema version, ignore paths, and risk extensions.
5. Update `AckitConfigReader` to parse new fields safely.
6. Update `AckitConfigWriter` default config output.
7. Update repository scanner to combine built-in ignored directories with configured ignore path rules.
8. Update risk scanner to apply custom risky extensions.
9. Update brand/PII scanner with conservative email/domain/IP-like detection.
10. Update CLI JSON output with `schemaVersion`, `toolVersion`, and redact profile.
11. Add focused tests for all new behavior.
12. Run `dotnet restore AgentContextKit.sln`.
13. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
14. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
15. Run `ackit scan`, `ackit scan --json`, `ackit doctor --json`, and `ackit redact-check --json` against safe fixtures.
16. Update handoff and task completion notes.
17. Commit the completed task.

## Test steps
1. `dotnet restore AgentContextKit.sln`
2. `dotnet build AgentContextKit.sln -c Release --no-restore`
3. `dotnet test AgentContextKit.sln -c Release --no-build`
4. `dotnet run --project src/AgentContextKit.Cli -- scan`
5. `dotnet run --project src/AgentContextKit.Cli -- scan --json`
6. `dotnet run --project src/AgentContextKit.Cli -- doctor --json`
7. Run `redact-check --profile public-release --json` in a temporary fixture with a synthetic secret-like value.

## Risks
- Simple YAML parsing can misread complex YAML. MVP docs must state that config supports a limited schema.
- Domain/IP/phone heuristics can produce false positives.
- Ignore path rules can hide files from reports if configured too broadly.
- JSON schema metadata is early and may still change before `1.0.0`.

## Rollback plan
Revert the TASK-0003 implementation commit. No destructive file actions, remote pushes, or publishes are performed.

## Completion notes
In progress.
