# .NET Minimal API Sample

Safe sample repository for AgentContextKit stack detection.

Expected stack signals:
- .NET
- ASP.NET Core
- ASP.NET Core Minimal API

This sample is not added to the main solution and does not contain secrets or environment files.

## Expected Health Gaps
- No license file.
- No security policy.
- No tests.
- No CI workflow.
- No agent instruction files unless generated locally.

## Suggested Commands
```powershell
Push-Location samples/dotnet-minimal-api
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
Pop-Location
```

Main repository note: this sample should not make the root repository report ASP.NET Core or Minimal API as the main product stack.
