using AgentContextKit.Core;
using System.Text.Json;

namespace AgentContextKit.Cli;

public static class Program
{
    private const string Version = "0.1.0-alpha.1";
    private const int JsonSchemaVersion = 1;

    public static int Main(string[] args)
    {
        try
        {
            var services = CreateServices();
            var repositoryPath = Directory.GetCurrentDirectory();
            var config = services.ConfigReader.Read(repositoryPath);
            var language = LanguageCode.From(GetOption(args, "--lang") ?? config.DefaultLanguage.Value);
            var command = args.Length == 0 ? "help" : args[0].Trim().ToLowerInvariant();
            var json = HasFlag(args, "--json");

            return command switch
            {
                "help" or "--help" or "-h" => RunHelp(language, services.TextProvider),
                "version" or "--version" => RunVersion(),
                "init" => RunInit(repositoryPath, language, json, services),
                "scan" => RunScan(repositoryPath, config, language, json, services),
                "generate" => RunGenerate(args, repositoryPath, config, language, json, services),
                "task" => RunTask(args, repositoryPath, language, json, services),
                "redact-check" => RunRedactCheck(args, repositoryPath, config, language, json, services),
                "doctor" => RunDoctor(repositoryPath, config, language, json, services),
                _ => RunUnknown(command, language, services.TextProvider)
            };
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ackit error: {ex.Message}");
            Console.Error.WriteLine("Suggested action: check repository permissions and run `ackit --help`.");
            return 1;
        }
    }

    private static int RunHelp(LanguageCode language, ITextProvider textProvider)
    {
        Console.WriteLine(textProvider.Get("help", language));
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("  ackit init [--lang en|tr] [--json]");
        Console.WriteLine("  ackit scan [--lang en|tr] [--json]");
        Console.WriteLine("  ackit generate [--target codex|claude|cursor|copilot|all] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit task \"<title>\" [--lang en|tr] [--json]");
        Console.WriteLine("  ackit redact-check [--profile public-release] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit doctor [--lang en|tr] [--json]");
        Console.WriteLine("  ackit version");
        return 0;
    }

    private static int RunVersion()
    {
        Console.WriteLine($"AgentContextKit {Version}");
        return 0;
    }

    private static int RunInit(string repositoryPath, LanguageCode language, bool json, Services services)
    {
        var result = services.ConfigWriter.WriteDefaultIfMissing(repositoryPath, language);
        var agentFiles = new[] { "AGENTS.md", "CLAUDE.md", ".cursor/rules/project.mdc", ".github/copilot-instructions.md" }
            .Select(file =>
            {
                var fullPath = Path.Combine(repositoryPath, file.Replace('/', Path.DirectorySeparatorChar));
                return new
                {
                    path = file,
                    exists = services.FileSystem.FileExists(fullPath)
                };
            })
            .ToArray();

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                command = "init",
                config = ToGeneratedFileDto(result),
                agentInstructionFiles = agentFiles
            });
            return 0;
        }

        PrintGeneratedResult(result, services.TextProvider, language);

        Console.WriteLine();
        Console.WriteLine("Detected agent instruction files:");
        foreach (var file in agentFiles)
        {
            Console.WriteLine($"- {file.path}: {(file.exists ? "found" : "missing")}");
        }

        return 0;
    }

    private static int RunScan(string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        if (json)
        {
            WriteJson(ToScanDto("scan", scan));
            return 0;
        }

        PrintScan(scan, language, services);
        return 0;
    }

    private static int RunGenerate(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var target = ParseTarget(GetOption(args, "--target"));
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var results = services.AgentInstructionGenerator.Generate(repositoryPath, target, language, scan);

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                command = "generate",
                target = target.ToString(),
                files = results.Select(ToGeneratedFileDto).ToArray()
            });
            return 0;
        }

        foreach (var result in results)
        {
            PrintGeneratedResult(result, services.TextProvider, language);
        }

        return 0;
    }

    private static int RunTask(string[] args, string repositoryPath, LanguageCode language, bool json, Services services)
    {
        var title = GetTaskTitle(args);
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.Error.WriteLine("ackit task requires a title.");
            Console.Error.WriteLine("Example: ackit task \"Add role based permission management\" --lang en");
            return 1;
        }

        var result = services.TaskFileGenerator.CreateTask(repositoryPath, new TaskSpec(title, language));
        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                command = "task",
                file = ToGeneratedFileDto(result)
            });
            return 0;
        }

        PrintGeneratedResult(result, services.TextProvider, language);
        return 0;
    }

    private static int RunRedactCheck(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var profile = GetOption(args, "--profile") ?? "default";
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var findings = scan.Findings
            .Where(finding => finding.Category is RiskCategory.Secret or RiskCategory.Pii or RiskCategory.Brand or RiskCategory.LocalPath or RiskCategory.ProductionConfig)
            .ToArray();

        var exitCode = findings.Any(finding => finding.Severity == RiskSeverity.Critical)
            ? 2
            : findings.Length > 0 ? 1 : 0;

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                command = "redact-check",
                profile,
                exitCode,
                findings = findings.Select(ToRiskFindingDto).ToArray()
            });
            return exitCode;
        }

        PrintFindings(findings, language, services);
        return exitCode;
    }

    private static int RunDoctor(string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var result = services.Doctor.Check(repositoryPath, scan);
        var exitCode = result.Checks.Any(check => !check.Passed && check.Severity >= RiskSeverity.High) ? 1 : 0;

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                command = "doctor",
                exitCode,
                checks = result.Checks.Select(ToDoctorCheckDto).ToArray()
            });
            return exitCode;
        }

        Console.WriteLine(services.TextProvider.Get("doctor", language));
        foreach (var check in result.Checks)
        {
            var status = check.Passed ? "PASS" : "FAIL";
            Console.WriteLine($"- {status} [{check.Severity}] {check.Name}: {check.Message}");
        }

        return exitCode;
    }

    private static int RunUnknown(string command, LanguageCode language, ITextProvider textProvider)
    {
        Console.Error.WriteLine($"Unknown command: {command}");
        RunHelp(language, textProvider);
        return 1;
    }

    private static void PrintScan(ScanResult scan, LanguageCode language, Services services)
    {
        Console.WriteLine(services.TextProvider.Get("scanSummary", language));
        Console.WriteLine($"Repository: {scan.RepositoryPath}");
        Console.WriteLine($"Files: {scan.Files.Count}");

        Console.WriteLine();
        Console.WriteLine("Stacks:");
        if (scan.Stacks.Count == 0)
        {
            Console.WriteLine("- Unknown");
        }
        else
        {
            foreach (var stack in scan.Stacks)
            {
                Console.WriteLine($"- {stack.Name}: {stack.Signal}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Repository health:");
        Console.WriteLine($"- README: {YesNo(scan.HasReadme)}");
        Console.WriteLine($"- LICENSE: {YesNo(scan.HasLicense)}");
        Console.WriteLine($"- SECURITY: {YesNo(scan.HasSecurityPolicy)}");
        Console.WriteLine($"- Tests: {YesNo(scan.HasTests)}");
        Console.WriteLine($"- CI: {YesNo(scan.HasCi)}");
        Console.WriteLine($"- Docker: {YesNo(scan.HasDocker)}");
        Console.WriteLine($"- Agent instructions: {YesNo(scan.HasAgentInstructions)}");

        Console.WriteLine();
        PrintFindings(scan.Findings, language, services);
    }

    private static void PrintFindings(IReadOnlyList<RiskFinding> findings, LanguageCode language, Services services)
    {
        if (findings.Count == 0)
        {
            Console.WriteLine(services.TextProvider.Get("noFindings", language));
            return;
        }

        foreach (var severity in new[] { RiskSeverity.Critical, RiskSeverity.High, RiskSeverity.Medium, RiskSeverity.Low, RiskSeverity.Info })
        {
            var group = findings.Where(finding => finding.Severity == severity).Take(25).ToArray();
            if (group.Length == 0)
            {
                continue;
            }

            Console.WriteLine($"{severity}:");
            foreach (var finding in group)
            {
                var match = string.IsNullOrWhiteSpace(finding.Match) ? "" : $" ({finding.Match})";
                Console.WriteLine($"- {finding.Path}: {finding.Message}{match}");
            }

            var omitted = findings.Count(finding => finding.Severity == severity) - group.Length;
            if (omitted > 0)
            {
                Console.WriteLine($"- ... {omitted} more");
            }
        }
    }

    private static void PrintGeneratedResult(GeneratedFileResult result, ITextProvider textProvider, LanguageCode language)
    {
        var status = result.Created
            ? textProvider.Get("created", language)
            : textProvider.Get("skipped", language);

        Console.WriteLine($"- {result.Path}: {status}");
    }

    private static void WriteJson(object value)
    {
        Console.WriteLine(JsonSerializer.Serialize(value, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
    }

    private static object ToScanDto(string command, ScanResult scan)
    {
        return new
        {
            schemaVersion = JsonSchemaVersion,
            toolVersion = Version,
            command,
            repositoryPath = scan.RepositoryPath,
            fileCount = scan.Files.Count,
            stacks = scan.Stacks.Select(stack => new
            {
                name = stack.Name,
                signal = stack.Signal
            }).ToArray(),
            health = new
            {
                hasReadme = scan.HasReadme,
                hasLicense = scan.HasLicense,
                hasSecurityPolicy = scan.HasSecurityPolicy,
                hasContributing = scan.HasContributing,
                hasCodeOfConduct = scan.HasCodeOfConduct,
                hasChangelog = scan.HasChangelog,
                hasTests = scan.HasTests,
                hasCi = scan.HasCi,
                hasDocker = scan.HasDocker,
                hasAgentInstructions = scan.HasAgentInstructions
            },
            findings = scan.Findings.Select(ToRiskFindingDto).ToArray()
        };
    }

    private static object ToRiskFindingDto(RiskFinding finding)
    {
        return new
        {
            severity = finding.Severity.ToString(),
            category = finding.Category.ToString(),
            path = finding.Path,
            message = finding.Message,
            match = finding.Match
        };
    }

    private static object ToDoctorCheckDto(DoctorCheck check)
    {
        return new
        {
            name = check.Name,
            severity = check.Severity.ToString(),
            passed = check.Passed,
            message = check.Message
        };
    }

    private static object ToGeneratedFileDto(GeneratedFileResult result)
    {
        return new
        {
            path = result.Path,
            status = result.Status.ToString(),
            created = result.Created,
            message = result.Message
        };
    }

    private static string YesNo(bool value)
    {
        return value ? "yes" : "no";
    }

    private static AgentTarget ParseTarget(string? value)
    {
        return value?.Trim().ToLowerInvariant() switch
        {
            "codex" => AgentTarget.Codex,
            "claude" => AgentTarget.Claude,
            "cursor" => AgentTarget.Cursor,
            "copilot" => AgentTarget.Copilot,
            "all" or null or "" => AgentTarget.All,
            _ => AgentTarget.All
        };
    }

    private static string? GetOption(string[] args, string name)
    {
        for (var index = 0; index < args.Length; index++)
        {
            var current = args[index];
            if (current.StartsWith(name + "=", StringComparison.OrdinalIgnoreCase))
            {
                return current[(name.Length + 1)..];
            }

            if (string.Equals(current, name, StringComparison.OrdinalIgnoreCase) && index + 1 < args.Length)
            {
                return args[index + 1];
            }
        }

        return null;
    }

    private static bool HasFlag(string[] args, string name)
    {
        return args.Any(arg => string.Equals(arg, name, StringComparison.OrdinalIgnoreCase));
    }

    private static string GetTaskTitle(string[] args)
    {
        var parts = new List<string>();

        for (var index = 1; index < args.Length; index++)
        {
            var current = args[index];
            if (current.StartsWith("--", StringComparison.Ordinal))
            {
                if (OptionConsumesValue(current) && !current.Contains('=', StringComparison.Ordinal) && index + 1 < args.Length)
                {
                    index++;
                }

                continue;
            }

            parts.Add(current);
        }

        return string.Join(' ', parts).Trim();
    }

    private static bool OptionConsumesValue(string option)
    {
        return string.Equals(option, "--lang", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(option, "--target", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(option, "--profile", StringComparison.OrdinalIgnoreCase);
    }

    private static Services CreateServices()
    {
        var fileSystem = new PhysicalFileSystem();
        var secretScanner = new SecretScanner();
        var brandPiiScanner = new BrandPiiScanner();
        var riskScanner = new RiskScanner(fileSystem, secretScanner, brandPiiScanner);
        var stackDetector = new StackDetector(fileSystem);
        var repositoryScanner = new RepositoryScanner(fileSystem, stackDetector, riskScanner);
        var templateRenderer = new TemplateRenderer();
        var textProvider = new TextProvider();
        var clock = new SystemClock();

        return new Services(
            fileSystem,
            new AckitConfigReader(fileSystem),
            new AckitConfigWriter(fileSystem),
            repositoryScanner,
            new AgentInstructionGenerator(fileSystem, templateRenderer, clock),
            new TaskFileGenerator(fileSystem, templateRenderer),
            new RepositoryDoctor(fileSystem),
            textProvider);
    }

    private sealed record Services(
        IFileSystem FileSystem,
        IAckitConfigReader ConfigReader,
        IAckitConfigWriter ConfigWriter,
        IRepositoryScanner RepositoryScanner,
        IAgentInstructionGenerator AgentInstructionGenerator,
        ITaskFileGenerator TaskFileGenerator,
        RepositoryDoctor Doctor,
        ITextProvider TextProvider);
}
