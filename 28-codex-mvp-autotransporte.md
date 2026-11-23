# Paquete Codex 28 — Autotransporte de Carga: Carga equilibrada VR

**Equipo:** B. **Escena:** `Assets/Scenes/Autotransporte.unity`. **Fuente vinculante:** [ficha de grúa cenital](17-autotransporte-carga-equilibrada-grua-vr.md).

## Resultado exigido

Juego tipo Jenga **sólo de acomodar**, nunca retirar: en visor VR el jugador ve desde **arriba como una grúa**, toma cajas del suelo, las eleva y deposita dentro de un tráiler abierto. Físicas de masa, fricción y colisiones reales del motor; piezas pueden caer/deslizarse y derrumbar pila. **3 rondas de dificultad progresiva, 3 vidas**, derrumbe grave quita vida, al perder tercera vida **reiniciar desde la ronda 1**. Récord por mayor puntaje.

## Vertical slice primero

1. Tráiler abierto formado por piso y paredes, vista cenital estable, zona de cajas en el suelo fuera del remolque.
2. Una garra simplificada toma una caja, la eleva, traslada y suelta. Input por mando VR si se tiene visor; para prueba pantalla, puntero/ratón con teclas subir/bajar/girar.
3. Cada caja tiene `Rigidbody`, masa, collider y fricción configurables; puntos por colocación **estable durante varios segundos** y dentro del volumen del tráiler.
4. Derrumbe (caída fuera del tráiler o daño de varias piezas) quita vida una sola vez por evento; HUD 3 vidas, ronda, score, resultado y reinicio.

## Tres rondas concretas

- **Ronda 1:** cajas cúbicas de tamaño parecido, masa ligera y piso despejado. Objetivo completar 3–4 piezas estables.
- **Ronda 2:** variar tamaño y masa, incluir fragilidad; completar 4–5 piezas.
- **Ronda 3:** formas distintas/centro de gravedad diferente, pilas más altas y huecos reducidos; completar 5–6 piezas.
- Cada ronda sólo termina cuando se completan los objetivos de apilamiento. No hay conducción, carreteras, straps ni extracción de cajas.
- **Perder una vida sin agotar las tres:** propuesta técnica para este MVP, **reiniciar la ronda actual con distribución limpia** y conservar las vidas/puntos ya obtenidos (evitar contabilizar dos veces piezas); marcar provisional hasta validación del responsable.
- **Al agotar las tres:** perder partida, presentar resultado y ofrecer reinicio inmediato desde la **primera ronda** con tres vidas nuevas, score desde 0. Mantener mejor récord histórico.
- Después de ronda 3: victoria, puntaje total, récord e invitación a repetir.

## Integración VR y simplificaciones

- XR Interaction Toolkit/OpenXR **sólo si versión Unity, visor, sistema y mandos son compatibles**. Control de grúa desde posición fija elevada, evitar movimiento artificial de la cabeza/cámara.
- En pantalla: cámara elevada estable y manipulación mouse con rueda para altura (fallback); este modo no es prueba VR.
- Usar cubos/prismas simples, materiales con colores por peso, sin modelado final ni físicas extravagantes que desestabilicen simulación.
- Puntuación por pieza estable, bonificación dificultad y espacio, no por soltar piezas inestables.

## Defaults editables, no aprobados

Pieza estable si se mueve menos de 0.1 m/s durante 2 s; evento grave si una caja sale de volumen del remolque o caen 2 piezas apiladas; puntaje +10 base, +5 difícil. 3 rondas y 3 vidas **sí** confirmados.

## Criterios de aceptación

- [ ] Perspectiva cenital, carga tomada **desde el suelo** y colocada **dentro del tráiler**, no mano alzada desde dentro.
- [ ] Física de distintas masas; apilamiento y colapso visibles.
- [ ] Sólo se puntúan piezas estables; evento grave resta vida una vez.
- [ ] Tres rondas crecientes completables y 3 vidas por intento.
- [ ] Al agotar vidas se reinicia desde ronda 1, con puntos/estado reiniciados.
- [ ] Fin tras ronda 3 muestra puntuación/record.
- [ ] XR validado con visor o indicado como pendiente; fallback monitor jugable.
- [ ] SFX para agarrar, colocar, daño y resultado, mute.

## Contrato de entrega para Codex

- Lee [el sprint completo](21-sprint-48h-siete-videojuegos.md) y la ficha de diseño antes de escribir código; respeta lo ya acordado.
- Usa escenas y scripts dentro del proyecto Unity de **tu equipo**, sin editar carpetas del otro. Rama A: `feat/mvp-48h-equipo-a`; rama B: `feat/mvp-48h-equipo-b`.
- Primero entrega una vertical slice que inicie, se juegue, finalice y reinicie; luego afina reglas y progresión.
- Todas las cifras de balance son configurables y provisionales; figuras simples, texto legible, 3–5 sonidos originales básicos y opción de silenciar, **sin arte final**.
- Implementa entrada de teclado/ratón para pruebas cuando falle hardware. El fallback NO demuestra integración real de XR, MediaPipe, AR o esfera.
- Guarda récord local, documenta plataforma, versión de Unity, controles, procedimiento de arranque, pruebas ejecutadas y pendientes en README de la escena. No se suben cachés `Library`, `Temp`, builds ni datos personales.
- Commit, push y PR; no merge a `main` sin revisión. Nunca afirmar haber ejecutado lo que sólo se escribió.
