# TASK-0094 English/Turkish Localization Parity Release Gate

## Purpose
Turn the existing English/Turkish CLI support into a release-gated contract: human-readable command surfaces must have deliberate localized labels and errors, while JSON fields, schema values, technical identifiers, exit codes, and security behavior remain language-independent.

## Scope
- Inventory every CLI command that accepts `--lang en|tr` and every user-facing help, label, status, summary, and argument-error path.
- Expand the central text provider for shared English/Turkish CLI labels without moving Core business logic into the CLI.
- Localize the human-readable command chrome for help, init, scan, baseline, SARIF/report/Web UI, prompt-pack/context-export, task errors, doctor, and unknown-command handling.
- Keep command signatures, rule IDs, diagnostic codes, stack names, severity enum names, file paths, JSON property names, JSON enum/status values, and exit codes stable.
- Add focused command-matrix tests for human output/exit parity, Turkish UTF-8 markers, invalid invocation parity, and JSON semantic invariance.
- Add `scripts/check-localization-parity.ps1` and integrate it into release-candidate evidence and readiness/documentation gates.
- Update localization, CLI/JSON/exit, troubleshooting/tutorial, release/readiness, roadmap/queue/changelog, and Codex handoff docs.

## Out Of Scope
- No new language, resource-file framework, runtime dependency, generated template rewrite, package version change, workflow dispatch, push, tag, release, or NuGet publish.
- Core diagnostic/finding message payloads are not translated in this task; their stable codes and security meaning remain authoritative.
- JSON and SARIF payloads do not gain localized fields or values.

## Affected Files
- `src/AgentContextKit.Core/Templates.cs`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/LocalizationParityTests.cs`
- `scripts/check-localization-parity.ps1`
- `docs/LOCALIZATION.md`
- `docs/CLI_CONTRACT.md`
- `docs/JSON_OUTPUT.md`
- `docs/EXIT_CODES.md`
- `docs/TROUBLESHOOTING.md`
- `docs/FIRST_FIVE_MINUTES.md`
- Release/readiness/roadmap/queue/changelog/Codex docs.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. The gate is local and uses disposable repositories only.

## SEO/i18n Impact
English remains the default and Turkish remains opt-in through `--lang tr` or config. Turkish human output uses natural UTF-8 text; machine-readable field names and values remain stable English contract tokens.

## Audit/Security Impact
Localization must not hide Critical/High findings, alter exit decisions, translate stable rule/diagnostic codes, expose raw secret values, or add remote calls. Error parity tests use sanitized fixtures.

## Acceptance Criteria
- Every command advertising `--lang en|tr` is represented in the parity test matrix.
- English and Turkish runs return the same exit code for equivalent repositories and arguments.
- Turkish help, shared labels, summaries, and known argument errors use deliberate UTF-8 localized text.
- JSON command outputs are semantically equivalent after removing documented volatile repository/time fields.
- JSON schema version, command names, status tokens, rule IDs, diagnostic codes, and exit codes do not change with language.
- Unknown languages continue to fall back to English.
- Full local gates and post-commit public release gates pass.

## Tests
Run focused localization tests/gate, restore/build/full tests, English/Turkish help and command smoke, config-check/scan/doctor, JSON/SARIF parse, sample smoke, dependency review, contract/readiness/RC/release gates, hygiene scans, diff checks, and post-commit public release gates.

## Risks
- Translating technical contract tokens could break scripts or documentation.
- Running stateful commands against the same fixture can create false parity differences; each language must use an equivalent disposable repository.
- Broad template translation changes would enlarge the review surface and are intentionally deferred.

## Rollback Plan
Revert the TASK-0094 commit. No package version, schema version, workflow, generated public asset, or remote state is changed.

## Completion Notes
- Task created after TASK-0093 commit `b74b847` and successful post-commit public release gate.
- Expanded `ITextProvider` and CLI composition for localized help, repository labels, yes/no values, baseline/SARIF/report summaries, local-only provider notices, suppression labels, and known argument errors.
- Kept command/option names, stack/severity names, paths, Core finding/diagnostic messages, JSON fields/status values, rule/diagnostic IDs, schema versions, and exit decisions language-independent.
- Added five focused tests covering help signatures, all 13 language-aware JSON commands, human marker/exit parity, known argument errors, baseline/suppression output, and semantic JSON equality after documented volatile fields are removed.
- Added `scripts/check-localization-parity.ps1` with ASCII-safe PowerShell markers, Turkish help smoke, Turkish JSON parse, and focused test execution; integrated it into RC evidence and v1.0 documentation/readiness gates.
- Updated localization, architecture, CLI/JSON/exit, troubleshooting/tutorial, release evidence/freeze/decision/checklist/validation, gap, roadmap/index/map/changelog, queue, and Codex handoff docs.
- Full validation passed: zero-warning Release build, 178/178 tests, clean Turkish scan/doctor/SARIF smoke, JSON/SARIF parse, sample smoke, clean vulnerability/deprecation reviews, all contract/readiness/RC/release gates, and clean hygiene scans. The 2,000-file benchmark completed in 3.171 seconds under the 30-second tripwire.
- No package/schema version, generated public asset, workflow dispatch, push, tag, release, publication, remote setting, or supply-chain decision was changed.
