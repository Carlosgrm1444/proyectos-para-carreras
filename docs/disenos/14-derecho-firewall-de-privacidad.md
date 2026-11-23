# Derecho — Firewall de privacidad (arcade infinito de protección de datos)

> **Estado:** propuesta de diseño v0.1, mecánica central definida por el responsable; balance, detalles de interpretación jurídica y pruebas pendientes. **No es código ejecutable.**  
> **Referencia:** [05-derecho.md](../carreras/05-derecho.md), propuesta 6 «Firewall de privacidad».
> **Inspiración estética/jugable:** aventura de combate isométrico tipo *Tunic* y estructura de supervivencia por oleadas cercana a *ProyectVR*. No reutilizar recursos protegidos de otros videojuegos.

## Premisa

Juego tradicional de PC/monitor con **mando convencional**, cámara cenital/isométrica. Un personaje guardián protege un **Centro de Datos** (zona segura situada en el centro del mapa) y escolta **solicitudes legítimas de las personas**, representadas por personajes caminantes. Otras entidades hostiles representan intentos de acceso o tratamiento indebido de datos y atacan las solicitudes legítimas, al guardián o al centro.

La representación debe conservar una dimensión **jurídica de protección de datos personales**: consentimiento válido, licitud/finalidad/proporcionalidad, ejercicio de derechos ARCO, responsabilidad, protección de las personas y respeto a solicitudes legítimas. No reducir toda la explicación a hacking y redes. Las acciones de combate son **metáforas jugables**, no instrucciones ni recomendaciones de atacar a personas reales.

## Condición de juego: supervivencia infinita

- **No existe última ronda ni victoria final.** Las oleadas continúan con dificultad progresiva hasta que termina la partida.
- **Dos barras de vida independientes:** vida del guardián y vida del Centro de Datos.
- **Fin de partida inmediato si cualquiera de los dos llega a cero.** El jugador no puede continuar después de su muerte aunque el centro siga en pie, ni al revés.
- **Récord:** mayor puntuación acumulada en una sola partida.
- **Rachas:** aumentar la **probabilidad de aparición de powerups**, no dar powerups garantizados. Independientemente de las rachas, existe una probabilidad base de aparición aleatoria.
- **Dificultad progresiva:** más solicitudes y enemigos por unidad de tiempo, enemigos nuevos o combinados, rutas menos favorables y mayor presión para proteger y clasificar. Debe ser exigente sin saltos bruscos.

## Bucle jugable

```text
Aparecen solicitudes legítimas y amenazas en distintos accesos del mapa
       ↓
El guardián identifica rutas y amenazas; protege a las solicitudes buenas
       ↓
Solicitudes buenas llegan al Centro de Datos → puntos
       ↓
Enemigos intentan alcanzar solicitudes buenas, al guardián o al centro
       ↓
El jugador esquiva, bloquea, combate o intercepta según tipo de enemigo
       ↓
Aciertos/rachas suben probabilidad de powerups; también hay drops aleatorios
       ↓
Rondas progresivamente más intensas hasta que cae el guardián o el centro
       ↓
Puntuación final y récord local
```

## Entidades y consecuencias

| Entidad/evento | Comportamiento | Resultado |
|---|---|---|
| Solicitud legítima (personaje) | Intenta llegar por caminos accesibles al Centro de Datos | Si llega a salvo, suma puntos; representa ejercicio legítimo de derechos o tratamiento autorizado, según caso |
| Enemigo que ataca solicitud legítima | Puede interceptarla/eliminarla en ruta | Si se pierde una solicitud buena, **resta puntos** |
| Solicitud maliciosa / acceso indebido | Intenta ingresar al Centro de Datos | Si entra, **reduce la vida del centro** |
| Enemigo atacante del centro | Intenta causar daño directo al Centro de Datos | Cada impacto reduce vida del centro |
| Enemigo atacante del guardián | Acerca y golpea al personaje | Reduce la vida del guardián |
| Enemigo derrotado | Cesa su amenaza actual | Puede contribuir al combo y a una bonificación **por validar**, sin sustituir el premio principal por solicitudes legítimas protegidas |

**Regla de aggro solicitada:** enemigos capaces de atacar al jugador o al centro priorizan normalmente el **objetivo más cercano**, pero cuando el jugador golpea a un enemigo, éste cambia su atención para perseguir al jugador durante un tiempo (duración/condiciones por balancear). Los enemigos interceptores de solicitudes pueden tener su propia prioridad específica.

## Armas y su significado jurídico (por validar con docentes)

| Arma | Interacción | Significado narrativo |
|---|---|---|
| **Espada Firewall** | Ataque cuerpo a cuerpo; derrota amenazas próximas | Rechazo de acceso o tratamiento no autorizado; límite jurídico a usos indebidos |
| **Arco ARCO** | Ataque a distancia, diseñado contra enemigos vulnerables al arco | Derechos de **Acceso, Rectificación, Cancelación y Oposición**; se disparan respuestas/recursos simbólicos que corrigen o detienen situaciones de incumplimiento |
| **Escudo del Consentimiento** | Bloquea daño y permite contraatacar tras resistir ciertos golpes | Consentimiento, deber de información y medidas de protección, representados de manera simplificada |

**Importante:** los derechos ARCO son mecanismos que ejercen las personas ante responsables del tratamiento; **no son armas ni ataques**. El arco sólo los representa visualmente. El contenido contextual debe explicar que no sustituyen todas las obligaciones y bases jurídicas del tratamiento.

## Arquetipos de enemigos (propuestos para concretar)

| Tipo | Patrón | Respuesta requerida | Analogía jurídica aproximada |
|---|---|---|---|
| **Intruso** | Se acerca a la solicitud o al centro; ataque simple | Espada | Acceso indebido |
| **Fuga remota** | Ataca desde lejos, se aleja si el guardián se acerca | Arco ARCO | Divulgación no autorizada o vulneración de confidencialidad |
| **Impostor blindado** | Protegido hasta realizar un golpe telegráfico | Bloquear con escudo, luego contraatacar | Solicitud aparentemente legítima que exige revisión de identidad o legitimación |
| **Interceptador rápido** | Persigue solicitudes sanas y puede eliminarlas | Intercepción/esquiva y arma apropiada | Obstaculización del ejercicio de derechos |
| **Saboteador del centro** | Prioriza el centro para causarle daño | Detener antes de alcanzar el núcleo | Tratamiento indebido o incumplimiento grave |

Los enemigos deben ser variados: **no todos representan solicitudes cifradas o atacantes informáticos**. Incorporar figuras de recopilación excesiva, negativa injustificada de rectificación, uso para una finalidad distinta, divulgación indebida, etc., con contexto jurídico simplificado.

## Mapa

- Centro de Datos fijo como zona protegida central.
- Múltiples puntos periféricos de llegada, senderos y accesos donde caminan solicitudes legítimas y enemigos.
- Obstáculos, muros, corredores, atajos y zonas de cobertura alrededor del centro para diversificar rutas.
- Obstáculos que guían tanto las rutas de NPCs como las estrategias del jugador.
- Evitar situaciones imposibles donde una solicitud legítima está condenada a morir sin capacidad de reacción del jugador.
- Dificultad progresiva también por ubicación/temporización de amenazas y combinación de tipos, no sólo velocidad.

## Powerups, probabilidades y rachas

**Los powerups aparecen aleatoriamente incluso sin racha.** La racha incrementa gradualmente la probabilidad de aparición, **no garantiza** un drop por alcanzar cierto número de enemigos/solicitudes defendidas.

| Powerup | Efecto de juego | Significado temático propuesto |
|---|---|---|
| **Restauración de derechos** | Recupera vida **del guardián y del Centro de Datos** a la vez (hasta su máximo) | Reparación y restablecimiento de salvaguardas |
| **Respuesta oportuna** | Aumenta velocidad del guardián por tiempo limitado | Atención diligente de solicitudes |
| **Protección reforzada** | Aumenta defensa/resistencia o potencia de bloqueo | Medidas de seguridad y responsabilidad |
| **Defensa efectiva** | Aumenta fuerza/daño o eficacia de ataques | Herramientas para detener incumplimientos |
| **Impulso de alcance** | Mejora temporalmente el disparo del arco / velocidad de ataque | Capacidad de respuesta frente a vulneraciones |

La presencia de powerups aleatorios puede cambiar el ritmo sin invalidar la dificultad; los porcentajes, duración, stack y límites se determinarán al balancear.

**Lógica a especificar para Codex:** probabilidad base de drop > 0 y probabilidad aumentada por racha (con límite superior). No utilizar «cada X aciertos = drop asegurado». Definir cuándo se rompe la racha y si depende de protecciones correctas, enemigos detenidos o ambos.

## Puntos, supervivencia y derrota

- **+ puntos:** cada solicitud legítima que llega sana al centro.
- **− puntos:** por cada solicitud legítima eliminada por enemigos; posiblemente otras penalizaciones documentadas posteriormente.
- **Vida del centro disminuye:** si entra una solicitud mala o un enemigo golpea el centro.
- **Vida del jugador disminuye:** si un enemigo lo golpea.
- **Fin de partida:** vida del jugador = 0 **o** vida del centro = 0.
- **Mejor marca:** mayor puntuación final (partida infinita de supervivencia). Registrar también oleada y tiempo como métricas auxiliares opcionales.
- **No usar la duración de una sola ronda como victoria:** el jugador puede llegar a niveles indefinidamente crecientes.

Los valores de daño y puntuación, tasa de aparición de solicitudes, umbrales de racha, distribución de powerups, velocidad enemiga e invulnerabilidad breve tras recibir un golpe requieren balance y test.

## Arquitectura propuesta para Codex (aún no implementar)

Unity + C# como opción coherente con el [núcleo común](../arquitectura/00-arquitectura-y-criterios-de-museo.md). Sistemas lógicos sugeridos:

- `DataCenterHealth`: salud y recepción de entidades, triggers de derrota.
- `GuardianController`: locomoción, espada, arco, escudo, vida del guardián.
- `RequestSpawner` / `RequestOutcome`: NPCs legítimos y maliciosos, rutas y consecuencias.
- `ThreatDirector`: aparición y progresión de arquetipos, navegación con obstáculos y targeting dinámico.
- `ScoreAndStreak`: puntos y rachas separados de probabilidad de drop.
- `PowerUpDropSystem`: base aleatoria + modificación por rachas, aplicada mediante probabilidad acotada.
- `LeaderboardLocal`: mayor puntuación persistente.
- `Hud`: vida de jugador/centro, puntaje, racha, siguiente oleada y avisos de entrada.

**No copiar arte ni nombres de personajes de Tunic.** Usar sólo el género visual como referencia.

## Criterios iniciales de aceptación

1. Partida no tiene ronda final ni condición de victoria.
2. Gana puntos al llegar una solicitud buena al centro y pierde puntos si un enemigo elimina una solicitud buena.
3. Solicitud maliciosa que ingresa y enemigo que ataca disminuyen vida del centro.
4. Ataques a jugador disminuyen vida del guardián.
5. Juego termina en cuanto **cualquiera** de los dos llega a 0 de vida.
6. Espada, arco y escudo operan y son necesarios para responder a enemigos distintos, incluido enemigo que exige bloquear antes de contraatacar.
7. Enemigos con doble objetivo persiguen al cercano, pero reaccionan al ser golpeados por el jugador.
8. El escenario incluye paredes/obstáculos y rutas no triviales.
9. Las oleadas aumentan presión de forma gradual y se mantiene una puntuación comparable entre partidas.
10. Powerups aparecen con probabilidad no nula sin racha; las rachas elevan la probabilidad, **no la garantizan**.
11. Powerup de vida recupera vida de **ambos** (hasta el máximo); otros cambian temporalmente velocidad, daño o defensa.
12. El récord local conserva el máximo puntaje; se muestran las dos vidas y el puntaje mientras se juega.
13. La metáfora de cada enemigo/arma/poder puede explicarse en una frase con respaldo de conceptos legales validados por docentes.

## Pendientes de especificación antes de ejecución

- Confirmación docente de significados jurídicos, distinción entre solicitudes legítimas/indebidas, y condiciones que la persona jugadora puede identificar.
- Diseño gráfico propio: protagonista, centro, 5 arquetipos y señales visuales de armas.
- Números: vida inicial, daño, ritmo de aparición, score, combo y probabilidades máximas.
- ¿Las solicitudes malas se diferencian visualmente por pista verificable o deben investigarse antes de impedir su llegada? Evitar sancionar al jugador por una condición imposible de distinguir.
- Duración media esperada y control de dificultad para exposición; mecanismo de salida voluntaria del juego infinito.
- Control exacto del arco y escudo en un mando, prioridad de ataques a NPCs y cómo se evita daño accidental a solicitudes buenas.
- Modo accesible, manejo offline, reinicio seguro y registro local de récord.

**Decisiones registradas:** arcade infinito, muerte del guardián **o** destrucción del centro = fin, rachas aumentan sólo probabilidad de poder y también hay drops base aleatorios.

