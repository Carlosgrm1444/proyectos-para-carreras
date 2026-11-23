# LIIA — Laberinto gravitatorio esférico

Vertical slice inicial para **Unity 2022.3.62f3 LTS**. El prototipo se genera al iniciar `Assets/Scenes/LIIA.unity`; su mapa se define mediante un PNG y se proyecta sobre una banda del planeta.

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
- En pantalla inicial: `1`, `2` o `3` cambia el nivel; Enter, Espacio o una dirección inicia la partida.
- `M`: activar o silenciar los efectos de sonido.

La entrada actual es teclado. No existe todavía comunicación con ESP32 ni integración con la esfera física.

## Loop implementado

- `Assets/Resources/Maps/laberinto_01.png` es la fuente de verdad del mapa.
- Los píxeles oscuros son muros; blanco es piso; verde es inicio; rojo es meta; amarillo es estrella; azul es checkpoint y morado es trampa.
- El nivel transforma los muros del PNG en un único mesh curvo extruido, con `MeshCollider` estático.
- La bola se mueve por celdas transitables y se posiciona matemáticamente sobre la superficie: no puede caer ni despegarse.
- La cámara permanece sobre la bola siguiendo el arco exterior de la esfera; el planeta no rota durante la partida.
- Estrellas de puntuación, checkpoint, trampa, tres vidas, cronómetro, récord local y pantalla de victoria.
- Modo de atracción para museo: pantalla inicial, instrucciones, selección de nivel y regreso automático tras la victoria.
- Efectos sonoros procedurales originales para inicio, estrella, checkpoint, trampa y meta.

## Estructura principal

- `Assets/Resources/Maps/laberinto_01.png`, `_02.png` y `_03.png`: niveles editables del prototipo.
- `Assets/Editor/CreateLIIAMazePng.cs`: genera y valida los PNG desde matrices legibles.
- `MazeLevelCatalog.cs`: catálogo y selección de niveles.
- `SphericalMazeLayout.cs`: interpreta el PNG y lo proyecta sobre la esfera.
- `SphericalMazeMeshBuilder.cs`: construye el piso y los muros extruidos combinados.
- `MazeBallController.cs`: movimiento y colisión determinista de la bola.
- `BallFollowCamera.cs`: cámara cenital local, sin atravesar el planeta.
- `GameFlow.cs`: vidas, estados, puntuación, reinicio y récord.
- `LIIAHUD.cs`: HUD de prueba sin assets externos.

## Editar o generar un mapa

Los archivos `laberinto_01.png`, `_02.png` y `_03.png` pueden editarse con un editor de imagen que conserve píxeles sólidos y sus dimensiones. Para volver a generar los ejemplos desde código, abrir Unity y ejecutar **LIIA → Generar mapas PNG de ejemplo**. Para comprobar que cada uno tenga un solo inicio, una meta y una ruta válida, ejecutar **LIIA → Validar mapas PNG activos**. Al iniciar el juego, el nivel se reconstruye desde ese PNG y se niega a construir un mapa inválido.

## Estado de verificación

El editor está instalado en el segundo SSD, fuera del repositorio: `/media/carlos-rodriguez/nvme0n1p1/repos/personal/Unity/Hub/Editor/2022.3.62f3/Editor/Unity`. El verificador en batch comprueba los tres mapas, el ciclo de estrella/checkpoint/trampa, el reinicio, la victoria, la puntuación y el récord. También se construyó y arrancó en modo de humo el ejecutable Linux. Antes de una exposición debe hacerse una pasada en Play con teclado, monitor y audio para comprobar sensaciones, minimapa y sonido.

No se deben presentar como probadas las integraciones de ESP32, VR, proyección o esfera física.

## Generar build de exposición

En Unity, usar **LIIA → Build → Linux x86_64** o **Windows x86_64**. Los resultados se generan en `Builds/`, carpeta ignorada por Git. El build de Linux es el que se valida en este equipo; el de Windows requiere que el módulo de plataforma esté instalado en Unity antes de usarlo.

## Verificar mecánicas

En Unity, ejecutar **LIIA → Verificar mecánicas en batch**. El verificador revisa los tres PNG de nivel y el loop central de estrella, checkpoint, trampa y reinicio por tres vidas. También puede ejecutarse desde línea de comandos con el método `LIIA.SphereMaze.Editor.VerifyLIIAMechanics.Run`.
