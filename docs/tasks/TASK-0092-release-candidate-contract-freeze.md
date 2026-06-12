# TASK-0092 Release-Candidate Contract Freeze And Maintainer Decision

## Purpose
Create a single release-candidate contract freeze and maintainer GO/NO-GO decision package without selecting a version, publishing, or claiming that unresolved hosted and remote evidence is complete.

## Scope
- Reconcile the current CLI, exit-code, config, JSON, baseline, SARIF, generated-file, privacy, and upgrade contracts.
- Record the locally frozen candidate surface and the rules for additive versus breaking changes.
- Distinguish locally complete contract evidence from hosted, remote-setting, supply-chain publication, and release actions.
- Provide a maintainer decision checklist with explicit GO, conditional GO, and NO-GO conditions.
- Update the 1.0 gap register, release evidence, roadmap, queue, changelog, documentation index, project map, and Codex handoff files.

## Out Of Scope
- No source/package version change, package build contract change, push, tag, GitHub Release edit, NuGet publish, SARIF upload, workflow dispatch, private vulnerability setting change, signing, SBOM publication, or provenance publication.
- No assertion that AgentContextKit is ready for a 1.0 release candidate or 1.0 GA.
- No breaking CLI, config, JSON, baseline, SARIF, or exit-code change.

## Affected Files
- `docs/RELEASE_CANDIDATE_CONTRACT_FREEZE.md`
- `docs/MAINTAINER_RC_DECISION.md`
- `docs/CLI_CONTRACT.md`
- `docs/EXIT_CODES.md`
- `docs/CONFIGURATION.md`
- `docs/JSON_OUTPUT.md`
- `docs/SARIF_OUTPUT.md`
- `docs/UPGRADE_COMPATIBILITY.md`
- `docs/RELEASE_CANDIDATE_EVIDENCE.md`
- `docs/RELEASE_CHECKLIST.md`
- `docs/RELEASE_VALIDATION.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/V100_READINESS.md`
- `docs/V100_GAP_ANALYSIS.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/ROADMAP.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `scripts/check-release-candidate-evidence.ps1`
- `scripts/check-v100-readiness.ps1`
- `CHANGELOG.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None locally. The final maintainer decision includes remote repository and release administration actions that are not executed by this task.

## Permission Impact
No new runtime or repository permissions. Hosted workflow dispatch, private vulnerability reporting, signing, provenance, release, and publication remain maintainer-only.

## SEO/i18n Impact
No output text change. English/Turkish parity remains an explicit pre-GA evidence item and cannot be marked complete from this documentation task.

## Audit/Security Impact
The freeze preserves offline-first operation, repository-relative generated paths, sanitized JSON/SARIF/baseline data, Critical finding visibility, explicit baseline opt-in, and no automatic config migration. It must not conceal unresolved security reporting or supply-chain decisions.

## Acceptance Criteria
- One normative document enumerates frozen candidate contracts and compatibility rules.
- One maintainer decision document separates local evidence, hosted evidence, remote settings, supply-chain decisions, and release actions.
- The current source version remains unchanged and no RC approval is implied.
- V100 gaps are updated only where local contract evidence genuinely improved.
- Full local tests, scans, contract/readiness/release gates, dependency review, sample smoke, and hygiene checks pass.
- Post-commit public release gates pass.

## Tests
Run restore/build/169-test suite, config-check JSON parse, scan/doctor, scan JSON and SARIF parse, sample smoke, dependency vulnerability/deprecation review, CLI/config/v0.2/v1.0/RC workflow/evidence/release gates, hygiene scans, diff checks, and post-commit public release gates.

## Risks
- Calling a conditional local freeze a release approval would overstate readiness.
- Duplicated contract text can drift; the freeze must link normative command-specific docs rather than replace them.
- A later breaking change requires reopening the freeze and recording migration/versioning impact.

## Rollback Plan
Revert the TASK-0092 commit. No runtime, package, version, workflow, or remote state is changed.

## Completion Notes
- Task created after TASK-0091 commit `47d6d8b` and successful post-commit public release gate.
- Added the conditional contract inventory and compatibility rules in `docs/RELEASE_CANDIDATE_CONTRACT_FREEZE.md` without selecting a version.
- Added the maintainer GO/NO-GO inputs, conditions, triggers, decision template, and remote-write boundary in `docs/MAINTAINER_RC_DECISION.md`; current status is NO-GO for RC publication.
- Updated normative CLI/config/JSON/SARIF/exit/upgrade docs, evidence/readiness/gap docs, queue/roadmap/index/map, release checklist/handoff, and Codex handoff files.
- Updated RC evidence and v1.0 readiness gates to require the new documents and critical decision markers.
- Full validation passed: zero-warning Release build, 169/169 tests, clean scan, doctor PASS, JSON/SARIF parse, sample smoke, clean dependency reviews, all contract/readiness/RC gates, local package verification, and clean hygiene scans. The 2,000-file benchmark completed in 7.427 seconds under the 30-second tripwire.
- The pre-commit public release gate failed only on the expected dirty working tree and must be rerun after commit.
- No version change, workflow dispatch, push, tag, release, publish, signing, SBOM, provenance, or repository setting change was performed.
