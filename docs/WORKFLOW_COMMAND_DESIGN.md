# Workflow Command Design

Status: future guidance-only design. No `ackit workflow` command is implemented.

## Goal
Expose reviewed Markdown guidance through a future CLI without installing or invoking an external tool, copying to clipboard, generating output, or using network access.

## Proposed Commands
- `ackit workflow list`
- `ackit workflow show repomix`
- `ackit workflow show gitingest`
- `ackit workflow show code2prompt`
- `ackit workflow show graphify`
- `ackit workflow show gitleaks`
- `ackit workflow show semgrep`
- `ackit workflow show trivy`
- `ackit workflow show docs-quality`

## Behavior
`list` would print reviewed profile IDs, purpose, evidence date, stale state, and offline classification. `show` would render embedded reviewed guidance equivalent to `docs/examples/external-tools/`, including prerequisites, exact trust boundary, manual commands, local output, privacy checklist, and cleanup.

The output must state: `Guidance only. AgentContextKit did not install or run this tool.` Commands are copied manually by the user after checking the installed tool's help.

## Non-Behavior
- No subprocess or executable discovery.
- No package manager, auto-install, update, registry, remote repository, hosted upload, telemetry, API/model call, clipboard write, or shell launch.
- No tool-specific result parsing.
- No claim that a profile is compatible with the installed version.

## Guidance Source
Reviewed Markdown remains canonical. A future implementation may embed generated read-only resources only if a docs-to-resource drift test proves they match. The command must not fetch guidance at runtime.

## Localization
Command/profile IDs remain English and stable. Human guidance may support `--lang en|tr` only after parity tests cover every safety warning. Missing translation must fall back visibly to reviewed English rather than omit a warning.

## Conceptual Exit Behavior
- `0`: list/show rendered known reviewed guidance.
- `1`: unknown/stale-disabled profile or invalid arguments.
- Never return `2` merely because an external tool is missing; no tool is probed.

This is not part of the current published CLI contract.
