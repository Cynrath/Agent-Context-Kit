# Exit Codes

AgentContextKit uses small, automation-friendly exit codes.

## General Codes
- `0`: command completed without a blocking condition.
- `1`: command error, invalid invocation, warning-level blocking condition, or high-risk CI condition.
- `2`: critical risk condition.

## Command Matrix
| Command | Exit code behavior |
| --- | --- |
| `ackit help` | `0` |
| `ackit version` | `0` |
| `ackit init` | `0` when config inspection/write completes |
| `ackit scan` | `0` in default report-only mode |
| `ackit scan --ci` | `0` with no high/critical findings, `1` with high findings, `2` with critical findings |
| `ackit report` | `0` when report creation/skip reporting completes |
| `ackit webui` | `0` when Web UI creation/skip reporting completes |
| `ackit prompt-pack` | `0` when prompt-pack creation/skip reporting completes |
| `ackit context-export` | `0` when manifest creation/skip reporting completes, `1` when required approval or prompt-pack input is missing |
| `ackit generate` | `0` when generation/skip reporting completes |
| `ackit task "<title>"` | `0` when task creation/skip reporting completes |
| `ackit task` without a title | `1` |
| `ackit redact-check` | `0` with no findings, `1` with non-critical findings, `2` with critical findings |
| `ackit doctor` | `0` with no failed high/critical checks, `1` with failed high/critical checks |
| Unknown command | `1` |
| Unhandled runtime error | `1` |

## Notes
- `ackit scan` is report-only by default so existing local workflows are not broken by findings.
- Use `ackit scan --ci` in automated checks when high or critical findings should fail the job.
- JSON output uses the same process exit code as human-readable output.
- Public release approval is not implied by exit code `0`; release blockers remain documented separately.
