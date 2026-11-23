# Memoria de trabajo

- Estado actual: vertical slice de monitor con mecánicas completas de software. El mapa se genera en tiempo de ejecución mediante `LIIAPrototypeBootstrap` a partir de PNGs.
- La esfera física aparente es reflectante y no está confirmada como pantalla; integración física y ESP32 están expresamente diferidas.
- Confirmado: movimiento cinemático anclado a la esfera, cinco niveles finales con costura de 360°, tres vidas, cuatro direcciones de teclado, estrellas, checkpoints, trampas, meta, récord de puntuación y desempate por tiempo.
- La escena se probó visualmente en Play durante la iteración de movimiento y cámara. El verificador batch comprueba mapas, ciclo de juego, puntuación y récord; el build Linux se construyó y pasó una prueba de humo.
- No hay arte final ni hardware integrado. Antes del montaje sólo queda una prueba de presentación con monitor y audio.
