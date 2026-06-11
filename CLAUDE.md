# Claude Project Context

Use the same repository rules as AGENTS.md.

## Stack
- .NET: .sln/.slnx/*proj/Program.cs
- .NET CLI / .NET Tool: PackAsTool/ToolCommandName
- GitHub Actions: .github/workflows

## Repository Health
- README: yes
- LICENSE: yes
- SECURITY: yes
- Tests: yes
- CI: yes
- Agent instructions: yes

## Release Status
- Current release: `v0.2.0-alpha.1` published and verified on GitHub and NuGet as a pre-release.
- Previous release: `v0.1.0-alpha.2` published and verified; pushed, released, and published.
- NuGet global tool install verification: completed.
- Published-package smoke workflow installs `AgentContextKit` `0.2.0-alpha.1`.
- Source-package smoke workflow installs the local `AgentContextKit` `0.2.0-alpha.1` package.

## Risk Summary
- No risk findings in the latest local scan.

## Recommended Checks
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- doctor`
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
