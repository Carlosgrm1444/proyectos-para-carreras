param([string]$UnityPath = "C:\Program Files\Unity\Hub\Editor\6000.6.10f1\Editor\Unity.exe")
$projectPath = (Resolve-Path $PSScriptRoot).Path
$outputPath = Join-Path $projectPath 'Builds\Linux\DerechoFirewall.x86_64'
$logPath = Join-Path $projectPath 'Builds\Linux\build.log'
New-Item -ItemType Directory -Force -Path (Split-Path $outputPath) | Out-Null
& $UnityPath -batchmode -quit -buildTarget StandaloneLinux64 -projectPath $projectPath -executeMethod BuildAutomation.BuildLinux -logFile $logPath
if ($LASTEXITCODE -ne 0 -or -not (Test-Path $outputPath)) { Write-Error "El build Linux falló. Revisa $logPath"; exit 1 }
Write-Host "Build Linux creado: $outputPath"

