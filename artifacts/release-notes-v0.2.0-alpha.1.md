## AgentContextKit v0.2.0-alpha.1

This alpha release adds the first SARIF-capable source package candidate and expands AgentContextKit's scanner/reporting foundation.

### Added
- Added `ackit sarif --output <repo-relative.sarif>` for SARIF 2.1.0 output.
- Added scanner rule catalog with stable `ACKIT` rule IDs.
- Added additive JSON `ruleId` metadata.
- Added configurable allowlist fields:
  - `safeDomains`
  - `ignoredPaths`
  - `ignoredFindingIds`
- Added expanded scanner patterns for package artifacts, provider-token-like values, bearer-like values, Unix home paths, and file URI leakage.
- Added sample gallery, demo scenarios, and sample smoke helper.
- Added Web UI preview and visual asset guidance.

### Changed
- SARIF rule metadata now comes from the centralized scanner rule catalog.
- Source package smoke now validates the `0.2.0-alpha.1` candidate package.
- Documentation now distinguishes the published `0.1.0-alpha.2` package from the `0.2.0-alpha.1` release candidate.

### Security
- Critical findings cannot be silently suppressed by config allowlists.
- SARIF output avoids raw secret matches and absolute local paths.
- Local `.ackit` reports, Web UI output, SARIF files, and generated artifacts remain local-only by default.

### Validation
- `dotnet build -c Release`: passed.
- `dotnet test -c Release`: 83/83 passed.
- `ackit scan --ci`: passed with no risk findings.
- `ackit doctor`: PASS.
- Local package smoke: passed.
- NuGet global tool install verified.
- `ackit version`: `AgentContextKit 0.2.0-alpha.1`.
- `ackit --help`: includes `sarif`.

### Install

```powershell
dotnet tool install --global AgentContextKit --version 0.2.0-alpha.1
Notes

This is a pre-release alpha package. APIs, output shape, and scanner behavior may continue to evolve before 1.0.0.
