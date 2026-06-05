# OSS Readiness

## Current Target
Maintain the completed `v0.1.0-alpha.2` public release and keep future release work local-only until maintainer approval.

## Required Signals
- Clear README.
- License.
- Security policy.
- Contribution guide.
- Contributor onboarding.
- Code of conduct.
- Issue templates.
- Pull request template.
- Support matrix.
- Maintainer guide.
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
- `v0.1.0-alpha.2` tag is pushed.
- GitHub Actions latest `master` run is green.
- Read-only GitHub CLI validation on 2026-06-05 confirmed `ci`, `cross-platform-smoke`, and `cross-platform-source-smoke` passed for commit `8dac9237c27ba912d056344155f1c9f901557bf5`.
- Repository description and topics are set.
- GitHub Release page is completed for `v0.1.0-alpha.2`.
- NuGet publish is completed for `AgentContextKit` version `0.1.0-alpha.2`.
- NuGet global tool install is verified for `0.1.0-alpha.2`.
- NuGet global tool smoke test is verified in a clean demo app, including init, scan, generation, task creation, report, Web UI, prompt-pack, context-export, JSON output, and redact-check Critical detection.
- Cross-platform GitHub Actions smoke workflow completed successfully on commit `868dff3` for Windows, Ubuntu, and macOS using the published NuGet global tool.
- `v0.1.0-alpha.2` is published on GitHub and NuGet.
- Codex for OSS form submission is completed per maintainer-provided status.
- Published-package smoke workflow now installs `AgentContextKit` `0.1.0-alpha.2`.
- Local `.ackit/reports/` and `.ackit/webui/` outputs are ignored local review artifacts and are not public release artifacts.
- GitHub issue templates, pull request template, maintainer guide, contributor onboarding, support matrix, repository hygiene, and issue triage docs are present.
