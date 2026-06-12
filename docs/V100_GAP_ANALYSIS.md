# 1.0 Readiness Gap Analysis

## Verdict
AgentContextKit is **not ready for 1.0 general availability**. The current published release is `v0.2.0-alpha.1`. Existing v1.0-labelled documents and scripts prove that early contract, convention, documentation, and release-gate assets exist; they do not prove long-term compatibility, production-scale performance, migration safety, or a complete support/security process.

## Priority Definitions
- **P0**: must be complete before a 1.0 release candidate.
- **P1**: must be complete before 1.0 general availability unless a documented maintainer decision accepts the residual risk.
- **P2**: valuable polish or adoption work that does not block 1.0 by default.

## Gap Register
| ID | Priority | Gap | Current Evidence | Owner | Done Evidence / Validation | 1.0 Blocker | Remote Write |
| --- | --- | --- | --- | --- | --- | --- | --- |
| V100-01 | P0 | Baseline-aware CI policy lacks release-candidate evidence. | TASK-0084 adds the identity foundation; TASK-0086 adds explicit serialization/update, scan classification, safe CI exits, integrity checks, and focused tests. SARIF/report/Web UI parity plus hosted/package evidence remain open. | Core + CLI | Complete TASK-0087 output parity, migration review, cross-platform package workflow, and release-candidate evidence. | Yes | No |
| V100-02 | P0 | CLI contract is a target, not a frozen compatibility promise. | Help, exit-code docs, and contract tests exist. | CLI + Docs | Final command/option review, deprecation policy, invalid-invocation matrix, compatibility tests, release-candidate sign-off. | Yes | No |
| V100-03 | P0 | Config schema accepts unknown keys and has no CLI validation/migration contract. | TASK-0085 adds deterministic report-only Core diagnostics; CLI exit behavior and schema migration remain open. | Core + CLI | CLI diagnostic integration, schema migration rules, invalid-config exit behavior, compatibility tests, and docs. | Yes | No |
| V100-04 | P0 | JSON and SARIF contracts lack published machine-readable schema/version migration assets. | JSON schema v2 docs and additive contract tests; SARIF 2.1.0 output. | Core + CLI | Versioned schema artifacts or equivalent normative contracts, golden compatibility fixtures, migration notes, additive/breaking rules enforced in tests. | Yes | No |
| V100-05 | P0 | Upgrade compatibility is not tested from supported pre-release config/output states. | Current-package and source-package smoke tests pass independently. | Tests + Release | Upgrade matrix from the selected supported predecessor, config compatibility fixtures, generated-file behavior checks, rollback notes. | Yes | No |
| V100-06 | P0 | Security response process is incomplete for sensitive disclosure. | `SECURITY.md`, local scanner model, synthetic fixtures. | Security + Maintainer | Private reporting channel, supported-version statement, response expectations, dependency/security review decision, Critical regression suite. | Yes | Yes, for hosted security settings/contact |
| V100-07 | P1 | Large-repository performance and resource limits are unknown. | Functional tests and capped HTML previews exist. | Core + Tests | Representative large-repo benchmark corpus, documented time/memory expectations, regression threshold, cancellation/error behavior review. | Yes | No |
| V100-08 | P1 | Runtime/platform support lifecycle is not frozen. | .NET 10 and three-OS Actions smoke are documented. | Maintainer + Docs | Minimum SDK/runtime policy, OS/runner policy, support window, EOL handling, cross-platform RC evidence. | Yes | No |
| V100-09 | P1 | Release supply-chain policy is incomplete. | Clean pack/install verification and artifact hygiene gates exist. | Release + Maintainer | Reproducible package-content check, dependency/license review, SBOM/provenance/signing decision, recovery/deprecation runbook. | Yes | Possibly, for signing/provenance publication |
| V100-10 | P1 | User migration and localization parity are not release-gated. | English/Turkish output and current command docs exist. | Docs + CLI | Upgrade guide, breaking-change template, English/Turkish help/output parity checks, tutorial and troubleshooting review. | Yes | No |
| V100-11 | P2 | External adoption evidence and issue feedback are limited. | Public package, samples, tutorials, issue backlog, cross-platform smoke. | Maintainer + Community | Reviewed external-repository trials, triaged feedback, known-limitations update, accepted residual-risk record. | No | Yes, for issues/discussions |
| V100-12 | P2 | Public presentation assets and hosted docs remain deferred. | Visual policy, screenshot plan, docs-site plan. | Docs | Sanitized screenshots and/or approved docs-site activation after privacy review. | No | Yes, for Pages/settings |

## Required Sequence
1. Complete the scoped `v0.2.0-alpha.2` hardening release or explicitly supersede it.
2. Implement the v0.3 baseline/config direction from `docs/V030_ROADMAP_DECISION.md`.
3. Freeze CLI, config, JSON, SARIF, exit-code, and migration contracts from release-candidate evidence.
4. Complete security response, performance, support-lifecycle, and supply-chain P0/P1 evidence.
5. Publish a 1.0 release candidate and run upgrade plus cross-platform validation.
6. Resolve or explicitly accept every remaining P1 risk before 1.0 GA.

## Readiness Gates
A future 1.0 release candidate must provide:
- zero open P0 gaps;
- a named owner and dated disposition for every P1 gap;
- clean restore/build/test/scan/doctor/sample/release gates;
- cross-platform source and published-package smoke evidence;
- upgrade compatibility evidence from the supported predecessor;
- reviewed package contents and security/privacy documentation;
- a maintainer-approved release, support, and rollback plan.

## Status Maintenance
Each implementation task must update this register with evidence rather than marking a gap complete from documentation alone. `scripts/check-v100-readiness.ps1` verifies that this analysis and the historical readiness assets exist; it is not a substitute for closing the gaps.
