# TASK-0082 v0.3 Roadmap Decision

## Purpose
Define a coherent v0.3 product direction after the published `v0.2.0-alpha.1` release without reusing completed historical milestone work as new scope.

## Scope
- Record the historical v0.3 naming collision and distinguish it from the next package line.
- Select baseline-aware CI policy and config diagnostics as the v0.3 product theme.
- Define compatibility, privacy, security, validation, and sequencing boundaries.
- Update product, architecture-decision, readiness, roadmap, queue, index, map, and Codex handoff docs.

## Out Of Scope
- No runtime implementation, version bump, package metadata change, workflow activation, tag, push, GitHub Release edit, or NuGet publish.
- No remote baseline service, telemetry, repository upload, automatic suppression, or broad Critical finding bypass.
- No commitment to a final stable `1.0` contract before TASK-0083 gap analysis.

## Affected Files
- `docs/V030_ROADMAP_DECISION.md`
- `docs/V030_READINESS.md`
- `docs/PRODUCT_SPEC.md`
- `docs/DECISIONS.md`
- `docs/ROADMAP.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. Future CI integration remains repository-local unless a maintainer separately enables remote GitHub features.

## SEO/i18n Impact
No current CLI text changes. Future user-facing baseline and config diagnostics must preserve English/Turkish terminology and JSON language independence.

## Audit/Security Impact
The roadmap requires sanitized deterministic finding fingerprints, no raw finding matches in baseline files, explicit policy decisions, and preserved visibility for Critical findings.

## Acceptance Criteria
- The historical internal v0.3 milestone is clearly separated from the future v0.3 package direction.
- The v0.3 theme, goals, non-goals, compatibility rules, security constraints, and candidate task sequence are documented.
- Queue and handoff docs mark TASK-0082 complete and point to TASK-0083.
- Build/test, scan/doctor, hygiene, documentation gates, and release gates pass.

## Tests
Run documentation review, restore/build/test, scan/doctor, JSON/SARIF/sample smoke, hygiene scans, diff checks, and local release gates.

## Risks
- Baselines can hide regressions if they contain raw suppression semantics or are updated without review.
- Finding fingerprints can become unstable if they include machine-specific paths or raw secret values.
- Adding fields can break consumers that incorrectly reject additive JSON/SARIF metadata.

## Rollback
Revert the TASK-0082 documentation commit. No runtime, package, workflow, or remote state changes are made.

## Completion Notes
- Task created after TASK-0081 scope planning.
- Added `docs/V030_ROADMAP_DECISION.md` and selected baseline-aware CI policy plus configuration diagnostics as the next v0.3 theme.
- Separated the completed historical v0.3-labelled milestone from the future package direction and refreshed stale public-state wording in `docs/V030_READINESS.md`.
- Added ADR-0011, product goals, roadmap scope, compatibility rules, security boundaries, release criteria, and a candidate implementation sequence.
- Updated the documentation index, project map, central queue, and Codex handoff files; TASK-0083 is next.
- Validation passed: restore, Release build with 0 warnings/errors, 127/127 tests, clean `scan --ci`, doctor PASS, JSON parse, SARIF parse, sample smoke, global tool version/help, hygiene scans, diff check, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; rerun it after commit.
- The post-commit public release gate passed with no blocking items; only the expected post-release HEAD warning and manual remote-tag verification note remain.
