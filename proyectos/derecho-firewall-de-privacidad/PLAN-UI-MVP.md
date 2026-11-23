# Planeación de mejora de UI

## Objetivo
Hacer que el estado del juego, las prioridades defensivas y los controles se entiendan de inmediato durante una partida.

## Fases

1. **Jerarquía del HUD (implementada):** métricas arriba a la izquierda, objetivos y recursos separados, contraataque destacado, ayuda reubicada abajo y textos en español correctamente codificados.
2. **Feedback de combate:** estados de espada, arco, escudo y contraataque con color, duración y confirmación sonora/visual.
3. **Tutorial contextual:** modal pausado, una explicación antes de la primera aparición/acción y botón explícito para continuar.
4. **Accesibilidad y legibilidad:** escala por resolución, contraste, tamaños mínimos, colores no dependientes únicamente del color y opción para ocultar ayuda.
5. **Validación:** probar 16:9, ventana pequeña y build Windows con mando Xbox; revisar que no se superpongan HUD, tutorial y pausa.

## Criterios de aceptación

- Ningún texto importante queda centrado sobre el área de juego.
- Vida, centro, power-ups y contraataque se identifican sin leer todo el HUD.
- La ayuda no tapa el objetivo ni los indicadores de vida.
- Los modales pausan el juego y solo continúan con confirmación explícita.
- Build sin errores de compilación y controles existentes intactos.

