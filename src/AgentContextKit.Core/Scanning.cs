using System.Text.RegularExpressions;

namespace AgentContextKit.Core;

public sealed class RepositoryScanner : IRepositoryScanner
{
    public static readonly IReadOnlySet<string> IgnoredDirectoryNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".git",
        ".vs",
        ".idea",
        "bin",
        "obj",
        "node_modules",
        "packages",
        "TestResults",
        "coverage"
    };

    private readonly IFileSystem _fileSystem;
    private readonly IStackDetector _stackDetector;
    private readonly IRiskScanner _riskScanner;

    public RepositoryScanner(IFileSystem fileSystem, IStackDetector stackDetector, IRiskScanner riskScanner)
    {
        _fileSystem = fileSystem;
        _stackDetector = stackDetector;
        _riskScanner = riskScanner;
    }

    public ScanResult Scan(string repositoryPath, AckitConfig? config = null)
    {
        var root = Path.GetFullPath(repositoryPath);
        var files = _fileSystem
            .EnumerateFiles(root, IgnoredDirectoryNames)
            .Select(file => ToRelativePath(root, file))
            .OrderBy(file => file, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var stacks = _stackDetector.Detect(root, files);
        var findings = _riskScanner.Scan(root, files, config ?? AckitConfig.Default);

        return new ScanResult(
            root,
            files,
            stacks,
            findings,
            HasAny(files, "README.md", "README.tr.md"),
            HasAny(files, "LICENSE", "LICENSE.md"),
            HasAny(files, "SECURITY.md"),
            HasAny(files, "CONTRIBUTING.md"),
            HasAny(files, "CODE_OF_CONDUCT.md"),
            HasAny(files, "CHANGELOG.md"),
            files.Any(file => file.StartsWith("tests/", StringComparison.OrdinalIgnoreCase) || file.Contains(".Tests/", StringComparison.OrdinalIgnoreCase)),
            files.Any(file => file.StartsWith(".github/workflows/", StringComparison.OrdinalIgnoreCase)),
            files.Any(file => string.Equals(file, "Dockerfile", StringComparison.OrdinalIgnoreCase) || file.Contains("docker-compose", StringComparison.OrdinalIgnoreCase)),
            HasAny(files, "AGENTS.md", "CLAUDE.md", ".github/copilot-instructions.md") ||
            files.Any(file => file.StartsWith(".cursor/rules/", StringComparison.OrdinalIgnoreCase)));
    }

    internal static string ToRelativePath(string root, string path)
    {
        return Path.GetRelativePath(root, path).Replace('\\', '/');
    }

    private static bool HasAny(IReadOnlyList<string> files, params string[] names)
    {
        return files.Any(file => names.Any(name => string.Equals(file, name, StringComparison.OrdinalIgnoreCase)));
    }
}

public sealed class StackDetector : IStackDetector
{
    public IReadOnlyList<StackInfo> Detect(string repositoryPath, IReadOnlyList<string> relativeFiles)
    {
        var files = relativeFiles.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var stacks = new List<StackInfo>();

        if (files.Any(file => file.EndsWith(".sln", StringComparison.OrdinalIgnoreCase) ||
                              file.EndsWith(".slnx", StringComparison.OrdinalIgnoreCase) ||
                              file.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)) ||
            files.Contains("Program.cs"))
        {
            stacks.Add(new StackInfo(".NET", ".sln/.slnx/.csproj/Program.cs"));
        }

        if (files.Contains("appsettings.json") ||
            files.Any(file => file.StartsWith("Controllers/", StringComparison.OrdinalIgnoreCase)) ||
            files.Any(file => file.StartsWith("Views/", StringComparison.OrdinalIgnoreCase)))
        {
            stacks.Add(new StackInfo("ASP.NET Core MVC", "appsettings.json/Controllers/Views"));
        }

        if (files.Contains("package.json"))
        {
            stacks.Add(new StackInfo("Node", "package.json"));
        }

        if (files.Any(file => file.StartsWith("vite.config.", StringComparison.OrdinalIgnoreCase)))
        {
            stacks.Add(new StackInfo("Vite", "vite.config.*"));
        }

        if (files.Any(file => file.StartsWith("next.config.", StringComparison.OrdinalIgnoreCase)))
        {
            stacks.Add(new StackInfo("Next.js", "next.config.*"));
        }

        if (files.Any(file => file.StartsWith("nuxt.config.", StringComparison.OrdinalIgnoreCase)))
        {
            stacks.Add(new StackInfo("Nuxt", "nuxt.config.*"));
        }

        if (files.Contains("angular.json"))
        {
            stacks.Add(new StackInfo("Angular", "angular.json"));
        }

        if (files.Contains("requirements.txt") || files.Contains("pyproject.toml"))
        {
            stacks.Add(new StackInfo("Python", "requirements.txt/pyproject.toml"));
        }

        if (files.Contains("composer.json") || files.Contains("artisan"))
        {
            stacks.Add(new StackInfo("PHP/Laravel", "composer.json/artisan"));
        }

        if (files.Contains("Dockerfile") || files.Any(file => file.Contains("docker-compose", StringComparison.OrdinalIgnoreCase)))
        {
            stacks.Add(new StackInfo("Docker", "Dockerfile/docker-compose"));
        }

        if (files.Any(file => file.StartsWith(".github/workflows/", StringComparison.OrdinalIgnoreCase)))
        {
            stacks.Add(new StackInfo("GitHub Actions", ".github/workflows"));
        }

        if (files.Any(file => file.EndsWith(".sql", StringComparison.OrdinalIgnoreCase)) ||
            files.Any(file => file.Contains("/Migrations/", StringComparison.OrdinalIgnoreCase) || file.StartsWith("Migrations/", StringComparison.OrdinalIgnoreCase)))
        {
            stacks.Add(new StackInfo("Database/Migrations", "*.sql/Migrations"));
        }

        return stacks;
    }
}

public sealed class ProjectMapBuilder : IProjectMapBuilder
{
    public ProjectMap Build(ScanResult scanResult)
    {
        return new ProjectMap(scanResult.Files, scanResult.Stacks);
    }
}

public sealed class RiskScanner : IRiskScanner
{
    private const long MaxTextFileBytes = 1_000_000;
    private readonly IFileSystem _fileSystem;
    private readonly ISecretScanner _secretScanner;
    private readonly IBrandPiiScanner _brandPiiScanner;

    public RiskScanner(IFileSystem fileSystem, ISecretScanner secretScanner, IBrandPiiScanner brandPiiScanner)
    {
        _fileSystem = fileSystem;
        _secretScanner = secretScanner;
        _brandPiiScanner = brandPiiScanner;
    }

    public IReadOnlyList<RiskFinding> Scan(string repositoryPath, IReadOnlyList<string> relativeFiles, AckitConfig config)
    {
        var findings = new List<RiskFinding>();

        foreach (var relativeFile in relativeFiles)
        {
            findings.AddRange(AnalyzePath(relativeFile));

            var fullPath = Path.Combine(repositoryPath, relativeFile.Replace('/', Path.DirectorySeparatorChar));
            if (!IsProbablyTextFile(relativeFile) || !_fileSystem.FileExists(fullPath) || _fileSystem.GetFileLength(fullPath) > MaxTextFileBytes)
            {
                continue;
            }

            string content;
            try
            {
                content = _fileSystem.ReadAllText(fullPath);
            }
            catch (IOException)
            {
                continue;
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }

            findings.AddRange(_secretScanner.ScanText(relativeFile, content));
            findings.AddRange(_brandPiiScanner.ScanText(relativeFile, content, config));
        }

        return findings
            .OrderByDescending(finding => finding.Severity)
            .ThenBy(finding => finding.Path, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static IEnumerable<RiskFinding> AnalyzePath(string relativeFile)
    {
        var fileName = Path.GetFileName(relativeFile);
        var lower = relativeFile.ToLowerInvariant();

        if (fileName.Equals(".env", StringComparison.OrdinalIgnoreCase) ||
            fileName.StartsWith(".env.", StringComparison.OrdinalIgnoreCase) ||
            fileName.Equals("secrets.json", StringComparison.OrdinalIgnoreCase))
        {
            yield return new RiskFinding(RiskSeverity.Critical, RiskCategory.Secret, relativeFile, "Secret-like configuration file is present.");
        }

        if (fileName.Equals("appsettings.Production.json", StringComparison.OrdinalIgnoreCase) ||
            fileName.Equals("appsettings.Staging.json", StringComparison.OrdinalIgnoreCase) ||
            fileName.Equals("appsettings.Local.json", StringComparison.OrdinalIgnoreCase))
        {
            yield return new RiskFinding(RiskSeverity.High, RiskCategory.ProductionConfig, relativeFile, "Environment-specific appsettings file should be reviewed before public release.");
        }

        if (lower.Contains("/uploads/") || lower.StartsWith("uploads/") ||
            lower.Contains("/backup/") || lower.StartsWith("backup/") ||
            lower.Contains("/backups/") || lower.StartsWith("backups/") ||
            lower.Contains("dataprotection-keys"))
        {
            yield return new RiskFinding(RiskSeverity.High, RiskCategory.RepositoryHygiene, relativeFile, "Risky data/storage path is present.");
        }

        if (fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".rar", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".7z", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".bak", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".tmp", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".log", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".mdf", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".ldf", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".db", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".sqlite", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".sqlite3", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".bacpac", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".dacpac", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
        {
            yield return new RiskFinding(RiskSeverity.Medium, RiskCategory.BuildArtifact, relativeFile, "File extension should be reviewed before public release.");
        }
    }

    private static bool IsProbablyTextFile(string relativeFile)
    {
        var fileName = Path.GetFileName(relativeFile);
        var extension = Path.GetExtension(relativeFile);

        if (fileName.Equals(".gitignore", StringComparison.OrdinalIgnoreCase) ||
            fileName.Equals(".editorconfig", StringComparison.OrdinalIgnoreCase) ||
            fileName.EndsWith(".mdc", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return extension.Equals(".md", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".txt", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".cs", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".csproj", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".sln", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".slnx", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".json", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".yml", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".yaml", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".xml", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".props", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".targets", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".config", StringComparison.OrdinalIgnoreCase);
    }
}

public sealed class SecretScanner : ISecretScanner
{
    private sealed record SecretPattern(Regex Regex, RiskSeverity Severity, RiskCategory Category, string Message);

    private const string EqualsSign = "=";
    private const string FileUriPrefix = "file:" + "///";

    private static readonly SecretPattern[] Patterns =
    [
        new(new Regex(@"sk-proj-[A-Za-z0-9_-]{8,}", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.Critical, RiskCategory.Secret, "OpenAI project key-like value detected."),
        new(new Regex(@"\bsk-[A-Za-z0-9]{16,}", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.Critical, RiskCategory.Secret, "API key-like value detected."),
        new(new Regex(@"github_pat_[A-Za-z0-9_]{16,}", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.Critical, RiskCategory.Secret, "GitHub fine-grained token-like value detected."),
        new(new Regex(@"\bghp_[A-Za-z0-9]{16,}", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.Critical, RiskCategory.Secret, "GitHub token-like value detected."),
        new(new Regex(@"BEGIN (RSA|OPENSSH) PRIVATE KEY", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.Critical, RiskCategory.Secret, "Private key block detected."),
        new(new Regex(@"aws_secret_access_key\s*[:=]", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.Critical, RiskCategory.Secret, "AWS secret access key setting detected."),
        new(new Regex(@"\b(password|pwd|mysql password|sql password)\b\s*[:=]", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.High, RiskCategory.Secret, "Password-like assignment detected."),
        new(new Regex(@"\b(api[_ -]?key|apikey|api_key|token|bearer)\b\s*[:=]", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.High, RiskCategory.Secret, "Token or API key assignment detected."),
        new(new Regex(@"\b(connectionstring|connection string)\b\s*[:=]|\b(data source|user id|uid)" + EqualsSign, RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.High, RiskCategory.Secret, "Connection string-like value detected."),
        new(new Regex(@"\b(smtp|recaptcha|cloudflare)\b\s*[:=]", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.Medium, RiskCategory.Secret, "Service credential-like setting detected."),
        new(new Regex(@"([A-Za-z]:\\|" + FileUriPrefix + ")", RegexOptions.IgnoreCase | RegexOptions.Compiled), RiskSeverity.Low, RiskCategory.LocalPath, "Local filesystem path detected.")
    ];

    public IReadOnlyList<RiskFinding> ScanText(string relativePath, string content)
    {
        var findings = new List<RiskFinding>();

        foreach (var pattern in Patterns)
        {
            var match = pattern.Regex.Match(content);
            if (match.Success)
            {
                findings.Add(new RiskFinding(pattern.Severity, pattern.Category, relativePath, pattern.Message, Truncate(match.Value)));
            }
        }

        return findings;
    }

    private static string Truncate(string value)
    {
        return value.Length <= 48 ? value : value[..48] + "...";
    }
}

public sealed class BrandPiiScanner : IBrandPiiScanner
{
    private static readonly Regex PhoneRegex = new(@"(?<![A-Za-z0-9-])(\+?\d[\d\s()-]{7,}\d)(?![A-Za-z0-9])", RegexOptions.Compiled);

    public IReadOnlyList<RiskFinding> ScanText(string relativePath, string content, AckitConfig config)
    {
        var findings = new List<RiskFinding>();

        foreach (var keyword in config.BrandKeywords.Where(keyword => !string.IsNullOrWhiteSpace(keyword)))
        {
            if (content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                findings.Add(new RiskFinding(RiskSeverity.Medium, RiskCategory.Brand, relativePath, "Configured brand keyword detected.", keyword));
            }
        }

        foreach (var keyword in config.PiiKeywords.Where(keyword => !string.IsNullOrWhiteSpace(keyword)))
        {
            if (content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                findings.Add(new RiskFinding(RiskSeverity.High, RiskCategory.Pii, relativePath, "Configured PII keyword detected.", keyword));
            }
        }

        var phoneMatch = PhoneRegex.Match(content);
        if (phoneMatch.Success && phoneMatch.Value.Count(char.IsDigit) >= 8)
        {
            findings.Add(new RiskFinding(RiskSeverity.Low, RiskCategory.Pii, relativePath, "Phone-like value detected.", phoneMatch.Value));
        }

        return findings;
    }
}

public sealed class RiskReporter : IRiskReporter
{
    public string Render(IReadOnlyList<RiskFinding> findings)
    {
        if (findings.Count == 0)
        {
            return "No risk findings.";
        }

        var lines = new List<string>();
        foreach (var severity in new[] { RiskSeverity.Critical, RiskSeverity.High, RiskSeverity.Medium, RiskSeverity.Low, RiskSeverity.Info })
        {
            var group = findings.Where(finding => finding.Severity == severity).ToArray();
            if (group.Length == 0)
            {
                continue;
            }

            lines.Add($"{severity}:");
            lines.AddRange(group.Select(finding => $"- {finding.Path}: {finding.Message}"));
        }

        return string.Join(Environment.NewLine, lines);
    }
}
