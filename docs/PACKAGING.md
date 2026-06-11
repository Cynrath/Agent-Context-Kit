# Packaging

AgentContextKit is designed to be packaged as a .NET tool with command name `ackit`.

## Package Metadata
Current package metadata is defined in `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`.

Important fields:
- `PackageId`: `AgentContextKit`
- `ToolCommandName`: `ackit`
- `Version`: `0.2.0-alpha.1`
- `Authors`: `Cynrath`
- `PackageLicenseExpression`: `MIT`
- `PackageReadmeFile`: `README.md`
- `RepositoryType`: `git`
- `RepositoryUrl`: `https://github.com/Cynrath/agent-context-kit`
- `PackageProjectUrl`: `https://github.com/Cynrath/agent-context-kit`

Run the dedicated metadata review before pack or publish checks:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
```

Use the failing gate before any public publish:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
```

See [NUGET_METADATA.md](NUGET_METADATA.md) for the field-by-field package metadata review.
See [RELEASE_BLOCKERS.md](RELEASE_BLOCKERS.md) before any public publish.

## Local Pack
```powershell
$pkg = Join-Path $env:TEMP "ackit-nupkg"
New-Item -ItemType Directory -Force -Path $pkg | Out-Null
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release -o $pkg
```

## Temporary Tool Install
```powershell
$tools = Join-Path $env:TEMP "ackit-tools"
New-Item -ItemType Directory -Force -Path $tools | Out-Null
dotnet tool install AgentContextKit --tool-path $tools --add-source $pkg --version 0.2.0-alpha.1 --ignore-failed-sources
& (Join-Path $tools "ackit.exe") --help
& (Join-Path $tools "ackit.exe") sarif --output .ackit/reports/local-package.sarif
```

## Public NuGet Install
Install the published package from NuGet:

```powershell
dotnet tool install --global AgentContextKit --version 0.2.0-alpha.1
ackit version
ackit --help
ackit scan --ci
```

## Current Published Package
Current source is published as the `0.2.0-alpha.1` package. The `v0.2.0-alpha.1` tag, GitHub pre-release, NuGet publish, and global tool install verification are complete.

## Manual NuGet Publish
NuGet publish is not automated by this project. Version `0.2.0-alpha.1` has been published and install-verified. Future versions must be published only from the reviewed release commit, only after all gates pass, and only with an approved API key stored outside the repository.

Future package push commands should use the next approved version, for example:

```powershell
dotnet nuget push (Join-Path $pkg "AgentContextKit.<next-version>.nupkg") --source https://api.nuget.org/v3/index.json --api-key $env:NUGET_API_KEY
```

## Release Blockers
- For future releases, do not publish until `scripts/check-package-metadata.ps1 -FailOnIssues` exits `0`.
- For future releases, do not publish until `scripts/check-release-blockers.ps1 -FailOnBlockers` exits `0`.
- For future releases, do not publish until restore/build/test/pack/tool-path validation passes.
- For future releases, do not publish while `ackit scan` reports unaccepted high or critical findings.
- For future releases, do not publish until GitHub Actions are green, the GitHub Release page is ready, and maintainer approval is explicit.
