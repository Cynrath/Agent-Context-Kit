# CLI Reference

AgentContextKit CLI command name: `ackit`.

During development, run commands through:
```powershell
dotnet run --project src/AgentContextKit.Cli -- <command>
```

## Global Options
### `--lang en|tr`
Select output/template language. Unknown values fall back to `en`.

### `--json`
Emit machine-readable JSON where supported. Current JSON output schema version: `2`.

## Commands
### `ackit init`
Creates `.ackit/config.yml` if it does not exist.

```powershell
ackit init --lang en
ackit init --lang tr --json
```

Safe behavior:
- Does not overwrite existing config.
- Reports detected agent instruction files.

### `ackit scan`
Scans repository structure, stack signals, docs, CI, tests, agent files, and risk findings.

```powershell
ackit scan
ackit scan --json
```

Stack signals include .NET, ASP.NET Core, Razor/Razor Pages, Blazor WebAssembly, .NET Worker Service, ASP.NET Core Minimal API, Node, npm, pnpm, Yarn, Bun, TypeScript, Vite, Next.js, Nuxt, Angular, Tailwind CSS, Python, PHP/Laravel, Docker, GitHub Actions, and database/migration files when matching local files are present.

### `ackit generate`
Generates agent context/workflow files. Existing files are skipped.

```powershell
ackit generate --target codex --lang en
ackit generate --target all --lang en --json
```

Targets:
- `codex`
- `claude`
- `cursor`
- `copilot`
- `all`

### `ackit task`
Creates a structured task file under `docs/tasks`.

```powershell
ackit task "Add admin permission checks" --lang en
ackit task "Admin yetki kontrolleri ekle" --lang tr
```

### `ackit redact-check`
Reports secret, PII, brand, local path, and production config risks.

```powershell
ackit redact-check --profile public-release
ackit redact-check --profile public-release --json
```

Exit codes:
- `0`: no findings
- `1`: warning findings
- `2`: critical findings

### `ackit doctor`
Reports repository health.

```powershell
ackit doctor
ackit doctor --json
```

### `ackit version`
Prints the CLI version.

### `ackit help`
Prints help.
