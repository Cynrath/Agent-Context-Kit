# Next Steps

PROJECT-CONTROL-0001 is active: use `docs/NEXT_TASKS.md` and `docs/PROJECT_EXECUTION_QUEUE.md` as the central local task queue.

1. Current published release is `v0.2.0-alpha.1`.
2. Local `master` is aligned with `origin/master` at the start of PROJECT-CONTROL-0001.
3. Read-only GitHub CLI validation confirmed latest `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs on `master` succeeded for commit `e0a0fa9`.
4. GitHub Release `v0.2.0-alpha.1` exists as a pre-release; its body has candidate-era wording, so `docs/RELEASE_BODY_V020_ALPHA1.md` is the maintainer-ready replacement draft.
5. Published NuGet `0.2.0-alpha.1` includes `ackit sarif`.
6. TASK-0066 through TASK-0092 are committed locally. `docs/RELEASE_CANDIDATE_CONTRACT_FREEZE.md` and `docs/MAINTAINER_RC_DECISION.md` keep RC publication at NO-GO.
7. `docs/V100_GAP_ANALYSIS.md` is the source of truth for 1.0 readiness; historical v1.0 scripts validate asset presence, not GA completion.
8. Remote writes remain maintainer-only: push, tag, GitHub Release edits, label creation, repo settings, branch protection, Code Scanning upload, issue creation, and NuGet publish.
9. Generated `.ackit/`, SARIF, local reports, Web UI, packages, archives, `bin/`, `obj/`, `TestResults`, and coverage artifacts must not be committed.
10. TASK-0088 initially found Legacy test dependency `xunit` `2.9.3`; TASK-0091 resolved it with `xunit.v3` `3.2.2` and runner `3.1.5` without reducing the 169-test suite.
11. TASK-0089 adds current-source `config-check`; the published `0.2.0-alpha.1` package does not include this command. Full local validation passed with 169/169 tests.
12. TASK-0090 adds a manual-only three-OS RC workflow. Static/local Windows smoke passed; hosted syntax/result evidence requires maintainer push and manual dispatch.
13. `check-public-release-gates.ps1 -FailOnIssues` must be rerun after commit because it correctly fails on dirty working trees before commit.
14. Local branch is ahead of origin; do not push from the agent session. Report `local ahead, maintainer push required`.
15. TASK-0093 is complete locally: machine-readable assets are indexed in `docs/schemas/README.md`; focused scanner/contract tests pass 5/5 and the full suite passes 173/173.
16. TASK-0094 is complete locally. Five focused tests cover help, all 13 language-aware JSON commands, known argument errors, exit parity, baseline/suppression labels, and JSON semantic invariance; the full suite passes 178/178.
17. TASK-0095 is complete locally. The evidence register, maintainer handoff, dependency review, and local evidence gate pass with 178/178 tests and a 3.368-second synthetic benchmark.
18. Private vulnerability reporting, NuGet signing, SBOM, provenance, bad-package recovery acceptance, hosted RC evidence, and final RC approval remain explicit maintainer-only actions.
19. TASK-0096 is complete locally. `docs/RC_LOCAL_READINESS.md` and `scripts/check-rc-local-readiness.ps1` pass with 178/178 tests, clean dependency review, local package verification, and a 3.495-second benchmark.
20. Before TASK-0097 edits, local and remote were aligned at `37d5220`; standard `ci`, published smoke, and source smoke are green for that commit.
21. TASK-0097 is complete locally. It records exact public run URLs and preserves the zero-run manual RC workflow blocker; full local validation passes with 178/178 tests and a 3.716-second benchmark.
22. Remaining RC progress requires maintainer action: dispatch the manual RC workflow, verify private reporting/security ownership, decide signing/SBOM/provenance/recovery, select a candidate, and approve release preparation.
23. TASK-0098 is complete locally. The official read-only GitHub endpoint returned `enabled: false`; the verified P0 blocker and maintainer-only activation path are documented without changing the setting.
24. TASK-0098 validation passes with 178/178 tests, zero scan/SARIF findings, full local release verification, and a 2.62-second synthetic benchmark. Remote activation and notification ownership remain open.
25. TASK-0099 is complete locally. Read-only audit found a valid NuGet.org repository signature, no observed author signature, no package/release SBOM, no accessible GitHub package attestation, and NuGet owner profile `Cyranth` versus project persona `Cynrath`.
26. TASK-0099 validation passes with 178/178 tests, zero scan/SARIF findings, clean dependency review, local package verification, and a 3.282-second synthetic benchmark. All remediation remains maintainer-only.
