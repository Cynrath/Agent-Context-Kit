# Architecture

## Solution Layout
```text
AgentContextKit.sln
src/
  AgentContextKit.Cli/
  AgentContextKit.Core/
tests/
  AgentContextKit.Tests/
```

## Boundaries
- `AgentContextKit.Cli`: argument parsing, console output, exit codes.
- `AgentContextKit.Core`: repository scanning, stack detection, risk scanning, template rendering, task generation, config, and reporting models.
- `AgentContextKit.Tests`: unit tests for Core behavior and selected CLI-safe flows.

## Core Services
- `IRepositoryScanner`
- `IStackDetector`
- `IProjectMapBuilder`
- `IRiskScanner`
- `ISecretScanner`
- `IBrandPiiScanner`
- `IRiskReporter`
- `ITemplateRenderer`
- `ITextProvider`
- `ITaskFileGenerator`
- `IAgentInstructionGenerator`
- `IHtmlReportGenerator`
- `IWebUiGenerator`
- `ILLMProvider`
- `IFileSystem`
- `IClock`
- `IAckitConfigReader`
- `IAckitConfigWriter`

## Design Notes
The CLI must not contain business logic. Core services are designed to be testable with file-system and clock abstractions. MVP templates are embedded in code for reliability, with a future path to external template packs.

`HtmlReportGenerator` creates self-contained local HTML reports from scan results. It encodes repository content, rejects output paths outside the repository, and skips existing files by default.

`WebUiGenerator` creates self-contained local Web UI prototype files from scan results and selected local task/context previews. It uses the same local-only trust boundary as HTML reports: encoded repository text, repository-relative output, no external assets, and no overwrite by default.

`StackDetector` uses repository file paths plus limited local reads of project/source files through `IFileSystem`. This keeps stack detection offline and testable while allowing project SDK signals such as `Microsoft.NET.Sdk.Web`, `Microsoft.NET.Sdk.Razor`, `Microsoft.NET.Sdk.BlazorWebAssembly`, and `Microsoft.NET.Sdk.Worker`.

For main repository stack detection, `StackDetector` ignores sample, docs, generated output, template, and fixture-style paths such as `samples/`, `docs/`, `.ackit/`, `.codex/`, `.cursor/`, `templates/`, `fixtures/`, and test data folders. Those files can still be scanned by `RiskScanner`; the exclusion only prevents sample projects from being reported as the main repository stack. .NET global tool projects are reported as `.NET CLI / .NET Tool` when tool packaging metadata such as `PackAsTool` or `ToolCommandName` is present.

Optional future LLM integration is documented in `docs/LLM_INTEGRATION_ARCHITECTURE.md`. `ILLMProvider` defines the provider-neutral Core boundary, but no provider adapter or live provider call exists today. Future provider implementations should stay behind Core interfaces, use dependency injection, require explicit user consent before export, and keep the CLI limited to argument parsing and output mapping.

## Output Schema
Human-readable CLI output is optimized for short terminal feedback. JSON output is produced by the CLI layer from Core models and includes `schemaVersion` and `toolVersion` metadata for automation.
