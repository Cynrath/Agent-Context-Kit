# v0.2.0-alpha.2 Scope

Status: release candidate prepared locally; hosted exact-commit validation and publication remain pending.

## Release Intent
`v0.2.0-alpha.2` should be a small compatibility-preserving hardening release after the published `v0.2.0-alpha.1` package. It should package the completed scanner precision and suppression-audit work without adding a new command or changing established schemas.

Current state remains:

- Published release: `v0.2.0-alpha.1`.
- Source/package/CLI version: `0.2.0-alpha.2`.
- Published-package smoke pin: `0.2.0-alpha.1`.
- Source-package smoke pin: `0.2.0-alpha.2`.
- JSON schema version: `2`.
- Config schema version: `1`.

The alpha.2 release-preparation task changes only candidate metadata and source-package validation. Public install commands and published-package smoke remain on alpha.1 until NuGet publication is verified.

## Package-Affecting Scope

### Scanner Precision Hardening
- Use culture-invariant semantics for case-insensitive ASCII token, email, domain, and local-path regexes.
- Keep scanner behavior stable under Turkish and other process cultures.
- Treat Shields.io badge hosts and common `System.IO` namespace-shaped tokens as narrow safe technical noise.
- Include expanded table-driven regression coverage for secret, artifact, local-path, PII/brand, rule-ID, and Critical suppression boundaries.

### Sanitized Suppression Audit
- Add local human/JSON audit records for config-driven `safeDomains`, `ignoredPaths`, and `ignoredFindingIds` suppressions.
- Include rule ID, severity, category, repository-relative path, and suppression reason.
- Omit raw match values and finding messages from suppression records.
- Keep Critical findings visible and exit-code relevant.
- Keep SARIF limited to visible findings; suppression audit records are not exported to SARIF.

## Contract Hardening Included In Validation
- JSON schema-v2 envelope and scanner finding contract tests.
- CLI success, invalid invocation, and human/JSON exit-code parity tests.
- Scanner fixture matrices for detection and known-noise boundaries.

These tests protect the release but do not create new public commands.

## Repository Documentation Scope
The release branch can also carry the completed repository-only documentation work:

- README installed-tool/source command polish.
- Safe screenshot capture plan without committed screenshots.
- GitHub Pages decision with Pages deferred.
- First-five-minutes tutorial.
- Existing-repository AI agent preparation tutorial.

These docs do not justify a breaking version change and do not activate remote services.

## Compatibility Matrix
| Surface | Alpha.2 Decision |
| --- | --- |
| CLI commands | No command added or removed. |
| CLI exit codes | Keep documented `0`/`1`/`2` behavior. |
| JSON schema | Keep `schemaVersion: 2`; suppression fields are additive. |
| Config schema | Keep `schemaVersion: 1`; no new config key required. |
| SARIF | Keep SARIF 2.1.0 visible-findings-only behavior. |
| Generated files | Keep skip-existing and repository-relative output rules. |
| Offline/privacy boundary | No upload, telemetry, remote AI call, or automatic redaction. |
| Package ID/tool command | Keep `AgentContextKit` / `ackit`. |
| Runtime | Keep .NET 10. |

Consumers should tolerate additive JSON properties. Any breaking schema or command change moves to a later version and requires a separate decision.

## Explicitly Out Of Scope
- New scanner rule families beyond the completed hardening set.
- Breaking JSON/config changes or schema version increment.
- New CLI command or renamed option.
- GitHub Code Scanning/SARIF upload activation.
- CodeQL activation.
- GitHub Pages activation or docs generator dependency.
- Screenshot/GIF/video asset capture.
- Remote LLM integration or provider/API-key handling.
- Automatic secret redaction, file deletion, or credential rotation.
- `v0.3` product direction or `1.0` readiness decisions.

## Release Preparation Gate
TASK-0123 must:

1. Change csproj and CLI runtime version to `0.2.0-alpha.2`.
2. Update source-package smoke to install the local `0.2.0-alpha.2` package.
3. Keep published-package smoke on `0.2.0-alpha.1` until NuGet alpha.2 publication succeeds.
4. Move the Unreleased changelog entries into an `0.2.0-alpha.2` release-candidate section and date it after publication.
5. Run restore, Release build, 127+ tests, scan, doctor, JSON/SARIF parse, sample smoke, hygiene, and release gates.
6. Pack and install the candidate from a temporary local package source on a temporary tool path.
7. Verify `ackit version`, help, scan, suppression audit behavior, SARIF, and fake-secret Critical exit behavior.
8. Obtain successful hosted `ci`, published-package smoke, and cross-platform source-package smoke for the exact pushed commit.

## Authorized OIDC Publication Gate
After local preparation and hosted validation:

1. Dispatch the exact-SHA manual release workflow.
2. Publish the NuGet package through GitHub OIDC Trusted Publishing only.
3. Create the exact-commit tag and GitHub pre-release only after NuGet availability is verified.
4. Verify global install and smoke commands from NuGet.
5. Update published-package smoke, README install commands, release docs, and agent instructions to alpha.2.

PROJECT-CONTROL-0102 explicitly authorizes these remote writes after all required gates pass. API keys and persistent package credentials are prohibited.

## Rollback
Before publication, revert the dedicated version/release-preparation commit and keep `0.2.0-alpha.1` as the published baseline. After publication, do not overwrite an immutable NuGet version; prepare a follow-up version if a package defect is found.
