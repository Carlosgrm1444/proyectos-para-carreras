param(
    [string]$RepositoryUrl = "https://github.com/Carlosgrm1444/proyectos-para-carreras.git",
    [string]$Branch = "codex/distribucion-multiplataforma"
)

$ErrorActionPreference = "Stop"
$git = "C:\Program Files\Git\cmd\git.exe"
$projectRoot = (Resolve-Path $PSScriptRoot).Path
$tempRoot = Join-Path ([IO.Path]::GetTempPath()) "derecho-firewall-lfs-publish"
$cloneRoot = Join-Path $tempRoot "repo"

if (-not (Test-Path $git)) { throw "Git no está instalado en $git" }
$env:PATH = "C:\Program Files\Git\cmd;C:\Program Files\Git\usr\bin;$env:PATH"

& $git lfs install
& $git -c credential.interactive=never ls-remote $RepositoryUrl "refs/heads/$Branch" | Out-Null
if ($LASTEXITCODE -ne 0) {
    throw "GitHub no tiene credenciales disponibles. Configura Git Credential Manager o inicia sesión antes de publicar."
}

if (Test-Path $tempRoot) { Remove-Item -LiteralPath $tempRoot -Recurse -Force }
New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null
& $git clone --branch $Branch --single-branch $RepositoryUrl $cloneRoot

$source = Join-Path $projectRoot "proyectos\derecho-firewall-de-privacidad"
$destination = Join-Path $cloneRoot "proyectos\derecho-firewall-de-privacidad"
New-Item -ItemType Directory -Force -Path $destination | Out-Null

robocopy $projectRoot $cloneRoot /E /XD .git Library Logs Temp UserSettings Builds /XF *.zip *.exe *.dll *.pdb | Out-Null
robocopy $source (Join-Path $destination "Builds\Packages") /E /XF *.exe *.dll *.pdb | Out-Null

& $git -C $cloneRoot lfs track "proyectos/**/Builds/Packages/*.zip"
& $git -C $cloneRoot add -A
if (-not (& $git -C $cloneRoot diff --cached --quiet)) {
    & $git -C $cloneRoot commit -m "feat: complete controller-only gameplay and exportables"
}
& $git -C $cloneRoot push origin $Branch
Write-Host "Publicación completada en $RepositoryUrl/tree/$Branch"

