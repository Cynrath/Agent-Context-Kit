# Packaging

AgentContextKit is designed to be packaged as a .NET tool with command name `ackit`.

## Package Metadata
Current package metadata is defined in `src/AgentContextKit.Cli/AgentContextKit.Cli.csproj`.

Important fields:
- `PackageId`: `AgentContextKit`
- `ToolCommandName`: `ackit`
- `Version`: `0.1.0-alpha.1`
- `Authors`: `Cynrath`
- `PackageLicenseExpression`: `MIT`
- `PackageReadmeFile`: `README.md`
- `RepositoryType`: `git`

`RepositoryUrl` and `PackageProjectUrl` intentionally use TODO placeholders until the public remote is selected.

Run the dedicated metadata review before pack or publish checks:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
```

Use the failing gate only after maintainer-selected public URLs are in place:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
```

See [NUGET_METADATA.md](NUGET_METADATA.md) for the field-by-field package metadata review.
See [RELEASE_BLOCKERS.md](RELEASE_BLOCKERS.md) before any public publish.

## Local Pack
```powershell
$pkg = Join-Path $env:TEMP "ackit-nupkg"
New-Item -ItemType Directory -Force -Path $pkg | Out-Null
dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
```

## Temporary Tool Install
```powershell
$tools = Join-Path $env:TEMP "ackit-tools"
New-Item -ItemType Directory -Force -Path $tools | Out-Null
dotnet tool install AgentContextKit --tool-path $tools --add-source $pkg --version 0.1.0-alpha.1 --ignore-failed-sources
& (Join-Path $tools "ackit.exe") --help
```

## Release Blockers
- Do not publish to NuGet until the real repository URL is set.
- Do not publish until `scripts/check-package-metadata.ps1 -FailOnIssues` exits `0`.
- Do not publish until `scripts/check-release-blockers.ps1 -FailOnBlockers` exits `0`.
- Do not publish until restore/build/test/pack/tool-path validation passes.
- Do not publish while `ackit scan` reports unaccepted high or critical findings.
