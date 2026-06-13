# Next Steps

PROJECT-CONTROL-0102 is active and explicitly authorizes the `v0.2.0-alpha.2` release sequence.

1. Current published release: `v0.2.0-alpha.1`.
2. Starting Git state: clean aligned `master`/`origin/master` at `8dadd16`.
3. TASK-0101 through TASK-0115 are complete and pushed.
4. TASK-0116 through TASK-0125 task files exist and must be executed in order without per-task prompts.
5. After successful validation, normal commits and pushes are automatic for this control task.
6. After the exact release commit passes 8/8 hosted checks, the authorized release workflow may publish NuGet `0.2.0-alpha.2`, create tag `v0.2.0-alpha.2`, and create the GitHub pre-release.
7. Publication credentials must use GitHub OIDC Trusted Publishing only. Secret values must never be displayed, logged, persisted, or committed.
8. Never force push, rewrite history, move an existing tag, overwrite an immutable NuGet version, or delete user changes.
9. Keep generated `.ackit/`, SARIF/HTML, package, archive, `bin/`, `obj/`, `TestResults`, and coverage outputs untracked.
10. TASK-0116 through TASK-0122 are complete locally with 186/186 tests and green contract/performance/package gates.
11. Commit/push the PowerShell 5.1 link-check compatibility fix, then require a fresh exact-SHA 8/8 before re-dispatching the release workflow.
12. Published-package smoke remains on `0.2.0-alpha.1` until NuGet alpha.2 publication succeeds; source-package smoke moves with the alpha.2 candidate.
13. `docs/V100_GAP_ANALYSIS.md`, `docs/RELEASE_CANDIDATE_CONTRACT_FREEZE.md`, and `docs/MAINTAINER_RC_DECISION.md` remain the historical 1.0/RC evidence boundary; PROJECT-CONTROL-0102 authorizes only the alpha.2 release described here.
