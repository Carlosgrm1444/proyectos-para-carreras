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
- [x] Agregar `XR Interaction Manager` a `Comercio.unity`.
- [x] Agregar un cubo de carga de prueba con collider, Rigidbody y `XR Grab Interactable`.
- [x] Preparar dos objetos de controlador con interactor directo para izquierda y derecha.
- [x] Desactivar la camara duplicada de la plantilla.
- [x] Configurar Build Settings con `Bootstrap.unity` como escena 0 y `Comercio.unity` como escena 1.
- [ ] Conectar el boton de inicio de Bootstrap con la carga de `Comercio.unity`.
- [ ] Probar locomocion, giro y recentrado con controles reales.
- [ ] Enlazar acciones XRI modernas a los interactors; la clase `XRController` usada por la plantilla aparece obsoleta en XRI 3.7.
- [ ] Agregar/probar interactor de rayo para interacciones lejanas.
- [ ] Probar agarrar, soltar y lanzar el cubo con ambos mandos.
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

Tambien contiene `XR Interaction Manager` y un objeto `Cube` interactivo para validar el primer flujo de agarre. Todavia no se marcaran los interactors ni el mapeo de acciones como completos hasta probarlos con el visor.

## Criterio de cierre

Marcar pendientes solo despues de comprobarlos dentro de Unity y con el visor conectado.
