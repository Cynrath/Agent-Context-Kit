# Configuration Diagnostics

## Status
TASK-0085 adds a Core validation service and stable diagnostic contract. Current CLI commands still use the existing backward-compatible config reader and do not fail on these diagnostics yet. A later task will decide CLI integration and exit behavior.

## Contract
Each diagnostic contains:

- stable code;
- severity: `Info`, `Warning`, or `Error`;
- one-based line number;
- optional normalized config key;
- sanitized invariant message.

Messages do not echo raw config values or full source lines.

## Codes
| Code | Severity | Meaning |
| --- | --- | --- |
| `ACKITCFG001` | Warning | Unknown key; current reader ignores it. |
| `ACKITCFG002` | Warning | Obsolete key alias; use the documented replacement. |
| `ACKITCFG003` | Error | Duplicate scalar/list key, including canonical plus obsolete alias. |
| `ACKITCFG004` | Error | Invalid or unsupported config schema version. |
| `ACKITCFG005` | Error | Unsupported default language. |
| `ACKITCFG006` | Error | Malformed scalar/list syntax or empty item. |
| `ACKITCFG007` | Error | List item has no supported list key. |
| `ACKITCFG008` | Error | Absolute, URI-like, traversal, empty, or unsafe repository path. |
| `ACKITCFG009` | Error | Invalid exact/wildcard DNS name. |
| `ACKITCFG010` | Error | Invalid or unknown scanner finding ID. |
| `ACKITCFG011` | Error | Attempt to suppress a Critical scanner rule. |
| `ACKITCFG012` | Error | Invalid risk extension. |
| `ACKITCFG013` | Warning | Broad ignore path can hide major repository areas. |
| `ACKITCFG014` | Warning | Duplicate list value. |

## Obsolete Keys
`allowedFindingIds` is accepted by the current reader for compatibility but produces `ACKITCFG002`. Use `ignoredFindingIds`.

## Safety Rules
- `ignorePaths` and `ignoredPaths` must be safe repository-relative paths.
- `safeDomains` accepts exact DNS names or one leading `*.` wildcard only.
- `ignoredFindingIds` must reference the scanner rule catalog.
- Critical rule `ACKIT001` cannot be configured for suppression.
- Broad `ignorePaths` values such as `src/`, `tests/`, `docs/`, or `samples/` produce warnings.

## Compatibility
Validation is report-only in TASK-0085. `AckitConfigReader` keeps existing fallback and normalization behavior, so current commands, JSON output, and exit codes do not change.

Future CLI integration must document whether warnings remain non-blocking, which errors fail a command, and how human/JSON diagnostics preserve the same exit decision.
