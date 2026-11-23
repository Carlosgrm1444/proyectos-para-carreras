# Distribución multiplataforma

Proyecto Unity 6.6: Firewall de Privacidad.

## Artefactos verificados

- Windows 64-bit: generado localmente en `Builds/Windows/DerechoFirewall.exe`.
- WebGL: generado localmente en `Builds/WebGL/index.html` con sus carpetas `Build` y `TemplateData`.
- Linux 64-bit: preparado mediante `build-linux.ps1`, pero requiere instalar el módulo **Linux Build Support** de Unity Hub.

## Compilar

Desde la carpeta del proyecto:

```powershell
.uild-windows.ps1
.uild-webgl.ps1
.uild-linux.ps1
```

Los scripts usan `Assets/Scenes/Main.unity` y escriben el log dentro de `Builds/<plataforma>/build.log`.

## Publicación

Los builds deben distribuirse como artefactos de Release o Git LFS. No conviene guardarlos en el historial normal del repositorio por su tamaño. El código fuente y los scripts de compilación sí están en esta rama.
