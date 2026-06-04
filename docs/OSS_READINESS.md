# OSS Readiness

## Current Target
Prepare a credible `0.1.0-alpha.1` foundation with a small but useful CLI.

## Required Signals
- Clear README.
- License.
- Security policy.
- Contribution guide.
- Code of conduct.
- Changelog.
- Roadmap.
- CI.
- Tests.
- Task-first docs.
- Safe defaults.

## Release Stance
The first release should be small, working, documented, and honest about limitations.

## Current Local Readiness Notes
- Source archive hygiene is documented in `docs/SOURCE_ARCHIVE.md`.
- Local ZIP/RAR archives must exclude `.git/`, `.ackit/`, `bin/`, `obj/`, test output, coverage, packages, archives, logs, and local secrets.
- GitHub source packages differ from local working-directory archives because GitHub uses tracked content rather than ignored local artifacts.
- Self-scan stack detection reports the main product as `.NET`, `.NET CLI / .NET Tool`, and `GitHub Actions`.
- Sample projects under `samples/` are intentionally not reported as main repository stacks.
- Public release remains blocked until package URLs, release tag, push approval, and NuGet publish approval are resolved by the maintainer.
