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
- `IFileSystem`
- `IClock`
- `IAckitConfigReader`
- `IAckitConfigWriter`

## Design Notes
The CLI must not contain business logic. Core services are designed to be testable with file-system and clock abstractions. MVP templates are embedded in code for reliability, with a future path to external template packs.
