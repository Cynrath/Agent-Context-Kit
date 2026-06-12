# Performance Policy

## Scope
Performance evidence currently covers offline repository enumeration, stack detection, risk scanning, and CLI rendering. It does not cover remote services because the MVP makes no remote calls.

## Synthetic Benchmark
Run:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/measure-scan-performance.ps1 -FileCount 2000 -MaxSeconds 30 -FailOnThreshold
```

The script creates a disposable repository under the user temp directory, writes synthetic non-sensitive source files, runs Release `scan --ci`, reports elapsed wall-clock time, and deletes the fixture.

## Current Threshold
- 2,000 synthetic source files.
- 30-second local ceiling.
- Exit code must be `0`.
- Threshold is a regression tripwire, not a production SLA.

The generous ceiling is intended to catch accidental pathological behavior across ordinary maintainer machines. Actual timings vary with filesystem, antivirus, CPU, SDK startup, and disk cache.

## Resource Boundaries
- Scanner file reads are local and sequential in the current implementation.
- Generated HTML previews and tables are capped.
- Build/output directories are excluded by default.
- No cancellation token or explicit memory budget is currently exposed by the CLI.

## Release-Candidate Evidence Still Required
- Hosted benchmark evidence on Windows, Ubuntu, and macOS.
- A representative larger corpus with mixed file sizes/extensions.
- Peak memory observation and a documented regression threshold.
- Cancellation/interruption and unreadable-file behavior review.

Until those items exist, large-repository performance remains a 1.0 P1 gap.
