# Release Automation

## Trigger
`.github/workflows/release.yml` is manual-only through `workflow_dispatch`. It requires `version`, exact `commit_sha`, and `prerelease` inputs. Pull requests, forks, pushes, and tags cannot start publication.

## Exact Commit Boundary
The validation job checks out the requested commit with full history, fetches `origin/master`, and fails unless the input SHA, checked-out HEAD, and current `origin/master` are identical. Version metadata must agree across the package project, CLI runtime, source-package smoke workflow, and release verification script.

## Validation Before Publication
The read-only validation job runs restore, Release build, tests, source scan, doctor, JSON/SARIF parse, sample smoke, Markdown links, contract/readiness/security gates, package metadata inspection, pack, package archive inspection, temporary tool installation, and installed-package smoke. Package files are transferred to the release job as a short-retention workflow artifact only after validation succeeds.

## Credential Boundary
The release job uses GitHub environment `nuget-release` and only that job receives:

- `contents: write` for the exact tag and GitHub pre-release;
- `id-token: write` for NuGet Trusted Publishing.

`NuGet/login@v1` requests a short-lived NuGet credential for user `Cyranth`. The output is used only by `dotnet nuget push`; it is not printed, written to a file, committed, or stored as a repository secret. No API key or `NuGet.Config` credential is supported.

## Idempotency And Partial Failure
- Concurrency serializes runs for the same version.
- An existing NuGet version is verified and not republished.
- An existing tag must target the exact requested commit or the workflow fails without moving it.
- If NuGet succeeds and GitHub Release creation later fails, rerun the same inputs. The workflow verifies the package, skips republish, verifies/creates the exact tag, and completes missing release assets.
- NuGet failure stops tag and release creation.

## Local Commands
```powershell
powershell -ExecutionPolicy Bypass -File scripts/prepare-release.ps1 -Version 0.2.0-alpha.2 -CommitSha (git rev-parse HEAD) -AllowDirty -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-workflow.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/verify-published-package.ps1 -Version 0.2.0-alpha.2
```

The published-package verifier uses a disposable tool path and repository, checks `version`/`--help`, exercises the installed command surface, expects exit code `2` for a synthetic secret, removes the fixture, and requires a final clean scan.
