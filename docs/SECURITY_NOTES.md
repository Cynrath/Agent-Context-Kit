# Security Notes

- Do not commit secrets.
- Review production configuration before public release.
- Review sample environment files before public release even when they do not contain real secrets.
- Do not commit private key or key-store files such as `.key`, `.pfx`, `.p12`, `id_rsa`, or `id_ed25519`.
- Keep redaction checks report-only unless explicitly approved.
- Treat Critical and High findings as blockers until reviewed.

## Current Risk Summary
- No risk findings in the latest local scan.
- Package URLs point to `https://github.com/Cynrath/agent-context-kit`.
- Public release remains blocked until the release tag points at the reviewed commit and maintainer-controlled push/NuGet publish actions are approved.
