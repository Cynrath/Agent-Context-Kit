# Trivy Filesystem With Prepared Offline Cache

## Purpose
Demonstrate a privacy-first local workflow without making the tool an AgentContextKit dependency.

## Prerequisites
- Trivy is already installed and vulnerability/check databases were prepared through a separately approved network step, then transferred to the lab cache.
- Use a disposable synthetic repository.
- Create the output directory: `.ackit/external/trivy/` when the tool does not create parents.

## Offline Boundary
Use skip-update and offline flags. A stale cache can miss recent vulnerabilities; this is not a security-ready claim.

## AgentContextKit Pre-Step
```powershell
ackit scan --ci
ackit doctor
```

Resolve any unexpected Critical/High finding before continuing. A minimal sample may produce expected doctor health gaps; review them rather than suppressing them.

## PowerShell
```powershell
New-Item -ItemType Directory -Force .ackit/external/trivy | Out-Null
trivy fs --cache-dir .ackit/external/trivy/cache --skip-db-update --skip-java-db-update --skip-check-update --offline-scan --format json --output .ackit/external/trivy/findings.json .
```

## Bash
```bash
mkdir -p .ackit/external/trivy
trivy fs --cache-dir .ackit/external/trivy/cache --skip-db-update --skip-java-db-update --skip-check-update --offline-scan --format json --output .ackit/external/trivy/findings.json .
```

## Output And Commit Boundary
- Output must remain under `.ackit/external/` or a disposable child lab rooted there.
- Generated output is local-only and must not be committed, attached to a release, or uploaded automatically.
- Findings and package metadata can reveal private dependencies. Record the database timestamp with results.

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
