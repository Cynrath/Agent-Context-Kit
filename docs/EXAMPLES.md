# Examples

## Start In A Repository
```powershell
ackit init --lang en
ackit scan
ackit scan --ci
ackit doctor
```

## Create AI Agent Files
```powershell
ackit generate --target all --lang en
```

Existing files are skipped. Review every generated file before committing.

## Create A Task
```powershell
ackit task "Add role based authorization checks" --lang en
```

## Public Release Review
```powershell
ackit scan
ackit redact-check --profile public-release
ackit doctor
```

## CI-Friendly JSON
```powershell
ackit scan --json
ackit scan --ci --json
ackit redact-check --profile public-release --json
ackit doctor --json
```

## HTML Report
```powershell
ackit report --output .ackit/reports/current.html --json
```

## Web UI Prototype
```powershell
ackit webui --output .ackit/webui/current.html --json
```

## Dry-Run Prompt Pack
```powershell
ackit prompt-pack --output .ackit/prompt-packs/current.md --json
```

Review the generated Markdown locally before any future context export. The command does not call a remote provider.

## User-Approved Context Export Manifest
```powershell
ackit context-export --prompt-pack .ackit/prompt-packs/current.md --approve --output .ackit/context-exports/current.json --json
```

The manifest records local approval only. It does not upload the prompt pack or call a remote provider.

## Local Package Validation
```powershell
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

## Scan Samples
```powershell
Push-Location samples/dotnet-minimal-api
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
Pop-Location

Push-Location samples/node-tooling
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan
Pop-Location
```

See [SAMPLES.md](SAMPLES.md).

## Workflow Collections
See [EXAMPLE_WORKFLOWS.md](EXAMPLE_WORKFLOWS.md) for copy-paste-ready local development, CI, HTML report, Web UI, prompt pack, public release preflight, and sample scanning workflows.
