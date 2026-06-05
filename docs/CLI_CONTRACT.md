# CLI Contract

This page records the intended stable v1.0 command contract for `ackit`. It is a local contract review aid, not public release approval.

## Command Name
The packaged tool command name is:

```text
ackit
```

During development, run the CLI through:

```powershell
dotnet run --project src/AgentContextKit.Cli -- <command>
```

## Stable Command Surface
The v1.0 target command surface is:

```text
ackit init [--lang en|tr] [--json]
ackit scan [--lang en|tr] [--json] [--ci]
ackit sarif --output <repo-relative.sarif> [--lang en|tr] [--json]
ackit report [--output <repo-relative.html>] [--lang en|tr] [--json]
ackit webui [--output <repo-relative.html>] [--lang en|tr] [--json]
ackit prompt-pack [--output <repo-relative.md>] [--lang en|tr] [--json]
ackit context-export --prompt-pack <repo-relative.md> --approve [--output <repo-relative.json>] [--lang en|tr] [--json]
ackit generate [--target codex|claude|cursor|copilot|all] [--lang en|tr] [--json]
ackit task "<title>" [--lang en|tr] [--json]
ackit redact-check [--profile public-release] [--lang en|tr] [--json]
ackit doctor [--lang en|tr] [--json]
ackit version
ackit help
```

## Stability Rules
- Keep command names stable before v1.0 release.
- Keep documented options stable unless a task explicitly documents a breaking pre-v1.0 change.
- Keep repository-relative output path behavior for generated files.
- Keep skip-existing-file behavior as the default.
- Keep offline-first behavior as the default.
- Keep public release actions outside the CLI command contract.

## Global Options
- `--lang en|tr`: selects English or Turkish output/templates where supported. Unknown language values fall back to English.
- `--json`: emits machine-readable JSON where supported.
- `--ci`: applies only to `scan` and turns high/critical findings into non-zero process exits.

## Output Paths
Generated local artifacts must stay repository-relative:
- `.ackit/reports/scan-report.html`
- `.ackit/reports/ackit.sarif`
- `.ackit/webui/index.html`
- `.ackit/prompt-packs/prompt-pack.md`
- `.ackit/context-exports/context-export-manifest.json`
- `docs/tasks/TASK-####.md`

Generated artifact directories under `.ackit/` are ignored by default.

## Exit Behavior
Exit codes are documented in `docs/EXIT_CODES.md`.

Stable expectations:
- `0`: command completed without a blocking condition.
- `1`: invalid invocation, command error, warning-level blocking condition, failed high/critical doctor check, or high-risk CI condition.
- `2`: critical risk condition.

`scan` remains report-only by default. Use `scan --ci` for automation that should fail on high or critical findings.

## JSON Contract
JSON output is documented in `docs/JSON_OUTPUT.md`.

Stable expectations:
- JSON output includes `schemaVersion`, `toolVersion`, `generatedAtUtc`, and `command` where supported.
- Repository-scoped JSON output includes repository metadata.
- Human output and JSON output use the same process exit code.
- JSON schema can still be revised before v1.0; v1.0 should freeze or explicitly version any breaking changes.

## Safety Boundary
The stable CLI contract does not include:
- GitHub push.
- Remote creation.
- Release tag creation.
- NuGet publish.
- Live LLM provider calls.
- Provider SDK setup.
- API key read, storage, generation, or validation.
- Repository upload.
- GitHub Code Scanning upload.
- Automatic redaction or deletion.

These remain maintainer-only or future explicitly documented tasks.

## Local Contract Check
Run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1
```

Run as a failing gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues
```

The script checks local help output and release-critical documentation. It does not push, publish, tag, upload, redact, delete, call providers, handle API keys, or create remotes.
