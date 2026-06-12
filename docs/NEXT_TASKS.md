# Next Tasks

This is the unified execution queue after the TASK-0066 through TASK-0099 local-only sequence. Maintainer-gated release/security work and local-only product intelligence are separate tracks.

## Completed Local Execution
- TASK-0066 through TASK-0099 are completed locally.
- The current published release remains `v0.2.0-alpha.1`.
- Standard `ci`, published-package smoke, and source-package smoke are green for current remote `master`.
- The release-candidate evidence boundary remains `LOCAL READY / REMOTE NO-GO` where applicable.
- Completed local documentation, tests, and gates do not claim 1.0 readiness or close remote P0/P1 decisions.

## Maintainer-Gated Release/Security Track
These actions require explicit maintainer control and do not block safe local-only product/documentation work:

1. Run the manual hosted RC evidence workflow on the exact reviewed commit.
2. Enable and verify private vulnerability reporting and record primary/backup security notification ownership.
3. Decide NuGet owner profile alignment: `Cyranth` versus public persona/package author `Cynrath`.
4. Decide author signing or record dated accepted risk.
5. Decide SBOM publication or record dated accepted risk.
6. Decide GitHub provenance/attestation or record dated accepted risk.
7. Accept package recovery ownership, thresholds, communication, and review date.
8. Select the next candidate version only after the required P0/P1 decisions and hosted evidence.

## Local-Only Ecosystem/Product Intelligence Track
1. TASK-0100 offline OSS ecosystem catalog and roadmap reset - completed locally.
2. TASK-0101 related tools comparison matrix - queued.
3. TASK-0102 offline workflow examples with external tools - queued.
4. TASK-0103 optional interoperability design, no dependency - queued.
5. TASK-0104 agent context pipeline taxonomy - queued.
6. TASK-0105 README ecosystem positioning section - queued.

## Current Remote State
- Current published release: `v0.2.0-alpha.1`.
- GitHub Release: published pre-release.
- NuGet package: `AgentContextKit` `0.2.0-alpha.1`.
- Published global tool includes `ackit sarif`.
- Local and remote `master` were aligned at TASK-0100 start.
- Latest checked `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` runs succeeded for `1d898c6`.
- TASK-0100 local validation passed with 178/178 tests, clean source scan/doctor, parsed JSON/SARIF, sample smoke, hygiene checks, and all local readiness/release evidence gates. The pre-commit public release gate reported only the expected dirty working tree blocker.

## Execution Rule
Continue local-only queued work in order without per-task prompts. Do not perform remote writes, auto-install external tools, add dependencies, upload repository content, or claim release readiness from documentation alone.
