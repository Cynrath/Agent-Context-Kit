# Maintainers

## Maintainer Identity
Public maintainer metadata uses the pseudonym `Cynrath`.

## Responsibilities
- Keep docs current.
- Keep release validation green.
- Review security-sensitive changes carefully.
- Avoid adding dependencies without clear value.
- Preserve offline-first behavior.

## Release Responsibilities
Before any public release:
- Run `scripts/verify-release.ps1`.
- Review `docs/RELEASE_CHECKLIST.md`.
- Update real repository URLs.
- Review package metadata.
- Review security and privacy docs.
- Confirm no secrets or private data are committed.

## Manual-Only Actions
- GitHub push.
- Tag creation.
- NuGet publish.
- Remote creation.
- Destructive cleanup.
