# Contador Público — Flujo de caja superviviente (AR con marcadores físicos)

> **Estado:** ficha de diseño v0.1, mecánica principal seleccionada; requisitos técnicos y balance por validar antes de Codex.  
> **Origen:** conversación de diseño, 22 de noviembre de 2026.  
> **Referencia:** [04-contador-publico.md](../carreras/04-contador-publico.md). Para esta versión, prevalece esta ficha sobre las propuestas históricas de tuberías físicas/ESP32 del banco inicial.

## Objetivo y enfoque

Juego arcade de realidad aumentada (AR) sobre **una hoja/tablero grande impreso**. La cámara muestra un **mapa tridimensional virtual** anclado a la hoja. Un pequeño personaje recorre el mapa y debe abrir/cerrar válvulas de un sistema visual de tuberías que representan **entradas y salidas de efectivo** para cumplir obligaciones de una empresa. El verdadero desafío combina decisiones básicas de liquidez con **manejo de controles físicos inusuales** y evasión de enemigos. El **récord es el menor tiempo** en completar un escenario bajo condiciones equivalentes, no una puntuación por puntos.

El contenido contable debe ser simplificado y validado por un docente: cobros no siempre son inmediatos, pagos tienen fecha y prioridad; no representar automáticamente ingresos contables como dinero disponible.

## Tablero y targets (marcadores)

La idea incluye **cuatro targets previstos**, pero sólo **tres funciones están definidas**:

1. **Target central fijo del escenario:** en el centro de la hoja grande, ancla y orienta el mapa AR. No debe desplazarse durante la sesión.
2. **Target móvil de movimiento:** pieza pequeña que se coloca en una de las cuatro direcciones de una **cruz de casillas impresa** sobre esa misma hoja; arriba/abajo/izquierda/derecha → movimiento del personaje en la dirección correspondiente. Regresar el target al centro o retirarlo de la dirección activa debe detener el movimiento (propuesta a probar). **No se usa joystick virtual.**
3. **Target rotatorio de válvula:** pieza impresa que el jugador gira físicamente. La cámara observa su **orientación angular** para abrir/cerrar o ajustar una válvula virtual seleccionada/próxima en el mapa.
4. **Cuarto target:** **sin función acordada**, no inventar una función definitiva. Posibles usos a discutir: selección de válvula activa, iniciar/reiniciar o confirmar, según los resultados de las pruebas con usuarios.

**Restricción para prototipo:** especificar ID único por target y evitar que el marcador móvil o el de giro cubran el target central. La propuesta depende de que el sistema de seguimiento AR pueda observar simultáneamente mapa y piezas móviles en cámara. Esto requiere pruebas reales de iluminación, oclusiones, resolución y reconocimiento de rotación; no se da por verificado.

## Bucle jugable

1. Una cámara reconoce el target central; aparece el mapa sobre la hoja.
2. El juego plantea una meta de tesorería simplificada, por ejemplo mantener dinero suficiente para cubrir proveedores y nómina, y alcanzar una salida segura.
3. El jugador desplaza la pieza de movimiento por la cruz para avanzar o retroceder con su personaje hacia las válvulas del mapa.
4. Cuando el personaje está junto a una válvula, el jugador gira la pieza rotatoria para abrir/cerrar el flujo de dinero correspondiente; la vinculación a la válvula activa está pendiente de definirse.
5. Enemigos se mueven por el mapa y obstaculizan al personaje; **no puede combatir**, sólo huir, esquivar o aprovechar caminos seguros.
6. El jugador completa las obligaciones/metas del escenario; se detiene el cronómetro y, si el caso es válido, se registra el **tiempo total**.
7. Se comparan tiempos únicamente para el mismo escenario o condiciones equivalentes. Si se incorporan escenarios distintos, conservar récords separados.

## Enemigos y dificultad

- **Perseguidor:** se acerca cuando detecta al personaje.
- **Patrullero:** recorre rutas fijas, obligando a cronometrar el paso.
- **Bloqueador:** ocupa temporalmente un camino o acceso a una válvula.
- **Intermitente:** alterna intervalos de reposo y movimiento rápido.

Estos comportamientos son **propuestas**, no enemigos aprobados. No se habilitan ataques ni armas. Definir más tarde qué efecto tiene el contacto: penalización de tiempo, regreso a punto seguro o reinicio; evitar pérdidas confusas que impidan comparar récords.

**Dificultad:** mapas progresivos con más válvulas, rutas, enemigos y obligaciones; el control por marcadores es deliberadamente parte del reto. Las reglas deben seguir siendo comprensibles para visitantes sin conocimientos de contabilidad.

## Victoria, derrota y récord

- **Victoria:** cumplir los objetivos financieros simplificados del nivel y llegar al estado/salida de finalización.
- **Derrota:** no cumplir obligaciones críticas o agotar el efectivo mínimo necesario (propuesta por validar); también puede definirse una condición de colisión/tiempo máximo en pruebas.
- **Récord:** **menos tiempo para completar**, siempre que todas las metas contables del escenario se hayan cumplido. No guardar como récord un intento fallido.
- **Medición:** iniciar reloj después de que el tablero se calibra y el jugador inicia la ronda; no contar preparación de cámara como tiempo de juego.
- **Sin ventaja injusta:** para récords, mantener las mismas condiciones/semilla de enemigos y obligaciones por escenario.

## Tecnologías previstas (sin validar)

- Motor propuesto: Unity + C#.
- AR: biblioteca que soporte **detección simultánea de imágenes/marcadores, ID, posición 2D sobre tablero y orientación/rotación**; seleccionar entre Vuforia, AR Foundation u otra sólo tras prueba concreta de capacidades.
- Cámara: móvil/tableta o webcam fija, todavía por decidir. El usuario mira el mapa AR en esa pantalla mientras manipula las piezas.
- Anclaje: target fijo suficientemente grande, calibración y mecanismo de recuperación si se pierde seguimiento.
- Control de movimiento: lectura de la posición relativa de la pieza móvil en la cruz → vector de desplazamiento continuo con zona neutra.
- Control rotatorio: diferencia angular de la pieza → estado de válvula con suavizado/umbrales para impedir activaciones accidentales.
- Registro: mejores tiempos locales por nivel, sin cuentas ni datos personales.

## Factibilidad y pruebas antes de desarrollar el juego completo

1. Verificar que **los targets se reconozcan en una misma toma**, incluso mientras se mueve uno o gira otro.
2. Comprobar que el target central conserva el anclaje mientras se manipulan los otros y que los movimientos no producen saltos en el mapa.
3. Ensayar la cruz física para las cuatro direcciones, retorno a neutro y prevención de movimientos falsos.
4. Medir si la rotación de un marcador puede controlar una válvula de forma precisa sin perder seguimiento.
5. Determinar cómo se identifica la válvula objetivo y **la función del cuarto target**.
6. Jugar una escena gris con un mapa, una válvula, un enemigo, condiciones de victoria y cronómetro.
7. Añadir escenarios, metas contables validadas por docentes y récord por tiempo sólo tras probar el prototipo.
8. Preparar modo de respaldo en pantalla sin AR para exhibición si falla el reconocimiento.

## Alcance del primer encargo posterior a Codex

**No iniciar hasta autorizar.** En primer lugar, prototipo de seguimiento y controles con las cuatro posiciones de la cruz, neutral, rotación de marcador y cámara; si falla, registrar el resultado en lugar de fingir compatibilidad. Después, integrar personaje, mapa, válvula y cronómetro. Solicitar evidencia reproducible, capturas/video de prueba, pasos de configuración y PR.

## Decisiones abiertas

- Función del **cuarto target**.
- Tipo de cámara, dispositivo y distancia entre cámara y hoja.
- Si cada válvula se selecciona al acercarse, mediante cuarto target o por otro mecanismo.
- Regla de colisión con enemigos y condiciones exactas de derrota.
- Número de niveles/mapas y metas financieras específicas.
- Si todas las piezas caben en una sola hoja o requieren soportes auxiliares para buena detección.

