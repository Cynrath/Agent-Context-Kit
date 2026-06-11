# Web UI Preview

AgentContextKit can generate an offline static Web UI for local repository review.

## What It Shows
The Web UI dashboard shows readiness score, stack signals, health checks, findings, generated context files, and task previews.

## Local-Only Behavior
The Web UI is generated as a local HTML file. It starts no server, uses no external assets, and makes no remote calls.

Generate a preview:

```powershell
ackit webui --output .ackit/webui/demo.html
```

The published NuGet `0.1.0-alpha.2` global tool supports `ackit webui`.

## Public Artifact Rule
Generated Web UI HTML can include local repository paths and local audit context. Keep it local and do not publish it as a GitHub Release asset, NuGet package asset, or committed docs file.

## Safe Screenshot Workflow
1. Generate the local Web UI under `.ackit/webui/`.
2. Open it locally.
3. Capture only the UI region needed for documentation.
4. Remove or crop any local path, private username, machine name, customer/client data, and raw finding match.
5. Save only a sanitized, small PNG or WebP under `docs/assets/screenshots/`.
6. Review the image using the checklist in `docs/VISUAL_ASSETS.md`.

## Screenshot Checklist
- No absolute path.
- No private username.
- No secret, token, password, or personal email.
- No customer/client data.
- No local machine name.
- No raw finding match.
- No generated `.ackit` HTML committed.

## Future Plan
- Add sanitized screenshot assets.
- Add a Web UI preview image to README.
- Add a GitHub README image once the asset passes the visual asset checklist.
- Consider an optional short GIF demo later if it stays small and sanitized.
