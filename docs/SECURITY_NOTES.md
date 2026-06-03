# Security Notes

- Do not commit secrets.
- Review production configuration before public release.
- Review sample environment files before public release even when they do not contain real secrets.
- Do not commit private key or key-store files such as `.key`, `.pfx`, `.p12`, `id_rsa`, or `id_ed25519`.
- Keep redaction checks report-only unless explicitly approved.
