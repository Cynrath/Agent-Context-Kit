# TASK-0053: Turkish Localization Polish

## Status
Completed.

## Purpose
Replace visible Turkish ASCII fallback CLI wording with natural UTF-8 Turkish output while preserving JSON behavior.

## Scope
- Locate Turkish CLI output strings.
- Replace visible fallback terms such as `olusturuldu` and `Tarama ozeti` with natural Turkish text.
- Keep existing CLI terminology consistent.
- Add tests that verify `--lang tr` human output includes expected Turkish characters.
- Confirm JSON output remains schema-compatible and unaffected by localized human strings.
- Update localization docs.

## Out Of Scope
- Full translation of every generated template.
- Changing command names, JSON field names, or public API names.

## Affected Files
- `src/AgentContextKit.Core/Templates.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/LOCALIZATION.md`
- `README.tr.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None. The project has no database or migrations.

## Admin Impact
None. There is no admin UI.

## Permission Impact
None.

## SEO/i18n Impact
Improves Turkish CLI i18n quality. README.tr may gain a note that human CLI output supports UTF-8 Turkish characters.

## Audit/Security Impact
None beyond keeping scanner/report behavior unchanged.

## Acceptance Criteria
- `ackit scan --lang tr` prints `Tarama özeti`.
- `ackit init --lang tr` or another generated-file command prints `oluşturuldu` when a file is created.
- `Risk bulgusu yok.` remains natural Turkish.
- JSON output remains valid and uses the existing schema.
- Tests cover Turkish human output and JSON stability.

## Tests
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --lang tr`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`

## Risks
- Some Windows terminals may use a legacy code page. The app should emit UTF-8-capable .NET strings and document terminal expectations instead of downgrading text.

## Rollback
- Revert the localization commit.
- Restore previous TextProvider values if terminal compatibility issues are found.

## Completion Notes
- Updated Turkish human CLI text for created/skipped status, scan summary, and doctor heading.
- Added tests for `scan --lang tr`, `init --lang tr`, `doctor --lang tr`, and `scan --lang tr --json`.
- Confirmed JSON output keeps stable schema fields and does not include localized scan summary text.
- Updated localization docs and README.tr with UTF-8 terminal guidance.
