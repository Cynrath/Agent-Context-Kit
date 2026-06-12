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
| V100-01 | P0 | Baseline-aware CI policy lacks release-candidate evidence. | TASK-0084 adds the identity foundation; TASK-0086 adds explicit serialization/update, scan classification, safe CI exits, and integrity checks; TASK-0087 adds JSON/SARIF/report/Web UI parity. Hosted/package and migration evidence remain open. | Core + CLI | Complete migration review, cross-platform package workflow, and release-candidate evidence. | Yes | No |
| V100-02 | P0 | CLI contract is a target, not a frozen compatibility promise. | Help, exit-code docs, and contract tests exist. | CLI + Docs | Final command/option review, deprecation policy, invalid-invocation matrix, compatibility tests, release-candidate sign-off. | Yes | No |
| V100-03 | P0 | Config schema accepts unknown keys and has no CLI validation/migration contract. | TASK-0085 adds deterministic report-only Core diagnostics; CLI exit behavior and schema migration remain open. | Core + CLI | CLI diagnostic integration, schema migration rules, invalid-config exit behavior, compatibility tests, and docs. | Yes | No |
| V100-04 | P0 | JSON and SARIF contracts lack published machine-readable schema/version migration assets. | JSON schema v2 docs and additive contract tests; SARIF 2.1.0 output. | Core + CLI | Versioned schema artifacts or equivalent normative contracts, golden compatibility fixtures, migration notes, additive/breaking rules enforced in tests. | Yes | No |
| V100-05 | P0 | Upgrade compatibility needs hosted release-candidate evidence. | TASK-0088 adds published-config and baseline-schema fixtures/tests plus upgrade and rollback guidance. | Tests + Release | Run upgrade/source-package smoke on Windows, Ubuntu, and macOS for the selected RC. | Yes | No |
| V100-06 | P0 | Security response process is incomplete for sensitive disclosure. | Response expectations, supported-version policy, Critical regression coverage, and a dated no-vulnerability review exist locally. | Security + Maintainer | Enable and verify private GitHub vulnerability reporting before RC. | Yes | Yes, for hosted security settings/contact |
| V100-07 | P1 | Large-repository performance and resource limits need broader evidence. | TASK-0088 adds a disposable 2,000-file benchmark; final 2026-06-12 rerun was 2.936 seconds against a 30-second tripwire. | Core + Tests | Add hosted three-OS evidence and review memory, cancellation, and mixed-corpus behavior. | Yes | No |
| V100-08 | P1 | Runtime/platform support lifecycle needs RC confirmation. | .NET 10, three-OS runner policy, support window, predecessor, and EOL handling are documented. | Maintainer + Docs | Obtain fresh hosted cross-platform RC evidence and approve the final support duration. | Yes | No |
| V100-09 | P1 | Release supply-chain policy has unresolved RC decisions. | Dependency/artifact policy, recovery guidance, and dated vulnerability review exist; `xunit` `2.9.3` is flagged Legacy. | Release + Maintainer | Resolve or accept the test dependency warning and decide signing, SBOM, and provenance. | Yes | Possibly, for signing/provenance publication |
| V100-10 | P1 | Localization parity is not release-gated. | Upgrade/rollback guidance and compatibility fixtures exist. | Docs + CLI | Add English/Turkish help/output parity checks and complete tutorial/troubleshooting review. | Yes | No |
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
