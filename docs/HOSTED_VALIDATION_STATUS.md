# Hosted Validation Status

The alpha.2 post-publish commit passed the standard hosted matrix. The separate manual 1.0 RC evidence workflow maps to blocker `RB-001` in `docs/RELEASE_BLOCKER_BOARD.md` and remains maintainer-gated.

## Current Commit
- Branch: `master`
- Commit: `ead65120928835419fb91bf695e845721620c394`
- Local/remote state observed on 2026-06-13: `master` equals `origin/master`.
- Commit title: `chore: complete v0.2.0-alpha.2 post-publish sync`

## Successful Standard Workflows
The following public GitHub Actions runs completed successfully on 2026-06-13 for the exact commit above.

| Workflow | Run | Hosted Scope | Result |
| --- | --- | --- | --- |
| `ci` | [27471224858](https://github.com/Cynrath/agent-context-kit/actions/runs/27471224858) | Restore, build, test, and self-scan on `windows-2025` and `ubuntu-latest` | SUCCESS |
| `cross-platform-smoke` | [27471224861](https://github.com/Cynrath/agent-context-kit/actions/runs/27471224861) | Published `AgentContextKit` `0.2.0-alpha.2` smoke on Windows, Ubuntu, and macOS | SUCCESS |
| `cross-platform-source-smoke` | [27471224867](https://github.com/Cynrath/agent-context-kit/actions/runs/27471224867) | Source restore/build/test, local alpha.2 package install, and smoke on Windows, Ubuntu, and macOS | SUCCESS |

## Evidence Value
These runs verify that:
- the current commit restores, builds, tests, and self-scans on hosted Windows and Ubuntu runners;
- the published package remains installable and usable on Windows, Ubuntu, and macOS;
- the current source package builds, installs, and completes its source smoke flow on Windows, Ubuntu, and macOS.

This evidence strengthens runtime/platform and package portability confidence. It does not verify the dedicated release-candidate predecessor upgrade flow.

## Missing Manual RC Evidence
The active `release-candidate-evidence` workflow had **zero runs** when checked on 2026-06-13.

The following remain unverified in hosted Actions:
- isolated predecessor and source-candidate tool comparison;
- predecessor config hash immutability;
- current-source `config-check` against the predecessor fixture;
- baseline create/load/classification flow;
- baseline-aware SARIF parse;
- the 2,000-file performance tripwire on all three hosted operating systems.

Therefore `docs/MAINTAINER_RC_DECISION.md` remains **NO-GO for release-candidate publication**.

## Maintainer Dispatch Packet
The following command is documentation only. Running it is a remote write and requires explicit maintainer action.

```powershell
gh workflow run release-candidate-evidence.yml --repo Cynrath/agent-context-kit --ref master
```

Read-only result discovery:

```powershell
gh run list --repo Cynrath/agent-context-kit --workflow release-candidate-evidence.yml --limit 5
gh run view <run-id> --repo Cynrath/agent-context-kit --json headSha,status,conclusion,url,jobs
```

Record the exact run URL, commit SHA, each OS result, benchmark elapsed time, predecessor version, source-candidate package version, and any accepted runner warning in `docs/SECURITY_SUPPLY_CHAIN_EVIDENCE.md` or a dedicated result-sync task. Do not record secrets or raw environment data.

## Remote Boundary
This status was produced from read-only Git and GitHub CLI queries. It did not dispatch workflows, edit settings, upload artifacts/SARIF, push, tag, create releases, or publish packages.
