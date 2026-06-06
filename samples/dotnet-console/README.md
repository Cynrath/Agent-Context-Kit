# .NET Console Sample

This sample is a minimal .NET console repository for AgentContextKit scanner demos.

## Expected Stack
- `.NET`

## Expected Health Gaps
- No license file.
- No security policy.
- No tests.
- No CI workflow.
- No agent instruction files unless generated locally.

## Suggested Commands
```powershell
Push-Location samples/dotnet-console
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
Pop-Location
```

## Safety
This sample contains no secrets, generated artifacts, `bin/`, `obj/`, or package output.
