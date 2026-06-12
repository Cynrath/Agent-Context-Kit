# TASK-0089 Config Diagnostics CLI Integration And Migration Contract

## Purpose
Expose the existing deterministic configuration validator through a read-only CLI command and freeze the pre-1.0 config compatibility/migration behavior.

## Scope
- Add `ackit config-check [--lang en|tr] [--json]`.
- Validate `.ackit/config.yml` without rewriting it.
- Return exit `1` when error diagnostics exist and exit `0` for valid/default/warning-only config.
- Treat a missing config file as valid default configuration and report that state explicitly.
- Emit sanitized human and JSON diagnostics with stable code, severity, line, optional key, and message.
- Document schema `1`, obsolete-key migration guidance, warning/error behavior, and no-auto-migration policy.
- Add CLI contract, JSON contract, help, exit-code, and privacy tests.

## Out Of Scope
- No config auto-fix, rewrite, migration command, YAML dependency, scan preflight failure, version bump, push, tag, release, or package publication.
- No change to existing config reader fallback behavior or other command exit codes.

## Affected Files
- `src/AgentContextKit.Cli/Program.cs`
- focused CLI tests under `tests/AgentContextKit.Tests/`
- `docs/CONFIGURATION.md`
- `docs/CONFIGURATION_DIAGNOSTICS.md`
- `docs/CLI_REFERENCE.md`
- `docs/CLI_CONTRACT.md`
- `docs/JSON_OUTPUT.md`
- `docs/EXIT_CODES.md`
- architecture/security/release/queue/Codex handoff docs.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
The command requires only local read access to `.ackit/config.yml`. It performs no remote or repository metadata writes.

## SEO/i18n Impact
Human labels support English/Turkish selection through the existing language option. Stable diagnostic codes, keys, and JSON field names remain English and language-independent.

## Audit/Security Impact
Diagnostics must never echo raw config values or full source lines. Critical suppression attempts remain errors. No auto-fix or implicit migration may modify security policy.

## Acceptance Criteria
- Help, CLI reference, contract, JSON, and exit-code docs include `config-check`.
- Missing config reports default status and exits `0`.
- Valid and warning-only config exit `0`; error diagnostics exit `1` in human and JSON modes.
- JSON uses schema `2` and includes deterministic sanitized summary/diagnostics.
- Obsolete `allowedFindingIds` produces migration guidance without mutation.
- Existing config reader and all other command behavior remain compatible.
- Full local validation and post-commit public gates pass.

## Tests
Add focused CLI tests, then run restore/build/test, source scan/doctor, JSON/SARIF parse, sample smoke, hygiene, CLI/config/readiness/evidence/release gates, local package verification, and post-commit public gates.

## Risks
- Users may interpret warning-only exit `0` as approval without reviewing migration guidance.
- CLI and Core diagnostic contracts can drift if DTO rendering is duplicated.
- Accidentally including raw values would create a privacy regression.

## Rollback Plan
Revert the TASK-0089 implementation commit. Existing config reading and validation Core APIs remain independently usable.

## Completion Notes
- Task created after TASK-0088 commit `b560214` and successful post-commit public gate.
- Added read-only `ackit config-check` with schema `2` JSON, sanitized diagnostics, stable status/summary fields, and English/Turkish human labels.
- Missing config reports valid defaults and exits `0`; warning-only config exits `0`; Error diagnostics exit `1` in human and JSON modes.
- Obsolete `allowedFindingIds` reports migration-required guidance without rewriting the file.
- Added five focused CLI/help/localization/privacy tests; full suite passed 169/169.
- Restore/build, clean scan, doctor PASS, config/scan JSON parse, SARIF parse, sample smoke, hygiene, CLI/config/v0.2/v1.0/evidence gates, and local package verification passed.
- The pre-commit public release gate failed only on the expected dirty working tree; rerun it after commit.
