# AgentContextKit v0.2.0-alpha.2

`v0.2.0-alpha.2` is a compatibility-preserving hardening pre-release for the offline-first `ackit` .NET global tool.

## Highlights
- Scanner rules evaluate all distinct email, phone, and IP candidates while preserving Critical secret detection.
- Narrow token, path, and phone boundaries reduce technical false positives without broad allowlists.
- Human, JSON, and Web UI findings omit raw sensitive matches; the compatible JSON `match` field remains nullable.
- Suppression audit records are deduplicated and sanitized.
- Baseline-aware CI treats severity escalation as a new finding without changing baseline fingerprints or schemas.
- Config diagnostics report unmatched quotes through sanitized `ACKITCFG006` output.
- Local Markdown links are validated by the documentation release gate.
- Manual release automation validates an exact commit and publishes through NuGet OIDC Trusted Publishing.

## Install
```powershell
dotnet tool install --global AgentContextKit --version 0.2.0-alpha.2
```

## Validation
- Release build: zero warnings and zero errors.
- Automated tests: 186 passed.
- Repository scan: no risk findings.
- Doctor: PASS.
- JSON and SARIF outputs parsed successfully.
- Sample, package, installed-tool, contract, localization, security, readiness, and documentation gates passed.
- Synthetic 2,000-file performance tripwire remained below the unchanged 30-second limit.

## Compatibility
CLI commands and exit codes, JSON schema `2`, config schema `1`, baseline schema `1`, SARIF 2.1.0, rule IDs, package ID `AgentContextKit`, and tool command `ackit` remain compatible.

Generated `.ackit` reports, Web UI files, SARIF files, and local paths remain local-only unless explicitly reviewed and sanitized.
