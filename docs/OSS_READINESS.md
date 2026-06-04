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
- Package metadata uses the selected public URL `https://github.com/Cynrath/agent-context-kit`.
- Codex for OSS application material is prepared in `docs/CODEX_FOR_OSS_APPLICATION.md`.
- GitHub repository is public.
- `master` and `v0.1.0-alpha.1` are pushed and point to `aee808244bf33d00808e7e70db6235132c2d3829`.
- GitHub Actions latest `master` run is green.
- Repository description and topics are set.
- GitHub Release page is completed: `https://github.com/Cynrath/agent-context-kit/releases/tag/v0.1.0-alpha.1`.
- NuGet publish is completed for `AgentContextKit` version `0.1.0-alpha.1`.
- NuGet global tool install is verified.
- NuGet global tool smoke test is verified in a clean demo app, including init, scan, generation, task creation, report, Web UI, prompt-pack, context-export, JSON output, and redact-check Critical detection.
- `v0.1.0-alpha.1` is published on GitHub and NuGet.
