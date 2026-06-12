using System.Net;
using System.Text.RegularExpressions;

namespace AgentContextKit.Core;

public enum ConfigDiagnosticSeverity
{
    Info,
    Warning,
    Error
}

public sealed record ConfigDiagnostic(
    string Code,
    ConfigDiagnosticSeverity Severity,
    int Line,
    string? Key,
    string Message);

public sealed record ConfigValidationResult(IReadOnlyList<ConfigDiagnostic> Diagnostics)
{
    public bool HasErrors => Diagnostics.Any(diagnostic => diagnostic.Severity == ConfigDiagnosticSeverity.Error);
}

public static class ConfigDiagnosticCodes
{
    public const string UnknownKey = "ACKITCFG001";
    public const string ObsoleteKey = "ACKITCFG002";
    public const string DuplicateKey = "ACKITCFG003";
    public const string InvalidSchemaVersion = "ACKITCFG004";
    public const string InvalidLanguage = "ACKITCFG005";
    public const string MalformedValue = "ACKITCFG006";
    public const string OrphanListItem = "ACKITCFG007";
    public const string InvalidPath = "ACKITCFG008";
    public const string InvalidDomain = "ACKITCFG009";
    public const string InvalidFindingId = "ACKITCFG010";
    public const string CriticalSuppression = "ACKITCFG011";
    public const string InvalidExtension = "ACKITCFG012";
    public const string BroadIgnorePath = "ACKITCFG013";
    public const string DuplicateListValue = "ACKITCFG014";
}

public sealed partial class AckitConfigValidator : IAckitConfigValidator
{
    private static readonly IReadOnlyDictionary<string, string> SupportedKeys =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["schemaVersion"] = "schemaVersion",
            ["defaultLanguage"] = "defaultLanguage",
            ["brandKeywords"] = "brandKeywords",
            ["piiKeywords"] = "piiKeywords",
            ["ignorePaths"] = "ignorePaths",
            ["riskExtensions"] = "riskExtensions",
            ["safeDomains"] = "safeDomains",
            ["ignoredPaths"] = "ignoredPaths",
            ["ignoredFindingIds"] = "ignoredFindingIds"
        };

    private static readonly IReadOnlyDictionary<string, string> ObsoleteKeys =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["allowedFindingIds"] = "ignoredFindingIds"
        };

    private static readonly IReadOnlySet<string> ListKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "brandKeywords",
        "piiKeywords",
        "ignorePaths",
        "riskExtensions",
        "safeDomains",
        "ignoredPaths",
        "ignoredFindingIds"
    };

    private static readonly IReadOnlySet<string> BroadIgnorePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".",
        "src",
        "tests",
        "docs",
        "samples"
    };

    public ConfigValidationResult Validate(string content)
    {
        ArgumentNullException.ThrowIfNull(content);

        var diagnostics = new List<ConfigDiagnostic>();
        var seenKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var seenValues = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        string? activeListKey = null;
        var lines = content.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');

        for (var index = 0; index < lines.Length; index++)
        {
            var lineNumber = index + 1;
            var trimmed = lines[index].Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith('#'))
            {
                continue;
            }

            if (trimmed.StartsWith("-", StringComparison.Ordinal))
            {
                ValidateListLine(trimmed, lineNumber, activeListKey, seenValues, diagnostics);
                continue;
            }

            var separator = trimmed.IndexOf(':', StringComparison.Ordinal);
            if (separator <= 0)
            {
                activeListKey = null;
                Add(diagnostics, ConfigDiagnosticCodes.MalformedValue, ConfigDiagnosticSeverity.Error, lineNumber, null,
                    "Configuration line must use key: value syntax.");
                continue;
            }

            var rawKey = trimmed[..separator].Trim();
            var rawValue = trimmed[(separator + 1)..].Trim();
            string canonicalKey;

            if (SupportedKeys.TryGetValue(rawKey, out var supportedKey))
            {
                canonicalKey = supportedKey;
            }
            else if (ObsoleteKeys.TryGetValue(rawKey, out var replacementKey))
            {
                canonicalKey = replacementKey;
                Add(diagnostics, ConfigDiagnosticCodes.ObsoleteKey, ConfigDiagnosticSeverity.Warning, lineNumber, rawKey,
                    $"Configuration key is obsolete; use {replacementKey}.");
            }
            else
            {
                activeListKey = null;
                Add(diagnostics, ConfigDiagnosticCodes.UnknownKey, ConfigDiagnosticSeverity.Warning, lineNumber, rawKey,
                    "Unknown configuration key is ignored by the current reader.");
                continue;
            }

            if (!seenKeys.Add(canonicalKey))
            {
                Add(diagnostics, ConfigDiagnosticCodes.DuplicateKey, ConfigDiagnosticSeverity.Error, lineNumber, canonicalKey,
                    "Configuration key is declared more than once.");
            }

            if (ListKeys.Contains(canonicalKey))
            {
                activeListKey = ValidateListDeclaration(canonicalKey, rawValue, lineNumber, seenValues, diagnostics)
                    ? canonicalKey
                    : null;
                continue;
            }

            activeListKey = null;
            ValidateScalar(canonicalKey, rawValue, lineNumber, diagnostics);
        }

        return new ConfigValidationResult(diagnostics
            .OrderBy(diagnostic => diagnostic.Line)
            .ThenBy(diagnostic => diagnostic.Code, StringComparer.Ordinal)
            .ToArray());
    }

    private static void ValidateScalar(
        string key,
        string rawValue,
        int line,
        List<ConfigDiagnostic> diagnostics)
    {
        var value = Unquote(rawValue);
        if (value.Length == 0)
        {
            Add(diagnostics, ConfigDiagnosticCodes.MalformedValue, ConfigDiagnosticSeverity.Error, line, key,
                "Configuration value is required.");
            return;
        }

        if (key == "schemaVersion")
        {
            if (!int.TryParse(value, out var schemaVersion) || schemaVersion != AckitConfig.Default.SchemaVersion)
            {
                Add(diagnostics, ConfigDiagnosticCodes.InvalidSchemaVersion, ConfigDiagnosticSeverity.Error, line, key,
                    $"Supported configuration schema version is {AckitConfig.Default.SchemaVersion}.");
            }

            return;
        }

        if (key == "defaultLanguage" &&
            !string.Equals(value, LanguageCode.English.Value, StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(value, LanguageCode.Turkish.Value, StringComparison.OrdinalIgnoreCase))
        {
            Add(diagnostics, ConfigDiagnosticCodes.InvalidLanguage, ConfigDiagnosticSeverity.Error, line, key,
                "Supported configuration languages are en and tr.");
        }
    }

    private static bool ValidateListDeclaration(
        string key,
        string rawValue,
        int line,
        Dictionary<string, HashSet<string>> seenValues,
        List<ConfigDiagnostic> diagnostics)
    {
        if (rawValue.Length == 0)
        {
            return true;
        }

        if (!rawValue.StartsWith("[", StringComparison.Ordinal) ||
            !rawValue.EndsWith("]", StringComparison.Ordinal))
        {
            Add(diagnostics, ConfigDiagnosticCodes.MalformedValue, ConfigDiagnosticSeverity.Error, line, key,
                "List configuration value must use [] or indented list items.");
            return false;
        }

        var inner = rawValue[1..^1];
        if (inner.Trim().Length == 0)
        {
            return false;
        }

        foreach (var item in inner.Split(',', StringSplitOptions.None))
        {
            ValidateListValue(key, Unquote(item), line, seenValues, diagnostics);
        }

        return false;
    }

    private static void ValidateListLine(
        string trimmed,
        int line,
        string? activeListKey,
        Dictionary<string, HashSet<string>> seenValues,
        List<ConfigDiagnostic> diagnostics)
    {
        if (activeListKey is null)
        {
            Add(diagnostics, ConfigDiagnosticCodes.OrphanListItem, ConfigDiagnosticSeverity.Error, line, null,
                "List item has no supported list key.");
            return;
        }

        if (trimmed.Length < 2 || trimmed[0] != '-' || !char.IsWhiteSpace(trimmed[1]))
        {
            Add(diagnostics, ConfigDiagnosticCodes.MalformedValue, ConfigDiagnosticSeverity.Error, line, activeListKey,
                "List item must start with '- '.");
            return;
        }

        ValidateListValue(activeListKey, Unquote(trimmed[2..]), line, seenValues, diagnostics);
    }

    private static void ValidateListValue(
        string key,
        string value,
        int line,
        Dictionary<string, HashSet<string>> seenValues,
        List<ConfigDiagnostic> diagnostics)
    {
        if (value.Length == 0)
        {
            Add(diagnostics, ConfigDiagnosticCodes.MalformedValue, ConfigDiagnosticSeverity.Error, line, key,
                "List item value is required.");
            return;
        }

        if (!seenValues.TryGetValue(key, out var values))
        {
            values = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            seenValues[key] = values;
        }

        if (!values.Add(value))
        {
            Add(diagnostics, ConfigDiagnosticCodes.DuplicateListValue, ConfigDiagnosticSeverity.Warning, line, key,
                "List value is declared more than once.");
        }

        if (key is "ignorePaths" or "ignoredPaths")
        {
            ValidatePath(key, value, line, diagnostics);
        }
        else if (key == "riskExtensions")
        {
            if (!ExtensionPattern().IsMatch(value))
            {
                Add(diagnostics, ConfigDiagnosticCodes.InvalidExtension, ConfigDiagnosticSeverity.Error, line, key,
                    "Risk extension must contain only a short extension name.");
            }
        }
        else if (key == "safeDomains")
        {
            ValidateDomain(key, value, line, diagnostics);
        }
        else if (key == "ignoredFindingIds")
        {
            ValidateFindingId(key, value, line, diagnostics);
        }
        else if (value.Any(char.IsControl))
        {
            Add(diagnostics, ConfigDiagnosticCodes.MalformedValue, ConfigDiagnosticSeverity.Error, line, key,
                "List item contains unsupported control characters.");
        }
    }

    private static void ValidatePath(
        string key,
        string value,
        int line,
        List<ConfigDiagnostic> diagnostics)
    {
        try
        {
            var normalized = BaselineFingerprint.NormalizeRelativePath(value);
            if (key == "ignorePaths" && BroadIgnorePaths.Contains(normalized.TrimEnd('/')))
            {
                Add(diagnostics, ConfigDiagnosticCodes.BroadIgnorePath, ConfigDiagnosticSeverity.Warning, line, key,
                    "Ignore path is broad and can hide repository content from scanning.");
            }
        }
        catch (ArgumentException)
        {
            Add(diagnostics, ConfigDiagnosticCodes.InvalidPath, ConfigDiagnosticSeverity.Error, line, key,
                "Path must be a safe repository-relative path without traversal.");
        }
    }

    private static void ValidateDomain(
        string key,
        string value,
        int line,
        List<ConfigDiagnostic> diagnostics)
    {
        var domain = value.StartsWith("*.", StringComparison.Ordinal) ? value[2..] : value;
        if (!domain.Contains(".", StringComparison.Ordinal) ||
            domain.Contains("/", StringComparison.Ordinal) ||
            domain.Contains(":", StringComparison.Ordinal) ||
            Uri.CheckHostName(domain) != UriHostNameType.Dns)
        {
            Add(diagnostics, ConfigDiagnosticCodes.InvalidDomain, ConfigDiagnosticSeverity.Error, line, key,
                "Safe domain must be an exact DNS name or a leading-wildcard subdomain rule.");
        }
    }

    private static void ValidateFindingId(
        string key,
        string value,
        int line,
        List<ConfigDiagnostic> diagnostics)
    {
        string normalized;
        try
        {
            normalized = BaselineFingerprint.NormalizeRuleId(value);
        }
        catch (ArgumentException)
        {
            Add(diagnostics, ConfigDiagnosticCodes.InvalidFindingId, ConfigDiagnosticSeverity.Error, line, key,
                "Finding ID must use a supported scanner rule ID format.");
            return;
        }

        var rule = RiskRuleCatalog.All.FirstOrDefault(candidate =>
            string.Equals(candidate.Id, normalized, StringComparison.OrdinalIgnoreCase));
        if (rule is null)
        {
            Add(diagnostics, ConfigDiagnosticCodes.InvalidFindingId, ConfigDiagnosticSeverity.Error, line, key,
                "Finding ID is not present in the scanner rule catalog.");
            return;
        }

        if (rule.DefaultSeverity == RiskSeverity.Critical)
        {
            Add(diagnostics, ConfigDiagnosticCodes.CriticalSuppression, ConfigDiagnosticSeverity.Error, line, key,
                "Critical scanner rules cannot be configured for suppression.");
        }
    }

    private static string Unquote(string value)
    {
        return value.Trim().Trim('"', '\'');
    }

    private static void Add(
        List<ConfigDiagnostic> diagnostics,
        string code,
        ConfigDiagnosticSeverity severity,
        int line,
        string? key,
        string message)
    {
        diagnostics.Add(new ConfigDiagnostic(code, severity, line, key, message));
    }

    [GeneratedRegex("^\\.?[A-Za-z0-9][A-Za-z0-9_-]{0,31}$", RegexOptions.CultureInvariant)]
    private static partial Regex ExtensionPattern();
}
