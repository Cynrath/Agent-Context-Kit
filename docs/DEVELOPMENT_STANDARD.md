# Development Standard

## Working Model
- Start every change from a task document.
- Keep scope small and explicit.
- Prefer safe defaults over surprise behavior.
- Update docs when behavior changes.
- Record assumptions in `docs/DECISIONS.md` or the active task.

## Code
- Use C# with nullable reference types enabled.
- Keep public names in English.
- Keep Core independent from console output.
- Avoid unnecessary dependencies.
- Add abstractions only when they improve testability or reduce real coupling.

## Security
- Do not expose stack traces in public CLI output.
- Provide cause and recommended action for errors.
- Do not send code or findings to remote services in the MVP.
- Report, do not mutate, risky content.

## Verification
Run:
```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release
```
