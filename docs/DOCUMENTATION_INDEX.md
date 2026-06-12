# Documentation Index

## Start Here
- `README.md`: project overview and quick start.
- `README.tr.md`: Turkish overview and quick start.
- `docs/FIRST_FIVE_MINUTES.md`: published-tool tutorial from install to scan, generated context, and first task.
- `docs/PREPARE_REPOSITORY_FOR_AI_AGENTS.md`: existing-repository security, config, agent instruction, task-first, and CI-readiness workflow.
- `docs/PRODUCT_SPEC.md`: product scope and MVP goals.
- `docs/ROADMAP.md`: version roadmap.

## Usage
- `docs/CLI_CONTRACT.md`: stable CLI command contract review.
- `docs/CLI_REFERENCE.md`: command reference.
- `docs/EXAMPLES.md`: common command flows.
- `docs/EXAMPLE_WORKFLOWS.md`: copy-paste-ready local workflow collections.
- `docs/SAMPLE_GALLERY.md`: sample repository gallery with expected stacks, health gaps, commands, and risk behavior.
- `docs/DEMO_SCENARIOS.md`: guided published package, source, sample, Web UI, report, and SARIF demos.
- `docs/GITHUB_ACTIONS_USAGE.md`: GitHub Actions CI usage, SARIF, and smoke workflow guidance.
- `docs/CODE_SCANNING_DECISION.md`: CodeQL and Code Scanning default/opt-in decision.
- `docs/SARIF_UPLOAD_WORKFLOW_DESIGN.md`: manual, job-scoped Code Scanning upload design.
- `docs/SAMPLES.md`: safe sample repositories.
- `docs/CONFIGURATION.md`: `.ackit/config.yml`.
- `docs/CONFIGURATION_DIAGNOSTICS.md`: stable config diagnostic codes, severities, safety checks, and compatibility boundary.
- `docs/CONFIG_GENERATED_CONVENTIONS.md`: stable config and generated-file conventions.
- `docs/SCANNER_RULES.md`: scanner rule catalog, SARIF mapping, and config allowlist behavior.
- `docs/SCANNER_FIXTURES.md`: scanner regression matrix and safe synthetic fixture conventions.
- `docs/BASELINE_MODEL.md`: versioned baseline identity, explicit local workflow, deterministic fingerprints, error codes, CI policy, and privacy boundary.
- `docs/SUPPRESSION_AUDIT.md`: sanitized local audit output for configured non-Critical suppressions.
- `docs/JSON_OUTPUT.md`: JSON output and exit codes.
- `docs/EXIT_CODES.md`: CLI exit code matrix.
- `docs/SARIF_OUTPUT.md`: SARIF 2.1.0 scanner output for local CI and future GitHub Code Scanning workflows.
- `docs/HTML_REPORTS.md`: offline static HTML report generation.
- `docs/WEB_UI_PROTOTYPE.md`: offline static Web UI prototype generation.
- `docs/WEB_UI_PREVIEW.md`: local Web UI preview, screenshot workflow, and public artifact boundaries.
- `docs/VISUAL_ASSETS.md`: public screenshot and visual asset policy.
- `docs/SCREENSHOT_CAPTURE_PLAN.md`: disposable demo, capture, sanitization, metadata, naming, and commit review plan.
- `docs/DOCS_SITE_PLAN.md`: hosted documentation and GitHub Pages decision, triggers, architecture, and maintainer-only activation plan.
- `docs/TROUBLESHOOTING.md`: common problems and fixes.
- `docs/FAQ.md`: frequently asked questions.
- `docs/SUPPORT_MATRIX.md`: supported OS, .NET, shell, and command coverage.

## Architecture And Development
- `docs/ARCHITECTURE.md`: solution structure and boundaries.
- `docs/DEVELOPMENT_STANDARD.md`: engineering workflow.
- `docs/SOURCE_HYGIENE.md`: source and package hygiene rules.
- `docs/SOURCE_ARCHIVE.md`: local ZIP/RAR source archive hygiene.
- `docs/assets/diagrams/ackit-flow.svg`: safe public flow diagram for README/docs.
- `docs/LOCALIZATION.md`: English/Turkish support.
- `docs/DECISIONS.md`: architecture decision records.
- `docs/LLM_INTEGRATION_ARCHITECTURE.md`: optional future LLM provider architecture.
- `docs/PROJECT_MAP.md`: generated project map.
- `docs/AI_WORKFLOW.md`: generated AI workflow.

## Security And Privacy
- `SECURITY.md`: security policy.
- `docs/SECURITY_MODEL.md`: scanner and trust boundary model.
- `docs/SCANNER_RULES.md`: stable `ACKIT` rule IDs and Critical suppression boundaries.
- `docs/SECURITY_NOTES.md`: generated security notes.
- `docs/PRIVACY.md`: local-only data handling.

## OSS And Maintainers
- `CONTRIBUTING.md`: contribution rules.
- `docs/CONTRIBUTOR_ONBOARDING.md`: contributor setup, task-first workflow, and validation.
- `CODE_OF_CONDUCT.md`: conduct expectations.
- `docs/SUPPORT.md`: support scope.
- `docs/SUPPORT_MATRIX.md`: supported platforms, .NET version, shells, and tested command classes.
- `docs/MAINTAINER_GUIDE.md`: maintainer change, release, NuGet, and rollback workflow.
- `docs/GITHUB_REPO_HYGIENE.md`: GitHub metadata, templates, branch protection, and Actions hygiene.
- `docs/GITHUB_LABELS.md`: recommended GitHub labels and optional maintainer-only label commands.
- `docs/GITHUB_SETTINGS_CHECKLIST.md`: repository metadata, branch protection, security, and release settings checklist.
- `docs/ISSUE_BACKLOG.md`: copy-ready first issue backlog.
- `docs/ISSUE_TRIAGE.md`: issue labels, routing, severity, and closure rules.
- `docs/GOVERNANCE.md`: decision and release governance.
- `docs/MAINTAINERS.md`: maintainer responsibilities.
- `docs/OSS_READINESS.md`: OSS readiness goals.
- `docs/CODEX_FOR_OSS_APPLICATION.md`: Codex for OSS application pack.
- `docs/THIRD_PARTY_NOTICES.md`: dependency/license notes.

## Packaging And Release
- `docs/PACKAGING.md`: NuGet tool packaging.
- `scripts/test-samples.ps1`: local sample smoke validation helper.
- `docs/examples/github-actions-scan-ci.yml`: non-active CI scan example.
- `docs/examples/github-actions-sarif-upload.yml`: non-active example workflow for SARIF upload after maintainer approval.
- `docs/examples/github-actions-published-tool-smoke.yml`: non-active published NuGet tool smoke example.
- `docs/examples/github-actions-source-package-smoke.yml`: non-active current source package smoke example.
- `docs/NUGET_METADATA.md`: NuGet package metadata review workflow.
- `docs/V020_READINESS.md`: v0.2 local readiness review.
- `docs/V020_ALPHA2_SCOPE.md`: compatibility-preserving alpha.2 package scope, exclusions, and release gates.
- `docs/V030_READINESS.md`: v0.3 local readiness review.
- `docs/V030_ROADMAP_DECISION.md`: next v0.3 product direction, compatibility rules, security boundaries, and candidate delivery sequence.
- `docs/V040_READINESS.md`: v0.4 local readiness review.
- `docs/V050_READINESS.md`: v0.5 local readiness review.
- `docs/V100_STABILIZATION_PLAN.md`: v1.0 local stabilization plan.
- `docs/V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md`: v1.0 documentation and release gate freeze.
- `docs/V100_READINESS.md`: v1.0 final local readiness review.
- `docs/V100_GAP_ANALYSIS.md`: current 1.0 P0/P1/P2 gap register, owners, evidence, and release criteria.
- `docs/MAINTAINER_RELEASE_HANDOFF.md`: maintainer-only public release handoff.
- `docs/PUBLIC_RELEASE_AUDIT.md`: final public release audit workflow.
- `docs/PUBLIC_RELEASE_GATES.md`: package metadata, audit, and blocker gate orchestration.
- `docs/RELEASE_VALIDATION.md`: local release validation.
- `docs/RELEASE_BLOCKERS.md`: public-release blocker/follow-up state and guard script.
- `docs/RELEASE_CHECKLIST.md`: release checklist.
- `docs/RELEASE_BODY_V020_ALPHA1.md`: corrected GitHub Release body draft for the published `v0.2.0-alpha.1` pre-release.
- `docs/NEXT_TASKS.md`: unified next task roadmap.
- `docs/PROJECT_EXECUTION_QUEUE.md`: execution queue with validation and remote-write status.
- `docs/RELEASE_CANDIDATE_0.1.0-alpha.1.md`: current RC report.
- `docs/tasks/TASK-0065-v020-alpha1-publish-verification.md`: `0.2.0-alpha.1` publication verification and docs sync.
- `CHANGELOG.md`: release notes.

## Task Tracking
- `docs/tasks/`: task-first implementation records.
- `.codex/SESSION_HANDOFF.md`: current session handoff.
- `.codex/NEXT_STEPS.md`: next action list.
