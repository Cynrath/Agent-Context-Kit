# Scanner Fixture Coverage

This document defines safe regression-fixture conventions for AgentContextKit scanner tests. Fixtures protect scanner precision; they are not a claim that pattern scanning can prove a repository safe.

## Coverage Matrix
| Fixture family | Expected behavior | Stable rule |
| --- | --- | --- |
| Provider-token-like and private-key values | Critical Secret finding | `ACKIT001` |
| Password, token, bearer, and connection-string assignments | High Secret finding | `ACKIT001` |
| Service credential assignments | Medium Secret finding | `ACKIT001` |
| Environment and production config paths | Critical, High, or Medium according to path class | `ACKIT001` or `ACKIT005` |
| Archives, packages, backups, and database artifacts | Medium BuildArtifact finding | `ACKIT003` |
| Absolute workstation and home paths | Low LocalPath finding | `ACKIT004` |
| Real-looking email, domain, phone, and internal IP values | PII or Brand finding | `ACKIT002` |
| Known technical domains and documentation IP ranges | No known-noise finding | none |
| Clearly synthetic emails in fixture-like paths | No email-like PII finding | none |

## Safe Test Data
- Assemble token prefixes from string fragments so repository self-scan does not contain a complete token-like literal.
- Use synthetic payload characters only; never copy a real credential into a fixture.
- Use documentation-reserved IP ranges for negative examples.
- Keep placeholder email exemptions limited to fixture-like paths and clearly non-real domains/local parts.
- Assert severity, category, and stable rule ID for positive fixtures.
- Run case-insensitive scanner regexes with culture-invariant semantics so Turkish and other process cultures do not change ASCII token/domain detection.

## Precision Boundary
Fixture paths do not create a general scanner bypass. Critical secret-like content remains reportable in tests, samples, and fixtures. Configured `ignoredPaths` and `ignoredFindingIds` also cannot suppress Critical findings.

Known-safe fixture behavior is intentionally narrow. A real-looking email or domain outside a fixture path remains reviewable unless it is a built-in technical domain or an explicit `safeDomains` entry.

## Validation
Run focused scanner tests:

```powershell
dotnet test AgentContextKit.sln -c Release --filter FullyQualifiedName~RiskScannerTests
```

Then run the full build, tests, repository scan, doctor, hygiene checks, and release gates.
