# Banco de minijuegos para presentar las carreras de la FCACS Nuevo Laredo

## Enfoque del proyecto

Este espacio no define todavía un videojuego final por carrera. Reúne **procesos reales que pueden transformarse en varios minijuegos arcade**, con la filosofía de Math Party y `ProyectVR`:

- Partidas cortas y fáciles de entender.
- Un personaje, vehículo, herramienta o espacio que se controla directamente.
- Aprendizaje integrado en los obstáculos y reglas, no en preguntas de opción múltiple.
- Puntuación visible durante toda la partida.
- Dificultad que aumenta progresivamente.
- Combos, bonificaciones, riesgo y penalizaciones.
- Récord local que invite a volver a jugar y competir con otros visitantes.
- Modalidades competitivas y cooperativas cuando el proceso lo permita.

## Carreras incluidas

| Carrera | Cantidad inicial de ideas | Procesos aprovechables |
|---|---:|---|
| [Administración](01-administracion.md) | 8 | Operaciones, recursos, inventario, calidad, mercado y estrategia |
| [Autotransporte de Carga](02-autotransporte-de-carga.md) | 8 | Inspección, carga, rutas, flotilla, mantenimiento y cadena de frío |
| [Comercio Exterior](03-comercio-exterior.md) | 1 en desarrollo | Clasificación y manejo de mercancías |
| [Contador Público](04-contador-publico.md) | 8 | Registro, conciliación, costos, flujo, presupuesto, auditoría y control |
| [Derecho](05-derecho.md) | 8 | Evidencia, derechos, mediación, argumentación, privacidad y propiedad intelectual |
| [Psicología](06-psicologia.md) | 8 | Atención, memoria, percepción, cerebro, conducta, entrevista y grupos |
| [Informática e Inteligencia Artificial](07-informatica-e-inteligencia-artificial.md) | 8 | Datos, modelos, programación, redes, nube, ciberseguridad, agentes e IoT |

Comercio Exterior y Aduanas queda fuera del alcance. Su documento anterior se conserva únicamente como referencia histórica en [08-comercio-exterior-y-aduanas.md](08-comercio-exterior-y-aduanas.md).

## Sistema arcade compartido

Los minijuegos pueden reutilizar el mismo núcleo de Math Party y `ProyectVR`.

### Duración sugerida

- Ronda normal: 90 a 180 segundos.
- Modo supervivencia: continúa hasta agotar tiempo, vidas o recursos.
- Tutorial: una acción práctica de 10 a 15 segundos.
- Reinicio completo: menos de 15 segundos.

### Puntuación base

```text
Puntuación = acciones correctas
           + bonificación por combo
           + eficiencia o tiempo restante
           + cooperación
           - errores
           - daños, retrasos o incumplimientos
```

Cada carrera cambia qué significa “correcto”. En Comercio Exterior puede ser clasificar y documentar; en Contaduría, conciliar; en Psicología, completar una secuencia de atención o memoria.

### Dificultad progresiva

La dificultad debe aumentar cada 15 a 30 segundos mediante una combinación de:

- Más objetos, pedidos, enemigos, documentos o incidentes.
- Menos tiempo entre eventos.
- Casos con propiedades parecidas.
- Distractores y datos irrelevantes.
- Dos procesos simultáneos.
- Fallas temporales de una herramienta.
- Decisiones de riesgo: atender un caso difícil por más puntos.
- Eventos especiales o “jefes” al alcanzar determinados puntajes.

No aumentar únicamente la velocidad. La progresión también debe agregar complejidad profesional.

### Combos y multiplicadores

- Tres acciones correctas consecutivas: multiplicador ×2.
- Cinco acciones: bonificación especial de tiempo o recurso.
- Error grave: rompe combo.
- Cooperación sincronizada: combo de equipo.
- Resolver un caso difícil sin pistas: puntuación adicional.

### Récords

- Mejor puntuación del día.
- Mejor puntuación histórica de la estación.
- Récord individual y récord cooperativo.
- Iniciales o nombre corto opcional; no almacenar datos personales.
- Pantalla de “récord a vencer” antes de iniciar.
- Resultado final que muestre cuántos puntos faltaron para entrar al podio.

### Formas de competir

- **Contra el récord:** la opción más simple y adecuada para museo.
- **Simultáneo:** dos jugadores o equipos en el mismo escenario.
- **Por turnos:** mismas condiciones y semilla de eventos.
- **Cooperativo con puntuación grupal:** todos intentan superar el récord de visitantes anteriores.
- **Roles asimétricos:** cada jugador controla una parte distinta del proceso.

## Arquetipos reutilizables

| Arquetipo | Funcionamiento | Carreras donde encaja |
|---|---|---|
| Supervivencia por oleadas | Mantener un sistema funcionando mientras aumenta la presión | Administración, Contaduría, Informática |
| Carrera con obstáculos | Llegar al objetivo tomando decisiones correctas | Comercio Exterior, Autotransporte, Derecho |
| Banda transportadora | Procesar objetos antes de que se acumulen | Comercio Exterior, Contaduría, Informática |
| Arena de clasificación | Llevar elementos a la zona correcta | Todas, con contenidos distintos |
| Defensa de torre | Proteger infraestructura o recursos mediante decisiones | Informática, Administración, Derecho |
| Investigación | Buscar, relacionar y validar evidencia | Derecho, Contaduría, Psicología |
| Ritmo y coordinación | Ejecutar secuencias con precisión | Psicología, Autotransporte, Administración |
| Construcción física | Colocar piezas que alteran el mundo digital | Autotransporte, Comercio Exterior, Informática |

## Plantilla para convertir un proceso en minijuego

Cada idea debe poder responder estas preguntas:

1. **Proceso real:** ¿qué hace el profesionista?
2. **Avatar:** ¿qué controla directamente el jugador?
3. **Objetos:** ¿qué debe transportar, conectar, revisar, proteger o transformar?
4. **Herramientas:** ¿qué opciones tiene y por qué una resulta mejor que otra?
5. **Obstáculos o enemigos:** ¿qué representa el error, retraso, riesgo o interferencia?
6. **Presión creciente:** ¿qué aumenta con el tiempo además de la velocidad?
7. **Puntuación:** ¿qué acción profesional correcta genera puntos?
8. **Derrota:** ¿qué condición comprensible termina la partida?
9. **Récord:** ¿qué hace que un visitante quiera intentarlo otra vez?

Ejemplo de Comercio Exterior: controlar un personaje que descarga y clasifica mercancías. Las pequeñas se cargan a mano, las medianas con diablito y las pesadas con montacargas. Elegir mal la herramienta cuesta tiempo o daña el objeto. Conforme avanza la partida aparecen nuevas categorías, obstáculos, enemigos y mercancías con mayor frecuencia. Si se llena la zona de recepción, termina la ronda. Cada clasificación otorga puntos según su dificultad y conserva o rompe el combo.

## Uso de tecnologías emergentes

La tecnología debe mejorar una mecánica, no ser decoración.

| Tecnología | Ejemplo útil |
|---|---|
| VR | Inspeccionar un vehículo, recorrer el cerebro o entrar a una red |
| AR | Revelar evidencia, información de mercancías o componentes |
| Visión artificial | Reconocer props, manos, postura u objetos clasificados |
| Multijugador local | Dividir roles y generar récords grupales |
| Electrónica | Botones, básculas, lectores RFID, luces, servos y sensores |
| Impresión 3D | Mercancías, cerebro modular, camiones, documentos y fichas |
| Proyección interactiva | Convertir una mesa o muro en escenario jugable |
| IA local | Clasificación, predicción, agentes y explicación de errores |

La arquitectura común está descrita en [00-arquitectura-y-criterios-de-museo.md](00-arquitectura-y-criterios-de-museo.md).

## Cómo usar este banco de ideas

1. Docentes señalan los procesos que mejor representan su carrera.
2. Se eligen dos o tres ideas por carrera, sin comprometerse todavía con una.
3. Se prueban en papel, cartón o una escena gris de Unity.
4. Se observa cuál se entiende sin explicación extensa.
5. Se selecciona el concepto que produce más ganas de repetir y competir.
6. Sólo entonces se define hardware, arte y alcance final.

## Primer criterio de selección

Una idea merece prototipo si cumple al menos cinco condiciones:

- La acción del jugador representa realmente un proceso de la carrera.
- Se entiende visualmente en menos de 20 segundos.
- Permite puntuación y dificultad progresiva.
- Tiene decisiones, no sólo reflejos.
- Puede jugarse de nuevo con casos diferentes.
- Funciona sin Internet.
- Posee un modo de respaldo si falla cámara, sensor o visor.
- Puede terminarse con el tiempo y equipo disponibles.

## Planificación nueva: estación LIIA y ejecución futura con Codex

- [Estación LIIA con esfera — observaciones del video, factibilidad, propuestas de juego y compuertas de validación](09-liia-esfera-interactiva.md).
- [Protocolo para definir, evaluar y documentar proyectos antes de entregar tareas a Codex](10-guia-de-planificacion-y-ejecucion-codex.md).

La estación esférica de **LIIA (Informática e Inteligencia Artificial)** tiene como videojuego seleccionado **[Laberinto gravitatorio esférico](15-liia-laberinto-gravitatorio-esferico.md)**: bolita por pistas exteriores, giro mediante cuatro botones electrónicos y estrellas de puntuación. Es una demostración lúdica de programación, electrónica y simulación, **no un tema curricular directo**; no constituye carrera adicional. Sigue sin demostrarse que la esfera real sea una pantalla. La esfera del video aparenta ser reflectante; **su capacidad para reproducir contenido digital no está comprobada**. Antes de desarrollar una integración, deberá identificarse el equipo y aprobarse una prueba técnica. Las fichas y tareas de Codex son planificación, no software implementado.
