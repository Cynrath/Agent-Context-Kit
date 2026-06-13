param()

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$checker = Join-Path $PSScriptRoot "check-local-markdown-links.ps1"
$tempRoot = Join-Path $env:TEMP ("ackit-markdown-link-tests-" + [guid]::NewGuid().ToString("N"))
$hostExecutable = (Get-Process -Id $PID).Path

function Invoke-Case {
    param(
        [string]$Name,
        [hashtable]$Files,
        [int]$ExpectedExitCode
    )

    $caseRoot = Join-Path $tempRoot $Name
    New-Item -ItemType Directory -Force -Path $caseRoot | Out-Null
    foreach ($entry in $Files.GetEnumerator()) {
        $path = Join-Path $caseRoot $entry.Key
        New-Item -ItemType Directory -Force -Path ([System.IO.Path]::GetDirectoryName($path)) | Out-Null
        Set-Content -LiteralPath $path -Value $entry.Value -Encoding utf8
    }

    $output = @(& $hostExecutable -NoProfile -ExecutionPolicy Bypass -File $checker -RepositoryRoot $caseRoot -FailOnIssues 2>&1)
    $actualExitCode = $LASTEXITCODE
    if ($actualExitCode -ne $ExpectedExitCode) {
        $output | Out-Host
        throw "Markdown link test '$Name' expected exit code $ExpectedExitCode but received $actualExitCode."
    }
}

try {
    Invoke-Case -Name "valid-local" -ExpectedExitCode 0 -Files @{
        "README.md" = "[Guide](docs/guide.md#start)"
        "docs/guide.md" = "# Start"
    }
    Invoke-Case -Name "external-and-code" -ExpectedExitCode 0 -Files @{
        "README.md" = '[Web](https://example.invalid) `[Example](missing.md)`'
    }
    Invoke-Case -Name "broken-local" -ExpectedExitCode 1 -Files @{
        "README.md" = "[Missing](docs/missing.md)"
    }
    Invoke-Case -Name "repository-escape" -ExpectedExitCode 1 -Files @{
        "README.md" = "[Outside](../outside.md)"
    }

    Write-Host "Local Markdown link gate tests passed."
}
finally {
    if (Test-Path $tempRoot) {
        Remove-Item -LiteralPath $tempRoot -Recurse -Force
    }
}
