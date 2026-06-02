using System.Globalization;
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
            ["FileList"] = visibleFiles.Count == 0 ? "- No files detected" : string.Join(Environment.NewLine, visibleFiles)
        };
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
