# Configuration

AgentContextKit reads optional configuration from `.ackit/config.yml`.

Config allowlist fields are part of the current source and `0.2.0-alpha.1` package. The published NuGet `0.2.0-alpha.1` package includes the scanner rule catalog and allowlist feature set.

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
  - .ackit/prompt-packs/
  - .ackit/context-exports/
riskExtensions:
  - .bak
  - .tmp
  - .log
  - .sql
safeDomains: []
ignoredPaths: []
ignoredFindingIds: []
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

### `safeDomains`
Additional exact domains to treat as safe technical references for domain-like Low findings.

Examples:
- `docs.example.invalid`
- `*.trusted.example`

Rules:
- Exact domains match only the exact domain.
- A leading `*.` matches subdomains only.
- This field does not suppress Critical secret-like findings.

### `ignoredPaths`
Repository-relative paths where non-Critical findings are suppressed but files remain visible in scan file lists.

Examples:
- `generated-reports/`
- `fixtures/public-placeholders/`
- `docs/known-safe-notes.md`

This differs from `ignorePaths`, which excludes matching files from scanning and file enumeration. Prefer `ignoredPaths` for known non-Critical noise because it keeps repository visibility.

### `ignoredFindingIds`
Stable scanner rule IDs to suppress for non-Critical findings.

Examples:
- `ACKIT002`
- `ACKIT003`

Critical findings cannot be silently ignored by this field. If a Critical secret-like finding appears, remove the value, rotate credentials if needed, or move secrets out of source.

## Scanner Precision Fields
The scanner uses a conservative built-in safe technical allowlist for common public platform/package domains and fixture-only placeholder data. Configurable allowlists are local, explicit, and narrow. They do not delete, redact, upload, publish, or mutate files.

See [SCANNER_RULES.md](SCANNER_RULES.md) for rule IDs, SARIF mapping, and suppression behavior.

## Safety
Configuration never causes AgentContextKit to delete, overwrite, redact, upload, or publish files. It only changes local analysis and generated reports.

## Generated File Conventions
See [CONFIG_GENERATED_CONVENTIONS.md](CONFIG_GENERATED_CONVENTIONS.md) for the v1.0 target conventions around default config, ignored generated artifact paths, repository-relative output paths, and skip-existing behavior.
