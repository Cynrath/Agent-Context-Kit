param(
    [switch]$FailOnIssues,
    [switch]$SkipBenchmark
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$solution = Join-Path $repoRoot "AgentContextKit.sln"
$issues = New-Object System.Collections.Generic.List[string]
$warnings = New-Object System.Collections.Generic.List[string]
$notes = New-Object System.Collections.Generic.List[string]

function Add-Issue { param([string]$Message) $issues.Add($Message) | Out-Null }
function Add-Warning { param([string]$Message) $warnings.Add($Message) | Out-Null }
function Add-Note { param([string]$Message) $notes.Add($Message) | Out-Null }

function Require-Path {
    param([string]$RelativePath, [string]$Description)
    if (Test-Path (Join-Path $repoRoot $RelativePath)) {
        Add-Note "$Description present: $RelativePath"
    }
    else {
        Add-Issue "$Description missing: $RelativePath"
    }
}

function Require-Text {
    param([string]$RelativePath, [string]$Needle, [string]$Description)
    $path = Join-Path $repoRoot $RelativePath
    if (-not (Test-Path $path)) {
        Add-Issue "$Description file missing: $RelativePath"
        return
    }

    $content = Get-Content -Raw $path
    if ($content.Contains($Needle)) {
        Add-Note "$Description present."
    }
    else {
        Add-Issue "$Description missing."
    }
}

Write-Host "AgentContextKit release-candidate evidence review"
Write-Host "Repository: $repoRoot"

$requiredPaths = @(
    @{ Path = "docs\UPGRADE_COMPATIBILITY.md"; Description = "Upgrade compatibility policy" },
    @{ Path = "docs\PERFORMANCE_POLICY.md"; Description = "Performance policy" },
    @{ Path = "docs\SECURITY_RESPONSE_READINESS.md"; Description = "Security response readiness" },
    @{ Path = "docs\SUPPORT_LIFECYCLE.md"; Description = "Support lifecycle" },
    @{ Path = "docs\SUPPLY_CHAIN_POLICY.md"; Description = "Supply-chain policy" },
    @{ Path = "docs\RELEASE_CANDIDATE_EVIDENCE.md"; Description = "Release-candidate evidence matrix" },
    @{ Path = "tests\fixtures\upgrade\v0.2.0-alpha.1-config.yml"; Description = "Published config fixture" },
    @{ Path = "tests\fixtures\upgrade\baseline-schema-v1.json"; Description = "Baseline schema fixture" },
    @{ Path = "tests\AgentContextKit.Tests\ReleaseCandidateEvidenceTests.cs"; Description = "Compatibility tests" },
    @{ Path = "scripts\measure-scan-performance.ps1"; Description = "Synthetic scan benchmark" }
)

foreach ($entry in $requiredPaths) {
    Require-Path -RelativePath $entry.Path -Description $entry.Description
}

Require-Text -RelativePath "docs\UPGRADE_COMPATIBILITY.md" -Needle "0.2.0-alpha.1" -Description "Supported predecessor"
Require-Text -RelativePath "docs\UPGRADE_COMPATIBILITY.md" -Needle "Rollback" -Description "Upgrade rollback guidance"
Require-Text -RelativePath "docs\PERFORMANCE_POLICY.md" -Needle "not a production SLA" -Description "Performance non-SLA boundary"
Require-Text -RelativePath "docs\SECURITY_RESPONSE_READINESS.md" -Needle "private vulnerability reporting" -Description "Private reporting blocker"
Require-Text -RelativePath "docs\SUPPORT_LIFECYCLE.md" -Needle ".NET 10" -Description "Runtime lifecycle baseline"
Require-Text -RelativePath "docs\SUPPLY_CHAIN_POLICY.md" -Needle "Signing, SBOM, And Provenance Decision" -Description "Supply-chain decision boundary"
Require-Text -RelativePath "docs\RELEASE_CANDIDATE_EVIDENCE.md" -Needle "Do not call the project 1.0 RC-ready" -Description "RC decision rule"

Push-Location $repoRoot
try {
    $testOutput = & dotnet test $solution -c Release --no-build --filter "FullyQualifiedName~ReleaseCandidateEvidenceTests" 2>&1
    if ($LASTEXITCODE -eq 0) {
        Add-Note "Release-candidate compatibility tests passed."
    }
    else {
        $testOutput | Write-Host
        Add-Issue "Release-candidate compatibility tests failed."
    }

    if (-not $SkipBenchmark) {
        & powershell -NoProfile -ExecutionPolicy Bypass -File (Join-Path $PSScriptRoot "measure-scan-performance.ps1") -FileCount 2000 -MaxSeconds 30 -FailOnThreshold
        if ($LASTEXITCODE -eq 0) {
            Add-Note "Synthetic scan benchmark passed."
        }
        else {
            Add-Issue "Synthetic scan benchmark failed."
        }
    }
    else {
        Add-Warning "Synthetic scan benchmark was skipped."
    }

    if (Get-Command git -ErrorAction SilentlyContinue) {
        $status = git status --short 2>$null
        if ($LASTEXITCODE -eq 0 -and $status) {
            Add-Warning "Working tree has uncommitted changes."
        }
    }
}
finally {
    Pop-Location
}

Write-Host ""
if ($issues.Count -eq 0) {
    Write-Host "No local release-candidate evidence issues detected."
}
else {
    Write-Host "Release-candidate evidence issues:"
    foreach ($issue in $issues) { Write-Host "- $issue" }
}

if ($warnings.Count -gt 0) {
    Write-Host ""
    Write-Host "Warnings:"
    foreach ($warning in $warnings) { Write-Host "- $warning" }
}

if ($notes.Count -gt 0) {
    Write-Host ""
    Write-Host "Notes:"
    foreach ($note in $notes) { Write-Host "- $note" }
}

Write-Host ""
Write-Host "This review is local-only. Hosted workflows, security settings, tags, releases, signing, provenance, SBOM publication, and NuGet publish remain maintainer-only."

if ($FailOnIssues -and $issues.Count -gt 0) {
    exit 1
}

exit 0
