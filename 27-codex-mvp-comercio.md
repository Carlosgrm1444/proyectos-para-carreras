# Paquete Codex 27 — Comercio Exterior: Clasificador en caos VR

**Equipo:** B. **Escena:** `Assets/Scenes/Comercio.unity`. **Fuente vinculante:** [ficha VR](12-comercio-exterior-clasificador-en-caos-vr.md).

## Resultado exigido

En un almacén tridimensional en **primera persona VR**, agarrar e inspeccionar/rotar mercancías, escoger entre **exactamente tres opciones** visibles, **transportar físicamente la mercancía en el juego hacia un depósito mediante joystick** (no teletransporte) y **recibir resultado sólo al depositarla**. Cada nueva oleada acelera la llegada de mercancía; enemigos simples obstaculizan y pueden **dañar integridad del objeto**, reduciendo puntos aunque la categoría sea correcta. Derrota si se acumulan demasiados objetos por entregar.

## Vertical slice primero

1. Almacén de primitivas, cinta/recepción de productos y **3 depósitos** visibles con categorías básicas.
2. Crear objeto de prueba (p. ej. taza genérica representada por cilindro): clic/grip lo recoge, rota, muestra 3 respuestas; pulsar una selecciona pero **no muestra si acertó**.
3. Personaje camina por joystick si XR existe, o WASD/mouse para pruebas sin visor. Depositar en destino valida elección/destino, muestra correcta/incorrecta y puntos. Ese es el primer loop.
4. HUD con mercancía pendiente, puntos, integridad, entrega, récord y derrota por saturación.

## Resto de MVP

- Tres a cinco objetos geométricos de tipos distintos con nombres/descripciones educativas. **No inventar clasificación arancelaria real**; son categorías didácticas temporales. Mantener 3 opciones por objeto.
- Enemigos sencillos patrullan rutas y persiguen o interfieren: el jugador puede evitarlos o rechazarlos con un botón de empuje corto; el impacto reduce integridad (p. ej. 100→80), y la entrega correcta recompensa proporcionalmente.
- Oleadas aumentan tasa de aparición, amenaza y dificultad de decisión. **No imponer cronómetro individual por objeto**; derrota exclusivamente por cola/saturación máxima de mercancías sin procesar.
- Habilitar locomoción suave por joystick en XR; **sin teletransporte**, con opciones de confort (giro por intervalos configurable, velocidad baja, viñeta opcional) al probar dispositivo real.
- Para pruebas monitor, ratón/movimiento en FPS equivalente. No etiquetar este fallback como VR completa.
- Una misma mercancía sólo valida una vez al depositar; recoger/seleccionar no otorga puntos.

## Parámetros sugeridos editables

Recepción máxima 5 piezas, una nueva pieza cada 20 s con intervalo decreciente por oleada, integridad inicial 100, daño enemigo −25 a integridad, entrega correcta +100 × integridad/100, incorrecta 0 o penalización configurable. No son valores aprobados.

## Criterios de aceptación

- [ ] Agarrar, rotar, mostrar exactamente 3 opciones, escoger sin feedback.
- [ ] Desplazamiento joystick si VR físico disponible; entrega en receptor da respuesta y puntos.
- [ ] Distinto destino/categoría se penaliza; la mercancía conserva integridad.
- [ ] Enemigo daña carga; entrega correcta dañada puntúa menos.
- [ ] Llegadas aceleran y cola máxima provoca derrota, récord local.
- [ ] Sin teletransporte ni temporizador de cada pieza.
- [ ] XR probado con visor y mando real o claramente indicado como pendiente; fallback de monitor jugable.
- [ ] Reinicio, instrucciones, sonido básico, mute.

## Contrato de entrega para Codex

- Lee [el sprint completo](21-sprint-48h-siete-videojuegos.md) y la ficha de diseño antes de escribir código; respeta lo ya acordado.
- Usa escenas y scripts dentro del proyecto Unity de **tu equipo**, sin editar carpetas del otro. Rama A: `feat/mvp-48h-equipo-a`; rama B: `feat/mvp-48h-equipo-b`.
- Primero entrega una vertical slice que inicie, se juegue, finalice y reinicie; luego afina reglas y progresión.
- Todas las cifras de balance son configurables y provisionales; figuras simples, texto legible, 3–5 sonidos originales básicos y opción de silenciar, **sin arte final**.
- Implementa entrada de teclado/ratón para pruebas cuando falle hardware. El fallback NO demuestra integración real de XR, MediaPipe, AR o esfera.
- Guarda récord local, documenta plataforma, versión de Unity, controles, procedimiento de arranque, pruebas ejecutadas y pendientes en README de la escena. No se suben cachés `Library`, `Temp`, builds ni datos personales.
- Commit, push y PR; no merge a `main` sin revisión. Nunca afirmar haber ejecutado lo que sólo se escribió.
