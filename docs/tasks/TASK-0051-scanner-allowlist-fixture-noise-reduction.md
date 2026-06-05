# TASK-0051: Scanner Allowlist And Fixture-Noise Reduction

## Status
Completed.

## Purpose
Reduce false-positive scanner noise from known-safe technical domains and intentional test fixtures while preserving Critical secret detection.

## Scope
- Review the current risk scanner and brand/PII scanner behavior.
- Add a conservative built-in safe technical domain allowlist for common platform/package domains.
- Treat clearly non-real fixture/sample/test placeholder values as fixture data instead of real PII.
- Keep production/source docs and real-looking secret patterns reportable.
- Document future `.ackit/config.yml` extension options such as `safeDomains` or `ignoredFindings` without changing the config schema in this task unless needed.
- Add focused tests for allowlisted domains, fixture email behavior, real email/domain behavior, and Critical secret behavior.

## Out Of Scope
- Broad user-configurable ignore rules that suppress arbitrary findings.
- Automatic redaction.
- Remote allowlist downloads or telemetry.

## Affected Files
- `src/AgentContextKit.Core/Scanning.cs`
- `src/AgentContextKit.Core/Configuration.cs` if documentation-only config planning becomes an implementation need.
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/SECURITY_MODEL.md`
- `docs/CONFIGURATION.md`
- `README.md`
- `README.tr.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `.codex/CONTEXT_PACK.md`

## DB Impact
None. The project has no database or migrations.

## Admin Impact
None. There is no admin UI.

## Permission Impact
None. No authentication or authorization behavior changes.

## SEO/i18n Impact
README and README.tr may gain short scanner precision notes. No SEO behavior changes.

## Audit/Security Impact
Improves audit accuracy by reducing known non-secret scanner noise. Must not create false negatives for API keys, private keys, tokens, or real PII.

## Acceptance Criteria
- `Microsoft.NET` does not produce a domain-like finding.
- `api.nuget.org` does not produce a domain-like finding.
- Safe technical domains such as `github.com`, `nuget.org`, and `learn.microsoft.com` remain allowlisted.
- Intentional fixture/test/sample placeholder email values do not produce a Medium real-PII finding.
- Real-looking email and domain findings still report outside fixture/sample/test placeholder cases.
- OpenAI project-key and GitHub fine-grained token style secrets still produce Critical findings where applicable.
- Tests cover the above behavior.

## Tests
- `dotnet test AgentContextKit.sln -c Release --no-build`
- `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- `dotnet run --project src/AgentContextKit.Cli -- scan --json`

## Risks
- Over-broad allowlists can hide real leakage.
- Fixture detection based only on path can hide unsafe data in test folders.
- Domain allowlists must remain conservative and technical, not organization/customer-specific.

## Rollback
- Revert the scanner implementation commit.
- Re-run restore, build, tests, scan, doctor, real-name scan, artifact scan, and documentation gates.

## Completion Notes
- Added a conservative built-in safe technical domain allowlist for common GitHub, Microsoft/.NET, NuGet, and package documentation references.
- Added fixture-only placeholder email handling for test/sample/fixture paths without suppressing real-looking domains or configured PII keywords.
- Kept Critical secret-like pattern detection active for test/sample/fixture paths.
- Split fake secret marker strings in scanner patterns, workflow smoke setup, and tests so source-hygiene literal scans do not see full unsafe prefixes.
- Added focused tests for safe technical domains, fixture placeholder email handling, real domain reporting, real email reporting, GitHub fine-grained token-like Critical detection, and OpenAI project-key-like Critical detection.
- Updated README, README.tr, SECURITY_MODEL, and CONFIGURATION docs.
