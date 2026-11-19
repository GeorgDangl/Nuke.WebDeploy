$ErrorActionPreference = 'Stop'
Push-Location $PSScriptRoot
try {
    $BuildProject = 'build/.build.csproj'
    $SdkVersion = '10.0.100'
    $CacheDirectory = '.fallout/temp'
    $env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    $env:DOTNET_ROLL_FORWARD = 'Major'
    $DotNet = Get-Command dotnet -ErrorAction SilentlyContinue
    $SdkAvailable = $false
    if ($DotNet) {
        & $DotNet.Source --version *> $null
        $SdkAvailable = $LASTEXITCODE -eq 0
    }
    if (-not $SdkAvailable) {
        New-Item -ItemType Directory -Force $CacheDirectory | Out-Null
        Invoke-WebRequest https://dot.net/v1/dotnet-install.ps1 -OutFile "$CacheDirectory/dotnet-install.ps1"
        & "$CacheDirectory/dotnet-install.ps1" -Version $SdkVersion -InstallDir "$CacheDirectory/dotnet" -NoPath
        if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
        $env:PATH = "$(Join-Path $PWD "$CacheDirectory/dotnet")$([IO.Path]::PathSeparator)$env:PATH"
    }
    & dotnet run --project $BuildProject -- @args
    exit $LASTEXITCODE
}
finally {
    Pop-Location
}
