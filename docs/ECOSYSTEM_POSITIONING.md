# Ecosystem Positioning

AgentContextKit is not trying to replace every repo-to-context, code graph, code search, documentation, or security scanner tool.

## AgentContextKit Focus
- local repository readiness;
- agent instruction files;
- task and handoff discipline;
- scanner safety and privacy-first findings;
- JSON, SARIF, report, and Web UI local outputs;
- release gates and maintainer handoff;
- offline-first project preparation for AI coding agents.

## Related Tool Focus
Related projects may specialize in:
- converting a repository into one LLM prompt;
- building a project knowledge graph;
- token counting and context compression;
- code search and navigation;
- deep language-aware static analysis;
- secret, vulnerability, license, or dependency scanning;
- hosted code intelligence;
- static documentation site generation and prose/link quality.

## Product Boundary
AgentContextKit should answer: "Is this repository prepared, documented, locally reviewable, and safer to hand to an AI coding agent or release process?"

It should not silently become:
- a full source-to-prompt exporter by default;
- a graph database;
- a hosted code intelligence service;
- a replacement for specialist SAST/secret/SBOM tools;
- an external executable manager;
- an API-backed agent runtime.

## Complementary Sequence
A conservative workflow is:
1. Use AgentContextKit to inspect repository health, privacy risks, agent instructions, tasks, output contracts, and release gates.
2. Review and fix findings locally.
3. Opt into a specialist packer, graph, scanner, or docs tool for a defined purpose.
4. Keep its outputs local and ignored.
5. Review external output before model upload, publication, or conversion into AgentContextKit-compatible data.

## Integration Principle
Documentation and example workflows come before adapters. Adapters come before dependencies. Every future adapter requires:
- verified license and redistribution terms;
- explicit user installation and opt-in;
- no network by default;
- no secret upload;
- sanitized, versioned output contracts;
- failure isolation so missing tools do not break core `ackit` behavior.

See `docs/RELATED_PROJECTS.md` for the initial catalog.
