param(
    [string]$UnityPath = "C:\Program Files\Unity\Hub\Editor\6000.6.10f1\Editor\Unity.exe"
)

$projectPath = (Resolve-Path (Join-Path $PSScriptRoot '.')).Path
$outputPath = Join-Path $projectPath 'Builds\Windows\DerechoFirewall.exe'

$projectProcess = Get-CimInstance Win32_Process -Filter "Name='Unity.exe'" |
    Where-Object { $_.CommandLine -like "*${projectPath}*" }

if ($projectProcess) {
    Write-Error "Unity ya está abierto con este proyecto. Ciérralo antes de generar el build."
    exit 2
}

if (-not (Test-Path -LiteralPath $UnityPath)) {
    Write-Error "No se encontró Unity en: $UnityPath"
    exit 3
}

New-Item -ItemType Directory -Force -Path (Split-Path $outputPath) | Out-Null
$logPath = Join-Path $projectPath 'Builds\Windows\build.log'

& $UnityPath -batchmode -quit -projectPath $projectPath -executeMethod BuildAutomation.BuildWindows -logFile $logPath
if ($LASTEXITCODE -ne 0 -or -not (Test-Path -LiteralPath $outputPath)) {
    Write-Error "El build falló. Revisa $logPath"
    exit 1
}

Write-Host "Build creado: $outputPath"

