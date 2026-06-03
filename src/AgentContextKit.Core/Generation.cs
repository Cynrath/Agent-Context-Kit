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

public sealed class WebUiGenerator : IWebUiGenerator
{
    private const string DefaultOutputPath = ".ackit/webui/index.html";
    private static readonly ExpectedPreviewFile[] ContextPreviewFiles =
    [
        new("AGENTS.md", "Codex"),
        new("CLAUDE.md", "Claude"),
        new(".cursor/rules/project.mdc", "Cursor"),
        new(".github/copilot-instructions.md", "Copilot"),
        new(".codex/HANDOFF.md", "Codex"),
        new(".codex/CONTEXT_PACK.md", "Codex"),
        new("docs/PROJECT_MAP.md", "Documentation"),
        new("docs/AI_WORKFLOW.md", "Documentation"),
        new("docs/SECURITY_NOTES.md", "Documentation"),
        new("docs/DEVELOPMENT_STANDARD.md", "Documentation")
    ];

    private readonly IFileSystem _fileSystem;
    private readonly IClock _clock;

    public WebUiGenerator(IFileSystem fileSystem, IClock clock)
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
            return new GeneratedFileResult(outputPath, GeneratedFileStatus.SkippedExisting, "Existing Web UI was not overwritten.");
        }

        var content = BuildHtml(repositoryPath, language, scanResult);
        _fileSystem.WriteAllText(fullPath, content);
        return new GeneratedFileResult(outputPath, GeneratedFileStatus.Created, "Web UI prototype created.");
    }

    private static string NormalizeOutputPath(string repositoryPath, string? relativeOutputPath)
    {
        var outputPath = string.IsNullOrWhiteSpace(relativeOutputPath)
            ? DefaultOutputPath
            : relativeOutputPath.Trim().Replace('\\', '/');

        if (Path.IsPathRooted(outputPath))
        {
            throw new InvalidOperationException("Web UI output path must be repository-relative.");
        }

        var segments = outputPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length == 0 || segments.Any(segment => segment is "." or ".."))
        {
            throw new InvalidOperationException("Web UI output path must stay inside the repository.");
        }

        if (!outputPath.EndsWith(".html", StringComparison.OrdinalIgnoreCase) &&
            !outputPath.EndsWith(".htm", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Web UI output path must end with .html or .htm.");
        }

        var repositoryFullPath = Path.GetFullPath(repositoryPath);
        var outputFullPath = Path.GetFullPath(Path.Combine(repositoryFullPath, outputPath.Replace('/', Path.DirectorySeparatorChar)));
        var repositoryPrefix = repositoryFullPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;

        if (!outputFullPath.StartsWith(repositoryPrefix, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Web UI output path must stay inside the repository.");
        }

        return outputPath;
    }

    private string BuildHtml(string repositoryPath, LanguageCode language, ScanResult scanResult)
    {
        var generatedAt = _clock.UtcNow.ToString("u", CultureInfo.InvariantCulture);
        var projectName = new DirectoryInfo(repositoryPath).Name;
        var taskFiles = scanResult.Files
            .Where(file => file.StartsWith("docs/tasks/TASK-", StringComparison.OrdinalIgnoreCase) &&
                           file.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(file => file, StringComparer.OrdinalIgnoreCase)
            .Take(12)
            .ToArray();

        var contextFiles = ContextPreviewFiles;
        var existingContextFileCount = contextFiles.Count(file => scanResult.Files.Contains(file.Path, StringComparer.OrdinalIgnoreCase));

        var builder = new StringBuilder();
        builder.AppendLine("<!doctype html>");
        builder.AppendLine("<html lang=\"" + E(language.Value) + "\">");
        builder.AppendLine("<head>");
        builder.AppendLine("  <meta charset=\"utf-8\">");
        builder.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
        builder.AppendLine("  <title>" + E(projectName) + " - AgentContextKit Web UI</title>");
        builder.AppendLine("  <style>");
        builder.AppendLine("    :root{color-scheme:light;--ink:#1d2633;--muted:#617086;--line:#d7e0ea;--panel:#f6f8fb;--panel2:#eef4f8;--accent:#1b6ca8;--ok:#1f7a4d;--warn:#a35f00;--bad:#b3262d;--info:#5c4d9b;}");
        builder.AppendLine("    *{box-sizing:border-box;} body{margin:0;font-family:Segoe UI,Arial,sans-serif;color:var(--ink);background:#fff;line-height:1.5;} a{color:var(--accent);text-decoration:none;} a:hover{text-decoration:underline;}");
        builder.AppendLine("    header{border-bottom:1px solid var(--line);background:var(--panel);padding:24px 20px;} main{max-width:1180px;margin:0 auto;padding:24px 20px 52px;} .top{max-width:1180px;margin:0 auto;}");
        builder.AppendLine("    h1{font-size:30px;margin:0 0 6px;} h2{font-size:20px;margin:28px 0 10px;} h3{font-size:16px;margin:0 0 6px;} p{margin:0 0 10px;color:var(--muted);} code{background:#fff;border:1px solid var(--line);border-radius:4px;padding:1px 4px;}");
        builder.AppendLine("    nav{display:flex;flex-wrap:wrap;gap:8px;margin-top:16px;} nav a{border:1px solid var(--line);border-radius:8px;background:#fff;padding:7px 10px;font-size:14px;}");
        builder.AppendLine("    .grid{display:grid;grid-template-columns:repeat(auto-fit,minmax(150px,1fr));gap:10px;margin:18px 0;} .metric{border:1px solid var(--line);background:#fff;border-radius:8px;padding:12px;min-height:82px;} .metric strong{display:block;font-size:24px;} .metric span{display:block;color:var(--muted);font-size:13px;}");
        builder.AppendLine("    .band{border-top:1px solid var(--line);padding-top:18px;} .split{display:grid;grid-template-columns:repeat(auto-fit,minmax(260px,1fr));gap:12px;} .tile{border:1px solid var(--line);border-radius:8px;background:var(--panel);padding:12px;}");
        builder.AppendLine("    table{border-collapse:collapse;width:100%;font-size:14px;} th,td{border:1px solid var(--line);padding:8px;text-align:left;vertical-align:top;} th{background:var(--panel2);} .tablewrap{overflow:auto;border:1px solid var(--line);border-radius:8px;} .tablewrap table{border:0;} .tablewrap th:first-child,.tablewrap td:first-child{border-left:0;} .tablewrap th:last-child,.tablewrap td:last-child{border-right:0;}");
        builder.AppendLine("    details{border:1px solid var(--line);border-radius:8px;background:#fff;margin:8px 0;} summary{cursor:pointer;padding:10px 12px;font-weight:600;} pre{white-space:pre-wrap;overflow:auto;margin:0;border-top:1px solid var(--line);background:#fbfcfe;padding:12px;font-size:13px;max-height:280px;}");
        builder.AppendLine("    .status{font-weight:700;} .ok{color:var(--ok);} .fail{color:var(--bad);} .severity-Critical{color:var(--bad);font-weight:700;} .severity-High{color:var(--warn);font-weight:700;} .severity-Medium{color:var(--info);font-weight:700;} .muted{color:var(--muted);} @media(max-width:700px){h1{font-size:24px;} main{padding:18px 14px 40px;} header{padding:20px 14px;} nav a{flex:1 1 150px;text-align:center;}}");
        builder.AppendLine("  </style>");
        builder.AppendLine("</head>");
        builder.AppendLine("<body>");
        builder.AppendLine("<header><div class=\"top\">");
        builder.AppendLine("  <h1>" + E(Label(language, "AgentContextKit Web UI", "AgentContextKit Web UI")) + "</h1>");
        builder.AppendLine("  <p>" + E(projectName) + " | " + E(generatedAt) + "</p>");
        builder.AppendLine("  <p><code>" + E(scanResult.RepositoryPath) + "</code></p>");
        builder.AppendLine("  <nav aria-label=\"" + E(Label(language, "Sections", "Bolumler")) + "\"><a href=\"#dashboard\">" + E(Label(language, "Dashboard", "Panel")) + "</a><a href=\"#health\">" + E(Label(language, "Health", "Saglik")) + "</a><a href=\"#findings\">" + E(Label(language, "Findings", "Bulgular")) + "</a><a href=\"#context\">" + E(Label(language, "Context Files", "Context Dosyalari")) + "</a><a href=\"#tasks\">" + E(Label(language, "Tasks", "Tasklar")) + "</a></nav>");
        builder.AppendLine("</div></header>");
        builder.AppendLine("<main>");

        AppendDashboard(builder, language, scanResult, taskFiles.Length, existingContextFileCount);
        AppendHealthAndStacks(builder, language, scanResult);
        AppendFindings(builder, language, scanResult);
        AppendGeneratedPreviewSection(builder, repositoryPath, language, scanResult, contextFiles);
        AppendTaskPreviewSection(builder, repositoryPath, language, taskFiles);

        builder.AppendLine("</main>");
        builder.AppendLine("</body></html>");
        return builder.ToString();
    }

    private static void AppendDashboard(StringBuilder builder, LanguageCode language, ScanResult scanResult, int taskCount, int contextCount)
    {
        var readinessScore = CalculateReadinessScore(scanResult);
        var reviewStatus = GetReviewStatus(language, scanResult);

        builder.AppendLine("<section id=\"dashboard\" class=\"band\">");
        builder.AppendLine("<h2>" + E(Label(language, "Scan Result Dashboard", "Tarama Sonucu Paneli")) + "</h2>");
        builder.AppendLine("<div class=\"grid\">");
        AppendMetric(builder, Label(language, "Readiness Score", "Hazirlik Skoru"), readinessScore.ToString(CultureInfo.InvariantCulture) + "%");
        AppendMetric(builder, Label(language, "Review Status", "Inceleme Durumu"), reviewStatus);
        AppendMetric(builder, Label(language, "Files", "Dosyalar"), scanResult.Files.Count);
        AppendMetric(builder, Label(language, "Stacks", "Stackler"), scanResult.Stacks.Count);
        AppendMetric(builder, Label(language, "Findings", "Bulgular"), scanResult.Findings.Count);
        AppendMetric(builder, Label(language, "Critical", "Kritik"), scanResult.Findings.Count(finding => finding.Severity == RiskSeverity.Critical));
        AppendMetric(builder, Label(language, "Task files", "Task dosyalari"), taskCount);
        AppendMetric(builder, Label(language, "Context files", "Context dosyalari"), contextCount);
        builder.AppendLine("</div>");
        builder.AppendLine("<div class=\"split\">");
        AppendSeverityBreakdown(builder, language, scanResult);
        AppendRecommendedChecks(builder, language, scanResult);
        builder.AppendLine("</div>");
        builder.AppendLine("</section>");
    }

    private static void AppendMetric(StringBuilder builder, string label, int value)
    {
        AppendMetric(builder, label, value.ToString(CultureInfo.InvariantCulture));
    }

    private static void AppendMetric(StringBuilder builder, string label, string value)
    {
        builder.AppendLine("<div class=\"metric\"><strong>" + E(value) + "</strong><span>" + E(label) + "</span></div>");
    }

    private static int CalculateReadinessScore(ScanResult scanResult)
    {
        var checks = new[]
        {
            scanResult.HasReadme,
            scanResult.HasLicense,
            scanResult.HasSecurityPolicy,
            scanResult.HasContributing,
            scanResult.HasCodeOfConduct,
            scanResult.HasChangelog,
            scanResult.HasTests,
            scanResult.HasCi,
            scanResult.HasAgentInstructions,
            !scanResult.Findings.Any(finding => finding.Severity is RiskSeverity.High or RiskSeverity.Critical)
        };

        var passed = checks.Count(check => check);
        return (int)Math.Round((double)passed / checks.Length * 100, MidpointRounding.AwayFromZero);
    }

    private static string GetReviewStatus(LanguageCode language, ScanResult scanResult)
    {
        if (scanResult.Findings.Any(finding => finding.Severity == RiskSeverity.Critical))
        {
            return Label(language, "Blocked", "Bloke");
        }

        if (scanResult.Findings.Any(finding => finding.Severity == RiskSeverity.High))
        {
            return Label(language, "Needs review", "Inceleme gerekli");
        }

        if (scanResult.Findings.Count > 0)
        {
            return Label(language, "Review recommended", "Inceleme onerilir");
        }

        return Label(language, "Ready for local review", "Lokal incelemeye hazir");
    }

    private static void AppendSeverityBreakdown(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<div class=\"tile\"><h3>" + E(Label(language, "Risk Severity Breakdown", "Risk Seviye Kirilimi")) + "</h3>");
        builder.AppendLine("<table><tbody>");
        AppendSeverityRow(builder, RiskSeverity.Critical, scanResult);
        AppendSeverityRow(builder, RiskSeverity.High, scanResult);
        AppendSeverityRow(builder, RiskSeverity.Medium, scanResult);
        AppendSeverityRow(builder, RiskSeverity.Low, scanResult);
        AppendSeverityRow(builder, RiskSeverity.Info, scanResult);
        builder.AppendLine("</tbody></table></div>");
    }

    private static void AppendSeverityRow(StringBuilder builder, RiskSeverity severity, ScanResult scanResult)
    {
        var severityName = severity.ToString();
        var count = scanResult.Findings.Count(finding => finding.Severity == severity);
        builder.AppendLine("<tr><th class=\"severity-" + E(severityName) + "\">" + E(severityName) + "</th><td>" + E(count.ToString(CultureInfo.InvariantCulture)) + "</td></tr>");
    }

    private static void AppendRecommendedChecks(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<div class=\"tile\"><h3>" + E(Label(language, "Recommended Checks", "Onerilen Kontroller")) + "</h3>");
        builder.AppendLine("<ul>");
        foreach (var check in BuildRecommendedChecks(scanResult))
        {
            builder.AppendLine("<li><code>" + E(check) + "</code></li>");
        }

        builder.AppendLine("</ul></div>");
    }

    private static IReadOnlyList<string> BuildRecommendedChecks(ScanResult scanResult)
    {
        var checks = new List<string>();

        if (scanResult.Stacks.Any(stack => stack.Name.Contains(".NET", StringComparison.OrdinalIgnoreCase) ||
                                           stack.Name.Contains("ASP.NET", StringComparison.OrdinalIgnoreCase) ||
                                           stack.Name.Contains("Blazor", StringComparison.OrdinalIgnoreCase)))
        {
            checks.Add("dotnet build AgentContextKit.sln -c Release --no-restore");
            checks.Add("dotnet test AgentContextKit.sln -c Release --no-build");
        }

        checks.Add("ackit scan --ci");
        checks.Add("ackit redact-check --profile public-release");
        checks.Add("ackit report --output .ackit/reports/current.html");
        checks.Add("ackit webui --output .ackit/webui/current.html");

        return checks;
    }

    private static void AppendHealthAndStacks(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<section id=\"health\" class=\"band\">");
        builder.AppendLine("<h2>" + E(Label(language, "Repository Health", "Repository Sagligi")) + "</h2>");
        builder.AppendLine("<div class=\"split\">");
        builder.AppendLine("<div class=\"tile\"><h3>" + E(Label(language, "Health Checks", "Saglik Kontrolleri")) + "</h3><table><tbody>");
        AppendHealthRow(builder, "README", scanResult.HasReadme);
        AppendHealthRow(builder, "LICENSE", scanResult.HasLicense);
        AppendHealthRow(builder, "SECURITY", scanResult.HasSecurityPolicy);
        AppendHealthRow(builder, "CONTRIBUTING", scanResult.HasContributing);
        AppendHealthRow(builder, "CODE_OF_CONDUCT", scanResult.HasCodeOfConduct);
        AppendHealthRow(builder, "CHANGELOG", scanResult.HasChangelog);
        AppendHealthRow(builder, "Tests", scanResult.HasTests);
        AppendHealthRow(builder, "CI", scanResult.HasCi);
        AppendHealthRow(builder, "Docker", scanResult.HasDocker);
        AppendHealthRow(builder, "Agent instructions", scanResult.HasAgentInstructions);
        builder.AppendLine("</tbody></table></div>");
        builder.AppendLine("<div class=\"tile\"><h3>" + E(Label(language, "Stack Signals", "Stack Sinyalleri")) + "</h3>");
        if (scanResult.Stacks.Count == 0)
        {
            builder.AppendLine("<p>" + E(Label(language, "No stack signals detected.", "Stack sinyali bulunmadi.")) + "</p>");
        }
        else
        {
            builder.AppendLine("<table><thead><tr><th>Name</th><th>Signal</th></tr></thead><tbody>");
            foreach (var stack in scanResult.Stacks)
            {
                builder.AppendLine("<tr><td>" + E(stack.Name) + "</td><td><code>" + E(stack.Signal) + "</code></td></tr>");
            }

            builder.AppendLine("</tbody></table>");
        }

        builder.AppendLine("</div></div>");
        builder.AppendLine("</section>");
    }

    private static void AppendHealthRow(StringBuilder builder, string name, bool value)
    {
        var className = value ? "ok" : "fail";
        builder.AppendLine("<tr><th>" + E(name) + "</th><td class=\"status " + className + "\">" + E(value ? "yes" : "no") + "</td></tr>");
    }

    private static void AppendFindings(StringBuilder builder, LanguageCode language, ScanResult scanResult)
    {
        builder.AppendLine("<section id=\"findings\" class=\"band\">");
        builder.AppendLine("<h2>" + E(Label(language, "Risk Finding Browser", "Risk Bulgusu Tarayici")) + "</h2>");
        if (scanResult.Findings.Count == 0)
        {
            builder.AppendLine("<p class=\"status ok\">" + E(Label(language, "No risk findings.", "Risk bulgusu yok.")) + "</p>");
        }
        else
        {
            builder.AppendLine("<div class=\"tablewrap\"><table><thead><tr><th>Severity</th><th>Category</th><th>Path</th><th>Message</th></tr></thead><tbody>");
            foreach (var finding in scanResult.Findings.Take(150))
            {
                var severity = finding.Severity.ToString();
                builder.AppendLine("<tr><td class=\"severity-" + E(severity) + "\">" + E(severity) + "</td><td>" + E(finding.Category.ToString()) + "</td><td><code>" + E(finding.Path) + "</code></td><td>" + E(finding.Message) + "</td></tr>");
            }

            builder.AppendLine("</tbody></table></div>");
            if (scanResult.Findings.Count > 150)
            {
                builder.AppendLine("<p class=\"muted\">" + E((scanResult.Findings.Count - 150).ToString(CultureInfo.InvariantCulture)) + " " + E(Label(language, "additional findings omitted.", "ek bulgu gosterilmedi.")) + "</p>");
            }
        }

        builder.AppendLine("</section>");
    }

    private void AppendGeneratedPreviewSection(StringBuilder builder, string repositoryPath, LanguageCode language, ScanResult scanResult, IReadOnlyList<ExpectedPreviewFile> files)
    {
        builder.AppendLine("<section id=\"context\" class=\"band\">");
        builder.AppendLine("<h2>" + E(Label(language, "Generated File Preview", "Uretilen Dosya Onizleme")) + "</h2>");
        builder.AppendLine("<p>" + E(Label(language, "Expected agent/context files with local status, category, size, and capped previews.", "Beklenen agent/context dosyalari; lokal durum, kategori, boyut ve sinirli onizleme.")) + "</p>");
        builder.AppendLine("<p class=\"muted\">" + E(Label(language, "Status values: Present, Missing.", "Durum degerleri: Mevcut, Eksik.")) + "</p>");
        builder.AppendLine("<div class=\"tablewrap\"><table><thead><tr><th>Category</th><th>Status</th><th>Size</th><th>Path</th></tr></thead><tbody>");
        foreach (var file in files)
        {
            var exists = scanResult.Files.Contains(file.Path, StringComparer.OrdinalIgnoreCase);
            var status = exists ? Label(language, "Present", "Mevcut") : Label(language, "Missing", "Eksik");
            var size = exists ? FormatBytes(ReadFileLength(repositoryPath, file.Path)) : "-";
            builder.AppendLine("<tr><td>" + E(file.Category) + "</td><td class=\"status " + (exists ? "ok" : "fail") + "\">" + E(status) + "</td><td>" + E(size) + "</td><td><code>" + E(file.Path) + "</code></td></tr>");
        }

        builder.AppendLine("</tbody></table></div>");
        foreach (var file in files)
        {
            var exists = scanResult.Files.Contains(file.Path, StringComparer.OrdinalIgnoreCase);
            var status = exists ? Label(language, "Present", "Mevcut") : Label(language, "Missing", "Eksik");
            builder.AppendLine("<details>");
            builder.AppendLine("<summary><code>" + E(file.Path) + "</code> - " + E(file.Category) + " - " + E(status) + "</summary>");
            builder.AppendLine("<pre>" + E(exists ? ReadPreview(repositoryPath, file.Path) : Label(language, "Missing file. Generate intentionally with ackit generate --target all.", "Eksik dosya. Bilerek uretmek icin ackit generate --target all kullanin.")) + "</pre>");
            builder.AppendLine("</details>");
        }

        builder.AppendLine("</section>");
    }

    private void AppendTaskPreviewSection(StringBuilder builder, string repositoryPath, LanguageCode language, IReadOnlyList<string> files)
    {
        builder.AppendLine("<section id=\"tasks\" class=\"band\">");
        builder.AppendLine("<h2>" + E(Label(language, "Task Preview", "Task Onizleme")) + "</h2>");
        builder.AppendLine("<p>" + E(Label(language, "Latest task files under docs/tasks.", "docs/tasks altindaki son task dosyalari.")) + "</p>");
        if (files.Count == 0)
        {
            builder.AppendLine("<p class=\"muted\">" + E(Label(language, "No matching files detected.", "Eslesen dosya bulunmadi.")) + "</p>");
        }
        else
        {
            foreach (var file in files)
            {
                builder.AppendLine("<details>");
                builder.AppendLine("<summary><code>" + E(file) + "</code></summary>");
                builder.AppendLine("<pre>" + E(ReadPreview(repositoryPath, file)) + "</pre>");
                builder.AppendLine("</details>");
            }
        }

        builder.AppendLine("</section>");
    }

    private long ReadFileLength(string repositoryPath, string relativePath)
    {
        try
        {
            var fullPath = Path.Combine(repositoryPath, relativePath.Replace('/', Path.DirectorySeparatorChar));
            return _fileSystem.GetFileLength(fullPath);
        }
        catch (IOException)
        {
            return 0;
        }
        catch (UnauthorizedAccessException)
        {
            return 0;
        }
    }

    private static string FormatBytes(long bytes)
    {
        if (bytes < 1024)
        {
            return bytes.ToString(CultureInfo.InvariantCulture) + " B";
        }

        if (bytes < 1024 * 1024)
        {
            return (bytes / 1024d).ToString("0.0", CultureInfo.InvariantCulture) + " KB";
        }

        return (bytes / 1024d / 1024d).ToString("0.0", CultureInfo.InvariantCulture) + " MB";
    }

    private string ReadPreview(string repositoryPath, string relativePath)
    {
        try
        {
            var fullPath = Path.Combine(repositoryPath, relativePath.Replace('/', Path.DirectorySeparatorChar));
            var content = _fileSystem.ReadAllText(fullPath).Replace("\r", "", StringComparison.Ordinal);
            var lines = content.Split('\n').Take(18).Select(TrimPreviewLine).ToArray();
            var preview = string.Join(Environment.NewLine, lines).TrimEnd();
            return string.IsNullOrWhiteSpace(preview) ? "(empty file)" : preview;
        }
        catch (IOException)
        {
            return "(preview unavailable)";
        }
        catch (UnauthorizedAccessException)
        {
            return "(preview unavailable)";
        }
    }

    private static string TrimPreviewLine(string line)
    {
        return line.Length <= 180 ? line : line[..180] + "...";
    }

    private static string Label(LanguageCode language, string english, string turkish)
    {
        return language.Value == "tr" ? turkish : english;
    }

    private static string E(string value)
    {
        return WebUtility.HtmlEncode(value);
    }

    private sealed record ExpectedPreviewFile(string Path, string Category);
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
