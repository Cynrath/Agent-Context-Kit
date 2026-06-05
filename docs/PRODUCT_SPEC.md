# Product Spec

## Summary
AgentContextKit is an offline-first .NET CLI that prepares repositories for safer AI-assisted development and public OSS readiness.

## Goals
- Generate reliable context files for AI coding agents.
- Detect repository stack and project structure.
- Establish task-first development workflow.
- Report secret/PII/brand/local path risks.
- Improve OSS readiness with docs and health checks.
- Provide JSON output for CI/script integrations.
- Provide local static review artifacts for reports and Web UI prototype review.

## Non-goals For MVP
- Hosted/server Web UI.
- LLM API integration.
- Automatic redaction.
- Remote repository creation.
- NuGet publishing automation.

## Future Optional LLM Scope
v0.5 may add optional LLM-assisted workflows, but the default product remains offline-first. Any future provider integration must require explicit user consent, dry-run context review, safe secret handling, and local auditability before remote calls or context export.

## Target Users
- AI-assisted developers.
- OSS maintainers.
- Small teams, agencies, and freelancers.
- Teams cleaning private projects before public release.

## Current Commands
- `init`
- `scan`
- `scan --ci`
- `sarif`
- `report`
- `webui`
- `prompt-pack`
- `context-export`
- `generate`
- `task`
- `redact-check`
- `doctor`
- `version`
- `help`

## Safety Principles
- Offline-first.
- Local-first.
- Security-first.
- Existing files are skipped by default.
- Risk reports are explicit and severity-based.
