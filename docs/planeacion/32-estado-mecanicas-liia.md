# Estado de mecánicas — LIIA

> Alcance acordado: terminar las mecánicas de juego. ESP32 y pantallas quedan para una etapa posterior.

## Mecánicas implementadas

| Mecánica | Implementación | Verificación disponible |
|---|---|---|
| Movimiento | Aceleración, velocidad máxima, resistencia y giro visual de bola. | Compilación Unity; requiere sensación final de prueba manual. |
| Restricción esférica | La bola se coloca matemáticamente sobre la superficie y no usa caída libre. | Revisada en juego durante iteración. |
| Colisiones | Las celdas de muro bloquean sólo el eje de movimiento correspondiente. | Incluida en controlador de bola. |
| Tres niveles | PNG `laberinto_01`, `_02`, `_03`; selector 1/2/3. | Validador confirmó ruta, inicio y meta de los tres. |
| Estrellas | +100 puntos; no se pueden recoger dos veces por intento. | Implementada mediante conjunto de celdas recolectadas. |
| Checkpoint | Guarda la celda azul más reciente. | Implementado. |
| Trampa / vidas | El bloque morado resta vida y reaparece en checkpoint/inicio; tres pérdidas reinician intento. | Implementado. |
| Meta | Suma llegada y bono de rapidez; congela partida y guarda récord. | Implementado. |
| Récord | Puntuación y tiempo por nivel, persistidos localmente. | Implementado con `PlayerPrefs` por índice de nivel. |
| Reinicio | `R` reinicia intento; inactividad vuelve a pantalla inicial. | Implementado. |
| Pantalla inicial | Selector de nivel, instrucciones e inicio con Enter/Espacio/dirección. | Implementado. |
| Audio | Tonos procedurales para eventos; `M` activa/silencia. | Compila; prueba auditiva pendiente. |
| Minimap | PNG del nivel y posición de jugador en HUD. | Compila; inspección visual pendiente. |

## Reglas del PNG

| Marca | Función |
|---|---|
| Oscuro | Muro |
| Claro | Piso |
| Verde | Inicio |
| Rojo | Meta |
| Amarillo | Estrella |
| Azul | Checkpoint |
| Morado | Trampa |

Cada mapa se valida automáticamente: exactamente un inicio, una meta y una ruta transitable entre ambos. La validación actual confirmó:

- Nivel 1: 1 estrella, 1 checkpoint, 1 trampa.
- Nivel 2: 2 estrellas, 1 checkpoint, 1 trampa.
- Nivel 3: 3 estrellas, 2 checkpoints, 3 trampas.

## Cierre de mecánicas

Para considerar esta etapa terminada sólo falta una prueba manual breve en el build actualizado:

1. Abrir cada nivel desde la pantalla inicial.
2. Confirmar que una estrella desaparece y suma 100.
3. Confirmar checkpoint azul y retorno desde trampa morada.
4. Agotar tres vidas y confirmar reinicio del intento.
5. Llegar a la meta y revisar récord individual por nivel.
6. Confirmar minimapa, audio y retorno por inactividad.

No se añadirá hardware, ESP32, VR/AR ni pantalla/proyección de esfera a esta entrega de mecánicas.

