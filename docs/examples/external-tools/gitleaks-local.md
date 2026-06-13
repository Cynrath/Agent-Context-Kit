# Gitleaks Local Redacted Report

## Purpose
Demonstrate a privacy-first local workflow without making the tool an AgentContextKit dependency.

## Prerequisites
- Gitleaks is already installed.
- Use a disposable synthetic repository.
- Create the output directory: `.ackit/external/gitleaks/` when the tool does not create parents.

## Offline Boundary
Local directory scan only. Redaction is mandatory; no upload or hosted dashboard.

## AgentContextKit Pre-Step
```powershell
ackit scan --ci
ackit doctor
```

Resolve any unexpected Critical/High finding before continuing. A minimal sample may produce expected doctor health gaps; review them rather than suppressing them.

## PowerShell
```powershell
New-Item -ItemType Directory -Force .ackit/external/gitleaks | Out-Null
gitleaks dir . --redact=100 --report-format sarif --report-path .ackit/external/gitleaks/findings.sarif
```

## Bash
```bash
mkdir -p .ackit/external/gitleaks
gitleaks dir . --redact=100 --report-format sarif --report-path .ackit/external/gitleaks/findings.sarif
```

## Output And Commit Boundary
- Output must remain under `.ackit/external/` or a disposable child lab rooted there.
- Generated output is local-only and must not be committed, attached to a release, or uploaded automatically.
- Redaction reduces raw match exposure but paths and finding metadata remain sensitive.

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
