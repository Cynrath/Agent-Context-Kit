namespace AgentContextKit.Core;

public interface IRepositoryScanner
{
    ScanResult Scan(string repositoryPath, AckitConfig? config = null);
}

public interface IStackDetector
{
    IReadOnlyList<StackInfo> Detect(string repositoryPath, IReadOnlyList<string> relativeFiles);
}

public interface IProjectMapBuilder
{
    ProjectMap Build(ScanResult scanResult);
}

public interface IRiskScanner
{
    IReadOnlyList<RiskFinding> Scan(string repositoryPath, IReadOnlyList<string> relativeFiles, AckitConfig config);
}

public interface ISecretScanner
{
    IReadOnlyList<RiskFinding> ScanText(string relativePath, string content);
}

public interface IBrandPiiScanner
{
    IReadOnlyList<RiskFinding> ScanText(string relativePath, string content, AckitConfig config);
}

public interface IRiskReporter
{
    string Render(IReadOnlyList<RiskFinding> findings);
}

public interface ITemplateRenderer
{
    string Render(string templateId, LanguageCode language, IReadOnlyDictionary<string, string> values);
}

public interface ITextProvider
{
    string Get(string key, LanguageCode language);
}

public interface ITaskFileGenerator
{
    GeneratedFileResult CreateTask(string repositoryPath, TaskSpec spec);
}

public interface IAgentInstructionGenerator
{
    IReadOnlyList<GeneratedFileResult> Generate(string repositoryPath, AgentTarget target, LanguageCode language, ScanResult scanResult);
}

public interface IHtmlReportGenerator
{
    GeneratedFileResult Generate(string repositoryPath, string? relativeOutputPath, LanguageCode language, ScanResult scanResult);
}

public interface IWebUiGenerator
{
    GeneratedFileResult Generate(string repositoryPath, string? relativeOutputPath, LanguageCode language, ScanResult scanResult);
}

public interface IPromptPackGenerator
{
    GeneratedFileResult Generate(string repositoryPath, string? relativeOutputPath, LanguageCode language, ScanResult scanResult);
}

public interface ILLMProvider
{
    string Name { get; }

    Task<LlmProviderResponse> GenerateAsync(LlmProviderRequest request, CancellationToken cancellationToken = default);
}

public interface IFileSystem
{
    bool FileExists(string path);

    bool DirectoryExists(string path);

    void CreateDirectory(string path);

    string ReadAllText(string path);

    void WriteAllText(string path, string content);

    long GetFileLength(string path);

    IEnumerable<string> EnumerateFiles(string rootPath, IReadOnlySet<string> ignoredDirectoryNames);
}

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}

public interface IAckitConfigReader
{
    AckitConfig Read(string repositoryPath);
}

public interface IAckitConfigWriter
{
    GeneratedFileResult WriteDefaultIfMissing(string repositoryPath, LanguageCode language);
}
