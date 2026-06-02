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
