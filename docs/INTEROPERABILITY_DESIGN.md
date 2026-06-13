# Optional Interoperability Design

This design must preserve `docs/NO_NETWORK_DEFAULT_POLICY.md`: no discovery, probe, execution, import, or network behavior enters default commands.

Status: design only. No external adapter, command, dependency, subprocess, parser, or auto-install exists.

## Principles
- No dependency or external executable by default.
- No auto-install, package download, update, or network access.
- Explicit user opt-in for every probe, guidance profile, execution, or import.
- AgentContextKit Core remains independent from external tool SDKs and output models.
- External failures cannot change normal `ackit scan`, `doctor`, generation, or release-gate behavior.

## Trust Boundary
An external executable is untrusted input even when its name is recognized. A future implementation must resolve an explicit executable path, reject shell composition, avoid user-controlled command strings, pass arguments as structured values, and never send repository content to stdin unless the selected profile explicitly requires and documents it.

PATH discovery can select a shadow binary. A future probe must report the resolved path, require confirmation before execution, and support an explicit path override. Version output is untrusted text and must be length-limited and sanitized.

## Process Lifecycle
- Default timeout: profile-defined and bounded; no unlimited process.
- Cancellation: propagate user cancellation and terminate the child process tree where the platform supports it.
- Working directory: repository root or an explicit disposable lab root.
- Environment: minimal inherited environment; never forward provider tokens automatically.
- Standard output/error: bounded capture, sanitized before display, optional local file only.
- Exit mapping: preserve the external exit code in metadata; map only to an AgentContextKit external-operation result, never to ACKIT scanner severity.

## Version Detection
A profile may define one safe, no-network version probe such as `--version`. Unknown, missing, timed-out, malformed, or unsupported versions remain non-fatal to default AgentContextKit commands. Version compatibility is profile-specific and evidence-dated.

## Output Path Policy
- Repository-relative path under `.ackit/external/<tool>/` by default.
- Reject absolute paths, traversal, symlink escapes, device paths, and output outside the repository/disposable lab.
- Generated output is local-only and ignored.
- Enforce profile size/count limits before parsing.

## Namespace Separation
- Tool profiles: `external/<tool-id>`.
- External rule IDs: `<tool-id>/<original-rule-id>`.
- Imported records never receive an `ACKIT` rule ID without an explicit reviewed mapping policy and regression tests.
- External diagnostics remain separate from AgentContextKit scanner findings and exit decisions by default.

## Import Boundaries
- SARIF: validate version/profile, keep tool identity and namespaced rule IDs, strip unsupported properties, normalize repository-relative paths.
- JSON: profile-specific parser only; no generic dynamic-object trust.
- SBOM: summary counts/components only until the maintainer approves an SBOM policy.
- Graph JSON: summary-only nodes/edges/labels; no raw source body ingestion.

See `docs/EXTERNAL_OUTPUT_IMPORT_BOUNDARY.md` for the detailed future design.

## Adapter Lifecycle
1. Official-source evidence and license review.
2. Privacy threat review and disposable sample lab.
3. Profile contract with supported versions and outputs.
4. Separate implementation task with no auto-install.
5. Focused unit/fixture tests plus cross-platform smoke.
6. Experimental opt-in status before any stable contract.
7. Removal path when evidence expires or upstream behavior changes.

## Failure Isolation
Missing tools, unsupported versions, non-zero exits, malformed output, oversized output, timeout, or cancellation affect only the requested external operation. Normal `ackit doctor` does not fail because optional tools are absent. Imported output never mutates source files or core baselines automatically.

## Cross-Platform Handling
Profiles must define executable names, argument behavior, path separators, process-tree cancellation, encoding, and tested platform support. Unsupported platforms return a clear external-profile diagnostic, not a core health failure.

## Test Strategy
- Fake process runner; no real external binary in unit tests.
- Sanitized fixtures for success, unsupported version, timeout, cancellation, large output, malformed output, traversal, and secret-like content.
- Contract tests for namespace preservation and exit mapping.
- Disposable sample-only smoke after manual installation in a separately approved task.
- No network in default tests.

## Future Command Concepts
- `ackit external-tools list`
- `ackit external-tools check`
- `ackit doctor --external-tools`
- `ackit workflow repomix|gitingest|code2prompt|graphify`
- `ackit external import --profile <profile>`
- `ackit external summarize --input <repository-relative-file>`

These names are design placeholders, not shipped CLI contracts.

Detailed discovery semantics are in `docs/EXTERNAL_TOOLS_COMMAND_DESIGN.md`.

## Threat Summary
Primary risks are executable substitution, source disclosure, secret/path leakage, unsafe shell arguments, unbounded output, stale scanner data, telemetry/network surprises, malformed parser input, and false equivalence between external and ACKIT findings. TASK-0107 provides the full threat model.

The authoritative register is `docs/EXTERNAL_TOOL_PRIVACY_THREAT_MODEL.md`. Future implementation acceptance must trace each profile to applicable threats, controls, residual risks, and evidence dates.
