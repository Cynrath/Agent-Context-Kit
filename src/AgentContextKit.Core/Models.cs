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
        [".ackit/cache/", ".ackit/reports/", ".ackit/webui/", ".ackit/prompt-packs/"],
        [".bak", ".tmp", ".log", ".sql"]);
}

public enum LlmMessageRole
{
    System,
    User,
    Assistant,
    Tool
}

public sealed record LlmMessage(LlmMessageRole Role, string Content);

public sealed record LlmTokenUsage(int? InputTokens, int? OutputTokens, int? TotalTokens);

public sealed record LlmProviderRequest
{
    public LlmProviderRequest(
        string model,
        IReadOnlyList<LlmMessage> messages,
        bool dryRun = true,
        string? exportReviewId = null,
        string? auditCorrelationId = null,
        IReadOnlyDictionary<string, string>? metadata = null)
    {
        Model = model;
        Messages = messages.ToArray();
        DryRun = dryRun;
        ExportReviewId = exportReviewId;
        AuditCorrelationId = auditCorrelationId;
        Metadata = metadata is null
            ? new Dictionary<string, string>()
            : new Dictionary<string, string>(metadata);
    }

    public string Model { get; init; }

    public IReadOnlyList<LlmMessage> Messages { get; init; }

    public bool DryRun { get; init; }

    public string? ExportReviewId { get; init; }

    public string? AuditCorrelationId { get; init; }

    public IReadOnlyDictionary<string, string> Metadata { get; init; }
}

public sealed record LlmProviderResponse
{
    public LlmProviderResponse(
        string outputText,
        string providerName,
        string model,
        string? requestId = null,
        LlmTokenUsage? tokenUsage = null,
        IReadOnlyList<string>? warnings = null)
    {
        OutputText = outputText;
        ProviderName = providerName;
        Model = model;
        RequestId = requestId;
        TokenUsage = tokenUsage;
        Warnings = warnings?.ToArray() ?? Array.Empty<string>();
    }

    public string OutputText { get; init; }

    public string ProviderName { get; init; }

    public string Model { get; init; }

    public string? RequestId { get; init; }

    public LlmTokenUsage? TokenUsage { get; init; }

    public IReadOnlyList<string> Warnings { get; init; }
}

public sealed record CommandResult(int ExitCode, string Message);

public sealed record DoctorCheck(string Name, RiskSeverity Severity, bool Passed, string Message);

public sealed record DoctorResult(IReadOnlyList<DoctorCheck> Checks);
