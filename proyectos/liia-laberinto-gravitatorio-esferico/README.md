# LIIA — Laberinto gravitatorio esférico

Juego de teclado para **Unity 2022.3.62f3 LTS**. El nivel se genera al iniciar `Assets/Scenes/LIIA.unity`; cada PNG se proyecta sobre una superficie esférica continua con costura horizontal de 360°.

## Cómo abrirlo

1. Instalar/abrir Unity 2022.3.62f3 con soporte de escritorio para la plataforma elegida.
2. Abrir esta carpeta como proyecto Unity.
3. Abrir `Assets/Scenes/LIIA.unity`.
4. Pulsar Play.

Si la escena no construye el nivel automáticamente, seleccionar `LIIAPrototype` y ejecutar el menú contextual **Build LIIA prototype**.

## Controles

- Flechas o WASD: mover la bola por el laberinto.
- Combinar dos direcciones para avanzar en diagonal donde el mapa lo permita.
- `R`: reiniciar el intento.
- En pantalla inicial: `1` a `5` cambia el nivel; Enter, Espacio o una dirección inicia la partida.
- En victoria: `R` repite; Enter, Espacio o `N` avanza al siguiente nivel.
- `M`: activar o silenciar los efectos de sonido.

La entrada actual es teclado. No existe todavía comunicación con ESP32 ni integración con la esfera física.

## Loop implementado

- Los cinco PNG de `Assets/Resources/Maps/` son la fuente de verdad de los niveles.
- Los píxeles oscuros son muros; blanco es piso; verde es inicio; rojo es meta; amarillo es estrella; azul es checkpoint y morado es trampa.
- El nivel transforma los muros del PNG en un único mesh curvo extruido, con `MeshCollider` estático.
- La bola se mueve por celdas transitables y se posiciona matemáticamente sobre la superficie: no puede caer ni despegarse.
- La cámara permanece sobre la bola siguiendo el arco exterior de la esfera; el planeta no rota durante la partida.
- La primera y la última columna del PNG son vecinas: la bola cruza la parte posterior del planeta sin corte ni teletransporte visible.
- Estrellas de puntuación, checkpoint, trampa, tres vidas, cronómetro, récord local y pantalla de victoria.
- Modo de atracción para museo: pantalla inicial, instrucciones, selección de nivel y regreso automático tras la victoria.
- Efectos sonoros procedurales originales para inicio, estrella, checkpoint, trampa y meta.

## Niveles finales

| # | Nombre | Ruta segura mínima | Estrellas | Checkpoints | Trampas | Cobertura |
|---|---|---:|---:|---:|---:|---|
| 1 | Órbita de arranque | 46 celdas | 3 | 1 | 1 | 360° × 132° |
| 2 | Doble hélice | 80 celdas | 4 | 2 | 2 | 360° × 138° |
| 3 | Cruce de meridianos | 110 celdas | 5 | 2 | 3 | 360° × 144° |
| 4 | Red neuronal | 120 celdas | 6 | 3 | 4 | 360° × 146° |
| 5 | Singularidad | 146 celdas | 7 | 3 | 5 | 360° × 150° |

La proyección evita únicamente los polos exactos, donde una cuadrícula rectangular colapsaría. El quinto nivel cubre latitudes de −75° a +75° y toda la circunferencia.

## Estructura principal

- `Assets/Resources/Maps/laberinto_01.png` a `_05.png`: niveles finales editables.
- `Assets/Editor/CreateLIIAMazePng.cs`: genera y valida los PNG desde matrices legibles.
- `MazeLevelCatalog.cs`: catálogo y selección de niveles.
- `SphericalMazeLayout.cs`: interpreta el PNG y lo proyecta sobre la esfera.
- `SphericalMazeMeshBuilder.cs`: construye el piso y los muros extruidos combinados.
- `MazeBallController.cs`: movimiento y colisión determinista de la bola.
- `BallFollowCamera.cs`: cámara cenital local, sin atravesar el planeta.
- `GameFlow.cs`: vidas, estados, puntuación, reinicio y récord.
- `LIIAHUD.cs`: HUD de prueba sin assets externos.

## Editar o generar un mapa

Los archivos `laberinto_01.png` a `_05.png` pueden editarse con un editor de imagen que conserve píxeles sólidos y sus dimensiones de 32×21. Para reconstruir la serie desde código, ejecutar **LIIA → Generar mapas PNG finales**. Para comprobar inicio/meta, marcadores, costura, conectividad y una ruta segura que no atraviese trampas, ejecutar **LIIA → Validar mapas PNG activos**. El juego rechaza un mapa inválido en lugar de construir un nivel roto.

## Estado de verificación

El editor está instalado en el segundo SSD, fuera del repositorio: `/media/carlos-rodriguez/nvme0n1p1/repos/personal/Unity/Hub/Editor/2022.3.62f3/Editor/Unity`. El verificador en batch comprueba los cinco mapas, su progresión mínima, la costura de 360°, el ciclo de estrella/checkpoint/trampa, el reinicio, la victoria, la puntuación y el récord. Antes de una exposición debe hacerse una pasada en Play con teclado, monitor y audio para comprobar balance, minimapa y sonido.

No se deben presentar como probadas las integraciones de ESP32, VR, proyección o esfera física.

## Generar build de exposición

En Unity, usar **LIIA → Build → Linux x86_64** o **Windows x86_64**. Los resultados se generan en `Builds/`, carpeta ignorada por Git. El build de Linux es el que se valida en este equipo; el de Windows requiere que el módulo de plataforma esté instalado en Unity antes de usarlo.

## Verificar mecánicas

En Unity, ejecutar **LIIA → Verificar mecánicas en batch**. El verificador revisa los cinco PNG finales, costura, cobertura, progresión y el loop central de estrella, checkpoint, trampa y reinicio por tres vidas. También puede ejecutarse desde línea de comandos con el método `LIIA.SphereMaze.Editor.VerifyLIIAMechanics.Run`.
