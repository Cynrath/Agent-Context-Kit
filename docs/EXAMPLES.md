# Examples

## Start In A Repository
```powershell
ackit init --lang en
ackit scan
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
ackit redact-check --profile public-release --json
ackit doctor --json
```

## Local Package Validation
```powershell
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```
