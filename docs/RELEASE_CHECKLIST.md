# Release Checklist

## Local Validation
- `dotnet restore` passes.
- `dotnet build -c Release` passes.
- `dotnet test -c Release` passes.
- `ackit doctor` has no unaccepted high/critical findings.
- `ackit redact-check --profile public-release` reviewed.
- README and README.tr are current.
- Configuration and JSON output docs are current.
- SECURITY, CONTRIBUTING, CODE_OF_CONDUCT, CHANGELOG are current.
- LICENSE is present.
- NuGet package metadata is reviewed.
- `scripts/check-package-metadata.ps1 -FailOnIssues` exits `0`.
- Local `dotnet pack` succeeds.
- Temporary `dotnet tool install --tool-path` verification succeeds.
- `scripts/verify-release.ps1` succeeds locally.
- `scripts/audit-public-release.ps1 -FailOnIssues` exits `0`.
- `scripts/check-release-blockers.ps1 -FailOnBlockers` exits `0`.
- `docs/PACKAGING.md` and `docs/RELEASE_VALIDATION.md` are current.
- `docs/RELEASE_BLOCKERS.md` has no unresolved public-release blockers.
- `docs/MAINTAINER_RELEASE_HANDOFF.md` has been reviewed.
- `RepositoryUrl` and `PackageProjectUrl` point to `https://github.com/Cynrath/agent-context-kit`.
- Local current release tag exists and points at the reviewed release commit.
- No secrets, dumps, uploads, backups, `bin/`, `obj/`, or generated junk are committed.

## Completed Public Release State
- Public repository exists: `https://github.com/Cynrath/agent-context-kit`.
- `master` is pushed.
- `v0.1.0-alpha.2` is pushed.
- GitHub Actions latest `master` run is green.
- Repository description is set.
- Repository topics are set.
- GitHub Release page for `v0.1.0-alpha.2` is created.
- NuGet package `AgentContextKit` version `0.1.0-alpha.2` is published.
- NuGet global tool install is verified for `0.1.0-alpha.2`.
- NuGet global tool smoke test is verified in a clean demo app.
- Cross-platform CI smoke workflow succeeded on Windows, Ubuntu, and macOS.
- Codex for OSS form submission is completed per maintainer-provided status.

## Alpha.2 Published State
- Scanner fixture/domain-like noise reduction is implemented locally.
- GitHub Actions Node 24 readiness is implemented locally.
- Turkish human CLI output polish is implemented locally.
- Source/package metadata and CLI runtime version are `0.1.0-alpha.2`.
- Local alpha.2 pack and temporary tool-path smoke passed before publication.
- `v0.1.0-alpha.2` tag, GitHub Release, NuGet publish, and install verification are complete.
- Hosted `ci`, published-package smoke, and source smoke validation remain maintainer checks after future pushes.

## v0.2.0-alpha.1 Candidate State
- Source/package metadata and CLI runtime version are prepared as `0.2.0-alpha.1`.
- `ackit sarif`, SARIF 2.1.0 output, scanner rule catalog, configurable allowlist fields, additive JSON `ruleId`, expanded scanner patterns, sample gallery, demo scenarios, Web UI preview docs, and visual asset guidance are candidate release content.
- Published install commands remain pinned to `0.1.0-alpha.2` until `0.2.0-alpha.1` is published.
- Before publication, maintainers must push the release-prep commit, confirm hosted CI/source smoke, create the `v0.2.0-alpha.1` tag, create the GitHub Release, publish NuGet, and verify install.

## Remaining Manual Actions
- Review all generated files before future publishing.
- For the next release, approve push, hosted CI/source smoke validation, tag, GitHub Release, NuGet publish, and NuGet install verification in a dedicated release task.
