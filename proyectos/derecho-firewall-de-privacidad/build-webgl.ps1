param([string]$UnityPath = "C:\Program Files\Unity\Hub\Editor\6000.6.10f1\Editor\Unity.exe")
$projectPath = (Resolve-Path $PSScriptRoot).Path
$outputPath = Join-Path $projectPath 'Builds\WebGL\index.html'
$logPath = Join-Path $projectPath 'Builds\WebGL\build.log'
New-Item -ItemType Directory -Force -Path (Split-Path $outputPath) | Out-Null
& $UnityPath -batchmode -quit -buildTarget WebGL -projectPath $projectPath -executeMethod BuildAutomation.BuildWebGl -logFile $logPath
if ($LASTEXITCODE -ne 0 -or -not (Test-Path $outputPath)) { Write-Error "El build WebGL falló. Revisa $logPath"; exit 1 }
Write-Host "Build WebGL creado: $outputPath"
