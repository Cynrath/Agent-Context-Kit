# Release Candidate Hosted Evidence

## Purpose
`.github/workflows/release-candidate-evidence.yml` is a manual-only Windows, Ubuntu, and macOS evidence workflow for a future release-candidate decision. It does not publish or approve a release.

## Current Hosted Status
Standard hosted validation for commit `37d52200fead0ce5c53571205d324b9b7ff6c75b` is green: `ci`, published-package smoke, and source-package smoke succeeded. See `docs/HOSTED_VALIDATION_STATUS.md` for run URLs and exact OS scope.

The dedicated `release-candidate-evidence` workflow still has zero runs. Standard smoke results do not replace its predecessor-config, baseline, SARIF, and performance evidence.

## What It Verifies
- restore, Release build, and the full test suite on each runner;
- published predecessor `AgentContextKit` `0.2.0-alpha.1` install in an isolated tool path;
- current source pack/install with a run-unique prerelease package version in a second tool path;
- predecessor config readability and unchanged config content hash;
- current-source `config-check` success for the predecessor fixture;
- current-source baseline create/load/classification behavior;
- baseline-aware SARIF generation and JSON parse;
- final clean scan;
- 2,000-file synthetic performance tripwire within 30 seconds.

## Safety And Permissions
- Trigger: `workflow_dispatch` only.
- Permission: `contents: read` only.
- Repository data: committed source plus sanitized synthetic/upgrade fixtures only.
- Artifact upload: disabled.
- SARIF/Code Scanning upload: disabled.
- Secrets and publishing credentials: not used.

The candidate package receives a run-unique local package version. This prevents the candidate install from being satisfied by the published predecessor package when source and public metadata still share a base version.

## Local Review
```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-release-candidate-workflow.ps1 -FailOnIssues
```

This static gate verifies required markers and rejects push/PR triggers, uploads, write permissions, and secret references. It cannot prove hosted runner behavior.

Local Windows reproduction on 2026-06-12 passed the isolated predecessor/source package install, predecessor scan JSON, current-source `config-check`, config hash immutability, baseline scan, SARIF parse, and final scan. The first run exposed fixture self-noise; the sanitized predecessor fixture now ignores only its own non-Critical `.ackit/config.yml` keyword matches while Critical findings remain unsuppressible.

No local `actionlint` or YAML parser was available. Static marker checks and local PowerShell smoke passed, but GitHub workflow syntax/runner behavior still requires the manual hosted dispatch.

## Maintainer Execution
After the commit is pushed, open GitHub Actions and manually dispatch `release-candidate-evidence` from the reviewed branch. Record:
- workflow URL and commit SHA;
- each OS result;
- predecessor and candidate package versions;
- benchmark elapsed time from each job;
- any accepted runner-specific warning or failure.

Equivalent maintainer command:

```powershell
gh workflow run release-candidate-evidence.yml --repo Cynrath/agent-context-kit --ref master
```

This command is intentionally documented but was not executed by TASK-0097.

Do not treat a local pass as hosted evidence. Do not tag, publish, or call the project RC-ready until all required jobs are green and remaining P0/P1 decisions are resolved or explicitly accepted.

## Failure Interpretation
- Predecessor install failure can indicate NuGet/network availability rather than source behavior.
- Candidate install/help failure indicates package or command-surface drift.
- Config hash change is a blocking mutation regression.
- Config/baseline/SARIF failure is a compatibility or privacy-contract regression.
- Benchmark threshold failure requires investigation; the threshold is a regression tripwire, not an SLA.

## Remote Boundary
This task only adds the workflow and documentation locally. Push and manual dispatch remain maintainer actions. No GitHub settings, release, tag, package, artifact, or Code Scanning state is changed.
