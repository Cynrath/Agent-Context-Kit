# Release Checklist

## Before Any Public Release
- `dotnet restore` passes.
- `dotnet build -c Release` passes.
- `dotnet test -c Release` passes.
- `ackit doctor` has no unaccepted high/critical findings.
- `ackit redact-check --profile public-release` reviewed.
- README and README.tr are current.
- SECURITY, CONTRIBUTING, CODE_OF_CONDUCT, CHANGELOG are current.
- LICENSE is present.
- NuGet package metadata is reviewed.
- Local `dotnet pack` succeeds.
- Temporary `dotnet tool install --tool-path` verification succeeds.
- No secrets, dumps, uploads, backups, `bin/`, `obj/`, or generated junk are committed.

## Manual Actions
- Create remote repository only outside the agent session.
- Review all generated files before publishing.
- Publish NuGet package only after explicit maintainer approval.
