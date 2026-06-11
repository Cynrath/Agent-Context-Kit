# GitHub Settings Checklist

This checklist documents maintainer-only GitHub repository settings for AgentContextKit. It does not require or authorize an agent to change repository metadata.

## Repository Metadata
Description:

```text
Offline-first CLI for generating safe AI coding agent context, task-first workflows, repo hygiene reports, and multi-agent instruction files.
```

Topics:
- `ai-tools`
- `coding-agents`
- `codex`
- `developer-tools`
- `dotnet`
- `cli`
- `repository-scanner`
- `agents-md`
- `open-source`
- `security`

## Branch Protection
Recommended default branch:
- `master`

Recommended protection after the solo bootstrap phase:
- Require pull request before merging.
- Require status checks before merging:
  - `ci`
  - `cross-platform-source-smoke`
- Consider requiring branches to be up to date before merging if contributor volume increases.
- Block force pushes.
- Block branch deletion.

Published-package smoke note:
- `cross-platform-smoke` validates the latest published NuGet package. Keep it green before release announcements, but consider whether it should block every PR if external NuGet availability causes noise.

## Security Settings
Recommended:
- Enable Dependabot alerts.
- Enable Dependabot security updates.
- Enable secret scanning if available for the repository plan.
- Consider CodeQL as a future hardening item once the project has enough code surface to justify it.

## Release Settings
- Mark alpha GitHub Releases as pre-release.
- Keep NuGet publish as a maintainer-only action.
- Do not publish `.nupkg` or `.snupkg` files as source repository artifacts.
- Update README install commands, release validation docs, and published-package smoke workflow when the current NuGet version changes.

## Manual Verification Checklist
- Repository is public.
- Description matches the approved text.
- Topics match the approved list.
- Default branch is `master`.
- Milestones are created only when a maintainer wants issue grouping for the next alpha.
- `ci` is green.
- `cross-platform-source-smoke` is green.
- `cross-platform-smoke` is green for the published package.
- Issue templates are visible in the GitHub issue picker.
- Pull request template appears on new PRs.
- Labels match `docs/GITHUB_LABELS.md`.
- Branch protection is configured intentionally for the current maintainer workflow.
- Dependabot and secret scanning settings have been reviewed.
- GitHub Release for the current alpha is marked as a pre-release.
- GitHub Release body matches `docs/RELEASE_BODY_V020_ALPHA1.md` if maintainer chooses to polish the published release text.
- NuGet package ownership and API key handling remain maintainer-controlled.

## Maintainer-Only Manual Actions
- Create or update labels from `docs/GITHUB_LABELS.md`.
- Configure branch protection and required checks.
- Review repository settings, topics, and security settings.
- Create milestones if the issue tracker needs version grouping.
- Edit the GitHub Release body if stale wording should be corrected.

## Optional Read-Only Checks
These commands inspect state and do not mutate repository settings:

```powershell
gh repo view Cynrath/agent-context-kit
gh run list --repo Cynrath/agent-context-kit --limit 10
gh run list --repo Cynrath/agent-context-kit --workflow ci.yml --limit 3
gh run list --repo Cynrath/agent-context-kit --workflow cross-platform-smoke.yml --limit 3
gh run list --repo Cynrath/agent-context-kit --workflow cross-platform-source-smoke.yml --limit 3
```
