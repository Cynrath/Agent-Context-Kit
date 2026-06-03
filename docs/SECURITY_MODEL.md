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
- Configured `ignorePaths` only suppress reporting for selected paths; they do not delete or mutate files.

## Scanner Precision Notes
- Real environment files such as `.env` and `.env.local` are Critical.
- Sample environment files such as `.env.example`, `.env.sample`, `.env.template`, and `.env.dist` are review findings, not Critical by path alone.
- Private key and key-store file names such as `id_rsa`, `id_ed25519`, `.pfx`, `.p12`, and `.key` are Critical.
- Private key blocks include generic, RSA, DSA, EC, OpenSSH, PGP, and encrypted private key headers.
- Loopback, wildcard, and documentation-reserved IP addresses are ignored to reduce docs false positives.
- Private/internal IP addresses remain reportable.
- Configured brand and PII keywords use token-boundary matching to reduce substring false positives.

## Limitations
Pattern-based scanners cannot guarantee full detection. Public release still requires manual review.
