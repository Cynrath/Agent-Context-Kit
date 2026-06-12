# TASK-0093 Machine-Readable JSON Schema And Golden Contract Assets

## Purpose
Close the local machine-readable contract gap for JSON command output, baseline manifests, and the AgentContextKit SARIF profile without adding runtime dependencies or changing the current output contract.

## Scope
- Add versioned Draft 2020-12 JSON Schema assets for command output schema `2`, baseline schema `1`, and the privacy-focused SARIF `2.1.0` profile.
- Add sanitized golden fixtures that cover every JSON-capable CLI command plus baseline and SARIF representative outputs.
- Add focused tests that parse all assets, verify catalog coverage, and compare live CLI top-level required fields with the schema contract.
- Add a local contract-asset gate and integrate it into release validation/readiness evidence.
- Update JSON/SARIF/baseline/CLI contract docs, documentation indexes, readiness gaps, roadmap/queue/changelog, and Codex handoff files.

## Out Of Scope
- No runtime JSON Schema validation dependency, generated schema download, SARIF upload, breaking output change, schema version increment, package version change, push, tag, release, or NuGet publish.
- No claim that a local profile replaces the official complete SARIF specification.
- No raw secret, absolute local path, user name, machine name, or real PII in fixtures.

## Affected Files
- `docs/schemas/README.md`
- `docs/schemas/ackit-command-output-v2.schema.json`
- `docs/schemas/ackit-baseline-v1.schema.json`
- `docs/schemas/ackit-sarif-profile-v1.schema.json`
- `tests/fixtures/contracts/command-output-v2-golden.json`
- `tests/fixtures/contracts/baseline-v1-golden.json`
- `tests/fixtures/contracts/sarif-profile-v1-golden.json`
- `tests/AgentContextKit.Tests/JsonContractAssetTests.cs`
- `src/AgentContextKit.Core/Scanning.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `scripts/check-json-contract-assets.ps1`
- JSON/SARIF/baseline/CLI/release/readiness/roadmap/queue/changelog/Codex docs.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. Validation is local and read-only apart from disposable test/build output.

## SEO/i18n Impact
JSON field names remain English and language-independent. Golden fixtures are sanitized machine contracts, not localized examples.

## Audit/Security Impact
Schemas and fixtures must preserve repository-relative paths, no raw secret values, no absolute local paths, Critical visibility, additive field compatibility, and sanitized baseline/SARIF behavior.

## Acceptance Criteria
- Every JSON-capable command appears in the machine-readable schema conditional catalog and golden fixture catalog.
- Schema/fixture files parse as JSON and use explicit version identifiers.
- Live CLI JSON outputs contain the schema-required envelope and command-specific top-level fields.
- Baseline and SARIF golden fixtures satisfy the local profile requirements and contain no private values.
- Existing JSON schema `2`, baseline schema `1`, SARIF `2.1.0`, CLI behavior, and prior test behavior remain compatible.
- Full local gates and post-commit public release gates pass.

## Tests
Run focused contract-asset tests/gate, restore/build/full tests, config-check/scan/doctor, JSON/SARIF parse, sample smoke, dependency review, all contract/readiness/RC/release gates, hygiene scans, diff checks, and post-commit public release gates.

## Risks
- A permissive schema may overstate validation strength; required fields and additive-property policy must be explicit.
- A schema stricter than current output could create false incompatibility failures.
- Golden fixtures can drift unless live command coverage is tied to the same command catalog.

## Rollback Plan
Revert the TASK-0093 commit. No runtime behavior, package version, workflow, or remote state changes are made.

## Completion Notes
- Task created after TASK-0092 commit `99860a8` and successful post-commit public release gate.
- Added versioned Draft 2020-12 schemas for all 13 JSON-capable commands, baseline schema `1`, and the local privacy-focused SARIF `2.1.0` profile.
- Added sanitized golden command, baseline, and SARIF fixtures plus four focused live-output/asset tests and `scripts/check-json-contract-assets.ps1`.
- Integrated the asset gate into release-candidate evidence, historical v1.0 readiness, documentation/release gates, and the normative contract/release docs.
- Added the exact standard domain `json-schema.org` to the built-in safe technical domain list so required schema URIs do not create self-scan noise; focused scanner/contract tests pass 5/5 without a broad suffix exemption.
- Full validation passed: zero-warning Release build, 173/173 tests, clean scan, doctor PASS, JSON/SARIF parse, sample smoke, clean vulnerability/deprecation reviews, all contract/readiness/RC/release gates, and clean hygiene scans. The 2,000-file benchmark completed in 3.72 seconds under the 30-second tripwire.
- No runtime output contract, schema version, package version, workflow, remote state, tag, release, or publication was changed.
