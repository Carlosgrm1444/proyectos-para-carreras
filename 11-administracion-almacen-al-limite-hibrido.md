# Administración — Almacén al límite (mecánica híbrida con videomapping)

> Estado: **propuesta de diseño v0.1, pendiente de confirmar detalles**. No es un proyecto ejecutable ni autorización para Codex.  
> Fuente del concepto: conversación de diseño del 22 de noviembre de 2026.  
> Documento de carrera relacionado: [01-administracion.md](01-administracion.md).

## Concepto definido por el responsable

Videojuego arcade de **gestión de inventarios y optimización de recorridos**: el visitante controla con un **mando convencional** a un personaje que trabaja en un almacén. La pared funciona como escenario híbrido de **proyección + cuatro anaqueles físicos representados por post-its o fichas reubicables**.

El participante **mueve físicamente las representaciones de los anaqueles** en la pared para reorganizar la distribución del almacén y acortar las rutas que recorrerá su personaje dentro del juego. Una cámara con visión por computadora reconoce sus nuevas posiciones para que el motor del videojuego actualice el mapa, la navegación y las colisiones.

### Qué debe preservarse

- **Cuatro racks o anaqueles**, representados físicamente sobre la pared mediante post-its/fichas movibles.
- **Un solo tipo de mercancía por anaquel**.
- **Control convencional** para desplazamiento y acciones del personaje digital.
- **Videomapping** integrado con la pared física: el mapa responde a la nueva colocación de los racks.
- **Punto de compra o abastecimiento** y **punto de entrega**, cuyo papel operativo debe aclararse.
- **Dos reglas de manejo de existencias, y solamente esas dos:** PEPS y UEPS.
- **Rondas** dentro de cada partida; cada ronda sube la dificultad.
- **Demanda variable entre partidas:** cada nueva partida reorganiza qué productos tienen demanda alta y cuáles demanda baja. No fijar siempre los mismos productos populares.
- **Puntuación acumulada** y récord por puntuación.
- **Derrota por atrasarse demasiado** con las solicitudes de mercancía.

## Mecánica principal

1. El juego establece la demanda de los tipos de producto al **iniciar la partida**. La distribución de demanda cambia en la siguiente partida.
2. Se muestran los cuatro anaqueles y las áreas operativas del almacén.
3. El jugador reacomoda los post-its/fichas físicas de los cuatro anaqueles para proponer una distribución más eficiente.
4. La cámara detecta **identidad, posición y orientación** de cada anaquel. El motor transforma esa información en posiciones/colisiones virtuales para calcular por dónde puede pasar el personaje.
5. Con el mando, el jugador recorre el almacén, recibe o repone mercancía según la regla asignada y atiende solicitudes.
6. Cada solicitud correctamente resuelta suma puntos; los recorridos largos, errores de PEPS/UEPS y demoras perjudican la puntuación.
7. Superada una ronda, aumenta la presión y surge la oportunidad de adaptar la ubicación de los anaqueles según el comportamiento de la demanda.
8. El juego termina cuando el atraso acumulado de solicitudes supera un límite definido durante el balance del juego. El resultado se compara con el récord local.

**Nota:** que el reacomodo se realice **entre rondas** es una recomendación para evitar que la cámara pierda el seguimiento y para mantener pausas claras, **no una decisión confirmada**. Debe validarse si también se permitirá reorganizar durante la ronda.

## Reglas de inventario

Cada mercancía / anaquel debe tener un método indicado de manera visible y comprobable:

| Regla | Comportamiento educativo propuesto | Error |
|---|---|---|
| **PEPS — Primeras Entradas, Primeras Salidas** | Reponer existencias por **detrás** del anaquel y retirar por **delante**; primero salen los lotes más antiguos | Retirar por el lado incorrecto o despachar un lote posterior antes que uno anterior reduce puntos |
| **UEPS — Últimas Entradas, Primeras Salidas** | Se debe retirar primero el lote que entró más recientemente. La forma exacta de entrada/salida por cara del anaquel **aún debe definirse** | Despachar un lote antiguo cuando exista uno más reciente reduce puntos |

Para que esta diferencia sea jugable, los lotes virtuales deben tener orden de entrada y el sistema debe saber qué lado del anaquel es frente y cuál es parte posterior. Los post-its pueden indicar identidad/posición/orientación del rack: **no se presupone que cada caja física esté representada por un post-it**.

**Aclaración pedagógica:** PEPS/UEPS también se utilizan como métodos de valoración de inventarios; este juego representa reglas simplificadas de flujo físico de lotes. **UEPS no debe presentarse como método de valuación permitido para cualquier contexto contable**. El contenido debe validarse con docentes de Administración.

## Sistema de partidas, rondas y demanda

- **Partida:** sesión completa del jugador, que incluye varias rondas y una única puntuación acumulada.
- **Ronda:** fase de dificultad dentro de la misma partida. Al avanzar, pueden crecer las solicitudes, su simultaneidad, la necesidad de reponer y los conflictos de ruta.
- **Variación entre partidas:** al iniciar cada partida, asignar nuevos pesos de demanda a las cuatro categorías. Se puede usar una semilla para reproducir pruebas, pero no mantener el mismo patrón de demanda por defecto.
- **Decisión estratégica real:** una distribución de anaqueles óptima para una partida puede ser ineficiente en la siguiente.
- **Condición de derrota:** cola de solicitudes vencidas / retraso acumulado sobre umbral configurable; la definición exacta y la señal visual necesitan prueba de jugabilidad.
- **Puntuación:** entregas completas y correctas, cumplimiento PEPS/UEPS, tiempo, eficiencia de ruta y combos; errores o demoras restan.
- **Récord:** máximo puntaje alcanzado por partida (almacenamiento local).

No incorporar por defecto FEFO, caducidades o valoración monetaria: el alcance acordado se limita a PEPS y UEPS.

## Necesidades técnicas

### Interacción física

- Cámara fija que vea la totalidad del espacio donde se colocan los cuatro racks.
- Marcador visual o patrón inequívoco por anaquel (por ejemplo, ArUco con ID específico) para detectar **qué rack es**, **dónde se colocó** y **hacia dónde apunta su frente**.
- Calibración de la cámara respecto de la proyección y del plano de la pared.
- Comprobación de cuatro marcadores válidos, sin solapamientos ni zonas de paso imposibles.
- Si se pierde la detección, no mover de manera errática los obstáculos del juego: congelar la última distribución válida y mostrar aviso para recuperar la lectura.
- Proyectar y recalcular escenario, rutas y colisiones tras aceptar una distribución estable; no inferir estabilidad de una lectura de una sola imagen.
- Usar fichas removibles resistentes para el piloto si los post-its se despegan o cambian de orientación.

### Motor y control

- Unity/C#, alineado con [la arquitectura común](00-arquitectura-y-criterios-de-museo.md), como propuesta tecnológica; no se ha realizado una prueba real.
- Control convencional del personaje.
- Sistema de pedidos, posiciones de racks, inventario por lotes, verificador PEPS/UEPS, puntuación, rondas y derrota.
- Detección de puntos de recogida/depósito según orientación válida del rack.
- Escenario simulable en PC **sin cámara ni proyector**, mediante editor/manual de distribución, para asegurar una primera versión jugable y un respaldo en museo.

### Importante: elementos proyectados vs. físicos

La descripción del responsable distingue `todo lo demás` de `racks, punto de compra, punto de entrega, personaje y mercancía`, pero no queda inequívoco si esta enumeración señala lo que **sí** o lo que **no** aparecerá proyectado. Por eso:
- **Confirmado:** los racks se representan físicamente con post-its/fichas; el personaje se controla con mando.
- **Por confirmar:** dónde se visualizarán el personaje, la mercancía, el punto de compra y el punto de entrega (proyección, soporte físico o pantalla auxiliar).
- **No diseñar ni implementar el pipeline de renderizado** hasta resolver esta definición.

## Aspectos abiertos para acordar antes de pasar a Codex

1. Confirmar qué se proyecta y qué es físico, en particular personaje, mercancía, compra y entrega.
2. Definir orientación y accesos del anaquel para UEPS; para PEPS está indicado **entrada posterior y salida anterior**.
3. Decidir si las reubicaciones físicas se permiten sólo **entre rondas** o también **en tiempo real**.
4. Definir cantidad de unidades/lotes por anaquel, mecánica de compra/reposición y comandos del mando.
5. Decidir si la regla PEPS/UEPS cambia por producto, ronda o partida.
6. Determinar rangos de puntaje, umbrales de atraso y dificultad, ajustados mediante pruebas.
7. Validar la lectura de cuatro fichas con cámara/proyector en la pared real, con distintas luces y posibles oclusiones.

## Primera evidencia que deberá producir Codex (cuando se autorice)

**Primero un prototipo de PC sin hardware:** 4 anaqueles reubicables en un editor, un personaje movible por mando, pedidos con demanda variable por partida, salida de lotes PEPS/UEPS, rondas, puntaje/derrota y récord local. Después, integrar cámara y videomapping si una prueba de calibración y detección demuestra que el montaje lo permite.

Criterios de aceptación a fijar en la especificación final: detección correcta de 4 racks; orientación inequívoca; colisiones actualizadas al mover racks; funcionamiento correcto de PEPS/UEPS con lotes; nuevos patrones de demanda entre partidas; presión creciente entre rondas; derrota y récord reproducibles; respaldo sin cámara.
