# Related Tools Comparison Matrix

Status: reviewed 2026-06-13. This is ecosystem intelligence, not an endorsement, dependency list, compatibility guarantee, or approval to execute a tool.

Use `docs/RELATED_TOOLS_EVIDENCE.md` for sources and confidence. Use `docs/RELATED_TOOLS_REVIEW_POLICY.md` for staleness rules.

The required evidence record fields are defined in `docs/ECOSYSTEM_EVIDENCE_SCHEMA.md`. Matrix cells summarize those records and must not silently fill unknown fields.

## Status Values
- `docs-only`: useful for positioning or conceptual comparison.
- `workflow-example`: safe local guidance can be documented, but AgentContextKit does not install or invoke it.
- `adapter-candidate-later`: only after license, privacy, schema, and executable-trust review.
- `not-recommended`: outside the current product boundary or requires unresolved terms/operational controls.

## Identity And Runtime
| Project | Category | Repository | Package / CLI | License | Primary runtime | Install method | Platforms | Maturity signal |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Repomix | Repo-to-context | [yamadashy/repomix](https://github.com/yamadashy/repomix) | `repomix` | MIT | Node.js / TypeScript | npm, npx, Bun, Homebrew | Windows, Linux, macOS | Active project; broad adoption |
| Gitingest | Repo-to-context | [coderamp-labs/gitingest](https://github.com/coderamp-labs/gitingest) | `gitingest` | MIT | Python | pip, pipx, uv; optional self-host | Windows, Linux, macOS | Active CLI/package/web project |
| Code2Prompt | Repo-to-context | [mufeedvh/code2prompt](https://github.com/mufeedvh/code2prompt) | `code2prompt` | MIT | Rust; Python bindings | binary release, Cargo, Python package | Windows, Linux, macOS | Active CLI/TUI/core/MCP ecosystem |
| Graphify | Graph | [safishamsi/graphify](https://github.com/safishamsi/graphify) | PyPI `graphifyy`; CLI `graphify` | MIT | Python / tree-sitter | uv, pipx, pip | Windows, Linux, macOS | Active and fast-moving |
| Joern | Code graph | [joernio/joern](https://github.com/joernio/joern) | `joern` | Apache-2.0 | Scala/JVM plus frontends | release bundle / container | Windows support needs verification; Linux/macOS documented | Established research platform |
| Zoekt | Code search | [sourcegraph/zoekt](https://github.com/sourcegraph/zoekt) | Go binaries | Apache-2.0 | Go | source/release build | Linux/macOS; Windows needs verification | Established indexed search engine |
| Semgrep CE | Static analysis | [semgrep/semgrep](https://github.com/semgrep/semgrep) | `semgrep` | LGPL-2.1 engine; rules use separate terms | OCaml / Python CLI | pipx, uv, Homebrew | Windows, Linux, macOS | Mature engine and ecosystem |
| ast-grep | Structural analysis | [ast-grep/ast-grep](https://github.com/ast-grep/ast-grep) | `ast-grep` / `sg` | MIT | Rust | npm, Cargo, Homebrew, binaries | Windows, Linux, macOS | Active multi-language project |
| Trivy | Security/SBOM | [aquasecurity/trivy](https://github.com/aquasecurity/trivy) | `trivy` | Apache-2.0 | Go | binaries and package managers | Windows, Linux, macOS | Mature multi-target scanner |
| Gitleaks | Secret scanning | [gitleaks/gitleaks](https://github.com/gitleaks/gitleaks) | `gitleaks` | MIT | Go | binaries, package managers, container | Windows, Linux, macOS | Mature dedicated scanner |
| detect-secrets | Secret scanning | [Yelp/detect-secrets](https://github.com/Yelp/detect-secrets) | `detect-secrets` | Apache-2.0 | Python | pip/pipx | Windows, Linux, macOS | Established pre-commit workflow |
| Secretlint | Secret scanning | [secretlint/secretlint](https://github.com/secretlint/secretlint) | `secretlint` | MIT | Node.js / TypeScript | npm/npx | Windows, Linux, macOS | Active plugin ecosystem |
| Syft | SBOM | [anchore/syft](https://github.com/anchore/syft) | `syft` | Apache-2.0 | Go | binaries, package managers, container | Windows, Linux, macOS | Mature SBOM generator |
| OSS Review Toolkit | Compliance | [oss-review-toolkit/ort](https://github.com/oss-review-toolkit/ort) | `ort` | Apache-2.0 | Kotlin/JVM | release/container/source | Windows, Linux, macOS with environment caveats | Mature but operationally heavy |
| CodeQL CLI | Semantic security | [github/codeql-cli-binaries](https://github.com/github/codeql-cli-binaries) | `codeql` | GitHub CodeQL Terms, not general OSI OSS | Binary/query packs | GitHub bundle | Windows, glibc Linux, macOS | Mature GitHub security product |
| AGENTS.md | Agent instructions | [agentsmd/agents.md](https://github.com/agentsmd/agents.md) | Markdown convention | MIT | Markdown/site | none | All | Widely adopted convention |
| Aider conventions | Agent memory | [Aider-AI/conventions](https://github.com/Aider-AI/conventions) | Markdown examples | Apache-2.0 | Markdown | none | All | Community reference set |
| MCP local servers | Tool protocol | [modelcontextprotocol/modelcontextprotocol](https://github.com/modelcontextprotocol/modelcontextprotocol) | varies per server | Specification repository has no asserted SPDX ID; server licenses vary | Varies | Varies | Varies | Fast-moving protocol ecosystem |
| MkDocs | Docs site | [mkdocs/mkdocs](https://github.com/mkdocs/mkdocs) | `mkdocs` | BSD-2-Clause | Python | pip/pipx | Windows, Linux, macOS | Mature static-site generator |
| Docusaurus | Docs site | [facebook/docusaurus](https://github.com/facebook/docusaurus) | `docusaurus` | MIT | Node.js / TypeScript | npm/yarn/pnpm | Windows, Linux, macOS | Mature feature-rich site generator |
| Starlight | Docs site | [withastro/starlight](https://github.com/withastro/starlight) | Astro integration | MIT | Node.js / TypeScript | npm ecosystem | Windows, Linux, macOS | Active docs framework |
| markdownlint-cli2 | Docs quality | [DavidAnson/markdownlint-cli2](https://github.com/DavidAnson/markdownlint-cli2) | `markdownlint-cli2` | MIT | Node.js | npm/npx | Windows, Linux, macOS | Focused active lint tool |
| Vale | Prose quality | [vale-cli/vale](https://github.com/vale-cli/vale) | `vale` | MIT | Go | binaries/package managers | Windows, Linux, macOS | Mature prose linter |
| Lychee | Link quality | [lycheeverse/lychee](https://github.com/lycheeverse/lychee) | `lychee` | Apache-2.0 | Rust | binaries/Cargo/container | Windows, Linux, macOS | Active link checker |

## Offline, Network, And Output Boundary
| Project | Offline local path | Network/API needed? | Default network behavior | Main outputs | Ignores respected? | Token counting | SARIF / JSON | SBOM | Graph | Agent / MCP |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Repomix | Yes, local directory packing | Optional for remote/web/AI use | Local CLI can remain local | XML, Markdown, JSON, plain text | `.gitignore`, `.ignore`, `.repomixignore` documented | Yes | JSON output style; no SARIF claim | No | Compressed structure, not a graph contract | Agent-oriented; no MCP claim in this review |
| Gitingest | Yes, local directory digest | Remote/private URL mode needs network; private mode may need PAT | Local path mode avoids remote fetch | Text digest, tree, summary, token count | `.gitignore` by default | Yes | No stable SARIF/JSON claim | No | Tree only | Prompt-oriented |
| Code2Prompt | Yes | No for core local CLI | Local CLI reads the selected tree | Prompt text, stdout/file/clipboard | `.gitignore` documented | Yes | No SARIF claim | No | Git context, not graph output | Local MCP server exists |
| Graphify | Partial: code AST extraction can be local | Document/media enrichment can require model API; Ollama/local modes exist | Depends on selected mode/backend | `graph.json`, HTML, Markdown report/wiki, optional graph formats | `.graphifyignore` and skip rules documented | Budget controls exist | JSON graph | No | Yes | Agent skill and MCP modes |
| Joern | Yes after installation | No for local CPG/query | Local processing | CPG database/query/export | Tool-specific | No | Export options; needs profile review | No | Yes | No generic agent contract |
| Zoekt | Yes | Optional remote sync/server deployment | Local indexing/search possible | Search index/results/API | Configuration-dependent | No | API results; no SARIF claim | No | No | No |
| Semgrep CE | Yes with local rules | Registry/platform/login optional | `--config` choice controls remote rule retrieval | Human, JSON, SARIF | `.semgrepignore`/git-aware behavior; verify per mode | No | Yes | No | No | CI/editor ecosystem |
| ast-grep | Yes | No for local rules | Local scan/rewrite | Human/structured results; exact formats need version review | Config/glob-based | No | JSON support; SARIF needs verification | No | AST matches, not graph | No |
| Trivy | Partial | Vulnerability/check databases normally update over network unless cache and skip flags are prepared | Attempts database updates by default for relevant scans | Table, JSON, SARIF, CycloneDX/SPDX | Target/config-dependent | No | Yes | Yes | No | CI ecosystem |
| Gitleaks | Yes | No | Local git/dir scan | Human, JSON, CSV, JUnit, SARIF, template | Allowlist/config rules, git-aware | No | Yes | No | No | CI/pre-commit ecosystem |
| detect-secrets | Yes | Optional verification plugins may call providers | Core scan/baseline is local | Baseline JSON and review output | Plugin/config filters | No | JSON baseline; no SARIF claim | No | No | Pre-commit ecosystem |
| Secretlint | Yes | No for local rules | Local lint | Human, JSON, checkstyle, JUnit and others | Config/ignore support | No | JSON; no SARIF claim | No | No | Editor/CI ecosystem |
| Syft | Yes for local filesystem/image data | Remote image sources may need network | Local target remains local | SPDX, CycloneDX, Syft JSON | Cataloger/config-dependent | No | JSON | Yes | No | CI ecosystem |
| OSS Review Toolkit | Partial | Registries, advisors, scanners, repositories may require network | Depends on configured stages | YAML/JSON/report artifacts | Repository/config-dependent | No | Structured outputs | Dependency/compliance inventory | No | CI/compliance ecosystem |
| CodeQL CLI | Partial | Bundle/query acquisition and upload can need network | Local database/query is possible after setup | Database and SARIF | Build/extractor dependent | No | SARIF | No | Relational semantic database | GitHub integration |
| AGENTS.md | Yes | No | Static file | Markdown instructions | N/A | No | No | No | No | Direct agent convention |
| Aider conventions | Yes as files | Aider sessions may use local or hosted models | Convention files themselves are local | Markdown | N/A | No | No | No | No | Aider-specific guidance |
| MCP local servers | Partial | Varies by server | Unknown until each server is reviewed | Tool/resource protocol | Varies | Varies | Varies | Varies | Varies | MCP by definition |
| MkDocs | Yes | Themes/plugins/assets can add network dependencies | Core local build is local | Static HTML site | Source/config rules | No | No | No | No | No |
| Docusaurus | Yes after dependencies exist | Package install and external integrations may use network | Local build is local | Static site/app | Build config | No | No | No | No | No |
| Starlight | Yes after dependencies exist | Package install/integrations may use network | Local build is local | Static docs site | Build config | No | No | No | No | No |
| markdownlint-cli2 | Yes | No after installation | Local lint | Diagnostics | Glob/config ignores | No | Formatters vary | No | No | Editor/CI integration |
| Vale | Yes with local styles | Style package acquisition can use network | Local lint after styles exist | Diagnostics/structured output | Config vocabulary | No | JSON support | No | No | Editor/CI integration |
| Lychee | Partial | External URL checks require network; local-file checks do not | Depends on target URLs | Link diagnostics | Exclude/config rules | No | Structured output options | No | No | CI integration |

## Risk And Recommendation
| Project | Sensitive output risk | AgentContextKit overlap | Complement | Integration recommendation | Status | Last reviewed | Notes / needs verification |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Repomix | High: packed output may disclose the full repository | Context assembly and ignore-aware traversal | Token-budgeted single-file context after readiness review | Keep as reviewed local example | workflow-example | 2026-06-13 | Review output before any model or clipboard use |
| Gitingest | High: digest can contain full source; PAT risk in remote mode | Repository digest/context | Local path digest and token estimate | Document local-path mode only | workflow-example | 2026-06-13 | Never put tokens in command examples |
| Code2Prompt | High: file/clipboard prompt may disclose full source | Prompt/context assembly | Templates and token tracking | File output under `.ackit/external/`; no clipboard default | workflow-example | 2026-06-13 | MCP is a separate trust boundary |
| Graphify | High: architecture graph and reports expose identifiers/relationships | Project map/context | Queryable graph and architecture relationships | Study `graph.json`; adapter only after schema/privacy review | adapter-candidate-later | 2026-06-13 | Preserve partial-offline distinction |
| Joern | High: detailed code graph | Structural analysis | Deep security/code graph research | Documentation only | docs-only | 2026-06-13 | Windows support and export profile need verification |
| Zoekt | High: source-derived index | Navigation/search signals | Large-repo local search | Documentation only | docs-only | 2026-06-13 | Windows support needs verification |
| Semgrep CE | Medium/high: findings and snippets | Pattern/security scanning | Language-aware rules | Local-rules example only | workflow-example | 2026-06-13 | Engine and rule licenses differ; registry/platform are separate |
| ast-grep | Medium; rewrite mode can mutate files | Pattern scanning | AST-aware scan/codemod | Scan-only example later | workflow-example | 2026-06-13 | Exact output/SARIF support needs verification |
| Trivy | Medium/high: package/SBOM/finding disclosure | Secret/package hygiene | Vulnerability, config, license, SBOM | Filesystem example with explicit cache/network warning | workflow-example | 2026-06-13 | Offline data can be stale |
| Gitleaks | Critical/high matches may leak in reports | Secret detection | Git history and dedicated rules | Redacted local report example | workflow-example | 2026-06-13 | Keep reports ignored |
| detect-secrets | Medium: baseline metadata and plugin behavior | Secret detection/baseline | Plugin-based review | Docs-only example; no provider verification | workflow-example | 2026-06-13 | Optional plugins need separate network review |
| Secretlint | Medium; raw mode could expose matches | Secret detection | Masked pluggable linting | Masked local example | workflow-example | 2026-06-13 | Do not enable raw match output |
| Syft | Medium/high: private components and paths | Dependency inventory | SBOM experiment | Keep experimental until SBOM decision | workflow-example | 2026-06-13 | Publication remains maintainer-gated |
| OSS Review Toolkit | High: broad dependency/compliance data | Release/dependency gates | Full compliance pipeline | Do not integrate in current scope | not-recommended | 2026-06-13 | Operational and network scope is too broad for current roadmap |
| CodeQL CLI | High: semantic DB/SARIF | Security analysis | Deep semantic queries | Terms-scoped documentation only | not-recommended | 2026-06-13 | Not general-purpose OSI OSS; usage terms apply |
| AGENTS.md | Medium: instructions can leak private paths/commands | Direct generated instruction overlap | Ecosystem compatibility | Continue format alignment | docs-only | 2026-06-13 | Scan before publication |
| Aider conventions | Medium: copied conventions can carry unsafe commands | Agent instruction concepts | Tool-specific patterns | Reference taxonomy only | docs-only | 2026-06-13 | Do not copy community content blindly |
| MCP local servers | High/unknown per server | Future tool/provider boundary | Controlled local tool exposure | Per-server review only | not-recommended | 2026-06-13 | License, executable, permission, telemetry, and network behavior vary |
| MkDocs | Low/medium: site can publish linked private content | Documentation organization | Lightweight future site | Deferred until docs-site activation | docs-only | 2026-06-13 | Theme/plugin review required |
| Docusaurus | Low/medium plus dependency/runtime scope | Documentation organization | Rich site/versioning/search | Deferred | not-recommended | 2026-06-13 | Too much toolchain for current need |
| Starlight | Low/medium plus dependency/runtime scope | Documentation organization | Structured modern docs | Deferred | not-recommended | 2026-06-13 | Revisit only after site decision |
| markdownlint-cli2 | Low | Documentation quality | Focused Markdown lint | First optional lint candidate | adapter-candidate-later | 2026-06-13 | No dependency yet |
| Vale | Low; style packages can add supply-chain/network scope | Documentation terminology | Prose consistency | Optional after terminology policy | docs-only | 2026-06-13 | Style source must be pinned/reviewed |
| Lychee | Medium for external URL disclosure/network | Link checks | Local and hosted link validation | Local-file checks first; external URLs opt-in | docs-only | 2026-06-13 | Network checks are not default |

## Decision
No tool is approved as a dependency or automatic integration. TASK-0102 may document manual local workflows. Any executable adapter or import parser requires a separate implementation task after TASK-0103, TASK-0106, TASK-0107, and TASK-0111 controls are accepted.
