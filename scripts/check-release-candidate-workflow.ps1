param(
    [switch]$FailOnIssues
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$workflowPath = Join-Path $repoRoot ".github\workflows\release-candidate-evidence.yml"
$issues = New-Object System.Collections.Generic.List[string]
$notes = New-Object System.Collections.Generic.List[string]

function Add-Issue { param([string]$Message) $issues.Add($Message) | Out-Null }
function Add-Note { param([string]$Message) $notes.Add($Message) | Out-Null }

Write-Host "AgentContextKit release-candidate workflow review"
Write-Host "Repository: $repoRoot"

if (-not (Test-Path $workflowPath)) {
    Add-Issue "Workflow is missing: .github/workflows/release-candidate-evidence.yml"
}
else {
    $content = Get-Content -Raw $workflowPath
    $required = @(
        "workflow_dispatch:",
        "contents: read",
        "windows-2025",
        "ubuntu-latest",
        "macos-latest",
        "PREDECESSOR_VERSION: 0.2.0-alpha.1",
        "CANDIDATE_PACKAGE_VERSION",
        "config-check --json",
        "baseline --output .ackit-baseline.json",
        "scan --baseline .ackit-baseline.json --ci --json",
        "measure-scan-performance.ps1",
        "Get-FileHash",
        "Artifact upload: disabled",
        "SARIF upload: disabled"
    )

    foreach ($needle in $required) {
        if ($content.Contains($needle)) {
            Add-Note "Required workflow marker present: $needle"
        }
        else {
            Add-Issue "Required workflow marker missing: $needle"
        }
    }

    $forbiddenPatterns = @(
        "(?m)^\s+push:\s*$",
        "(?m)^\s+pull_request:\s*$",
        "upload-artifact",
        "upload-sarif",
        "security-events:\s*write",
        "secrets\."
    )

    foreach ($pattern in $forbiddenPatterns) {
        if ($content -match $pattern) {
            Add-Issue "Forbidden workflow pattern found: $pattern"
        }
    }
}

Write-Host ""
if ($issues.Count -eq 0) {
    Write-Host "No release-candidate workflow issues detected."
}
else {
    Write-Host "Release-candidate workflow issues:"
    foreach ($issue in $issues) { Write-Host "- $issue" }
}

if ($notes.Count -gt 0) {
    Write-Host ""
    Write-Host "Notes:"
    foreach ($note in $notes) { Write-Host "- $note" }
}

Write-Host ""
Write-Host "This gate is static and local-only. A maintainer push and manual GitHub Actions dispatch are required for hosted evidence."

if ($FailOnIssues -and $issues.Count -gt 0) {
    exit 1
}

exit 0
