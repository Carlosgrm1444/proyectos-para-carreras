# Fase 0 — Decisiones y preparación del MVP

**Estado:** completada con configuración provisional lista para iniciar la Fase 1.

**Fecha:** 23 de noviembre de 2026.

## Objetivo cerrado

El MVP será una experiencia educativa de Comercio Exterior en primera persona VR. El jugador recibe una mercancía, la inspecciona, escoge una de tres categorías, la transporta por el almacén y sólo conoce el resultado al depositarla. La partida termina cuando la recepción se satura.

## Decisiones del MVP

| Tema | Decisión para el primer prototipo |
|---|---|
| Plataforma | Windows x64 PC-VR |
| Motor | Unity 6000.6.10f1 |
| Render | URP 3D con materiales simples y alto contraste |
| Runtime XR | OpenXR + XR Interaction Toolkit |
| Locomoción | Continua mediante joystick; el teletransporte queda fuera del flujo principal |
| Giro | Giro por intervalos configurable como valor inicial; giro suave opcional |
| Postura | Debe funcionar sentado y de pie |
| Partida | Supervivencia individual de 3–5 minutos |
| Derrota | Recepción llena; no hay cronómetro individual oculto por mercancía |
| Récord | Mayor puntuación local de la partida |
| Feedback | Resultado, explicación y puntos sólo al depositar |
| Contenido | Categorías didácticas de alto nivel, sin fracciones arancelarias reales |
| Alcance | Una escena, un tipo de enemigo y una acción de repulsión |

## Controles VR definidos

La implementación debe usar acciones del Input System, no nombres de botones ligados a una marca específica de visor.

| Acción | Control inicial | Uso |
|---|---|---|
| Movimiento | Joystick izquierdo | Avanzar, retroceder y desplazarse lateralmente |
| Giro | Joystick derecho | Giro por intervalos; opción de giro suave en configuración |
| Agarrar/soltar | Grip de cada mano | Tomar, sostener y soltar mercancía |
| Inspeccionar | Movimiento de la mano | Acercar, girar y observar el objeto |
| Seleccionar opción | Trigger o puntero de la mano | Elegir una de tres categorías |
| Repulsión | Botón de acción | Alejar un enemigo cercano, con enfriamiento |
| Pausa | Botón Menu | Pausar toda la simulación y abrir opciones |
| Recentrar | Combinación definida por runtime | Reorientar la vista sin mover el mundo |

Si el visor elegido no dispone de un control equivalente, se remapea la acción, pero no se cambia la semántica del juego.

## Categorías iniciales

Para la vertical slice y la primera oleada se usarán tres destinos:

1. **Textiles** — icono de rollo o prenda.
2. **Plásticos y juguetes** — icono de cubo o pelota.
3. **Cerámica y vidrio** — icono de taza o recipiente.

Cada destino tendrá nombre, icono, silueta y color propios. El color será sólo una ayuda secundaria.

## Catálogo inicial de mercancías

La respuesta correcta es un dato interno. Las opciones mostradas siempre serán exactamente tres.

| Mercancía | Pista principal | Categoría correcta | Transporte inicial | Dificultad |
|---|---|---|---|---:|
| Playera | Tejido y producto terminado | Textiles | Mano | 1 |
| Pelota | Plástico y uso recreativo | Plásticos y juguetes | Mano | 1 |
| Taza | Cerámica y función de recipiente | Cerámica y vidrio | Mano | 1 |
| Rollo de tela | Material textil y volumen mayor | Textiles | Mano | 2 |
| Caja de juguetes | Plástico y conjunto recreativo | Plásticos y juguetes | Mano | 2 |
| Vaso de vidrio | Vidrio y fragilidad | Cerámica y vidrio | Mano | 2 |
| Caja de producto mixto | Presentación compuesta | Por definir en datos | Mano | 3 |
| Componente industrial | Pieza y función técnica | Por definir en datos | Mano | 3 |

Las dos últimas mercancías se mantienen fuera de la primera vertical slice hasta cerrar su explicación educativa y evitar ambigüedad. No se implementarán como clasificación oficial.

## Parámetros provisionales de balance

Estos valores serán editables y se probarán en la Fase 8:

- Capacidad máxima de recepción: 5 mercancías.
- Primer intervalo de llegada: 20 segundos.
- Intervalo mínimo: 7 segundos.
- Reducción por oleada: 1.5 segundos.
- Integridad inicial: 100 %.
- Daño estándar de enemigo: 20 %.
- Repulsión: alcance corto y enfriamiento inicial de 3 segundos.
- Duración objetivo: 3–5 minutos.
- Puntuación base: 100 puntos por entrega correcta al 100 % de integridad.
- Entrega incorrecta: 0 puntos y ruptura de combo; se podrá ajustar tras pruebas.

## Requisitos de hardware pendientes

La Fase 0 deja lista la configuración de software, pero no inventa datos del dispositivo. Antes de probar la Fase 2 se deben registrar:

- Modelo exacto del visor.
- Runtime utilizado: OpenXR Runtime seleccionado.
- Tipo de conexión: cable, Wi-Fi o visor autónomo.
- Modelo de controladores y estado de tracking.
- GPU, CPU y frecuencia del monitor espejo.
- Dimensiones del área física y altura de cámara.
- Modalidad de exhibición: sentado, de pie o ambas.

Estos datos son un bloqueo de prueba de hardware, no un bloqueo para crear la arquitectura inicial.

## Riesgos y mitigaciones

| Riesgo | Mitigación inicial |
|---|---|
| Mareo por movimiento | Velocidad moderada, giro por intervalos, viñeta opcional y pausa inmediata |
| Respuesta revelada antes de tiempo | Evaluador único que sólo se activa en el depósito |
| Destino confundible | Nombre, icono, silueta y sonido además del color |
| Mercancía inalcanzable | Puntos de aparición y zonas de depósito validados por pruebas |
| Enemigo atascado | Patrulla simple, límites de navegación y recuperación por timeout |
| Contenido ambiguo | Datos educativos separados de la lógica; explicaciones revisables |
| Rendimiento insuficiente | Graybox y perfil de rendimiento antes del arte |

## Puerta de salida de la Fase 0

- [x] Alcance del MVP definido.
- [x] Plataforma primaria definida provisionalmente.
- [x] Controles VR abstractos definidos.
- [x] Tres categorías iniciales definidas.
- [x] Catálogo inicial de ocho mercancías propuesto.
- [x] Valores de balance iniciales documentados.
- [x] Riesgos de comodidad identificados.
- [ ] Modelo de visor y área física registrados con el equipo real.

La única tarea pendiente de esta fase requiere acceso al equipo de exhibición. La Fase 1 puede comenzar con la configuración provisional de Unity y actualizar el perfil XR cuando se confirme el visor.

## Siguiente paso

Iniciar la Fase 1: crear la base Unity, configurar OpenXR/XR Interaction Toolkit, preparar escenas `Bootstrap` y `Comercio`, y establecer la arquitectura de datos y estados de partida.
