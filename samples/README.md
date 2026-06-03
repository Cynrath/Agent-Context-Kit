# Samples

These sample repositories are small, safe fixtures for trying AgentContextKit scans.

They intentionally avoid secrets, environment files, uploads, backups, private keys, package artifacts, and generated build outputs.

## Samples
- `dotnet-minimal-api`: .NET Minimal API stack detection.
- `node-tooling`: Node, TypeScript, and Tailwind CSS stack detection.

## Scan Samples
From the repository root:

```powershell
Push-Location samples/dotnet-minimal-api
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
Pop-Location

Push-Location samples/node-tooling
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
Pop-Location
```
