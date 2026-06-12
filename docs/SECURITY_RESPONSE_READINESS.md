# Security Response Readiness

## Current State
AgentContextKit is an offline-first pre-release tool. `SECURITY.md` warns users not to place secrets, private source, credentials, or customer data in public issues.

## Supported Version
Security fixes target the latest published pre-release. The immediately previous release may be used for rollback/upgrade verification but is not promised ongoing fixes.

## Proposed Response Targets
These are maintainer targets, not contractual SLAs:
- acknowledge a private report within 3 business days;
- complete initial severity/reproduction triage within 7 business days;
- publish remediation timing after impact and release-path review;
- coordinate disclosure so a fix or mitigation is available before public technical detail when practical.

## Local Evidence
- Critical secret/token regression fixtures.
- Config rules cannot suppress Critical findings.
- Baseline status does not hide Critical findings.
- SARIF baseline metadata omits raw matches.
- Hygiene and public release gates scan tracked source/artifacts.

## Maintainer-Only Blocker
A private vulnerability reporting channel must be enabled and verified on GitHub before a 1.0 release candidate. Do not request sensitive details through a public issue. Repository security settings are remote writes and are not changed by this task.

## Dependency Review
Before an RC, run:

```powershell
dotnet list AgentContextKit.sln package --vulnerable --include-transitive
dotnet list AgentContextKit.sln package --deprecated
```

Record the dated result in the release-candidate evidence pack. Network/source availability can affect these commands, so they are maintainer validation rather than an unconditional local gate.

## Incident Records
For a confirmed issue, record affected versions, severity, remediation commit, release/tag/package actions, credential rotation guidance when relevant, and disclosure timeline. Never store reporter secrets or private source in repository docs.
