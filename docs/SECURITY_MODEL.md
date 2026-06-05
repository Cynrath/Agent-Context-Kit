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
- SARIF output is generated only when `ackit sarif --output <repo-relative.sarif>` is requested.
- SARIF artifacts use repository-relative paths and do not include raw scanner match values.
- GitHub Code Scanning upload is not automatic; upload workflows remain maintainer-only and example-only unless explicitly enabled.

## Scanner Precision Notes
- Real environment files such as `.env` and `.env.local` are Critical.
- Sample environment files such as `.env.example`, `.env.sample`, `.env.template`, and `.env.dist` are review findings, not Critical by path alone.
- Private key and key-store file names such as `id_rsa`, `id_ed25519`, `.pfx`, `.p12`, and `.key` are Critical.
- Private key blocks include generic, RSA, DSA, EC, OpenSSH, PGP, and encrypted private key headers.
- Loopback, wildcard, and documentation-reserved IP addresses are ignored to reduce docs false positives.
- Private/internal IP addresses remain reportable.
- Configured brand and PII keywords use token-boundary matching to reduce substring false positives.
- Safe technical domain references for common platform/package documentation are allowlisted by default, including GitHub, Microsoft Learn, Microsoft/.NET, NuGet, and the NuGet API host.
- Fixture/sample/test paths can contain clearly non-real placeholder email values without being treated as real PII. Real-looking domains and configured PII keywords remain reportable.
- Critical secret-like values remain reportable even in test, sample, and fixture paths.

## Scanner Allowlist Policy
Built-in allowlists are intentionally narrow and technical. They are for public platform/package domains and explicit placeholder fixture data, not customer domains, production hosts, private organizations, or broad path suppression.

Future config may add `safeDomains` or `ignoredFindings` style controls. Any such control should remain local-only, explicit, reviewable, and documented so it does not hide secrets by accident.

## SARIF Output Privacy
`ackit sarif` is designed as a local report format, not an upload action. The generated SARIF file:

- uses stable AgentContextKit rule IDs instead of embedding scanner internals;
- maps file locations to repository-relative URI paths with `/` separators;
- omits raw secret/PII match text from result messages;
- does not include absolute drive roots, user profile paths, or local workspace paths; and
- is written under an explicit repository-relative `.sarif` output path.

If maintainers later upload SARIF to GitHub Code Scanning, they should review the artifact first and enable upload from a dedicated workflow with `security-events: write` permission.

## Limitations
Pattern-based scanners cannot guarantee full detection. Public release still requires manual review.
