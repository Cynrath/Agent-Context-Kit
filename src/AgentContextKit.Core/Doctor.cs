namespace AgentContextKit.Core;

public sealed class RepositoryDoctor
{
    private readonly IFileSystem _fileSystem;

    public RepositoryDoctor(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public DoctorResult Check(string repositoryPath, ScanResult scanResult)
    {
        var checks = new List<DoctorCheck>
        {
            FileCheck("README", scanResult.HasReadme, RiskSeverity.High, "README.md or README.tr.md should exist."),
            FileCheck("LICENSE", scanResult.HasLicense, RiskSeverity.High, "LICENSE should exist."),
            FileCheck("SECURITY", scanResult.HasSecurityPolicy, RiskSeverity.High, "SECURITY.md should exist."),
            FileCheck("CONTRIBUTING", scanResult.HasContributing, RiskSeverity.Medium, "CONTRIBUTING.md should exist."),
            FileCheck("CODE_OF_CONDUCT", scanResult.HasCodeOfConduct, RiskSeverity.Medium, "CODE_OF_CONDUCT.md should exist."),
            FileCheck("CHANGELOG", scanResult.HasChangelog, RiskSeverity.Medium, "CHANGELOG.md should exist."),
            FileCheck("Tests", scanResult.HasTests, RiskSeverity.High, "A tests project or tests directory should exist."),
            FileCheck("CI", scanResult.HasCi, RiskSeverity.Medium, ".github/workflows should exist."),
            FileCheck("AGENTS", scanResult.HasAgentInstructions, RiskSeverity.Medium, "At least one AI instruction file should exist."),
            FileCheck("GitIgnore", _fileSystem.FileExists(Path.Combine(repositoryPath, ".gitignore")), RiskSeverity.High, ".gitignore should exist."),
            FileCheck("NuGetToolMetadata", HasToolMetadata(repositoryPath), RiskSeverity.Medium, "CLI project should include PackAsTool and ToolCommandName metadata.")
        };

        var buildArtifactDirectories = new[] { "bin", "obj", "TestResults", "coverage", "node_modules" }
            .Where(directory => _fileSystem.DirectoryExists(Path.Combine(repositoryPath, directory)))
            .ToArray();

        checks.Add(new DoctorCheck(
            "BuildArtifacts",
            RiskSeverity.Low,
            buildArtifactDirectories.Length == 0,
            buildArtifactDirectories.Length == 0
                ? "No top-level build artifact directories detected."
                : "Top-level build artifact directories detected: " + string.Join(", ", buildArtifactDirectories)));

        var criticalRisk = scanResult.Findings.Any(finding => finding.Severity == RiskSeverity.Critical);
        checks.Add(new DoctorCheck(
            "CriticalRedactRisk",
            RiskSeverity.Critical,
            !criticalRisk,
            criticalRisk ? "Critical redact-check findings are present." : "No critical redact-check findings detected."));

        return new DoctorResult(checks);
    }

    private static DoctorCheck FileCheck(string name, bool passed, RiskSeverity severity, string missingMessage)
    {
        return new DoctorCheck(name, severity, passed, passed ? $"{name} check passed." : missingMessage);
    }

    private bool HasToolMetadata(string repositoryPath)
    {
        var path = Path.Combine(repositoryPath, "src", "AgentContextKit.Cli", "AgentContextKit.Cli.csproj");
        if (!_fileSystem.FileExists(path))
        {
            return false;
        }

        var content = _fileSystem.ReadAllText(path);
        return content.Contains("<PackAsTool>true</PackAsTool>", StringComparison.OrdinalIgnoreCase) &&
               content.Contains("<ToolCommandName>ackit</ToolCommandName>", StringComparison.OrdinalIgnoreCase);
    }
}
