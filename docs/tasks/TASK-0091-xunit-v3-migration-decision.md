# TASK-0091 xUnit v3 Migration Or Release-Candidate Risk Acceptance

## Purpose
Resolve the release-candidate dependency deprecation warning for test package `xunit` `2.9.3` by safely migrating to xUnit v3 when compatible, or by recording an explicit dated risk acceptance if migration cannot be validated.

## Scope
- Verify current xUnit v3 package/version and official migration guidance.
- Test the migration in a disposable project copy before editing the repository project.
- Preserve the existing test source/API surface where compatible.
- Update test package references, lock/restore state, dependency evidence, and supply-chain docs.
- Run all tests and release gates after migration.

## Out Of Scope
- No product runtime dependency changes, test rewrites unrelated to v3 compatibility, package version bump, push, tag, release, NuGet publish, or remote repository changes.
- No acceptance of a failing or partially discovered test suite.

## Affected Files
- `tests/AgentContextKit.Tests/AgentContextKit.Tests.csproj`
- test source only if required by official v3 compatibility changes.
- dependency, supply-chain, release-candidate, roadmap, queue, changelog, and Codex handoff docs.

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
NuGet restore/search requires network access. No credentials or remote writes are required.

## SEO/i18n Impact
None.

## Audit/Security Impact
The migration must not reduce discovered test count, remove Critical regression coverage, or add runtime dependencies. Vulnerability/deprecation review results must be dated and recorded.

## Acceptance Criteria
- Official migration requirements and selected package versions are documented.
- Disposable migration smoke passes before repository edits.
- Full repository test count is preserved or intentionally explained.
- `dotnet list ... --deprecated` no longer reports `xunit` Legacy, or a dated maintainer risk acceptance explains why migration is deferred.
- Vulnerability review remains clean.
- Full local validation and post-commit public gates pass.

## Tests
Run disposable migration smoke, restore/build/full tests, dependency deprecation/vulnerability review, scan/doctor, JSON/SARIF/sample smoke, hygiene, workflow/CLI/config/v0.2/v1.0/RC evidence/release gates, local package verification, and post-commit public gates.

## Risks
- xUnit v3 may change assertion or runner behavior.
- Test discovery may silently decrease if runner packages are mismatched.
- Migration can introduce new transitive test-only dependencies and license obligations.

## Rollback Plan
Revert the TASK-0091 commit and restore the prior test package references. Product runtime/package behavior is unaffected.

## Completion Notes
- Task created after TASK-0090 commit `fd1d8be` and successful post-commit public gate.
- The official xUnit v3 migration guide was reviewed; it requires the `xunit.v3` package and executable test projects.
- Stable package versions verified from NuGet on 2026-06-12: `xunit.v3` `3.2.2` and `xunit.runner.visualstudio` `3.1.5`.
- Disposable migration smoke under ignored `.ackit/tmp` passed 169/169 tests before repository edits.
- Repository restore/build/test passed with 169/169 tests after migration.
- Post-migration direct/transitive vulnerability and deprecation reviews reported no findings.
- Full CLI, JSON/SARIF parse, sample, contract, config, v0.2, v1.0, RC evidence/workflow, package verification, and hygiene gates passed. The final 2,000-file benchmark completed in 3.892 seconds under the 30-second tripwire.
- The pre-commit public release gate failed only on the expected dirty working tree and must be rerun after commit.
- No test source changes, runtime dependency changes, remote writes, package publication, or risk acceptance were required.
