# Privacy

Current default network behavior is governed by `docs/NO_NETWORK_DEFAULT_POLICY.md`: normal commands do not upload repository content, call model APIs, send telemetry, or invoke external tools.

## External Tool Boundary
AgentContextKit does not install or invoke external tools by default. External prompt bundles, graphs, indexes, SARIF, JSON, SBOMs, and reports remain separate local artifacts and are governed by `docs/EXTERNAL_TOOL_PRIVACY_THREAT_MODEL.md` and `docs/EXTERNAL_OUTPUT_PRIVACY.md`.

AgentContextKit MVP is local-only.

## Data Handling
- Repository files are read locally.
- No remote upload is performed.
- No telemetry is collected.
- No LLM API is called.
- No automatic redaction is performed.

## Generated Output
Generated files can include project structure and stack information. Review generated files before committing or sharing them.

## Risk Reports
Risk reports may include short matching snippets. Treat reports as sensitive until reviewed.

## User Responsibility
Before publishing a repository, manually review:
- secrets
- production config
- private domains
- names, addresses, emails, phone numbers
- local paths
- backups, dumps, uploads, logs
