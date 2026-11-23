# Fase 0 — Checklist de estabilización

## Estado

- [x] Arena, cámara y escena de entrada creadas.
- [x] Guardian, Centro de Datos, solicitudes y amenazas tienen colliders.
- [x] Obstáculos sólidos con navegación lateral inicial.
- [x] HUD sin dependencia de `UnityEngine.UI`.
- [x] Diagnóstico de mando activable con `Select`.
- [x] Confirmar el mapeo real del control Xbox conectado: LT = -1, RT = +1, ambos = 0.
- [ ] Confirmar compilación sin errores en Unity 6.6.
- [ ] Confirmar cinco minutos de juego sin excepciones.
- [ ] Confirmar reinicio después de game over.

## Prueba de mando

1. Ejecutar `Assets/Scenes/Main.unity`.
2. Pulsar `Select`.
3. Mover stick izquierdo: deben cambiar `Horizontal` y `Vertical`.
4. Mover stick derecho: deben cambiar `AimHorizontal` y `AimVertical`.
5. Mantener LT y anotar el eje que cambia y su signo.
6. Mantener RT y anotar el eje que cambia y su signo.
7. Pulsar RB: debe activar la espada.
8. Si LT/RT no aparecen, documentar el modelo del mando y el backend detectado antes de ajustar más código.

## Mapeo confirmado

- `LeftTrigger` usa el eje combinado 3: escudo cuando el valor es menor que `-0.35`.
- `RightTrigger` usa el mismo eje: arco cuando el valor es mayor que `0.35`.
- Al presionar ambos gatillos el eje vuelve a `0`; no debe activar ninguna de las dos acciones.

## Criterios de salida

- El proyecto entra en Play Mode sin errores de compilación.
- El mando mueve, apunta, ataca y bloquea de forma repetible.
- La experiencia requiere mando Xbox conectado.
- Las amenazas y solicitudes rodean obstáculos y no los atraviesan.
- Las colisiones no dejan entidades permanentemente atrapadas.
- Se puede perder por vida del guardián o del Centro y reiniciar sin recargar manualmente la escena.

