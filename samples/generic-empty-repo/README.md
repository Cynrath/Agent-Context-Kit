# Generic Empty Repository Sample

This sample simulates a nearly empty repository. It is useful for seeing how AgentContextKit reports missing repository-health signals.

## Expected Stack
- Unknown or no strong stack signal.

## Expected Health Gaps
- No license file.
- No security policy.
- No tests.
- No CI workflow.
- No `.gitignore`.
- No agent instruction files unless generated locally.

## Suggested Commands
```powershell
Push-Location samples/generic-empty-repo
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- doctor
Pop-Location
```

`doctor` can return a non-zero exit code here because the sample intentionally lacks release-quality repository metadata.

## Safety
This sample contains no secrets, generated artifacts, package output, or private repository content.
