# Comercio Exterior — Clasificador en caos VR (diseño de experiencia v0.1)

> **Estado:** mecánica VR principal elegida conceptualmente; pendiente de balance, validación docente, dispositivo y pruebas de confort. Documento de planificación, no código.  
> **Fuente:** ajustes acordados en la conversación de diseño del 22 de noviembre de 2026.  
> **Relación:** complementa [03-comercio-exterior.md](03-comercio-exterior.md). Para la versión VR, **esta ficha prevalece** sobre las propuestas históricas de pantalla, RFID, mesa y videomapping incluidas allí.

## 1. Premisa y objetivo académico

El visitante interpreta a un operador/inspector de mercancía en un **almacén aduanero virtual 3D en primera persona**. Examina cada producto con las manos virtuales, elige su categoría entre **tres opciones visibles**, recorre un tramo del almacén con el **joystick** y entrega el objeto en el destino que considera correcto. **El resultado de la clasificación se oculta hasta que la mercancía se entrega**.

La selección se inspira en material, función y presentación de las mercancías, de forma educativa y simplificada. Las categorías y fichas deben validarse con docentes; no presentar las decisiones como clasificación arancelaria legal ni simular fracciones arancelarias reales sin respaldo.

## 2. Decisiones explícitas del diseño

| Elemento | Decisión |
|---|---|
| Formato | **VR inmersiva en primera persona** |
| Desplazamiento | **Movimiento continuo con joystick**, no teletransporte como navegación principal |
| Interacción con objetos | Agarrar, sostener, acercar, girar y soltar objetos con mandos VR |
| Identificación | **Tres opciones de categoría** que aparecen en UI próxima al objeto o al agarrarlo; forma exacta por probar |
| Selección | Se elige una opción mediante botones/puntero del mando, **sin mostrar si es correcta** |
| Entrega | Tras seleccionar, recorrer físicamente **dentro del mundo virtual** un tramo del almacén hasta una banda, puerta, compuerta o receptáculo correspondiente a la categoría |
| Resultado | **Evaluar y comunicar acierto/error únicamente al depositar/entregar**, nunca al seleccionar |
| Dificultad | Más mercancías, decisiones difíciles, mayor presión de tiempo y obstáculos progresivos por oleadas/rondas |
| Objetivo | Clasificar correctamente el mayor número de mercancías, conseguir puntuación y superar récord |
| Fuera de alcance principal | Reconocimiento de voz, identificación física RFID/NFC, mesa con props, videomapping y teletransporte |

Nota: «recorrer parte del almacén» significa desplazamiento con joystick dentro del entorno virtual; no requerir caminar físicamente por una zona extensa del museo.

## 3. Bucle de jugabilidad

```text
Llega una mercancía a la banda de recepción
       ↓
Jugador se acerca con joystick y la agarra con mano virtual
       ↓
Puede examinarla: girar, acercar, observar pistas visuales
       ↓
Aparecen 3 categorías plausibles en una interfaz asociada al objeto
       ↓
Elige una (sin feedback inmediato de acierto/error)
       ↓
Transporta la mercancía por un recorrido del almacén en VR
       ↓
Elige y alcanza la zona de entrega: banda, puerta, tolva/compuerta
       ↓
Deposita el objeto → recién entonces se verifica categoría y destino
       ↓
Puntos / penalización / explicación breve / combo / siguiente mercancía
```

**Validación de dos decisiones:** la categoría seleccionada y el destino físico en el mundo virtual. Si el jugador escoge una categoría y entrega en una zona no correspondiente, debe registrarse un error de destino; no revelar la categoría correcta antes de la entrega. Para el MVP, diseñar una salida visible por cada categoría de las tres opciones, sin indicaciones implícitas de cuál es correcta.

## 4. Ejemplo pedagógico (ficticio, pendiente de revisión docente)

- Mercancía: **una taza de cerámica**.
- Al agarrarla aparecen opciones: **A. Cerámica/vidrio; B. Plásticos/juguetes; C. Textiles**.
- El jugador rota la taza, selecciona una opción y **no recibe confirmación**.
- Recorre el almacén con joystick hasta la puerta/banda/tolva de la categoría elegida.
- Deposita la taza. **Sólo en ese instante** aparece «Correcto» o «Incorrecto», una explicación corta y el cambio de puntuación.

No usar el ejemplo como clasificación aduanera oficial; es una categoría educativa de muestra.

## 5. Vista VR e interfaz

- La cámara es primera persona, con manos virtuales y mandos controlando agarre e interacción.
- La UI de 3 opciones debe colocarse sobre/adyacente al objeto, preferiblemente en un panel diegético estable y legible cuando esté agarrado, evitando tapar sus características.
- Los distractores deben ser plausibles y no revelar la respuesta por color o sonido.
- Mostrar estado **«seleccionado / pendiente de entrega»** sin marcar «correcto».
- La UI de tiempo, cola, puntaje, combo y ronda debe ser visible sin saturar el campo de visión.
- Los destinos deben distinguirse por nombres, iconos y siluetas; no sólo por color.
- Se puede rectificar la opción antes de entregar (propuesta para evitar errores de pulsación; por validar).
- Indicar al jugador una orientación general del almacén, **sin mostrar la ruta correcta** para el producto antes de la decisión.

## 6. Rondas, presión, puntuación y derrota (valores pendientes de balance)

| Parte | Mecánica |
|---|---|
| Ronda inicial | Objetos reconocibles, tres categorías claramente distintas, ruta breve y pocos riesgos |
| Rondas siguientes | Productos parecidos, objetos de formas similares, rutas más largas, obstáculos, pedidos simultáneos y menos margen de tiempo |
| Puntuación | Entregas correctas, rapidez, combos y, posteriormente, manejo cuidadoso |
| Penalizaciones | Categoría equivocada, entrega en zona incorrecta, daño simulado a mercancía, pedido vencido |
| Derrota | Se satura la recepción o se acumulan demasiadas mercancías sin clasificar; umbral a definir |
| Récord | Mejor puntuación acumulada local por partida |

La estructura del [ArcadeCore común](00-arquitectura-y-criterios-de-museo.md) sigue siendo referencia. No añadir herramientas físicas de montacargas/diablito en el MVP VR sin demostrar que aportan a esta interacción.

## 7. Tecnologías para la futura implementación

- **Unity + C#** como motor propuesto, alineado con la arquitectura común.
- **OpenXR / XR Interaction Toolkit** o componentes equivalentes compatibles con el visor definido: agarre, selección UI, interacción de entrega y movimiento continuo.
- **Mandos VR**: joystick para locomoción y control de agarre/selección.
- **Escenario y objetos 3D** con colisiones, zonas receptoras, sistema de inventario de solicitudes y evaluador de decisiones.
- **Salida espejo para espectadores**, de ser viable, sin recopilar datos personales.
- Funcionamiento local y récord en almacenamiento local.

**Visor, versión de Unity, rendimiento mínimo, SDK y plataforma de compilación pendientes de confirmar.** No asumir una versión o modelo específico.

## 8. Comodidad y seguridad en exhibición

El movimiento continuo con joystick puede causar mareos por movimiento visual. **Se respeta la decisión de no usar teletransporte como control principal**, pero deben probarse:
- Velocidad moderada configurable, aceleración suave, giro por saltos opcional y viñeta de confort opcional.
- Opción de juego sentado y zona física supervisada/despejada.
- Evitar saltos forzados, cambios bruscos de cámara, altura artificial y movimientos involuntarios.
- Sesiones cortas, pausa inmediata y salida accesible.
- Pantalla espejo para que un mediador pueda asistir y evitar golpes contra obstáculos físicos.
- Higiene del visor y modo demostración en pantalla para quienes no quieran usar VR.

## 9. Pruebas y criterios iniciales de aceptación para Codex

**No implementar hasta que se autorice un paquete de trabajo.**

1. El usuario se desplaza por un almacén tridimensional **con joystick, sin teletransporte**.
2. Puede agarrar, girar y soltar al menos un objeto con mando VR.
3. Al agarrarlo aparecen **exactamente tres** opciones seleccionables.
4. Elegir una opción **no revela la respuesta correcta ni otorga puntos**.
5. El personaje debe recorrer un sector del almacén llevando la mercancía.
6. Hay tres zonas de entrega identificables (banda/puerta/tolva son opciones de arte por decidir).
7. Sólo el evento de **entrega válida** evalúa categoría y destino y aplica puntuación/penalización.
8. El sistema controla nuevas solicitudes, presión creciente, derrota y récord.
9. El jugador puede completar la interacción principal sin mareo incapacitante en una prueba supervisada; registrar tolerancia y cambios necesarios sin prometer resultado.
10. Se documenta el procedimiento de build, pruebas, visor usado y limitaciones.

## 10. Por resolver antes de cerrar la especificación

- Modelo de visor VR y espacio físico disponible.
- ¿Tres destinos fijos para las tres opciones de cada objeto, o varias salidas por grupos de mercancías?
- ¿Selección previa obligatoria para poder transportar o se puede elegir sobre la marcha?
- ¿Cómo se transportan cargas muy pesadas? ¿Excluidas del MVP?
- Duración objetivo exacta de partida y balance de oleadas.
- Lista de mercancías, tres opciones por cada una y justificación académica validada.

**Decisión de diseño registrada:** la retroalimentación se difiere hasta la entrega para mantener incertidumbre, recorrido y consecuencias de la elección.
