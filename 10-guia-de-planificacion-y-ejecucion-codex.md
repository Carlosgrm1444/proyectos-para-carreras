# Protocolo de planificación de minijuegos y entrega a Codex

> Estado: marco de trabajo documental, 22 de noviembre de 2026. No inicia programación.  
> Alcance: todas las carreras del [banco de ideas](README.md), incluida la posible estación esférica de [LIIA — Informática e Inteligencia Artificial](09-liia-esfera-interactiva.md).  
> El repositorio actual es un banco de conceptos: no confundir los 49 conceptos con 49 desarrollos comprometidos.

## Principios

1. **Diseño antes de código.** Un concepto requiere validación pedagógica, lúdica y técnica antes de autorizar su ejecución.
2. **Separar hechos y supuestos.** Cualquier hardware se documenta como confirmado, observado, propuesto o pendiente.
3. **Una tarea verificable por entrega.** Codex trabaja con alcance, archivos, criterios de aceptación, pruebas y salida esperada explícitos.
4. **Ramas y Pull Requests.** No modificar `main` directamente; hacer revisión humana antes de integrar.
5. **Reutilización.** Aprovechar ArcadeCore (GameFlow, ScoreSystem, ComboSystem, DifficultyDirector, LeaderboardLocal, InputAdapters) documentado en [arquitectura](00-arquitectura-y-criterios-de-museo.md).
6. **Accesibilidad, privacidad y operación de museo.** Jugar sin Internet, sin datos personales, con respaldo si falla una tecnología, tutorial breve y reinicio fácil.
7. **No crear software sobre tecnología imaginada.** Sin modelos, conectores o protocolos confirmados, primero diseñar pruebas de factibilidad.

## Inventario y seguimiento

| ID propuesto | Carrera/alcance | Documento base | Estado inicial | Siguiente decisión |
|---|---|---|---|---|
| ADM | Administración | [01](01-administracion.md) | Banco de 8 conceptos | Elegir candidatos con docentes |
| ATC | Autotransporte de Carga | [02](02-autotransporte-de-carga.md) | Banco de 8 conceptos | Elegir candidatos con docentes |
| CEX | Comercio Exterior | [03](03-comercio-exterior.md) | Una idea activa: Clasificador en caos | Validar mecánica y alcance |
| CON | Contador Público | [04](04-contador-publico.md) | Banco de 8 conceptos | Elegir candidatos con docentes |
| DER | Derecho | [05](05-derecho.md) | Banco de 8 conceptos | Elegir candidatos con docentes |
| PSI | Psicología | [06](06-psicologia.md) | Banco de 8 conceptos | Elegir candidatos con docentes |
| LIIA | Informática e Inteligencia Artificial | [07](07-informatica-e-inteligencia-artificial.md) | Banco de 8 conceptos; [Laberinto gravitatorio esférico seleccionado](15-liia-laberinto-gravitatorio-esferico.md) | Validar físicas, control electrónico y factibilidad de visualización de la esfera |

**No duplicar carrera:** la estación esférica pertenece a LIIA (Licenciatura en Informática e Inteligencia Artificial) y no se contará como otra carrera. Comercio Exterior y Aduanas permanece como referencia histórica fuera del alcance según el README vigente.

## Etapas de cada proyecto

| Etapa | Resultado | Requisito de salida |
|---|---|---|
| P0 — Contexto | Carrera, audiencia, proceso real y responsable | Objetivo validable |
| P1 — Selección | 2–3 candidatos y matriz de criterios | Una elección argumentada |
| P2 — Diseño jugable | Ficha de juego con controles, reglas, reto, puntuación y derrota | Mecánica comprensible sin explicación larga |
| P3 — Factibilidad | Inventario de hardware, software, riesgos y plan B | Sin dependencias técnicas críticas no verificadas |
| P4 — Especificación | Historia de usuario, estados, datos, diagramas y checklist | Criterios de aceptación comprobables |
| P5 — Paquetes para Codex | Tareas pequeñas, dependencias, instrucciones y límites | PRs planificados, no bloqueados |
| P6 — Implementación | Prototipo con evidencias de build y pruebas | Código revisado y ejecutable |
| P7 — Validación | Prueba con usuarios/docentes, incidencias, métricas | Aprobación o lista de correcciones |
| P8 — Instalación | Manual, plan de fallas, respaldo, operación y mantenimiento | Estación operable por personal no desarrollador |

Hasta nueva decisión, el conjunto de proyectos permanece en P0–P3. **No se autoriza automáticamente P6.**

## Plantilla de ficha de juego (crear una por concepto aprobado)

```markdown
# [ID] Nombre del videojuego
- Estado / versión / fecha:
- Carrera y responsable de validación:
- Proceso profesional que representa:
- Audiencia y contexto de exhibición:
- Resultado de aprendizaje (observable):
- Idea central en una frase:
- Avatar/elemento manipulable:
- Acción principal y controles:
- Objetos, herramientas y obstáculos:
- Bucle de juego (paso a paso):
- Reglas y estados (inicio, tutorial, juego, resultado, reinicio):
- Reglas de puntuación, combo, bonificaciones y penalizaciones:
- Progresión de dificultad con ejemplos:
- Condición de victoria/derrota y duración:
- Modos y cantidad de jugadores:
- Accesibilidad, privacidad y alternativa sin sensores:
- Hardware/software confirmado (fuente):
- Hardware/software propuesto y por verificar:
- Integración con ArcadeCore:
- Arte, UI, audio y licencias:
- Fuera de alcance:
- Riesgos y mitigación:
- Dependencias y decisiones pendientes:
- Casos de prueba y criterios de aceptación:
- Firma/validación académica y técnica:
```

## Formato obligatorio del paquete de trabajo para Codex

```markdown
# TASK-[ID]-[NN] — Acción concreta
- Contexto del proyecto y archivos fuente (rutas exactas):
- Objetivo funcional:
- Entrada y salida esperada:
- Alcance incluido y explícitamente excluido:
- Rama de trabajo / rama base:
- Dependencias y prerrequisitos confirmados:
- Herramientas, versión de motor y sistema operativo:
- Diseño de arquitectura y convenciones:
- Archivos previstos / estructuras a preservar:
- Pasos de implementación sugeridos (sin sobreespecificar lo trivial):
- Criterios de aceptación medibles:
- Pruebas: unitarias, integración, manuales, build y evidencia:
- Seguridad, privacidad, accesibilidad y funcionamiento offline:
- Documentación que debe actualizar:
- Condiciones para bloquear o pedir decisión:
- Entregable: PR con resumen, capturas/logs y riesgos conocidos.
```

**Instrucción estándar para Codex:** leer primero `README.md`, `00-arquitectura-y-criterios-de-museo.md`, la ficha de la carrera y este protocolo. No inventar detalles de hardware, dependencias o reglas académicas. Si falta información crítica, documentar el bloqueo y no fingir que se probó.

## Criterios de calidad compartidos

- Demostración breve y jugable: objetivo reconocible tras un tutorial de 10–15 segundos; duración habitual de 90–180 segundos.
- Acciones relacionadas con la profesión, no simple trivia.
- Resultado reproducible: versión de motor, dependencias, comando de build, evidencia y logs pertinentes.
- Funcionalidad desconectada de Internet.
- Controles accesibles, alto contraste, subtítulos/indicaciones visuales y opción alternativa.
- Sin almacenar audio/video de participantes ni información personal identificable.
- Puntaje local y reinicio; recuperación ante cierre inesperado.
- Pruebas para fallas de sensores o de hardware especial.
- Se registran los supuestos, riesgos y decisiones sin afirmar verificaciones inexistentes.

## Matriz de selección de conceptos (llenar antes de construir)

Puntuar de 0 a 3 por criterio (0 = no cumple, 3 = excelente), dejando justificación y evidencia:
- Representatividad del proceso académico.
- Comprensión del objetivo en menos de 20 segundos.
- Diversión y deseo de volver a jugar.
- Mecánica con decisiones significativas y dificultad creciente.
- Factibilidad con equipo y tiempo disponibles.
- Confiabilidad para uso repetitivo de museo.
- Accesibilidad y posibilidad de modo de respaldo.

El resultado orienta la selección, **no** sustituye la validación docente ni una prueba con visitantes.

## Registro de decisiones (formato)

| ID | Fecha | Proyecto | Decisión | Evidencia | Alternativas | Responsable validador | Consecuencias |
|---|---|---|---|---|---|---|---|
| DEC-001 | 2026-11-22 | LIIA-ESF | Considerar esfera como elemento físico central de LIIA, sujeto a factibilidad | Video de referencia | Pantalla auxiliar, esfera simulada | Pendiente | No programar adaptador hasta confirmar equipo |

## Política de cambios

- Una modificación de concepto debe actualizar ficha de proyecto, matriz de decisiones y, si cambia el software, sus criterios de aceptación.
- Los desarrollos de Codex se realizan en ramas y PRs separados; ningún merge automático por una decisión documental.
- El propietario del proyecto decide prioridades; docentes validan la exactitud del proceso; un responsable técnico verifica factibilidad y pruebas.
- No se afirma que la documentación sea un prototipo o que una integración haya sido probada cuando no lo está.
