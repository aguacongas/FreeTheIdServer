Write-Host $env:APPVEYOR $env:APPVEYOR_PULL_REQUEST_NUMBER

if ($env:APPVEYOR -and $env:APPVEYOR_PULL_REQUEST_NUMBER) {
    exit 0
}

$fileversion = "$env:SemVer.0"
$path = (Get-Location).Path

dotnet pack -c Release -o $path\artifacts\build -p:Version=$env:Version -p:FileVersion=$fileversion -p:SourceRevisionId=$env:APPVEYOR_REPO_COMMIT

dotnet publish src\Aguacongas.FreeTheIdServer.Open\Aguacongas.FreeTheIdServer.Open.csproj -c Release -o $path\artifacts\Aguacongas.FreeTheIdServer.Open -p:Version=$env:Version -p:FileVersion=$fileversion -p:SourceRevisionId=$env:APPVEYOR_REPO_COMMIT
if ($LASTEXITCODE -ne 0) {
    throw "publis failed src/Aguacongas.FreeTheIdServer/Aguacongas.FreeTheIdServer.Open.csproj"
}

dotnet publish src\Aguacongas.FreeTheIdServer.BlazorApp\Aguacongas.FreeTheIdServer.BlazorApp.csproj -c Release -o $path\artifacts\Aguacongas.FreeTheIdServer.BlazorApp -p:Version=$env:Version -p:FileVersion=$fileversion -p:SourceRevisionId=$env:APPVEYOR_REPO_COMMIT
if ($LASTEXITCODE -ne 0) {
    throw "publish failed src/Aguacongas.FreeTheIdServer.BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.csproj"
}

7z a $path\artifacts\build\Aguacongas.FreeTheIdServer.Open.$env:version.zip $path\artifacts\Aguacongas.FreeTheIdServer.Open
7z a $path\artifacts\build\Aguacongas.FreeTheIdServer.BlazorApp$env:version.zip $path\artifacts\Aguacongas.FreeTheIdServer.BlazorApp

$runtimes = "win-x86", "win-x64", "linux-x64", "osx-x64"

foreach($r in $runtimes) {
    dotnet publish src\Aguacongas.FreeTheIdServer.Open\Aguacongas.FreeTheIdServer.Open.csproj -c Release -o $path\artifacts\Aguacongas.FreeTheIdServer.Open-$r -r $r  -p:Version=$env:Version -p:FileVersion=$fileversion -p:SourceRevisionId=$env:APPVEYOR_REPO_COMMIT        
    7z a $path\artifacts\build\Aguacongas.FreeTheIdServer.Open-$r.$env:version.zip $path\artifacts\Aguacongas.FreeTheIdServer.Open-$r
}
