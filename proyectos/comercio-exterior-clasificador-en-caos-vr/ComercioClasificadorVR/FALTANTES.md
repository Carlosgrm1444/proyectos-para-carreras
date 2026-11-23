# Faltantes y bloqueos — Comercio Clasificador VR

Este documento registra únicamente elementos que no pueden cerrarse de forma fiable desde la máquina actual.

## Pendientes de hardware

- [ ] Registrar el modelo exacto del visor Meta Quest.
- [ ] Confirmar si se probará por Quest Link, Air Link u otro runtime.
- [ ] Confirmar espacio físico, altura de cámara y modo sentado/de pie.
- [ ] Medir la frecuencia estable del visor durante una escena VR simple.

## Pendientes de Unity/XR

- [x] Esperar a que el Editor termine de importar y responda por `com.unity.pipeline`.
- [x] Instalar mediante Package Manager `com.unity.xr.openxr` 1.19.0 y `com.unity.xr.interaction.toolkit` 3.7.0.
- [ ] Verificar que Meta/Oculus sea el runtime OpenXR activo dentro de Unity.
- [ ] Crear y probar el perfil de interacción de los controladores del visor.
- [x] Crear las escenas `Bootstrap.unity` y `Comercio.unity`.
- [x] Crear un XR Origin base con cámara, offset y seguimiento de pose en `Comercio.unity`.
- [ ] Añadir y probar locomoción continua, giro por intervalos y recentrado con los controles reales.

## Base creada en la Fase 1

- [x] Proyecto URP 3D `ComercioClasificadorVR` con Unity 6000.6.10f1.
- [x] Paquete Pipeline 1.0.1 y Cinemachine 3.1.7.
- [x] Estructura inicial `Assets/_Project/Scripts/Core`.
- [x] Estados base de sesión: inicio, tutorial, juego, pausa, resultados y derrota.
- [x] Configuración editable de cola, integridad, daño, repulsión y puntuación.
- [x] Escenas base creadas con cámara y luz de plantilla.
- [x] Escena `Comercio.unity` preparada con `XR Origin`, `Camera Offset` y `Main Camera`.

## Pendientes de contenido

- [ ] Confirmar el catálogo final de mercancías y sus tres opciones.
- [ ] Revisar explicaciones educativas antes de convertirlas en contenido definitivo.
- [ ] Crear iconos y prefabs de las categorías.

## Verificación pendiente

La compilación de scripts terminó sin errores. Unity conserva un aviso/error de importación de un shader interno de Probe Volume de URP (`TraceVirtualOffset.urtshader`); no bloquea la compilación, pero debe revisarse antes de la primera build. También debe comprobarse que el perfil OpenXR activo dentro de Unity coincida con Meta Quest; Windows ya detecta el runtime de Meta.

## Criterio para cerrar este documento

Se marcarán los puntos conforme se comprueben dentro de Unity y con el visor conectado. No se considerará que XR está validado sólo porque Windows detecte el dispositivo USB o porque el runtime OpenXR esté instalado.
