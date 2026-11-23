# Faltantes y bloqueos - Comercio Clasificador VR

Este documento registra elementos que no pueden cerrarse sin el visor y sus controles.

## Pendientes de hardware

- [ ] Registrar el modelo exacto del visor Meta Quest.
- [ ] Confirmar Quest Link, Air Link u otro runtime.
- [ ] Confirmar espacio fisico, altura de camara y modo sentado/de pie.
- [ ] Medir la frecuencia estable del visor durante una escena VR simple.

## Pendientes de Unity/XR

- [x] Instalar OpenXR 1.19.0 y XR Interaction Toolkit 3.7.0.
- [ ] Verificar que Meta/Oculus sea el runtime OpenXR activo dentro de Unity.
- [ ] Crear y probar el perfil de interaccion de los controladores.
- [x] Crear `Bootstrap.unity` y `Comercio.unity`.
- [x] Crear XR Origin con camara, offset y seguimiento de pose.
- [x] Anadir locomocion continua y giro por intervalos al XR Origin.
- [x] Preparar suelo con colision y seis obstaculos de prueba.
- [x] Desactivar la camara duplicada de la plantilla.
- [ ] Probar locomocion, giro y recentrado con controles reales.
- [ ] Confirmar mapeo final y ajustar velocidad/angulo tras la prueba de comodidad.

## Base creada en Fase 1

- [x] Proyecto URP 3D `ComercioClasificadorVR` con Unity 6000.6.10f1.
- [x] Estados base de sesion: inicio, tutorial, juego, pausa, resultados y derrota.
- [x] Configuracion editable de cola, integridad, dano, repulsion y puntuacion.
- [x] Escena `Comercio.unity` preparada con XR Origin, Camera Offset y Main Camera.

## Pendientes de contenido

- [ ] Confirmar catalogo final de mercancias y sus tres opciones.
- [ ] Revisar explicaciones educativas antes de convertirlas en contenido definitivo.
- [ ] Crear iconos y prefabs de las categorias.

## Verificacion pendiente

La compilacion de scripts termino sin errores (`compilationFailed: false`). Unity conserva un error de importacion de un shader interno de Probe Volume de URP (`TraceVirtualOffset.urtshader`); no bloquea la compilacion ni la configuracion del escenario, pero debe revisarse antes de la primera build.

La escena contiene `LocomotionMediator`, `ContinuousMoveProvider` y `SnapTurnProvider`. La validacion automatica confirma que estan guardados; no puede confirmar comodidad, tracking ni botones sin el visor conectado.

## Criterio de cierre

Marcar pendientes solo despues de comprobarlos dentro de Unity y con el visor conectado.
