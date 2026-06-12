using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AgentContextKit.Core;

public static class BaselineSchema
{
    public const int CurrentVersion = 1;

    public const string FingerprintAlgorithm = "sha256-rule-path-location-occurrence-v1";
}

public sealed record BaselineLocation
{
    public BaselineLocation(int? startLine = null, int? startColumn = null, int? occurrence = null)
    {
        if (startLine is <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startLine), "Baseline line numbers must be positive.");
        }

        if (startColumn is <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startColumn), "Baseline column numbers must be positive.");
        }

        if (startColumn.HasValue && !startLine.HasValue)
        {
            throw new ArgumentException("A baseline column requires a line number.", nameof(startColumn));
        }

        if (occurrence is <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(occurrence), "Baseline occurrence numbers must be positive.");
        }

        StartLine = startLine;
        StartColumn = startColumn;
        Occurrence = occurrence;
    }

    public int? StartLine { get; }

    public int? StartColumn { get; }

    public int? Occurrence { get; }
}

public sealed record BaselineEntry
{
    public BaselineEntry(
        string ruleId,
        string path,
        RiskSeverity severity,
        BaselineLocation? location = null)
    {
        RuleId = BaselineFingerprint.NormalizeRuleId(ruleId);
        Path = BaselineFingerprint.NormalizeRelativePath(path);
        Severity = severity;
        StartLine = location?.StartLine;
        StartColumn = location?.StartColumn;
        Occurrence = location?.Occurrence;
        Fingerprint = BaselineFingerprint.Create(RuleId, Path, location);
    }

    public string Fingerprint { get; }

    public string RuleId { get; }

    public string Path { get; }

    public RiskSeverity Severity { get; }

    public int? StartLine { get; }

    public int? StartColumn { get; }

    public int? Occurrence { get; }
}

public sealed record BaselineManifest
{
    public BaselineManifest(IEnumerable<BaselineEntry> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        var ordered = entries
            .OrderBy(entry => entry.Path, StringComparer.Ordinal)
            .ThenBy(entry => entry.RuleId, StringComparer.Ordinal)
            .ThenBy(entry => entry.StartLine)
            .ThenBy(entry => entry.StartColumn)
            .ThenBy(entry => entry.Occurrence)
            .ThenBy(entry => entry.Fingerprint, StringComparer.Ordinal)
            .ToArray();

        var duplicate = ordered
            .GroupBy(entry => entry.Fingerprint, StringComparer.Ordinal)
            .FirstOrDefault(group => group.Count() > 1);

        if (duplicate is not null)
        {
            throw new ArgumentException($"Duplicate baseline fingerprint: {duplicate.Key}", nameof(entries));
        }

        SchemaVersion = BaselineSchema.CurrentVersion;
        FingerprintAlgorithm = BaselineSchema.FingerprintAlgorithm;
        Entries = Array.AsReadOnly(ordered);
    }

    public int SchemaVersion { get; }

    public string FingerprintAlgorithm { get; }

    public IReadOnlyList<BaselineEntry> Entries { get; }
}

public static partial class BaselineFingerprint
{
    public static string Create(string ruleId, string relativePath, BaselineLocation? location = null)
    {
        var normalizedRuleId = NormalizeRuleId(ruleId);
        var normalizedPath = NormalizeRelativePath(relativePath);
        var canonical = string.Join('\n',
            BaselineSchema.FingerprintAlgorithm,
            normalizedRuleId,
            normalizedPath,
            location?.StartLine?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty,
            location?.StartColumn?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty,
            location?.Occurrence?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty);
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(canonical));
        return Convert.ToHexStringLower(hash);
    }

    public static string NormalizeRuleId(string ruleId)
    {
        if (string.IsNullOrWhiteSpace(ruleId))
        {
            throw new ArgumentException("Baseline rule ID is required.", nameof(ruleId));
        }

        var normalized = ruleId.Trim().Normalize(NormalizationForm.FormC).ToUpperInvariant();
        if (!RuleIdPattern().IsMatch(normalized))
        {
            throw new ArgumentException("Baseline rule ID contains unsupported characters.", nameof(ruleId));
        }

        return normalized;
    }

    public static string NormalizeRelativePath(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            throw new ArgumentException("Baseline path is required.", nameof(relativePath));
        }

        var normalized = relativePath.Trim().Normalize(NormalizationForm.FormC).Replace('\\', '/');
        if (normalized.StartsWith("/", StringComparison.Ordinal) ||
            normalized.Contains(":", StringComparison.Ordinal) ||
            normalized.Any(char.IsControl))
        {
            throw new ArgumentException("Baseline path must be a safe repository-relative path.", nameof(relativePath));
        }

        var segments = new List<string>();
        foreach (var segment in normalized.Split('/', StringSplitOptions.RemoveEmptyEntries))
        {
            if (segment == ".")
            {
                continue;
            }

            if (segment == "..")
            {
                throw new ArgumentException("Baseline path must not escape the repository.", nameof(relativePath));
            }

            if (segment.Any(char.IsControl))
            {
                throw new ArgumentException("Baseline path contains unsupported characters.", nameof(relativePath));
            }

            segments.Add(segment);
        }

        if (segments.Count == 0)
        {
            throw new ArgumentException("Baseline path is required.", nameof(relativePath));
        }

        return string.Join('/', segments);
    }

    [GeneratedRegex("^[A-Z0-9][A-Z0-9._-]{0,63}$", RegexOptions.CultureInvariant)]
    private static partial Regex RuleIdPattern();
}
