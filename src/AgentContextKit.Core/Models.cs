namespace AgentContextKit.Core;

public enum RiskSeverity
{
    Info = 0,
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public enum RiskCategory
{
    Secret,
    Pii,
    Brand,
    LocalPath,
    ProductionConfig,
    BuildArtifact,
    RepositoryHygiene,
    Documentation,
    Configuration
}

public enum AgentTarget
{
    Codex,
    Claude,
    Cursor,
    Copilot,
    All
}

public enum GeneratedFileStatus
{
    Created,
    SkippedExisting
}

public readonly record struct LanguageCode
{
    private LanguageCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static LanguageCode English => new("en");

    public static LanguageCode Turkish => new("tr");

    public static LanguageCode From(string? value)
    {
        return string.Equals(value?.Trim(), "tr", StringComparison.OrdinalIgnoreCase)
            ? Turkish
            : English;
    }

    public override string ToString()
    {
        return Value;
    }
}

public sealed record StackInfo(string Name, string Signal);

public sealed record RiskFinding(
    RiskSeverity Severity,
    RiskCategory Category,
    string Path,
    string Message,
    string? Match = null);

public sealed record ProjectMap(IReadOnlyList<string> Files, IReadOnlyList<StackInfo> Stacks);

public sealed record ScanResult(
    string RepositoryPath,
    IReadOnlyList<string> Files,
    IReadOnlyList<StackInfo> Stacks,
    IReadOnlyList<RiskFinding> Findings,
    bool HasReadme,
    bool HasLicense,
    bool HasSecurityPolicy,
    bool HasContributing,
    bool HasCodeOfConduct,
    bool HasChangelog,
    bool HasTests,
    bool HasCi,
    bool HasDocker,
    bool HasAgentInstructions);

public sealed record GeneratedFileResult(
    string Path,
    GeneratedFileStatus Status,
    string Message)
{
    public bool Created => Status == GeneratedFileStatus.Created;
}

public sealed record TaskSpec(string Title, LanguageCode Language);

public sealed record RepositoryProfile(string RootPath, IReadOnlyList<string> Files, IReadOnlyList<StackInfo> Stacks);

public sealed record AckitConfig(
    int SchemaVersion,
    LanguageCode DefaultLanguage,
    IReadOnlyList<string> BrandKeywords,
    IReadOnlyList<string> PiiKeywords,
    IReadOnlyList<string> IgnorePaths,
    IReadOnlyList<string> RiskExtensions)
{
    public static AckitConfig Default => new(
        1,
        LanguageCode.English,
        Array.Empty<string>(),
        Array.Empty<string>(),
        [".ackit/cache/"],
        [".bak", ".tmp", ".log", ".sql"]);
}

public sealed record CommandResult(int ExitCode, string Message);

public sealed record DoctorCheck(string Name, RiskSeverity Severity, bool Passed, string Message);

public sealed record DoctorResult(IReadOnlyList<DoctorCheck> Checks);
