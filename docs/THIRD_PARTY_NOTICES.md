# Third Party Notices

AgentContextKit uses xUnit v3 test tooling under the Apache-2.0 license. TASK-0091 validated `xunit.v3` `3.2.2` and `xunit.runner.visualstudio` `3.1.5`; these packages are test-only and are not shipped as CLI runtime dependencies.

Runtime dependencies for the CLI/Core MVP are intentionally minimized.

Package validation uses the installed .NET SDK and NuGet tooling; no additional runtime package dependency is introduced.

If additional third-party packages are added, record:
- Package name
- Version
- License
- Purpose
- Link to project or license
