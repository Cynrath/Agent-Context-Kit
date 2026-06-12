# TASK-0075 Safe Suppression Audit Log

## Purpose
Make configured non-Critical scanner suppressions locally auditable without exposing raw finding matches or weakening Critical detection.

## Scope
- Preserve visible finding behavior and exit-code decisions.
- Record additive suppression metadata for `safeDomains`, `ignoredPaths`, and `ignoredFindingIds`.
- Expose suppression counts and sanitized records in human and JSON `scan` output.
- Keep suppressed findings out of SARIF results.
- Document privacy and compatibility boundaries.

## Affected Files
- `src/AgentContextKit.Core/Abstractions.cs`
- `src/AgentContextKit.Core/Models.cs`
- `src/AgentContextKit.Core/Scanning.cs`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/SUPPRESSION_AUDIT.md`
- scanner/config/JSON/CLI/security docs
- queue, roadmap, changelog, and `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/i18n Impact
JSON field names and suppression reason identifiers remain English and language-independent. Human audit headings remain concise terminal output.

## Audit/Security Impact
Audit records include rule ID, severity, category, repository-relative path, and config reason. They omit raw match values and finding message content. Critical findings cannot create suppression records because they remain visible.

## Acceptance Criteria
- Configured non-Critical suppressions produce sanitized audit records.
- `safeDomains`, `ignoredPaths`, and `ignoredFindingIds` are distinguishable reasons.
- Critical findings remain visible and are not listed as suppressed.
- JSON output adds suppression metadata without changing schema version 2 or existing fields.
- Human scan output reports suppression counts without raw matches.
- SARIF contains visible findings only.
- Full tests and release gates pass.

## Tests
Run focused suppression/scanner/JSON tests, full Release tests, scan, doctor, SARIF parse, sample smoke, hygiene checks, and release gates.

## Risks
- Audit output could leak raw matched values if DTO boundaries are not explicit.
- Double-counting can occur when more than one config rule matches the same finding.
- Breaking positional `ScanResult` constructors would create broad unrelated churn.

## Rollback
Revert the suppression audit model, scanner, CLI, tests, and documentation commit. Existing suppression behavior remains available in the previous implementation.

## Completion Notes
- Added additive Core suppression models while preserving existing positional `ScanResult` construction.
- Added default interface implementations so existing custom scanner implementations continue with an empty audit until they opt in.
- Added sanitized config suppression audit records for `safeDomains`, `ignoredPaths`, and `ignoredFindingIds`.
- Added human scan summaries and JSON `suppressionSummary`/`suppressions` fields without changing schema version 2 or exit-code behavior.
- Audit records omit raw matches and messages; Critical findings remain visible even when a lower-severity duplicate is suppressed.
- SARIF remains visible-findings-only and has a focused regression test excluding suppression records.
- Published `0.2.0-alpha.1` availability is documented separately from the current-source audit feature.
- Focused scanner, JSON audit, and SARIF tests passed; full Release tests passed 127/127 with 0 build warnings and 0 build errors.
- Repository scan, doctor, JSON scan, SARIF parse, sample smoke, hygiene scans, CLI/config/v0.2/v1.0 gates, and local release verification passed.
- The pre-commit public release gate failed only because the working tree contained the task changes, which is expected before commit.
- The post-commit public release gate passed with no blockers; only the expected post-release `HEAD` warning and manual remote-tag verification note remain.
