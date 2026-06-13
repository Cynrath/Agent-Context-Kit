# Maintainer Decision Register

This register records future maintainer decisions. It contains no approval yet.

| Decision ID | Related blocker | Status | Decision | Rationale | Owner role | Evidence | Effective scope | Review/expiry | Remote action |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| MD-001 | RB-001 hosted evidence | Pending maintainer | TBD | Manual three-OS evidence is required | Release maintainer | Workflow run URLs/results | Next candidate only | TBD | Push/dispatch required |
| MD-002 | RB-002 private reporting | Pending maintainer | TBD | Verified disabled state must be changed and re-verified | Security maintainer | GitHub setting/read-only verification | Repository | TBD | Setting change required |
| MD-003 | RB-003 notifications | Pending maintainer | TBD | Private reports require primary/backup ownership | Security maintainer | Tested notification route | Repository | TBD | Possible settings/contact action |
| MD-004 | RB-004 owner identity | Pending maintainer | TBD | `Cyranth`/`Cynrath` mismatch needs an owned disposition | Package maintainer | NuGet owner/profile evidence | Package ownership | TBD | Possible owner change |
| MD-005 | RB-005 author signing | Pending maintainer | TBD | Current package has repository signature only | Package/security maintainer | Exact package signature verification | Next candidate | TBD | Signing pipeline if selected |
| MD-006 | RB-006 SBOM | Pending maintainer | TBD | Privacy and publication lifecycle are unresolved | Release/security maintainer | Exact SBOM/digest/location or defer record | Next candidate | TBD | Publication if selected |
| MD-007 | RB-007 provenance | Pending maintainer | TBD | No accessible package attestation is recorded | Release/security maintainer | Attestation and subject digest or defer record | Next candidate | TBD | Attestation if selected |
| MD-008 | RB-008 recovery | Pending maintainer | TBD | Incident ownership and actions require acceptance | Package/security maintainer | Procedure/tabletop evidence | Package lifecycle | TBD | Only during setup/incident |
| MD-009 | RB-009 candidate | Pending maintainer | TBD | Candidate selection waits for prerequisite decisions | Release maintainer | Scope/version/commit/package diff | Next candidate | TBD | Later release writes |
| MD-010 | RB-010 approval | NO-GO | No release approval | Required P0/P1 evidence is incomplete | Release maintainer | Full decision packet | Exact future candidate | Until replaced | No remote write authorized |

## Recording A Decision
Replace `TBD` only with explicit maintainer evidence. Record the exact commit/version scope. Accepted risk must include why, compensating controls, owner role, review/expiry date, and rollback/recovery path. Do not use this register to store credentials, certificates, private report content, or recovery secrets.
