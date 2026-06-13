# TASK-0127 Alpha.2 Supply-Chain Evidence Refresh

## Purpose
Replace predecessor-only supply-chain evidence with a dated, exact alpha.2 read-only audit.

## Current State
TASK-0099 evidence covers `0.2.0-alpha.1`; equivalent alpha.2 claims are intentionally unverified.

## Scope
Download public alpha.2 artifacts to ignored temp storage; record package/release digests, metadata, signature, assets, SBOM/provenance status, and owner identity.

## Out Of Scope
Signing, republishing, changing NuGet ownership, uploading SBOM/provenance, or editing release assets.

## Affected Files
Supply-chain evidence/status docs, audit scripts/tests, decision register, queue/handoff files.

## Implementation
Run reproducible read-only inspection and update evidence only from observed results.

## Security/Privacy Boundary
No credentials, local paths, certificate private data, or package contents with sensitive values are committed.

## Compatibility
Documentation/evidence only.

## Acceptance Criteria
Alpha.2 digest, metadata, signature type, asset digests, and attestation/SBOM observations are reproducible and accurately scoped.

## Tests
Evidence structure gate and digest/metadata validation.

## Validation
Package download/hash/verify, release JSON, attestation query, hygiene and documentation gates.

## Rollback
Revert evidence commit if any observed fact is incorrect.

## Completion Evidence
Pending.

## Commit
`docs: refresh alpha2 supply-chain evidence`

## Push
Normal push after validation.

## Hosted Checks
Standard 8/8 after push.
