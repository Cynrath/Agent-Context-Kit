# Issue Triage

This document keeps issue handling consistent and safe.

## Label Recommendations
- `bug`: reproducible incorrect behavior.
- `enhancement`: new capability or workflow improvement.
- `documentation`: docs, examples, or wording.
- `security`: hardening work that does not disclose a vulnerability.
- `scanner`: stack/risk scanner behavior.
- `ci`: GitHub Actions or release validation.
- `localization`: English or Turkish output/docs.
- `release`: packaging, versioning, GitHub Release, or NuGet follow-up.
- `needs-repro`: missing reproduction details.
- `blocked`: requires maintainer-only external action.
- `good first issue`: small, low-risk contributor task.

## Routing
- Bugs need command, version, OS, .NET SDK version, expected behavior, actual behavior, and redacted output.
- Feature requests need a concrete workflow and expected outcome.
- Security hardening issues should stay non-sensitive. Vulnerability disclosure goes through `SECURITY.md`.
- Documentation issues should point to a page or section.
- Release issues should reference the relevant task, release doc, and package version.

## Severity Mapping
- Critical: confirmed secret exposure, unsafe public release artifact, or a command that can leak sensitive content.
- High: release-blocking behavior, broken install, broken CI, or incorrect scanner behavior that hides meaningful risk.
- Medium: confusing output, false positives that block normal work, missing docs for common flows.
- Low: polish, small doc gaps, wording, or backlog cleanup.

## Duplicate And Invalid Closure
- Close duplicates with a link to the canonical issue.
- Close invalid issues when the report cannot be reproduced and requested details are not provided.
- Close support requests that require private repository inspection, and ask for a redacted minimal reproduction instead.
- Never request real secrets or private production config to keep an issue open.

## Backlog And Discussion Conversion
- Convert broad ideas into a focused backlog issue when the next action is clear.
- Move open-ended design discussion out of the release path unless it blocks current support.
- Link accepted backlog items from `docs/ROADMAP.md` when they affect version planning.
