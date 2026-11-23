# Psicología — Atención bajo ataque (supervivencia por movimiento corporal)

> **Estado:** concepto seleccionado y reglas principales acordadas; documentación de diseño, no código.  
> **Carrera:** Licenciatura en Psicología, FCACS.  
> **Tecnología central:** MediaPipe Pose o seguimiento corporal equivalente con cámara y pantalla.  
> **Referencia:** [06-psicologia.md](../carreras/06-psicologia.md), propuesta 2.

## Objetivo y fundamento educativo

Arcade de atención selectiva y control inhibitorio: el jugador usa **su cuerpo como control**, mediante detección de movimiento por cámara, para recoger estímulos objetivos y evitar distractores de acuerdo con una regla visible. Las reglas cambian conforme avanzan las oleadas y la dificultad crece progresivamente.

El juego **no diagnostica ni mide clínicamente la atención**, ni deduce emociones de rostros. La explicación educativa se limita a identificar estímulos relevantes, ignorar distractores e inhibir respuestas automáticas cuando cambia la regla.

## Mecánica central

1. La cámara reconoce los movimientos del cuerpo (primera propuesta: posición de manos, brazos y tronco) y los refleja en la pantalla mediante un personaje o cursores corporales.
2. El juego indica qué objetivos son válidos, por ejemplo «toca estrellas amarillas; evita círculos rojos».
3. El jugador debe tocar/recoger estímulos correctos y **evitar los distractores** moviendo los brazos o el cuerpo.
4. Cada objetivo correcto suma **puntos** y contribuye a una **racha/combo**.
5. **Tocar un distractor o sufrir un impacto de obstáculo quita vida, en lugar de restar puntos**; rompe la racha.
6. Los **combos permiten recuperar vida**; el ritmo/umbral exacto queda por equilibrar. La vida no supera su máximo.
7. Nuevas oleadas elevan progresivamente la velocidad y cantidad de estímulos, los distractores y la complejidad de reglas.
8. La partida termina cuando la vida llega a cero; se registra **récord por puntuación máxima**.

## Ejemplo de una ronda

- Aparecen estrellas amarillas y círculos rojos en la pantalla.
- Si el jugador toca una estrella amarilla con su mano virtual, recibe puntos y aumenta el combo.
- Si toca un círculo rojo, pierde una unidad de vida; los puntos ya ganados no se descuentan.
- Al acumular una racha suficiente de aciertos consecutivos, recupera vida.
- En la siguiente oleada, cambia la instrucción: ahora debe tocar **círculos azules** y evitar las figuras que antes eran correctas.
- Conforme continúa, aparecen reglas dobles o estímulos más rápidos.

## Vida, combos y récord

| Sistema | Regla seleccionada |
|---|---|
| Vida | Barra o corazones visibles durante toda la partida |
| Pérdida de vida | Contacto con distractor o impacto de obstáculo; sin descuento de puntos |
| Recuperación | Alcanzar combos de aciertos consecutivos; puede restaurar una fracción/unidad de vida hasta el máximo |
| Racha | Aciertos sucesivos; se reinicia si el jugador toca un distractor |
| Puntuación | Se acumula por objetivos correctos, con bonificaciones de combo por definir |
| Derrota | Vida del jugador llega a cero |
| Récord | Mayor puntuación conseguida en una partida |
| Rondas | Oleadas sucesivas con **dificultad creciente**, sin final predeterminado como propuesta inicial de supervivencia |

**Pendientes de balance, no decididos por el usuario:** cantidad de vida inicial, umbral de combo necesario para curar, cantidad de curación, duración de rondas, velocidad máxima, tratamiento de objetivos válidos no recogidos y condiciones exactas de cambio de regla.

## Dificultad progresiva

- Oleadas iniciales con reglas simples y figuras visualmente distintas.
- Nuevas oleadas añaden objetivos simultáneos, distractores similares y más velocidad.
- Reglas alternan entre colores, formas y combinaciones; el jugador debe **inhibir la regla anterior**.
- Obstáculos específicos pueden requerir esquivar lateralmente o agacharse, únicamente si el espacio y la accesibilidad lo permiten.
- La progresión debe permitir supervivencia y recuperación mediante combos sin generar situaciones imposibles de evitar.

## Interfaz y tecnología

- **MediaPipe Pose** (o alternativa según plataforma) para puntos corporales y seguimiento de brazos, manos y torso. No requiere reconocimiento facial, identificación del participante ni guardar video.
- Motor propuesto Unity + C#; arquitectura de captura y comunicación por validar con prototipo. No asumir que MediaPipe se integra automáticamente en una versión determinada de Unity.
- Pantalla con espejo del jugador o avatar, objetivos y obstáculos; HUD de vida, puntuación, combo, regla vigente, oleada y récord.
- Detección robusta de impactos con tolerancias y breve invulnerabilidad tras perder vida, para evitar que un mismo movimiento descuente muchas vidas.
- Prueba de calibración breve, indicador de si el cuerpo está dentro del encuadre y pausa segura si se pierde seguimiento.
- Modo accesible sentado y movimientos de brazos equivalentes, sin exigir agacharse o saltar para participar.
- Operación local sin almacenamiento de imágenes, audio ni datos identificables.

## Pruebas de aceptación antes de Codex

1. Una cámara detecta el movimiento necesario y el avatar/manos virtuales responden con latencia adecuada para jugar.
2. Tocar un objetivo válido suma puntos y racha.
3. Tocar un distractor **resta vida, sin restar puntuación**.
4. Una racha suficiente **restaura vida** sin sobrepasar el máximo.
5. Cambios de regla entre oleadas están claramente comunicados.
6. Más oleadas → mayor dificultad de manera progresiva.
7. Vida igual a cero → termina la partida y muestra puntaje/record.
8. No se almacenan grabaciones del usuario.
9. El juego se puede probar con distintos tamaños corporales y con opción accesible/sentada.
10. Si se pierde la detección, el juego no castiga movimientos involuntarios.

**Decisión registrada:** Psicología elige **Atención bajo ataque** con MediaPipe, vida por errores, curación por combos y dificultad progresiva, conservando los puntos como sistema de récord.

