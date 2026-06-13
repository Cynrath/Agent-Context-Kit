# Interoperability Backlog

The governing architecture is `docs/INTEROPERABILITY_DESIGN.md`. All entries remain no-dependency, no-auto-install, no-network-by-default, explicit-opt-in concepts.

## Status
Ideas only. No command, dependency, auto-install, network call, schema commitment, or compatibility promise exists.

## Universal Constraints
Every item below is:
- no dependency yet;
- no auto-install yet;
- no network by default;
- no secret upload;
- opt-in only;
- subject to license, executable trust, privacy, output-schema, and cross-platform review;
- unable to weaken Critical finding behavior or default offline operation.

## Discovery And Diagnostics
### `ackit external-tools list`
Report manually installed known tools and versions without installing or executing scans. Avoid broad PATH probing unless explicitly requested.

### `ackit doctor --external-tools`
Optional diagnostics for selected tools. Missing tools must remain informational and must not fail normal `ackit doctor`.

### `ackit docs ecosystem`
Open or print local ecosystem documentation without contacting external services.

## Docs-Only Workflow Commands
### `ackit workflow graphify`
Print reviewed local workflow guidance. Do not invoke Graphify automatically.

### `ackit workflow repomix`
Print a local ignored-output recipe and privacy checklist.

### `ackit workflow gitingest`
Default to local paths; never request/store a repository token.

### `ackit workflow code2prompt`
Prefer file output over clipboard and require a pre-export scan reminder.

## Future Import And Comparison
### `ackit import graphify-out/graph.json`
Potential sanitized graph summary import. Requires a versioned external schema/profile, repository-relative paths, size limits, and no raw content ingestion by default.

### `ackit compare external-context`
Compare external packer file coverage against AgentContextKit project-map/context selections without reading or uploading the external artifact beyond local processing.

### `ackit scan --with-gitleaks`
Potential explicit subprocess orchestration. Requires executable pinning guidance, timeout/cancellation, exit-code mapping, redacted output, separate rule namespaces, and no automatic install.

## Other Candidates
- Semgrep local-rule summary import.
- ast-grep scan-only recipe.
- Trivy filesystem/SBOM summary with explicit database freshness metadata.
- detect-secrets baseline comparison without merging baseline schemas.
- Secretlint masked JSON summary.
- Syft SBOM generation only after the maintainer SBOM decision.
- Markdownlint/Vale/Lychee documentation quality profile.

## Sequencing
1. TASK-0101 deepen and normalize the comparison matrix.
2. TASK-0102 validate docs-only external workflows in disposable repositories without adding dependencies.
3. TASK-0103 design optional interoperability contracts and subprocess security boundaries.
4. TASK-0104 define the agent context pipeline taxonomy.
5. TASK-0105 finalize public README ecosystem positioning after the above reviews.
