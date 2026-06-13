# Related Tools Review Policy

Field requirements are defined by `docs/ECOSYSTEM_EVIDENCE_SCHEMA.md`.

## Purpose
Keep ecosystem documentation evidence-backed, dated, conservative, and separate from dependency or execution approval.

## Source Priority
1. Official repository license and release metadata.
2. Official README and versioned project documentation.
3. Official vendor documentation for terms-scoped tools such as CodeQL.
4. A disposable sample-only local lab, when a later task explicitly approves tool installation and execution.

Do not use blog posts, package mirrors, search snippets, popularity lists, or generated summaries as final evidence.

## Confidence
- `High`: official sources directly support the claim and mode boundary.
- `Medium`: official sources support the core claim, but platform/version/mode behavior needs a disposable lab check.
- `Low`: incomplete or ambiguous evidence; the comparison must say `Needs verification` and cannot recommend an adapter.

## Review Cadence
- Fast-moving agent, graph, scanner, and protocol tools: review every 60-90 days.
- Stable conventions and docs tools: review every 180 days.
- Review immediately after a license change, ownership transfer, archive notice, major CLI rewrite, telemetry/default-network change, or security incident affecting the recommendation.

## Required Review Record
Every confirmed row must identify project/repository, package/CLI, license source, capability source, network boundary, privacy risk, reviewer role, last-reviewed date, stale-after date, confidence, and recommendation.

## Stale Evidence Behavior
- A stale row remains historical context but loses any `adapter-candidate-later` recommendation until re-reviewed.
- Unknown fields must not be inferred from popularity or implementation language.
- Platform support means the official project documents or publishes that platform; source portability alone is not proof.
- `Offline` applies to a specific workflow after installation, not package acquisition, database refresh, remote repository fetch, hosted UI, or model/API use.

## Approval Separation
Documentation review does not approve dependency addition, executable download or invocation, auto-install, repository-content upload, telemetry, artifact import, stable ACKIT rule mapping, or release/security control completion.

Each requires a separate task with license, privacy, threat, test, rollback, and maintainer boundaries.
