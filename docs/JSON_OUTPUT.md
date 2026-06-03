# JSON Output

AgentContextKit supports machine-readable JSON output for automation and CI usage.

Supported commands:
- `ackit init --json`
- `ackit scan --json`
- `ackit report --json`
- `ackit webui --json`
- `ackit generate --json`
- `ackit task "<title>" --json`
- `ackit redact-check --json`
- `ackit doctor --json`

## Common Fields
JSON responses include:

```json
{
  "schemaVersion": 2,
  "toolVersion": "0.1.0-alpha.1",
  "generatedAtUtc": "2026-06-03T00:00:00+00:00",
  "command": "scan"
}
```

`schemaVersion` describes the JSON output shape, not the repository config format. The schema is early and can change before `1.0.0`.

## Schema Version 2
Schema version `2` adds:
- `generatedAtUtc` on JSON command outputs.
- `repositoryName` on repository-scoped outputs.
- `riskSummary` on `scan` and `redact-check`.
- `checkSummary` on `doctor`.
- `fileSummary` on `generate`.
- `ciMode` and `exitCode` on `scan`.
- `report` generated file metadata on `report`.
- `webUi` generated file metadata on `webui`.

## Exit Codes
Human output and JSON output use the same exit code strategy.

See [EXIT_CODES.md](EXIT_CODES.md) for the full exit code matrix.

`redact-check`:
- `0`: no findings
- `1`: warning findings
- `2`: critical findings

`scan --ci`:
- `0`: no high/critical findings
- `1`: high findings and no critical findings
- `2`: critical findings

`doctor`:
- `0`: no failed high/critical checks
- `1`: at least one failed high/critical check

## Example
```powershell
dotnet run --project src/AgentContextKit.Cli -- scan --json
```

Example shape:
```json
{
  "schemaVersion": 2,
  "toolVersion": "0.1.0-alpha.1",
  "generatedAtUtc": "2026-06-03T00:00:00+00:00",
  "command": "scan",
  "ciMode": false,
  "exitCode": 0,
  "repositoryPath": "...",
  "repositoryName": "agent-context-kit",
  "fileCount": 12,
  "stacks": [],
  "health": {},
  "riskSummary": {
    "total": 0,
    "critical": 0,
    "high": 0,
    "medium": 0,
    "low": 0,
    "info": 0
  },
  "findings": []
}
```

## Summary Shapes
`riskSummary`:
```json
{
  "total": 1,
  "critical": 1,
  "high": 0,
  "medium": 0,
  "low": 0,
  "info": 0
}
```

`checkSummary`:
```json
{
  "total": 13,
  "passed": 13,
  "failed": 0,
  "failedHighOrCritical": 0
}
```

`fileSummary`:
```json
{
  "total": 3,
  "created": 1,
  "skipped": 2
}
```

`webUi`:
```json
{
  "path": ".ackit/webui/index.html",
  "status": "Created",
  "created": true,
  "message": "Web UI prototype created."
}
```
