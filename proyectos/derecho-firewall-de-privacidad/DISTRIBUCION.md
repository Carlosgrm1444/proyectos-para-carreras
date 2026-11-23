# Plan de distribución multiplataforma

## Targets

1. Windows 64-bit: `build-windows.ps1` → `Builds/Windows/DerechoFirewall.exe`.
2. WebGL: `build-webgl.ps1` → `Builds/WebGL/index.html` y sus archivos auxiliares.
3. Linux 64-bit: `build-linux.ps1` → `Builds/Linux/DerechoFirewall.x86_64`.

## Verificación

- Cada build usa `Assets/Scenes/Main.unity`.
- El target se fija desde la línea de comandos y también desde `BuildAutomation`.
- Windows fue generado correctamente.
- WebGL fue generado correctamente; existe `Builds/WebGL/index.html` con sus archivos auxiliares.
- Linux requiere instalar `Linux Build Support` para Unity 6.6.

## Publicación

- La fuente y los scripts pueden subirse a la rama del repositorio mediante GitHub.
- Los ejecutables de Windows, WebGL y Linux deben publicarse como artefactos/release o Git LFS; no conviene guardarlos en el historial normal por su tamaño.
- `package-builds.ps1` genera ZIPs reproducibles para los targets existentes. Windows y WebGL están empaquetados localmente; Linux sigue pendiente.

