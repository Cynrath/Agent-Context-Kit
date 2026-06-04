# Next Steps

TASK-0041 through TASK-0045 are active for final local public release preparation.

1. Keep package URLs at `https://github.com/Cynrath/agent-context-kit`.
2. Keep `origin` at `https://github.com/Cynrath/agent-context-kit.git`.
3. Finish public GitHub/source archive hygiene checks.
4. Finish the `v0.1.0-alpha.1` changelog, release candidate, packaging, and handoff docs.
5. Add `docs/CODEX_FOR_OSS_APPLICATION.md`.
6. Run restore, build, test, scan, doctor, release verification, public gates, audit, blocker checks, artifact scan, and real-name scan.
7. Commit the reviewed local release preparation changes after build/test pass.
8. Create local tag `v0.1.0-alpha.1` only after the final reviewed commit and passing gates.
9. Do not push, publish, redact, delete, force push, or create remotes from the agent session.
10. Follow `docs/MAINTAINER_RELEASE_HANDOFF.md` for maintainer-only push and NuGet publish.
11. Keep the legacy v1.0 readiness reference available: `TASK-0039` and `scripts/check-v100-readiness.ps1`.
