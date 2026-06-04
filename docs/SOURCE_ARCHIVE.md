# Source Archive Hygiene

This document separates GitHub source readiness from local ZIP/RAR archive sharing.

## GitHub Source Package
GitHub source archives are created from tracked repository content. They do not include the local `.git/` database, untracked local build outputs, or ignored temporary artifacts.

For a normal GitHub push, these local folders are not blockers when they are untracked:
- `.git/`
- `.ackit/`
- `src/*/bin/`
- `src/*/obj/`
- `tests/*/bin/`
- `tests/*/obj/`
- `TestResults/`
- `coverage/`

The repository `.gitignore` covers build outputs, test output, coverage, logs, temp files, local `.ackit/` outputs, archives, packages, local secrets, dumps, and OS files. The `.git/` directory is not a `.gitignore` concern because Git never tracks its own internal repository database.

## Local ZIP/RAR Archive
Local archives created from the working directory can accidentally include untracked or ignored directories. Before sharing a local ZIP/RAR, exclude repository internals, build artifacts, generated local outputs, packages, logs, temp files, and local secrets.

Recommended WinRAR exclude list:

```text
.git* .ackit* *\bin* *\obj* *\TestResults* *\coverage* *\logs* *\publish* *\out* *.user *.suo *.log *.binlog *.trx *.coverage *.coveragexml *.tmp *.bak *.zip *.rar *.7z *.nupkg *.snupkg .env .env.* secrets.json appsettings.Production.json appsettings.Staging.json appsettings.Local.json
```

The same pattern is stored in `winrar_exclude.txt` for copy-paste use.

## Intentional AI Instruction Files
The repository intentionally includes agent instruction files:
- `AGENTS.md`
- `CLAUDE.md`
- `.cursor/rules/project.mdc`
- `.github/copilot-instructions.md`

`.cursor/rules/project.mdc` is a deliberate repository-specific AI instruction file. It should stay in the public source unless a maintainer explicitly decides to remove Cursor support.

## Local Cleanup Check
Before creating a local source archive:

```powershell
git status --short --ignored
Get-ChildItem -Path . -Directory -Force -Recurse | Where-Object { $_.Name -in @('bin','obj','.ackit','TestResults','coverage') } | Select-Object FullName
```

If ignored `bin/`, `obj/`, or `.ackit/` folders are empty local artifacts, they can be removed before archiving. If `obj/` was removed, run `dotnet restore AgentContextKit.sln` before using `dotnet build --no-restore`; otherwise `NETSDK1004` is expected because restore assets were intentionally deleted.

## Public Release Boundary
Source archive hygiene does not approve public release. Public release still requires the release tag to point at the reviewed commit plus explicit maintainer approval for push and NuGet publish.
