# Security Policy

## Supported Versions
AgentContextKit is pre-release. Security fixes are prioritized for the latest published pre-release branch/tag. The immediately previous published pre-release is retained as an upgrade and rollback reference, not as a promise of parallel security maintenance.

## Reporting A Vulnerability
Do not open a public issue with secrets, private source code, production configuration, credentials, or customer data.

Until a private security contact is published, report only non-sensitive reproduction steps publicly and mark the issue as a security concern. For sensitive details, wait for a maintainer-provided private channel.

Private GitHub vulnerability reporting must be enabled and verified by the maintainer before a 1.0 release candidate. See `docs/SECURITY_RESPONSE_READINESS.md` for response targets, evidence, and the maintainer-only activation boundary.

## Security Model
- The MVP is offline-first.
- Repository content is analyzed locally.
- No AI API or remote upload is used by the MVP.
- Existing files are not overwritten by default.
- Redaction checks are report-only.

## Known Limitations
- Secret detection is pattern-based and may produce false positives or false negatives.
- Brand/PII detection depends on configured keywords and simple patterns.
- Users must manually review findings before publishing a repository.
