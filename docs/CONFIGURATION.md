# Configuration

AgentContextKit reads optional configuration from `.ackit/config.yml`.

The MVP parser intentionally supports a small YAML-like subset:
- `key: value`
- inline lists such as `brandKeywords: ["example"]`
- multiline lists such as:

```yaml
ignorePaths:
  - vendor/
  - generated/
```

Unknown fields are ignored. Invalid or unknown language values fall back to English.

## Default Config
```yaml
schemaVersion: 1
defaultLanguage: en
brandKeywords: []
piiKeywords: []
ignorePaths:
  - .ackit/cache/
  - .ackit/reports/
  - .ackit/webui/
riskExtensions:
  - .bak
  - .tmp
  - .log
  - .sql
```

## Fields
### `schemaVersion`
Config schema version. Current value: `1`.

### `defaultLanguage`
Default CLI/generated-doc language.

Supported values:
- `en`
- `tr`

### `brandKeywords`
Custom brand, product, customer, or domain words to report before public release.

Matching uses token boundaries. For example, `Acme` matches `Acme Corp` but not `AcmeCorp`.

### `piiKeywords`
Custom person, phone, address, customer identifier, or private organization words to report before public release.

Matching uses token boundaries to reduce substring false positives.

### `ignorePaths`
Repository-relative paths to exclude from scanning and risk reports.

Matching is prefix-based and case-insensitive. Examples:
- `vendor/`
- `generated/`
- `docs/private-notes.md`

Do not configure broad values such as `src/` unless you intentionally want to hide those files from reports.

### `riskExtensions`
Additional file extensions to flag as public-release review risks.

Examples:
- `.dump`
- `.backup`
- `.private`

## Safety
Configuration never causes AgentContextKit to delete, overwrite, redact, upload, or publish files. It only changes local analysis and generated reports.
