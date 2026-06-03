using System.Globalization;
using System.Net;
using System.Text;

namespace AgentContextKit.Core;

public sealed class AgentInstructionGenerator : IAgentInstructionGenerator
{
    private readonly IFileSystem _fileSystem;
    private readonly ITemplateRenderer _templateRenderer;
    private readonly IClock _clock;

    public AgentInstructionGenerator(IFileSystem fileSystem, ITemplateRenderer templateRenderer, IClock clock)
    {
        _fileSystem = fileSystem;
        _templateRenderer = templateRenderer;
        _clock = clock;
    }

    public IReadOnlyList<GeneratedFileResult> Generate(string repositoryPath, AgentTarget target, LanguageCode language, ScanResult scanResult)
    {
        var values = BuildValues(repositoryPath, scanResult);
        var outputs = BuildOutputs(target);
        var results = new List<GeneratedFileResult>();

        foreach (var output in outputs)
        {
            var content = _templateRenderer.Render(output.TemplateId, language, values);
            results.Add(SafeWrite(repositoryPath, output.Path, content));
        }

        return results;
    }

    private GeneratedFileResult SafeWrite(string repositoryPath, string relativePath, string content)
    {
        var fullPath = Path.Combine(repositoryPath, relativePath.Replace('/', Path.DirectorySeparatorChar));
        if (_fileSystem.FileExists(fullPath))
        {
            return new GeneratedFileResult(relativePath, GeneratedFileStatus.SkippedExisting, "Existing file was not overwritten.");
        }

        _fileSystem.WriteAllText(fullPath, content.TrimEnd() + Environment.NewLine);
        return new GeneratedFileResult(relativePath, GeneratedFileStatus.Created, "File created.");
    }

    private IReadOnlyDictionary<string, string> BuildValues(string repositoryPath, ScanResult scanResult)
    {
        var projectName = new DirectoryInfo(repositoryPath).Name;
        var stackList = scanResult.Stacks.Count == 0
            ? "- Unknown"
            : string.Join(Environment.NewLine, scanResult.Stacks.Select(stack => $"- {stack.Name}: {stack.Signal}"));

        var visibleFiles = scanResult.Files.Take(120).Select(file => $"- {file}").ToList();
        if (scanResult.Files.Count > visibleFiles.Count)
        {
            visibleFiles.Add($"- ... {scanResult.Files.Count - visibleFiles.Count} more files");
        }

        return new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["ProjectName"] = projectName,
            ["RepositoryPath"] = repositoryPath,
            ["GeneratedAt"] = _clock.UtcNow.ToString("u", CultureInfo.InvariantCulture),
            ["StackList"] = stackList,
            ["FileList"] = visibleFiles.Count == 0 ? "- No files detected" : string.Join(Environment.NewLine, visibleFiles),
            ["HealthSummary"] = BuildHealthSummary(scanResult),
            ["RiskSummary"] = BuildRiskSummary(scanResult),
            ["RecommendedChecks"] = BuildRecommendedChecks(scanResult)
        };
    }

    private static string BuildHealthSummary(ScanResult scanResult)
    {
        var lines = new[]
        {
            $"- README: {YesNo(scanResult.HasReadme)}",
            $"- LICENSE: {YesNo(scanResult.HasLicense)}",
            $"- SECURITY: {YesNo(scanResult.HasSecurityPolicy)}",
            $"- Tests: {YesNo(scanResult.HasTests)}",
            $"- CI: {YesNo(scanResult.HasCi)}",
            $"- Agent instructions: {YesNo(scanResult.HasAgentInstructions)}"
        };

        return string.Join(Environment.NewLine, lines);
    }

    private static string BuildRiskSummary(ScanResult scanResult)
    {
        if (scanResult.Findings.Count == 0)
        {
            return "- No risk findings";
        }

        return string.Join(Environment.NewLine, new[]
        {
            $"- Critical: {scanResult.Findings.Count(finding => finding.Severity == RiskSeverity.Critical)}",
            $"- High: {scanResult.Findings.Count(finding => finding.Severity == RiskSeverity.High)}",
            $"- Medium: {scanResult.Findings.Count(finding => finding.Severity == RiskSeverity.Medium)}",
            $"- Low: {scanResult.Findings.Count(finding => finding.Severity == RiskSeverity.Low)}",
            $"- Info: {scanResult.Findings.Count(finding => finding.Severity == RiskSeverity.Info)}"
        });
    }

    private static string BuildRecommendedChecks(ScanResult scanResult)
    {
        var checks = new List<string>
        {
            "- ackit scan",
            "- ackit doctor",
            "- ackit redact-check --profile public-release"
        };

        if (scanResult.Stacks.Any(stack => stack.Name.Contains(".NET", StringComparison.OrdinalIgnoreCase) ||
                                           stack.Name.Contains("ASP.NET", StringComparison.OrdinalIgnoreCase) ||
                                           stack.Name.Contains("Blazor", StringComparison.OrdinalIgnoreCase)))
        {
            checks.Insert(0, "- dotnet build");
            checks.Insert(1, "- dotnet test");
        }

        return string.Join(Environment.NewLine, checks);
    }

    private static string YesNo(bool value)
    {
        return value ? "yes" : "no";
    }

    private static IReadOnlyList<(string Path, string TemplateId)> BuildOutputs(AgentTarget target)
    {
        var outputs = new List<(string Path, string TemplateId)>();

        if (target is AgentTarget.Codex or AgentTarget.All)
        {
            outputs.Add(("AGENTS.md", "AGENTS"));
            outputs.Add((".codex/HANDOFF.md", "HANDOFF"));
            outputs.Add((".codex/CONTEXT_PACK.md", "CONTEXT_PACK"));
        }

        if (target is AgentTarget.Claude or AgentTarget.All)
        {
            outputs.Add(("CLAUDE.md", "CLAUDE"));
        }

        if (target is AgentTarget.Cursor or AgentTarget.All)
        {
            outputs.Add((".cursor/rules/project.mdc", "CURSOR"));
        }

        if (target is AgentTarget.Copilot or AgentTarget.All)
        {
            outputs.Add((".github/copilot-instructions.md", "COPILOT"));
        }

        if (target == AgentTarget.All)
        {
            outputs.Add(("docs/PROJECT_MAP.md", "PROJECT_MAP"));
            outputs.Add(("docs/AI_WORKFLOW.md", "AI_WORKFLOW"));
            outputs.Add(("docs/SECURITY_NOTES.md", "SECURITY_NOTES"));
            outputs.Add(("docs/DEVELOPMENT_STANDARD.md", "DEVELOPMENT_STANDARD"));
            outputs.Add(("docs/tasks/TASK-0001.md", "TASK"));
        }

        return outputs;
    }
}

public sealed class HtmlReportGenerator : IHtmlReportGenerator
{
    private const string DefaultOutputPath = ".ackit/reports/scan-report.html";
    private readonly IFileSystem _fileSystem;
    private readonly IClock _clock;

    public HtmlReportGenerator(IFileSystem fileSystem, IClock clock)
    {
        _fileSystem = fileSystem;
        _clock = clock;
    }

    public GeneratedFileResult Generate(string repositoryPath, string? relativeOutputPath, LanguageCode language, ScanResult scanResult)
    {
        var outputPath = NormalizeOutputPath(repositoryPath, relativeOutputPath);
        var fullPath = Path.Combine(repositoryPath, outputPath.Replace('/', Path.DirectorySeparatorChar));

        if (_fileSystem.FileExists(fullPath))
        {
            return new GeneratedFileResult(outputPath, GeneratedFileStatus.SkippedExisting, "Existing report was not overwritten.");
        }

        var content = BuildHtml(repositoryPath, language, scanResult);
        _fileSystem.WriteAllText(fullPath, content);
        return new GeneratedFileResult(outputPath, GeneratedFileStatus.Created, "HTML report created.");
    }

    private static string NormalizeOutputPath(string repositoryPath, string? relativeOutputPath)
    {
        var outputPath = string.IsNullOrWhiteSpace(relativeOutputPath)
            ? DefaultOutputPath
            : relativeOutputPath.Trim().Replace('\\', '/');

        if (Path.IsPathRooted(outputPath))
        {
            throw new InvalidOperationException("Report output path must be repository-relative.");
        }

        var segments = outputPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length == 0 || segments.Any(segment => segment is "." or ".."))
        {
            throw new InvalidOperationException("Report output path must stay inside the repository.");
        }

        if (!outputPath.EndsWith(".html", StringComparison.OrdinalIgnoreCase) &&
            !outputPath.EndsWith(".htm", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Report output path must end with .html or .htm.");
        }

        var repositoryFullPath = Path.GetFullPath(repositoryPath);
        var reportFullPath = Path.GetFullPath(Path.Combine(repositoryFullPath, outputPath.Replace('/', Path.DirectorySeparatorChar)));
        var repositoryPrefix = repositoryFullPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;

        if (!reportFullPath.StartsWith(repositoryPrefix, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Report output path must stay inside the repository.");
        }

        return outputPath;
    }

    private string BuildHtml(string repositoryPath, LanguageCode language, ScanResult scanResult)
    {
        var generatedAt = _clock.UtcNow.ToString("u", CultureInfo.InvariantCulture);
        var projectName = new DirectoryInfo(repositoryPath).Name;
        var builder = new StringBuilder();

        builder.AppendLine("<!doctype html>");
        builder.AppendLine("<html lang=\"" + E(language.Value) + "\">");
        builder.AppendLine("<head>");
        builder.AppendLine("  <meta charset=\"utf-8\">");
        builder.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
        builder.AppendLine("  <title>" + E(projectName) + " - AgentContextKit Report</title>");
        builder.AppendLine("  <style>");
        builder.AppendLine("    :root{color-scheme:light;--ink:#18212f;--muted:#5d6b7d;--line:#d9e0e8;--panel:#f7f9fb;--accent:#2364aa;--ok:#20744a;--warn:#9b5d00;--critical:#a5282c;}");
        builder.AppendLine("    body{margin:0;font-family:Segoe UI,Arial,sans-serif;color:var(--ink);background:#fff;line-height:1.5;}");
        builder.AppendLine("    main{max-width:1120px;margin:0 auto;padding:32px 20px 48px;}");
        builder.AppendLine("    header{border-bottom:1px solid var(--line);padding-bottom:18px;margin-bottom:24px;}");
        builder.AppendLine("    h1{font-size:30px;margin:0 0 6px;} h2{font-size:20px;margin:28px 0 10px;} p{margin:0 0 10px;color:var(--muted);}");
        builder.AppendLine("    .grid{display:grid;grid-template-columns:repeat(auto-fit,minmax(160px,1fr));gap:10px;margin:16px 0;}");
        builder.AppendLine("    .metric{border:1px solid var(--line);background:var(--panel);border-radius:8px;padding:12px;}");
        builder.AppendLine("    .metric strong{display:block;font-size:22px;color:var(--ink);} .metric span{color:var(--muted);font-size:13px;}");
        builder.AppendLine("    table{border-collapse:collapse;width:100%;font-size:14px;} th,td{border:1px solid var(--line);padding:8px;text-align:left;vertical-align:top;} th{background:var(--panel);}");
        builder.AppendLine("    ul{padding-left:20px;} code{background:var(--panel);border:1px solid var(--line);border-radius:4px;padding:1px 4px;}");
        builder.AppendLine("    .severity-Critical{color:var(--critical);font-weight:700;} .severity-High{color:var(--warn);font-weight:700;} .ok{color:var(--ok);font-weight:700;}");
        builder.AppendLine("  </style>");
        builder.AppendLine("</head>");
        builder.AppendLine("<body><main>");
        builder.AppendLine("<header>");
        builder.AppendLine("  <h1>" + E(Label(language, "AgentContextKit Report", "AgentContextKit Raporu")) + "</h1>");
        builder.AppendLine("  <p>" + E(projectName) + " | " + E(generatedAt) + "</p>");
        builder.AppendLine("  <p>" + E(scanResult.RepositoryPath) + "</p>");
        builder.AppendLine("</header>");

        AppendMetrics(builder, language, scanResult);
        AppendHealth(builder, language, scanResult);
        AppendStacks(builder, language, scanResult);
        AppendFindings(builder, language, scanResult);
        AppendFiles(builder, language, scanResult);

        builder.AppendLine("</main></body></html>");
        return builder.ToString();
    }

    private static void AppendMetrics(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<section class=\"grid\" aria-label=\"" + E(Label(language, "Summary", "Ozet")) + "\">");
        AppendMetric(builder, Label(language, "Files", "Dosyalar"), scanResult.Files.Count.ToString(CultureInfo.InvariantCulture));
        AppendMetric(builder, Label(language, "Stacks", "Stackler"), scanResult.Stacks.Count.ToString(CultureInfo.InvariantCulture));
        AppendMetric(builder, Label(language, "Findings", "Bulgular"), scanResult.Findings.Count.ToString(CultureInfo.InvariantCulture));
        AppendMetric(builder, Label(language, "Critical", "Kritik"), scanResult.Findings.Count(finding => finding.Severity == RiskSeverity.Critical).ToString(CultureInfo.InvariantCulture));
        builder.AppendLine("</section>");
    }

    private static void AppendMetric(StringBuilder builder, string label, string value)
    {
        builder.AppendLine("<div class=\"metric\"><strong>" + E(value) + "</strong><span>" + E(label) + "</span></div>");
    }

    private static void AppendHealth(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<section>");
        builder.AppendLine("<h2>" + E(Label(language, "Repository Health", "Repository Sagligi")) + "</h2>");
        builder.AppendLine("<table><tbody>");
        AppendHealthRow(builder, "README", scanResult.HasReadme);
        AppendHealthRow(builder, "LICENSE", scanResult.HasLicense);
        AppendHealthRow(builder, "SECURITY", scanResult.HasSecurityPolicy);
        AppendHealthRow(builder, "Tests", scanResult.HasTests);
        AppendHealthRow(builder, "CI", scanResult.HasCi);
        AppendHealthRow(builder, "Agent instructions", scanResult.HasAgentInstructions);
        builder.AppendLine("</tbody></table>");
        builder.AppendLine("</section>");
    }

    private static void AppendHealthRow(StringBuilder builder, string name, bool value)
    {
        builder.AppendLine("<tr><th>" + E(name) + "</th><td class=\"" + (value ? "ok" : "") + "\">" + E(value ? "yes" : "no") + "</td></tr>");
    }

    private static void AppendStacks(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<section>");
        builder.AppendLine("<h2>" + E(Label(language, "Stacks", "Stackler")) + "</h2>");
        if (scanResult.Stacks.Count == 0)
        {
            builder.AppendLine("<p>Unknown</p>");
        }
        else
        {
            builder.AppendLine("<ul>");
            foreach (var stack in scanResult.Stacks)
            {
                builder.AppendLine("<li><strong>" + E(stack.Name) + "</strong>: " + E(stack.Signal) + "</li>");
            }

            builder.AppendLine("</ul>");
        }

        builder.AppendLine("</section>");
    }

    private static void AppendFindings(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<section>");
        builder.AppendLine("<h2>" + E(Label(language, "Risk Findings", "Risk Bulgulari")) + "</h2>");
        if (scanResult.Findings.Count == 0)
        {
            builder.AppendLine("<p class=\"ok\">" + E(Label(language, "No risk findings.", "Risk bulgusu yok.")) + "</p>");
        }
        else
        {
            builder.AppendLine("<table><thead><tr><th>Severity</th><th>Category</th><th>Path</th><th>Message</th></tr></thead><tbody>");
            foreach (var finding in scanResult.Findings.Take(100))
            {
                builder.AppendLine("<tr><td class=\"severity-" + E(finding.Severity.ToString()) + "\">" + E(finding.Severity.ToString()) + "</td><td>" + E(finding.Category.ToString()) + "</td><td><code>" + E(finding.Path) + "</code></td><td>" + E(finding.Message) + "</td></tr>");
            }

            builder.AppendLine("</tbody></table>");
        }

        builder.AppendLine("</section>");
    }

    private static void AppendFiles(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<section>");
        builder.AppendLine("<h2>" + E(Label(language, "Files", "Dosyalar")) + "</h2>");
        builder.AppendLine("<ul>");
        foreach (var file in scanResult.Files.Take(150))
        {
            builder.AppendLine("<li><code>" + E(file) + "</code></li>");
        }

        if (scanResult.Files.Count > 150)
        {
            builder.AppendLine("<li>... " + E((scanResult.Files.Count - 150).ToString(CultureInfo.InvariantCulture)) + " more</li>");
        }

        builder.AppendLine("</ul>");
        builder.AppendLine("</section>");
    }

    private static string Label(LanguageCode language, string english, string turkish)
    {
        return language.Value == "tr" ? turkish : english;
    }

    private static string E(string value)
    {
        return WebUtility.HtmlEncode(value);
    }
}

public sealed class TaskFileGenerator : ITaskFileGenerator
{
    private readonly IFileSystem _fileSystem;
    private readonly ITemplateRenderer _templateRenderer;

    public TaskFileGenerator(IFileSystem fileSystem, ITemplateRenderer templateRenderer)
    {
        _fileSystem = fileSystem;
        _templateRenderer = templateRenderer;
    }

    public GeneratedFileResult CreateTask(string repositoryPath, TaskSpec spec)
    {
        var taskDirectory = Path.Combine(repositoryPath, "docs", "tasks");
        _fileSystem.CreateDirectory(taskDirectory);

        var number = GetNextTaskNumber(repositoryPath);
        var taskNumber = $"TASK-{number:0000}";
        var slug = Slugify(spec.Title);
        var relativePath = $"docs/tasks/{taskNumber}-{slug}.md";
        var fullPath = Path.Combine(repositoryPath, relativePath.Replace('/', Path.DirectorySeparatorChar));

        if (_fileSystem.FileExists(fullPath))
        {
            return new GeneratedFileResult(relativePath, GeneratedFileStatus.SkippedExisting, "Existing task file was not overwritten.");
        }

        var content = _templateRenderer.Render("TASK", spec.Language, new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["TaskNumber"] = taskNumber,
            ["TaskTitle"] = spec.Title
        });

        _fileSystem.WriteAllText(fullPath, content.TrimEnd() + Environment.NewLine);
        return new GeneratedFileResult(relativePath, GeneratedFileStatus.Created, "Task file created.");
    }

    public static string Slugify(string title)
    {
        var normalized = title.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();
        var previousDash = false;

        foreach (var character in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(character);
            if (category == UnicodeCategory.NonSpacingMark)
            {
                continue;
            }

            var lower = char.ToLowerInvariant(character);
            if (char.IsLetterOrDigit(lower))
            {
                builder.Append(lower);
                previousDash = false;
            }
            else if (!previousDash && builder.Length > 0)
            {
                builder.Append('-');
                previousDash = true;
            }
        }

        var slug = builder.ToString().Trim('-');
        if (slug.Length == 0)
        {
            slug = "task";
        }

        return slug.Length <= 64 ? slug : slug[..64].Trim('-');
    }

    private int GetNextTaskNumber(string repositoryPath)
    {
        var taskDirectory = Path.Combine(repositoryPath, "docs", "tasks");
        if (!_fileSystem.DirectoryExists(taskDirectory))
        {
            return 1;
        }

        var max = 0;
        foreach (var file in _fileSystem.EnumerateFiles(taskDirectory, RepositoryScanner.IgnoredDirectoryNames))
        {
            var name = Path.GetFileName(file);
            if (name.Length < 9 || !name.StartsWith("TASK-", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (int.TryParse(name.AsSpan(5, 4), NumberStyles.None, CultureInfo.InvariantCulture, out var value))
            {
                max = Math.Max(max, value);
            }
        }

        return max + 1;
    }
}
