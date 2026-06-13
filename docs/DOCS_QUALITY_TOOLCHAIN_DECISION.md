# Documentation Quality Toolchain Decision

Decision date: 2026-06-13. Current decision: no documentation toolchain dependency.

## Current Default
- Repository Markdown is canonical.
- `README.md`, `README.tr.md`, and `docs/DOCUMENTATION_INDEX.md` are the public entry points.
- Existing .NET build/test and AgentContextKit scan/gates remain the required local pipeline.
- No Node/Python/Rust docs package, generated site, external URL crawler, or hosted docs workflow is added.

## Candidate Roles
| Tool | Possible future role | Network boundary | Current decision |
| --- | --- | --- | --- |
| markdownlint-cli2 | Markdown syntax/style lint only | Local after installation | Preferred first optional lint experiment; no dependency yet |
| Vale | Terminology and prose consistency | Local after reviewed styles exist; style acquisition may use network | Optional after terminology/style ownership is defined |
| Lychee | Link checking | Local-file mode can be offline; external URL checks require network | Local-file checks first; external links hosted/opt-in only |
| MkDocs | Lightweight static docs site | Local build; themes/plugins/install may use network | Deferred until docs-site activation trigger |
| Docusaurus | Versioned/rich docs site | Node dependency and optional integrations | Deferred; too much toolchain for current need |
| Starlight | Structured modern docs site | Node/Astro dependency and optional integrations | Deferred; revisit with docs-site decision |

## Activation Gates
1. A documented recurring quality problem that manual review does not control.
2. Official-source license and dependency review.
3. Pinned reviewed version and lockfile strategy.
4. No telemetry/hosted upload by default.
5. Offline local mode and cross-platform commands.
6. Clear config ownership, false-positive policy, and rollback.
7. Separate task, focused validation, and no generated site/output commit unless explicitly approved.

## Link Checking Policy
Repository-local Markdown/image links may be checked locally in a future optional experiment. External URL health is time-dependent and networked; keep it outside required local gates and run only in a reviewed hosted/manual mode with rate/error policy.

## Site Generator Policy
Do not add a site generator until search, versioned docs, or navigation needs justify it. Keep source Markdown portable and avoid generator-specific syntax in canonical docs where practical.

## Revisit
Revisit after repeated broken-link/formatting defects, a stable beta/1.0 docs freeze, or a maintainer-approved docs-site task. This decision does not block local product work.
