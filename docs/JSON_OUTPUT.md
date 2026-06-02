# JSON Output

AgentContextKit supports machine-readable JSON output for automation and CI usage.

Supported commands:
- `ackit init --json`
- `ackit scan --json`
- `ackit generate --json`
- `ackit task "<title>" --json`
- `ackit redact-check --json`
- `ackit doctor --json`

## Common Fields
JSON responses include:

```json
{
  "schemaVersion": 1,
  "toolVersion": "0.1.0-alpha.1",
  "command": "scan"
}
```

`schemaVersion` describes the JSON output shape, not the repository config format. The schema is early and can change before `1.0.0`.

## Exit Codes
Human output and JSON output use the same exit code strategy.

`redact-check`:
- `0`: no findings
- `1`: warning findings
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
  "schemaVersion": 1,
  "toolVersion": "0.1.0-alpha.1",
  "command": "scan",
  "repositoryPath": "...",
  "fileCount": 12,
  "stacks": [],
  "health": {},
  "findings": []
}
```
