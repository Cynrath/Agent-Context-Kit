# TASK-0100 Offline OSS Ecosystem Catalog And Roadmap Reset

## Purpose
Reclassify the post-TASK-0099 project state, separate maintainer-gated release/security work from safe local product intelligence, and create an initial evidence-based catalog of related offline-capable open-source tools without adding dependencies or copying external code.

## Current Project State
- TASK-0066 through TASK-0099 are complete locally.
- Current published release remains `v0.2.0-alpha.1`.
- Standard hosted CI/source/published smoke workflows are green for current remote `master`.
- The RC evidence boundary remains `LOCAL READY / REMOTE NO-GO` where applicable.
- Maintainer-controlled security, supply-chain, hosted RC, ownership, and candidate decisions remain open.

## Why This Task Exists
AgentContextKit now has enough local scanning, context generation, agent instruction, task/handoff, JSON/SARIF, report, and release-gate functionality to need explicit ecosystem positioning. Related projects solve adjacent problems, but undocumented overlap could lead to unnecessary dependencies, duplicated scope, unsafe network assumptions, or unclear product identity.

## Scope
- Research an initial set of official project repositories and documentation.
- Record license, runtime, offline capability, cloud/API requirements, outputs, overlap, complementary value, privacy considerations, maturity notes, and a conservative integration decision.
- Define offline-first evaluation criteria and an integration risk model.
- Create docs-only workflow examples and a no-dependency interoperability backlog.
- Reset the execution queue into completed local work, maintainer-gated release/security work, and local-only ecosystem/product intelligence.
- Add a minimal README ecosystem link in English and Turkish.

## Out Of Scope
- No external dependency, package, binary, submodule, vendored code, adapter, command, workflow activation, auto-install, or network integration.
- No external project code or documentation is copied.
- No claim that a tool is integration-ready without verified license and privacy review.
- No push, tag, GitHub Release edit, NuGet publish, Code Scanning upload, repository setting change, or other remote write.

## Maintainer-Gated Items Not Solved By This Task
- Manual hosted RC evidence workflow dispatch and review.
- Private vulnerability reporting enablement and notification ownership.
- NuGet owner profile `Cyranth` versus public persona/package author `Cynrath` disposition.
- Author-signing, SBOM, provenance/attestation, and package-recovery decisions.
- Candidate version selection and release approval after P0/P1 decisions.

## Offline-First Criteria
A tool is `yes` only when its primary documented workflow can run locally after installation without uploading repository content or requiring a hosted account/API. `partial` means a useful local path exists but important features, model inference, indexing, or collaboration may require optional services. `no` means the relevant workflow requires cloud/API execution.

## Candidate OSS Project Categories
- Repo-to-context and prompt packers.
- Project graph, code graph, and knowledge graph tools.
- Local security, secret, dependency, and static-analysis scanners.
- Agent instruction, convention, and project-memory ecosystems.
- Documentation site and documentation-quality tools.

## Evaluation Matrix
Each catalog row records project/source, verified license, runtime, offline capability, cloud/API requirement, output, overlap, complement, proposed integration mode, security/privacy notes, maturity notes, and decision.

## Integration Risk Model
- License compatibility and redistribution obligations.
- Repository content leaving the local machine.
- Secret/path leakage through generated context or reports.
- Automatic installation or executable trust.
- Output schema/version stability.
- Cross-platform availability and runtime weight.
- Scope duplication and long-term maintenance burden.
- Hosted account/API lock-in.

## Documentation Deliverables
- `docs/RELATED_PROJECTS.md`
- `docs/OFFLINE_OSS_ECOSYSTEM.md`
- `docs/EXTERNAL_TOOL_WORKFLOWS.md`
- `docs/ECOSYSTEM_POSITIONING.md`
- `docs/INTEROPERABILITY_BACKLOG.md`
- Queue, README, index/map/roadmap, changelog, and Codex handoff updates.

## Affected Files
- Ecosystem catalog, positioning, workflow, interoperability, roadmap, queue, README, index/map, changelog, and Codex handoff Markdown files only.
- No source project, package metadata, workflow, dependency manifest, or generated artifact changes.

## Database Impact
None. AgentContextKit has no database change in this task.

## Admin Impact
None locally. GitHub settings, labels, security controls, releases, and package-owner actions remain maintainer-only.

## Permission Impact
None. No GitHub token scope, workflow permission, filesystem privilege, or remote API permission is added or changed.

## SEO And Localization Impact
- README adds a short English and Turkish ecosystem section linking to the detailed catalog.
- No website metadata, hosted docs site, routing, or search-index configuration changes.

## Audit And Security Impact
- External tools are classified by offline behavior, network/API boundary, license, output, and repository-content exposure risk.
- Documentation does not approve execution, installation, content upload, Critical suppression, or dependency integration.
- Generated outputs remain local and ignored unless a future reviewed task defines a sanitized public artifact.

## Tests
- Restore, Release build, full test suite, source scan, doctor, JSON parse, global-tool SARIF parse, and sample smoke.
- Identity, tracked artifact, literal token/local-path, and whitespace hygiene scans.
- Config, v0.2, historical v1.0, RC, security/supply-chain, published-state, documentation, and release verification gates.

## Risks
- License or offline claims can become stale; TASK-0101 must add normalized evidence dates and higher-confidence source links.
- Wide comparison tables may be hard to read on small screens; detailed matrices remain in docs rather than README.
- Docs-only workflow examples could be mistaken for approved integrations; every workflow preserves explicit opt-in and no-auto-install language.

## Acceptance Criteria
- TASK-0066 through TASK-0099 remain classified as completed locally.
- Maintainer-gated work is visibly separated and does not block local-only product intelligence.
- The initial catalog uses official project sources and avoids unsupported license/offline claims.
- External tools are not added as dependencies and no code is copied.
- README changes remain concise and link to the detailed catalog.
- Full build/test/scan/doctor/hygiene/release gates pass before commit.

## Validation Steps
Run the requested restore/build/test/scan/doctor/JSON/SARIF/sample/hygiene/readiness/release gate sequence, verify generated artifacts remain untracked, and rerun the public release gate after commit.

## Rollback Plan
Revert the TASK-0100 commit. No external dependency, remote state, or generated public artifact is created.

## Completion Notes
- Task created after confirming no unfinished local-only task remained in the queue.
- Confirmed TASK-0066 through TASK-0099 remain complete locally and separated the remaining hosted RC/security decisions into a maintainer-gated track.
- Added the initial official-source catalog, offline-first criteria, product positioning, external workflow sketches, and no-dependency interoperability backlog.
- Added concise English and Turkish README ecosystem links and synchronized the documentation index, project map, roadmap, changelog, queue, and Codex handoff files.
- Added no external dependency, adapter, binary, workflow activation, auto-install, remote call, or copied external code.
- Validation passed on 2026-06-13: Release build with zero warnings/errors, 178/178 tests, clean source scan and doctor, parsed JSON and SARIF, global tool `0.2.0-alpha.1`, sample smoke, clean identity/artifact/token/path hygiene, and all requested local readiness/security/supply-chain/release verification gates.
- The pre-commit public release gate failed only because the working tree contains the intentional TASK-0100 changes; package metadata and release audit content were otherwise clean. Rerun after commit.
