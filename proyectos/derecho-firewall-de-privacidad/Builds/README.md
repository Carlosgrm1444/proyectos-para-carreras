# Builds

Estado verificado el 23 de noviembre de 2026:

- Windows 64-bit: OK. El ejecutable principal es `Builds/Windows/DerechoFirewall.exe`; el paquete completo ocupa aproximadamente 100 MB.
- WebGL: OK. `Builds/WebGL/index.html` y los archivos de `Builds/WebGL/Build` fueron generados.
- Linux 64-bit: pendiente. La instalación local de Unity 6.6 no tiene **Linux Build Support**.

Los paquetes ejecutables no se agregan al historial normal del repositorio. Publícalos como artefactos de GitHub Releases o mediante Git LFS. Los scripts `build-webgl.ps1` y `build-linux.ps1` permiten reproducir los builds cuando esté instalado el módulo correspondiente.
