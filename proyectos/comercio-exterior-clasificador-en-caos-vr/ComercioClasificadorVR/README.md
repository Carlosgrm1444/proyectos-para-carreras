# Comercio Clasificador VR

Base técnica del minijuego **Clasificador en caos VR**.

## Estado

Fase 1 en progreso. El proyecto usa Unity 6000.6.10f1, plantilla URP 3D y Windows x64 como plataforma inicial. La especificación jugable está en `../PLAN-POR-FASES.md` y las decisiones de la Fase 0 en `../FASE-0-DECISIONES.md`.

Ya están instalados OpenXR 1.19.0, XR Interaction Toolkit 3.7.0, Pipeline 1.0.1 y Cinemachine 3.1.7. Las escenas base son `Assets/Scenes/Bootstrap.unity` y `Assets/Scenes/Comercio.unity`.

## Apertura

1. Abrir esta carpeta desde Unity Hub con Unity 6000.6.10f1.
2. Esperar a que termine la importación y la resolución de paquetes.
3. Configurar el runtime OpenXR del visor Meta/Quest antes de probar VR.
4. No subir `Library`, `Temp`, `Logs`, `obj` ni builds al repositorio.

## Estructura inicial

`Assets/_Project` contiene el código y datos propios del juego. La lógica se separará en Core, XR, Cargo, Classification, Enemies, Input y UI. Los nombres de entrada serán acciones abstractas para conservar compatibilidad entre visores.

## Próximo hito

Configurar el perfil de interacción Meta/Quest dentro de OpenXR, añadir locomoción continua y giro por intervalos, y validar el rig con el visor conectado. El XR Origin base ya está en `Assets/Scenes/Comercio.unity`. Los bloqueos o verificaciones de hardware están en `FALTANTES.md`.
