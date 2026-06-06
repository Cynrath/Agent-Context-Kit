# GitHub Actions Usage

AgentContextKit can run in CI as a local repository hygiene and safety check. The repository workflows in `.github/workflows/` are the active workflows; files under `docs/examples/` are documentation-only examples to copy after review.

## Why Use AgentContextKit In CI?
- Catch high and critical scanner findings before release.
- Keep repository health checks visible for maintainers.
- Verify the published NuGet global tool works across platforms.
- Verify current source can be packed and installed before future publication.
- Optionally generate SARIF for review or GitHub Code Scanning after maintainer approval.

## Recommended Workflow Order
1. Run source restore/build/test.
2. Run `ackit scan --ci`.
3. Run `ackit doctor`.
4. For source-only features, pack the current branch and install from a temporary package source.
5. Generate SARIF only when the command is available from the selected package/source.
6. Upload SARIF only after privacy and repository-permission review.

## `scan --ci`
`ackit scan --ci` is the default CI safety check.

Exit behavior:
- `0`: no high or critical findings.
- `1`: high findings and no critical findings.
- `2`: critical findings.

Use the published package for stable CI checks:

```yaml
- run: dotnet tool install --global AgentContextKit --version 0.1.0-alpha.2
- run: echo "$HOME/.dotnet/tools" >> "$GITHUB_PATH"
- run: ackit scan --ci
```

See `docs/examples/github-actions-scan-ci.yml`.

## `doctor`
`ackit doctor` checks repository health signals such as README, license, security policy, tests, CI, `.gitignore`, agent instructions, and package metadata.

Use it after `scan --ci` so risk findings are handled first. A minimal demo app can fail `doctor` because it intentionally lacks full OSS metadata; that is expected health reporting, not necessarily a tool failure.

## SARIF Output
`ackit sarif --output .ackit/reports/ackit.sarif` is available in current source after `v0.1.0-alpha.2`. The published NuGet package `0.1.0-alpha.2` does not include this command.

Use one of these approaches:
- Wait for the next alpha package that includes SARIF, then install that package in CI.
- Pack the current branch locally and install from the temporary package source.

SARIF output is local-only by default. It uses repository-relative artifact locations and does not write raw scanner match values into result messages.

## Published Tool Vs Source Package Smoke
Published tool smoke validates the package users install from NuGet. It should stay pinned to the current published version:

```powershell
dotnet tool install --global AgentContextKit --version 0.1.0-alpha.2
ackit version
ackit --help
ackit scan --ci
```

Source package smoke validates the current branch before publication. Use it for features added after the latest package, including `ackit sarif`:

```powershell
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
dotnet tool install AgentContextKit --tool-path $tools --add-source $pkg --version 0.1.0-alpha.2 --ignore-failed-sources
```

See:
- `docs/examples/github-actions-published-tool-smoke.yml`
- `docs/examples/github-actions-source-package-smoke.yml`

## Cross-Platform Smoke
Use Windows, Ubuntu, and macOS smoke runs for tool behavior that may depend on paths, shells, generated files, or global tool installation.

The active repository workflows are:
- `.github/workflows/cross-platform-smoke.yml`: published package smoke.
- `.github/workflows/cross-platform-source-smoke.yml`: current branch/source package smoke.

## Privacy Notes
- The MVP does not upload repository content.
- `.ackit/` outputs are local generated artifacts and are ignored by git.
- HTML reports and Web UI files may contain local paths and should not be published as release artifacts.
- SARIF uses repository-relative paths and omits raw scanner match values, but maintainers should still review the SARIF file before upload.
- Do not add secrets, API keys, production config, dumps, backups, or private repository data to workflow logs or artifacts.

## Failure Interpretation
- `scan --ci` failure means scanner severity reached the CI threshold.
- `doctor` failure means repository health is incomplete; decide whether it is expected for a demo app or blocking for the real repository.
- Source package smoke failure can indicate package metadata, packing, path handling, generated output, or current source regressions.
- Published package smoke failure can indicate NuGet availability, global tool install, or package runtime behavior problems.
- SARIF parse/upload failure can indicate invalid SARIF shape, missing command availability, or insufficient workflow permissions.

## When To Upload SARIF
Upload SARIF only when:
- the repository owner wants GitHub Code Scanning integration;
- the generated SARIF artifact has been reviewed;
- `security-events: write` is intentionally granted;
- SARIF upload alerts are part of the maintainer workflow; and
- the workflow uses a package/source that actually includes `ackit sarif`.

## When Not To Upload SARIF
Do not upload SARIF when:
- using NuGet `0.1.0-alpha.2`, because it does not include `ackit sarif`;
- reviewing a private or sensitive repository without approval;
- findings include data that should not appear in a public security integration;
- repository permissions have not been reviewed; or
- the team only needs local CI pass/fail behavior from `scan --ci`.

## Example Workflows
- `docs/examples/github-actions-scan-ci.yml`
- `docs/examples/github-actions-sarif-upload.yml`
- `docs/examples/github-actions-published-tool-smoke.yml`
- `docs/examples/github-actions-source-package-smoke.yml`

These files are examples only. Copy them into `.github/workflows/` only after repository-specific review.
