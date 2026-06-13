# TASK-0120 Config Diagnostics Polish

## Purpose
Improve config-check diagnostic precision and sanitization without changing config schema v1.

## Current State
ACKITCFG001–014 cover syntax, schema, path, domain, finding-ID, duplicate, and Critical-suppression diagnostics.

## Scope
Audit line/key accuracy, duplicate handling, unsafe-value redaction, warning/error exit parity, and localized display.

## Out Of Scope
New config keys/schema, automatic mutation, echoing values, or Critical suppression.

## Affected Files
Configuration validation/CLI/tests/docs.

## Implementation Steps
Review parser/validator order; reproduce ambiguous diagnostics; patch minimally; add positive/negative tests.

## Security/Privacy Boundary
Diagnostics may show safe key names and line numbers, never configuration values.

## Backward Compatibility
Config schema 1 and diagnostic codes remain stable.

## Database Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/I18n Impact
English/Turkish labels remain consistent where touched.

## Audit/Security Impact
Safer actionable diagnostics reduce misconfiguration without leaking secrets.

## Acceptance Criteria
Diagnostics are deterministic, sanitized, code-stable, and covered by positive/negative tests.

## Tests
Focused validator/CLI localization/exit tests plus full suite.

## Validation
Run config-check human/JSON over valid, warning, and error fixtures.

## Rollback
Revert config diagnostic changes.

## Completion Evidence
Completed locally on 2026-06-13. Unmatched or mismatched scalar/list quotes now produce sanitized `ACKITCFG006`; valid quoted values remain accepted and sensitive values are not echoed.

## Commit
Include with compatible product hardening if changed.

## Push
Normal push after validation.

## Hosted Checks
All exact-commit CI and smoke jobs must pass.
