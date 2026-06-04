# TASK-0042: Public GitHub Readiness

## Status
Completed.

## Purpose
Prepare the repository for a clean first public GitHub push without leaking local artifacts, generated outputs, private metadata, or maintainer identity details.

## Scope
- Review `.gitignore` and `winrar_exclude.txt`.
- Confirm source archive guidance separates GitHub source packages from local ZIP/RAR archives.
- Clean safe local generated artifact folders when present.
- Scan tracked files for archives, package outputs, local secrets, production appsettings, generated junk, and prohibited real-name terms.
- Document `.cursor/rules/project.mdc` as an intentional public AI instruction file.
- Update public README guidance, release docs, and handoff docs where needed.

## Affected Files
- `.gitignore`
- `winrar_exclude.txt`
- `README.md`
- `README.tr.md`
- `docs/SOURCE_ARCHIVE.md`
- `docs/SOURCE_HYGIENE.md`
- `docs/MAINTAINER_RELEASE_HANDOFF.md`
- `docs/OSS_READINESS.md`
- `docs/PROJECT_MAP.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None.

## SEO/i18n Impact
README and README.tr public-facing wording may change. No runtime localization behavior changes.

## Audit/Security Impact
High release-safety impact. Prevents accidental publication of `.git/`, `.ackit/`, `bin/`, `obj/`, package files, archives, logs, temp files, local secrets, and real-name metadata.

## Acceptance Criteria
- `.gitignore` covers `.ackit/`, `bin/`, `obj/`, `TestResults/`, `coverage/`, `logs/`, `publish/`, `out/`, archives, package outputs, `.env*`, `secrets.json`, and local appsettings files.
- `winrar_exclude.txt` includes the recommended source archive exclude list.
- `docs/SOURCE_ARCHIVE.md` documents GitHub source packages versus local working-directory archives.
- Tracked files do not include forbidden artifacts or local secret-like files.
- Prohibited maintainer identity terms have no public metadata matches.
- `.cursor/rules/project.mdc` remains documented as intentional.

## Tests
- `git ls-files | rg -n ".(rar|zip|nupkg|snupkg|bak|tmp|binlog|trx)$|(^|/)(bin|obj|TestResults|coverage|publish|out)/"`
- Run the prohibited maintainer identity term scan with `rg` and safe excludes.
- `powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`

## Risks
- Removing `obj/` before a no-restore build causes expected `NETSDK1004`; run restore before no-restore build.
- Local artifact cleanup must be limited to safe generated directories.

## Rollback
- Restore edited docs and ignore files from git.
- Regenerate local `.ackit/`, `bin/`, or `obj/` artifacts by running the relevant CLI commands, `dotnet restore`, or `dotnet build`.

## Completion Notes
Completed during final public release preparation.

- `.gitignore` now covers `.ackit/` plus build outputs, test outputs, coverage, logs, publish/out folders, packages, archives, local secrets, and local appsettings.
- `winrar_exclude.txt` already matched the recommended local archive exclude list.
- `docs/SOURCE_ARCHIVE.md` documents GitHub source archives versus local ZIP/RAR archives and notes `.cursor/rules/project.mdc` as intentional.
- Tracked artifact scan returned no matches.
- Prohibited maintainer identity term scan returned no matches.
- Local `bin/` and `obj/` folders were non-empty restore/build artifacts and were kept.
- Generated `src/AgentContextKit.Cli/bin/Release/net10.0/publish` output was safely removed after package validation.
