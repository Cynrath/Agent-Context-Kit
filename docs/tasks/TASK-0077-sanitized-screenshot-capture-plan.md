# TASK-0077 Sanitized Screenshot Capture Plan

## Purpose
Define a repeatable, privacy-safe process for capturing future README and documentation screenshots without committing generated local HTML or machine-specific data.

## Scope
- Select safe screenshot candidates and source scenarios.
- Define capture, crop, sanitization, optimization, naming, and review steps.
- Reserve the public screenshot asset directory with contributor guidance.
- Keep README text-only until reviewed assets exist.
- Sync visual asset, Web UI preview, repository hygiene, issue backlog, roadmap, queue, and Codex handoff docs.

## Out Of Scope
- No screenshot, GIF, or video capture.
- No generated or fabricated image.
- No committed `.ackit/`, HTML, SARIF, package, archive, or machine-specific artifact.
- No push, tag, NuGet publish, GitHub Release edit, Pages activation, or repository setting change.

## Affected Files
- `docs/SCREENSHOT_CAPTURE_PLAN.md`
- `docs/assets/screenshots/README.md`
- `docs/VISUAL_ASSETS.md`
- `docs/WEB_UI_PREVIEW.md`
- `docs/DOCUMENTATION_INDEX.md`
- `docs/PROJECT_MAP.md`
- `docs/GITHUB_REPO_HYGIENE.md`
- `docs/ISSUE_BACKLOG.md`
- `docs/ROADMAP.md`
- `docs/NEXT_TASKS.md`
- `docs/PROJECT_EXECUTION_QUEUE.md`
- `.codex/*`

## DB Impact
None.

## Admin Impact
None.

## Permission Impact
None. Future GitHub asset publication remains a normal reviewed commit, not an automated remote write.

## SEO/i18n Impact
The plan covers English README placement first and requires equivalent Turkish alt text/caption review before an asset is linked from both README files.

## Audit/Security Impact
The process rejects absolute paths, user names, machine names, private repository data, customer data, raw finding matches, tokens, and personal email addresses. Generated local HTML remains ignored and untracked.

## Acceptance Criteria
- A dedicated screenshot capture plan defines source scenarios, candidate views, naming, dimensions, formats, sanitization, and reviewer checks.
- The screenshot directory contains guidance only; no image is fabricated or committed.
- README remains text-only until a sanitized asset passes review.
- Existing local-only Web UI/report rules stay explicit.
- Queue, roadmap, issue backlog, and handoff docs record the completed plan and pending manual capture.
- Documentation, hygiene, and release gates pass.

## Tests
Run documentation link/search checks, build/test/scan/doctor, hygiene scans, `git diff --check`, and release gates.

## Risks
- A future capture may expose a local path in a header even when the main dashboard is generic.
- Image metadata can retain authoring or location information.
- README visuals can become stale as the Web UI changes.

## Rollback
Revert the TASK-0077 documentation commit. No runtime behavior or generated asset is changed.

## Completion Notes
- Task created after TASK-0076.
- Added a disposable-demo capture plan, prioritized filenames/views, crop and metadata rules, sanitization rejection criteria, README placement guidance, and commit review commands.
- Added `docs/assets/screenshots/README.md` as policy guidance only; no screenshot, generated HTML, or fabricated image was created or committed.
- Validation passed: restore, Release build with 0 warnings/errors, 127/127 tests, clean `scan --ci`, doctor PASS, JSON parse, SARIF parse, sample smoke, screenshot-directory check, hygiene scans, diff check, CLI/config/v0.2/v1.0 gates, and local release verification.
- The pre-commit public release gate failed only on the expected dirty working tree; the post-commit rerun passed with no blocking items.
