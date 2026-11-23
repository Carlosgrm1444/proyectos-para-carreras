# Comercio Exterior - Clasificador en caos

## Estado

**Concepto activo: Clasificador en caos, ahora planteado como videojuego VR en primera persona.** La especificación prioritaria está en [Clasificador en caos VR — movimiento con joystick, tres opciones, entrega diferida](../disenos/12-comercio-exterior-clasificador-en-caos-vr.md). Esta sigue siendo la única idea activa para Comercio Exterior; las alternativas iniciales se descartan por ahora.

**Nota de versión:** las secciones tradicionales de esta ficha (banda, herramientas, RFID, cámara cenital, proyección sobre mesa) se conservan como antecedentes y banco de mecánicas, **no como requisitos aprobados para la versión VR**. En caso de contradicción, prevalece el documento VR.

## Base académica

El proceso elegido se inspira en clasificación arancelaria y merceología, materias del plan de Licenciado en Comercio Exterior. El alcance del juego será educativo: usar categorías ficticias o de alto nivel validadas por docentes, nunca proporcionar fracciones arancelarias reales ni asesoría aduanera.

- [Documento curricular oficial](https://www.uat.edu.mx/DocumentosCurriculares2023/LICENCIADO%20EN%20COMERCIO%20EXTERIOR%20DC_RC%202023.pdf)
- [Malla curricular oficial](https://www.uat.edu.mx/MallasCurriculares2023/LICENCIADO%20EN%20COMERCIO%20EXTERIOR%20MC_RC%202023.pdf)

## El proceso real, explicado sencillo

Antes de importar o exportar una mercancía, se necesita identificar correctamente qué es. Para clasificarla se consideran aspectos como:

- **Material:** plástico, metal, cerámica, tela, papel, etc.
- **Función:** para qué sirve el objeto.
- **Presentación:** si es una pieza terminada, componente, juguete, empaque o conjunto.
- **Características relevantes:** tamaño, peso, composición o uso principal.

Ejemplo sencillo: una taza de cerámica y un vaso de plástico sirven para beber, pero no pertenecen a la misma familia porque están fabricados con materiales distintos. En el juego esta decisión no se responderá con un botón: el jugador debe llevar físicamente la mercancía a la zona que corresponde.

## Fantasía del juego

El jugador es un operador de un almacén aduanero que debe impedir que la zona de recepción se sature. Una banda transportadora entrega mercancías continuamente; el jugador las identifica, escoge la herramienta correcta, atraviesa el almacén y las deposita en su área de clasificación.

La partida debe sentirse como un juego arcade de supervivencia: cada vez llegan más objetos, hay más tipos, aparecen rutas bloqueadas y enemigos que generan caos. La recompensa es una puntuación alta y el récord de la estación.

## Objetivo

Clasificar la mayor cantidad posible de mercancías antes de que la recepción se llene, se dañen demasiados objetos o se agote la tolerancia operativa de la partida.

## Bucle principal

```text
Mercancía llega por banda
        ↓
Observar material, función y peso
        ↓
Elegir herramienta de traslado
        ↓
Moverse por el almacén y superar obstáculos
        ↓
Llevar mercancía a la zona correcta
        ↓
Recibir puntos, combo y posible bonificación
        ↓
La siguiente oleada llega con más presión
```

## El personaje

El personaje no sólo responde preguntas: se mueve dentro del almacén.

Acciones iniciales:

- Caminar y correr.
- Recoger y soltar mercancías pequeñas.
- Empujar un diablito.
- Subir a un montacargas.
- Saltar o esquivar obstáculos simples.
- Activar puertas, interruptores o rutas alternativas.
- Depositar mercancía en una zona de clasificación.

## Mercancías educativas

Cada mercancía tiene datos ocultos para el sistema y pistas visibles para el jugador.

| Propiedad | Para qué sirve en gameplay | Ejemplo |
|---|---|---|
| Material | Define parte de su categoría | Taza de cerámica, juguete de plástico |
| Función | Distingue objetos con forma parecida | Contenedor, herramienta, componente |
| Peso/tamaño | Define herramienta necesaria | Caja ligera, bulto mediano, tarima pesada |
| Fragilidad | Cambia velocidad y riesgo de transporte | Vaso, aparato, tela |
| Dificultad | Define puntos otorgados | Objeto básico, compuesto o ambiguo |

Primera colección sugerida:

| Mercancía | Pista educativa | Traslado | Puntos base |
|---|---|---|---:|
| Playera | Textil, producto terminado | Mano | 10 |
| Pelota | Plástico, juguete | Mano | 10 |
| Taza | Cerámica, utensilio | Mano; no se debe golpear | 15 |
| Caja de juguetes | Producto mixto, volumen medio | Diablito | 20 |
| Rollo de tela | Textil, voluminoso | Diablito | 20 |
| Tarima de componentes | Pieza industrial, pesada | Montacargas | 30 |
| Caja frágil de vidrio | Material frágil, manipulación lenta | Mano o diablito con cuidado | 25 |

Los nombres, materiales y zonas definitivas deben ser revisados por docente de Comercio Exterior antes de producción.

## Zonas de clasificación

En la primera versión conviene usar pocas zonas, claramente distintas por icono, forma y color:

1. Textiles.
2. Plásticos y juguetes.
3. Cerámica y vidrio.
4. Componentes industriales.

No usar sólo color: cada zona tendrá icono grande, forma de entrada distinta y señal auditiva opcional.

Conforme sube la dificultad pueden agregarse zonas o subcategorías, por ejemplo:

- Producto terminado frente a componente.
- Carga frágil.
- Mercancía que necesita revisión.
- Mercancía compuesta.

## Herramientas de traslado

La elección de herramienta es parte del aprendizaje y de la estrategia; no es una animación automática.

| Herramienta | Puede mover | Ventaja | Desventaja | Error posible |
|---|---|---|---|---|
| Manos | Objetos ligeros y pequeños | Muy rápida, pasa por espacios estrechos | Sólo un objeto; no mueve peso alto | Intentar tomar una tarima hace perder tiempo |
| Diablito | Cajas y bultos medianos | Mueve más volumen; estable en rectas | Giro lento, ocupa espacio, puede atorarse | Chocar puede hacer caer o retrasar la carga |
| Montacargas | Tarimas y objetos pesados | Única opción para carga pesada; mueve varios bultos | Lento, ancho, no pasa por rutas estrechas | Usarlo con mercancía pequeña desperdicia tiempo; chocar daña carga |

Reglas de seguridad arcade:

- Un objeto frágil transportado demasiado rápido puede quedar marcado como dañado.
- El montacargas no puede cruzar pasillos angostos.
- El diablito requiere alinear el objeto antes de empujarlo.
- Las manos permiten saltar y tomar atajos, pero no cargar peso alto.
- Elegir una herramienta incorrecta no debe bloquear la partida; debe costar tiempo, puntos o combo.

## Obstáculos y enemigos

Los enemigos representan caos operativo, no personas reales. Deben ser visualmente claros y no violentos.

| Elemento | Qué representa | Efecto en juego |
|---|---|---|
| Cajas caídas | Pasillo obstruido | Obliga a rodear o abrir una ruta |
| Charco o piso resbaloso | Riesgo operativo | Reduce control y puede hacer caer carga |
| Puerta cerrada | Acceso restringido | Requiere activar interruptor o tomar otra ruta |
| Robot averiado | Equipo fuera de servicio | Bloquea temporalmente una zona |
| Carrito rebelde | Movimiento inesperado de carga | Empuja al jugador o altera trayectorias |
| Inspector dron ficticio | Revisión adicional | Detiene una mercancía y exige llevarla a revisión |
| Bicho de inventario | Error de ubicación o registro | Cambia temporalmente la señal de una zona; el jugador debe confirmar con pistas |

Los enemigos no deben matar al personaje. Generan retraso, desorden, pérdida de mercancía o ruptura de combo.

## Progresión de dificultad

| Oleada | Tiempo aproximado | Qué cambia |
|---|---:|---|
| 1: inducción | 0–30 s | Dos categorías, objetos ligeros, banda lenta, sin enemigos |
| 2: operación normal | 30–60 s | Cuatro categorías, aparecen diablito y fragilidad |
| 3: hora pico | 60–90 s | Mayor frecuencia, primer objeto pesado y rutas bloqueadas |
| 4: congestión | 90–120 s | Enemigos, mercancías similares y dos objetos activos |
| 5: operación crítica | 120–150 s | Montacargas, inspección especial y banda muy rápida |
| Supervivencia | 150 s en adelante | Frecuencia, variedad, incidentes y rutas aumentan hasta perder |

La dificultad no debe crecer solamente porque todo va más rápido. Debe introducir decisiones nuevas: herramienta, categoría, ruta, fragilidad y prioridad.

## Puntuación y récord

### Puntos por clasificación

```text
Puntos = valor base de la mercancía
       + bonificación por herramienta correcta
       + bonificación por rapidez
       + multiplicador de combo
       - penalización por daño, error o demora
```

Propuesta inicial:

| Acción | Puntos |
|---|---:|
| Clasificar objeto básico correctamente | +10 |
| Clasificar objeto mediano correctamente | +20 |
| Clasificar objeto pesado o frágil correctamente | +30 |
| Elegir herramienta adecuada | +5 |
| Entregar con tiempo alto restante | +5 a +15 |
| Combo de 3 | multiplicador ×1.5 |
| Combo de 5 | multiplicador ×2 |
| Combo de 10 | multiplicador ×3 y bonificación de tiempo |
| Categoría equivocada | -10 y rompe combo |
| Herramienta inadecuada | -5 o retraso |
| Mercancía dañada | -20 |
| Bandeja llena | termina partida |

### Récords

- Récord individual del día.
- Récord histórico individual.
- Récord cooperativo, si se habilita modo de dos jugadores.
- Mostrar en la pantalla inicial: “Récord actual: 4,250 puntos”.
- Al terminar: mostrar cuántas clasificaciones correctas, mejor combo, oleada alcanzada y puntos faltantes para el podio.
- Pedir sólo iniciales opcionales para guardar un nuevo récord.

## Condiciones de derrota

La partida termina cuando ocurre una de estas condiciones:

1. La zona de recepción acumula demasiadas mercancías.
2. Se alcanza el máximo de mercancías dañadas.
3. Se pierde toda la tolerancia operativa por errores graves.
4. En modo de tiempo fijo, termina el cronómetro.

La condición principal recomendada es la **bandeja de recepción llena**, porque se entiende de inmediato y visualmente comunica el caos acumulado.

## Modos de juego

### Supervivencia individual

Una persona intenta durar y puntuar lo más posible. Es el modo principal para museo.

### Cooperativo de dos jugadores

- Jugador A: clasifica y mueve objetos ligeros.
- Jugador B: opera diablito y montacargas.
- Ambos comparten bandeja, errores y récord.
- Ciertas mercancías requieren que uno abra ruta mientras otro transporta.

### Versus por turnos

Dos visitantes juegan la misma secuencia de oleadas; gana quien obtenga mayor puntuación. Esto evita que una partida dependa de suerte.

## Primera versión jugable: MVP

No iniciar con visión artificial, VR, montacargas complejo ni muchos objetos. Primero validar que mover, escoger herramienta y clasificar sea divertido.

Incluye:

- Una escena de almacén simple.
- Un personaje jugable.
- Banda transportadora visual.
- Cuatro zonas de clasificación.
- Cuatro mercancías: playera, pelota, taza y caja de juguetes.
- Manos y diablito.
- Un obstáculo: cajas caídas.
- Una bandeja de recepción que se llena.
- Puntos, combo, dificultad por oleadas y récord local.
- Ronda de dos minutos más modo supervivencia opcional.

Cuando ese núcleo funcione, añadir en este orden:

1. Fragilidad y daño.
2. Montacargas y tarima pesada.
3. Rutas bloqueadas y enemigos de caos.
4. Dos jugadores.
5. Props físicos, cámara o visión artificial.
6. AR/VR sólo si aporta algo que el modo pantalla no logra.

## Tecnologías posibles

| Nivel | Qué se agrega | Para qué aporta |
|---|---|---|
| Base | Unity, C#, controles, físicas y guardado local | Validar diversión, dificultad y récord |
| Interacción física | Botones grandes, diablito/mercancías impresas en 3D | Hacer visible el proceso al público |
| Cámara | ArUco o RFID en props | Reconocer la mercancía real llevada a la estación |
| Visión artificial | Clasificador local de objetos | Mostrar cómo una cámara puede ayudar a reconocer mercancía; siempre con respaldo |
| Proyección | Mapa o banda proyectados en mesa | Convertir el espacio físico en almacén |
| VR | Inspección de carga o almacén | Experiencia individual supervisada; no necesaria para el MVP |

## Propuesta tecnológica: estación híbrida de almacén aduanero

La versión de museo recomendada no es un videojuego aislado en una pantalla. Es una instalación donde el visitante toca una mercancía real, la registra en una zona física, toma decisiones mediante herramientas y ve cómo esas decisiones cambian el almacén digital.

### Flujo de una mercancía física

```text
Prop físico llega a recepción
        ↓
Cámara o lector identifica el prop
        ↓
El juego crea la mercancía digital y muestra sus pistas
        ↓
Visitante escoge manos, diablito o montacargas
        ↓
Transporta el objeto en el mundo virtual
        ↓
Deposita el prop en una estación física de clasificación
        ↓
Lector valida zona + juego da puntos, combo o penalización
```

### Tecnologías por componente

| Componente | Tecnología propuesta | Función dentro de la experiencia | Por qué no es decoración |
|---|---|---|---|
| Pantalla principal | PC con Unity + pantalla de 55 a 65 pulgadas o proyector | Muestra personaje, banda, enemigos, oleadas y récord | Es el mundo compartido que observa el público |
| Mercancías | Props robustos impresos en 3D, con materiales o acabados distintos | Permiten observar peso, forma, fragilidad y tipo de mercancía | La propiedad física decide herramienta y riesgo |
| Identificación fiable | Marcadores ArUco en base del prop o etiquetas RFID/NFC | Reconoce qué objeto llegó y en qué estación se depositó | Une la mercancía física con su gemelo digital |
| Cámara cenital | Webcam USB fija + OpenCV | Detecta posición, orientación y movimiento de props sobre la mesa | Puede bloquear o abrir rutas según dónde esté la carga |
| Visión artificial opcional | Clasificador local de objetos | Intenta reconocer la mercancía sin marcador y muestra confianza | Enseña una aplicación real de visión, pero conserva ArUco/RFID como respaldo |
| Herramientas | Control convencional, joystick industrial grande y botones físicos | Cambian cómo se mueve el avatar o vehículo en el juego | Manos, diablito y montacargas tienen controles y límites distintos |
| Diablito físico | Asa con sensor de inclinación o encoder; alternativamente palanca grande | Activa modo diablito y simula empujar/cargar | Introduce costo espacial y velocidad distinta de usar las manos |
| Montacargas | Joystick de dos ejes y botones de elevar/bajar | Controla el montacargas virtual | Permite mover tarimas, pero no entrar a pasillos angostos |
| Zonas de clasificación | Bases con lector RFID/NFC, marcador o botón de confirmación | Saben dónde terminó cada mercancía | La clasificación se valida en el espacio físico, no en un menú |
| Retroalimentación | LEDs, torre semáforo, bocina y vibración | Señala carga correcta, daño, atasco o recepción llena | Permite entender estado sin leer mucho texto |
| Proyección mapping | Proyector cenital sobre mesa | Dibuja rutas, zonas bloqueadas, bandas y peligros alrededor de los props | Convierte la mesa física en parte del nivel jugable |
| Récord | Panel LED secundario o monitor pequeño | Muestra récord actual, combo y oleada | Hace visible la competencia a quienes están esperando |

## Configuración recomendada para la primera instalación

### Estación de dos niveles

**Nivel físico, frente al visitante**

- Mesa de recepción y clasificación de aproximadamente 120 × 70 cm.
- Seis o siete mercancías impresas en 3D, grandes y lavables.
- Tres zonas físicas de clasificación para el primer piloto.
- Un joystick grande para montacargas, una palanca para diablito y dos botones para manos/soltar.
- Torre de LEDs para normal, atasco, riesgo y fin de ronda.

**Nivel digital, visible para el público**

- Pantalla vertical u horizontal grande detrás de la mesa.
- Almacén digital en Unity con banda, personaje, herramientas, obstáculos y enemigos de caos.
- Panel de récord y oleada visible desde lejos.
- Modo atracción automático cuando nadie juega: mercancías se acumulan y se muestra “¿puedes superar 4,250 puntos?”.

### Hardware y software sugeridos

| Área | Recomendación inicial |
|---|---|
| Motor | Unity 2022.3 LTS o versión LTS compatible con el equipo |
| Juego | C#, Universal Render Pipeline, Unity Input System y guardado local de récords |
| PC | Equipo con GPU dedicada; no depender de nube ni Internet |
| Visión | Cámara USB cenital + OpenCV; ArUco primero, clasificador visual después |
| Electrónica | ESP32 programado con PlatformIO, conectado por USB serial |
| Sensores | RFID/NFC o ArUco para identidad; botones, encoder y opcional celda de carga |
| Datos | JSON o SQLite local para récords y configuración de oleadas |
| Modelos 3D | Blender y piezas impresas en PLA/PETG; duplicados para desgaste |
| Proyección | Proyector de tiro corto si la iluminación del museo permite calibrarlo |

**Recomendación histórica superada:** se proponía no iniciar con VR por el flujo de visitantes y la higiene. El diseño actual del responsable elige **VR en primera persona como modalidad principal**; se deben mitigar explícitamente esos riesgos según [la ficha VR](../disenos/12-comercio-exterior-clasificador-en-caos-vr.md).

## Cómo implementar visión artificial sin volver frágil la estación

La mejor estrategia es usar tres capas, de menor a mayor riesgo:

1. **ArUco o RFID como verdad del juego.** Cada prop tiene un identificador seguro; la ronda no falla por iluminación.
2. **Cámara cenital para posición.** Detecta si una mercancía está en recepción, en tránsito o dentro de una zona.
3. **Clasificador visual local como reto extra.** Una cámara intenta reconocer el tipo de mercancía y enseña porcentaje de confianza. Si no reconoce con seguridad, el juego pide revisión humana; nunca bloquea la partida.

Esto permite decir con honestidad que la estación usa visión artificial, sin dejar que una mala iluminación arruine la experiencia.

## Integraciones físicas que sí aportan gameplay

| Integración | Uso jugable | Prioridad |
|---|---|---|
| Botón de paro/pausa | Detiene momentáneamente banda ante atasco; usarlo cuesta segundos | Alta |
| Encoder del diablito | La velocidad de empuje física afecta velocidad virtual | Media |
| Joystick de montacargas | Mover carga pesada con inercia y giro amplio | Alta |
| Celda de carga | Determina si una mercancía rebasa el límite de manos | Media |
| RFID/NFC | Valida objeto y zona final | Alta |
| LED en cada zona | Indica destino disponible, error o revisión | Alta |
| Proyector | Muestra carriles bloqueados y rutas temporales sobre la mesa | Media |
| Impresora térmica | Entrega ticket de puntuación o “dictamen educativo” | Baja |

## Desarrollo por fases

| Fase | Resultado | Tecnologías |
|---|---|---|
| 1. Diversión | Personaje, banda, cuatro categorías, puntos, combo, derrota y récord | Unity + control/teclado |
| 2. Herramientas | Manos, diablito, montacargas y físicas diferenciadas | Unity + Input System |
| 3. Museo físico | Props, zonas y LEDs conectados | ESP32 + RFID/ArUco |
| 4. Mesa viva | Cámara cenital y proyección con rutas/peligros | OpenCV + proyector |
| 5. IA demostrable | Clasificador local con confianza y revisión | Modelo local + OpenCV/Sentis |
| 6. Expansión | Cooperativo, más oleadas, más categorías y tickets | Red local opcional + electrónica adicional |

La fase 1 debe ser divertida aun sin cámara, props o proyector. Cada fase posterior añade una capa tangible sin cambiar el núcleo del juego.

## Decisiones pendientes con docentes

- Categorías educativas exactas.
- Lista de mercancías y propiedades que deben importar.
- Nivel de fidelidad con el proceso real.
- Palabras que pueden usarse en pantalla sin confundir al visitante.
- Errores que deben ser graves y errores que sólo deben restar puntos.
- Si se desea enfatizar clasificación, logística o seguridad de manejo.

## Criterio para pasar a desarrollo

Podemos empezar a implementar cuando confirmemos estas tres decisiones:

1. Las cuatro categorías iniciales.
2. Las cuatro mercancías del MVP.
3. ~~Si el primer prototipo será para teclado/control o directamente para VR.~~ **Se ha seleccionado VR en primera persona con locomoción por joystick**; resta confirmar visor, espacio y controles concretos.

El núcleo recomendado es Unity con C#, usando el patrón de puntuación, dificultad progresiva y récord de ProyectVR/Math Party.


