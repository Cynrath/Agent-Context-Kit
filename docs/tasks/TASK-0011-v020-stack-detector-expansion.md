# TASK-0011: v0.2 Stack Detector Expansion

## Purpose
Start v0.2 product work by expanding stack detection beyond simple filename checks, while keeping the CLI offline-first and behavior-compatible.

## Scope
- Detect official .NET project SDK signals from project files.
- Detect ASP.NET Core, Razor, Blazor WebAssembly, Worker Service, and Minimal API signals.
- Detect Node package manager, TypeScript, and Tailwind CSS signals.
- Preserve existing stack detector behavior.
- Add focused tests for new stack signals.
- Update docs and handoff.

## Out of scope
- Web UI.
- LLM integration.
- NuGet publish.
- GitHub push.
- Remote repository creation.
- Release tag creation.
- Replacing TODO package URLs.
- Broad scanner rewrites.
- New dependencies.

## Affected files
- `src/AgentContextKit.Core/Scanning.cs`
- `src/AgentContextKit.Cli/Program.cs`
- `tests/AgentContextKit.Tests/AgentContextKitBehaviorTests.cs`
- `docs/ROADMAP.md`
- `docs/ARCHITECTURE.md`
- `docs/PROJECT_MAP.md`
- `docs/DOCUMENTATION_INDEX.md`
- `CHANGELOG.md`
- `.codex/SESSION_HANDOFF.md`
- `.codex/NEXT_STEPS.md`
- `docs/tasks/TASK-0011-v020-stack-detector-expansion.md`

## DB impact
None.

## Admin impact
None.

## Permission impact
None.

## SEO/i18n impact
Documentation-only impact. Stack names are English technical labels in CLI output and JSON.

## Audit/security impact
No secret handling behavior changes. Reading project and small source files for stack detection remains local-only and uses existing file-system boundaries.

## Architecture impact
`StackDetector` may use `IFileSystem` for testable project/source file reads. CLI/Core boundaries remain unchanged.

## CLI impact
`ackit scan` can report more stack entries for the same repository. JSON schema shape stays unchanged.

## Testing impact
Add unit tests for .NET SDK and frontend stack signals. Run build/test/scan/release verification.

## OSS/release impact
Improves repository analysis accuracy for v0.2 while keeping public release blockers unchanged.

## Current docs consulted
- Microsoft Learn: `.NET project SDKs`, including `Microsoft.NET.Sdk.Web`, `Microsoft.NET.Sdk.Razor`, `Microsoft.NET.Sdk.BlazorWebAssembly`, and `Microsoft.NET.Sdk.Worker`.

## Acceptance criteria
- Existing `.NET` and `Node` detections still work.
- `Microsoft.NET.Sdk.Web` project files produce an ASP.NET Core stack signal.
- `Microsoft.NET.Sdk.Razor` project files produce a Razor stack signal.
- `Microsoft.NET.Sdk.BlazorWebAssembly` project files produce a Blazor WebAssembly stack signal.
- `Microsoft.NET.Sdk.Worker` project files produce a Worker Service stack signal.
- Minimal API source signals are detected conservatively.
- `package-lock.json`, `pnpm-lock.yaml`, `yarn.lock`, or `bun.lockb` produce package manager stack signals.
- `tsconfig.json` produces a TypeScript stack signal.
- `tailwind.config.*` produces a Tailwind CSS stack signal.
- `dotnet build` passes.
- `dotnet test` passes.
- `ackit scan` reports no risk findings.
- `scripts/verify-release.ps1` passes.
- Work is committed.
- No push, publish, tag, remote creation, deletion, overwrite of unrelated files, or automatic redaction occurs.

## Implementation steps
1. Create this task file.
2. Commit task-only changes.
3. Update `StackDetector` to read project/source signals through `IFileSystem`.
4. Update CLI service construction to pass the file system.
5. Add tests for new stack signals.
6. Update docs and handoff.
7. Run `dotnet build AgentContextKit.sln -c Release --no-restore`.
8. Run `dotnet test AgentContextKit.sln -c Release --no-build`.
9. Run `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`.
10. Run `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`.
11. Update completion notes.
12. Commit implementation.
13. Continue with the next v0.2 task.

## Test steps
1. `dotnet build AgentContextKit.sln -c Release --no-restore`
2. `dotnet test AgentContextKit.sln -c Release --no-build`
3. `dotnet run --project src/AgentContextKit.Cli/AgentContextKit.Cli.csproj -c Release --no-build -- scan`
4. `powershell -ExecutionPolicy Bypass -File scripts/verify-release.ps1`

## Risks
- Content-based stack detection can create false positives if signals are too broad.
- Reading too many files could slow scans; keep reads limited to project files and known small source/config files.
- Additional stack entries can affect downstream scripts that expect a fixed stack list.

## Rollback plan
Revert the TASK-0011 implementation commit. Do not run destructive git commands.

## Completion notes
Pending.
