# Taken from psake https://github.com/psake/psake

<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

if(Test-Path .\.git\index.lock) { Remove-Item .\.git\index.lock -Force }

# ── 1. Restore ─────────────────────────────────────────────────────────────────
Write-Host "Restoring nuget packages" -ForegroundColor Yellow -BackgroundColor DarkGreen
exec { & dotnet restore .\src }

# ── 2. Build ───────────────────────────────────────────────────────────────────
# Builds all three SDK target frameworks: netstandard2.0, net8.0, net10.0
Write-Host "Building solution" -ForegroundColor Yellow -BackgroundColor DarkGreen
exec { & dotnet build .\src -c Release }

# ── 3. Test — net10.0 (HttpClient code path, runs on any OS) ──────────────────
Write-Host "Running tests on net10.0 (HttpClient path)" -ForegroundColor Yellow -BackgroundColor DarkGreen
exec { & dotnet test .\src\SDK.Tests\OneSpanSign.Sdk.Test.csproj --framework net10.0 -c Release --no-build }

# ── 4. Test — net48 (WebRequest / netstandard2.0 code path, Windows only) ─────
if ($IsWindows -or ($PSVersionTable.PSVersion.Major -le 5)) {
    Write-Host "Running tests on net48 (WebRequest / netstandard2.0 path)" -ForegroundColor Yellow -BackgroundColor DarkGreen
    exec { & dotnet test .\src\SDK.Tests\OneSpanSign.Sdk.Test.csproj --framework net48 -c Release --no-build }
} else {
    Write-Host "Skipping net48 tests — not running on Windows" -ForegroundColor Cyan
}

# ── 5. Example tests (Samples.Tests) ──────────────────────────────────────────
# Requires .\src\Samples\signers.properties to exist.
# Copy the right environment file first, e.g.:
#   Copy-Item .\src\Samples\signers.properties.q6 .\src\Samples\signers.properties
$signersProps = ".\src\Samples\signers.properties"
if (Test-Path $signersProps) {
    Write-Host "Running example tests (Samples.Tests, net10.0)" -ForegroundColor Yellow -BackgroundColor DarkGreen
    exec { & dotnet test .\src\Samples.Tests\OneSpanSign.Sdk.Samples.Tests.csproj --framework net10.0 -c Release --no-build }
} else {
    Write-Host "Skipping Samples.Tests — $signersProps not found." -ForegroundColor Cyan
    Write-Host "  Copy the appropriate environment file first, e.g.:" -ForegroundColor Cyan
    Write-Host "  Copy-Item .\src\Samples\signers.properties.q3 .\src\Samples\signers.properties" -ForegroundColor Cyan
}

# ── 6. Pack NuGet ──────────────────────────────────────────────────────────────
# Produces lib/netstandard2.0/, lib/net8.0/, lib/net10.0/ inside the .nupkg
Write-Host "Packaging nuget" -ForegroundColor Yellow -BackgroundColor DarkGreen
exec { & dotnet pack .\src\SDK --no-build -c Release -o .\artifacts }
