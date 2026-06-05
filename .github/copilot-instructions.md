# Copilot Instructions

Prefer minimal, tested, secure changes that follow the project docs and task files.

## Repository Health
- README: yes
- LICENSE: yes
- SECURITY: yes
- Tests: yes
- CI: yes
- Agent instructions: yes

## Release Status
- Current release: `v0.1.0-alpha.2` published on GitHub and NuGet.
- Main stack: `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.

## Recommended Checks
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- doctor`
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
