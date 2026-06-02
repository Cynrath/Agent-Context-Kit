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

        var language = LanguageCode.English;
        var brandKeywords = new List<string>();
        var piiKeywords = new List<string>();
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
        }

        return new AckitConfig(language, brandKeywords, piiKeywords);
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
        defaultLanguage: {language.Value}
        brandKeywords: []
        piiKeywords: []
        """;

        _fileSystem.WriteAllText(path, content + Environment.NewLine);
        return new GeneratedFileResult(".ackit/config.yml", GeneratedFileStatus.Created, "Config created.");
    }
}
