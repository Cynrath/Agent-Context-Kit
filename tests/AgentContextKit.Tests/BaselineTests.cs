using AgentContextKit.Core;

namespace AgentContextKit.Tests;

public sealed class BaselineTests
{
    [Fact]
    public void FingerprintIsDeterministicAcrossSeparatorAndDotSegmentVariants()
    {
        var location = new BaselineLocation(12, 4);

        var first = BaselineFingerprint.Create("ackit001", @"src\.\Config\settings.json", location);
        var second = BaselineFingerprint.Create("ACKIT001", "src/Config/settings.json", location);

        Assert.Equal(first, second);
        Assert.Matches("^[0-9a-f]{64}$", first);
    }

    [Fact]
    public void FingerprintPreservesRepositoryPathCase()
    {
        var lower = BaselineFingerprint.Create("ACKIT001", "src/config/settings.json");
        var upper = BaselineFingerprint.Create("ACKIT001", "src/Config/settings.json");

        Assert.NotEqual(lower, upper);
    }

    [Fact]
    public void FingerprintNormalizesUnicodeAndDoesNotDependOnSeverity()
    {
        var decomposed = BaselineFingerprint.Create("ACKIT002", "docs/cafe\u0301.md");
        var composedEntry = new BaselineEntry("ACKIT002", "docs/café.md", RiskSeverity.Medium);
        var changedSeverityEntry = new BaselineEntry("ACKIT002", "docs/café.md", RiskSeverity.High);

        Assert.Equal(decomposed, composedEntry.Fingerprint);
        Assert.Equal(composedEntry.Fingerprint, changedSeverityEntry.Fingerprint);
    }

    [Fact]
    public void FingerprintChangesForRulePathOrLocationChanges()
    {
        var baseline = BaselineFingerprint.Create("ACKIT001", "src/settings.json", new BaselineLocation(10, 2));

        Assert.NotEqual(baseline, BaselineFingerprint.Create("ACKIT002", "src/settings.json", new BaselineLocation(10, 2)));
        Assert.NotEqual(baseline, BaselineFingerprint.Create("ACKIT001", "src/other.json", new BaselineLocation(10, 2)));
        Assert.NotEqual(baseline, BaselineFingerprint.Create("ACKIT001", "src/settings.json", new BaselineLocation(11, 2)));
    }

    [Fact]
    public void BaselinePathsRejectUnsafeValues()
    {
        string[] paths =
        [
            "",
            "../settings.json",
            "src/../../settings.json",
            "/" + "home/example/settings.json",
            "C" + ":\\Users\\example\\settings.json",
            "file" + "://settings.json"
        ];

        foreach (var path in paths)
        {
            Assert.Throws<ArgumentException>(() => BaselineFingerprint.NormalizeRelativePath(path));
        }
    }

    [Fact]
    public void BaselineLocationRequiresPositiveLineAndColumn()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BaselineLocation(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new BaselineLocation(1, 0));
        Assert.Throws<ArgumentException>(() => new BaselineLocation(startColumn: 2));
    }

    [Fact]
    public void BaselineEntryContainsOnlySanitizedIdentityMetadata()
    {
        var entry = new BaselineEntry("ackit001", @"src\settings.json", RiskSeverity.Critical, new BaselineLocation(7, 3));

        Assert.Equal("ACKIT001", entry.RuleId);
        Assert.Equal("src/settings.json", entry.Path);
        Assert.Equal(RiskSeverity.Critical, entry.Severity);
        Assert.Equal(7, entry.StartLine);
        Assert.Equal(3, entry.StartColumn);
        Assert.Null(typeof(BaselineEntry).GetProperty("Match"));
        Assert.Null(typeof(BaselineEntry).GetProperty("Message"));
        Assert.Null(typeof(BaselineEntry).GetProperty("RepositoryPath"));
    }

    [Fact]
    public void BaselineManifestUsesIndependentSchemaAndDeterministicOrdering()
    {
        var manifest = new BaselineManifest(
        [
            new BaselineEntry("ACKIT003", "z/file.zip", RiskSeverity.Medium),
            new BaselineEntry("ACKIT001", "a/settings.json", RiskSeverity.Critical, new BaselineLocation(2)),
            new BaselineEntry("ACKIT001", "a/settings.json", RiskSeverity.Critical, new BaselineLocation(1))
        ]);

        Assert.Equal(BaselineSchema.CurrentVersion, manifest.SchemaVersion);
        Assert.Equal(BaselineSchema.FingerprintAlgorithm, manifest.FingerprintAlgorithm);
        Assert.Collection(
            manifest.Entries,
            entry => Assert.Equal(1, entry.StartLine),
            entry => Assert.Equal(2, entry.StartLine),
            entry => Assert.Equal("z/file.zip", entry.Path));
    }

    [Fact]
    public void BaselineManifestRejectsDuplicateFingerprints()
    {
        var entry = new BaselineEntry("ACKIT003", "artifacts/package.zip", RiskSeverity.Medium);

        Assert.Throws<ArgumentException>(() => new BaselineManifest([entry, entry]));
    }
}
