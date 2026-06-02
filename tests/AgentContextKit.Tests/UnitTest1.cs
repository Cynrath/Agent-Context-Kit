using AgentContextKit.Core;
using System.Text.Json.Nodes;

[assembly: Xunit.CollectionBehavior(DisableTestParallelization = true)]

namespace AgentContextKit.Tests;

public sealed class StackDetectorTests
{
    [Fact]
    public void DetectsDotNetSolution()
    {
        var detector = new StackDetector();

        var stacks = detector.Detect("repo", ["AgentContextKit.sln", "src/App/App.csproj"]);

        Assert.Contains(stacks, stack => stack.Name == ".NET");
    }

    [Fact]
    public void DetectsNodePackageJson()
    {
        var detector = new StackDetector();

        var stacks = detector.Detect("repo", ["package.json"]);

        Assert.Contains(stacks, stack => stack.Name == "Node");
    }
}

public sealed class RiskScannerTests
{
    [Fact]
    public void SecretScannerFindsPasswordAssignment()
    {
        var scanner = new SecretScanner();

        var findings = scanner.ScanText("appsettings.json", "password" + "=not-for-public");

        Assert.Contains(findings, finding => finding.Severity == RiskSeverity.High && finding.Category == RiskCategory.Secret);
    }

    [Fact]
    public void SecretScannerFindsCriticalOpenAiLikeKey()
    {
        var scanner = new SecretScanner();

        var fakeKey = "sk-proj-" + "1234" + "567890abcdef";
        var findings = scanner.ScanText("settings.txt", "token" + "=" + fakeKey);

        Assert.Contains(findings, finding => finding.Severity == RiskSeverity.Critical);
    }

    [Fact]
    public void RepositoryScannerIgnoresGitBinAndObjContent()
    {
        using var repo = TempRepository.Create();
        repo.Write(".git/config", "token" + "=" + "sk-proj-" + "1234" + "567890abcdef");
        repo.Write("bin/output.txt", "password" + "=hidden");
        repo.Write("obj/cache.txt", "password" + "=hidden");
        repo.Write("README.md", "# Test");

        var scanner = TestServices.CreateRepositoryScanner();
        var result = scanner.Scan(repo.Path);

        Assert.DoesNotContain(result.Files, file => file.StartsWith(".git/", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(result.Files, file => file.StartsWith("bin/", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(result.Files, file => file.StartsWith("obj/", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(result.Findings, finding => finding.Severity == RiskSeverity.Critical);
    }

    [Fact]
    public void RepositoryScannerUsesConfiguredIgnorePaths()
    {
        using var repo = TempRepository.Create();
        repo.Write("generated/secret.txt", "token" + "=" + "sk-proj-" + "1234" + "567890abcdef");
        repo.Write("README.md", "# Test");
        var scanner = TestServices.CreateRepositoryScanner();

        var result = scanner.Scan(repo.Path, AckitConfig.Default with
        {
            IgnorePaths = ["generated/"]
        });

        Assert.DoesNotContain(result.Files, file => file.StartsWith("generated/", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(result.Findings, finding => finding.Path.StartsWith("generated/", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void RiskScannerUsesConfiguredRiskExtensions()
    {
        using var repo = TempRepository.Create();
        repo.Write("private.snapshot", "not secret");
        var scanner = TestServices.CreateRepositoryScanner();

        var result = scanner.Scan(repo.Path, AckitConfig.Default with
        {
            RiskExtensions = [".snapshot"]
        });

        Assert.Contains(result.Findings, finding => finding.Path == "private.snapshot" && finding.Category == RiskCategory.BuildArtifact);
    }

    [Fact]
    public void BrandPiiScannerFindsConfiguredKeywordAndEmail()
    {
        var scanner = new BrandPiiScanner();

        var findings = scanner.ScanText("notes.txt", "Contact private-name at private@example.internal", AckitConfig.Default with
        {
            PiiKeywords = ["private-name"]
        });

        Assert.Contains(findings, finding => finding.Category == RiskCategory.Pii && finding.Match == "private-name");
    }
}

public sealed class TemplateAndGenerationTests
{
    [Fact]
    public void TemplateRendererFallsBackToEnglishForUnknownLanguage()
    {
        var renderer = new TemplateRenderer();

        var content = renderer.Render("AGENTS", LanguageCode.From("de"), new Dictionary<string, string>
        {
            ["ProjectName"] = "Demo",
            ["StackList"] = "- .NET"
        });

        Assert.Contains("Agent Instructions", content);
    }

    [Fact]
    public void TaskGeneratorProducesSafeSlugAndNextTaskNumber()
    {
        using var repo = TempRepository.Create();
        repo.Write("docs/tasks/TASK-0001-foundation.md", "# Existing");
        var generator = new TaskFileGenerator(new PhysicalFileSystem(), new TemplateRenderer());

        var result = generator.CreateTask(repo.Path, new TaskSpec("Admin panel yetki sistemi ekle!", LanguageCode.Turkish));

        Assert.True(result.Created);
        Assert.Equal("docs/tasks/TASK-0002-admin-panel-yetki-sistemi-ekle.md", result.Path);
        Assert.True(File.Exists(System.IO.Path.Combine(repo.Path, result.Path.Replace('/', System.IO.Path.DirectorySeparatorChar))));
    }

    [Fact]
    public void AgentGeneratorDoesNotOverwriteExistingFiles()
    {
        using var repo = TempRepository.Create();
        repo.Write("AGENTS.md", "original");
        var generator = new AgentInstructionGenerator(new PhysicalFileSystem(), new TemplateRenderer(), new FixedClock());
        var scan = new ScanResult(repo.Path, ["AGENTS.md"], [new StackInfo(".NET", ".csproj")], [], true, false, false, false, false, false, false, false, false, true);

        var results = generator.Generate(repo.Path, AgentTarget.Codex, LanguageCode.English, scan);

        Assert.Contains(results, result => result.Path == "AGENTS.md" && result.Status == GeneratedFileStatus.SkippedExisting);
        Assert.Equal("original", File.ReadAllText(System.IO.Path.Combine(repo.Path, "AGENTS.md")));
    }
}

public sealed class ConfigAndDoctorTests
{
    [Fact]
    public void ConfigReaderFallsBackToEnglishForUnknownLanguage()
    {
        using var repo = TempRepository.Create();
        repo.Write(".ackit/config.yml", "defaultLanguage: de");
        var reader = new AckitConfigReader(new PhysicalFileSystem());

        var config = reader.Read(repo.Path);

        Assert.Equal("en", config.DefaultLanguage.Value);
    }

    [Fact]
    public void ConfigReaderParsesSchemaIgnorePathsAndRiskExtensions()
    {
        using var repo = TempRepository.Create();
        repo.Write(".ackit/config.yml", """
        schemaVersion: 3
        defaultLanguage: tr
        brandKeywords: ["Acme"]
        piiKeywords:
          - private-name
        ignorePaths:
          - generated/
        riskExtensions:
          - snapshot
        """);
        var reader = new AckitConfigReader(new PhysicalFileSystem());

        var config = reader.Read(repo.Path);

        Assert.Equal(3, config.SchemaVersion);
        Assert.Equal("tr", config.DefaultLanguage.Value);
        Assert.Contains("Acme", config.BrandKeywords);
        Assert.Contains("private-name", config.PiiKeywords);
        Assert.Contains("generated/", config.IgnorePaths);
        Assert.Contains(".snapshot", config.RiskExtensions);
    }

    [Fact]
    public void DoctorReportsMissingReadmeLicenseAndSecurity()
    {
        using var repo = TempRepository.Create();
        var scanner = TestServices.CreateRepositoryScanner();
        var scan = scanner.Scan(repo.Path);
        var doctor = new RepositoryDoctor(new PhysicalFileSystem());

        var result = doctor.Check(repo.Path, scan);

        Assert.Contains(result.Checks, check => check.Name == "README" && !check.Passed);
        Assert.Contains(result.Checks, check => check.Name == "LICENSE" && !check.Passed);
        Assert.Contains(result.Checks, check => check.Name == "SECURITY" && !check.Passed);
    }
}

public sealed class CliJsonAndMetadataTests
{
    [Fact]
    public void ScanJsonOutputIsValid()
    {
        using var repo = TempRepository.Create();
        repo.Write("README.md", "# Demo");
        repo.Write("LICENSE", "MIT");

        var result = RunCli(repo.Path, ["scan", "--json"]);
        var json = JsonNode.Parse(result.Output);

        Assert.Equal(0, result.ExitCode);
        Assert.Equal(1, json?["schemaVersion"]?.GetValue<int>());
        Assert.Equal("0.1.0-alpha.1", json?["toolVersion"]?.GetValue<string>());
        Assert.Equal("scan", json?["command"]?.GetValue<string>());
        Assert.True(json?["fileCount"]?.GetValue<int>() >= 2);
    }

    [Fact]
    public void DoctorJsonOutputIsValid()
    {
        using var repo = TempRepository.Create();
        repo.Write("README.md", "# Demo");
        repo.Write("LICENSE", "MIT");
        repo.Write("SECURITY.md", "# Security");
        repo.Write(".gitignore", "bin/");
        repo.Write("AGENTS.md", "# Agents");
        repo.Write("tests/DemoTests.cs", "// tests");

        var result = RunCli(repo.Path, ["doctor", "--json"]);
        var json = JsonNode.Parse(result.Output);

        Assert.Equal(0, result.ExitCode);
        Assert.Equal(1, json?["schemaVersion"]?.GetValue<int>());
        Assert.Equal("0.1.0-alpha.1", json?["toolVersion"]?.GetValue<string>());
        Assert.Equal("doctor", json?["command"]?.GetValue<string>());
        Assert.True(json?["checks"]?.AsArray().Count > 0);
    }

    [Fact]
    public void RedactCheckJsonPreservesCriticalExitCode()
    {
        using var repo = TempRepository.Create();
        repo.Write("settings.txt", "token" + "=" + "sk-proj-" + "1234" + "567890abcdef");

        var result = RunCli(repo.Path, ["redact-check", "--profile", "public-release", "--json"]);
        var json = JsonNode.Parse(result.Output);

        Assert.Equal(2, result.ExitCode);
        Assert.Equal(1, json?["schemaVersion"]?.GetValue<int>());
        Assert.Equal("0.1.0-alpha.1", json?["toolVersion"]?.GetValue<string>());
        Assert.Equal("redact-check", json?["command"]?.GetValue<string>());
        Assert.Equal("public-release", json?["profile"]?.GetValue<string>());
        Assert.Equal(2, json?["exitCode"]?.GetValue<int>());
    }

    [Fact]
    public void PackageMetadataAndLicenseUsePseudonym()
    {
        var root = LocateRepositoryRoot();
        var projectFile = File.ReadAllText(System.IO.Path.Combine(root, "src", "AgentContextKit.Cli", "AgentContextKit.Cli.csproj"));
        var license = File.ReadAllText(System.IO.Path.Combine(root, "LICENSE"));

        Assert.Contains("<Authors>Cynrath</Authors>", projectFile);
        Assert.Contains("<Company>Cynrath</Company>", projectFile);
        Assert.Contains("Copyright (c) 2026 Cynrath", license);
    }

    private static (int ExitCode, string Output, string Error) RunCli(string workingDirectory, string[] args)
    {
        var originalDirectory = Directory.GetCurrentDirectory();
        var originalOut = Console.Out;
        var originalError = Console.Error;

        using var output = new StringWriter();
        using var error = new StringWriter();

        try
        {
            Directory.SetCurrentDirectory(workingDirectory);
            Console.SetOut(output);
            Console.SetError(error);
            var exitCode = AgentContextKit.Cli.Program.Main(args);
            return (exitCode, output.ToString(), error.ToString());
        }
        finally
        {
            Console.SetOut(originalOut);
            Console.SetError(originalError);
            Directory.SetCurrentDirectory(originalDirectory);
        }
    }

    private static string LocateRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(System.IO.Path.Combine(directory.FullName, "AgentContextKit.sln")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Repository root could not be located.");
    }
}

internal static class TestServices
{
    public static RepositoryScanner CreateRepositoryScanner()
    {
        var fileSystem = new PhysicalFileSystem();
        var secretScanner = new SecretScanner();
        var brandPiiScanner = new BrandPiiScanner();
        var riskScanner = new RiskScanner(fileSystem, secretScanner, brandPiiScanner);
        return new RepositoryScanner(fileSystem, new StackDetector(), riskScanner);
    }
}

internal sealed class FixedClock : IClock
{
    public DateTimeOffset UtcNow => new(2026, 6, 2, 0, 0, 0, TimeSpan.Zero);
}

internal sealed class TempRepository : IDisposable
{
    private TempRepository(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public static TempRepository Create()
    {
        var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "ackit-tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(path);
        return new TempRepository(path);
    }

    public void Write(string relativePath, string content)
    {
        var fullPath = System.IO.Path.Combine(Path, relativePath.Replace('/', System.IO.Path.DirectorySeparatorChar));
        var directory = System.IO.Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(fullPath, content);
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, recursive: true);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
