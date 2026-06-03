# TASK-0012: v0.2 Risk Scanner Precision

## Purpose
Improve risk scanner precision and coverage while preserving the offline, report-only MVP safety model.

## Scope
- Reduce false positives for safe environment sample files.
- Keep real environment files critical.
- Add critical file-path findings for private key and key-store file names.
- Expand private key block detection beyond RSA/OpenSSH.
- Ignore loopback, wildcard, and documentation-reserved IP addresses.
- Keep private/internal IP addresses reportable.
- Match configured brand/PII keywords with token boundaries to reduce substring false positives.
- Add focused tests.
- Update docs and handoff.

## Out of scope
- Automatic redaction.
- Secret mutation or deletion.
- Remote scanning.
- LLM integration.
- New dependencies.
- GitHub push.
- NuGet publish.
- Release tag creation.
- Replacing TODO package URLs.

## Affected files
- `src/AgentContextKit.Core/Scanning.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/SECURITY_MODEL.md`
- `docs/SECURITY_NOTES.md`
- `docs/CONFIGURATION.md`
- `docs/ROADMAP.md`
- `docs/PROJECT_MAP.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0012-v020-risk-scanner-precision.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. Risk messages remain English technical labels in the current CLI layer.

## Audit/security impact
Improves public-release review accuracy. The scanner remains pattern-based and cannot guarantee complete secret detection.

## Architecture impact
No architecture boundary changes. Risk scanner remains in Core and file-system reads stay local.

## CLI impact
`ackit scan` and `ackit redact-check` may report fewer false positives for sample env files and docs IPs, and more accurate high-confidence key file findings.

## Testing impact
Add tests for environment sample behavior, key file detection, generic private key blocks, ignored IP addresses, reportable private IP addresses, and keyword boundary matching.

## OSS/release impact
Improves v0.2 public-release readiness checks while keeping existing release blockers unchanged.

## Acceptance criteria
- `.env` remains Critical.
- `.env.local` or similar non-sample env files remain Critical.
- `.env.example`, `.env.sample`, `.env.template`, and `.env.dist` are not Critical.
- Key-store/private-key file names such as `.pfx`, `.p12`, `.key`, `id_rsa`, and `id_ed25519` are Critical.
- Generic private key, EC private key, and PGP private key block headers are Critical.
- `127.0.0.1`, `0.0.0.0`, and documentation-reserved IP ranges are ignored.
- Private/internal IP addresses remain reportable.
- Configured keyword `Acme` does not match `AcmeCorp`, but does match `Acme Corp`.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Update path risk analysis for env samples and key files.
4. Update private key block patterns.
5. Update IP filtering.
6. Update configured keyword matching to use token boundaries.
7. Add focused tests.
8. Update security/config/roadmap/project map/changelog/handoff docs.
9. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
10. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
11. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
12. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
13. Update completion notes.
14. Commit implementation.
15. Continue with the next v0.2 task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
4. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- More precise filtering can hide edge-case sensitive data if patterns are too permissive.
- New key-file rules can flag fixture files in future tests unless intentionally ignored.
- Keyword boundary matching changes existing configured keyword behavior for substrings.

## Rollback plan
Revert the TASK-0012 implementation commit. Do not run destructive git commands.

## Completion notes
Completed.

- Added environment sample precision: `.env.example`, `.env.sample`, `.env.template`, and `.env.dist` are review findings instead of Critical path findings.
- Kept real environment files such as `.env` and `.env.local` Critical.
- Added Critical private key/key-store path findings for `id_rsa`, `id_dsa`, `id_ecdsa`, `id_ed25519`, `.pfx`, `.p12`, and `.key`.
- Expanded private key block matching to generic, RSA, DSA, EC, OpenSSH, PGP, and encrypted private key headers.
- Ignored wildcard, loopback, broadcast, and documentation-reserved IP addresses.
- Kept private/internal IP addresses reportable.
- Added token-boundary matching for configured brand and PII keywords.
- Tightened token/API-key assignment matching to avoid source-code local variable false positives such as `var token =`.
- Reworked tests and task text to avoid self-scan private-key header literals.
- Added focused tests for env sample behavior, key file detection, private key blocks, IP filtering, private IP reporting, and keyword boundaries.
- Updated security model, security notes, configuration docs, roadmap, project map, changelog, context pack, handoff, and next steps.
- `dotnet build AgentContextKit.sln -c Release --no-restore` passed with 0 warnings and 0 errors.
- `dotnet test AgentContextKit.sln -c Release --no-build` passed, 29/29.
- `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan` passed with no risk findings after self-scan fixes.
- `powershell -NoProfile -ExecutionPolicy Bypass -File scripts/verify-release.ps1` passed.
- Release verification reported known public-release blockers in non-failing mode.
- Temporary package/tool folders were left under the user temp directory for inspection.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction was performed.
