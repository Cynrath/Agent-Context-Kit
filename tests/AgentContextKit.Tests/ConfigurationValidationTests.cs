using AgentContextKit.Core;

namespace AgentContextKit.Tests;

public sealed class ConfigurationValidationTests
{
    private readonly AckitConfigValidator _validator = new();

    [Fact]
    public void ValidConfigurationHasNoDiagnostics()
    {
        var result = _validator.Validate("""
        schemaVersion: 1
        defaultLanguage: tr
        brandKeywords: ["ExampleBrand"]
        piiKeywords: []
        ignorePaths:
          - .ackit/reports/
        riskExtensions: [.bak, dump]
        safeDomains:
          - docs.example.invalid
          - "*.trusted.example"
        ignoredPaths: [fixtures/placeholders/]
        ignoredFindingIds: [ACKIT002, ACKIT003]
        """);

        Assert.Empty(result.Diagnostics);
        Assert.False(result.HasErrors);
    }

    [Fact]
    public void UnknownAndObsoleteKeysAreDistinctWarnings()
    {
        var result = _validator.Validate("""
        futureSetting: true
        allowedFindingIds: [ACKIT003]
        """);

        Assert.Contains(result.Diagnostics, diagnostic =>
            diagnostic.Code == ConfigDiagnosticCodes.UnknownKey &&
            diagnostic.Severity == ConfigDiagnosticSeverity.Warning &&
            diagnostic.Line == 1 &&
            diagnostic.Key == "futureSetting");
        Assert.Contains(result.Diagnostics, diagnostic =>
            diagnostic.Code == ConfigDiagnosticCodes.ObsoleteKey &&
            diagnostic.Severity == ConfigDiagnosticSeverity.Warning &&
            diagnostic.Line == 2 &&
            diagnostic.Key == "allowedFindingIds");
    }

    [Fact]
    public void DuplicateCanonicalAndObsoleteKeysAreErrors()
    {
        var result = _validator.Validate("""
        defaultLanguage: en
        defaultLanguage: tr
        ignoredFindingIds: [ACKIT003]
        allowedFindingIds: [ACKIT002]
        """);

        Assert.Equal(2, result.Diagnostics.Count(diagnostic => diagnostic.Code == ConfigDiagnosticCodes.DuplicateKey));
        Assert.True(result.HasErrors);
    }

    [Fact]
    public void ScalarAndListSyntaxDiagnosticsAreDeterministic()
    {
        var result = _validator.Validate("""
        schemaVersion: 2
        defaultLanguage: de
        brandKeywords: example
        - orphan
        invalid line
        """);

        Assert.Collection(
            result.Diagnostics,
            diagnostic => Assert.Equal(ConfigDiagnosticCodes.InvalidSchemaVersion, diagnostic.Code),
            diagnostic => Assert.Equal(ConfigDiagnosticCodes.InvalidLanguage, diagnostic.Code),
            diagnostic => Assert.Equal(ConfigDiagnosticCodes.MalformedValue, diagnostic.Code),
            diagnostic => Assert.Equal(ConfigDiagnosticCodes.OrphanListItem, diagnostic.Code),
            diagnostic => Assert.Equal(ConfigDiagnosticCodes.MalformedValue, diagnostic.Code));
        Assert.Equal([1, 2, 3, 4, 5], result.Diagnostics.Select(diagnostic => diagnostic.Line));
    }

    [Fact]
    public void UnsafePathsDomainsExtensionsAndFindingIdsAreErrors()
    {
        var result = _validator.Validate(string.Join('\n',
            "schemaVersion: 1",
            "ignorePaths:",
            "  - " + "../private",
            "riskExtensions: [bad/ext]",
            "safeDomains: [https" + "://example.invalid/path]",
            "ignoredFindingIds: [ACKIT001, UNKNOWN001]"));

        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Code == ConfigDiagnosticCodes.InvalidPath);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Code == ConfigDiagnosticCodes.InvalidExtension);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Code == ConfigDiagnosticCodes.InvalidDomain);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Code == ConfigDiagnosticCodes.CriticalSuppression);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Code == ConfigDiagnosticCodes.InvalidFindingId);
        Assert.True(result.HasErrors);
    }

    [Fact]
    public void BroadIgnoreAndDuplicateListValuesAreWarnings()
    {
        var result = _validator.Validate("""
        ignorePaths: [src/, src/]
        """);

        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Code == ConfigDiagnosticCodes.BroadIgnorePath);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Code == ConfigDiagnosticCodes.DuplicateListValue);
        Assert.False(result.HasErrors);
    }

    [Fact]
    public void DiagnosticsDoNotEchoConfigurationValues()
    {
        const string sensitiveValue = "sensitive-value-not-for-output";

        var result = _validator.Validate($"safeDomains: [{sensitiveValue}/path]");
        var rendered = string.Join('\n', result.Diagnostics.Select(diagnostic => diagnostic.Message));

        Assert.DoesNotContain(sensitiveValue, rendered, StringComparison.Ordinal);
    }

    [Fact]
    public void ValidationDoesNotChangeCurrentReaderFallbackBehavior()
    {
        using var repo = TempRepository.Create();
        repo.Write(".ackit/config.yml", "defaultLanguage: de\nfutureSetting: true");
        var reader = new AckitConfigReader(new PhysicalFileSystem());

        var diagnostics = _validator.Validate(File.ReadAllText(Path.Combine(repo.Path, ".ackit", "config.yml")));
        var config = reader.Read(repo.Path);

        Assert.True(diagnostics.HasErrors);
        Assert.Equal("en", config.DefaultLanguage.Value);
    }
}
