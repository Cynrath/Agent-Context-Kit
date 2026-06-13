# External Tool Privacy Threat Model

Status: documentation-only threat model. Scope includes manually installed external tools and their prompt, graph, index, finding, SARIF, JSON, SBOM, and site outputs.

## Assets
- repository source, history, comments, configuration, and generated agent instructions;
- credentials, tokens, private endpoints, customer/user data, and package feeds;
- local usernames, machine names, absolute paths, workspace layout, and private repository URLs;
- architecture relationships, dependency/component inventory, security findings, baselines, and release evidence.

## Trust Zones
1. AgentContextKit local process and reviewed repository files.
2. External executable, its dependencies, environment, caches, and configuration.
3. External output under ignored `.ackit/external/`.
4. Clipboard, shell history, editor extensions, hosted dashboards, model APIs, and artifact uploads.
5. Human sharing/release decision.

Zones 2-4 are not trusted merely because execution starts locally.

## Threat Register
| Threat | Example impact | Required mitigation | Residual risk |
| --- | --- | --- | --- |
| Full source disclosure | Prompt/digest contains proprietary repository content | Run `ackit scan --ci`; respect ignores; output locally; review every shared derivative | Review can miss sensitive business logic |
| Secret leakage | Scanner/packer includes a credential or raw finding match | Resolve Critical/High findings first; use masking/redaction; never pass tokens in commands | Pattern scanners are incomplete |
| Local path leakage | Output reveals username, drive, workspace, or machine layout | Require repository-relative paths; reject absolute/traversal paths; sanitize summaries | Tool-controlled formats may embed hidden metadata |
| Dependency/package leakage | SBOM or scan exposes private packages and versions | Summary-only by default; publication requires SBOM/privacy decision | Component names may still reveal product design |
| Private repository URL leakage | Git metadata or reports include internal origin | Use synthetic/disposable clone; strip remote metadata from shared output | Derived names can remain |
| Graph-derived architecture leakage | Graph reveals services, call paths, ownership, or security boundaries | Keep graph local; share aggregate counts/approved diagrams only | Even sanitized labels can reveal architecture |
| SARIF finding leakage | Paths, snippets, weaknesses, and rule metadata become public | No raw matches; relative paths; review SARIF before opt-in upload | Findings themselves may expose exploitable areas |
| SBOM component leakage | Internal packages/feeds and vulnerable versions are disclosed | Keep local until policy, audience, digest, retention, and release location are approved | Public SBOM can aid both users and attackers |
| Clipboard retention | Full prompt persists in clipboard managers or remote sync | Prefer explicit ignored file output; avoid clipboard for sensitive repositories | OS/editor clipboard behavior varies |
| Shell history | Tokens, URLs, paths, or sensitive arguments persist | Never include credentials in command arguments; use local paths and reviewed config | Tool errors can echo arguments |
| Hosted upload | Repository/output leaves the machine | No hosted mode by default; explicit maintainer approval and content review | Provider retention/control may be unclear |
| Model API upload | Source/context sent to a remote model | No model/API mode in default workflows; local model still needs process/output review | Local model runtime may have telemetry/update behavior |
| Telemetry | Paths, tool versions, usage, or errors are sent externally | Verify telemetry docs/config before use; isolate network where practical | Upstream defaults can change |
| External binary trust | Malicious/shadow binary reads or changes repository | Manual install review; explicit path; report resolved path; disposable lab; minimal environment | Package supply chain remains external |
| Stale vulnerability DB | Offline scan misses recent issues | Record DB timestamp; label stale evidence; do not claim security readiness | Offline freshness and reproducibility trade off |
| False sense of security | Multiple green tools are treated as proof of safety | Preserve manual review and maintainer gates; document tool coverage limits | Unknown classes of issue remain |

## Mandatory Controls
- Run `ackit scan --ci` and `ackit doctor` before an external workflow.
- Use a disposable clone/sample with no secrets, private repos, customer data, or PAT.
- Keep output under ignored `.ackit/external/`.
- Avoid clipboard for source bundles.
- Review and sanitize before sharing or publishing.
- Do not upload, enable telemetry, fetch remote repositories, or call model APIs by default.
- Do not normalize external findings into stable ACKIT rule IDs without a separate mapping policy.
- Record tool version, evidence date, network mode, output type, and cleanup result without storing source payloads.

## Abuse Cases
- A shadow executable named like a known tool is found first on PATH.
- A local mode silently downloads rules, a vulnerability DB, a model, or update metadata.
- An external report embeds raw matched credentials despite human-readable masking.
- A graph/SBOM is committed because it appears to be metadata rather than source-derived data.
- A user copies a command with a credential argument into persistent shell history.

## Responsibility
AgentContextKit documents the boundary but does not control external binaries. The user selects and installs tools; the maintainer approves any shared/released derivative or hosted action. Upstream projects own their executable behavior. Security-ready and release-ready claims remain prohibited without complete evidence.
