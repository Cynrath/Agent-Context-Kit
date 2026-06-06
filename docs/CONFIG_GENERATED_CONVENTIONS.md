# Config And Generated File Conventions

This page records the v1.0 target conventions for `.ackit/config.yml`, ignored generated artifacts, and generated output paths.

## Config File
Default config path:

```text
.ackit/config.yml
```

Default fields:

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

Config conventions:
- Unknown fields are ignored.
- Unknown language values fall back to English.
- `ignorePaths` are repository-relative and prefix-matched case-insensitively.
- `riskExtensions` are normalized with a leading dot.
- `safeDomains` are exact domains or leading-wildcard subdomain rules.
- `ignoredPaths` and `ignoredFindingIds` suppress only non-Critical findings.
- Config changes must be backward-compatible or documented as a pre-v1.0 breaking change.

## Ignored Local Artifact Paths
These paths are local generated artifacts and should remain ignored by default config and `.gitignore`:

```text
.ackit/cache/
.ackit/reports/
.ackit/webui/
.ackit/prompt-packs/
.ackit/context-exports/
```

## Generated Output Paths
Default local artifact outputs:

```text
.ackit/reports/scan-report.html
.ackit/webui/index.html
.ackit/prompt-packs/prompt-pack.md
.ackit/context-exports/context-export-manifest.json
```

Generated agent/context outputs:

```text
AGENTS.md
CLAUDE.md
.cursor/rules/project.mdc
.github/copilot-instructions.md
docs/PROJECT_MAP.md
docs/AI_WORKFLOW.md
docs/SECURITY_NOTES.md
docs/tasks/TASK-0001.md
.codex/HANDOFF.md
.codex/CONTEXT_PACK.md
```

Generated task files use:

```text
docs/tasks/TASK-####-slug.md
```

## Write Behavior
Generated files must follow these rules:
- Output paths are repository-relative.
- Output paths outside the repository are rejected.
- Existing files are skipped by default.
- Generated local review artifacts under `.ackit/` are ignored.
- No command deletes, redacts, uploads, pushes, publishes, tags, or creates remotes.

## Ownership
Generated files are intended as reviewable local artifacts. Users may edit committed docs such as `AGENTS.md`, `README.md`, or task files after generation. Generated `.ackit/` review artifacts are disposable local outputs and are not release approval.

## Local Convention Check
Run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1
```

Run as a failing gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues
```

The script checks source defaults, generated default config output, `.gitignore`, and release-critical docs. It does not push, publish, tag, upload, redact, delete, call providers, handle API keys, or create remotes.
