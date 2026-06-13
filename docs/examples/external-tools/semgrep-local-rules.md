# Semgrep CE With Reviewed Local Rules

## Purpose
Demonstrate a privacy-first local workflow without making the tool an AgentContextKit dependency.

## Prerequisites
- Semgrep CE is already installed and a separately reviewed local rule file exists.
- Use a disposable synthetic repository.
- Create the output directory: `.ackit/external/semgrep/` when the tool does not create parents.

## Offline Boundary
Use a local rule path only. Do not use automatic registry configuration, login, platform upload, or CI mode.

## AgentContextKit Pre-Step
```powershell
ackit scan --ci
ackit doctor
```

Resolve any unexpected Critical/High finding before continuing. A minimal sample may produce expected doctor health gaps; review them rather than suppressing them.

## PowerShell
```powershell
New-Item -ItemType Directory -Force .ackit/external/semgrep | Out-Null
semgrep scan --config <reviewed-local-rule-file> --json --output .ackit/external/semgrep/findings.json .
```

## Bash
```bash
mkdir -p .ackit/external/semgrep
semgrep scan --config <reviewed-local-rule-file> --json --output .ackit/external/semgrep/findings.json .
```

## Output And Commit Boundary
- Output must remain under `.ackit/external/` or a disposable child lab rooted there.
- Generated output is local-only and must not be committed, attached to a release, or uploaded automatically.
- Rule output may contain snippets and paths. Rule licensing is separate from the engine license.

## Privacy Review Checklist
- No raw secret, token, personal email, customer data, machine name, or absolute local path.
- No private repository URL or internal package/feed name.
- No unexpected file ignored by the source repository's ignore policy.
- No raw finding match or full source bundle unless sharing is explicitly approved.
- No hosted upload, clipboard transfer, telemetry, or model/API call.

## When To Share Output
Share only a sanitized, purpose-limited derivative after a human review and explicit maintainer approval. Do not share the raw output by default.

## Rollback And Cleanup
PowerShell:
```powershell
if (Test-Path .ackit/external) { Remove-Item .ackit/external -Recurse -Force }
```

Bash:
```bash
rm -rf -- .ackit/external
```

Run cleanup only from the disposable repository root after confirming the resolved path.
