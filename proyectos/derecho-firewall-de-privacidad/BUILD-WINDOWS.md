# Build Windows

- Unity: `6000.6.10f1`.
- Plataforma: Windows, Mac, Linux Standalone.
- Escena incluida: `Assets/Scenes/Main.unity`, con `GameBootstrap` configurado.
- Entrada: Input Manager clásico; no instalar Input System.
- Audio: módulo integrado `com.unity.modules.audio`.

## Procedimiento

1. Abrir el proyecto con Unity 6000.6.10f1.
2. Abrir `File > Build Profiles`.
3. Seleccionar Windows; `Main.unity` ya está incluida en Build Settings.
4. Construir en una carpeta nueva, fuera de `Library`.
5. Probar el ejecutable sin Unity y completar `QA-MATRIZ.md`.
6. Guardar una copia offline del ejecutable y su carpeta `_Data`.

## Automatización

Desde Unity: `Firewall > Build Windows`.

Desde línea de comandos, con Unity cerrado:

```text
Unity.exe -batchmode -quit -projectPath "RUTA\derecho-firewall-de-privacidad" -executeMethod BuildAutomation.BuildWindows -logFile build.log
```

La salida se crea en `Builds/Windows/DerechoFirewall.exe`.

Estado actual: build generado correctamente en 2026-11-22. El log reporta `Build Successful`, el ejecutable arranca sin abrir Unity y la captura de prueba confirma renderizado correcto sin superficies magenta.

También puedes ejecutar `build-windows.ps1`; el script bloquea el intento si detecta que este proyecto sigue abierto en Unity.

## Entrega mínima

- Ejecutable Windows y carpeta de datos.
- `OPERACION-EXHIBICION.md`.
- Versión de Unity y fecha de build.
- Copia sin cuentas ni datos personales.

