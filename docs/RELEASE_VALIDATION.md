# Release Validation

This checklist validates local release readiness without publishing.

## Required Commands
```powershell
dotnet restore AgentContextKit.sln
dotnet build AgentContextKit.sln -c Release --no-restore
dotnet test AgentContextKit.sln -c Release --no-build
dotnet run --project src/AgentContextKit.Cli -- scan
dotnet run --project src/AgentContextKit.Cli -- scan --ci
dotnet run --project src/AgentContextKit.Cli -- report --json
dotnet run --project src/AgentContextKit.Cli -- webui --json
dotnet run --project src/AgentContextKit.Cli -- prompt-pack --output .ackit/prompt-packs/release-validation.md --json
dotnet run --project src/AgentContextKit.Cli -- context-export --prompt-pack .ackit/prompt-packs/release-validation.md --approve --output .ackit/context-exports/release-validation.json --json
dotnet run --project src/AgentContextKit.Cli -- doctor
```

## Scripted Validation
```powershell
powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1
```

The script creates temporary package/tool folders under the user temp directory and leaves them in place for inspection. It also runs `scripts/check-release-blockers.ps1` in report-only mode, so local validation can pass while public-release blockers remain visible.

## v0.2 Readiness Review
Run the v0.2 local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1
```

Use it as a failing gate for missing v0.2 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v020-readiness.ps1 -FailOnIssues
```

See [V020_READINESS.md](V020_READINESS.md).

## v0.3 Readiness Review
Run the v0.3 local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1
```

Use it as a failing gate for missing v0.3 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v030-readiness.ps1 -FailOnIssues
```

See [V030_READINESS.md](V030_READINESS.md).

## v0.4 Readiness Review
Run the v0.4 local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1
```

Use it as a failing gate for missing v0.4 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v040-readiness.ps1 -FailOnIssues
```

See [V040_READINESS.md](V040_READINESS.md).

## v0.5 Readiness Review
Run the v0.5 local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1
```

Use it as a failing gate for missing v0.5 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v050-readiness.ps1 -FailOnIssues
```

See [V050_READINESS.md](V050_READINESS.md).

## v1.0 Local Contract Gates
Run the stable CLI contract check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1
```

Use it as a failing local gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-cli-contract.ps1 -FailOnIssues
```

See [CLI_CONTRACT.md](CLI_CONTRACT.md).

Run the config/generated convention check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1
```

Use it as a failing local gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-config-generated-conventions.ps1 -FailOnIssues
```

See [CONFIG_GENERATED_CONVENTIONS.md](CONFIG_GENERATED_CONVENTIONS.md).

Run the documentation/release gate freeze check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1
```

Use it as a failing local gate:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-documentation-release-gates.ps1 -FailOnIssues
```

See [V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md](V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md).

## v1.0 Final Local Readiness Review
Run the v1.0 final local readiness check:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1
```

Use it as a failing local gate for missing v1.0 readiness assets:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-v100-readiness.ps1 -FailOnIssues
```

See [V100_READINESS.md](V100_READINESS.md).

## Release Blocker Review
Report current blockers:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1
```

Use the blocker check as a failing gate before public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-release-blockers.ps1 -FailOnBlockers
```

If the working tree is clean, package metadata is final, and the release tag exists locally, the failing gate should return `0`. See [RELEASE_BLOCKERS.md](RELEASE_BLOCKERS.md).

## Package Metadata Review
Run package metadata review in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1
```

Use it as a failing gate before public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-package-metadata.ps1 -FailOnIssues
```

With the final package URLs in metadata, the failing gate should return `0`. See [NUGET_METADATA.md](NUGET_METADATA.md).

## Public Release Audit
Run the final public release audit in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1
```

Use it as a failing gate before public release:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/audit-public-release.ps1 -FailOnIssues
```

See [PUBLIC_RELEASE_AUDIT.md](PUBLIC_RELEASE_AUDIT.md).

## Public Release Gate Orchestration
Run all public release gates in report-only mode:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1
```

Run all public release gates as failing checks before GitHub Release page creation or NuGet publish:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/check-public-release-gates.ps1 -FailOnIssues
```

See [PUBLIC_RELEASE_GATES.md](PUBLIC_RELEASE_GATES.md).

## Package Validation
```powershell
$stamp = Get-Date -Format "yyyyMMddHHmmss"
$pkg = Join-Path $env:TEMP "ackit-nupkg-$stamp"
$tools = Join-Path $env:TEMP "ackit-tools-$stamp"
New-Item -ItemType Directory -Force -Path $pkg, $tools | Out-Null

dotnet pack src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -o $pkg
dotnet tool install AgentContextKit --tool-path $tools --add-source $pkg --version 0.1.0-alpha.1 --ignore-failed-sources
& (Join-Path $tools "ackit.exe") --help
& (Join-Path $tools "ackit.exe") scan --json
```

## Manual Release Gates
- Run `scripts/check-package-metadata.ps1 -FailOnIssues` and confirm it exits `0`.
- Run `scripts/audit-public-release.ps1 -FailOnIssues` and confirm it exits `0`.
- Run `scripts/check-release-blockers.ps1 -FailOnBlockers` and confirm it exits `0`.
- Run `scripts/check-public-release-gates.ps1 -FailOnIssues` and confirm it exits `0`.
- Confirm `RepositoryUrl` points to the real public repository.
- Confirm `PackageProjectUrl` points to the real public project/repository page.
- Confirm package README renders correctly.
- Confirm license and security policy are current.
- Confirm no secrets, dumps, backups, uploads, `bin/`, `obj/`, or generated package outputs are committed.
- Confirm no permanent global tool install is required for validation.
- Confirm `origin/master` and remote `v0.1.0-alpha.1` point to `aee808244bf33d00808e7e70db6235132c2d3829`.
- Confirm GitHub Actions latest `master` run is green.
- Confirm GitHub Release page exists before release announcement.
- Confirm NuGet package availability after publish.

See [MAINTAINER_RELEASE_HANDOFF.md](MAINTAINER_RELEASE_HANDOFF.md) for remaining post-push GitHub Release and NuGet publish steps.
