# Example Workflows

These workflows are local-only and offline-first. They do not push, publish, tag, create remotes, redact automatically, or upload repository content.

## Local Development Loop
Use this during normal implementation work:

```powershell
git status --short --branch
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- doctor
```

## CI Gate
Use this when high or critical scan findings should fail automation:

```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
```

The repository GitHub Actions workflow already runs the same CI scan after tests.

## HTML Report Review
Use this to create a local report for manual review:

```powershell
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- report --output .ackit/reports/current.html --json
```

Open `.ackit/reports/current.html` locally. Reports under `.ackit/reports/` are ignored by git.

## Public Release Preflight
Use this before asking the maintainer to approve public release actions:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

Expected while TODO package URLs remain:
- Package metadata report-only mode exits `0` but reports TODO URL blockers.
- Public release audit report-only mode exits `0` but reports maintainer-only blockers.
- Release blocker report-only mode exits `0` but reports maintainer-only blockers.

Only after the maintainer selects real public URLs and explicitly approves release actions should failing public gates be used:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

## Sample Repository Checks
Use these sample scans to validate stack detection examples:

```powershell
Push-Location samples/dotnet-minimal-api
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
Pop-Location

Push-Location samples/node-tooling
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
Pop-Location
```

## Generated Agent Context Refresh
Use this only when you intentionally want to create missing generated context files. Existing files are skipped:

```powershell
dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- generate --target all --lang en --json
```

Review every generated file before committing.
