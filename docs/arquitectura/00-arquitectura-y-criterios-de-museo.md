# Arquitectura arcade compartida y criterios para museo

## Objetivo

Crear un núcleo reutilizable para que todas las carreras puedan tener minijuegos con la misma sensación de Math Party y `ProyectVR`: entrar rápido, entender una acción, acumular puntos, sobrevivir a una dificultad creciente y tratar de romper un récord.

Las ideas de cada carrera son intercambiables. Esta arquitectura no obliga a escoger todavía cuál se desarrollará.

## Stack recomendado

- **Unity + C#:** juego, física, multijugador, VR, AR y proyección.
- **Unity Input System:** controles, teclado, botones físicos y unión dinámica de jugadores.
- **Universal Render Pipeline:** calidad visual estable en equipos de museo.
- **OpenCV:** cámara, ArUco/ChArUco y objetos físicos.
- **MediaPipe:** manos, pose y movimiento corporal.
- **Unity Sentis:** inferencia local de modelos cuando sea necesaria.
- **ESP32/Arduino + PlatformIO:** lectores, luces, básculas, botones y servos.
- **SQLite o JSON local:** récords y métricas anónimas.

No se necesita backend para la primera exhibición. Si una estación requiere Python para visión, puede usarse FastAPI local; Docker sólo aporta valor en desarrollo o cuando existan varios servicios. El ejecutable del museo debe iniciar y recuperarse con la menor cantidad de dependencias posible.

## Núcleo común

```text
ArcadeCore
├── GameFlow
│   ├── Atracción
│   ├── Unirse
│   ├── Tutorial
│   ├── Partida
│   ├── Resultado
│   └── Reinicio
├── ScoreSystem
├── ComboSystem
├── DifficultyDirector
├── TimerAndLives
├── LeaderboardLocal
├── PlayerJoin
├── InputAdapters
│   ├── Controles
│   ├── Cámara
│   ├── Serial / ESP32
│   └── XR
└── ContentData
    ├── Procesos
    ├── Casos
    ├── Oleadas
    └── Eventos
```

Cada minijuego implementa acciones propias, pero informa al núcleo de aciertos, errores, combos y nivel de dificultad.

## Sistema de puntuación

### Eventos estándar

| Evento | Ejemplo | Puntos orientativos |
|---|---|---:|
| Acción correcta simple | Clasificar, registrar o conectar | +10 |
| Acción correcta difícil | Caso ambiguo o doble condición | +25 |
| Acción sincronizada | Dos jugadores completan un proceso | +20 |
| Cadena de cinco aciertos | Combo | +50 |
| Uso eficiente de recursos | Ruta, tiempo, combustible o presupuesto | +5 a +30 |
| Error recuperable | Documento o zona incorrecta | -10 |
| Error crítico | Incumplimiento, daño o pérdida | -25 y rompe combo |
| Superar oleada | Mantener el sistema estable | +100 |

Los valores deben configurarse por datos. La precisión debe pesar más que pulsar rápido.

### Combos

- Multiplicador progresivo: ×1, ×1.5, ×2 y ×3.
- El combo aumenta por decisiones correctas consecutivas.
- Un error leve reduce un nivel; uno crítico reinicia.
- En cooperación, ciertas acciones sólo cuentan si ambos roles se coordinan.
- Efectos audiovisuales muestran el combo sin llenar la pantalla de UI.

### Récords

- Podio diario y podio histórico.
- Récord por número de jugadores para no comparar injustamente.
- Récord de precisión y récord de puntuación pueden coexistir.
- Iniciales opcionales de tres caracteres.
- Reinicio administrativo protegido.
- Si se usan varias estaciones, un “pasaporte” anónimo puede sumar puntos por carrera, pero no es necesario para el MVP.

## Director de dificultad progresiva

La dificultad debe responder al tiempo y al desempeño.

### Variables que puede modificar

- Frecuencia de aparición.
- Cantidad de elementos simultáneos.
- Tiempo disponible.
- Similitud entre opciones.
- Número de reglas activas.
- Distractores.
- Incidentes y fallas.
- Velocidad física.
- Valor de riesgo y recompensa.

### Ejemplo de oleadas

| Tiempo | Nivel | Cambio |
|---|---:|---|
| 0–30 s | 1 | Un solo proceso y ejemplos claros |
| 30–60 s | 2 | Más velocidad o volumen |
| 60–90 s | 3 | Distractores y una segunda regla |
| 90–120 s | 4 | Dos tareas simultáneas |
| 120–150 s | 5 | Evento especial o “jefe” |
| 150 s en adelante | Infinito | Escalado hasta que el equipo falle |

### Ajuste dinámico opcional

- Tres errores seguidos: mantener dificultad o mostrar pista.
- Diez aciertos seguidos: adelantar siguiente oleada.
- Jugador experto: ofrecer objetivo de riesgo por más puntos.
- Nunca ocultar reglas o cambiar la respuesta correcta arbitrariamente.

## Modos reutilizables

### Récord individual

Una persona juega contra el podio. Es el modo más simple para flujo continuo de museo.

### Versus local

Dos jugadores reciben condiciones equivalentes. Se recomienda compartir la misma semilla de eventos.

### Cooperativo puntuado

Cada jugador tiene un rol y todos comparten puntuación. La coordinación produce bonificaciones.

### Relevo

Jugadores resuelven etapas consecutivas de un proceso. Permite grupos grandes sin llenar el escenario de personajes.

### Supervivencia

El sistema genera presión creciente hasta que se agota tiempo, vidas, confianza, presupuesto o integridad.

## Tecnología por nivel de riesgo

### Nivel A: prototipo robusto

- Pantalla, PC y controles.
- Todo el proceso es virtual.
- Ideal para probar si la idea es divertida y comprensible.

### Nivel B: interacción física

- Cámara fija, ArUco/RFID, props impresos y ESP32.
- El objeto físico altera el mundo digital.
- Debe conservar control convencional como respaldo.

### Nivel C: inmersión supervisada

- VR, AR o seguimiento corporal.
- Requiere área delimitada, mediador, higiene y pantalla espejo.
- Conviene usarlo en una o dos estaciones, no en todas.

## Reglas de museo

- Partidas de 2 a 3 minutos; supervivencia máxima de 5 minutos.
- El récord debe verse antes y después de jugar.
- El tutorial enseña haciendo, no leyendo.
- Reinicio automático tras inactividad.
- Sin Internet, cuentas o datos personales.
- Texto grande, subtítulos, alto contraste y señales que no dependan sólo de color.
- Alternativa sentada y alternativa sin cámara/VR.
- Cámara y micrófono procesados localmente; no guardar grabaciones.
- Contenido validado por docentes y marcado como simulación simplificada.

## Reutilización entre carreras

| Sistema | Puede reutilizarse en |
|---|---|
| Banda transportadora | Comercio Exterior, Contaduría, Informática |
| Mapa y rutas | Autotransporte, Comercio Exterior, Administración |
| Objetos perseguidores | Psicología, Informática, Administración |
| Evidencia y conexiones | Derecho, Contaduría, Psicología |
| Clasificación física | Comercio Exterior, Informática, Contaduría |
| Roles asimétricos | Todas |
| Oleadas e incidentes | Todas |

## Tiempos orientativos

| Trabajo | Tiempo aproximado para una persona con experiencia en Unity |
|---|---|
| Núcleo de puntuación, combos, oleadas y récords | 1 a 2 semanas |
| Prototipo de una idea con controles | 3 a 7 días |
| Convertir prototipo en piloto de museo | 1 a 2 semanas adicionales |
| Integrar cámara o electrónica | 1 a 2 semanas adicionales |
| Integrar AR/VR y endurecer operación | 2 a 4 semanas adicionales |

La mejor estrategia es crear el núcleo común, probar varias ideas en gris y escoger después. Desarrollar una propuesta completa por carrera desde el inicio sería más lento y reduciría la oportunidad de descubrir cuál es realmente divertida.

## Referencias técnicas

- [Unity XR](https://docs.unity3d.com/6000.1/Documentation/Manual/XR.html)
- [Unity AR Foundation](https://docs.unity3d.com/6000.0/Documentation/Manual/AROverview.html)
- [Unity XR Interaction Toolkit](https://docs.unity3d.com/6000.0/Documentation/Manual/com.unity.xr.interaction.toolkit.html)
- [Unity Sentis](https://docs.unity3d.com/6000.0/Documentation/Manual/com.unity.ai.inference.html)
- [MediaPipe Vision](https://ai.google.dev/edge/api/mediapipe/python/mp/tasks/vision)
- [OpenCV ArUco](https://docs.opencv.org/4.11.0/d1/dcb/tutorial_aruco_faq.html)
- [PlatformIO con Arduino y ESP32](https://docs.platformio.org/en/stable/frameworks/arduino.html)
