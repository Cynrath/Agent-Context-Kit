using AgentContextKit.Core;
using System.Text.Json;

namespace AgentContextKit.Cli;

public static class Program
{
    private const string Version = "0.2.0-alpha.1";
    private const string DefaultBaselinePath = ".ackit-baseline.json";
    private const int JsonSchemaVersion = 2;
    private const int ExitSuccess = 0;
    private const int ExitError = 1;
    private const int ExitCritical = 2;

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
            var ci = HasFlag(args, "--ci");

            return command switch
            {
                "help" or "--help" or "-h" => RunHelp(language, services.TextProvider),
                "version" or "--version" => RunVersion(),
                "init" => RunInit(repositoryPath, language, json, services),
                "scan" => RunScan(args, repositoryPath, config, language, json, ci, services),
                "baseline" => RunBaseline(args, repositoryPath, config, language, json, services),
                "sarif" => RunSarif(args, repositoryPath, config, language, json, services),
                "report" => RunReport(args, repositoryPath, config, language, json, services),
                "webui" => RunWebUi(args, repositoryPath, config, language, json, services),
                "prompt-pack" => RunPromptPack(args, repositoryPath, config, language, json, services),
                "context-export" => RunContextExport(args, repositoryPath, config, language, json, services),
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
            return ExitError;
        }
    }

    private static int RunHelp(LanguageCode language, ITextProvider textProvider)
    {
        Console.WriteLine(textProvider.Get("help", language));
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("  ackit init [--lang en|tr] [--json]");
        Console.WriteLine("  ackit scan [--baseline <repo-relative.json>] [--lang en|tr] [--json] [--ci]");
        Console.WriteLine("  ackit baseline [--output <repo-relative.json>] [--update] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit sarif --output <repo-relative.sarif> [--lang en|tr] [--json]");
        Console.WriteLine("  ackit report [--output <repo-relative.html>] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit webui [--output <repo-relative.html>] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit prompt-pack [--output <repo-relative.md>] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit context-export --prompt-pack <repo-relative.md> --approve [--output <repo-relative.json>] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit generate [--target codex|claude|cursor|copilot|all] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit task \"<title>\" [--lang en|tr] [--json]");
        Console.WriteLine("  ackit redact-check [--profile public-release] [--lang en|tr] [--json]");
        Console.WriteLine("  ackit doctor [--lang en|tr] [--json]");
        Console.WriteLine("  ackit version");
        return ExitSuccess;
    }

    private static int RunVersion()
    {
        Console.WriteLine($"AgentContextKit {Version}");
        return ExitSuccess;
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
                generatedAtUtc = services.Clock.UtcNow,
                command = "init",
                config = ToGeneratedFileDto(result),
                agentInstructionFiles = agentFiles
            });
            return ExitSuccess;
        }

        PrintGeneratedResult(result, services.TextProvider, language);

        Console.WriteLine();
        Console.WriteLine("Detected agent instruction files:");
        foreach (var file in agentFiles)
        {
            Console.WriteLine($"- {file.path}: {(file.exists ? "found" : "missing")}");
        }

        return ExitSuccess;
    }

    private static int RunScan(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, bool ci, Services services)
    {
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var baselinePath = GetOption(args, "--baseline");
        BaselineEvaluation? baseline = null;
        if (!string.IsNullOrWhiteSpace(baselinePath))
        {
            try
            {
                var manifest = services.BaselineStore.Load(repositoryPath, baselinePath);
                baseline = services.BaselineClassifier.Classify(scan.Findings, manifest);
            }
            catch (BaselineException ex)
            {
                return WriteBaselineError("scan", ex, json, services.Clock.UtcNow);
            }
        }

        var exitCode = baseline is null
            ? GetScanExitCode(scan, ci)
            : GetBaselineScanExitCode(baseline, ci);
        if (json)
        {
            WriteJson(ToScanDto("scan", scan, services.Clock.UtcNow, ci, exitCode, baselinePath, baseline));
            return exitCode;
        }

        PrintScan(scan, language, services);
        if (baseline is not null)
        {
            PrintBaselineClassification(baselinePath!, baseline);
        }

        return exitCode;
    }

    private static int RunBaseline(
        string[] args,
        string repositoryPath,
        AckitConfig config,
        LanguageCode language,
        bool json,
        Services services)
    {
        var outputPath = GetOption(args, "--output") ?? DefaultBaselinePath;
        try
        {
            var scan = services.RepositoryScanner.Scan(repositoryPath, config);
            var manifest = services.BaselineClassifier.CreateManifest(scan.Findings);
            var result = services.BaselineStore.Write(repositoryPath, outputPath, manifest, HasFlag(args, "--update"));
            if (json)
            {
                WriteJson(new
                {
                    schemaVersion = JsonSchemaVersion,
                    toolVersion = Version,
                    generatedAtUtc = services.Clock.UtcNow,
                    command = "baseline",
                    repositoryName = GetRepositoryName(repositoryPath),
                    baseline = new
                    {
                        path = result.Path,
                        status = result.Status.ToString(),
                        schemaVersion = manifest.SchemaVersion,
                        fingerprintAlgorithm = manifest.FingerprintAlgorithm,
                        entryCount = result.EntryCount
                    }
                });
                return ExitSuccess;
            }

            var verb = result.Status == BaselineFileStatus.Created ? "created" : "updated";
            Console.WriteLine($"Baseline {verb}: {result.Path}");
            Console.WriteLine($"Entries: {result.EntryCount}");
            Console.WriteLine("Review and commit the baseline only if it contains no private repository metadata.");
            return ExitSuccess;
        }
        catch (BaselineException ex)
        {
            return WriteBaselineError("baseline", ex, json, services.Clock.UtcNow);
        }
    }

    private static int RunSarif(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var outputPath = GetOption(args, "--output");
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("ackit sarif requires --output <repo-relative.sarif>.");
            return ExitError;
        }

        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var result = services.SarifReportWriter.Generate(repositoryPath, outputPath, scan, Version);
        var criticalHighCount = scan.Findings.Count(finding => finding.Severity is RiskSeverity.Critical or RiskSeverity.High);

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc = services.Clock.UtcNow,
                command = "sarif",
                repositoryName = GetRepositoryName(repositoryPath),
                riskSummary = ToRiskSummary(scan.Findings),
                criticalHighCount,
                sarif = ToGeneratedFileDto(result)
            });
            return ExitSuccess;
        }

        PrintGeneratedResult(result, services.TextProvider, language);
        Console.WriteLine($"SARIF findings: {scan.Findings.Count}");
        Console.WriteLine($"Critical/high findings: {criticalHighCount}");
        return ExitSuccess;
    }

    private static int RunReport(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var outputPath = GetOption(args, "--output");
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var result = services.HtmlReportGenerator.Generate(repositoryPath, outputPath, language, scan);

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc = services.Clock.UtcNow,
                command = "report",
                repositoryPath,
                repositoryName = GetRepositoryName(repositoryPath),
                riskSummary = ToRiskSummary(scan.Findings),
                report = ToGeneratedFileDto(result)
            });
            return ExitSuccess;
        }

        PrintGeneratedResult(result, services.TextProvider, language);
        Console.WriteLine($"Risk findings: {scan.Findings.Count}");
        return ExitSuccess;
    }

    private static int RunWebUi(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var outputPath = GetOption(args, "--output");
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var result = services.WebUiGenerator.Generate(repositoryPath, outputPath, language, scan);

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc = services.Clock.UtcNow,
                command = "webui",
                repositoryPath,
                repositoryName = GetRepositoryName(repositoryPath),
                riskSummary = ToRiskSummary(scan.Findings),
                webUi = ToGeneratedFileDto(result)
            });
            return ExitSuccess;
        }

        PrintGeneratedResult(result, services.TextProvider, language);
        Console.WriteLine($"Risk findings: {scan.Findings.Count}");
        return ExitSuccess;
    }

    private static int RunPromptPack(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var outputPath = GetOption(args, "--output");
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var result = services.PromptPackGenerator.Generate(repositoryPath, outputPath, language, scan);

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc = services.Clock.UtcNow,
                command = "prompt-pack",
                repositoryPath,
                repositoryName = GetRepositoryName(repositoryPath),
                riskSummary = ToRiskSummary(scan.Findings),
                promptPack = ToGeneratedFileDto(result)
            });
            return ExitSuccess;
        }

        PrintGeneratedResult(result, services.TextProvider, language);
        Console.WriteLine("No remote LLM provider call was made.");
        Console.WriteLine($"Risk findings: {scan.Findings.Count}");
        return ExitSuccess;
    }

    private static int RunContextExport(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        if (!HasFlag(args, "--approve"))
        {
            Console.Error.WriteLine("ackit context-export requires explicit --approve.");
            return ExitError;
        }

        var promptPackPath = GetOption(args, "--prompt-pack");
        if (string.IsNullOrWhiteSpace(promptPackPath))
        {
            Console.Error.WriteLine("ackit context-export requires --prompt-pack <repo-relative.md>.");
            return ExitError;
        }

        var outputPath = GetOption(args, "--output");
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var result = services.ContextExportManifestGenerator.Generate(
            repositoryPath,
            new ContextExportSpec(promptPackPath, outputPath, "explicit-cli-flag", language),
            scan);

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc = services.Clock.UtcNow,
                command = "context-export",
                repositoryPath,
                repositoryName = GetRepositoryName(repositoryPath),
                riskSummary = ToRiskSummary(scan.Findings),
                contextExport = ToGeneratedFileDto(result)
            });
            return ExitSuccess;
        }

        PrintGeneratedResult(result, services.TextProvider, language);
        Console.WriteLine("No remote LLM provider call was made.");
        Console.WriteLine("Approval recorded locally only.");
        return ExitSuccess;
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
                generatedAtUtc = services.Clock.UtcNow,
                command = "generate",
                target = target.ToString(),
                fileSummary = ToGeneratedFileSummary(results),
                files = results.Select(ToGeneratedFileDto).ToArray()
            });
            return ExitSuccess;
        }

        foreach (var result in results)
        {
            PrintGeneratedResult(result, services.TextProvider, language);
        }

        return ExitSuccess;
    }

    private static int RunTask(string[] args, string repositoryPath, LanguageCode language, bool json, Services services)
    {
        var title = GetTaskTitle(args);
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.Error.WriteLine("ackit task requires a title.");
            Console.Error.WriteLine("Example: ackit task \"Add role based permission management\" --lang en");
            return ExitError;
        }

        var result = services.TaskFileGenerator.CreateTask(repositoryPath, new TaskSpec(title, language));
        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc = services.Clock.UtcNow,
                command = "task",
                file = ToGeneratedFileDto(result)
            });
            return ExitSuccess;
        }

        PrintGeneratedResult(result, services.TextProvider, language);
        return ExitSuccess;
    }

    private static int RunRedactCheck(string[] args, string repositoryPath, AckitConfig config, LanguageCode language, bool json, Services services)
    {
        var profile = GetOption(args, "--profile") ?? "default";
        var scan = services.RepositoryScanner.Scan(repositoryPath, config);
        var findings = scan.Findings
            .Where(finding => finding.Category is RiskCategory.Secret or RiskCategory.Pii or RiskCategory.Brand or RiskCategory.LocalPath or RiskCategory.ProductionConfig)
            .ToArray();

        var exitCode = findings.Any(finding => finding.Severity == RiskSeverity.Critical)
            ? ExitCritical
            : findings.Length > 0 ? ExitError : ExitSuccess;

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc = services.Clock.UtcNow,
                command = "redact-check",
                repositoryPath,
                repositoryName = GetRepositoryName(repositoryPath),
                profile,
                exitCode,
                riskSummary = ToRiskSummary(findings),
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
        var exitCode = result.Checks.Any(check => !check.Passed && check.Severity >= RiskSeverity.High)
            ? ExitError
            : ExitSuccess;

        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc = services.Clock.UtcNow,
                command = "doctor",
                repositoryPath,
                repositoryName = GetRepositoryName(repositoryPath),
                exitCode,
                checkSummary = ToDoctorCheckSummary(result.Checks),
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
        return ExitError;
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
        PrintSuppressions(scan.Suppressions);
    }

    private static void PrintBaselineClassification(string baselinePath, BaselineEvaluation baseline)
    {
        Console.WriteLine();
        Console.WriteLine("Baseline classification:");
        Console.WriteLine($"- File: {baselinePath}");
        Console.WriteLine($"- Existing findings: {baseline.Existing.Count}");
        Console.WriteLine($"- New findings: {baseline.New.Count}");
        foreach (var finding in baseline.Findings.Take(25))
        {
            Console.WriteLine($"- {finding.Status}: {finding.Finding.Path} {RiskRuleCatalog.GetRuleId(finding.Finding)} [{finding.Finding.Severity}] occurrence {finding.Occurrence}");
        }

        if (baseline.Findings.Count > 25)
        {
            Console.WriteLine($"- ... {baseline.Findings.Count - 25} more");
        }
    }

    private static void PrintSuppressions(IReadOnlyList<RiskSuppression> suppressions)
    {
        if (suppressions.Count == 0)
        {
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Suppressed findings: {suppressions.Count}");
        foreach (var suppression in suppressions.Take(25))
        {
            Console.WriteLine($"- {suppression.Path}: {suppression.RuleId} [{suppression.Severity}/{suppression.Category}] via {ToSuppressionReason(suppression.Reason)}");
        }

        if (suppressions.Count > 25)
        {
            Console.WriteLine($"- ... {suppressions.Count - 25} more");
        }
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

    private static object ToScanDto(
        string command,
        ScanResult scan,
        DateTimeOffset generatedAtUtc,
        bool ciMode,
        int exitCode,
        string? baselinePath = null,
        BaselineEvaluation? baseline = null)
    {
        var result = new Dictionary<string, object?>
        {
            ["schemaVersion"] = JsonSchemaVersion,
            ["toolVersion"] = Version,
            ["generatedAtUtc"] = generatedAtUtc,
            ["command"] = command,
            ["ciMode"] = ciMode,
            ["exitCode"] = exitCode,
            ["repositoryPath"] = scan.RepositoryPath,
            ["repositoryName"] = GetRepositoryName(scan.RepositoryPath),
            ["fileCount"] = scan.Files.Count,
            ["stacks"] = scan.Stacks.Select(stack => new
            {
                name = stack.Name,
                signal = stack.Signal
            }).ToArray(),
            ["health"] = new
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
            ["riskSummary"] = ToRiskSummary(scan.Findings),
            ["findings"] = scan.Findings.Select(ToRiskFindingDto).ToArray(),
            ["suppressionSummary"] = new
            {
                total = scan.Suppressions.Count,
                safeDomains = scan.Suppressions.Count(suppression => suppression.Reason == RiskSuppressionReason.SafeDomain),
                ignoredPaths = scan.Suppressions.Count(suppression => suppression.Reason == RiskSuppressionReason.IgnoredPath),
                ignoredFindingIds = scan.Suppressions.Count(suppression => suppression.Reason == RiskSuppressionReason.IgnoredFindingId)
            },
            ["suppressions"] = scan.Suppressions.Select(suppression => new
            {
                ruleId = suppression.RuleId,
                severity = suppression.Severity.ToString(),
                category = suppression.Category.ToString(),
                path = suppression.Path,
                reason = ToSuppressionReason(suppression.Reason)
            }).ToArray()
        };

        if (baseline is not null)
        {
            result["baseline"] = new
            {
                path = baselinePath,
                schemaVersion = BaselineSchema.CurrentVersion,
                fingerprintAlgorithm = BaselineSchema.FingerprintAlgorithm,
                entryCount = baseline.BaselineEntryCount,
                existing = baseline.Existing.Count,
                @new = baseline.New.Count,
                classifiedFindings = baseline.Findings.Select(finding => new
                {
                    ruleId = RiskRuleCatalog.GetRuleId(finding.Finding),
                    severity = finding.Finding.Severity.ToString(),
                    path = finding.Finding.Path,
                    fingerprint = finding.Fingerprint,
                    status = finding.Status.ToString().ToLowerInvariant(),
                    occurrence = finding.Occurrence
                }).ToArray()
            };
        }

        return result;
    }

    private static string ToSuppressionReason(RiskSuppressionReason reason)
    {
        return reason switch
        {
            RiskSuppressionReason.SafeDomain => "safeDomains",
            RiskSuppressionReason.IgnoredPath => "ignoredPaths",
            RiskSuppressionReason.IgnoredFindingId => "ignoredFindingIds",
            _ => "unknown"
        };
    }

    private static object ToRiskFindingDto(RiskFinding finding)
    {
        return new
        {
            ruleId = RiskRuleCatalog.GetRuleId(finding),
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

    private static object ToRiskSummary(IReadOnlyList<RiskFinding> findings)
    {
        return new
        {
            total = findings.Count,
            critical = findings.Count(finding => finding.Severity == RiskSeverity.Critical),
            high = findings.Count(finding => finding.Severity == RiskSeverity.High),
            medium = findings.Count(finding => finding.Severity == RiskSeverity.Medium),
            low = findings.Count(finding => finding.Severity == RiskSeverity.Low),
            info = findings.Count(finding => finding.Severity == RiskSeverity.Info)
        };
    }

    private static object ToDoctorCheckSummary(IReadOnlyList<DoctorCheck> checks)
    {
        return new
        {
            total = checks.Count,
            passed = checks.Count(check => check.Passed),
            failed = checks.Count(check => !check.Passed),
            failedHighOrCritical = checks.Count(check => !check.Passed && check.Severity >= RiskSeverity.High)
        };
    }

    private static object ToGeneratedFileSummary(IReadOnlyList<GeneratedFileResult> results)
    {
        return new
        {
            total = results.Count,
            created = results.Count(result => result.Created),
            skipped = results.Count(result => !result.Created)
        };
    }

    private static string GetRepositoryName(string repositoryPath)
    {
        var trimmed = repositoryPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        return Path.GetFileName(trimmed);
    }

    private static string YesNo(bool value)
    {
        return value ? "yes" : "no";
    }

    private static int GetScanExitCode(ScanResult scan, bool ci)
    {
        if (!ci)
        {
            return ExitSuccess;
        }

        if (scan.Findings.Any(finding => finding.Severity == RiskSeverity.Critical))
        {
            return ExitCritical;
        }

        if (scan.Findings.Any(finding => finding.Severity == RiskSeverity.High))
        {
            return ExitError;
        }

        return ExitSuccess;
    }

    private static int GetBaselineScanExitCode(BaselineEvaluation baseline, bool ci)
    {
        if (!ci)
        {
            return ExitSuccess;
        }

        if (baseline.New.Any(finding => finding.Finding.Severity == RiskSeverity.Critical))
        {
            return ExitCritical;
        }

        if (baseline.New.Any(finding => finding.Finding.Severity == RiskSeverity.High))
        {
            return ExitError;
        }

        return ExitSuccess;
    }

    private static int WriteBaselineError(string command, BaselineException exception, bool json, DateTimeOffset generatedAtUtc)
    {
        if (json)
        {
            WriteJson(new
            {
                schemaVersion = JsonSchemaVersion,
                toolVersion = Version,
                generatedAtUtc,
                command,
                exitCode = ExitError,
                error = new
                {
                    code = exception.Code,
                    message = exception.Message
                }
            });
            return ExitError;
        }

        Console.Error.WriteLine($"{exception.Code}: {exception.Message}");
        return ExitError;
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
               string.Equals(option, "--profile", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(option, "--baseline", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(option, "--prompt-pack", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(option, "--output", StringComparison.OrdinalIgnoreCase);
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
            new BaselineStore(fileSystem),
            new BaselineClassifier(),
            repositoryScanner,
            new AgentInstructionGenerator(fileSystem, templateRenderer, clock),
            new HtmlReportGenerator(fileSystem, clock),
            new WebUiGenerator(fileSystem, clock),
            new PromptPackGenerator(fileSystem, clock),
            new ContextExportManifestGenerator(fileSystem, clock),
            new SarifReportWriter(fileSystem),
            new TaskFileGenerator(fileSystem, templateRenderer),
            new RepositoryDoctor(fileSystem),
            clock,
            textProvider);
    }

    private sealed record Services(
        IFileSystem FileSystem,
        IAckitConfigReader ConfigReader,
        IAckitConfigWriter ConfigWriter,
        IBaselineStore BaselineStore,
        IBaselineClassifier BaselineClassifier,
        IRepositoryScanner RepositoryScanner,
        IAgentInstructionGenerator AgentInstructionGenerator,
        IHtmlReportGenerator HtmlReportGenerator,
        IWebUiGenerator WebUiGenerator,
        IPromptPackGenerator PromptPackGenerator,
        IContextExportManifestGenerator ContextExportManifestGenerator,
        ISarifReportWriter SarifReportWriter,
        ITaskFileGenerator TaskFileGenerator,
        RepositoryDoctor Doctor,
        IClock Clock,
        ITextProvider TextProvider);
}
