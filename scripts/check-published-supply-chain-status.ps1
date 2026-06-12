param(
    [switch]$FailOnIssues
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$issues = New-Object System.Collections.Generic.List[string]
$warnings = New-Object System.Collections.Generic.List[string]
$notes = New-Object System.Collections.Generic.List[string]

function Add-Issue { param([string]$Message) $issues.Add($Message) | Out-Null }
function Add-Warning { param([string]$Message) $warnings.Add($Message) | Out-Null }
function Add-Note { param([string]$Message) $notes.Add($Message) | Out-Null }

function Read-RequiredFile {
    param([string]$RelativePath, [string]$Description)

    $path = Join-Path $repoRoot $RelativePath
    if (-not (Test-Path $path)) {
        Add-Issue "$Description missing: $RelativePath"
        return ""
    }

    Add-Note "$Description present: $RelativePath"
    return Get-Content -Raw $path
}

function Require-Text {
    param([string]$Content, [string]$Marker, [string]$Description)

    if ($Content.IndexOf($Marker, [StringComparison]::Ordinal) -ge 0) {
        Add-Note "$Description present."
    }
    else {
        Add-Issue "$Description missing."
    }
}

Write-Host "AgentContextKit published supply-chain status review"
Write-Host "Repository: $repoRoot"

$status = Read-RequiredFile "docs\PUBLISHED_SUPPLY_CHAIN_STATUS.md" "Published supply-chain status"
$task = Read-RequiredFile "docs\tasks\TASK-0099-published-supply-chain-status-audit.md" "TASK-0099"
$evidence = Read-RequiredFile "docs\SECURITY_SUPPLY_CHAIN_EVIDENCE.md" "Security/supply-chain evidence register"
$policy = Read-RequiredFile "docs\SUPPLY_CHAIN_POLICY.md" "Supply-chain policy"

foreach ($marker in @(
    'NuGet package `AgentContextKit` `0.2.0-alpha.1`',
    "d6d0804f5dfca7c03f2b8ea173de74e3b0e9e8eb73ceec5dda9ac18cae1988ce",
    'Signature type: `Repository`',
    "No author signature was observed",
    'Public owner profile `Cyranth`; project persona/author `Cynrath`',
    "no manually uploaded assets",
    "no accessible GitHub SLSA provenance attestation",
    "did not sign, publish, attest, upload"
)) {
    Require-Text $status $marker "Published-state marker $marker"
}

Require-Text $task "No signing, certificate handling" "Task remote-write boundary"
Require-Text $evidence "VERIFIED PUBLISHED STATE" "Published-state evidence vocabulary"
Require-Text $evidence "NuGet owner identity" "NuGet owner identity evidence row"
Require-Text $evidence "Repository signature; no author signature observed" "Package signature evidence row"
Require-Text $evidence "Not present in package or GitHub Release assets" "SBOM published-state evidence"
Require-Text $evidence "No accessible GitHub attestation for package digest" "Provenance published-state evidence"
Require-Text $policy "repository-signed by NuGet.org" "Repository-signing truth boundary"
Require-Text $policy "must not be described as author-signed" "Author-signing truth boundary"

if (Get-Command git -ErrorAction SilentlyContinue) {
    Push-Location $repoRoot
    try {
        $workingTree = git status --short 2>$null
        if ($LASTEXITCODE -eq 0 -and $workingTree) {
            Add-Warning "Working tree has uncommitted changes."
        }
    }
    finally {
        Pop-Location
    }
}

Write-Host ""
if ($issues.Count -eq 0) {
    Write-Host "No published supply-chain status issues detected."
}
else {
    Write-Host "Published supply-chain status issues:"
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
Write-Host "This gate validates committed evidence structure only. It does not download, sign, publish, attest, upload, edit releases, modify package ownership, or perform package recovery actions."

if ($FailOnIssues -and $issues.Count -gt 0) {
    exit 1
}

exit 0
