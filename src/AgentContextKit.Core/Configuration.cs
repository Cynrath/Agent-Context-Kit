namespace AgentContextKit.Core;

public sealed class AckitConfigReader : IAckitConfigReader
{
    private readonly IFileSystem _fileSystem;

    public AckitConfigReader(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public AckitConfig Read(string repositoryPath)
    {
        var path = Path.Combine(repositoryPath, ".ackit", "config.yml");
        if (!_fileSystem.FileExists(path))
        {
            return AckitConfig.Default;
        }

        var schemaVersion = 1;
        var language = LanguageCode.English;
        var brandKeywords = new List<string>();
        var piiKeywords = new List<string>();
        var ignorePaths = new List<string>();
        var riskExtensions = new List<string>();
        var section = "";

        foreach (var rawLine in _fileSystem.ReadAllText(path).Split('\n'))
        {
            var line = rawLine.Trim();
            if (line.Length == 0 || line.StartsWith('#'))
            {
                continue;
            }

            if (line.StartsWith("defaultLanguage:", StringComparison.OrdinalIgnoreCase))
            {
                language = LanguageCode.From(line["defaultLanguage:".Length..].Trim());
                section = "";
                continue;
            }

            if (line.StartsWith("schemaVersion:", StringComparison.OrdinalIgnoreCase))
            {
                var rawValue = line["schemaVersion:".Length..].Trim();
                if (int.TryParse(rawValue, out var parsedSchemaVersion) && parsedSchemaVersion > 0)
                {
                    schemaVersion = parsedSchemaVersion;
                }

                section = "";
                continue;
            }

            if (line.StartsWith("brandKeywords:", StringComparison.OrdinalIgnoreCase))
            {
                section = "brand";
                AddInlineList(brandKeywords, line);
                continue;
            }

            if (line.StartsWith("piiKeywords:", StringComparison.OrdinalIgnoreCase))
            {
                section = "pii";
                AddInlineList(piiKeywords, line);
                continue;
            }

            if (line.StartsWith("ignorePaths:", StringComparison.OrdinalIgnoreCase))
            {
                section = "ignore";
                AddInlineList(ignorePaths, line);
                continue;
            }

            if (line.StartsWith("riskExtensions:", StringComparison.OrdinalIgnoreCase))
            {
                section = "extension";
                AddInlineList(riskExtensions, line);
                continue;
            }

            if (!line.StartsWith("- ", StringComparison.Ordinal))
            {
                continue;
            }

            var value = line[2..].Trim().Trim('"');
            if (value.Length == 0)
            {
                continue;
            }

            if (section == "brand")
            {
                brandKeywords.Add(value);
            }
            else if (section == "pii")
            {
                piiKeywords.Add(value);
            }
            else if (section == "ignore")
            {
                ignorePaths.Add(value);
            }
            else if (section == "extension")
            {
                riskExtensions.Add(NormalizeExtension(value));
            }
        }

        return new AckitConfig(
            schemaVersion,
            language,
            brandKeywords,
            piiKeywords,
            ignorePaths.Count == 0 ? AckitConfig.Default.IgnorePaths : NormalizeIgnorePaths(ignorePaths),
            riskExtensions.Count == 0 ? AckitConfig.Default.RiskExtensions : NormalizeExtensions(riskExtensions));
    }

    private static void AddInlineList(List<string> target, string line)
    {
        var start = line.IndexOf('[', StringComparison.Ordinal);
        var end = line.LastIndexOf(']');
        if (start < 0 || end <= start)
        {
            return;
        }

        foreach (var item in line[(start + 1)..end].Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            var value = item.Trim().Trim('"');
            if (value.Length > 0)
            {
                target.Add(value);
            }
        }
    }

    private static IReadOnlyList<string> NormalizeIgnorePaths(IReadOnlyList<string> paths)
    {
        return paths
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Select(path => path.Trim().Trim('"').Replace('\\', '/'))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static IReadOnlyList<string> NormalizeExtensions(IReadOnlyList<string> extensions)
    {
        return extensions
            .Where(extension => !string.IsNullOrWhiteSpace(extension))
            .Select(NormalizeExtension)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static string NormalizeExtension(string extension)
    {
        var value = extension.Trim().Trim('"');
        return value.StartsWith(".", StringComparison.Ordinal) ? value : "." + value;
    }
}

public sealed class AckitConfigWriter : IAckitConfigWriter
{
    private readonly IFileSystem _fileSystem;

    public AckitConfigWriter(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public GeneratedFileResult WriteDefaultIfMissing(string repositoryPath, LanguageCode language)
    {
        var path = Path.Combine(repositoryPath, ".ackit", "config.yml");
        if (_fileSystem.FileExists(path))
        {
            return new GeneratedFileResult(".ackit/config.yml", GeneratedFileStatus.SkippedExisting, "Existing config was not overwritten.");
        }

        var content = $"""
        schemaVersion: 1
        defaultLanguage: {language.Value}
        brandKeywords: []
        piiKeywords: []
        ignorePaths:
          - .ackit/cache/
          - .ackit/reports/
          - .ackit/webui/
          - .ackit/prompt-packs/
          - .ackit/context-exports/
        riskExtensions:
          - .bak
          - .tmp
          - .log
          - .sql
        """;

        _fileSystem.WriteAllText(path, content + Environment.NewLine);
        return new GeneratedFileResult(".ackit/config.yml", GeneratedFileStatus.Created, "Config created.");
    }
}
