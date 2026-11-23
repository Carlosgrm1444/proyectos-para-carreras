param(
    [string]$RepositoryUrl = "https://github.com/Carlosgrm1444/proyectos-para-carreras.git",
    [string]$Branch = "codex/distribucion-multiplataforma",
    [string]$ProjectRelativePath = "proyectos\derecho-firewall-de-privacidad",
    [string]$CommitMessage = "chore: publish project updates"
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

$source = Join-Path $projectRoot $ProjectRelativePath
$destination = Join-Path $cloneRoot $ProjectRelativePath
New-Item -ItemType Directory -Force -Path $destination | Out-Null

robocopy $projectRoot $cloneRoot /E /XD .git Library Logs Temp UserSettings Builds /XF *.zip *.exe *.dll *.pdb | Out-Null
$packages = Join-Path $source "Builds\Packages"
$packageDestination = Join-Path $destination "Builds\Packages"
if (Test-Path $packages) {
    New-Item -ItemType Directory -Force -Path $packageDestination | Out-Null
    robocopy $packages $packageDestination /E /XF *.exe *.dll *.pdb | Out-Null
}
$manifest = Join-Path $source "Builds\manifest.json"
$manifestDestination = Join-Path $destination "Builds\manifest.json"
if (Test-Path $manifest) {
    New-Item -ItemType Directory -Force -Path (Split-Path $manifestDestination) | Out-Null
    Copy-Item $manifest $manifestDestination -Force
}

& $git -C $cloneRoot lfs track "proyectos/**/Builds/Packages/*.zip"
& $git -C $cloneRoot config user.name "Codex"
& $git -C $cloneRoot config user.email "codex@users.noreply.github.com"
& $git -C $cloneRoot add -A
& $git -C $cloneRoot add -f (($ProjectRelativePath -replace '\\','/') + "/Builds/Packages/*.zip")
if (-not (& $git -C $cloneRoot diff --cached --quiet)) {
    & $git -C $cloneRoot commit -m $CommitMessage
}
& $git -C $cloneRoot push origin $Branch
Write-Host "Publicación completada en $RepositoryUrl/tree/$Branch"
