param([switch]$FailOnIssues)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$workflowPath = Join-Path $repoRoot ".github\workflows\release.yml"
$issues = [System.Collections.Generic.List[string]]::new()

if (-not (Test-Path $workflowPath)) {
    $issues.Add("Release workflow is missing.") | Out-Null
    $content = ""
}
else {
    $content = Get-Content -Raw $workflowPath
}

$required = @(
    "workflow_dispatch:",
    "version:",
    "commit_sha:",
    "prerelease:",
    "concurrency:",
    "contents: read",
    "environment: nuget-release",
    "contents: write",
    "id-token: write",
    "NuGet/login@v1",
    "user: Cyranth",
    "steps.login.outputs.NUGET_API_KEY",
    'git tag --list $tagName',
    "gh release list",
    "scripts/prepare-release.ps1",
    "scripts/verify-published-package.ps1",
    "scripts/check-local-markdown-links.ps1",
    "scripts/verify-release.ps1",
    "gh release create"
)

$prepareRelease = Get-Content -Raw (Join-Path $repoRoot "scripts\prepare-release.ps1")
if (-not $prepareRelease.Contains('git tag --list $tagName')) {
    $issues.Add("Release preparation must treat an absent target tag as an idempotent state.") | Out-Null
}

foreach ($marker in $required) {
    if (-not $content.Contains($marker)) {
        $issues.Add("Required workflow marker missing: $marker") | Out-Null
    }
}

foreach ($forbidden in @('push:', 'pull_request:', 'NUGET_API_KEY: ${{ secrets.', 'force-with-lease', '--force')) {
    if ($content.Contains($forbidden)) {
        $issues.Add("Forbidden workflow marker present: $forbidden") | Out-Null
    }
}

$publishIndex = $content.IndexOf("dotnet nuget push", [StringComparison]::Ordinal)
$tagIndex = $content.IndexOf('git tag "$tagName"', [StringComparison]::Ordinal)
$releaseIndex = $content.IndexOf("gh release create", [StringComparison]::Ordinal)
if ($publishIndex -lt 0 -or $tagIndex -lt 0 -or $releaseIndex -lt 0 -or $publishIndex -gt $tagIndex -or $tagIndex -gt $releaseIndex) {
    $issues.Add("Workflow must publish and verify NuGet before tag and GitHub Release creation.") | Out-Null
}

if ($issues.Count -eq 0) {
    Write-Host "Release workflow static checks passed."
}
else {
    Write-Host "Release workflow issues:"
    foreach ($issue in $issues) { Write-Host "- $issue" }
}

if ($FailOnIssues -and $issues.Count -gt 0) { exit 1 }
exit 0
