# TASK-0095 Security Reporting And Supply-Chain Maintainer Evidence Handoff

## Purpose
Consolidate the maintainer-only evidence required for private vulnerability reporting, dependency review, package signing, SBOM, provenance, and bad-package recovery without performing remote settings changes, signing, publication, or release actions.

## Scope
- Create one dated security/supply-chain evidence register that distinguishes verified local facts, proposed decisions, pending maintainer evidence, and completed remote evidence.
- Create a copy-paste maintainer handoff for enabling/verifying private vulnerability reporting and recording signing/SBOM/provenance/recovery decisions.
- Define acceptable evidence fields: date, maintainer, candidate commit/version, setting or run URL, decision, publication location, recovery owner, and residual-risk acceptance.
- Add a local gate that validates the handoff/evidence structure and prevents pending remote actions from being represented as complete.
- Integrate the gate into release-candidate evidence and v1.0 readiness/documentation gates.
- Update security, supply-chain, maintainer decision, release/readiness, gap, roadmap/queue/changelog, index/map, and Codex handoff docs.

## Out Of Scope
- No GitHub settings changes, private report test submission, security advisory, signing key/certificate handling, package signing, SBOM generation/upload, provenance attestation, workflow activation, push, tag, release, or NuGet publish.
- No credential, certificate identifier, private report content, secret, customer data, or private URL is stored in repository docs.
- This task does not approve an RC or mark maintainer-only evidence complete.

## Affected Files
- `docs/SECURITY_SUPPLY_CHAIN_EVIDENCE.md`
- `docs/MAINTAINER_SECURITY_SUPPLY_CHAIN_HANDOFF.md`
- `scripts/check-security-supply-chain-evidence.ps1`
- `SECURITY.md`
- `docs/SECURITY_RESPONSE_READINESS.md`
- `docs/SUPPLY_CHAIN_POLICY.md`
- `docs/MAINTAINER_RC_DECISION.md`
- Release/readiness/roadmap/queue/changelog/index/map/Codex docs.

## DB Impact
None.

## Admin Impact
GitHub security/settings actions are documented for the maintainer but are not performed.

## Permission Impact
Remote security settings, attestations, signing, and publication require maintainer credentials and explicit action. The local gate is read-only.

## SEO/i18n Impact
None. Evidence terminology remains English and operational; no public marketing claim is added.

## Audit/Security Impact
The evidence register must never contain reporter secrets, raw vulnerability details, credentials, certificate private data, customer data, or machine-local paths. Pending evidence must stay visibly pending until a maintainer records verifiable facts.

## Acceptance Criteria
- Private reporting, dependency review, signing, SBOM, provenance, and recovery each have explicit owner, status, evidence, and decision fields.
- Local facts and maintainer-only evidence are clearly separated.
- Private vulnerability reporting remains a required pre-RC maintainer action.
- Signing/SBOM/provenance options and recommended default are documented without falsely claiming activation or publication.
- Bad-package recovery preserves NuGet immutability and uses successor/unlist/deprecate guidance.
- The local evidence gate passes while remote items remain explicitly pending.
- Full local gates and post-commit public release gates pass.

## Tests
Run the focused evidence gate, restore/build/full tests, scan/doctor, JSON/SARIF parse, sample smoke, dependency review, localization/contract/readiness/RC/release gates, hygiene scans, diff checks, and post-commit public release gates.

## Risks
- A checklist can be mistaken for completed evidence; status vocabulary and required evidence fields must prevent that.
- Publishing certificate, report, or advisory details can create new security exposure; repository records must stay metadata-only.
- Deferring signing/SBOM/provenance without an owned residual-risk decision would leave the P1 gap unresolved.

## Rollback Plan
Revert the TASK-0095 commit. No runtime, package, workflow, security setting, signing, publication, or remote state is changed.

## Completion Notes
- Task created after TASK-0094 commit `c8c5e62` and successful post-commit public release gate.
- Added `docs/SECURITY_SUPPLY_CHAIN_EVIDENCE.md`, `docs/MAINTAINER_SECURITY_SUPPLY_CHAIN_HANDOFF.md`, and `scripts/check-security-supply-chain-evidence.ps1`.
- Integrated the evidence gate into release-candidate evidence, v1.0 documentation, and v1.0 readiness checks.
- Recorded private vulnerability reporting, NuGet signing, SBOM, provenance, and bad-package recovery as `PENDING MAINTAINER`; no remote setting or publication was represented as complete.
- Used official GitHub and Microsoft references for private reporting, SBOM export, artifact attestations, and NuGet package signing.
- Vulnerable and deprecated dependency reviews passed. Restore, zero-warning Release build, 178/178 tests, clean scan, doctor PASS, JSON/SARIF parse, sample smoke, and local package verification passed.
- The 2,000-file synthetic scan benchmark completed in 3.368 seconds against the 30-second local tripwire.
- Identity, tracked-artifact, fake-token/local-path, and diff hygiene checks passed. A prose-only permissions description replaced an exact workflow permission token that created self-scan noise.
- All local contract, localization, security/supply-chain, readiness, release-candidate, workflow, documentation, and release verification gates passed.
- Remote security settings, certificates, signing, SBOM/provenance publication, hosted evidence, and final RC approval remain maintainer-only.
