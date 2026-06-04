# Next Steps

TASK-0049 cross-platform CI smoke test and alpha.2 preparation is completed locally.

1. Keep package URLs at `https://github.com/Cynrath/agent-context-kit`.
2. Treat GitHub repository public status, `master` push, and `v0.1.0-alpha.1` tag push as complete.
3. Treat GitHub Release page and NuGet publish for `AgentContextKit` version `0.1.0-alpha.1` as complete.
4. Treat NuGet global tool install verification and clean demo app smoke test as complete.
5. Keep the expected `ackit doctor` minimal-demo health failures documented as correct behavior.
6. Treat cross-platform CI smoke workflow coverage for Windows, Ubuntu, and macOS as prepared locally.
7. Hosted matrix execution requires a future manual push; do not push from the agent session.
8. Use TASK-0049 as alpha.2 preparation only; do not tag or publish.
9. Use `docs/CODEX_FOR_OSS_APPLICATION.md` to fill the Codex for OSS form.
10. Do not push, create GitHub releases, publish NuGet packages, redact, delete, force push, or create remotes from the agent session.
11. Keep the Turkish CLI ASCII fallback localization polish as a v0.2 backlog item, not a `0.1.0-alpha.1` blocker.
12. Keep the legacy v1.0 readiness reference available: `TASK-0039` and `scripts/check-v100-readiness.ps1`.
