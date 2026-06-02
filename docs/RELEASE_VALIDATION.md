# Release Validation

This checklist validates local release readiness without publishing.

## Required Commands
```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli -- scan
dotnet run --project src/AgentContextKit.Cli -- doctor
```

## Package Validation
```powershell
$stamp = Get-Date -Format "yyyyMMddHHmmss"
$pkg = Join-Path $env:TEMP "ackit-nupkg-$stamp"
$tools = Join-Path $env:TEMP "ackit-tools-$stamp"
New-Item -ItemType Directory -Force -Path $pkg, $tools | Out-Null

dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
dotnet tool install AgentContextKit --tool-path $tools --add-source $pkg --version 0.1.0-alpha.1 --ignore-failed-sources
& (Join-Path $tools "ackit.exe") --help
& (Join-Path $tools "ackit.exe") scan --json
```

## Manual Release Gates
- Confirm `RepositoryUrl` points to the real public repository.
- Confirm package README renders correctly.
- Confirm license and security policy are current.
- Confirm no secrets, dumps, backups, uploads, `bin/`, `obj/`, or generated package outputs are committed.
- Confirm no permanent global tool install is required for validation.
