param(
    [Parameter(Mandatory = $true)]
    [string]$Version,
    [string]$PackageSource,
    [int]$MaxAttempts = 30,
    [int]$DelaySeconds = 10,
    [switch]$KeepTemporaryFiles
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$stamp = Get-Date -Format "yyyyMMddHHmmssfff"
$tempBase = @($env:TEMP, $env:TMPDIR, $env:RUNNER_TEMP, [System.IO.Path]::GetTempPath()) |
    Where-Object { -not [string]::IsNullOrWhiteSpace($_) } |
    Select-Object -First 1
if ([string]::IsNullOrWhiteSpace($tempBase)) {
    throw "No temporary directory is available for package verification."
}
$root = Join-Path $tempBase "ackit-package-verification-$stamp"
$toolRoot = Join-Path $root "tool"
$smokeRoot = Join-Path $root "smoke"

function Invoke-Ackit {
    param([string[]]$Arguments, [switch]$AllowFailure)

    & $script:ackit @Arguments | Out-Host
    $exitCode = $LASTEXITCODE
    if (-not $AllowFailure -and $exitCode -ne 0) {
        throw "Installed ackit command failed with exit code $exitCode."
    }

    return $exitCode
}

try {
    New-Item -ItemType Directory -Force -Path $toolRoot, $smokeRoot | Out-Null

    if ([string]::IsNullOrWhiteSpace($PackageSource)) {
        $packageUrl = "https://api.nuget.org/v3-flatcontainer/agentcontextkit/$Version/agentcontextkit.$Version.nupkg"
        $available = $false
        for ($attempt = 1; $attempt -le $MaxAttempts; $attempt++) {
            try {
                Invoke-WebRequest -Uri $packageUrl -Method Head -UseBasicParsing | Out-Null
                $available = $true
                break
            }
            catch {
                if ($attempt -eq $MaxAttempts) { break }
                Start-Sleep -Seconds $DelaySeconds
            }
        }

        if (-not $available) {
            throw "AgentContextKit $Version did not become available from NuGet within the retry window."
        }

        dotnet tool install AgentContextKit --tool-path $toolRoot --version $Version
    }
    else {
        $resolvedSource = (Resolve-Path $PackageSource).Path
        dotnet tool install AgentContextKit --tool-path $toolRoot --add-source $resolvedSource --version $Version --ignore-failed-sources
    }

    if ($LASTEXITCODE -ne 0) { throw "AgentContextKit $Version tool installation failed." }

    $script:ackit = if ($env:OS -eq "Windows_NT") {
        Join-Path $toolRoot "ackit.exe"
    }
    else {
        Join-Path $toolRoot "ackit"
    }

    $versionOutput = (& $script:ackit version | Out-String).Trim()
    if ($LASTEXITCODE -ne 0 -or $versionOutput -ne "AgentContextKit $Version") {
        throw "Installed tool version output did not match AgentContextKit $Version."
    }

    $helpOutput = (& $script:ackit --help | Out-String)
    if ($LASTEXITCODE -ne 0 -or -not $helpOutput.Contains("ackit sarif")) {
        throw "Installed tool help validation failed."
    }

    Push-Location $smokeRoot
    try {
        dotnet new console -n DemoApp --no-restore | Out-Null
        if ($LASTEXITCODE -ne 0) { throw "Disposable console project creation failed." }
        Push-Location "DemoApp"
        try {
            git init --quiet
            Invoke-Ackit @("init", "--lang", "en") | Out-Null
            Invoke-Ackit @("scan", "--ci") | Out-Null
            Invoke-Ackit @("generate", "--target", "all", "--lang", "en") | Out-Null
            Invoke-Ackit @("task", "Published package smoke test", "--lang", "en") | Out-Null
            Invoke-Ackit @("report", "--output", ".ackit/reports/smoke.html") | Out-Null
            Invoke-Ackit @("webui", "--output", ".ackit/webui/smoke.html") | Out-Null
            Invoke-Ackit @("sarif", "--output", ".ackit/reports/smoke.sarif") | Out-Null
            Get-Content ".ackit/reports/smoke.sarif" -Raw | ConvertFrom-Json | Out-Null
            Invoke-Ackit @("prompt-pack", "--output", ".ackit/prompt-packs/smoke.md", "--lang", "en") | Out-Null
            Invoke-Ackit @("context-export", "--prompt-pack", ".ackit/prompt-packs/smoke.md", "--approve", "--output", ".ackit/context-exports/smoke.json") | Out-Null

            $syntheticSecret = "OPENAI" + "_API_KEY=" + "sk-" + "verification-1234567890abcdef"
            [System.IO.File]::WriteAllText((Join-Path (Get-Location) ".env.test"), $syntheticSecret)
            $redactExitCode = Invoke-Ackit @("redact-check", "--profile", "public-release") -AllowFailure
            if ($redactExitCode -ne 2) { throw "Expected redact-check exit code 2 for synthetic secret." }
            Remove-Item -LiteralPath ".env.test" -Force
            Invoke-Ackit @("scan", "--ci") | Out-Null
        }
        finally {
            Pop-Location
        }
    }
    finally {
        Pop-Location
    }

    Write-Host "AgentContextKit $Version package verification passed."
}
finally {
    if (-not $KeepTemporaryFiles -and (Test-Path $root)) {
        Remove-Item -LiteralPath $root -Recurse -Force
    }
}
