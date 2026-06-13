# TASK-0116 Ecosystem Documentation Consistency And Link Audit

## Purpose
Remove stale execution-policy statements and add a dependency-free local Markdown-link gate.

## Current State
Active handoff/queue documents still describe PROJECT-CONTROL-0101 and prohibit agent pushes despite explicit release authorization.

## Scope
Update active policy text; add and test `scripts/check-local-markdown-links.ps1`; connect it to documentation gates.

## Out Of Scope
Internet link checking, remote content mutation, and broad historical task rewriting.

## Affected Files
Active queue/handoff/docs, the new script, release gates, tests, and documentation indexes.

## Implementation Steps
Find stale phrases; replace active policy; implement repository-relative Markdown target validation; add positive/negative tests; wire the gate.

## Security/Privacy Boundary
The script reads local Markdown only and performs no network calls or content upload.

## Backward Compatibility
Existing valid Markdown and external links remain accepted.

## Database Impact
None.

## Admin Impact
None.

## Permission Impact
No new runtime or hosted permission.

## SEO/I18n Impact
English/Turkish active release wording remains aligned.

## Audit/Security Impact
Broken local documentation references become release-gate failures.

## Acceptance Criteria
Active stale statements are removed; valid links pass; broken local links fail with `-FailOnIssues`; external links are skipped.

## Tests
Focused script tests for valid local, missing local, anchor, and external targets.

## Validation
Run the link gate and documentation/release gate.

## Rollback
Remove the gate integration and script; restore prior wording.

## Completion Evidence
Completed locally on 2026-06-13. Active queue/handoff policy now reflects PROJECT-CONTROL-0102 authorization. The dependency-free link checker validated 283 Markdown files and 225 local targets; its valid/external/code/broken cases passed and the documentation gate invokes it.

## Commit
Included in the hardening/docs commit after validation.

## Push
Normal push is authorized after the combined validation.

## Hosted Checks
Standard CI must execute the affected test suite after push.
