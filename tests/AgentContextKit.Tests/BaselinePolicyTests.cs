using System.Text.Json.Nodes;
using AgentContextKit.Core;

namespace AgentContextKit.Tests;

public sealed class BaselinePolicyTests
{
    [Fact]
    public void StoreRoundTripsDeterministicSanitizedManifest()
    {
        using var repo = TempRepository.Create();
        var store = new BaselineStore(new PhysicalFileSystem());
        var manifest = new BaselineManifest(
        [
            new BaselineEntry("ACKIT002", "docs/public.md", RiskSeverity.Medium, new BaselineLocation(occurrence: 1)),
            new BaselineEntry("ACKIT001", "config/settings.txt", RiskSeverity.Critical, new BaselineLocation(occurrence: 1))
        ]);

        var result = store.Write(repo.Path, ".ackit-baseline.json", manifest, update: false);
        var content = File.ReadAllText(Path.Combine(repo.Path, ".ackit-baseline.json"));
        var loaded = store.Load(repo.Path, ".ackit-baseline.json");

        Assert.Equal(BaselineFileStatus.Created, result.Status);
        Assert.Equal(2, result.EntryCount);
        Assert.Equal(manifest.Entries.Select(entry => entry.Fingerprint), loaded.Entries.Select(entry => entry.Fingerprint));
        Assert.DoesNotContain("repositoryPath", content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("message", content, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("match", content, StringComparison.OrdinalIgnoreCase);
        Assert.NotNull(JsonNode.Parse(content));
    }

    [Fact]
    public void StoreRequiresExplicitUpdateForExistingFile()
    {
        using var repo = TempRepository.Create();
        var store = new BaselineStore(new PhysicalFileSystem());
        var manifest = new BaselineManifest([]);
        store.Write(repo.Path, "baseline.json", manifest, update: false);

        var exception = Assert.Throws<BaselineException>(() => store.Write(repo.Path, "baseline.json", manifest, update: false));
        var updated = store.Write(repo.Path, "baseline.json", manifest, update: true);

        Assert.Equal("ACKITBASE003", exception.Code);
        Assert.Equal(BaselineFileStatus.Updated, updated.Status);
    }

    [Theory]
    [InlineData("missing.json", "ACKITBASE002")]
    [InlineData("../outside.json", "ACKITBASE001")]
    [InlineData("baseline.txt", "ACKITBASE001")]
    public void StoreRejectsMissingOrUnsafePaths(string path, string expectedCode)
    {
        using var repo = TempRepository.Create();
        var store = new BaselineStore(new PhysicalFileSystem());

        var exception = Assert.Throws<BaselineException>(() => store.Load(repo.Path, path));

        Assert.Equal(expectedCode, exception.Code);
    }

    [Fact]
    public void StoreRejectsInvalidJsonAndFingerprintTampering()
    {
        using var repo = TempRepository.Create();
        var store = new BaselineStore(new PhysicalFileSystem());
        repo.Write("invalid.json", "{");
        var invalid = Assert.Throws<BaselineException>(() => store.Load(repo.Path, "invalid.json"));

        var manifest = new BaselineManifest(
        [
            new BaselineEntry("ACKIT001", "settings.txt", RiskSeverity.Critical, new BaselineLocation(occurrence: 1))
        ]);
        store.Write(repo.Path, "tampered.json", manifest, update: false);
        var path = Path.Combine(repo.Path, "tampered.json");
        File.WriteAllText(path, File.ReadAllText(path).Replace(manifest.Entries[0].Fingerprint, new string('0', 64), StringComparison.Ordinal));
        var tampered = Assert.Throws<BaselineException>(() => store.Load(repo.Path, "tampered.json"));

        Assert.Equal("ACKITBASE004", invalid.Code);
        Assert.Equal("ACKITBASE006", tampered.Code);
    }

    [Fact]
    public void ClassifierUsesOccurrenceAndKeepsNewAndExistingFindings()
    {
        var classifier = new BaselineClassifier();
        RiskFinding[] original =
        [
            new(RiskSeverity.Critical, RiskCategory.Secret, "settings.txt", "First secret-like value", "redacted-one"),
            new(RiskSeverity.Critical, RiskCategory.Secret, "settings.txt", "Second secret-like value", "redacted-two")
        ];
        var manifest = classifier.CreateManifest(original);
        RiskFinding[] current =
        [
            .. original,
            new(RiskSeverity.High, RiskCategory.Secret, "other.txt", "New credential-like value", "redacted-three")
        ];

        var result = classifier.Classify(current, manifest);

        Assert.Equal(2, manifest.Entries.Count);
        Assert.Equal([1, 2], manifest.Entries.Select(entry => entry.Occurrence));
        Assert.Equal(2, result.Existing.Count);
        Assert.Single(result.New);
        Assert.Contains(result.Existing, finding => finding.Finding.Severity == RiskSeverity.Critical);
        Assert.Equal("other.txt", result.New[0].Finding.Path);
    }

    [Fact]
    public void EvaluationRejectsDifferentScanFindings()
    {
        var classifier = new BaselineClassifier();
        RiskFinding[] original =
        [
            new(RiskSeverity.High, RiskCategory.Secret, "settings.txt", "Credential assignment detected.")
        ];
        var evaluation = classifier.Classify(original, classifier.CreateManifest(original));
        RiskFinding[] different =
        [
            new(RiskSeverity.High, RiskCategory.Secret, "other.txt", "Credential assignment detected.")
        ];

        Assert.Throws<InvalidOperationException>(() => evaluation.ValidateAgainst(different));
    }
}
