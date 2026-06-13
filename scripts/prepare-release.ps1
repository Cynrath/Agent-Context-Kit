param(
    [Parameter(Mandatory = $true)]
    [string]$Version,
    [string]$CommitSha,
    [switch]$RequireOriginMaster,
    [switch]$AllowDirty,
    [switch]$FailOnIssues
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$issues = [System.Collections.Generic.List[string]]::new()
$notes = [System.Collections.Generic.List[string]]::new()

function Add-Issue { param([string]$Message) $issues.Add($Message) | Out-Null }
function Add-Note { param([string]$Message) $notes.Add($Message) | Out-Null }

function Get-ProjectValue {
    param([xml]$Project, [string]$Name)

    foreach ($group in $Project.Project.PropertyGroup) {
        $value = $group.$Name
        if ($null -ne $value -and -not [string]::IsNullOrWhiteSpace([string]$value)) {
            return [string]$value
        }
    }

    return $null
}

if ($Version -notmatch '^\d+\.\d+\.\d+(?:-[0-9A-Za-z.-]+)?$') {
    Add-Issue "Version is not a supported SemVer value."
}

Push-Location $repoRoot
try {
    $head = (git rev-parse HEAD).Trim()
    if ($LASTEXITCODE -ne 0) { throw "Unable to resolve HEAD." }
    if ([string]::IsNullOrWhiteSpace($CommitSha)) { $CommitSha = $head }

    $resolvedCommit = git rev-parse "$CommitSha^{commit}" 2>$null
    if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($resolvedCommit)) {
        Add-Issue "Commit SHA could not be resolved."
        $resolvedCommit = ""
    }
    else {
        $resolvedCommit = $resolvedCommit.Trim()
    }

    if ($resolvedCommit -and $resolvedCommit -ne $head) {
        Add-Issue "Requested commit does not match HEAD."
    }
    else {
        Add-Note "Commit matches HEAD: $head"
    }

    if (-not $AllowDirty -and (git status --porcelain)) {
        Add-Issue "Working tree is not clean."
    }

    if ($RequireOriginMaster) {
        git fetch --no-tags origin master | Out-Null
        if ($LASTEXITCODE -ne 0) {
            Add-Issue "origin/master could not be fetched."
        }
        else {
            $originMaster = (git rev-parse origin/master).Trim()
            if ($originMaster -ne $resolvedCommit) {
                Add-Issue "commit_sha must equal origin/master HEAD."
            }
            else {
                Add-Note "Commit matches origin/master."
            }
        }
    }

    [xml]$project = Get-Content (Join-Path $repoRoot "src\AgentContextKit.Cli\AgentContextKit.Cli.csproj")
    $projectVersion = Get-ProjectValue -Project $project -Name "Version"
    if ($projectVersion -ne $Version) { Add-Issue "Project version does not match requested version." }

    $program = Get-Content -Raw (Join-Path $repoRoot "src\AgentContextKit.Cli\Program.cs")
    if ($program -notmatch ('private const string Version = "' + [regex]::Escape($Version) + '";')) {
        Add-Issue "CLI runtime version does not match requested version."
    }

    $sourceSmoke = Get-Content -Raw (Join-Path $repoRoot ".github\workflows\cross-platform-source-smoke.yml")
    if (-not $sourceSmoke.Contains("--version $Version")) {
        Add-Issue "Source-package smoke version does not match requested version."
    }

    $verifyRelease = Get-Content -Raw (Join-Path $repoRoot "scripts\verify-release.ps1")
    if ($verifyRelease -notmatch ('\[string\]\$Version = "' + [regex]::Escape($Version) + '"')) {
        Add-Issue "verify-release default version does not match requested version."
    }

    $tagName = "v$Version"
    $existingTag = @(git tag --list $tagName)
    if ($existingTag -contains $tagName) {
        $tagCommit = git rev-list -n 1 $tagName
        if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($tagCommit)) {
            Add-Issue "Existing tag $tagName could not be resolved."
        }
        elseif ($tagCommit.Trim() -ne $resolvedCommit) {
            Add-Issue "Existing tag $tagName targets a different commit."
        }
        else {
            Add-Note "Existing tag $tagName targets the requested commit."
        }
    }
    else {
        Add-Note "Target tag does not exist locally."
    }
}
finally {
    Pop-Location
}

Write-Host "AgentContextKit release preparation review"
Write-Host "Version: $Version"
foreach ($note in $notes) { Write-Host "- $note" }

if ($issues.Count -gt 0) {
    Write-Host "Release preparation issues:"
    foreach ($issue in $issues) { Write-Host "- $issue" }
}
else {
    Write-Host "Release preparation checks passed."
}

if ($FailOnIssues -and $issues.Count -gt 0) { exit 1 }
exit 0
