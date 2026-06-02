# Security Model

## Trust Boundary
AgentContextKit runs locally against a repository path. The MVP does not upload repository contents, does not call AI APIs, and does not automatically redact or delete files.

## Risk Categories
- Secret
- PII
- Brand
- LocalPath
- ProductionConfig
- BuildArtifact
- RepositoryHygiene

## Severity
- Critical
- High
- Medium
- Low
- Info

## Safe Defaults
- Existing files are skipped.
- Generated files are opt-in by command.
- Redaction checks are report-only.
- Critical risks return non-zero exit codes in `redact-check`.

## Limitations
Pattern-based scanners cannot guarantee full detection. Public release still requires manual review.
