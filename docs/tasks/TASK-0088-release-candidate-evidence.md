# TASK-0088 Release Candidate Evidence

## Purpose
Build a reviewable local evidence pack for upgrade compatibility, performance, security response, support lifecycle, supply chain, and release-candidate decisions after the baseline/config hardening work.

## Scope
- Define the supported predecessor and local upgrade compatibility matrix.
- Add compatibility fixtures/tests for the published `0.2.0-alpha.1` config and current baseline schema.
- Add a repeatable local large-repository scan benchmark with documented non-normative thresholds.
- Document security response readiness, support lifecycle, and supply-chain decisions/manual blockers.
- Add a local release-candidate evidence gate that checks required docs, commands, fixtures, and scripts.
- Update the 1.0 gap register and central queue with evidence versus remaining hosted/manual actions.

## Out Of Scope
- No version bump, release candidate tag, push, GitHub Release, NuGet publish, package signing, provenance publication, SBOM upload, GitHub security setting changes, or hosted workflow dispatch.
- No claim that 1.0 GA is ready while P0/P1 hosted/manual evidence remains open.
- No destructive migration or automatic rewrite of user config/baseline files.

## Affected Files
- Compatibility/performance/security/support/supply-chain/release-candidate docs.
- test fixtures and focused compatibility tests.
- local benchmark/evidence gate scripts.
- roadmap, queue, readiness, release validation, changelog, and Codex handoff docs.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
The local gate is read-only except for disposable temp directories. Hosted security settings and publication permissions remain maintainer-only.

## SEO/i18n Impact
None. Evidence docs are maintainer-facing English documents.

## Audit/Security Impact
Fixtures must contain no real secrets, private paths, customer data, or broad Critical suppression. Benchmark output must omit repository content and use disposable synthetic files.

## Acceptance Criteria
- Supported predecessor and upgrade expectations are explicit.
- Published config compatibility and baseline schema integrity have focused tests.
- A repeatable synthetic scan benchmark reports file count, elapsed time, and threshold result.
- Security response, support lifecycle, and supply-chain docs separate completed local evidence from maintainer-only actions.
- A release-candidate evidence script fails on missing required assets or failed local checks.
- Full local validation passes; remaining remote actions are listed without performing them.

## Tests
Run focused compatibility tests, benchmark smoke, evidence gate, restore/build/test, scan/doctor, JSON/SARIF/sample smoke, hygiene scans, contract/readiness/release gates, and post-commit public gates.

## Risks
- Synthetic performance data can be mistaken for production guarantees.
- A documentation-only security process can be mistaken for an enabled private reporting channel.
- Pre-release compatibility promises can unintentionally freeze unstable behavior.

## Rollback
Revert the TASK-0088 commit. No remote state or published artifact is changed.

## Completion Notes
- Task created after TASK-0087 commit `4a31a3c` and successful post-commit public gate.
- Published-config and baseline-schema compatibility fixtures were added; focused tests passed 2/2.
- The final disposable 2,000-file benchmark rerun completed in 2.936 seconds against the 30-second tripwire.
- Dependency vulnerability review found no vulnerable packages; deprecation review flagged test dependency `xunit` `2.9.3` as Legacy.
- The local evidence gate passed with the benchmark included.
- Hosted three-OS RC evidence, private vulnerability reporting, supply-chain decisions, and xUnit migration/risk acceptance remain outside this local task.
- Full repository validation passed: restore, Release build, 164/164 tests, clean scan, doctor PASS, JSON/SARIF parse, sample smoke, hygiene, contract/readiness/evidence gates, and local package verification.
- The pre-commit public release gate failed only on the expected dirty working tree; rerun it after commit.
