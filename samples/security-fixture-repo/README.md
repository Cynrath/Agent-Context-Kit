# Security Fixture Repository Sample

This sample demonstrates safe scanner fixture wording without storing real secrets.

## Expected Stack
- Unknown or no strong stack signal.

## Expected Health Gaps
- No license file.
- No security policy.
- No tests.
- No CI workflow.
- No agent instruction files unless generated locally.

## Expected Risk Behavior
- `ackit scan --ci` should not report high or critical findings.
- `ackit redact-check` should not report critical findings.
- The sample explains fake-secret testing without writing real credentials or exact sensitive token prefixes.
- Scanner rule catalog demos should use `ACKIT` IDs and placeholder text only.
- Config allowlist demos should use non-Critical fixture paths or safe domains; Critical secret-like fixtures should remain reportable in separate temporary local tests.

## Safe Fake-Secret Notes
Do not put real credentials in samples.

When documenting fake tokens, split sensitive-looking prefixes, for example:
- `sk` + `-proj-` + `demo`
- `github` + `_pat_` + `demo`
- `OPENAI_API_KEY` followed by an equals sign

This keeps public docs readable without storing exact token-like values.

## Config Allowlist Notes
For local experiments, use placeholder-only values:

```yaml
safeDomains:
  - docs.example.invalid
ignoredPaths:
  - fixture-output/
ignoredFindingIds:
  - ACKIT003
```

These settings are for non-Critical scanner noise. Critical findings are intentionally not suppressed by the new allowlist fields.

## Suggested Commands
```powershell
Push-Location samples/security-fixture-repo
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan --ci
dotnet run --project ../../src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- redact-check --profile public-release
Pop-Location
```

## Safety
This sample contains no real secrets, local machine paths, generated artifacts, package output, or private repository content.
