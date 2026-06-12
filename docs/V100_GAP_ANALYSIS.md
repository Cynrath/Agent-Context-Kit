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
| V100-01 | P0 | Baseline-aware CI policy lacks hosted release-candidate evidence. | TASK-0084/0086/0087 complete the local policy and output parity; TASK-0090 adds a manual three-OS predecessor/config/baseline/SARIF workflow design. | Core + CLI | Obtain a green hosted workflow run and freeze the RC policy. | Yes | No |
| V100-02 | P0 | CLI contract is a target, not a frozen compatibility promise. | Help, exit-code docs, and contract tests exist. | CLI + Docs | Final command/option review, deprecation policy, invalid-invocation matrix, compatibility tests, release-candidate sign-off. | Yes | No |
| V100-03 | P0 | Config diagnostics require release-candidate freeze and hosted predecessor evidence. | TASK-0085 adds deterministic Core diagnostics; TASK-0089 adds read-only `config-check`, warning/error exits, sanitized JSON, missing-default behavior, and manual obsolete-key migration rules. | Core + CLI | Run hosted predecessor-config smoke and approve the contract for the selected RC. | Yes | No |
| V100-04 | P0 | JSON and SARIF contracts lack published machine-readable schema/version migration assets. | JSON schema v2 docs and additive contract tests; SARIF 2.1.0 output. | Core + CLI | Versioned schema artifacts or equivalent normative contracts, golden compatibility fixtures, migration notes, additive/breaking rules enforced in tests. | Yes | No |
| V100-05 | P0 | Upgrade compatibility needs hosted release-candidate evidence. | TASK-0088 adds fixtures/tests/guidance; TASK-0090 adds a manual three-OS isolated predecessor/source-candidate workflow design. | Tests + Release | Push and obtain a green manual hosted run for the selected RC. | Yes | Yes, to push/dispatch |
| V100-06 | P0 | Security response process is incomplete for sensitive disclosure. | Response expectations, supported-version policy, Critical regression coverage, and a dated no-vulnerability review exist locally. | Security + Maintainer | Enable and verify private GitHub vulnerability reporting before RC. | Yes | Yes, for hosted security settings/contact |
| V100-07 | P1 | Large-repository performance and resource limits need broader evidence. | TASK-0088 adds a disposable 2,000-file benchmark; TASK-0090 runs the same tripwire in the manual three-OS workflow design. | Core + Tests | Obtain hosted timings and review memory, cancellation, and mixed-corpus behavior. | Yes | Yes, to push/dispatch |
| V100-08 | P1 | Runtime/platform support lifecycle needs RC confirmation. | .NET 10, lifecycle policy, and a manual Windows/Ubuntu/macOS RC workflow are documented. | Maintainer + Docs | Obtain fresh hosted RC results and approve the final support duration. | Yes | Yes, to push/dispatch |
| V100-09 | P1 | Release supply-chain policy has unresolved RC decisions. | Dependency/artifact policy, recovery guidance, clean dated dependency review, and the TASK-0091 xUnit v3 migration exist. | Release + Maintainer | Decide signing, SBOM, provenance, and publication/recovery evidence. | Yes | Possibly, for signing/provenance publication |
| V100-10 | P1 | Localization parity is not release-gated. | Upgrade/rollback guidance, compatibility fixtures, and localized `config-check` labels exist. | Docs + CLI | Add complete English/Turkish help/output parity gates and finish tutorial/troubleshooting review. | Yes | No |
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
