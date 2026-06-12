param(
    [ValidateRange(100, 20000)]
    [int]$FileCount = 2000,
    [ValidateRange(1, 300)]
    [double]$MaxSeconds = 30,
    [switch]$FailOnThreshold
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$cliProject = Join-Path $repoRoot "src\AgentContextKit.Cli\AgentContextKit.Cli.csproj"
$tempRoot = Join-Path $env:TEMP ("ackit-performance-" + [guid]::NewGuid().ToString("N"))

Write-Host "AgentContextKit synthetic scan benchmark"
Write-Host "Files requested: $FileCount"
Write-Host "Threshold: $MaxSeconds seconds"

try {
    New-Item -ItemType Directory -Path $tempRoot | Out-Null
    New-Item -ItemType Directory -Path (Join-Path $tempRoot "src") | Out-Null
    New-Item -ItemType Directory -Path (Join-Path $tempRoot "tests") | Out-Null
    New-Item -ItemType Directory -Path (Join-Path $tempRoot ".github\workflows") -Force | Out-Null

    Set-Content -Path (Join-Path $tempRoot "README.md") -Value "# Synthetic benchmark"
    Set-Content -Path (Join-Path $tempRoot "LICENSE") -Value "MIT"
    Set-Content -Path (Join-Path $tempRoot "SECURITY.md") -Value "# Security"
    Set-Content -Path (Join-Path $tempRoot ".gitignore") -Value "bin/`nobj/"
    Set-Content -Path (Join-Path $tempRoot "AGENTS.md") -Value "# Agent instructions"
    Set-Content -Path (Join-Path $tempRoot "tests\FixtureTests.cs") -Value "// synthetic tests"
    Set-Content -Path (Join-Path $tempRoot ".github\workflows\ci.yml") -Value "name: synthetic-ci"

    for ($index = 1; $index -le $FileCount; $index++) {
        $bucket = Join-Path $tempRoot ("src\bucket-{0:D3}" -f ($index % 100))
        if (-not (Test-Path $bucket)) {
            New-Item -ItemType Directory -Path $bucket | Out-Null
        }

        $path = Join-Path $bucket ("File{0:D5}.cs" -f $index)
        Set-Content -Path $path -Value ("namespace Synthetic; public sealed class File{0:D5} {{ }}" -f $index)
    }

    Push-Location $tempRoot
    try {
        $stopwatch = [System.Diagnostics.Stopwatch]::StartNew()
        $output = & dotnet run --project $cliProject -c Release --no-build -- scan --ci 2>&1
        $exitCode = $LASTEXITCODE
        $stopwatch.Stop()
    }
    finally {
        Pop-Location
    }

    if ($exitCode -ne 0) {
        $output | Write-Host
        throw "Synthetic scan failed with exit code $exitCode."
    }

    $elapsed = [Math]::Round($stopwatch.Elapsed.TotalSeconds, 3)
    $withinThreshold = $elapsed -le $MaxSeconds
    Write-Host "Elapsed seconds: $elapsed"
    Write-Host "Threshold result: $(if ($withinThreshold) { 'PASS' } else { 'FAIL' })"
    Write-Host "This synthetic benchmark is local evidence, not a production SLA."

    if ($FailOnThreshold -and -not $withinThreshold) {
        exit 1
    }
}
finally {
    if (Test-Path $tempRoot) {
        Remove-Item -LiteralPath $tempRoot -Recurse -Force
    }
}

exit 0
