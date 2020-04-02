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

Write-Host "Restoring nuget packages" -ForegroundColor Yellow -BackgroundColor DarkGreen
exec { & dotnet restore .\src }

Write-Host "Building solution" -ForegroundColor Yellow -BackgroundColor DarkGreen
exec { & dotnet build .\src -c Release }

Write-Host "Running Tests" -ForegroundColor Yellow -BackgroundColor DarkGreen
exec { & dotnet test .\src\SDK.Tests\OneSpanSign.Sdk.Test.csproj -c Release }

Write-Host "Packaging nuget" -ForegroundColor Yellow -BackgroundColor DarkGreen
exec { & dotnet pack .\src\SDK --no-build -c Release -o .\artifacts }