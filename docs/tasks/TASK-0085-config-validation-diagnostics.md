# TASK-0085 Config Validation And Diagnostics

## Purpose
Add deterministic configuration diagnostics so invalid, unknown, obsolete, duplicate, and unsafe `.ackit/config.yml` settings can be reviewed before future baseline/CI policy integration.

## Scope
- Define Core diagnostic severity, code, line, key, and message models.
- Add a validator that inspects config text without remote access or mutation.
- Detect unknown top-level keys, obsolete aliases, duplicate scalar/section keys, invalid schema/language values, malformed list items, and unsafe path/domain/finding-ID values.
- Keep current config reading behavior backward-compatible in this task.
- Add focused tests and normative configuration diagnostics documentation.
- Prepare a future CLI integration boundary without adding a new command or changing current exit codes.

## Out Of Scope
- No `ackit config validate` command, scan preflight failure, config auto-fix, file rewrite, baseline config field, or migration command.
- No YAML dependency or broad parser replacement.
- No version bump, push, tag, GitHub Release change, NuGet publish, or remote write.

## Affected Files
- `src/AgentContextKit.Core/Configuration.cs` or a focused config validation file.
- `src/AgentContextKit.Core/Models.cs` or focused diagnostic models.
- `tests/AgentContextKit.Tests/` focused tests.
- `docs/CONFIGURATION_DIAGNOSTICS.md`
- `docs/CONFIGURATION.md`
- `docs/ARCHITECTURE.md`
- `docs/SECURITY_MODEL.md`
- `docs/V030_ROADMAP_DECISION.md`
- `docs/V100_GAP_ANALYSIS.md`
- queue/index/map/changelog/Codex handoff files.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. Validation is local, read-only, and deterministic.

## SEO/i18n Impact
Diagnostic codes and config keys remain English and language-independent. Future CLI message localization is separate; this task keeps invariant Core messages for contract testing.

## Audit/Security Impact
Validation must reject unsafe traversal/absolute ignore paths, malformed domains, and Critical rule suppression attempts while never echoing secret values or full config lines.

## Acceptance Criteria
- Diagnostics have stable codes, severity, line number, optional key, and sanitized message.
- Unknown and obsolete keys are distinguishable.
- Duplicate keys/sections are deterministic.
- Schema/language/list/domain/path/finding-ID validation is covered.
- Critical finding-ID suppression is reported as an error.
- Current config reader output and current CLI behavior remain unchanged.
- Full local validation passes.

## Tests
Add focused xUnit coverage, then run restore/build/test, scan/doctor, JSON/SARIF/sample smoke, hygiene scans, contract/config/readiness/release gates, and public release gate checks.

## Risks
- Validator and reader grammar can drift if parsing rules are duplicated carelessly.
- Turning warnings into runtime failures too early would break existing repositories.
- Diagnostic messages could leak config content if they include raw values.

## Rollback
Revert the TASK-0085 commit. Existing config parsing and CLI behavior remain unchanged.

## Completion Notes
- Task created after TASK-0084 baseline foundation.
- Added `IAckitConfigValidator`, stable diagnostic models/codes, and a report-only `AckitConfigValidator` for the existing small YAML-like grammar.
- Diagnostics cover unknown/obsolete/duplicate keys, invalid schema/language, malformed/orphan lists, unsafe paths/domains/extensions/finding IDs, Critical suppression attempts, broad ignore paths, and duplicate list values.
- Diagnostic messages omit raw values and full config lines; line/key metadata remains deterministic.
- Kept `AckitConfigReader` and all current CLI/JSON/exit behavior unchanged.
- Added 8 focused validator tests and configuration diagnostics, architecture, security, roadmap, 1.0 gap, queue, index, map, changelog, and Codex docs.
- Validation passed: restore, Release build with 0 warnings/errors, 144/144 tests, clean scan, doctor PASS, JSON/SARIF parse, sample smoke, hygiene scans, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; rerun it after commit.
- The post-commit public release gate passed with no blocking items; only the expected post-release HEAD warning and manual remote-tag verification note remain.
