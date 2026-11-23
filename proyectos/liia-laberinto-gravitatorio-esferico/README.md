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

La entrada actual es teclado. No existe todavía comunicación con ESP32 ni integración con la esfera física.

## Loop implementado

- `Assets/Resources/Maps/laberinto_01.png` es la fuente de verdad del mapa.
- Los píxeles oscuros son muros; blanco es piso; verde es inicio; rojo es meta; amarillo es estrella; azul es checkpoint y morado es trampa.
- El nivel transforma los muros del PNG en un único mesh curvo extruido, con `MeshCollider` estático.
- La bola se mueve por celdas transitables y se posiciona matemáticamente sobre la superficie: no puede caer ni despegarse.
- La cámara permanece sobre la bola siguiendo el arco exterior de la esfera; el planeta no rota durante la partida.
- Estrellas de puntuación, checkpoint, trampa, tres vidas, cronómetro, récord local y pantalla de victoria.

## Estructura principal

- `Assets/Resources/Maps/laberinto_01.png`: diseño editable del primer laberinto.
- `Assets/Editor/CreateLIIAMazePng.cs`: genera el PNG de ejemplo desde una matriz legible.
- `SphericalMazeLayout.cs`: interpreta el PNG y lo proyecta sobre la esfera.
- `SphericalMazeMeshBuilder.cs`: construye el piso y los muros extruidos combinados.
- `MazeBallController.cs`: movimiento y colisión determinista de la bola.
- `BallFollowCamera.cs`: cámara cenital local, sin atravesar el planeta.
- `GameFlow.cs`: vidas, estados, puntuación, reinicio y récord.
- `LIIAHUD.cs`: HUD de prueba sin assets externos.

## Editar o generar un mapa

El archivo `laberinto_01.png` puede editarse con un editor de imagen que conserve píxeles sólidos y sus dimensiones. Para volver a generar el mapa de ejemplo desde código, abrir Unity y ejecutar **LIIA → Generar mapa PNG de ejemplo**. Para comprobar que tenga un solo inicio, una meta y una ruta válida, ejecutar **LIIA → Validar mapa PNG activo**. Al iniciar el juego, el nivel se reconstruye desde ese PNG y se niega a construir un mapa inválido.

## Estado de verificación

El editor está instalado en el segundo SSD, fuera del repositorio: `/media/carlos-rodriguez/nvme0n1p1/repos/personal/Unity/Hub/Editor/2022.3.62f3/Editor/Unity`. La importación y compilación batch terminaron con salida 0; la escena ya se abrió en la interfaz gráfica y falta completar la prueba manual de Play con teclado.

No se deben presentar como probadas las integraciones de ESP32, VR, proyección o esfera física.
