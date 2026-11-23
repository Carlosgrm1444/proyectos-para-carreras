# Mejoras de UI arcade

## Distribución

- Superior izquierdo: puntuación, récord y combo.
- Superior central: oleada y tiempo transcurrido.
- Superior derecho: vida suavizada del Guardián y del Centro de Datos.
- Inferior: controles según el dispositivo, power-ups y estado del escudo.
- La ayuda completa permanece oculta y se abre con Select.

## Estilo y feedback

- Paleta futurista azul oscuro, paneles azul grisáceo, cian para el Centro, dorado para el Guardián, verde para solicitudes y rojo para daño.
- Tarjeta compacta de inicio y tutorial con botón A destacado.
- Números de puntuación con movimiento ascendente y desvanecimiento.
- Destellos perimetrales difuminados, avisos de oleada y alerta pulsante cuando el Centro está crítico.
- Pantalla de derrota con causa, puntuación, récord, oleada, tiempo y acción Reintentar.
- Iluminación ambiental y colores del escenario ajustados para mejorar contraste.

## Compatibilidad

- Escala virtual 1920×1080 con ajuste proporcional para 1280×720.
- Solo se admite mando Xbox.
- No se modificaron las reglas de combate, enemigos, puntuación ni generación de oleadas.

## Validación

- Build Windows generado sin errores de scripts.
- El HUD anterior fue sustituido en `GameBootstrap` por `ArcadeHudController`.
- Verificación visual realizada en 1920×1080 y 1280×720: paneles completos, márgenes consistentes y textos sin recortes.
- Capturas de comprobación: `Builds/Windows/ui-validation.png` y `Builds/Windows/ui-validation-1280x720.png`.
