# Upgrade Compatibility

## Supported Predecessor
The first release-candidate upgrade target is the published NuGet package `AgentContextKit` `0.2.0-alpha.1`. Older pre-releases remain historical references, not direct compatibility targets.

## Compatibility Expectations
- Package ID remains `AgentContextKit`; tool command remains `ackit`.
- Config schema remains `1` for the current source line.
- JSON output schema remains `2`; additive fields are allowed and consumers must ignore unknown fields.
- SARIF remains `2.1.0`; additive result properties are allowed.
- Existing generated files are skipped by default and are not overwritten during upgrade.
- Default `scan` and `scan --ci` exit behavior is unchanged unless `--baseline` is explicit.

## Config Evidence
`tests/fixtures/upgrade/v0.2.0-alpha.1-config.yml` represents the published predecessor config surface. `ReleaseCandidateEvidenceTests` verifies that the current reader preserves its language, keywords, ignore paths, extensions, safe domains, ignored paths, and ignored finding IDs without validation errors.

Unknown or unsafe config diagnostics exist in Core but are not yet a CLI migration command. Config CLI validation remains a release-candidate gap until its invocation and migration behavior are explicitly frozen.

## Baseline Evidence
Baseline files were introduced after `0.2.0-alpha.1`; there is no predecessor baseline to migrate. `tests/fixtures/upgrade/baseline-schema-v1.json` is the first golden baseline fixture and verifies schema/algorithm/fingerprint integrity for future upgrades.

Changing baseline schema or fingerprint algorithm requires a new version identifier and migration guidance. Existing fingerprints must not be silently reinterpreted.

## Upgrade Procedure
1. Record the installed version with `ackit version`.
2. Keep `.ackit/config.yml` and any committed `.ackit-baseline.json` under version control or backup.
3. Install the candidate into a temporary tool path before changing the global install.
4. Run `ackit scan`, `ackit scan --json`, `ackit doctor`, and baseline-aware commands when applicable.
5. Review generated-file skip behavior and JSON/SARIF additive fields.
6. Only then update the global tool.

## Rollback
Uninstall the candidate and reinstall the supported predecessor:

```powershell
dotnet tool uninstall --global AgentContextKit
dotnet tool install --global AgentContextKit --version 0.2.0-alpha.1
```

Restore reviewed config/baseline files if a future migration task changed them. Current commands do not rewrite config or baseline implicitly.

## Remaining Evidence
- Hosted Windows/Ubuntu/macOS upgrade smoke from `0.2.0-alpha.1` to the selected release candidate.
- CLI config validation/migration contract.
- English/Turkish upgrade and error-output parity.
