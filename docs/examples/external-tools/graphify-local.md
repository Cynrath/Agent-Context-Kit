# Graphify Local AST Lab

## Purpose
Demonstrate a privacy-first local workflow without making the tool an AgentContextKit dependency.

## Prerequisites
- Graphify is already installed. Use only a synthetic disposable copy under `.ackit/external/graphify-lab/repository`.
- Use a disposable synthetic repository.
- Create the output directory: `.ackit/external/graphify/` when the tool does not create parents.

## Offline Boundary
Use a code-only/local AST mode. Do not enable document/media model enrichment, hosted serving, or remote model backends.

## AgentContextKit Pre-Step
```powershell
ackit scan --ci
ackit doctor
```

Resolve any unexpected Critical/High finding before continuing. A minimal sample may produce expected doctor health gaps; review them rather than suppressing them.

## PowerShell
```powershell
New-Item -ItemType Directory -Force .ackit/external/graphify-lab/repository | Out-Null
Push-Location .ackit/external/graphify-lab/repository\ngraphify extract . --no-viz --no-cluster\nPop-Location
```

## Bash
```bash
mkdir -p .ackit/external/graphify-lab/repository
cd .ackit/external/graphify-lab/repository\ngraphify extract . --no-viz --no-cluster\ncd -
```

## Output And Commit Boundary
- Output must remain under `.ackit/external/` or a disposable child lab rooted there.
- Generated output is local-only and must not be committed, attached to a release, or uploaded automatically.
- Graph output exposes architecture, identifiers, paths, and relationships. Current Graphify writes its own `graphify-out/`; contain the entire lab under `.ackit/external/`.

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
