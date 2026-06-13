# TASK-0102 Offline Workflow Examples With External Tools

## Purpose
Provide copy-ready, docs-only local workflow examples without installing or invoking external tools.

## Background
The catalog identifies complementary tools, but users need privacy-first examples that preserve the offline boundary.

## Current State
A high-level workflow document exists; per-tool examples and cleanup guidance do not.

## Scope
- Add reviewed examples for packers, graph, secret/static scanners, SBOM experiments, and docs quality.
- Include PowerShell and Bash where practical.
- Keep outputs under ignored `.ackit/external/`.

## Out Of Scope
- No tool installation, execution, auto-download, token example, hosted upload, or committed output.
- No guarantee that copied commands fit every future tool version.

## Affected Files
- docs/EXTERNAL_TOOL_WORKFLOWS.md
- docs/examples/external-tools/*.md
- queue/index/map and .codex handoff files

## Data And Privacy Boundary
Every example starts with ackit scan/doctor, uses local ignored output, and requires review before sharing.

## Offline And Default-Network Policy
AgentContextKit remains local-only and performs no external installation, subprocess invocation, repository upload, telemetry, or network call in this task. Any future networked behavior requires explicit opt-in and a separate reviewed task.

## Database Impact
None.

## Admin Impact
None locally. Repository settings, hosted workflows, releases, package ownership, and security controls remain maintainer-only.

## Permission Impact
None. No token scope, workflow permission, filesystem privilege, or remote API permission is added.

## SEO And I18n Impact
Documentation wording and links may change. No hosted site metadata or runtime localization contract changes unless explicitly listed in scope.

## Security Impact
Every example starts with ackit scan/doctor, uses local ignored output, and requires review before sharing. No security control is declared enabled or complete by this documentation task.

## Documentation Impact
This is a documentation/design task. Active docs, indexes, roadmap, queue, and handoff files must remain mutually consistent.

## Acceptance Criteria
- Each example has purpose, prerequisites, offline boundary, commands, output, privacy checklist, sharing rule, and cleanup.
- No command contains credentials or enables hosted upload.

## Validation Steps
- Static command review and literal-token/path hygiene.
- Full repository docs and release gates.

## Tests
No new runtime test is expected unless implementation scope changes. Run the full repository build/test/scan/doctor/sample/hygiene/gate sequence before commit.

## Risks
- External CLI syntax may drift.
- Shell-specific cleanup commands can be misused outside the repository.

## Rollback Plan
Remove the example documents; no tools or outputs were created.

## Completion Notes
- Planned under PROJECT-CONTROL-0101.
- Completed locally on 2026-06-13.
- Added ten docs-only local workflow examples and a shared boundary/entry index.
- Examples assume manual installation, keep generated output under ignored `.ackit/external/`, prohibit credentials and hosted upload, and include privacy review plus scoped cleanup.
- No external tool was installed or executed.
