# Baseline Model

## Status
TASK-0084 added the Core model and deterministic fingerprint foundation. TASK-0086 adds explicit local create/update/load and opt-in scan/CI classification. Default scan behavior remains unchanged when no baseline is supplied.

## Purpose
The future v0.3 baseline feature will let repositories distinguish reviewed existing findings from new findings without hiding risk. The baseline is local, repository-relative, explicit, and reviewable.

## Schema
The baseline manifest has an independent schema from CLI JSON output:

- baseline schema version: `1`;
- fingerprint algorithm: `sha256-rule-path-location-occurrence-v1`;
- entries: deterministic ordered finding identities.

Each entry contains only:

- lowercase SHA-256 fingerprint;
- normalized scanner rule ID;
- normalized repository-relative path;
- severity recorded for review context;
- optional positive start line and start column;
- optional positive occurrence number for deterministic same-rule findings in one file.

## Fingerprint Input
Fingerprint v1 hashes this canonical metadata:

1. fingerprint algorithm ID;
2. uppercase rule ID;
3. normalized repository-relative path;
4. optional numeric start line;
5. optional numeric start column;
6. optional numeric occurrence.

The fingerprint never includes a finding message, raw match, secret value, repository root, username, machine name, timestamp, or absolute path.
Severity is stored as review context but is not part of the fingerprint, so a catalog severity adjustment does not silently create a new finding identity.

## Path Rules
- Convert `\` separators to `/`.
- Normalize Unicode to Form C.
- Remove empty and `.` path segments.
- Preserve path case because case-sensitive repositories can contain distinct files.
- Reject absolute paths, drive-qualified paths, URI-like paths, control characters, and `..` traversal.

Equivalent Git paths with different platform separators produce the same fingerprint. Paths that differ only by case intentionally remain distinct.

## Determinism
`BaselineManifest` sorts entries by path, rule ID, line, column, occurrence, and fingerprint using ordinal comparison. Duplicate fingerprints are rejected instead of silently collapsed.

When scanner findings do not provide line/column metadata, `BaselineClassifier` assigns a positive occurrence within each normalized rule/path group using deterministic scanner order. Severity, message, and raw match values do not determine occurrence and never enter the fingerprint.

## Local Workflow
Create the default root-level baseline:

```powershell
ackit baseline
```

Review the resulting `.ackit-baseline.json`, then opt into classification:

```powershell
ackit scan --baseline .ackit-baseline.json
ackit scan --baseline .ackit-baseline.json --ci
```

Replace an existing reviewed baseline only with explicit intent:

```powershell
ackit baseline --update
```

The default root path is shareable because `.ackit/` is reserved for ignored generated output. Teams may choose another repository-relative `.json` path with `--output` and `--baseline`.

## Error Contract
- `ACKITBASE001`: unsafe or non-JSON baseline path.
- `ACKITBASE002`: baseline file not found.
- `ACKITBASE003`: existing file would be overwritten without `--update`.
- `ACKITBASE004`: invalid or empty JSON.
- `ACKITBASE005`: incompatible schema or fingerprint algorithm.
- `ACKITBASE006`: invalid entry, duplicate identity, or fingerprint integrity failure.
- `ACKITBASE007`: sanitized local file read/write failure.

## Security Boundary
A baseline records prior review state; it does not declare a finding safe. Critical findings are not suppressed by this model. Future integration must keep Critical findings visible, require explicit baseline updates, and produce reviewable diffs.

## Future Integration
TASK-0087 will extend baseline metadata to SARIF, HTML reports, and Web UI while preserving the current additive JSON shape. Migration and cross-platform package validation remain separate release-readiness work.

Changing canonicalization or fingerprint inputs requires a new fingerprint algorithm ID and migration guidance. Existing fingerprints must never change silently.
