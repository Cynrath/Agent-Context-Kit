# Claude Project Context

Use the same repository rules as AGENTS.md.

## Stack
- .NET: .sln/.slnx/.csproj/Program.cs
- GitHub Actions: .github/workflows

## Repository Health
- README: yes
- LICENSE: yes
- SECURITY: yes
- Tests: yes
- CI: yes
- Agent instructions: yes

## Risk Summary
- No risk findings in the latest local scan.
- Public release remains blocked by TODO package URLs and missing release tag.

## Recommended Checks
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
