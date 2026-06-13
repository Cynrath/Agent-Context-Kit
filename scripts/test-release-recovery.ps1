param()

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$verifier = Join-Path $PSScriptRoot "verify-existing-release.ps1"
$pwsh = (Get-Command pwsh -ErrorAction Stop).Source
$version = "0.2.0-alpha.2"
$automationSha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
$releaseSha = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb"
$tempRoot = Join-Path ([System.IO.Path]::GetTempPath()) ("ackit-release-recovery-tests-" + [Guid]::NewGuid().ToString("N"))

function New-Evidence {
    [ordered]@{
        schemaVersion = 1
        verifiedAtUtc = "2026-06-13T00:00:00.0000000Z"
        repository = "Cynrath/agent-context-kit"
        version = $version
        automationCommitSha = $automationSha
        originMasterSha = $automationSha
        releaseCommitSha = $releaseSha
        tagCommitSha = $releaseSha
        releaseTag = "v$version"
        releaseTargetCommitish = $releaseSha
        isPrerelease = $true
        packageAccessible = $true
        installedToolVerified = $true
        packageId = "AgentContextKit"
        packageVersion = $version
        authors = "Cynrath"
        repositoryUrl = "https://github.com/Cynrath/agent-context-kit"
        repositoryCommit = $releaseSha
        projectUrl = "https://github.com/Cynrath/agent-context-kit"
        license = "MIT"
        releaseAssetNames = @("AgentContextKit.$version.nupkg", "AgentContextKit.$version.snupkg")
        hashes = [ordered]@{
            nugetNupkg = "a" * 64
            releaseNupkg = "b" * 64
            releaseSnupkg = "c" * 64
        }
    }
}

function Write-Fixture {
    param([string]$Name, [object]$Evidence)
    $path = Join-Path $tempRoot "$Name.json"
    [System.IO.File]::WriteAllText($path, ($Evidence | ConvertTo-Json -Depth 8), [System.Text.UTF8Encoding]::new($false))
    return $path
}

function Invoke-Fixture {
    param([string]$Path, [bool]$ShouldPass)
    $stdoutPath = Join-Path $tempRoot ([System.IO.Path]::GetRandomFileName())
    $stderrPath = Join-Path $tempRoot ([System.IO.Path]::GetRandomFileName())
    $process = Start-Process -FilePath $pwsh -ArgumentList @(
        "-NoProfile",
        "-File", $verifier,
        "-Version", $version,
        "-AutomationCommitSha", $automationSha,
        "-ReleaseCommitSha", $releaseSha,
        "-Prerelease", "true",
        "-EvidencePath", $Path
    ) -Wait -PassThru -NoNewWindow -RedirectStandardOutput $stdoutPath -RedirectStandardError $stderrPath
    $passed = $process.ExitCode -eq 0
    if ($passed -ne $ShouldPass) {
        $details = @(
            (Get-Content -LiteralPath $stdoutPath -Raw -ErrorAction SilentlyContinue),
            (Get-Content -LiteralPath $stderrPath -Raw -ErrorAction SilentlyContinue)
        ) -join [Environment]::NewLine
        throw "Fixture result mismatch for $Path. Expected pass: $ShouldPass; exit code: $($process.ExitCode). $details"
    }
}

try {
    New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null

    $validPath = Write-Fixture -Name "valid" -Evidence (New-Evidence)
    Invoke-Fixture -Path $validPath -ShouldPass $true
    Invoke-Fixture -Path $validPath -ShouldPass $true

    $wrongTag = New-Evidence
    $wrongTag.tagCommitSha = "c" * 40
    Invoke-Fixture -Path (Write-Fixture -Name "wrong-tag" -Evidence $wrongTag) -ShouldPass $false

    $missingAsset = New-Evidence
    $missingAsset.releaseAssetNames = @("AgentContextKit.$version.nupkg")
    Invoke-Fixture -Path (Write-Fixture -Name "missing-asset" -Evidence $missingAsset) -ShouldPass $false

    $unverifiedTool = New-Evidence
    $unverifiedTool.installedToolVerified = $false
    Invoke-Fixture -Path (Write-Fixture -Name "unverified-tool" -Evidence $unverifiedTool) -ShouldPass $false

    Write-Host "Release recovery positive, negative, and idempotency tests passed."
}
finally {
    if (Test-Path $tempRoot) { Remove-Item -LiteralPath $tempRoot -Recurse -Force }
}
