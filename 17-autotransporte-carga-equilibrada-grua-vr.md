# Autotransporte de Carga — Carga equilibrada VR (grúa cenital y apilamiento)

> **Estado:** concepto seleccionado, con tres rondas y tres vidas acordadas; ficha de diseño para posterior Codex, **sin código**.
> **Carrera:** Licenciado en Autotransporte de Carga.
> **Documento de origen:** [02-autotransporte-de-carga.md](02-autotransporte-de-carga.md).

## Visión del juego

**Juego VR de apilamiento de mercancía con física**, semejante en desafío espacial a Jenga, pero **sin retirar piezas**. El jugador observa **desde arriba, como operador de una grúa**, una zona de mercancía en el suelo y la caja abierta de un tráiler. Su única acción principal es **tomar piezas del suelo, elevarlas y acomodarlas dentro de la caja del tráiler**, buscando una colocación estable y eficiente. No tiene que viajar, conducir, inspeccionar, asegurar correas, pelear ni realizar otra fase de juego.

La experiencia representa de forma sencilla la distribución de peso, aprovechamiento de espacio, estabilidad y daño de la carga. El objetivo pedagógico es que colocar cajas mal puede afectar la estabilidad y la mercancía. No pretende sustituir capacitación profesional de estiba/sujeción.

## Cámara, control y entorno

- **Cámara VR:** perspectiva cenital oblicua o desde una cabina virtual elevada, con vista clara y estable del interior abierto del tráiler y las piezas disponibles en el piso. **El jugador no se encuentra caminando dentro de la caja**.
- **Manipulación:** usar mandos de VR para apuntar/controlar una grúa virtual o mecanismo de agarre, tomar una pieza del suelo, elevarla, trasladarla y depositarla sobre el piso del remolque o encima de otras piezas.
- **Piezas:** cajas y bultos 3D con masas, dimensiones, formas y niveles de fragilidad distintos.
- **Físicas:** colisiones, gravedad, fricción, inclinación y apilamiento; las cajas pueden deslizarse, inclinarse, caer o causar un derrumbe.
- **Posicionamiento:** el jugador debe estimar visualmente dónde colocar cada pieza, distribuir peso y aprovechar volumen sin bloquear por completo el área útil.
- **Sin fases añadidas:** no conducir el camión, no simular carretera, no retirar cajas, no usar correas como otro minijuego, no convertirlo en inventario/almacén de pedidos.

## Bucle jugable

```text
Se muestra la caja del tráiler y mercancías disponibles en el suelo
         ↓
El jugador toma una caja con la grúa virtual
         ↓
Eleva, traslada y coloca la caja dentro del tráiler
         ↓
Al soltar, se simulan estabilidad y colisiones
         ↓
Si permanece bien acomodada, se acredita la colocación/puntos
         ↓
Llega la siguiente pieza, de dificultad progresivamente mayor a lo largo de tres rondas
         ↓
Se acumula puntuación y se intenta superar el récord
```

## Dificultad progresiva

1. **Introducción:** cajas regulares y ligeras, espacio amplio, pilas bajas.
2. **Variedad:** distintas formas, masas y tamaños que exigen planear.
3. **Precisión:** menos huecos disponibles, pilas más altas y mercancía frágil.
4. **Equilibrio:** piezas asimétricas, centros de masa desplazados y apoyos reducidos.
5. **Supervivencia de apilamiento:** secuencia cada vez más difícil, con decisiones de riesgo/recompensa y presión opcional de tiempo **por validar**.

Debe existir una forma de detectar cuándo una pieza está colocada de manera estable (p. ej., tras unos segundos de reposo y dentro del tráiler), evitando otorgar puntos por una caja que cae inmediatamente.

## Rondas, vidas, puntuación y récord

- **Tres rondas** en cada partida, con complejidad ascendente. La ronda 1 introduce cajas regulares y estables; la 2 introduce diferentes pesos, tamaños y fragilidad; la 3 exige apilar piezas más difíciles en espacio más limitado. La distribución exacta se ajustará en pruebas.
- **Tres vidas iniciales** disponibles para atravesar las tres rondas.
- **Pérdida de vida:** una falla grave de apilamiento, como un derrumbe, consume una vida. Debe definirse con precisión qué tamaño de derrumbe cuenta como fallo y evitar pérdidas repetidas por la misma colisión.
- **Al agotar las tres vidas**, la partida termina y **el laberinto de carga / secuencia de tres rondas se reinicia desde la ronda 1**, con tres vidas restauradas y puntuación de la nueva partida desde cero. El récord histórico se conserva.
- **Entre pérdidas parciales de vida**, la ronda vigente no vuelve automáticamente a la ronda 1; queda por balancear si se limpia y repite esa misma ronda o se sigue con un estado controlado de la carga.
- **Al superar la tercera ronda**, la partida se completa, se muestra puntuación total y se compara con el récord.
- **Gana puntos** por cada pieza correctamente acomodada, con bonificaciones por peso/forma difícil, estabilidad y uso eficiente del espacio (ponderaciones pendientes).
- **Derrumbes, daños o piezas fuera del tráiler** pueden afectar puntuación y vidas según gravedad; evitar doble penalización involuntaria.
- **Récord:** mayor puntuación total de la partida completa, guardada localmente.
- Los umbrales de daño, cantidad de piezas por ronda, duración y puntajes deben balancearse posteriormente.

## Tecnología prevista

- Unity + C# y simulación de cuerpos rígidos; verificar determinismo suficiente y evitar penetración o inestabilidad explosiva de colisiones.
- OpenXR / XR Interaction Toolkit (según visor compatible) para apuntar, agarrar, elevar y colocar; la grúa puede modelarse como un mecanismo simplificado en lugar de un simulador realista complejo.
- Visor VR, manos/mandos virtuales; PC o visor autónomo según rendimiento de la física (no confirmado).
- Cámara virtual estacionaria elevada y controles de movimiento del objeto desacoplados de la cabeza del jugador para reducir mareos.
- Guardado local de récord sin cuentas.

## Primer prototipo y pruebas de aceptación

1. Un visor VR muestra desde arriba una caja abierta y piezas en el suelo.
2. Los mandos permiten controlar un agarre tipo grúa, elevar una caja y depositarla sobre otra.
3. Las cajas responden a masa, gravedad, fricción y colisiones al soltarlas.
4. Un apilamiento estable contabiliza puntos; una pieza inestable cae físicamente.
5. La dificultad crece con geometría, masa y menor espacio.
6. No existe conducción, trayecto ni extracción de cajas.
7. El HUD muestra puntuación, ronda (1 de 3), vidas restantes y récord.
8. Hay tres rondas de dificultad creciente; un derrumbe grave consume una de tres vidas, y al agotarse se reinicia la partida desde la primera ronda y se restauran las tres vidas.
9. Al completar la tercera ronda se muestra la puntuación acumulada y se evalúa el récord.
8. La cámara no se desplaza automáticamente con caídas o derrumbes, evitando mareo.
9. Se documentan visor, rendimiento y limitaciones físicas.

## Pendientes antes de desarrollar

- Determinar exactamente qué fallos cuentan como derrumbe grave y consumen vida, y cómo se restaura el estado de la ronda tras perder una vida sin agotar las tres.
- ¿Se pueden rotar cajas antes de colocarlas y con qué control?
- ¿Hay piezas que por peso requieran mecánica de grúa diferente?
- ¿Cómo se equilibra estabilidad, espacio libre, valor de la carga y velocidad en puntuación?
- ¿La grúa se manipula con rayos del mando o controles directos de movimiento?
- Definir modelo de visor, volumen de la caja y duración objetivo.

**Decisión clave:** *Carga equilibrada* será **VR con vista superior de grúa y solamente apilamiento**, no VR desde dentro del remolque ni juego combinado con conducción.
