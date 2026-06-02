# Decisions

## ADR-0001: Use .NET 10
AgentContextKit targets .NET 10 because the project brief requires the latest stable .NET generation and the local environment has .NET SDK 10.0.300 installed. `net10.0` also aligns with future global-tool packaging guidance.

## ADR-0002: Offline-first
The MVP runs locally and does not upload repository content. This reduces secret, PII, and private-code leakage risk and makes the tool useful in restricted environments.

## ADR-0003: No LLM API In MVP
LLM integration is deferred because the MVP value is repository hygiene, context generation, task workflow, and risk reporting. Avoiding API calls keeps the first release safer, simpler, and easier to test.

## ADR-0004: No Web UI In MVP
The first release is a CLI-only product to keep scope small and releaseable. A local Web UI can be added later after the scanner and report formats stabilize.

## ADR-0005: Single Repository With Modular Projects
The solution uses one repository with separate CLI, Core, and Tests projects. This keeps maintenance simple while preserving clean boundaries and future extension points.

## ADR-0006: English/Turkish Localization
The CLI and generated docs support English and Turkish because the project is intended for both international OSS users and Turkish developers. Unknown language input falls back to English.

## ADR-0007: MIT License
MIT is used because it is permissive, widely recognized by OSS users, and compatible with a small developer CLI intended for broad adoption.

## ADR-0008: Safe Overwrite Strategy
Generated files are skipped when they already exist. The MVP does not overwrite, delete, or redact files automatically. Future overwrite/backup/dry-run options can be added with explicit user intent.
