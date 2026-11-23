# Paquete Codex 25 — Psicología: Atención bajo ataque

**Equipo:** A. **Escena:** `Assets/Scenes/Psicologia.unity`. **Fuente vinculante:** [ficha de MediaPipe](../disenos/16-psicologia-atencion-bajo-ataque-mediapipe.md).

## Objetivo

Arcade de **atención selectiva e inhibición** mediante movimientos corporales: tocar objetivos correctos, evitar distractores, ganar puntuación/combo por aciertos, **perder vida por errores**, **curar vida mediante combos**, seguir oleadas infinitas cada vez más difíciles, perder cuando vida = 0. No es test clínico ni detector de emociones.

## Vertical slice primero

1. Pantalla con HUD grande: regla vigente, puntos, combo, barra de vida, oleada y mejor puntuación.
2. Un avatar simplificado o dos manos virtuales, inicialmente movibles por ratón o teclas; figuras de color y forma claramente distintas.
3. Ejemplo: sólo estrellas amarillas cuentan como objetivos; tocar otro objeto resta vida **sin restar puntos**. Cinco aciertos seguidos recuperan vida.
4. Vida 0 produce fin de partida, récord y reinicio. Debe jugarse antes de conectar webcam.

## Seguimiento corporal y expansión mínima

- Usar **MediaPipe Pose** o equivalente para obtener puntos de muñeca/mano y torso desde webcam, con calibración breve a marco de pantalla. Integrar directamente sólo si SDK/plataforma lo permite; alternativa un proceso local que comunique coordenadas por UDP/HTTP local, documentando dependencias.
- Procesar cámara sin grabar fotos/video ni guardar datos identificables. Mostrar señal de seguimiento perdido y **pausar colisiones** cuando se pierde pose para no castigar al jugador.
- Desduplicar choques por contacto: invulnerabilidad corta tras daño. Reglas cambian por oleada: «amarillo», «círculo azul», combinación de forma+color; siempre anunciar regla con texto/símbolos, no sólo color.
- Incrementar cantidad, velocidad y similitud de distractores en cada oleada. No generar una oleada imposible de completar; mantener camino para recuperar vida.
- Modo sentado o sólo brazos para accesibilidad; agacharse NO obligatorio.

## Defaults editables (no aprobados)

Vida máxima/inicial 5; daño 1; objetivo válido +10; combo de 5 aciertos cura +1 hasta máximo; oleada cada 25 s; invulnerabilidad 0.7 s tras daño. Combos se reinician al tocar distractor. Calibración mínima a 1 jugador.

## Aceptación

- [ ] Acierto suma puntos y racha; distractor sólo quita vida y rompe combo.
- [ ] Combo restaura salud sin superar máximo; sin vida se termina.
- [ ] Oleadas cambian reglas y crecen en dificultad.
- [ ] HUD, récord persistente, fin y reinicio.
- [ ] Webcam MediaPipe probada con persona/entorno real o marcada explícitamente como pendiente; sin grabación.
- [ ] Fallback mouse/teclado jugable, opción sentada, mute y SFX básicos.

## Contrato de entrega para Codex

- Lee [el sprint completo](../planeacion/21-sprint-48h-siete-videojuegos.md) y la ficha de diseño antes de escribir código; respeta lo ya acordado.
- Usa escenas y scripts dentro del proyecto Unity de **tu equipo**, sin editar carpetas del otro. Rama A: `feat/mvp-48h-equipo-a`; rama B: `feat/mvp-48h-equipo-b`.
- Primero entrega una vertical slice que inicie, se juegue, finalice y reinicie; luego afina reglas y progresión.
- Todas las cifras de balance son configurables y provisionales; figuras simples, texto legible, 3–5 sonidos originales básicos y opción de silenciar, **sin arte final**.
- Implementa entrada de teclado/ratón para pruebas cuando falle hardware. El fallback NO demuestra integración real de XR, MediaPipe, AR o esfera.
- Guarda récord local, documenta plataforma, versión de Unity, controles, procedimiento de arranque, pruebas ejecutadas y pendientes en README de la escena. No se suben cachés `Library`, `Temp`, builds ni datos personales.
- Commit, push y PR; no merge a `main` sin revisión. Nunca afirmar haber ejecutado lo que sólo se escribió.
