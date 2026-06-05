# Issue Triage

This document keeps issue handling consistent and safe.

## Label Recommendations
- `type: bug`: reproducible incorrect behavior.
- `type: feature`: new capability or workflow improvement.
- `type: docs`: docs, examples, or wording.
- `type: ci`: GitHub Actions or release validation.
- `type: security-hardening`: hardening work that does not disclose a vulnerability.
- `type: refactor`: internal cleanup without behavior change.
- `status: needs-triage`: missing maintainer classification.
- `status: accepted`: accepted for implementation.
- `status: blocked`: requires maintainer-only external action.
- `status: duplicate`: duplicate of another issue.
- `status: wontfix`: intentionally not planned.
- `area: scanner`: stack/risk scanner behavior.
- `area: cli`: CLI commands and UX.
- `area: docs`: documentation surface.
- `area: workflows`: GitHub Actions or release validation.
- `area: templates`: generated or GitHub templates.
- `good first issue`: small, low-risk contributor task.
- `help wanted`: maintainer welcomes external help.

See `docs/GITHUB_LABELS.md` for colors, descriptions, and optional maintainer-only label commands.

## Routing
- Bugs need command, version, OS, .NET SDK version, expected behavior, actual behavior, and redacted output.
- Feature requests need a concrete workflow and expected outcome.
- Security hardening issues should stay non-sensitive. Vulnerability disclosure goes through `SECURITY.md`.
- Documentation issues should point to a page or section.
- Release issues should reference the relevant task, release doc, and package version.
- SARIF or GitHub Code Scanning issues should include the `ackit sarif` command used, tool version, operating system, whether upload was local/example/active workflow, and a redacted SARIF snippet when safe.
- New public issues should start with `status: needs-triage` until reviewed.

## Severity Mapping
- Critical: confirmed secret exposure, unsafe public release artifact, or a command that can leak sensitive content.
- High: release-blocking behavior, broken install, broken CI, or incorrect scanner behavior that hides meaningful risk.
- Medium: confusing output, false positives that block normal work, invalid SARIF shape, missing docs for common flows.
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
