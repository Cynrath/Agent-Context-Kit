# v0.2.0-alpha.2 Candidate Planning Refresh

Status: planning only. No version is selected, bumped, tagged, packed, published, or approved.

## Current Published Baseline
- Current published release: `v0.2.0-alpha.1`.
- Published package includes `ackit sarif`.
- Current source includes later baseline/config/localization/security-evidence hardening.
- Local validation can be ready while future candidate status remains `REMOTE NO-GO`.

## Proposed Scope
1. Scanner precision refinements with no reduction in Critical secret detection.
2. Sanitized suppression-audit polish without raw-match disclosure.
3. Baseline-aware CI policy polish while keeping findings visible.
4. Config diagnostics polish without automatic migration or unsafe fallback changes.
5. External ecosystem documentation, evidence, privacy, and offline policy completion.
6. Documentation/contract consistency and focused regression tests for any runtime change.

## Explicit Exclusions
- No external dependency expansion.
- No external tool auto-install or execution.
- No default network behavior, hosted upload, telemetry, model/API call, or remote repository ingestion.
- No external SARIF/JSON/SBOM/graph parser in this planning scope.
- No broad Critical suppression.
- No version bump, tag, GitHub Release, NuGet publish, or final release notes in this task.

## Compatibility Intent
Prefer a small backward-compatible hardening candidate. Published command names, default behavior, exit codes, JSON schema, config schema, baseline identity, and SARIF profile require explicit review before any change. Design-only commands documented under TASK-0109/0110 are not part of the candidate.

## Required Maintainer Dispositions
Before selecting or approving a candidate:
- RB-001 hosted RC evidence;
- RB-002 private vulnerability reporting;
- RB-003 notification ownership;
- RB-004 NuGet owner identity disposition;
- RB-005 author signing decision;
- RB-006 SBOM decision;
- RB-007 provenance/attestation decision;
- RB-008 package recovery decision;
- RB-009 exact candidate version/commit; and
- RB-010 explicit GO/NO-GO.

See `docs/RELEASE_BLOCKER_BOARD.md` and `docs/MAINTAINER_DECISION_REGISTER.md`.

## Future Candidate Task
A separate maintainer-approved release-preparation task must choose the version, update project/runtime/workflow metadata, review package contents/diff, run local package install smoke, obtain exact hosted evidence, finalize changelog/release notes, and then request each remote write explicitly.

## Decision
Continue local product/docs hardening without claiming candidate or release readiness. `v0.2.0-alpha.2` remains a planning label only.
