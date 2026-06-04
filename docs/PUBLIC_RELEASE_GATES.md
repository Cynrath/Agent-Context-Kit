# Public Release Gates

This page documents the single local command for public release gate orchestration.

The gate script wraps existing checks:
- `scripts/check-package-metadata.ps1`
- `scripts/audit-public-release.ps1`
- `scripts/check-release-blockers.ps1`

## Report-Only Mode
Run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
```

Report-only mode keeps underlying checks non-failing and exits `0` when the scripts complete, while still printing known blockers.

Expected current blockers:
- Package metadata URLs are final and should pass the metadata gate.
- Missing release tag until `v0.1.0-alpha.1` points at the final reviewed commit.
- No explicit maintainer approval for push or NuGet publish.
- Public repository URL is `https://github.com/Cynrath/agent-context-kit`; current local origin is `https://github.com/Cynrath/agent-context-kit.git`.

## Failing Gate Mode
Run only when preparing a real public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

Before the final local tag exists at `HEAD`, failing mode is expected to exit non-zero because the audit gate requires a release tag. After the tag points at the reviewed commit and the working tree is clean, failing mode should exit `0`.

## Maintainer-Only Actions
This script does not:
- Push commits.
- Create or push tags.
- Create remotes.
- Publish NuGet packages.
- Replace package metadata URLs.
- Redact or delete files.

Follow [MAINTAINER_RELEASE_HANDOFF.md](MAINTAINER_RELEASE_HANDOFF.md) only after explicit maintainer approval.
