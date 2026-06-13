# Configuration Diagnostics

## Status
TASK-0085 added the Core validation service and stable diagnostic contract. TASK-0089 exposes it through read-only `ackit config-check` while preserving the existing config reader behavior for all other commands.

## Contract
Each diagnostic contains:

- stable code;
- severity: `Info`, `Warning`, or `Error`;
- one-based line number;
- optional normalized config key;
- sanitized invariant message.

Messages do not echo raw config values or full source lines.

Quoted scalar and list values must have matching quote characters. An unmatched or mismatched quote produces sanitized `ACKITCFG006` without echoing the value.

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
`AckitConfigReader` keeps existing fallback and normalization behavior, so scan/generation commands are not changed by validation diagnostics.

`ackit config-check` contract:
- missing `.ackit/config.yml`: status `default`, exit `0`;
- valid config: status `valid`, exit `0`;
- warning-only config: status `warnings`, exit `0`;
- one or more Error diagnostics: status `errors`, exit `1`;
- human and JSON modes return the same process exit code;
- the command never rewrites or auto-migrates the file.

`allowedFindingIds` remains readable for predecessor compatibility but produces `ACKITCFG002`, sets `migrationRequired` in JSON, and should be replaced with `ignoredFindingIds` during a reviewed edit.
