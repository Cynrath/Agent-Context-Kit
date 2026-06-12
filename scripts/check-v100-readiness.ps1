param(
    [switch]$FailOnIssues
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$projectPath = Join-Path $repoRoot "src\AgentContextKit.Cli\AgentContextKit.Cli.csproj"
$issues = New-Object System.Collections.Generic.List[string]
$publicBlockers = New-Object System.Collections.Generic.List[string]
$warnings = New-Object System.Collections.Generic.List[string]
$notes = New-Object System.Collections.Generic.List[string]

function Add-Issue {
    param([string]$Message)
    $issues.Add($Message) | Out-Null
}

function Add-PublicBlocker {
    param([string]$Message)
    $publicBlockers.Add($Message) | Out-Null
}

function Add-Warning {
    param([string]$Message)
    $warnings.Add($Message) | Out-Null
}

function Add-Note {
    param([string]$Message)
    $notes.Add($Message) | Out-Null
}

function Get-MetadataValue {
    param(
        [xml]$Project,
        [string]$Name
    )

    foreach ($group in $Project.Project.PropertyGroup) {
        $value = $group.$Name
        if ($null -ne $value -and -not [string]::IsNullOrWhiteSpace([string]$value)) {
            return [string]$value
        }
    }

    return $null
}

function Require-Path {
    param(
        [string]$RelativePath,
        [string]$Description
    )

    $path = Join-Path $repoRoot $RelativePath
    if (Test-Path $path) {
        Add-Note "$Description present: $RelativePath"
    }
    else {
        Add-Issue "$Description missing: $RelativePath"
    }
}

function Get-FileText {
    param([string]$RelativePath)

    $path = Join-Path $repoRoot $RelativePath
    if (-not (Test-Path $path)) {
        Add-Issue "Required file missing: $RelativePath"
        return ""
    }

    return Get-Content -Raw $path
}

function Require-Text {
    param(
        [string]$Content,
        [string]$Needle,
        [string]$Description
    )

    if ($Content.Contains($Needle)) {
        Add-Note "$Description present."
    }
    else {
        Add-Issue "$Description missing."
    }
}

Write-Host "AgentContextKit historical v1.0 asset readiness review"
Write-Host "Repository: $repoRoot"

$requiredPaths = @(
    @{ Path = "docs\tasks\TASK-0035-v100-stabilization-plan.md"; Description = "v1.0 stabilization task" },
    @{ Path = "docs\tasks\TASK-0036-v100-stable-cli-contract-review.md"; Description = "v1.0 CLI contract task" },
    @{ Path = "docs\tasks\TASK-0037-v100-config-generated-convention-freeze.md"; Description = "v1.0 config/generated convention task" },
    @{ Path = "docs\tasks\TASK-0038-v100-documentation-release-gate-freeze.md"; Description = "v1.0 documentation/release gate task" },
    @{ Path = "docs\tasks\TASK-0039-v100-final-local-readiness-consolidation.md"; Description = "v1.0 final readiness task" },
    @{ Path = "docs\V100_STABILIZATION_PLAN.md"; Description = "v1.0 stabilization plan" },
    @{ Path = "docs\CLI_CONTRACT.md"; Description = "Stable CLI contract" },
    @{ Path = "docs\CONFIG_GENERATED_CONVENTIONS.md"; Description = "Config/generated conventions" },
    @{ Path = "docs\V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md"; Description = "v1.0 documentation/release gate freeze" },
    @{ Path = "docs\V100_READINESS.md"; Description = "v1.0 final readiness docs" },
    @{ Path = "docs\V100_GAP_ANALYSIS.md"; Description = "current v1.0 gap analysis" },
    @{ Path = "docs\RELEASE_VALIDATION.md"; Description = "Release validation docs" },
    @{ Path = "docs\DOCUMENTATION_INDEX.md"; Description = "Documentation index" },
    @{ Path = "docs\ROADMAP.md"; Description = "Roadmap" },
    @{ Path = "docs\PROJECT_MAP.md"; Description = "Project map" },
    @{ Path = "docs\RELEASE_BLOCKERS.md"; Description = "Release blockers" },
    @{ Path = "docs\PUBLIC_RELEASE_AUDIT.md"; Description = "Public release audit" },
    @{ Path = "docs\PUBLIC_RELEASE_GATES.md"; Description = "Public release gates" },
    @{ Path = "docs\MAINTAINER_RELEASE_HANDOFF.md"; Description = "Maintainer release handoff" },
    @{ Path = "CHANGELOG.md"; Description = "Changelog" },
    @{ Path = ".codex\CONTEXT_PACK.md"; Description = "Codex context pack" },
    @{ Path = ".codex\NEXT_STEPS.md"; Description = "Codex next steps" },
    @{ Path = ".codex\SESSION_HANDOFF.md"; Description = "Codex session handoff" },
    @{ Path = "scripts\check-cli-contract.ps1"; Description = "CLI contract check script" },
    @{ Path = "scripts\check-config-generated-conventions.ps1"; Description = "Config/generated convention check script" },
    @{ Path = "scripts\check-v100-documentation-release-gates.ps1"; Description = "Documentation/release gate check script" },
    @{ Path = "scripts\check-v100-readiness.ps1"; Description = "v1.0 final readiness check script" },
    @{ Path = "scripts\check-v050-readiness.ps1"; Description = "v0.5 readiness check script" },
    @{ Path = "scripts\verify-release.ps1"; Description = "Local release verification script" },
    @{ Path = "src\AgentContextKit.Cli\AgentContextKit.Cli.csproj"; Description = "CLI package project" }
)

foreach ($entry in $requiredPaths) {
    Require-Path -RelativePath $entry.Path -Description $entry.Description
}

$releaseValidation = Get-FileText "docs\RELEASE_VALIDATION.md"
$documentationIndex = Get-FileText "docs\DOCUMENTATION_INDEX.md"
$roadmap = Get-FileText "docs\ROADMAP.md"
$projectMap = Get-FileText "docs\PROJECT_MAP.md"
$changelog = Get-FileText "CHANGELOG.md"
$readinessDoc = Get-FileText "docs\V100_READINESS.md"
$gapAnalysis = Get-FileText "docs\V100_GAP_ANALYSIS.md"
$freezeDoc = Get-FileText "docs\V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md"
$freezeScript = Get-FileText "scripts\check-v100-documentation-release-gates.ps1"
$contextPack = Get-FileText ".codex\CONTEXT_PACK.md"
$nextSteps = Get-FileText ".codex\NEXT_STEPS.md"
$sessionHandoff = Get-FileText ".codex\SESSION_HANDOFF.md"

$releaseValidationNeedles = @(
    "scripts/check-v100-readiness.ps1",
    "scripts/check-cli-contract.ps1",
    "scripts/check-config-generated-conventions.ps1",
    "scripts/check-v100-documentation-release-gates.ps1",
    "scripts/check-v050-readiness.ps1",
    "scripts/verify-release.ps1",
    "V100_READINESS.md",
    "CLI_CONTRACT.md",
    "CONFIG_GENERATED_CONVENTIONS.md",
    "V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md"
)

foreach ($needle in $releaseValidationNeedles) {
    Require-Text -Content $releaseValidation -Needle $needle -Description "Release validation reference $needle"
}

$documentationIndexNeedles = @(
    "V100_STABILIZATION_PLAN.md",
    "V100_DOCUMENTATION_RELEASE_GATE_FREEZE.md",
    "V100_READINESS.md",
    "V100_GAP_ANALYSIS.md",
    "RELEASE_VALIDATION.md",
    "MAINTAINER_RELEASE_HANDOFF.md"
)

foreach ($needle in $documentationIndexNeedles) {
    Require-Text -Content $documentationIndex -Needle $needle -Description "Documentation index reference $needle"
}

$readinessDocNeedles = @(
    "Historical Local Readiness Scope",
    "Usage",
    "Current Published State",
    "What This Gate Proves",
    "Actual 1.0 Readiness",
    "Required Validation",
    "Maintainer-Only Boundary",
    "does not"
)

foreach ($needle in $readinessDocNeedles) {
    Require-Text -Content $readinessDoc -Needle $needle -Description "Readiness doc section $needle"
}

$gapAnalysisNeedles = @(
    "Verdict",
    "Priority Definitions",
    "Gap Register",
    "V100-01",
    "Required Sequence",
    "Readiness Gates",
    "Status Maintenance"
)

foreach ($needle in $gapAnalysisNeedles) {
    Require-Text -Content $gapAnalysis -Needle $needle -Description "Gap analysis section $needle"
}

Require-Text -Content $roadmap -Needle "Final local readiness consolidation" -Description "Roadmap final local readiness note"

$projectMapNeedles = @(
    "Final local readiness consolidation",
    "docs/V100_READINESS.md",
    "docs/tasks/TASK-0039-v100-final-local-readiness-consolidation.md",
    "scripts/check-v100-readiness.ps1"
)

foreach ($needle in $projectMapNeedles) {
    Require-Text -Content $projectMap -Needle $needle -Description "Project map note $needle"
}

Require-Text -Content $changelog -Needle "v1.0 final local readiness review" -Description "Changelog v1.0 final readiness note"

$freezeDocNeedles = @(
    "docs/V100_READINESS.md",
    "scripts/check-v100-readiness.ps1"
)

foreach ($needle in $freezeDocNeedles) {
    Require-Text -Content $freezeDoc -Needle $needle -Description "Freeze doc reference $needle"
}

$freezeScriptNeedles = @(
    "docs\V100_READINESS.md",
    "scripts\check-v100-readiness.ps1"
)

foreach ($needle in $freezeScriptNeedles) {
    Require-Text -Content $freezeScript -Needle $needle -Description "Freeze script reference $needle"
}

$codexNeedles = @(
    "V100_GAP_ANALYSIS.md"
)

foreach ($needle in $codexNeedles) {
    Require-Text -Content $contextPack -Needle $needle -Description "Context pack reference $needle"
    Require-Text -Content $nextSteps -Needle $needle -Description "Next steps reference $needle"
    Require-Text -Content $sessionHandoff -Needle $needle -Description "Session handoff reference $needle"
}

if (-not (Test-Path $projectPath)) {
    Add-Issue "Package project file was not found: $projectPath"
}
else {
    [xml]$project = Get-Content $projectPath
    $repositoryUrl = Get-MetadataValue -Project $project -Name "RepositoryUrl"
    $packageProjectUrl = Get-MetadataValue -Project $project -Name "PackageProjectUrl"

    if ([string]::IsNullOrWhiteSpace($repositoryUrl) -or $repositoryUrl -match "TODO|example\.com|localhost") {
        Add-PublicBlocker "RepositoryUrl is missing or still a placeholder."
    }

    if ([string]::IsNullOrWhiteSpace($packageProjectUrl) -or $packageProjectUrl -match "TODO|example\.com|localhost") {
        Add-PublicBlocker "PackageProjectUrl is missing or still a placeholder."
    }
}

if (Get-Command git -ErrorAction SilentlyContinue) {
    Push-Location $repoRoot
    try {
        $releaseTagCommit = git rev-parse "v0.2.0-alpha.1^{commit}" 2>$null
        if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($releaseTagCommit)) {
            Add-PublicBlocker "Current release tag v0.2.0-alpha.1 was not found locally."
        }

        $status = git status --short 2>$null
        if ($LASTEXITCODE -eq 0 -and $status) {
            Add-Warning "Working tree has uncommitted changes."
        }
    }
    finally {
        Pop-Location
    }
}
else {
    Add-Warning "git was not found; tag and working tree checks were skipped."
}

Write-Host ""
if ($issues.Count -eq 0) {
    Write-Host "No historical v1.0 asset or gap-analysis presence issues detected."
}
else {
    Write-Host "Historical v1.0 asset or gap-analysis presence issues:"
    foreach ($issue in $issues) {
        Write-Host "- $issue"
    }
}

if ($publicBlockers.Count -gt 0) {
    Write-Host ""
    Write-Host "Public release blockers:"
    foreach ($blocker in $publicBlockers) {
        Write-Host "- $blocker"
    }
}

if ($warnings.Count -gt 0) {
    Write-Host ""
    Write-Host "Warnings:"
    foreach ($warning in $warnings) {
        Write-Host "- $warning"
    }
}

if ($notes.Count -gt 0) {
    Write-Host ""
    Write-Host "Notes:"
    foreach ($note in $notes) {
        Write-Host "- $note"
    }
}

Write-Host ""
Write-Host "This review is local-only. It does not push, publish, tag, redact, delete, upload, mutate files, call providers, handle API keys, or create remotes."

if ($FailOnIssues -and $issues.Count -gt 0) {
    Write-Host ""
    Write-Host "Historical v1.0 asset readiness gate failed."
    exit 1
}

exit 0
