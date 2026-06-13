# Disposable External Workflow Lab

Status: plan only. No external tool is installed or executed by this task.

## Purpose
Provide a repeatable future lab for validating documentation examples without exposing private repositories or adding external tools to the default pipeline.

## Lab Rules
- Use a disposable clone/copy of an AgentContextKit sample or another synthetic public fixture.
- No real secrets, personal data, customer data, private repository, private remote, PAT, internal package feed, or private dependency.
- Remove or replace Git remotes before testing tools that inspect Git metadata.
- Keep the entire lab and outputs under `.ackit/external/lab/<tool>/<run-id>/`.
- Do not commit outputs, caches, databases, generated sites, reports, graphs, prompts, SARIF, or SBOMs.
- Record only sanitized evidence metadata.

## Modes
### Network-Off Mode
Preferred mode. Block or disconnect network where practical, use only tools/modes that can operate with prepared local dependencies, and fail the experiment if the tool attempts an unexpected fetch.

### Network-Required Preparation Mode
Isolated maintainer-approved preparation for package installation, rule/database cache acquisition, or update checks. It is not part of the repository's default validation. Record source, version, digest where available, date, and exactly what was downloaded. Do not process repository content during this mode.

### Local Execution Mode
Return to network-off mode, copy only the prepared tool/cache into the disposable environment, run against synthetic input, and keep output local.

## Suggested Layout
```text
.ackit/external/lab/<tool>/<run-id>/
  repository/
  output/
  cache/
  evidence.md
```

## Preflight
1. Confirm the resolved lab root is inside `.ackit/external/lab/`.
2. Confirm input is synthetic and has no remote/private data.
3. Run `ackit scan --ci` and `ackit doctor` inside the sample.
4. Record the expected doctor gaps for minimal samples.
5. Confirm `.ackit/external/` is ignored.

## Evidence Template
```text
tool/profile:
tool version:
official source:
license evidence date:
operating system:
network mode: off | preparation-only
input sample:
command profile:
external exit code:
output file names and byte counts:
unexpected paths/network/telemetry:
privacy review: pass | fail
cleanup verified: yes | no
notes:
```

Do not paste source, raw findings, secrets, full paths, user names, machine names, tokens, private URLs, or package credentials into evidence.

## Expected Outputs
Only profile-declared file names, bounded sizes, repository-relative paths, and sanitized diagnostics. Any output outside the lab root or any unexplained network access fails the experiment.

## Failure Handling
- Stop the external process and record a sanitized failure category.
- Do not retry with broader permissions, disabled masking, a token, a private repository, or hosted upload.
- Remove the run directory after review.
- Downgrade evidence confidence and keep the profile docs-only until resolved.

## Cleanup
Resolve and inspect the absolute lab path first. Delete only the specific `.ackit/external/lab/<tool>/<run-id>/` directory. Then run `ackit scan --ci` from the repository root and verify `git status --short` contains no generated output.

## Activation Gate
A future task may run one tool only after approving installation source, version, license/terms, network mode, sample, command, output profile, timeout, cleanup, and evidence retention. Default CI remains tool-free and network-independent.
