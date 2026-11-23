# Estado de mecánicas — LIIA

> Alcance acordado: terminar las mecánicas de juego. ESP32 y pantallas quedan para una etapa posterior.

## Mecánicas implementadas

| Mecánica | Implementación | Verificación disponible |
|---|---|---|
| Movimiento | Aceleración, velocidad máxima, resistencia y giro visual de bola. | Compilación Unity; requiere sensación final de prueba manual. |
| Restricción esférica | La bola se coloca matemáticamente sobre la superficie y no usa caída libre. | Revisada en juego durante iteración. |
| Colisiones | Las celdas de muro bloquean sólo el eje de movimiento correspondiente. | Incluida en controlador de bola. |
| Cinco niveles finales | PNG `laberinto_01` a `_05`; selector 1–5 y avance desde victoria. | Validador confirmó ruta segura, progresión y costura de 360° en los cinco. |
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

Cada mapa se valida automáticamente: exactamente un inicio, una meta, marcadores alcanzables y una ruta segura que no exige atravesar trampas. La validación actual confirmó:

- Nivel 1: ruta mínima 46; 3 estrellas, 1 checkpoint y 1 trampa.
- Nivel 2: ruta mínima 80; 4 estrellas, 2 checkpoints y 2 trampas.
- Nivel 3: ruta mínima 110; 5 estrellas, 2 checkpoints y 3 trampas.
- Nivel 4: ruta mínima 120; 6 estrellas, 3 checkpoints y 4 trampas.
- Nivel 5: ruta mínima 146; 7 estrellas, 3 checkpoints y 5 trampas.

Todos usan una costura horizontal continua de 360°. La cobertura vertical crece de 132° a 150° para utilizar progresivamente casi toda la superficie sin colapsar la cuadrícula en los polos exactos.

## Cierre de mecánicas

La lógica de estas mecánicas queda comprobada automáticamente. Antes de instalar la estación se requiere una pasada manual breve en un monitor con audio para confirmar la presentación física:

1. Abrir cada nivel desde la pantalla inicial.
2. Confirmar que una estrella desaparece y suma 100.
3. Confirmar checkpoint azul y retorno desde trampa morada.
4. Agotar tres vidas y confirmar reinicio del intento.
5. Llegar a la meta y revisar récord individual por nivel.
6. Confirmar minimapa, audio y retorno por inactividad.

No se añadirá hardware, ESP32, VR/AR ni pantalla/proyección de esfera a esta entrega de mecánicas.

## Evidencia automatizada

El verificador de proyecto **LIIA → Verificar mecánicas en batch** se ejecutó correctamente el 23 de noviembre de 2026. Comprueba:

- Los cinco PNG: inicio y meta únicos, ruta segura, marcadores alcanzables, costura continua y dificultad mínima esperada.
- Inicio en modo de atracción y comienzo limpio con tres vidas.
- Restricción radial de la bola a la superficie esférica.
- Una sola puntuación por estrella.
- Retorno al último checkpoint después de una trampa.
- Reinicio integral al agotar las tres vidas.
- Victoria, puntuación de estrella/meta/bono y guardado del récord individual.
- Inmutabilidad del intento después de alcanzar la meta.

Además, se construyó el ejecutable Linux actualizado y permaneció activo durante una prueba de humo de 12 segundos sin excepciones fatales. La comprobación visual y auditiva no se falsea con una ejecución sin GPU: se realiza en el monitor final de la estación.

