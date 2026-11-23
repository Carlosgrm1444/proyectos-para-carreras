# Comercio Exterior — Clasificador en caos VR (diseño de experiencia v0.2)

> **Estado:** mecánica VR principal elegida conceptualmente; pendiente de balance, validación docente, dispositivo y pruebas de confort. Documento de planificación, no código.  
> **Fuente:** ajustes acordados en la conversación de diseño del 22 de noviembre de 2026.  
> **Relación:** complementa [03-comercio-exterior.md](../carreras/03-comercio-exterior.md). Para la versión VR, **esta ficha prevalece** sobre las propuestas históricas de pantalla, RFID, mesa y videomapping incluidas allí.

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
| Dificultad | **Las mercancías llegan con frecuencia progresivamente mayor**, aumentando la cola; los enemigos aparecen en el trayecto y pueden dañar la carga |
| Objetivo | Clasificar correctamente el mayor número de mercancías, combatir/evitar enemigos simples, proteger su integridad, conseguir puntuación y superar récord |
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

## 6. Oleadas, enemigos, integridad de mercancía, puntuación y derrota

**Nuevas decisiones confirmadas:**

- **Incremento progresivo de llegadas:** los productos ingresan de forma continua y cada ronda acelera la frecuencia de llegada. No se pierde por el reloj de un producto individual.
- **Derrota por saturación:** si la zona de recepción acumula demasiadas mercancías sin procesar, termina la partida. El umbral numérico se balanceará después.
- **Enemigos durante el recorrido:** obstáculos móviles o criaturas ficticias aparecen entre la recepción y los puntos de entrega e intentan golpear o dañar la mercancía transportada.
- **Defensa sencilla del jugador:** debe existir la posibilidad de evitar o repeler enemigos, sin un sistema complejo de combate. Propuesta para evaluar: un botón de empuje/repulsión de corto alcance, con breve enfriamiento; también se puede esquivar.
- **Valor ligado al estado de la carga:** cada objeto tiene una **integridad** (por ejemplo, de 100 % a 0 %). Los golpes disminuyen su valor y, cuando la clasificación y entrega son correctas, reducen los puntos recibidos. El sistema informa el estado al transportar, pero nunca si la clasificación es acertada hasta depositar.

| Parte | Regla prevista |
|---|---|
| Ronda inicial | Pocas mercancías, llegada lenta, categorías evidentes, uno o ningún enemigo |
| Rondas siguientes | Menor intervalo entre llegadas, más enemigos, recorridos con obstáculos y opciones más parecidas |
| Enemigos sugeridos | Carritos autónomos averiados, robots de almacén traviesos y drones de inspección ficticios; todos no violentos y de comportamiento sencillo |
| Defensa | Esquivar o activar un empuje de alcance corto usando botón VR; sin combos de combate, armas complejas ni necesidad de matar enemigos |
| Daño | Colisiones de enemigo con mercancía reducen integridad; impacto visual/sonoro moderado; sin sacudidas forzadas de cámara |
| Puntuación | Entrega correcta × factor de integridad, más bonificación de rapidez/racha según balance. Ejemplo **ilustrativo, no definitivo**: base 100 puntos × integridad 70 % = 70 puntos |
| Penalizaciones | Clasificación o destino incorrecto, mercancía dañada, pedidos demorados; evitar castigar dos veces el mismo golpe |
| Derrota | Cola de mercancías sin clasificar llega al máximo permitido |
| Récord | Mayor puntuación acumulada local de una partida |

El núcleo sigue siendo **inspeccionar → elegir una de tres categorías sin conocer el acierto → transportar protegiendo el objeto → entregar → recibir resultado**. El combate es secundario a la clasificación; no se convierte en un juego de lucha.

**Punto pendiente de balance:** cómo progresa el volumen de llegadas, tamaño máximo de cola, número de enemigos, potencia y enfriamiento del empuje, daño por golpe, límites de integridad y puntuación para preservar partidas breves y accesibles.

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
8. Las mercancías llegan continuamente con frecuencia creciente entre rondas; la partida termina cuando la recepción se satura, con récord por puntos.
9. Durante el traslado aparecen enemigos sencillos que pueden reducir la integridad de la mercancía; se pueden esquivar o repeler con un control simple, y el daño reduce puntos aun cuando el objeto se clasifique correctamente.
10. El jugador puede completar la interacción principal sin mareo incapacitante en una prueba supervisada; registrar tolerancia y cambios necesarios sin prometer resultado.
11. Se documenta el procedimiento de build, pruebas, visor usado y limitaciones.

## 10. Por resolver antes de cerrar la especificación

- Modelo de visor VR y espacio físico disponible.
- ¿Tres destinos fijos para las tres opciones de cada objeto, o varias salidas por grupos de mercancías?
- ¿Selección previa obligatoria para poder transportar o se puede elegir sobre la marcha?
- ¿Cómo se transportan cargas muy pesadas? ¿Excluidas del MVP?
- Duración objetivo exacta de partida, tasa progresiva de llegada, límite de acumulación y balance de enemigos/daño.
- Lista de mercancías, tres opciones por cada una y justificación académica validada.

**Decisión de diseño registrada:** la retroalimentación se difiere hasta la entrega para mantener incertidumbre, recorrido y consecuencias de la elección.
