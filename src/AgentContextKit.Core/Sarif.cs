using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgentContextKit.Core;

public sealed class SarifReportWriter : ISarifReportWriter
{
    private const string SchemaUri = "https://json.schemastore.org/sarif-2.1.0.json";
    private readonly IFileSystem _fileSystem;

    public SarifReportWriter(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public GeneratedFileResult Generate(string repositoryPath, string relativeOutputPath, ScanResult scanResult, string toolVersion)
    {
        var outputPath = NormalizeOutputPath(repositoryPath, relativeOutputPath);
        var fullPath = Path.Combine(repositoryPath, outputPath.Replace('/', Path.DirectorySeparatorChar));

        if (_fileSystem.FileExists(fullPath))
        {
            return new GeneratedFileResult(outputPath, GeneratedFileStatus.SkippedExisting, "Existing SARIF report was not overwritten.");
        }

        var report = BuildReport(repositoryPath, scanResult, toolVersion);
        var content = JsonSerializer.Serialize(report, new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        _fileSystem.WriteAllText(fullPath, content + Environment.NewLine);
        return new GeneratedFileResult(outputPath, GeneratedFileStatus.Created, "SARIF report created.");
    }

    public SarifReport BuildReport(string repositoryPath, ScanResult scanResult, string toolVersion)
    {
        return new SarifReport
        {
            Schema = SchemaUri,
            Version = "2.1.0",
            Runs =
            [
                new SarifRun
                {
                    Tool = new SarifTool
                    {
                        Driver = new SarifDriver
                        {
                            Name = "AgentContextKit",
                            InformationUri = "https://github.com/Cynrath/agent-context-kit",
                            Version = toolVersion,
                            Rules = RiskRuleCatalog.All.Select(ToSarifRule).ToArray()
                        }
                    },
                    Results = scanResult.Findings
                        .Select(finding => ToResult(repositoryPath, finding))
                        .ToArray()
                }
            ]
        };
    }

    private static SarifResult ToResult(string repositoryPath, RiskFinding finding)
    {
        var rule = RiskRuleCatalog.GetRule(finding);
        return new SarifResult
        {
            RuleId = rule.Id,
            Level = ToSarifLevel(finding.Severity),
            Message = new SarifMessage
            {
                Text = SanitizeMessage(finding.Message)
            },
            Locations =
            [
                new SarifLocation
                {
                    PhysicalLocation = new SarifPhysicalLocation
                    {
                        ArtifactLocation = new SarifArtifactLocation
                        {
                            Uri = NormalizeArtifactUri(repositoryPath, finding.Path)
                        }
                    }
                }
            ]
        };
    }

    private static string NormalizeOutputPath(string repositoryPath, string relativeOutputPath)
    {
        if (string.IsNullOrWhiteSpace(relativeOutputPath))
        {
            throw new InvalidOperationException("SARIF output path is required.");
        }

        var outputPath = relativeOutputPath.Trim().Replace('\\', '/');
        if (Path.IsPathRooted(outputPath))
        {
            throw new InvalidOperationException("SARIF output path must be repository-relative.");
        }

        var segments = outputPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length == 0 || segments.Any(segment => segment is "." or ".."))
        {
            throw new InvalidOperationException("SARIF output path must stay inside the repository.");
        }

        if (!outputPath.EndsWith(".sarif", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("SARIF output path must end with .sarif.");
        }

        var repositoryFullPath = Path.GetFullPath(repositoryPath);
        var sarifFullPath = Path.GetFullPath(Path.Combine(repositoryFullPath, outputPath.Replace('/', Path.DirectorySeparatorChar)));
        var repositoryPrefix = repositoryFullPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;

        if (!sarifFullPath.StartsWith(repositoryPrefix, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("SARIF output path must stay inside the repository.");
        }

        return outputPath;
    }

    private static string NormalizeArtifactUri(string repositoryPath, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return "unknown";
        }

        var normalized = path.Trim();
        if (Path.IsPathRooted(normalized))
        {
            try
            {
                var repositoryFullPath = Path.GetFullPath(repositoryPath);
                var findingFullPath = Path.GetFullPath(normalized);
                var relative = Path.GetRelativePath(repositoryFullPath, findingFullPath);
                if (!relative.StartsWith("..", StringComparison.Ordinal) &&
                    !Path.IsPathRooted(relative))
                {
                    normalized = relative;
                }
                else
                {
                    normalized = Path.GetFileName(findingFullPath);
                }
            }
            catch (Exception ex) when (ex is ArgumentException or NotSupportedException or PathTooLongException)
            {
                normalized = Path.GetFileName(normalized);
            }
        }

        normalized = normalized.Replace('\\', '/').TrimStart('/');
        if (normalized.Length >= 2 && char.IsLetter(normalized[0]) && normalized[1] == ':')
        {
            normalized = normalized[2..].TrimStart('/');
        }

        var segments = normalized
            .Split('/', StringSplitOptions.RemoveEmptyEntries)
            .Where(segment => segment is not "." and not "..")
            .ToArray();

        return segments.Length == 0
            ? "unknown"
            : string.Join('/', segments);
    }

    private static string ToSarifLevel(RiskSeverity severity)
    {
        return severity switch
        {
            RiskSeverity.Critical or RiskSeverity.High => "error",
            RiskSeverity.Medium => "warning",
            _ => "note"
        };
    }

    private static string SanitizeMessage(string message)
    {
        return string.IsNullOrWhiteSpace(message)
            ? "AgentContextKit finding."
            : message.Trim();
    }

    private static SarifRule ToSarifRule(RiskRule rule)
    {
        return new SarifRule
        {
            Id = rule.Id,
            Name = rule.Name,
            ShortDescription = new SarifMessage
            {
                Text = rule.Description
            },
            FullDescription = new SarifMessage
            {
                Text = $"{rule.Description} Default severity: {rule.DefaultSeverity}."
            },
            Help = new SarifMessage
            {
                Text = rule.Recommendation
            }
        };
    }
}

public sealed class SarifReport
{
    [JsonPropertyName("$schema")]
    public string Schema { get; init; } = "";

    [JsonPropertyName("version")]
    public string Version { get; init; } = "";

    [JsonPropertyName("runs")]
    public IReadOnlyList<SarifRun> Runs { get; init; } = Array.Empty<SarifRun>();
}

public sealed class SarifRun
{
    [JsonPropertyName("tool")]
    public SarifTool Tool { get; init; } = new();

    [JsonPropertyName("results")]
    public IReadOnlyList<SarifResult> Results { get; init; } = Array.Empty<SarifResult>();
}

public sealed class SarifTool
{
    [JsonPropertyName("driver")]
    public SarifDriver Driver { get; init; } = new();
}

public sealed class SarifDriver
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = "";

    [JsonPropertyName("informationUri")]
    public string InformationUri { get; init; } = "";

    [JsonPropertyName("version")]
    public string Version { get; init; } = "";

    [JsonPropertyName("rules")]
    public IReadOnlyList<SarifRule> Rules { get; init; } = Array.Empty<SarifRule>();
}

public sealed class SarifRule
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = "";

    [JsonPropertyName("name")]
    public string Name { get; init; } = "";

    [JsonPropertyName("shortDescription")]
    public SarifMessage ShortDescription { get; init; } = new();

    [JsonPropertyName("fullDescription")]
    public SarifMessage? FullDescription { get; init; }

    [JsonPropertyName("help")]
    public SarifMessage? Help { get; init; }
}

public sealed class SarifResult
{
    [JsonPropertyName("ruleId")]
    public string RuleId { get; init; } = "";

    [JsonPropertyName("level")]
    public string Level { get; init; } = "";

    [JsonPropertyName("message")]
    public SarifMessage Message { get; init; } = new();

    [JsonPropertyName("locations")]
    public IReadOnlyList<SarifLocation> Locations { get; init; } = Array.Empty<SarifLocation>();
}

public sealed class SarifMessage
{
    [JsonPropertyName("text")]
    public string Text { get; init; } = "";
}

public sealed class SarifLocation
{
    [JsonPropertyName("physicalLocation")]
    public SarifPhysicalLocation PhysicalLocation { get; init; } = new();
}

public sealed class SarifPhysicalLocation
{
    [JsonPropertyName("artifactLocation")]
    public SarifArtifactLocation ArtifactLocation { get; init; } = new();

    [JsonPropertyName("region")]
    public SarifRegion? Region { get; init; }
}

public sealed class SarifArtifactLocation
{
    [JsonPropertyName("uri")]
    public string Uri { get; init; } = "";
}

public sealed class SarifRegion
{
    [JsonPropertyName("startLine")]
    public int StartLine { get; init; }
}
