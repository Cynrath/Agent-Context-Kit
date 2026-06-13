# Agent Context Pipeline

AgentContextKit uses eight review stages. The sequence is guidance, not an automated remote pipeline. Optional external enrichment is outside the default trust boundary.

| Stage | Goal | Typical ackit commands | Outputs | Privacy boundary | External-tool boundary | Done criteria | Failure criteria |
| --- | --- | --- | --- | --- | --- | --- | --- |
| 1. Inspect | Understand stack, health, and current risk | `ackit scan --ci`, `ackit doctor`, `ackit config-check` | Human/JSON diagnostics | Local read-only repository analysis | None by default | Stack/health/risk are reviewed | Unknown Critical/High finding, invalid config, or unexplained health gap |
| 2. Harden | Remove or disposition unsafe repository state | `ackit redact-check`, `ackit baseline`, config review | Findings, baseline, review decisions | Raw matches stay local; Critical findings remain visible | External scanners may be consulted later, separately | Risks are fixed or explicitly reviewed; baseline is sanitized | Secret/PII/path leakage remains or suppression hides required review |
| 3. Generate | Create agent and workflow context | `ackit init`, `ackit generate`, `ackit task` | Config, AGENTS/Claude/Cursor/Copilot files, task/handoff docs | Existing files are not overwritten by default | No external packer required | Generated files are present and reviewed | Stale, unsafe, machine-specific, or unreviewed instructions |
| 4. Review | Confirm generated context matches repository truth | `ackit scan`, `ackit doctor`, manual diff | Reviewed local docs and task scope | Human review before sharing or commit | External output is not trusted as source of truth | Commands, paths, ownership, and scope are accurate | Agent docs expose secrets/private paths or contradict source |
| 5. Optional external enrichment | Add a prompt bundle, graph, search index, deeper scan, or SBOM | AgentContextKit provides docs only today | Ignored `.ackit/external/` artifacts | Separate executable/output trust zone | Explicit opt-in; manual installation; no auto-upload | Synthetic/disposable workflow and sanitized useful output | Unexpected network, telemetry, full-source disclosure, unsafe output, or stale data |
| 6. Validate | Re-run product and repository gates | `ackit scan --ci`, `ackit doctor`, `ackit sarif`, build/test scripts | Scan/doctor/JSON/SARIF/test evidence | SARIF is sanitized and local by default | External evidence remains namespaced and optional | Required local gates pass | Build/test/gate failure or malformed/unreviewed artifact |
| 7. Handoff | Preserve task, state, decisions, and next steps | `ackit task`, generated handoff/context docs | Task file, session handoff, context pack | Include minimum operational context, no credentials | External tool names/results are summarized, not embedded raw | Next operator can resume without guessing | Missing blocker, owner, rollback, test, or privacy note |
| 8. Release decision | Decide GO/NO-GO using exact evidence | Release scripts plus maintainer review | Blocker board, decision register, release evidence | No token/package credential in repository | Hosted/security/package actions are maintainer-controlled | Required P0/P1 evidence has explicit disposition | Hosted evidence/security/ownership/signing/SBOM/provenance/recovery gaps remain |

## Pipeline Rules
- Stages can repeat, but optional external enrichment cannot bypass Inspect, Harden, or Review.
- Local completion is not release approval. The current release-candidate boundary remains `LOCAL READY / REMOTE NO-GO` where maintainer evidence is missing.
- External output never replaces source, tests, or AgentContextKit findings automatically.
- Handoff records what was observed and validated, not assumptions about remote state.

## Failure Recovery
Stop the affected optional stage, preserve source files, remove ignored generated output only after resolving the disposable root, rerun AgentContextKit scan/doctor, and document the unresolved boundary. Do not broaden suppression or add network access to make a workflow pass.
