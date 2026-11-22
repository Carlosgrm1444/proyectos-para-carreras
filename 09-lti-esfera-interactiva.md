# LTI — Estación esférica interactiva (concepto y ficha técnica preliminar)

> **Estado:** documentación inicial / investigación de factibilidad. **No aprobado para programación ni instalación.**  
> **Carrera destinataria:** Licenciatura en Tecnologías de la Información (LTI). Esta estación se registra por separado de las ocho ideas de `07-informatica-e-inteligencia-artificial.md`, hasta decidir con la facultad su relación curricular.  
> **Referencia visual:** video `1000455433.mp4`, aportado en la conversación de planificación del 22 de noviembre de 2026. El video **no está incorporado a este repositorio**.  
> **Decisión inicial del proyecto:** reservar la esfera como posible elemento central de la experiencia de LTI, **condicionado** a confirmar su tecnología y permisos de uso.

## 1. Objetivo

Explorar un videojuego arcade educativo, de duración breve y puntuación competitiva, que utilice una esfera física como soporte visual o escenográfico central. El juego debe enseñar un proceso representativo de LTI mediante acciones y consecuencias, no mediante un cuestionario.

Se seguirá el núcleo común documentado en [00-arquitectura-y-criterios-de-museo.md](00-arquitectura-y-criterios-de-museo.md): sesiones de 90 a 180 segundos, tutorial práctico corto, combos, dificultad progresiva, récords locales, funcionamiento sin Internet y modo de respaldo.

**Importante:** utilizar una esfera como soporte físico no significa que la esfera esté preparada para mostrar gráficos interactivos. La factibilidad del hardware es un prerrequisito de la implementación.

## 2. Qué muestra realmente la evidencia

Observaciones limitadas a la grabación proporcionada:

| Observación del video | Evidencia / límite |
|---|---|
| Una esfera grande está suspendida a cierta altura, próxima al techo de un espacio interior | Visible |
| La superficie se ve plateada, muy reflectante, con un patrón de parches o facetas rectangulares | Visible |
| La esfera refleja luminarias y elementos de la sala | Visible |
| El video no muestra una sesión de juego, un cursor, un marcador ni interacción del público | No demostrado |
| No se aprecia con certeza el tipo de material, pantalla, proyector, cableado, controlador ni protocolo de video | No determinado |
| No se puede inferir diámetro exacto, altura de montaje, peso, brillo, resolución ni latencia | No determinado |
| No se demuestra que acepte HDMI, video en tiempo real, un flujo de Unity o proyección calibrada | No demostrado |

**Hipótesis que NO debe presentarse como hecho:** puede ser una esfera decorativa reflectante o una instalación con capacidades de visualización que no se distinguen en el clip. La apariencia no basta para identificar la tecnología.

### Restricción crítica

Si la esfera es esencialmente un objeto reflectante/espejado, **no debe asumirse** que proyectar imágenes sobre ella dará una pantalla legible: la reflexión especular, los puntos brillantes, las facetas y la iluminación ambiental pueden impedirlo. No instalar proyectores, cámaras, soportes ni modificar la esfera sin autorización y una prueba controlada.

## 3. Preguntas que deben responderse antes de diseñar para el hardware

| ID | Pregunta / evidencia requerida | Estado |
|---|---|---|
| H-01 | ¿Quién es propietario o responsable de la esfera y autoriza pruebas? | Pendiente |
| H-02 | ¿La esfera es decorativa reflectante, una pantalla LED esférica, una superficie de proyección u otro dispositivo? Marca/modelo/ficha técnica | Pendiente |
| H-03 | ¿Dimensiones, altura, posición accesible y distancia de público? | Pendiente |
| H-04 | ¿Dispone de entrada de imagen o controlador? Resolución, relación de mapeo, FPS, latencia y cableado | Pendiente |
| H-05 | ¿Es posible mostrar una secuencia de prueba generada por computadora? | Pendiente |
| H-06 | ¿Se permiten proyectores externos y cuál es el nivel de luz ambiente? | Pendiente |
| H-07 | ¿Con qué PC, GPU, controles, sonido y red local se contará? | Pendiente |
| H-08 | ¿El lugar permite cámara, sensores, estación de juego, señalética y operación supervisada? | Pendiente |
| H-09 | ¿Quién operará, limpiará, revisará y reiniciará la estación? | Pendiente |
| H-10 | ¿Qué docentes validarán la representación de los contenidos de LTI? | Pendiente |

**Punto de decisión G0:** no elegir la ruta de integración ni comprometer un calendario de implementación esférica hasta resolver H-01, H-02, H-04 y H-05.

## 4. Rutas técnicas posibles (no confirmadas)

| Ruta | Condición de entrada | Diseño de alto nivel | Riesgo / salida |
|---|---|---|---|
| A. Esfera visual digital | El fabricante confirma pantalla/controlador y permite inyectar video en tiempo real | Unity dibuja el mundo y un adaptador traduce el contenido al formato de la esfera (UV/equirectangular, cubemap u otro, **según especificación real**) | Probar distorsión, orientación, visibilidad, latencia y superficie utilizable |
| B. Proyección esférica | Prueba presencial confirma material adecuado y autorización de montaje | Unity + sistema de calibración de proyección y mapeo geométrico | Una superficie espejada puede hacer esta ruta inviable: **no asumir viabilidad** |
| C. Esfera pasiva + pantalla/proyección auxiliar | La esfera es decorativa o no recibe contenido digital | La esfera sirve como elemento escenográfico/objeto de referencia; el videojuego se muestra en monitor, muro o mesa | Preserva el concepto del museo sin atribuirle capacidades inexistentes a la esfera |
| D. Simulación completamente digital | No hay hardware disponible para pruebas | Mismo gameplay en una esfera virtual, ejecutado en PC y mando | Es el modo base obligatorio y evita bloquear la producción |

**Regla:** desarrollar primero el prototipo para pantalla convencional (D); implementar A o B solamente después de una prueba física aprobada. Mantener C como plan alternativo.

## 5. Conceptos de juego para evaluar con docentes (ninguno seleccionado aún)

### Opción 1 — Red planetaria: paquetes en órbita

- **Proceso LTI:** topología de redes, enrutamiento, disponibilidad y redundancia.
- **Fantasía:** el jugador ve nodos conectados alrededor de un planeta y guía paquetes hacia sus destinos.
- **Acción jugable:** activar enlaces, redirigir tráfico y evitar nodos saturados o caídos.
- **Presión:** llegan más paquetes, fallan enlaces y aparecen prioridades de servicio.
- **Puntos:** entregas correctas y oportunas, continuidad del servicio, eficiencia de rutas y combos.
- **Errores:** paquetes perdidos, rutas imposibles, congestión.
- **Utilidad de la esfera:** el recorrido de la red sobre una superficie continua puede ser espacialmente intuitivo; debe confirmarse que los jugadores puedan leerlo desde su posición.

### Opción 2 — Escudo digital: defensa del sistema

- **Proceso LTI:** monitoreo, priorización, seguridad defensiva y respuesta a incidentes.
- **Fantasía:** conservar operativos servicios distribuidos en una esfera.
- **Acción jugable:** elegir defensas, aislar fallas simuladas y restablecer nodos.
- **Presión:** incidentes simultáneos, recursos limitados y servicios dependientes.
- **Puntos:** servicios disponibles, ataques simulados contenidos y mínima afectación a usuarios legítimos.
- **Cuidado:** diseñar amenazas ficticias; no enseñar procedimientos de ataque real.

### Opción 3 — Pulso IoT: planeta de sensores

- **Proceso LTI:** lectura de sensores, comunicación, calidad de datos y automatización.
- **Fantasía:** mantener estable una red distribuida de estaciones de monitoreo.
- **Acción jugable:** conectar sensores, identificar alertas confiables y corregir fallos.
- **Presión:** lecturas contradictorias, enlaces intermitentes y múltiples incidentes.
- **Puntos:** datos válidos, diagnósticos correctos y continuidad de operación.

**Recomendación preliminar para comparar prototipos:** iniciar el estudio de diseño con *Red planetaria*, porque enlaza una mecánica espacial con un proceso informático reconocible. **No constituye aprobación del juego final**. La selección requiere evaluación lúdica, académica y visual.

## 6. Experiencia de visitante (requisitos de producto propuestos)

- Atracción visual desde lejos; instrucciones legibles desde el punto de control del jugador.
- Una única acción práctica de inicio; una primera partida comprensible sin explicaciones largas.
- Partida individual de 90–180 segundos; reinicio rápido y modo supervivencia opcional según [arquitectura común](00-arquitectura-y-criterios-de-museo.md).
- Controles estándar (mando/teclado) como modalidad base; sensores, celulares o cámaras únicamente como ampliaciones evaluadas.
- Marcador, combo y récord local. Los elementos críticos no deben depender sólo del color.
- Si la esfera está suspendida y fuera de alcance, **no** diseñar interacción por contacto físico con ella.
- Alternativa jugable sentada, sin cámara, sin Internet y con volumen ajustable.
- No almacenar rostros, grabaciones, datos personales ni cuentas de participantes.

## 7. Arquitectura lógica prevista (independiente del hardware)

```text
[Game/ArcadeCore de Unity]
        |
        +-- GameModeLTI (reglas y procesos validados)
        +-- ScoreSystem / ComboSystem / DifficultyDirector
        +-- InputAdapters (mando primero; sensores opcionales)
        +-- SphereSceneRenderer (escenario 3D / orientación)
        +-- DisplayAdapter
               +-- DesktopPreview (obligatorio)
               +-- SphereHardwareAdapter (solo tras G0)
               +-- AuxiliaryScreen (respaldo)
```

No programar un adaptador de hardware hipotético como si tuviera protocolos conocidos. El contrato final del adaptador se documentará tras medir el equipo.

## 8. Plan de validación por compuertas

### G0 — Identificación de la esfera
- Evidencia: autorización, fotos de conectores/controlador, ficha técnica o declaración técnica del responsable.
- Aprobación: se conoce qué dispositivo es y si puede recibir contenido dinámico.
- Si falla: se selecciona la ruta C o D.

### G1 — Prueba visual sin videojuego
- Evidencia: patrones de prueba, cuadrícula, texto, movimiento y calibración documentados.
- Evaluar: visibilidad desde posiciones reales, lectura, brillo, distorsión, orientación y respuesta.
- Aprobación: puede comunicar información crítica del juego sin confundir al público.
- Si falla: la pantalla convencional queda como dispositivo de interacción principal.

### G2 — Prototipo funcional de mecánica
- Evidencia: build de PC con controles, estado de juego, puntaje, derrota y reinicio.
- Aprobación: visitantes de prueba entienden la acción y desean repetir.
- No depende de tener esfera física.

### G3 — Integración piloto supervisada
- Evidencia: sesión con el sistema real, controladores, plan de operación y registro de incidencias.
- Aprobación: estabilidad, seguridad del montaje, accesibilidad, recuperación de fallas y validación docente.

## 9. Alcance para Codex cuando sea autorizado

**Todavía no ejecutar estas tareas.** Cada paquete debe generar un PR independiente y evidencia verificable:

1. Definir una ficha de juego validada: proceso, mecánica, controles, reglas, puntaje, derrota, curva de dificultad y objetos.
2. Crear proyecto Unity y pruebas iniciales sólo si la versión de Unity, sistema operativo y plataforma de despliegue están definidos.
3. Implementar `ArcadeCore` o integrar su versión compartida, sin duplicar sistemas de otros minijuegos.
4. Construir una maqueta gris jugable de 90–180 segundos para monitor y gamepad.
5. Registrar pruebas de comprensión, controles, puntuación, reinicio y funcionamiento offline.
6. Con G0/G1 aprobados, desarrollar adaptador de visualización y calibración específico.
7. Probar modo de respaldo, documentación de instalación, operación, incidencias y rollback.

**Criterio de entrega mínimo para Codex:** código versionado; instrucciones reproducibles de instalación/build; pruebas o checklist ejecutado; capturas/video del prototipo; descripción de limitaciones; PR revisable; cero suposiciones técnicas no verificadas.

## 10. Decisiones y cambios

| Fecha | Decisión | Motivo | Estado |
|---|---|---|---|
| 2026-11-22 | Registrar una estación con esfera para LTI | Solicitud del responsable del proyecto | Acordado como intención |
| 2026-11-22 | Tratar el tipo de esfera y la salida de video como incógnitas | El video no permite demostrar capacidad de pantalla | Pendiente de G0 |
| 2026-11-22 | Evitar programación e integración física anticipadas | Primero se diseñarán todos los proyectos y después Codex ejecutará paquetes acotados | Regla de trabajo |
| 2026-11-22 | Mantener opciones de juego abiertas | Falta validación docente y prueba de jugabilidad | Pendiente |

## 11. Pendientes inmediatos

- Identificar la esfera y obtener autorización y especificaciones.
- Definir si LTI será una estación independiente o se integrará académicamente con Informática e IA, sin reescribir el catálogo vigente.
- Elegir entre las tres mecánicas con el criterio del banco de minijuegos.
- Identificar lugar, público, responsable técnico y equipo disponible.
- Completar [la plantilla de preparación para Codex](10-guia-de-planificacion-y-ejecucion-codex.md).

**Este documento es de planificación, no certifica compatibilidad de la esfera ni autoriza modificar la instalación.**
