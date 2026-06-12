# Baseline Model

## Status
TASK-0084 adds the Core model and deterministic fingerprint foundation only. No current CLI command reads, writes, creates, updates, or applies a baseline. Scan findings and exit codes remain unchanged.

## Purpose
The future v0.3 baseline feature will let repositories distinguish reviewed existing findings from new findings without hiding risk. The baseline is local, repository-relative, explicit, and reviewable.

## Schema
The baseline manifest has an independent schema from CLI JSON output:

- baseline schema version: `1`;
- fingerprint algorithm: `sha256-rule-path-location-v1`;
- entries: deterministic ordered finding identities.

Each entry contains only:

- lowercase SHA-256 fingerprint;
- normalized scanner rule ID;
- normalized repository-relative path;
- severity recorded for review context;
- optional positive start line and start column.

## Fingerprint Input
Fingerprint v1 hashes this canonical metadata:

1. fingerprint algorithm ID;
2. uppercase rule ID;
3. normalized repository-relative path;
4. optional numeric start line;
5. optional numeric start column.

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
`BaselineManifest` sorts entries by path, rule ID, line, column, and fingerprint using ordinal comparison. Duplicate fingerprints are rejected instead of silently collapsed.

## Security Boundary
A baseline records prior review state; it does not declare a finding safe. Critical findings are not suppressed by this model. Future integration must keep Critical findings visible, require explicit baseline updates, and produce reviewable diffs.

## Future Integration
Separate tasks will add:

- config validation and baseline path policy;
- baseline file serialization and explicit create/update commands;
- scanner classification for baseline versus new findings;
- CI exit behavior;
- additive JSON, SARIF, HTML report, and Web UI metadata;
- migration and cross-platform package validation.

Changing canonicalization or fingerprint inputs requires a new fingerprint algorithm ID and migration guidance. Existing fingerprints must never change silently.
