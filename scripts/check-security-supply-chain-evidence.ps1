param(
    [switch]$FailOnIssues,
    [switch]$RunDependencyReview
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

Write-Host "AgentContextKit security and supply-chain evidence review"
Write-Host "Repository: $repoRoot"

$evidence = Read-RequiredFile "docs\SECURITY_SUPPLY_CHAIN_EVIDENCE.md" "Security/supply-chain evidence register"
$handoff = Read-RequiredFile "docs\MAINTAINER_SECURITY_SUPPLY_CHAIN_HANDOFF.md" "Maintainer security/supply-chain handoff"
$securityPolicy = Read-RequiredFile "SECURITY.md" "Public security policy"
$responseReadiness = Read-RequiredFile "docs\SECURITY_RESPONSE_READINESS.md" "Security response readiness"
$supplyChain = Read-RequiredFile "docs\SUPPLY_CHAIN_POLICY.md" "Supply-chain policy"
$maintainerDecision = Read-RequiredFile "docs\MAINTAINER_RC_DECISION.md" "Maintainer RC decision"
$task = Read-RequiredFile "docs\tasks\TASK-0095-security-supply-chain-maintainer-evidence.md" "TASK-0095"
$privateReportingStatus = Read-RequiredFile "docs\PRIVATE_VULNERABILITY_REPORTING_STATUS.md" "Private vulnerability reporting status"
$privateReportingTask = Read-RequiredFile "docs\tasks\TASK-0098-private-vulnerability-reporting-status.md" "TASK-0098"

foreach ($marker in @(
    "Local evidence register prepared on 2026-06-12",
    "PENDING MAINTAINER",
    "VERIFIED LOCAL",
    "ACCEPTED RISK",
    "VERIFIED REMOTE STATE",
    "Private vulnerability reporting | VERIFIED REMOTE STATE: DISABLED on 2026-06-13",
    "NuGet author signing | PENDING MAINTAINER",
    "SBOM | PENDING MAINTAINER",
    "Build/package provenance | PENDING MAINTAINER",
    "Candidate commit:",
    "Decision date:",
    "Maintainer: Cynrath",
    "Next review date:"
)) {
    Require-Text $evidence $marker "Evidence register marker $marker"
}

foreach ($marker in @(
    "Private Vulnerability Reporting",
    "Final-Candidate Dependency Review",
    "NuGet Signing Decision",
    "SBOM Decision",
    "Provenance Decision",
    "Bad-Package Recovery Acceptance",
    "scripts/check-security-supply-chain-evidence.ps1"
)) {
    Require-Text $handoff $marker "Maintainer handoff section $marker"
}

Require-Text $securityPolicy "must be enabled and verified by the maintainer" "Public security policy private-reporting blocker"
Require-Text $responseReadiness "private vulnerability reporting channel must be enabled and verified" "Security response maintainer blocker"
Require-Text $supplyChain "current pre-release package is not documented as signed" "Unsigned-package truth boundary"
Require-Text $supplyChain "no SBOM/provenance artifact is published" "Unpublished SBOM/provenance truth boundary"
Require-Text $maintainerDecision "NO-GO for release-candidate publication" "Maintainer NO-GO decision"
Require-Text $task "does not approve an RC" "Task non-approval boundary"
Require-Text $privateReportingStatus 'Result: `enabled: false`' "Verified disabled private-reporting state"
Require-Text $privateReportingStatus "P0 blocker remains open" "Private-reporting P0 blocker"
Require-Text $privateReportingStatus "repos/Cynrath/agent-context-kit/private-vulnerability-reporting" "Private-reporting read-only endpoint"
Require-Text $privateReportingTask "No GitHub setting change" "Private-reporting task remote-write boundary"

if ($RunDependencyReview) {
    Push-Location $repoRoot
    try {
        $vulnerableOutput = & dotnet list AgentContextKit.sln package --vulnerable --include-transitive 2>&1
        if ($LASTEXITCODE -eq 0) {
            Add-Note "Dependency vulnerability review command completed successfully."
        }
        else {
            $vulnerableOutput | Write-Host
            Add-Issue "Dependency vulnerability review command failed or package sources were unavailable."
        }

        $deprecatedOutput = & dotnet list AgentContextKit.sln package --deprecated --include-transitive 2>&1
        if ($LASTEXITCODE -eq 0) {
            Add-Note "Dependency deprecation review command completed successfully."
        }
        else {
            $deprecatedOutput | Write-Host
            Add-Issue "Dependency deprecation review command failed or package sources were unavailable."
        }
    }
    finally {
        Pop-Location
    }
}
else {
    Add-Warning "Dependency review commands were not run by this gate invocation."
}

if (Get-Command git -ErrorAction SilentlyContinue) {
    Push-Location $repoRoot
    try {
        $status = git status --short 2>$null
        if ($LASTEXITCODE -eq 0 -and $status) {
            Add-Warning "Working tree has uncommitted changes."
        }
    }
    finally {
        Pop-Location
    }
}

Write-Host ""
if ($issues.Count -eq 0) {
    Write-Host "No security/supply-chain evidence structure issues detected."
}
else {
    Write-Host "Security/supply-chain evidence structure issues:"
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
Write-Host "This gate validates local evidence structure only. It does not verify or change GitHub settings, handle certificates, sign packages, generate/upload SBOM or provenance, push, tag, release, or publish."

if ($FailOnIssues -and $issues.Count -gt 0) {
    exit 1
}

exit 0
