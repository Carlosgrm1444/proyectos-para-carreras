# Fase 3 — Progresión, puntuación y supervivencia

## Implementado

- Oleada inicial 1; aumenta cada 20 segundos.
- La cadencia de solicitudes baja progresivamente hasta 1.35 segundos.
- La cadencia de amenazas baja progresivamente hasta 1.6 segundos.
- Tipos de amenaza combinables durante la misma partida.
- Solicitud salvada: +10 puntos y aumenta la racha.
- Solicitud perdida: -5 puntos y rompe la racha.
- Amenaza derrotada: +2 puntos, aumenta la racha y puede generar un power-up.
- Power-ups con probabilidad base del 8%; la racha puede aumentarla hasta 20%, sin garantizar drops.
- Curación: restaura 2 puntos tanto al guardián como al Centro, sin superar sus máximos.
- Velocidad: aumenta 35% el movimiento del guardián durante 8 segundos.
- Daño: aumenta 50% el daño de espada durante 8 segundos.
- Récord local guardado con `PlayerPrefs` y visible en el HUD.
- HUD con puntos, récord, racha, oleada y barras de vida de guardián/Centro.

## Reglas de la racha

La racha inicia en cero, aumenta al salvar solicitudes o derrotar amenazas y vuelve a cero cuando se pierde una solicitud. No se multiplica la puntuación todavía; por ahora controla la probabilidad de power-up y queda visible para balancearla.

## Prueba manual de salida

- [ ] Jugar más de 20 segundos y confirmar que cambia la oleada.
- [ ] Confirmar que la aparición de amenazas se acelera en oleadas posteriores.
- [ ] Confirmar que salvar una solicitud incrementa puntos y racha.
- [ ] Confirmar que perder una solicitud resta puntos y reinicia la racha.
- [ ] Recoger los tres tipos de power-up y observar sus efectos.
- [ ] Perder una partida y confirmar que el récord queda visible al reiniciar.
- [ ] Confirmar que el HUD no tapa el área jugable en la resolución objetivo.

## Pendiente para la siguiente iteración

- Balancear dificultad con una partida real de dos a cinco minutos.
- Añadir power-up de alcance o proyectil mejorado.
- Mantener supervivencia infinita; la salida voluntaria con Q durante pausa ya existe.
- Extender la dificultad después de alcanzar las cadencias mínimas para evitar que la curva se estanque.
