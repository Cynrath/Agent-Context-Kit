# FAQ

## Does AgentContextKit upload my code?
No. The MVP is offline-first and local-only.

## Does it call an LLM API?
No. LLM integration is not part of the MVP.

## Does it redact files automatically?
No. Redaction checks are report-only.

## Can it overwrite existing files?
Generated files are skipped when they already exist.

## Can I use it in CI?
Yes. Use `--json` and command exit codes, especially for `redact-check`.

## Is the scanner perfect?
No. It is pattern-based and can produce false positives or false negatives. Manual release review is still required.

## Why are there English and Turkish docs?
The project targets international OSS users and Turkish developers.

## Is it release-ready for NuGet?
Local package validation passes, but public publish remains blocked until real repository URLs are set and the maintainer explicitly publishes.
