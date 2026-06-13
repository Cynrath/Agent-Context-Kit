# External Output Import Boundary

Status: design only. AgentContextKit does not currently import external SARIF, JSON, SBOM, graph, prompt, or index files.

## Universal Input Controls
- Explicit user-selected repository-relative input path under `.ackit/external/`.
- Reject absolute paths, traversal, symlink escape, device paths, archives, and remote URLs.
- Bounded bytes, records, nesting depth, string length, and parse time per profile.
- Treat every field as untrusted; no polymorphic or generic dynamic execution.
- Reject or omit raw secret matches, source bodies, environment values, and absolute local paths.
- Parsing failure affects only the import request and never mutates core findings, baselines, source, or config.

## SARIF Import
- Accept only explicitly supported SARIF versions/profiles.
- Preserve external tool identity.
- Namespace rule IDs as `<tool-id>/<external-rule-id>`.
- Normalize only valid repository-relative locations.
- Drop unsupported properties and reject embedded absolute/external URI locations by default.
- Produce a sanitized external summary; do not merge into ACKIT findings or exit decisions.

## JSON Import
- One parser per reviewed profile and version range.
- Required schema discriminator/version and expected tool identity.
- Unknown fields ignored only when the profile explicitly permits additive evolution; missing required fields fail safely.
- No generic `external.json` parser and no script/template evaluation.

## SBOM Import
- Support only an approved profile/version after the maintainer decides SBOM policy.
- Default summary: format/version, component count by type, license-field coverage, and vulnerability-reference presence.
- Do not ingest file contents, package feed credentials, private repository URLs, or absolute paths.
- No publication, signing, attestation, or release attachment is implied.

## Graph JSON Import
- Summary only: node/edge counts, bounded label categories, language/file-type counts, and sanitized relationship types.
- Do not ingest raw source, code snippets, embeddings, document contents, free-form model output, or absolute paths.
- High-cardinality labels and identifiers remain local and excluded from default summaries.

## Size And Resource Limits
Initial design ceiling: profile-specific, conservative, and configurable only within a safe maximum. Read streams rather than whole files where practical. Cancel on limit, timeout, malformed nesting, or excessive errors. Exact limits require fixture/performance evidence in an implementation task.

## Path Normalization
Convert separators to `/` after canonical containment validation. Store repository-relative paths only. Case handling follows the host filesystem for containment, but imported identity must not silently collapse distinct paths on case-sensitive systems.

## Finding Mapping Policy
External findings remain in an external collection. A future mapping to ACKIT requires semantic equivalence, severity policy, stable upstream rule identity, collision handling, fixture tests, and maintainer approval. Mapping must never hide or downgrade a core Critical finding.

## Sanitized Summary Envelope
Possible future fields: profile ID/version, source tool/version, format/schema version, input byte count, accepted/rejected record counts, namespaced rule/component/category counts, warning codes, and repository-relative output path. Exclude raw result text and source-derived free text by default.

## Failure Isolation
Return a sanitized import diagnostic and non-zero external-operation result. Do not partially update baselines, scanner output, config, task docs, or generated instructions. Keep the original local file unchanged for human review.
