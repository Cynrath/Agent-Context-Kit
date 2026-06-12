# TASK-0099 Published Supply-Chain Status Audit

## Purpose
Audit the published `AgentContextKit` `0.2.0-alpha.1` package and GitHub Release using read-only evidence so signing, SBOM, provenance, and recovery decisions start from an exact current state rather than assumptions.

## Scope
- Inspect the published NuGet package signature state and archive contents without committing the package.
- Inspect the published GitHub Release asset list without modifying the release.
- Record whether author signing, repository signing, SBOM, provenance/attestation, and recovery evidence are present.
- Update the supply-chain evidence register, maintainer handoff, RC decision/readiness, gap register, release validation/checklist, queue, and Codex handoffs.
- Add a local gate that protects the documented published-state boundary.

## Out Of Scope
- No signing, certificate handling, SBOM generation/publication, provenance generation/publication, package deprecation/unlisting, release edit, workflow dispatch, push, tag, GitHub Release, NuGet publish, or candidate approval.
- No maintainer risk acceptance is inferred from missing artifacts.

## Affected Files
- `docs/PUBLISHED_SUPPLY_CHAIN_STATUS.md`
- `docs/SECURITY_SUPPLY_CHAIN_EVIDENCE.md`
- `docs/MAINTAINER_SECURITY_SUPPLY_CHAIN_HANDOFF.md`
- RC/readiness/release/roadmap/queue/index/map/changelog/Codex docs.
- `scripts/check-published-supply-chain-status.ps1` and integrated readiness gates.

## DB Impact
None.

## Admin Impact
None locally. Any release edit, package lifecycle action, signing setup, or security setting change remains maintainer-only.

## Permission Impact
The audit uses public/read-only NuGet and GitHub data. Remote writes require explicit maintainer authorization and are not performed.

## SEO/i18n Impact
None. Operational supply-chain documentation remains English.

## Audit/Security Impact
- Store package hashes, signature type/status, public release asset names, and artifact-presence conclusions only.
- Do not store credentials, certificate private material, tokens, user paths, or downloaded package binaries.
- Distinguish NuGet repository signing from author signing.

## Acceptance Criteria
- Published package identity and SHA-256 are recorded from a fresh read-only download.
- Signature verification records author-signature and repository-signature truth separately.
- Package and GitHub Release contents are checked for SBOM and provenance/attestation artifacts.
- Recovery remains a documented maintainer decision rather than an automatically executed package action.
- Local evidence gates, full validation, hygiene, and post-commit public release gates pass.

## Tests
Download the public package to an ignored local path, verify it with the installed .NET SDK, inspect archive entries, inspect GitHub Release assets, remove generated audit artifacts, then run restore/build/tests/scan/doctor/JSON/SARIF/sample/readiness/release/hygiene gates.

## Risks
- Treating a NuGet repository signature as an author signature would overstate publisher identity assurance.
- Absence of SBOM/provenance in package or release assets does not prove none exists elsewhere; conclusions must name the inspected surfaces.
- Downloaded packages or generated reports could be committed accidentally; audit outputs must remain ignored and be removed after use.

## Rollback Plan
Revert the TASK-0099 commit. No remote state is changed.

## Completion Notes
- Task created before the published package and release asset audit.
- Downloaded the exact public `AgentContextKit` `0.2.0-alpha.1` package to an ignored path and recorded SHA-256 `d6d0804f5dfca7c03f2b8ea173de74e3b0e9e8eb73ceec5dda9ac18cae1988ce`.
- `dotnet nuget verify --all --verbosity detailed` passed and reported a NuGet.org `Repository` signature; no author signature was observed.
- Package metadata uses `Cynrath`, while the public NuGet owner profile is `Cyranth`; the mismatch is recorded as a maintainer-only P1 blocker before RC.
- The 13-entry package and GitHub Release asset list contained no SBOM/provenance artifact. `gh attestation verify` returned no accessible attestation for the exact package digest.
- Added `docs/PUBLISHED_SUPPLY_CHAIN_STATUS.md` and `scripts/check-published-supply-chain-status.ps1`, then integrated the evidence into security, RC, v1.0, release, roadmap/queue, index/map, changelog, and Codex docs.
- Restore, zero-warning Release build, 178/178 tests, clean scan/doctor, zero-result JSON/SARIF, sample smoke, clean dependency review, local package verification, and all integrated gates passed.
- The final 2,000-file synthetic benchmark completed in 3.282 seconds against the 30-second local threshold.
- The downloaded package and generated SARIF were removed; no generated package, report, SARIF, HTML, or archive is included in the task changes.
- No ownership change, signing, certificate handling, SBOM/provenance generation or publication, attestation, release edit, package recovery action, push, tag, GitHub Release, or NuGet publish was performed.
