# Fase 0 — Dirección y preparación del MVP LIIA

> **Estado:** en ejecución  
> **Siguiente fase al cerrar ésta:** Fase 1 — núcleo de movimiento estable  
> **Documento rector:** [plan integral](29-plan-integral-liia-laberinto-esferico.md)

## 1. Decisión de producto del MVP

El primer entregable demostrable será **un laberinto esférico jugable en monitor**, controlado por teclado, con una bola que rueda visualmente, tiene inercia controlada y nunca se desprende de la superficie.

La esfera física y el ESP32 no bloquean este entregable. Se integrarán cuando el juego base sea claro, agradable y recuperable sin ayuda técnica.

### Qué sí incluye el MVP de juego

- Un mapa PNG convertido en laberinto curvo con muros extruidos.
- Inicio, meta, movimiento con inercia, choque, reinicio y récord local.
- Cámara sobre la zona local del planeta, sin atravesarlo ni rotar de manera desorientadora.
- Teclado con flechas/WASD como control de desarrollo y respaldo.
- Un nivel corto que se pueda iniciar, completar y reiniciar.

### Qué queda fuera del MVP actual

- VR, AR, multijugador, proyección o videomapping.
- Afirmar que la esfera física puede mostrar imágenes.
- ESP32 como requisito para jugar en computadora.
- Varios niveles, obstáculos móviles, sonidos finales o arte final.
- Física libre que permita que la bola caiga del planeta.

## 2. Estado de partida verificado

| Elemento | Estado actual | Evidencia / ubicación |
|---|---|---|
| Motor | Listo | Unity 2022.3.62f3 LTS instalado y proyecto abre. |
| Escena | Lista para iterar | `proyectos/liia-laberinto-gravitatorio-esferico/Assets/Scenes/LIIA.unity` |
| Mapa | Funcional | `Assets/Resources/Maps/laberinto_01.png` |
| Generación | Funcional | `SphericalMazeLayout` y `SphericalMazeMeshBuilder` |
| Movimiento | Implementado, en ajuste | `MazeBallController`; requiere prueba de sensación con usuarios. |
| Cámara | Implementada, en ajuste | `BallFollowCamera`; requiere prueba de legibilidad. |
| Meta / récord | Básicos | `GameFlow` y HUD. |
| Estrellas / vidas / checkpoints | Pendiente de restaurar al nuevo pipeline PNG | No se considera listo. |
| ESP32 | Pendiente | Sin firmware ni prueba de conexión registrados. |
| Esfera física / proyección | Pendiente de validación física | No debe prometerse. |

## 3. Contrato de la experiencia inicial

### Recorrido del visitante

1. La pantalla de atracción muestra el título, controles y récord.
2. El visitante pulsa una flecha o botón físico y empieza un intento.
3. La bola acelera por rutas visibles, se desliza un poco al soltar y gira visualmente.
4. Al tocar un muro, se detiene sólo el componente de movimiento bloqueado.
5. Al llegar a la meta, se muestra tiempo, puntuación y récord.
6. Tras unos segundos o al pulsar reiniciar, vuelve a estar disponible para otra persona.

### Parámetros iniciales de balance

Estos valores son puntos de partida, no valores finales:

| Parámetro | Valor inicial | Criterio de ajuste |
|---|---:|---|
| Aceleración | 32 celdas/s² | Debe responder con rapidez sin sentirse instantánea. |
| Velocidad máxima | 4.6 celdas/s | Un nivel estándar debe durar 90–180 s en primera visita. |
| Resistencia al rodar | 8.5 celdas/s² | Debe deslizar brevemente, sin percibirse pesada. |
| Radio de bola | 0.22 unidades | No debe ocultar caminos ni atravesar muros. |
| Cámara sobre superficie | 8.5 unidades | Debe mostrar el siguiente giro sin alejar demasiado el mapa. |
| Vidas de la versión final | 3 | Se activa en Fase 3, al existir trampas/caídas diseñadas. |

## 4. Criterios de aceptación para cerrar Fase 0

- [x] Alcance del MVP definido y documentado.
- [x] Fuente única del mapa definida: PNG.
- [x] Regla de estabilidad definida: bola restringida a superficie, sin física de caída libre.
- [x] Ruta de visualización segura definida: monitor primero.
- [x] Ruta de control segura definida: teclado primero, ESP32 después.
- [ ] Duración objetivo de un nivel confirmada por la persona responsable de la exhibición.
- [ ] Fecha del evento / hito de demostración registrada.
- [ ] Inventario real de ESP32, botones, fuente, cables, monitor y esfera registrado.
- [ ] Responsables de software, diseño de nivel, electrónica y montaje confirmados.

Los últimos cuatro puntos no impiden continuar con Fase 1; sí son necesarios antes de comprometer fechas de hardware y montaje.

## 5. Registro de decisiones

| ID | Decisión | Estado | Razón |
|---|---|---|---|
| D-01 | El mapa parte de PNG, no de cubos colocados manualmente. | Aprobada | Diseño repetible, validable y editable. |
| D-02 | La bola usa cinemática con sensación física, no `Rigidbody` libre. | Aprobada | Impide desprendimientos y fallos de solver. |
| D-03 | La cámara sigue la normal local de la superficie. | Aprobada | Cada zona se percibe como piso. |
| D-04 | El planeta queda visualmente estático durante el MVP. | Aprobada | Evita desorientación y movimientos dobles. |
| D-05 | El teclado es el control de respaldo permanente. | Aprobada | Permite diagnosticar el juego aun si falla la electrónica. |
| D-06 | El monitor es la salida garantizada. | Aprobada | La capacidad visual de la esfera física no está comprobada. |
| D-07 | ESP32 por USB/HID o serial se elegirá después de inventario. | Pendiente | Depende del modelo y sistema operativo disponibles. |
| D-08 | La entrega actual termina al cerrar las mecánicas del juego; ESP32 y pantallas se abordan después. | Aprobada | Mantener el alcance jugable verificable antes de integrar hardware. |

## 6. Riesgos inmediatos y respuesta

| Riesgo | Señal | Acción en la siguiente fase |
|---|---|---|
| La bola se siente pesada o artificial | El jugador corrige en exceso o deja de intentar | Pruebas cortas de parámetros y telemetría de velocidad. |
| La cámara oculta el siguiente camino | Se choca sin poder anticiparlo | Ajustar distancia/FOV y añadir referencias de dirección. |
| El mapa curvo se ve como una mancha oscura | Muros/piso no se distinguen | Mejorar materiales, luz y contraste antes de añadir contenido. |
| Mapa PNG inválido | Inicio/meta inaccesibles | Implementar validador de conectividad en Fase 2. |
| Hardware no disponible a tiempo | No hay ESP32 o botones al entrar a Fase 5 | Mantener teclado, documentar faltante y no bloquear pruebas de juego. |

## 7. Trabajo que se inicia en Fase 1

1. Añadir modo de diagnóstico de desarrollo: velocidad, celda actual, FPS y estado de cámara.
2. Preparar una escena de prueba con recta, curva, choque y diagonal para ajustar valores sin depender del laberinto completo.
3. Fijar protocolo de prueba de 5 minutos por configuración.
4. Registrar los valores que den mejor respuesta con al menos cinco personas de prueba.
5. Cerrar Fase 1 sólo cuando la bola, la cámara y el choque pasen los criterios del plan integral.

