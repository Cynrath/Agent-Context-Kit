param(
    [switch]$FailOnIssues
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$failures = New-Object System.Collections.Generic.List[string]

function Add-Failure {
    param([string]$Message)
    $failures.Add($Message) | Out-Null
}

function Invoke-Gate {
    param(
        [string]$Name,
        [string]$ScriptName,
        [string]$FailFlag
    )

    $scriptPath = Join-Path $PSScriptRoot $ScriptName
    Write-Host ""
    Write-Host "==> $Name"

    if (-not (Test-Path $scriptPath)) {
        Add-Failure "$Name script was not found: $scriptPath"
        return
    }

    $arguments = @("-NoProfile", "-ExecutionPolicy", "Bypass", "-File", $scriptPath)
    if ($FailOnIssues) {
        $arguments += $FailFlag
    }

    & powershell @arguments
    $exitCode = $LASTEXITCODE

    if ($exitCode -ne 0) {
        Add-Failure "$Name exited with code $exitCode."
    }
}

Write-Host "AgentContextKit public release gates"
Write-Host "Repository: $repoRoot"
Write-Host "Mode: $(if ($FailOnIssues) { "failing gate" } else { "report-only" })"

Invoke-Gate -Name "Package metadata gate" -ScriptName "check-package-metadata.ps1" -FailFlag "-FailOnIssues"
Invoke-Gate -Name "Public release audit gate" -ScriptName "audit-public-release.ps1" -FailFlag "-FailOnIssues"
Invoke-Gate -Name "Release blocker gate" -ScriptName "check-release-blockers.ps1" -FailFlag "-FailOnBlockers"

Write-Host ""
if ($failures.Count -eq 0) {
    Write-Host "Public release gate orchestration completed without script failures."
}
else {
    Write-Host "Public release gate failures:"
    foreach ($failure in $failures) {
        Write-Host "- $failure"
    }
}

Write-Host ""
Write-Host "This gate is local-only. It does not push, publish, tag, redact, delete, mutate files, or create remotes."

if ($FailOnIssues -and $failures.Count -gt 0) {
    Write-Host ""
    Write-Host "Public release gates failed."
    exit 1
}

if (-not $FailOnIssues -and $failures.Count -gt 0) {
    exit 1
}

exit 0
