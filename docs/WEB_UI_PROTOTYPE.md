# Web UI Prototype

AgentContextKit can generate an offline static Web UI prototype from local scan results.

## Command
Default output:

```powershell
ackit webui
```

The default path is:

```text
.ackit/webui/index.html
```

Custom repository-relative output:

```powershell
ackit webui --output .ackit/webui/current.html
ackit webui --output docs/local-webui.html --json
```

## Included Views
- Scan result dashboard.
- Repository health summary.
- Stack signals.
- Risk finding browser.
- Generated agent/context file preview.
- Latest task file preview.

## Safety Behavior
- The Web UI is a local static HTML file.
- No local server is started.
- No external CSS, JavaScript, fonts, images, CDNs, telemetry, or remote calls are used.
- Repository-controlled text is HTML-encoded.
- Existing Web UI files are skipped by default.
- Output paths must stay inside the repository.
- `.ackit/webui/` is ignored by git and by the default scan config.

## JSON Output
`ackit webui --json` returns generated file metadata and a risk summary:

```json
{
  "schemaVersion": 2,
  "toolVersion": "0.1.0-alpha.1",
  "command": "webui",
  "webUi": {
    "path": ".ackit/webui/index.html",
    "status": "Created",
    "created": true
  },
  "riskSummary": {
    "total": 0,
    "critical": 0,
    "high": 0,
    "medium": 0,
    "low": 0,
    "info": 0
  }
}
```

## Review Notes
The Web UI prototype is for local review. It does not approve public release, publish packages, push commits, create release tags, replace release blocker checks, or start a hosted application.
