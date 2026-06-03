# Samples

AgentContextKit includes small sample repositories under `samples/`.

The samples are designed for local scan and generated-doc experiments. They intentionally avoid secrets, environment files, uploads, backups, private keys, package artifacts, and generated build outputs.

## Available Samples
- `samples/dotnet-minimal-api`: .NET Minimal API stack detection.
- `samples/node-tooling`: Node, TypeScript, and Tailwind CSS stack detection.

## Scan The .NET Sample
```powershell
Push-Location samples/dotnet-minimal-api
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
Pop-Location
```

Expected stack signals:
- `.NET`
- `ASP.NET Core`
- `ASP.NET Core Minimal API`

## Scan The Node Tooling Sample
```powershell
Push-Location samples/node-tooling
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
Pop-Location
```

Expected stack signals:
- `Node`
- `TypeScript`
- `Tailwind CSS`

## Safety
Do not add secrets, `.env` files, dumps, backups, uploads, private keys, package outputs, `node_modules`, `bin`, or `obj` directories to samples.
