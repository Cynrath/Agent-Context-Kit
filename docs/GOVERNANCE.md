# Governance

AgentContextKit is currently maintained as a small maintainer-led open source project.

## Decision Making
Project decisions should be recorded in `docs/DECISIONS.md` when they affect architecture, security posture, release process, or user-visible behavior.

## Contribution Model
Contributions should:
- start from a task file
- keep scope small
- include tests when behavior changes
- preserve offline-first behavior
- avoid unnecessary dependencies

## Security Model Changes
Any change that uploads data, calls remote services, changes redaction behavior, or weakens overwrite safety requires an explicit decision record.

## Release Model
Public releases require:
- passing local verification
- updated docs
- reviewed package metadata
- explicit maintainer approval

No automated agent session should push, tag, or publish without explicit maintainer instruction.
