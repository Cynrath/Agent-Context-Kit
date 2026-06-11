# Troubleshooting

## `dotnet --info` prints an exception
The local environment may still build and test successfully. Prefer the release validation script:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

If build/test fails, capture the exact command and output.

## `dotnet tool install` finds another package source
Use an isolated temp source and exact version:

```powershell
dotnet tool install AgentContextKit --tool-path <temp-tools> --add-source <temp-nupkg> --version 0.2.0-alpha.1 --ignore-failed-sources
```

## PowerShell cannot run scripts
Use:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

## `ackit scan` reports false positives
Review the finding first. If it is safe to suppress, add a targeted rule to `.ackit/config.yml`:

```yaml
ignorePaths:
  - generated/
```

Avoid broad rules such as `src/`.

## `ackit generate` skips files
This is expected. Existing files are not overwritten by default.

## JSON output changes
JSON output is alpha. Treat `schemaVersion` as early and review `docs/JSON_OUTPUT.md` before relying on it in CI.
