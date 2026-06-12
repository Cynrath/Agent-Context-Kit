# Offline OSS Ecosystem

## Purpose
Define how AgentContextKit evaluates related open-source tools while preserving its offline-first, privacy-first trust boundary.

## Research Method
The initial 2026-06-13 catalog uses official GitHub repository metadata, license identifiers, project READMEs, and official documentation. Stars and activity were used only as maturity signals, not quality guarantees. Projects with ambiguous licenses, unclear local behavior, or separate commercial/service terms are not labeled integration-ready.

## Offline Capability Levels
### Yes
- Repository processing occurs on the user's machine.
- No repository upload or hosted account is required for the listed core workflow.
- Generated output can remain in an ignored local directory.

### Partial
- A meaningful local workflow exists, but one or more of these apply:
  - model inference may use an API;
  - remote repository ingestion uses network credentials;
  - vulnerability databases or rule registries require updates;
  - external link checks require network;
  - server/collaboration features are hosted;
  - license terms limit eligible codebases or use cases.

### No
- The relevant workflow requires repository upload, a hosted service, or an external API.
- Such tools may be mentioned for positioning but are not candidates for default AgentContextKit workflows.

## AgentContextKit Boundary
AgentContextKit should remain usable after installation with:
- no account;
- no API key;
- no repository upload;
- no telemetry requirement;
- no automatic third-party executable installation;
- no implicit network access;
- repository-relative, sanitized local outputs.

An external tool does not inherit this guarantee. Every example must state its own network, credential, output, and privacy behavior.

## Category Map
| Category | Typical value | Typical risk | AgentContextKit relationship |
| --- | --- | --- | --- |
| Repo-to-context packers | Single prompt-ready repository artifact | Full-source disclosure and oversized context | Run only after readiness/hygiene review; keep output local |
| Code/knowledge graphs | Relationships, symbols, paths, queryable graph | Detailed source-derived metadata and optional model/API enrichment | Future sanitized import candidate, not current dependency |
| Security/static scanners | Deeper language, history, vulnerability, secret or license checks | Raw findings, network databases, separate rule licenses | Optional companion workflows with sanitized summaries |
| Agent instruction conventions | Tool-specific project guidance | Stale/conflicting instructions and private paths | Align generated files and taxonomy, do not copy community content blindly |
| Docs tooling | Static sites, prose/link/Markdown quality | Plugin supply chain, network link checks, accidental publication | Optional docs-quality workflow after privacy review |

## Evaluation Rules
1. Verify the exact repository and license from an official source.
2. Separate the engine license from rule packs, plugins, hosted services, and CLI terms.
3. Verify whether local operation is the default or an optional mode.
4. Identify every network touchpoint: installation, model, registry, database, remote repository, telemetry, link checks, upload.
5. Treat generated prompt, graph, SARIF, JSON, baseline, SBOM, and site output as potentially sensitive.
6. Prefer docs-only examples before adapters.
7. Require explicit opt-in before execution and never auto-install tools.
8. Do not make external-tool success part of default `ackit doctor` or release gates without a separate product decision.

## Initial Conclusions
- AgentContextKit overlaps most with readiness/context packaging boundaries, not deep semantic analysis or full-repository prompt packing.
- Local prompt packers are complementary after `ackit scan --ci` and hygiene review.
- Dedicated scanners can deepen coverage, but their raw findings should not be merged automatically into AgentContextKit's stable rule IDs.
- Graph tools are promising for future import/compare workflows, but schema stability and source-derived privacy need dedicated design.
- Documentation tools should remain optional because the repository already treats Markdown as canonical and Pages activation is deferred.

See `docs/RELATED_PROJECTS.md` for the initial matrix and `docs/INTEROPERABILITY_BACKLOG.md` for non-approved future ideas.
