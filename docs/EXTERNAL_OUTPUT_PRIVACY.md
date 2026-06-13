# External Output Privacy

The governing threat register is `docs/EXTERNAL_TOOL_PRIVACY_THREAT_MODEL.md`.

External output is not safer because it is generated locally. Prompt packs, digests, graphs, indexes, SARIF, JSON, SBOMs, and static sites can reconstruct repository structure or expose source-derived data.

## Storage Boundary
- Store under ignored `.ackit/external/<tool>/` or a disposable lab.
- Reject absolute/traversal output paths in any future integration.
- Do not commit, release, attach, paste, or upload raw outputs by default.
- Record only sanitized evidence metadata when validating a workflow.

## Content Classes
| Class | Typical exposure | Default handling |
| --- | --- | --- |
| Source bundle/prompt | Full source, comments, credentials, customer data | Local-only; human review; usually do not share |
| Graph/index | Architecture, identifiers, relationships, paths | Local-only; summary before sharing |
| SARIF/findings | Paths, rule IDs, snippets, security weaknesses | Strip raw matches; relative paths; review upload separately |
| SBOM | Private components, versions, package feeds, paths | Summary-only until publication policy is approved |
| Static report/site | Combined findings and project metadata | Local-only; never publish generated HTML directly |

## Sanitization Minimum
No raw secret, source snippet, absolute path, username, machine name, private repository URL, internal package/feed, customer identifier, clipboard transfer, or unreviewed telemetry. Sanitization is a review process, not an automatic guarantee.

## Sharing Decision
The user or maintainer must identify purpose, audience, minimum fields, retention, and rollback before sharing. External output cannot be normalized into core findings merely to make it easier to publish.
