param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")),
    [switch]$FailOnIssues
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = (Resolve-Path $RepositoryRoot).Path
$issues = [System.Collections.Generic.List[string]]::new()
$checkedTargets = 0
$skippedExternal = 0
$excludedSegments = @(".git", "bin", "obj", "TestResults", "coverage", "publish", "out", ".ackit")

function Get-RelativePath {
    param(
        [string]$BasePath,
        [string]$TargetPath
    )

    $baseFullPath = [System.IO.Path]::GetFullPath($BasePath).TrimEnd('\', '/') + [System.IO.Path]::DirectorySeparatorChar
    $targetFullPath = [System.IO.Path]::GetFullPath($TargetPath)
    $baseUri = [System.Uri]::new($baseFullPath)
    $targetUri = [System.Uri]::new($targetFullPath)
    return [System.Uri]::UnescapeDataString($baseUri.MakeRelativeUri($targetUri).ToString()).Replace('/', [System.IO.Path]::DirectorySeparatorChar)
}

function Test-ExcludedPath {
    param([string]$Path)

    $relative = Get-RelativePath -BasePath $repoRoot -TargetPath $Path
    $segments = $relative -split '[\\/]'
    return $segments | Where-Object { $excludedSegments -contains $_ } | Select-Object -First 1
}

function Get-MarkdownTextWithoutCode {
    param([string]$Path)

    $builder = [System.Text.StringBuilder]::new()
    $insideFence = $false
    foreach ($line in [System.IO.File]::ReadLines($Path)) {
        if ($line -match '^\s*(```|~~~)') {
            $insideFence = -not $insideFence
            continue
        }

        if ($insideFence) {
            continue
        }

        $withoutInlineCode = [regex]::Replace($line, '`[^`]*`', '')
        [void]$builder.AppendLine($withoutInlineCode)
    }

    return $builder.ToString()
}

function Test-MarkdownTarget {
    param(
        [string]$SourcePath,
        [string]$RawTarget
    )

    $target = $RawTarget.Trim().Trim('<', '>')
    if ([string]::IsNullOrWhiteSpace($target) -or $target.StartsWith('#')) {
        return
    }

    if ($target -match '^(?i:https?|mailto|tel|data):') {
        $script:skippedExternal++
        return
    }

    if ($target -match '[{}$]') {
        return
    }

    $target = ($target -split '#', 2)[0]
    $target = ($target -split '\?', 2)[0]
    if ([string]::IsNullOrWhiteSpace($target)) {
        return
    }

    try {
        $target = [System.Uri]::UnescapeDataString($target)
    }
    catch {
        $issues.Add("$(Get-RelativePath -BasePath $repoRoot -TargetPath $SourcePath): invalid encoded link target '$RawTarget'.")
        return
    }

    $sourceRelative = (Get-RelativePath -BasePath $repoRoot -TargetPath $SourcePath).Replace('\', '/')
    $script:checkedTargets++
    if ($target -match '^[A-Za-z]:[\\/]' -or $target.StartsWith('\\')) {
        $issues.Add("${sourceRelative}: link uses an absolute local path: '$RawTarget'.")
        return
    }

    if ($target.StartsWith('/') -or $target.StartsWith('\')) {
        $combinedRelative = $target.TrimStart('/', '\')
    }
    else {
        $sourceDirectory = [System.IO.Path]::GetDirectoryName($sourceRelative)
        $combinedRelative = if ([string]::IsNullOrWhiteSpace($sourceDirectory)) {
            $target
        }
        else {
            "$($sourceDirectory.Replace('\', '/'))/$target"
        }
    }

    $segments = [System.Collections.Generic.List[string]]::new()
    foreach ($segment in ($combinedRelative.Replace('\', '/') -split '/')) {
        if ([string]::IsNullOrWhiteSpace($segment) -or $segment -eq '.') { continue }
        if ($segment -eq '..') {
            if ($segments.Count -eq 0) {
                $issues.Add("${sourceRelative}: link escapes repository: '$RawTarget'.")
                return
            }
            $segments.RemoveAt($segments.Count - 1)
            continue
        }
        $segments.Add($segment)
    }

    $repoRelativeTarget = $segments -join [System.IO.Path]::DirectorySeparatorChar
    $fullCandidate = Join-Path $repoRoot $repoRelativeTarget
    if (-not (Test-Path -LiteralPath $fullCandidate)) {
        $issues.Add("${sourceRelative}: missing local Markdown target '$RawTarget'.")
    }
}

$markdownFiles = @(Get-ChildItem -LiteralPath $repoRoot -Recurse -File -Filter *.md |
    Where-Object { -not (Test-ExcludedPath $_.FullName) })

$inlinePattern = [regex]'!?\[[^\]]*\]\((?<target><[^>]+>|[^\s\)]+)(?:\s+["''][^"'']*["''])?\)'
$referencePattern = [regex]'(?m)^\s*\[[^\]]+\]:\s*(?<target><[^>]+>|\S+)'

foreach ($file in $markdownFiles) {
    $content = Get-MarkdownTextWithoutCode $file.FullName
    foreach ($match in $inlinePattern.Matches($content)) {
        Test-MarkdownTarget -SourcePath $file.FullName -RawTarget $match.Groups['target'].Value
    }
    foreach ($match in $referencePattern.Matches($content)) {
        Test-MarkdownTarget -SourcePath $file.FullName -RawTarget $match.Groups['target'].Value
    }
}

Write-Host "Local Markdown link audit"
Write-Host "Repository: $repoRoot"
Write-Host "Markdown files: $($markdownFiles.Count)"
Write-Host "Local targets checked: $checkedTargets"
Write-Host "External targets skipped: $skippedExternal"

if ($issues.Count -eq 0) {
    Write-Host "No broken local Markdown targets detected."
    exit 0
}

Write-Host "Broken local Markdown targets:"
foreach ($issue in $issues) {
    Write-Host "- $issue"
}

if ($FailOnIssues) {
    exit 1
}

exit 0
