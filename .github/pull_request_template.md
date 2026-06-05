# Summary
- 

# Related Task Or Issue
- 

# Change Type
- [ ] Bug fix
- [ ] Feature
- [ ] Documentation
- [ ] CI or release workflow
- [ ] Security hardening
- [ ] Refactor or maintenance

# Safety And Security Impact
- 

# Localization Impact
- [ ] No localization impact
- [ ] English user-facing text changed
- [ ] Turkish user-facing text changed
- [ ] JSON/schema output changed

# Generated Files Impact
- [ ] No generated files changed
- [ ] Agent instruction templates changed
- [ ] Report/Web UI/prompt-pack/context-export output changed
- [ ] Generated output was local-only and not committed

# Tests Run
```powershell

```

# Checklist
- [ ] `dotnet build AgentContextKit.sln -c Release --no-restore`
- [ ] `dotnet test AgentContextKit.sln -c Release --no-build`
- [ ] `dotnet run --project src/AgentContextKit.Cli -- scan --ci`
- [ ] `dotnet run --project src/AgentContextKit.Cli -- doctor`
- [ ] Docs updated where needed
- [ ] No secrets, private config, or private repository content committed
- [ ] No archives, packages, `bin/`, `obj/`, reports, or generated junk committed
- [ ] No push, tag, GitHub Release, NuGet publish, or remote side effect included
