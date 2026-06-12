# v0.3 Roadmap Decision

## Decision
The next v0.3 product line will focus on **baseline-aware CI policy and configuration diagnostics**.

Earlier repository documents used “v0.3” for an internal milestone that delivered CI mode, standardized exit codes, offline HTML reports, example workflows, and readiness scripts. Those capabilities are already implemented and published in the current `v0.2.0-alpha.1` package. They remain historical implementation evidence, not the scope of the future v0.3 package.

## Product Problem
Repositories need a safe way to adopt scanning incrementally. A clean repository can fail on new findings immediately, but an existing repository may need to record reviewed findings while still preventing new High or Critical risk from entering CI. Configuration errors also need deterministic diagnostics before users trust policy results.

## Goals
1. Add deterministic, sanitized finding fingerprints that do not include raw matches, secrets, usernames, or absolute paths.
2. Define a local, repository-relative baseline manifest with its own schema version.
3. Distinguish existing baseline findings from new findings in human output and additive JSON, SARIF, report, and Web UI metadata.
4. Support CI policy that fails on new findings while keeping reviewed baseline entries visible and auditable.
5. Add configuration validation for unknown keys, invalid values, unsafe combinations, and obsolete settings.
6. Preserve offline-first behavior, current command compatibility, and deterministic cross-platform output.

## Security Boundaries
- Baseline entries store rule IDs, sanitized repository-relative locations, and deterministic fingerprints only.
- Raw finding matches, secret values, local absolute paths, machine names, and user profile data are forbidden in baseline files.
- Critical findings cannot be broadly ignored through a baseline or config wildcard.
- Updating a baseline must be an explicit local action with a reviewable diff.
- Baseline status does not convert a finding into “safe”; it records prior review state.

## Compatibility Rules
- Existing commands and exit codes remain compatible by default.
- New options or commands are additive and must be covered by CLI contract tests.
- Existing JSON schema v2 consumers must continue to work when additive fields are introduced.
- A baseline file uses an independent schema version so scanner output schema and policy state can evolve separately.
- SARIF additions must remain valid SARIF 2.1.0 and must not expose raw matches.
- Path normalization and fingerprints must be stable on Windows, Ubuntu, and macOS.

## Non-Goals
- Hosted dashboards, cloud baseline storage, telemetry, repository upload, or remote policy services.
- Automatic secret redaction or automatic acceptance of findings.
- A general plugin system or custom executable policy hooks.
- GitHub Code Scanning activation, branch protection changes, or other remote repository writes.
- A large Web UI redesign unrelated to baseline review.

## Candidate Delivery Sequence
1. Baseline model, schema, path normalization, and fingerprint design. Completed locally by TASK-0084; scanner/CLI integration remains separate.
2. Config validation and deterministic diagnostics.
3. Baseline creation/review commands and explicit update workflow.
4. Baseline-aware scan and CI exit policy.
5. Additive JSON, SARIF, HTML report, and Web UI integration.
6. Cross-platform tests, migration guidance, and package readiness review.

Each implementation step requires its own task file, focused tests, and a compatibility review. Version metadata changes remain a separate release-preparation task.

## Release Criteria
- No false-negative regression for Critical token/secret patterns.
- New-versus-existing classification is deterministic across supported operating systems.
- Baseline files and generated outputs pass public-source hygiene checks.
- Invalid config fails with actionable diagnostics and documented exit behavior.
- JSON/SARIF contracts, CLI help, README examples, and security docs agree.
- Full build/test, sample smoke, cross-platform workflows, and release gates pass.

## Relationship To 1.0
v0.3 is a hardening line, not a declaration of stable `1.0` compatibility. TASK-0083 will separately measure remaining 1.0 gaps across CLI, config, schemas, security policy, migration guarantees, support policy, and release operations.
