# Instrucciones para Codex — siete minijuegos de FCACS

## Prioridad actual

**Siete prototipos jugables en 48 horas de trabajo**, no siete productos terminados. Empieza leyendo [21-sprint-48h-siete-videojuegos.md](21-sprint-48h-siete-videojuegos.md), luego la tarea de tu juego (22–28) y su diseño fuente (11–17). Los requisitos específicos de cada diseño prevalecen sobre ejemplos genéricos de `00`, y el sprint `21` sustituye el sprint histórico `20`.

## Reglas de edición

- **Equipo A** trabaja solo en `prototipos/equipo-a/` y rama `feat/mvp-48h-equipo-a`: Administración (23), Contador (24), Psicología (25), LIIA (26).
- **Equipo B** trabaja solo en `prototipos/equipo-b/` y rama `feat/mvp-48h-equipo-b`: Derecho (22), Comercio Exterior (27), Autotransporte (28).
- Cada carpeta de equipo es un proyecto Unity **independiente**. Crear `Assets/`, `Packages/`, `ProjectSettings/`, scripts/escenas y archivos `.meta` dentro de su proyecto. No configurar paquetes XR del equipo B en el proyecto A sin requerimiento técnico.
- **No sobrescribir trabajo del otro equipo, no hacer push directo a main, no hacer merge sin revisión**. Si una rama ya existe, recuperar sus cambios antes de escribir, evitar `--force`.
- Mantener dependencias, versión Unity, plataforma, controles y procedimiento de ejecución documentados. Usar ramas/PR para integrar, no mover manualmente los proyectos entre computadoras.
- **No subir** `Library/`, `Temp/`, cachés, builds, credenciales ni grabaciones de cámara/usuarios.

## Cómo desarrollar

1. Dar prioridad a una **vertical slice** de cada juego antes de extras. Siempre debe ser posible iniciar, controlar, obtener consecuencia, perder/ganar según reglas, reiniciar y guardar récord.
2. Usar primitivas y UI funcional sin arte final. Añadir unos pocos efectos sonoros **originales o con licencia válida** para acción, daño y resultado; mute.
3. Reutilizar sólo un sistema común **mínimo dentro de cada proyecto**: estado de juego, puntuación o tiempo, vidas, mejor marca local, reinicio, audio.
4. Guardar los parámetros de balance en Inspector/configuración. Distinguir reglas **aprobadas** de números **provisionales**.
5. Usar teclado/ratón como respaldo cuando AR, VR, MediaPipe, ESP32 o videomapping no estén disponibles. **No afirmar que se probó un dispositivo** si se utilizó sólo un simulador/monitor.
6. Antes de cambios grandes, ejecutar pruebas locales de escena si Unity está instalado. Si el editor no está accesible, indicar claramente que falta compilar/probar.
7. Al cerrar una tarea: README en la carpeta del proyecto, checklist, lista de bloqueos, evidencia de ejecución si existe, commit, push y PR.

## Paquetes específicos

| Juego | Codex | Diseño de referencia |
|---|---|---|
| Derecho | [22](22-codex-mvp-derecho.md) | [14](14-derecho-firewall-de-privacidad.md) |
| Administración | [23](23-codex-mvp-administracion.md) | [11](11-administracion-almacen-al-limite-hibrido.md) |
| Contador Público | [24](24-codex-mvp-contador.md) | [13](13-contador-publico-flujo-caja-ar.md) |
| Psicología | [25](25-codex-mvp-psicologia.md) | [16](16-psicologia-atencion-bajo-ataque-mediapipe.md) |
| LIIA | [26](26-codex-mvp-liia.md) | [15](15-liia-laberinto-gravitatorio-esferico.md) |
| Comercio Exterior | [27](27-codex-mvp-comercio.md) | [12](12-comercio-exterior-clasificador-en-caos-vr.md) |
| Autotransporte | [28](28-codex-mvp-autotransporte.md) | [17](17-autotransporte-carga-equilibrada-grua-vr.md) |

## Criterio de verdad

Un archivo de diseño **no** demuestra que el videojuego esté construido. Un prototipo con mouse de respaldo **no** equivale a AR/VR/MediaPipe/proyección verificada. Los siete MVPs deben registrar pruebas y limitaciones reales.
