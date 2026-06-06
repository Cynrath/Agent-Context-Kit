# SARIF Output

AgentContextKit current source can write scanner findings as SARIF 2.1.0 for CI review and future GitHub Code Scanning workflows.

Availability note: the published NuGet package `AgentContextKit` `0.1.0-alpha.2` does not include `ackit sarif`. Use source/current-branch execution today; the command is planned for the next alpha package.

## What SARIF Is
SARIF is a standard JSON format for static analysis results. Security platforms and CI systems can read it to show findings with rules, severity levels, messages, and file locations.

## What AgentContextKit Produces
`ackit sarif` runs the normal repository scan and risk scanner, then writes a SARIF file.

```powershell
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/ackit.sarif
```

The SARIF file includes:
- `version`: `2.1.0`
- `$schema`: SARIF 2.1.0 schema URI
- Tool name: `AgentContextKit`
- Tool information URI: `https://github.com/Cynrath/agent-context-kit`
- Tool version
- Stable AgentContextKit rules
- Scanner findings as SARIF results

`--output` is required in the first SARIF MVP so users explicitly choose where the generated report is written. Recommended local path: `.ackit/reports/ackit.sarif`.

## Privacy-First Path Behavior
SARIF artifact locations are repository-relative and use `/` separators.

AgentContextKit does not write absolute local paths into SARIF locations. Paths with Windows separators are normalized before output. Local `.ackit/` outputs are ignored and should not be committed.

## Secret Masking Behavior
SARIF result messages use safe finding messages. Raw scanner match values are not written to SARIF.

For example, if a secret-like value is detected, SARIF records the rule, severity, message, and file path, but not the raw matched token.

## Severity Mapping
| AgentContextKit severity | SARIF level |
| --- | --- |
| Critical | `error` |
| High | `error` |
| Medium | `warning` |
| Low | `note` |
| Info | `note` |

## Rule ID Mapping
| Rule ID | Name | Used For |
| --- | --- | --- |
| `ACKIT001` | `SecretLike` | Secret-like values, credentials, keys, and production secret risks. |
| `ACKIT002` | `PiiOrBrandLike` | PII-like, brand-like, email-like, and domain-like review findings. |
| `ACKIT003` | `GeneratedOrBuildArtifact` | Generated, build, package, backup, or artifact files requiring review. |
| `ACKIT004` | `LocalPathOrPrivateLocation` | Local path or private machine location findings. |
| `ACKIT005` | `RepositoryHygiene` | Repository hygiene, configuration, documentation, or release readiness findings. |
| `ACKIT999` | `GeneralFinding` | General fallback finding. |

## JSON Command Result
`ackit sarif --json` prints a command summary, not the SARIF payload.

```powershell
dotnet run --project src/AgentContextKit.Cli -- sarif --output .ackit/reports/ackit.sarif --json
```

Use the output file path to read or upload the SARIF artifact.

## CI Usage Example
The example workflow is stored at `docs/examples/github-actions-sarif-upload.yml`.

It shows two documentation-only approaches:
- use the next published package after SARIF support is released; or
- build and install the current source package locally in CI.

The example is not active. It is not copied into `.github/workflows`, and this task does not upload SARIF to GitHub Code Scanning.

For a hands-on local source demo, see [DEMO_SCENARIOS.md](DEMO_SCENARIOS.md).

## GitHub Code Scanning Upload Note
Uploading SARIF to GitHub Code Scanning requires a workflow permission such as:

```yaml
permissions:
  contents: read
  security-events: write
```

Keep upload workflows maintainer-reviewed because Code Scanning is a public repository integration surface.

## Limitations
- The first SARIF MVP uses file-level locations.
- Full line and column mapping is reserved for a later version.
- SARIF upload is documentation-only in this task.
- Pattern-based scanner results still require maintainer review before public release decisions.
