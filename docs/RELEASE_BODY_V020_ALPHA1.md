# GitHub Release Body Draft: v0.2.0-alpha.1

This is the maintainer-ready replacement body for the already published `v0.2.0-alpha.1` GitHub pre-release. It is documentation only; it does not edit GitHub.

## AgentContextKit v0.2.0-alpha.1

`v0.2.0-alpha.1` is a published GitHub and NuGet pre-release for AgentContextKit.

This release adds the first published SARIF-capable global tool package and expands the scanner/reporting foundation for safer AI coding agent workflows.

### Added
- Added `ackit sarif --output <repo-relative.sarif>` for SARIF 2.1.0 output in the published NuGet package.
- Added scanner rule catalog with stable `ACKIT` rule IDs.
- Added additive JSON `ruleId` metadata.
- Added configurable non-Critical allowlist fields:
  - `safeDomains`
  - `ignoredPaths`
  - `ignoredFindingIds`
- Added expanded scanner patterns for package artifacts, provider-token-like values, bearer-like values, Unix home paths, and file URI leakage.
- Added sample gallery, demo scenarios, and sample smoke helper.
- Added Web UI preview and visual asset guidance.

### Changed
- SARIF rule metadata now comes from the centralized scanner rule catalog.
- Published-package smoke validates `AgentContextKit` `0.2.0-alpha.1`.
- Source-package smoke validates the current branch before future package publication.
- Documentation now treats `0.2.0-alpha.1` as the current published pre-release and `0.1.0-alpha.2` as the previous release.

### Security
- Critical findings cannot be silently suppressed by config allowlists.
- SARIF output avoids raw secret matches and absolute local paths.
- Local `.ackit` reports, Web UI output, SARIF files, and generated artifacts remain local-only by default.
- GitHub Code Scanning upload remains documentation-only until maintainer opt-in.

### Validation
- `dotnet build -c Release`: passed.
- `dotnet test -c Release`: 83/83 passed.
- `ackit scan --ci`: passed with no risk findings.
- `ackit doctor`: PASS.
- Local package smoke: passed.
- NuGet global tool install verified.
- `ackit version`: `AgentContextKit 0.2.0-alpha.1`.
- `ackit --help`: includes `sarif`.
- `ackit sarif`: generated parseable SARIF from the published package.
- Hosted `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke`: passed on `master`.

### Install

```powershell
dotnet tool install --global AgentContextKit --version 0.2.0-alpha.1
ackit version
ackit --help
ackit sarif --output .ackit/reports/ackit.sarif
```

### Notes
This is a pre-release alpha package. APIs, output shape, and scanner behavior may continue to evolve before `1.0.0`.

## Maintainer Action
If desired, copy this body into the GitHub Release page manually. Do not edit the GitHub Release from an agent session without explicit maintainer approval.
