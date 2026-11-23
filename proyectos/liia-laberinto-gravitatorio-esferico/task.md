# Estado de trabajo

## Alcance de la entrega actual

- [x] Juego de teclado: bola restringida a la superficie, laberintos PNG, movimiento con inercia, colisión, cámara local y giro visual.
- [x] Loop arcade: tres niveles, estrellas, checkpoint, trampas, tres vidas, reinicio, meta, puntuación, bono de rapidez y récord por nivel.
- [x] Presentación de software: pantalla inicial, selector de nivel, HUD, minimapa, formas además de color, efectos sonoros originales y retorno al modo de atracción.
- [x] Verificación automática de mapas, vidas, checkpoint, puntuación, victoria y récord; build Linux construido y probado en modo de humo.

## Evidencia manual disponible

- [x] La escena se abrió en Unity y se iteraron movimiento, cámara, peso percibido y orientación a partir de pruebas de Play con teclado.
- [x] Se comprobó visualmente la recogida de estrella, checkpoint, trampa y llegada a meta durante la iteración.
- [ ] Antes de un montaje público: pasada final con monitor y audio para apreciar legibilidad a distancia, minimapa y sonidos. No bloquea el cierre de mecánicas de software.

## Diferido explícitamente

- [ ] ESP32, botones físicos y diagnóstico de conexión.
- [ ] Integración con pantallas, proyección o esfera física.

Estos dos últimos grupos se abordarán en una etapa posterior: no forman parte de la entrega actual de mecánicas.
