# Documentation Site Plan

The current toolchain decision is `docs/DOCS_QUALITY_TOOLCHAIN_DECISION.md`: Markdown remains canonical, no docs dependency is added, and site generators stay deferred.

Status: deferred. Repository Markdown remains the canonical documentation surface.

## Current Observation
Read-only checks on 2026-06-12 found:

- No Pages deployment workflow under `.github/workflows/`.
- No Jekyll, MkDocs, Docusaurus, Astro, Vite, or other site generator configuration.
- The GitHub Pages API returned `404`; the practical inference is that no Pages site is currently configured for this repository.
- Latest checked `ci`, published-package smoke, and source-package smoke runs were successful.

No repository setting was changed while gathering this information.

## Decision
Do not enable GitHub Pages yet.

The current README plus `docs/DOCUMENTATION_INDEX.md` provide a usable, version-controlled documentation entry point. Adding a hosted site now would introduce navigation, base-path, link-checking, accessibility, and maintenance work without resolving a current product blocker.

Revisit the decision when at least one trigger is true:

- Users repeatedly report that repository navigation is difficult.
- The docs need full-text search, versioned releases, or a guided multi-page tutorial experience.
- The project reaches a stable beta/1.0 documentation freeze.
- The documentation set grows enough that the index is no longer practical.
- Sanitized screenshot assets are approved and a public presentation surface has clear value.

## Options Considered
| Option | Benefits | Costs/Risks | Current Decision |
| --- | --- | --- | --- |
| Repository Markdown | Zero hosting setup, reviewable with code, no new dependencies | Limited search and site navigation | Keep as canonical now. |
| Native GitHub Pages from `master` `/docs` | Low infrastructure overhead, repository-controlled content | Requires Pages setting, base-path/link validation, minimal site entry/config work | Preferred future first experiment. |
| Actions-based Pages deployment | Flexible build and validation | Adds workflow permissions and deployment maintenance | Use only if branch-based Pages is insufficient. |
| Third-party generator/framework | Rich navigation and search | Adds Node/Python/Ruby tooling and dependency churn | Do not add without a documented need. |

## Proposed Future Information Architecture
If Pages is enabled later, start with a small structure:

1. Overview and install.
2. First five minutes tutorial.
3. CLI reference and exit codes.
4. Configuration, scanner rules, and suppression audit.
5. Reports, Web UI, JSON, and SARIF.
6. Security/privacy model.
7. Contributor and maintainer guides.
8. Release and packaging references.

English should be the primary site navigation. Keep `README.tr.md` directly reachable, and add localized site navigation only when translated pages can be maintained without drift.

## Preferred Future Implementation
Start with a short-lived branch or local spike before changing repository settings:

- Use the existing `docs/` directory as source.
- Add a minimal `docs/index.md` that routes to existing Markdown instead of duplicating it.
- Evaluate native GitHub Pages/Jekyll first; do not add a package manager solely for docs.
- Validate all relative links under the repository project base path.
- Keep visual assets under `docs/assets/` and follow `docs/VISUAL_ASSETS.md`.
- Keep generated `.ackit` HTML, SARIF, local reports, Web UI files, packages, and archives out of the site source.

An Actions deployment should be a separate task. If selected, review the minimum Pages permissions, pin official actions to reviewed major versions, and keep deployment isolated from release publishing.

## Privacy And Security Requirements
- No analytics, tracking pixels, telemetry, comments widget, or external fonts by default.
- No raw scanner match, secret-like value, local path, private repository data, or personal information.
- No generated local Web UI/report HTML copied into the site.
- No unreviewed third-party JavaScript or CDN dependency.
- No API key or deployment secret stored in repository files.
- Screenshot assets must pass `docs/SCREENSHOT_CAPTURE_PLAN.md`.

## Quality Gate Before Activation
- Navigation works from the repository project base path, not only from a domain root.
- Internal links and image paths are checked.
- Mobile layout and keyboard navigation are reviewed.
- Headings, alt text, color contrast, and focus states are accessible.
- English/Turkish links do not imply untranslated coverage.
- Self-scan, hygiene checks, build/tests, and public release gates pass.
- A maintainer reviews the rendered Pages preview before enabling the production source.

## Maintainer-Only Activation
Future remote actions require explicit maintainer approval:

1. Choose branch-based Pages or an Actions deployment.
2. Configure Pages source in repository settings.
3. If Actions is used, review required Pages permissions and environment protection.
4. Verify the published URL, navigation, and asset paths.
5. Add the docs URL to repository metadata only after validation.

## Rollback
Disable Pages in repository settings, remove any Pages-only workflow/config in a reviewed commit, and keep repository Markdown as the canonical fallback. No product runtime or NuGet package rollback is required.
