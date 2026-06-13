# Related Tools Evidence

Review date: 2026-06-13. Reviewer role: AgentContextKit maintainer-support documentation review. This register uses official project repositories and official vendor/project documentation only.

Records follow `docs/ECOSYSTEM_EVIDENCE_SCHEMA.md`. Compact tables may share common `licenseLastChecked` and `lastReviewed` values from the document review date; every row still requires its own confidence and stale-after disposition.

## Evidence Rules
- Repository metadata and license identifiers come from the official GitHub repository and its license metadata/file.
- Capability claims come from the official README or documentation linked below.
- `Medium` confidence means the core claim is documented but platform, output-version, or mode-specific behavior still needs a disposable lab check.
- No evidence row authorizes installation, execution, dependency addition, output sharing, or integration.

## Repo-To-Context And Graph Evidence
| ID | Project | License evidence | CLI/output/offline evidence | Network/privacy evidence | Confidence | Stale after |
| --- | --- | --- | --- | --- | --- | --- |
| E-REPOMIX | Repomix | [repository/license](https://github.com/yamadashy/repomix) | Official README documents local CLI, XML/Markdown/JSON/plain output, token counts, and ignore files | README also documents web/remote/AI modes; packed output can represent the full repository | High | 2026-09-13 |
| E-GITINGEST | Gitingest | [repository/license](https://github.com/coderamp-labs/gitingest) | Official README documents local directory input, digest output, token count, and `.gitignore` default | URL/private modes use network and may use a PAT; local-path mode is the reviewed boundary | High | 2026-09-13 |
| E-CODE2PROMPT | Code2Prompt | [repository/license](https://github.com/mufeedvh/code2prompt) | Official README documents local CLI/TUI, file/stdout/clipboard, `.gitignore`, templates, token tracking, and MCP | Clipboard/MCP/model use is outside the default AgentContextKit boundary | High | 2026-09-13 |
| E-GRAPHIFY | Graphify | [repository/license](https://github.com/safishamsi/graphify) | Official README documents `graph.json`, HTML, report/wiki, local AST extraction, query, and platform integrations | Document/media enrichment and some backends can use model APIs; local/Ollama options do not make every mode offline | High | 2026-08-13 |
| E-JOERN | Joern | [repository/license](https://github.com/joernio/joern) | Official repository documents local Code Property Graph creation/query | Export/platform details require a disposable review before examples | Medium | 2026-09-13 |
| E-ZOEKT | Zoekt | [repository/license](https://github.com/sourcegraph/zoekt) | Official repository documents local trigram indexing and search | Deployment/sync modes are separate; Windows support needs verification | Medium | 2026-09-13 |

## Scanner, Security, And Supply-Chain Evidence
| ID | Project | License evidence | CLI/output/offline evidence | Network/privacy evidence | Confidence | Stale after |
| --- | --- | --- | --- | --- | --- | --- |
| E-SEMGREP | Semgrep CE | [engine repository/license](https://github.com/semgrep/semgrep) | Official docs describe local scans, local YAML rules, JSON/SARIF, and CE behavior | Registry/platform/login and rule licensing are separate from the LGPL engine | High | 2026-09-13 |
| E-ASTGREP | ast-grep | [repository/license](https://github.com/ast-grep/ast-grep) | Official repository documents local structural search/lint/rewrite | Exact output format coverage needs version verification; rewrite mode is excluded from examples | Medium | 2026-09-13 |
| E-TRIVY | Trivy | [repository/license](https://github.com/aquasecurity/trivy) | [filesystem command source](https://github.com/aquasecurity/trivy/blob/main/docs/guide/references/configuration/cli/trivy_filesystem.md) and official repository document local scans and structured outputs | [air-gap guidance source](https://github.com/aquasecurity/trivy/blob/main/docs/guide/advanced/air-gap.md) documents DB caches and skip-update flags; stale data remains a risk | High | 2026-08-13 |
| E-GITLEAKS | Gitleaks | [repository/license](https://github.com/gitleaks/gitleaks) | Official README documents local git/dir scans, redaction, JSON/CSV/JUnit/SARIF/template reports | Reports remain sensitive even when redacted | High | 2026-09-13 |
| E-DETECTSECRETS | detect-secrets | [repository/license](https://github.com/Yelp/detect-secrets) | Official repository documents local scans and baseline workflow | Provider verification plugins can change the network boundary | Medium | 2026-09-13 |
| E-SECRETLINT | Secretlint | [repository/license](https://github.com/secretlint/secretlint) | Official repository documents local rules and masked output formats | Raw-match configuration is excluded | Medium | 2026-09-13 |
| E-SYFT | Syft | [repository/license](https://github.com/anchore/syft) | Official repository documents local cataloging and SPDX/CycloneDX/Syft outputs | SBOMs can disclose private component names and paths | High | 2026-09-13 |
| E-ORT | OSS Review Toolkit | [repository/license](https://github.com/oss-review-toolkit/ort) | Official repository documents analyzer/scanner/advisor/evaluator/reporter stages | Registries and integrations create a broad optional network/data boundary | Medium | 2026-09-13 |
| E-CODEQL | CodeQL CLI | [binary repository](https://github.com/github/codeql-cli-binaries) and [GitHub terms-scoped documentation](https://docs.github.com/en/code-security/concepts/code-scanning/codeql/codeql-cli) | Official docs describe local databases, query analysis, and SARIF | Usage is governed by GitHub CodeQL Terms; upload is a separate hosted action | High | 2026-08-13 |

## Instruction And Documentation Evidence
| ID | Project | License evidence | Primary evidence | Confidence | Stale after |
| --- | --- | --- | --- | --- | --- |
| E-AGENTS | AGENTS.md | [repository/license](https://github.com/agentsmd/agents.md) | Official repository documents the Markdown instruction convention | High | 2026-12-13 |
| E-AIDER-CONV | Aider conventions | [repository/license](https://github.com/Aider-AI/conventions) | Official repository contains community convention-file examples | Medium | 2026-12-13 |
| E-MCP | MCP ecosystem | [specification repository](https://github.com/modelcontextprotocol/modelcontextprotocol) | Specification is public; each server implementation requires independent license/network/permission review | Medium | 2026-08-13 |
| E-MKDOCS | MkDocs | [repository/license](https://github.com/mkdocs/mkdocs) | Official repository documents local static-site generation | High | 2026-12-13 |
| E-DOCUSAURUS | Docusaurus | [repository/license](https://github.com/facebook/docusaurus) | Official repository documents Node-based documentation sites | High | 2026-12-13 |
| E-STARLIGHT | Starlight | [repository/license](https://github.com/withastro/starlight) | Official repository documents Astro-based documentation sites | High | 2026-12-13 |
| E-MDLINT | markdownlint-cli2 | [repository/license](https://github.com/DavidAnson/markdownlint-cli2) | Official repository documents local Markdown linting | High | 2026-12-13 |
| E-VALE | Vale | [repository/license](https://github.com/vale-cli/vale) | Official repository documents local prose linting | High | 2026-12-13 |
| E-LYCHEE | Lychee | [repository/license](https://github.com/lycheeverse/lychee) | Official repository documents local/external link checking | High | 2026-12-13 |

## Open Verification Items
- Confirm Joern and Zoekt Windows support before publishing Windows commands.
- Confirm ast-grep's exact stable structured output options before an import design profile.
- Confirm each MCP server independently; the protocol repository does not transfer a license or privacy guarantee to servers.
- Re-check Graphify frequently because its modes and integrations change quickly.
