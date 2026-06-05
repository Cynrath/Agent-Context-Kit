# Localization

## Supported Languages
- `en`: English, default.
- `tr`: Turkish.

## Fallback
Unknown language codes fall back to English.

## Architecture
- `LanguageCode` represents the selected language.
- `ITextProvider` returns CLI text.
- `TemplateCatalog` stores generated document templates.
- `TemplateRenderer` replaces structured placeholders.

## Rules
- Keep public API names in English.
- Keep Turkish output natural and technical.
- Do not scatter CLI strings across command code when a shared text/template path is practical.

## Turkish CLI Output
Human-readable Turkish CLI output may include UTF-8 Turkish characters such as `ö`, `ı`, and `ğ`.

Examples:
- `Tarama özeti`
- `oluşturuldu`
- `var olan atlandı`
- `Sağlık kontrolleri`
- `Risk bulgusu yok.`

JSON output keeps stable English field names and schema values regardless of `--lang`.

On Windows, use a UTF-8-capable terminal profile if Turkish characters render incorrectly. The CLI should not downgrade Turkish text to ASCII fallback wording.
