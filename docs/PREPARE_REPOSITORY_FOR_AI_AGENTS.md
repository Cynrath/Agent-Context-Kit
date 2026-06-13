# Prepare A Repository For AI Coding Agents

This tutorial follows [Agent Context Pipeline](AGENT_CONTEXT_PIPELINE.md). Complete local review before any optional external prompt packer, graph, scanner, or SBOM workflow.

This workflow prepares an existing repository for safer use with Codex, Claude Code, Cursor, GitHub Copilot, or another coding agent. AgentContextKit runs locally and does not upload repository content or call a remote AI provider in the MVP.

Start with [First Five Minutes With Ackit](FIRST_FIVE_MINUTES.md) if the global tool is not installed yet.

## 1. Establish A Baseline

Run from the target repository root:

```powershell
git status --short --branch
ackit version
ackit --help
```

Before generation:

- Understand every existing worktree change; do not overwrite or hide user work.
- Identify the repository build and test commands.
- Confirm `.gitignore` covers build output, packages, local reports, environment files, and editor state.
- Locate README, architecture, security, contribution, CI, and release documentation.
- Check whether agent instruction files already exist.

If the worktree is dirty, record the current state and keep generated-file review separate from unrelated changes.

## 2. Initialize Local Configuration

```powershell
ackit init --lang en
Get-Content .ackit/config.yml
```

`init` creates `.ackit/config.yml` only when it is missing and reports detected agent instruction files. Existing config is not overwritten.

Use `--lang tr` when Turkish generated output is preferred.

## 3. Scan Before Generating Files

```powershell
ackit scan
ackit redact-check --profile public-release
ackit doctor
```

Use report-only `scan` first. Review:

- Main stack signals.
- README, license, security, test, CI, and agent-instruction health.
- Secret, PII, brand, production-config, local-path, package, and artifact findings.
- Doctor gaps that should be fixed before an agent receives the repository.

`doctor` can return a non-zero exit code when repository-health files are missing. That is a remediation list, not proof that the CLI failed.

Treat High and Critical findings as blockers until a human reviews the source. AgentContextKit reports risky content; it does not rotate credentials, redact values, delete files, or decide that a finding is safe.

## 4. Configure Narrowly

Review [Configuration](CONFIGURATION.md) and [Scanner Rules](SCANNER_RULES.md) before changing `.ackit/config.yml`.

Safe configuration rules:

- Add project-specific `brandKeywords` or `piiKeywords` only when they represent real review requirements.
- Use `safeDomains` only for known public technical domains.
- Prefer `ignoredPaths` over `ignorePaths` for non-Critical known noise so files remain visible in repository enumeration.
- Use `ignoredFindingIds` only for reviewed non-Critical rule noise.
- Never attempt to suppress a Critical secret-like finding with an allowlist.
- Do not broadly ignore `src/`, configuration directories, or the whole docs tree.

Re-run `ackit scan` after each config change and inspect current-source suppression audit output when available.

## 5. Generate Agent Instructions

Capture status before and after generation:

```powershell
git status --short
ackit generate --target all --lang en
git status --short
```

Targets can be generated separately with `codex`, `claude`, `cursor`, or `copilot`. The `all` target can create:

- `AGENTS.md`
- `CLAUDE.md`
- `.cursor/rules/project.mdc`
- `.github/copilot-instructions.md`
- Repository map, AI workflow, security, and development-standard docs.

Existing files are skipped by default. Generated files are starting points, not unquestioned policy. Review them against the actual architecture, commands, permissions, localization, security model, and deployment process.

Remove stale or incorrect statements before committing. Do not add secrets, private URLs, customer data, or machine-specific paths to agent instructions.

## 6. Establish Task-First Work

Create a concrete task before asking an agent to implement changes:

```powershell
ackit task "Add input validation to the import flow" --lang en
```

Complete the generated task with:

- Purpose and scope.
- Affected files and architecture boundaries.
- Database, admin, permission, SEO/i18n, audit, and security impact.
- Acceptance criteria and tests.
- Risks and rollback.

The task should be specific enough that an agent can inspect, implement, verify, and report without inventing product requirements.

## 7. Define Verification Commands

Put repository-specific commands in `AGENTS.md` and relevant task files. A typical .NET sequence is:

```powershell
dotnet restore YourSolution.sln
dotnet build YourSolution.sln -c Release --no-restore
dotnet test YourSolution.sln -c Release --no-build
ackit scan --ci
```

`scan --ci` returns non-zero for High or Critical findings. Use it only after report-only findings have been reviewed. Do not enable a CI workflow or required check without maintainer approval.

## 8. Keep Review Artifacts Local

Optional review outputs:

```powershell
ackit report --output .ackit/reports/readiness.html
ackit webui --output .ackit/webui/readiness.html
ackit sarif --output .ackit/reports/readiness.sarif
```

Generated `.ackit/` HTML, SARIF, prompt packs, and context-export manifests are local artifacts. Review them before sharing, keep them ignored, and do not treat SARIF generation as permission to upload Code Scanning data.

## 9. Repository-Ready Checklist

- Worktree baseline is understood.
- Build and tests pass or known failures are documented.
- README, license, security policy, contribution guidance, tests, CI, and `.gitignore` are present or intentionally tracked as gaps.
- `ackit scan` and `redact-check` findings are reviewed.
- No unresolved High/Critical finding is handed to an external agent or public release flow.
- Config allowlists are narrow, documented, and non-Critical only.
- Agent instruction files match the real stack and commands.
- A scoped task exists with acceptance criteria, tests, risks, and rollback.
- Generated local artifacts are ignored and unstaged.
- `ackit scan --ci` passes before the repository is declared ready.

## 10. Ongoing Maintenance

Re-run the preparation review when:

- Build/test commands or architecture change.
- A new agent tool is adopted.
- CI, permissions, auth, deployment, or data handling changes.
- The repository moves from private to public.
- A release or context export is prepared.

Keep agent instructions and tasks versioned with the code they describe.

## Rollback

Use `git status --short` to identify exactly what generation added. Remove or revert only the reviewed files from this adoption task. Do not use destructive bulk cleanup commands, and do not discard unrelated user changes.
