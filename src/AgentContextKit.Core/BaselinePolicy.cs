using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgentContextKit.Core;

public enum BaselineFileStatus
{
    Created,
    Updated
}

public sealed record BaselineFileResult(string Path, BaselineFileStatus Status, int EntryCount);

public enum BaselineFindingStatus
{
    Existing,
    New
}

public sealed record BaselineFindingEvaluation(
    RiskFinding Finding,
    string Fingerprint,
    BaselineFindingStatus Status,
    int Occurrence);

public sealed record BaselineEvaluation(int BaselineEntryCount, IReadOnlyList<BaselineFindingEvaluation> Findings)
{
    public IReadOnlyList<BaselineFindingEvaluation> Existing =>
        Findings.Where(finding => finding.Status == BaselineFindingStatus.Existing).ToArray();

    public IReadOnlyList<BaselineFindingEvaluation> New =>
        Findings.Where(finding => finding.Status == BaselineFindingStatus.New).ToArray();

    public void ValidateAgainst(IReadOnlyList<RiskFinding> findings)
    {
        ArgumentNullException.ThrowIfNull(findings);
        if (Findings.Count != findings.Count)
        {
            throw new InvalidOperationException("Baseline classification does not match the scan findings.");
        }

        for (var index = 0; index < findings.Count; index++)
        {
            if (!Equals(Findings[index].Finding, findings[index]))
            {
                throw new InvalidOperationException("Baseline classification does not match the scan findings.");
            }
        }
    }
}

public sealed class BaselineException : Exception
{
    public BaselineException(string code, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
    }

    public string Code { get; }
}

public sealed class BaselineStore : IBaselineStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly IFileSystem _fileSystem;

    public BaselineStore(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public BaselineFileResult Write(string repositoryPath, string relativePath, BaselineManifest manifest, bool update)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        var (normalizedPath, fullPath) = ResolvePath(repositoryPath, relativePath);
        var exists = _fileSystem.FileExists(fullPath);
        if (exists && !update)
        {
            throw new BaselineException("ACKITBASE003", "Baseline file already exists. Use --update to replace it explicitly.");
        }

        var document = new BaselineDocument(
            manifest.SchemaVersion,
            manifest.FingerprintAlgorithm,
            manifest.Entries.Select(entry => new BaselineEntryDocument(
                entry.Fingerprint,
                entry.RuleId,
                entry.Path,
                entry.Severity,
                entry.StartLine,
                entry.StartColumn,
                entry.Occurrence)).ToArray());
        var content = JsonSerializer.Serialize(document, JsonOptions) + Environment.NewLine;
        try
        {
            _fileSystem.WriteAllText(fullPath, content);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new BaselineException("ACKITBASE007", "Baseline file could not be written.", ex);
        }

        return new BaselineFileResult(
            normalizedPath,
            exists ? BaselineFileStatus.Updated : BaselineFileStatus.Created,
            manifest.Entries.Count);
    }

    public BaselineManifest Load(string repositoryPath, string relativePath)
    {
        var (_, fullPath) = ResolvePath(repositoryPath, relativePath);
        if (!_fileSystem.FileExists(fullPath))
        {
            throw new BaselineException("ACKITBASE002", "Baseline file was not found.");
        }

        BaselineDocument? document;
        try
        {
            var content = _fileSystem.ReadAllText(fullPath);
            document = JsonSerializer.Deserialize<BaselineDocument>(content, JsonOptions);
        }
        catch (JsonException ex)
        {
            throw new BaselineException("ACKITBASE004", "Baseline file is not valid JSON.", ex);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new BaselineException("ACKITBASE007", "Baseline file could not be read.", ex);
        }

        if (document is null)
        {
            throw new BaselineException("ACKITBASE004", "Baseline file is empty or invalid.");
        }

        if (document.SchemaVersion != BaselineSchema.CurrentVersion ||
            !string.Equals(document.FingerprintAlgorithm, BaselineSchema.FingerprintAlgorithm, StringComparison.Ordinal))
        {
            throw new BaselineException("ACKITBASE005", "Baseline schema or fingerprint algorithm is not supported.");
        }

        try
        {
            var entries = (document.Entries ?? Array.Empty<BaselineEntryDocument>())
                .Select(ToValidatedEntry)
                .ToArray();
            return new BaselineManifest(entries);
        }
        catch (BaselineException)
        {
            throw;
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            throw new BaselineException("ACKITBASE006", "Baseline entries failed integrity validation.", ex);
        }
    }

    private static BaselineEntry ToValidatedEntry(BaselineEntryDocument document)
    {
        if (!Enum.IsDefined(document.Severity))
        {
            throw new BaselineException("ACKITBASE006", "Baseline entry severity is invalid.");
        }

        var location = document.StartLine.HasValue || document.StartColumn.HasValue || document.Occurrence.HasValue
            ? new BaselineLocation(document.StartLine, document.StartColumn, document.Occurrence)
            : null;
        var entry = new BaselineEntry(document.RuleId ?? string.Empty, document.Path ?? string.Empty, document.Severity, location);
        if (!string.Equals(entry.Fingerprint, document.Fingerprint, StringComparison.Ordinal))
        {
            throw new BaselineException("ACKITBASE006", "Baseline entry fingerprint does not match its metadata.");
        }

        return entry;
    }

    private static (string RelativePath, string FullPath) ResolvePath(string repositoryPath, string relativePath)
    {
        string normalizedPath;
        try
        {
            normalizedPath = BaselineFingerprint.NormalizeRelativePath(relativePath);
        }
        catch (ArgumentException ex)
        {
            throw new BaselineException("ACKITBASE001", "Baseline path must be a safe repository-relative JSON path.", ex);
        }

        if (!normalizedPath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
        {
            throw new BaselineException("ACKITBASE001", "Baseline path must use the .json extension.");
        }

        var root = Path.GetFullPath(repositoryPath);
        var fullPath = Path.GetFullPath(Path.Combine(root, normalizedPath.Replace('/', Path.DirectorySeparatorChar)));
        var relativeCheck = Path.GetRelativePath(root, fullPath);
        if (relativeCheck == ".." || relativeCheck.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal))
        {
            throw new BaselineException("ACKITBASE001", "Baseline path must remain inside the repository.");
        }

        return (normalizedPath, fullPath);
    }

    private sealed record BaselineDocument(
        int SchemaVersion,
        string? FingerprintAlgorithm,
        IReadOnlyList<BaselineEntryDocument>? Entries);

    private sealed record BaselineEntryDocument(
        string? Fingerprint,
        string? RuleId,
        string? Path,
        RiskSeverity Severity,
        int? StartLine,
        int? StartColumn,
        int? Occurrence);
}

public sealed class BaselineClassifier : IBaselineClassifier
{
    public BaselineManifest CreateManifest(IReadOnlyList<RiskFinding> findings)
    {
        return new BaselineManifest(CreateEvaluations(findings, null).Select(ToEntry));
    }

    public BaselineEvaluation Classify(IReadOnlyList<RiskFinding> findings, BaselineManifest manifest)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        var baselineEntries = manifest.Entries.ToDictionary(entry => entry.Fingerprint, StringComparer.Ordinal);
        return new BaselineEvaluation(manifest.Entries.Count, CreateEvaluations(findings, baselineEntries));
    }

    private static IReadOnlyList<BaselineFindingEvaluation> CreateEvaluations(
        IReadOnlyList<RiskFinding> findings,
        IReadOnlyDictionary<string, BaselineEntry>? baselineEntries)
    {
        ArgumentNullException.ThrowIfNull(findings);
        var indexed = findings.Select((finding, index) => new IndexedFinding(finding, index)).ToArray();
        var occurrences = new int[indexed.Length];

        foreach (var group in indexed.GroupBy(item => new FindingGroupKey(
                     RiskRuleCatalog.GetRuleId(item.Finding),
                     BaselineFingerprint.NormalizeRelativePath(item.Finding.Path))))
        {
            var ordered = group
                .OrderBy(item => item.Index)
                .ToArray();
            for (var index = 0; index < ordered.Length; index++)
            {
                occurrences[ordered[index].Index] = index + 1;
            }
        }

        return indexed.Select(item =>
        {
            var occurrence = occurrences[item.Index];
            var fingerprint = BaselineFingerprint.Create(
                RiskRuleCatalog.GetRuleId(item.Finding),
                item.Finding.Path,
                new BaselineLocation(occurrence: occurrence));
            var status = baselineEntries?.TryGetValue(fingerprint, out var baselineEntry) == true &&
                         item.Finding.Severity <= baselineEntry.Severity
                ? BaselineFindingStatus.Existing
                : BaselineFindingStatus.New;
            return new BaselineFindingEvaluation(item.Finding, fingerprint, status, occurrence);
        }).ToArray();
    }

    private static BaselineEntry ToEntry(BaselineFindingEvaluation evaluation)
    {
        return new BaselineEntry(
            RiskRuleCatalog.GetRuleId(evaluation.Finding),
            evaluation.Finding.Path,
            evaluation.Finding.Severity,
            new BaselineLocation(occurrence: evaluation.Occurrence));
    }

    private sealed record IndexedFinding(RiskFinding Finding, int Index);

    private sealed record FindingGroupKey(string RuleId, string Path);
}
