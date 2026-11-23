# Guía de operación — Estación LIIA

> Guía de una página para una persona operadora durante la exhibición.

## Antes de abrir al público

1. Verificar que el monitor, computadora y controlador estén conectados de forma estable.
2. Iniciar el ejecutable `LIIA` del build de exposición o abrir el proyecto en Unity para pruebas.
3. Confirmar que aparece la pantalla **LIIA · Laberinto gravitatorio**.
4. Pulsar `1`, `2` o `3` para comprobar que el selector de nivel responde.
5. Pulsar Enter, mover la bola, recoger una estrella y tocar el checkpoint.
6. Pulsar `M` y confirmar que el audio se silencia/reactiva.
7. Pulsar `R` y confirmar que reinicia el intento.

## Instrucción para visitantes

> “Usa las flechas para mover la esfera amarilla. Llega a la meta roja. La esfera amarilla da puntos, el pilar azul guarda tu avance y el bloque morado te hace perder una vida.”

No es necesario explicar más al inicio: la leyenda aparece en pantalla.

## Controles de operación

| Acción | Tecla |
|---|---|
| Iniciar partida | Enter, Espacio o cualquier flecha/WASD |
| Mover | Flechas o WASD |
| Reiniciar intento | R |
| Cambiar de nivel en pantalla inicial | 1, 2, 3 |
| Silenciar / activar sonido | M |
| Mostrar diagnóstico técnico | F3 |

Tras 45 segundos sin entrada, una partida vuelve sola a la pantalla inicial. Después de una victoria, vuelve automáticamente tras 10 segundos.

## Si algo falla

| Situación | Acción inmediata |
|---|---|
| Un visitante quedó atrapado | Pulsar `R`. |
| No se oye sonido | Pulsar `M`; revisar volumen del sistema. |
| La pantalla no reacciona al controlador físico | Probar teclado; si funciona, continuar con teclado y revisar ESP32/cable fuera de la sesión. |
| El juego muestra un mapa vacío o mensaje de mapa inválido | Cerrar, abrir el build respaldado; en desarrollo ejecutar **LIIA → Validar mapas PNG activos**. |
| La aplicación se congela | Cerrar la aplicación y abrir de nuevo el build. |

## Cierre

1. Esperar a que no haya una partida activa.
2. Cerrar la aplicación de forma normal.
3. Apagar monitor y computadora según el procedimiento del recinto.
4. No desconectar el cableado tirando de los botones o de la esfera física.

## Límites conocidos

- El teclado es el control de respaldo garantizado.
- La integración de ESP32 y la función visual de la esfera física requieren validación de hardware específica.
- La salida visual garantizada es el monitor; no se debe prometer proyección o pantalla sobre la esfera sin una prueba real.

