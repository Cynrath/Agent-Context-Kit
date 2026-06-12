# AI Workflow

For repository adoption before the first agent task, use [Prepare A Repository For AI Coding Agents](PREPARE_REPOSITORY_FOR_AI_AGENTS.md).

1. Read README, architecture, security, and the active task.
2. Create or update a task before implementation.
3. Inspect git status and preserve user changes.
4. Make small, focused changes.
5. Run relevant checks.
6. Update docs, task completion notes, and handoff.
7. Commit a logical unit of work.

## Recommended Checks
- `dotnet build AgentContextKit.sln -c Release --no-restore`
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- report --json`
- `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`
