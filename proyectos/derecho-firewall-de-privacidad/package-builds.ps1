param(
    [string]$OutputDirectory = "Builds\Packages"
)

$projectPath = (Resolve-Path $PSScriptRoot).Path
$packagePath = Join-Path $projectPath $OutputDirectory
New-Item -ItemType Directory -Force -Path $packagePath | Out-Null

function Package-Directory([string]$source, [string]$name) {
    $sourcePath = Join-Path $projectPath $source
    if (-not (Test-Path $sourcePath)) {
        Write-Warning "No existe $source; se omite $name."
        return
    }

    $zipPath = Join-Path $packagePath "$name.zip"
    Compress-Archive -Path (Join-Path $sourcePath '*') -DestinationPath $zipPath -CompressionLevel Optimal -Force
    Write-Host "Paquete creado: $zipPath"
}

Package-Directory 'Builds\Windows' 'DerechoFirewall-Windows-x64'
Package-Directory 'Builds\WebGL' 'DerechoFirewall-WebGL'
Package-Directory 'Builds\Linux' 'DerechoFirewall-Linux-x64'

