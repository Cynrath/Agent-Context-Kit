# External Tool Workflows

## Boundary
These are documentation-only, opt-in workflow sketches. AgentContextKit does not install, invoke, bundle, or support these tools. Verify each tool's current CLI and license before use.

Universal rules:
- install tools manually from their official source;
- run against a disposable clone or reviewed local repository;
- write outputs under ignored `.ackit/external/` or another ignored local directory;
- never provide secrets, tokens, private package credentials, or unreviewed source to hosted services;
- run `ackit scan --ci` before creating an external context artifact;
- scan/review the generated artifact before sharing it;
- do not commit generated prompts, graphs, indexes, reports, SARIF, SBOMs, or sites by default.

## Repo-To-Context Packers
### Repomix
```powershell
ackit scan --ci
repomix --output .ackit/external/repomix-output.xml
```

Review the output as a near-full repository disclosure. Token counts and compression do not make the content safe to upload.

### Gitingest Local CLI
```powershell
ackit scan --ci
gitingest . --output .ackit/external/gitingest-digest.txt
```

Use a local path. Remote/private repository modes introduce network and token handling and are outside this offline example.

### Code2Prompt
```powershell
ackit scan --ci
code2prompt . --output-file .ackit/external/code2prompt.txt
```

Avoid clipboard output for sensitive repositories because clipboard managers and other applications may retain the content.

## Graph And Search Tools
### Graphify
```powershell
ackit scan --ci
graphify . --no-viz
```

Run in a disposable clone. AST code extraction is documented as local, but non-code enrichment may use the configured assistant/model. Review `graphify-out/graph.json` and reports before any sharing or future import experiment.

### Joern Or Zoekt
Use their official local installation and indexing instructions. Keep graph/index directories outside tracked source. AgentContextKit currently consumes neither output format.

## Security And Static Analysis
### Gitleaks
```powershell
gitleaks dir . --report-format sarif --report-path .ackit/external/gitleaks.sarif
```

Treat the report as sensitive. Do not assume external SARIF rule IDs map to AgentContextKit rules.

### Semgrep With Local Rules
```powershell
semgrep scan --config path/to/reviewed-local-rules.yml --json --output .ackit/external/semgrep.json
```

Local rules preserve the clearest offline boundary. Registry/platform/login workflows need separate network and license review.

### Trivy Filesystem Scan
```powershell
trivy fs --format json --output .ackit/external/trivy.json .
```

Database acquisition/update can use the network. For controlled offline runs, prepare and document the cache/database source and age.

### detect-secrets And Secretlint
```powershell
detect-secrets scan > .ackit/external/detect-secrets.json
secretlint "**/*" --format json --output .ackit/external/secretlint.json
```

Keep masking enabled. Review optional provider-verification plugins before allowing network access.

## SBOM Experiment
Only after the maintainer selects an SBOM policy:

```powershell
syft dir:. -o spdx-json=.ackit/external/sbom.spdx.json
```

An SBOM can reveal private package names, source locations, and internal components. A generated file is not approved for publication until privacy, digest, lifecycle, and release-location review is complete.

## Docs Quality
Potential local checks include Markdown linting, Vale prose rules, and Lychee local-file links. External URL checking is networked and should be a separate hosted/opt-in mode.

## Non-Integration Rule
These examples do not create `ackit workflow` commands, adapters, normalized schemas, or automatic findings. Any such behavior requires TASK-0103 or a later dedicated design and test task.
