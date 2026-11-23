# Plan maestro por fases — Comercio Exterior: Clasificador en caos VR

## 1. Propósito

Construir una experiencia educativa de Comercio Exterior en VR donde el jugador inspecciona mercancías, elige una de exactamente tres categorías, transporta la carga por un almacén y conoce el resultado únicamente al depositarla. La presión proviene de la acumulación progresiva de mercancías y la partida termina cuando se satura la recepción.

Este documento organiza el proyecto desde su definición hasta su publicación y mantenimiento. La fuente de diseño principal es [Clasificador en caos VR](../../docs/disenos/12-comercio-exterior-clasificador-en-caos-vr.md).

## 2. Reglas que no deben cambiarse durante el MVP

1. La experiencia principal es VR en primera persona.
2. La locomoción es continua con joystick; el teletransporte no es el control principal.
3. Cada mercancía presenta exactamente tres opciones.
4. Elegir una opción no revela si es correcta y no otorga puntos.
5. La categoría elegida y el destino físico sólo se evalúan al depositar.
6. La derrota principal ocurre por saturación de la recepción, no por un cronómetro individual.
7. Los enemigos y obstáculos afectan la integridad de la carga; el combate es secundario.
8. Las categorías son didácticas y de alto nivel; no se muestran fracciones arancelarias reales ni asesoría aduanera.
9. Los destinos se distinguen por nombre, icono, forma y color; nunca sólo por color.
10. El récord se mide por mayor puntuación acumulada.

## 3. Alcance objetivo

### MVP jugable

- Un almacén 3D en escala VR.
- Movimiento continuo, giro configurable y pausa.
- Manos/controladores capaces de agarrar, soltar, acercar y rotar mercancía.
- Tres depósitos claramente diferenciados.
- Entre 6 y 8 mercancías educativas configurables.
- Tres opciones plausibles por mercancía.
- Selección sin feedback y validación diferida al entregar.
- Recepción con cola visible, llegadas progresivas y derrota por saturación.
- Integridad de carga, un enemigo básico y una defensa de repulsión.
- Puntos, combo, oleada, récord local, pausa, reinicio y audio silenciable.
- Tutorial progresivo y explicaciones educativas breves después de cada entrega.
- Build principal para Windows PC-VR con salida espejo para espectadores.

### Fuera del MVP

- Multijugador, modo cooperativo o versus.
- Montacargas y diablito con físicas complejas.
- RFID, ArUco, cámara cenital, props físicos o videomapping.
- Reconocimiento visual o de voz.
- Fracciones arancelarias reales.
- Enemigos con combate avanzado.
- Publicación en una tienda de VR.

## 4. Base técnica propuesta

- Motor: Unity 6000.6.10f1 para mantener compatibilidad con el entorno ya instalado.
- Render: URP 3D, iluminación horneada cuando sea conveniente y materiales simples.
- XR: OpenXR y XR Interaction Toolkit.
- Entrada: Input System mediante acciones abstractas, sin depender de una marca concreta de visor.
- Plataforma primaria: Windows x64 PC-VR.
- Persistencia: `PlayerPrefs` o archivo JSON local para récord, volumen y opciones de confort.
- Datos educativos: `ScriptableObject` para mercancías, opciones, categoría correcta, explicación e iconografía.
- Pruebas sin visor: XR Device Simulator únicamente como herramienta de desarrollo; no cuenta como validación VR.

### Estructura sugerida

```text
Assets/_Project/
  Art/
  Audio/
  Data/Merchandise/
  Materials/
  Prefabs/
    Cargo/
    Deposits/
    Enemies/
    UI/
  Scenes/
    Bootstrap.unity
    Comercio.unity
  Scripts/
    Core/
    Cargo/
    Classification/
    Enemies/
    Input/
    UI/
    XR/
  Settings/
  Tests/
    EditMode/
    PlayMode/
```

### Estados principales

```text
Inicio → Tutorial → Jugando → Pausa → Derrota → Resultados → Reinicio
```

La pausa y cada modal del tutorial deben congelar generación, IA, integridad, enfriamientos y reloj de oleada.

## 5. Planeación por fases

## Fase 0 — Cierre de decisiones y preparación

**Objetivo:** eliminar decisiones que podrían obligar a rehacer controles, escala o contenido.

### Trabajo

- Confirmar visor, runtime y mandos disponibles.
- Confirmar si el destino es PC-VR por cable/inalámbrico o visor autónomo.
- Medir el espacio de exhibición y definir si se jugará de pie, sentado o ambos.
- Cerrar el mapa de controles inicial:
  - stick izquierdo: movimiento;
  - stick derecho: giro por saltos o giro suave;
  - grip: agarrar/soltar;
  - trigger o puntero: seleccionar opción;
  - botón de acción: repulsión;
  - menú: pausa.
- Elegir tres categorías didácticas iniciales.
- Elegir de 6 a 8 mercancías y redactar tres opciones más una explicación breve por objeto.
- Definir duración objetivo: 3 a 5 minutos para una partida de exhibición.
- Establecer valores provisionales de cola, aparición, daño, repulsión y puntos.

### Entregables

- Ficha de hardware y runtime.
- Tabla de controles.
- Catálogo educativo v0.1.
- Hoja de balance v0.1.
- Lista de riesgos de comodidad y seguridad.

### Criterio de salida

No quedan decisiones abiertas sobre visor, plataforma, controles ni categorías del primer prototipo.

---

## Fase 1 — Proyecto base y arquitectura

**Objetivo:** disponer de un proyecto Unity reproducible, versionado y capaz de entrar en Play sin errores.

### Trabajo

- Crear el proyecto URP dentro de esta carpeta o en una subcarpeta `UnityProject` acordada.
- Configurar OpenXR, XR Interaction Toolkit e Input System mediante Package Manager.
- Crear `Bootstrap.unity` y `Comercio.unity`.
- Configurar capas, etiquetas, colisiones y acciones de entrada.
- Preparar `GameSession`, estados de partida y servicios de puntuación/configuración.
- Añadir `.gitignore`, Git LFS para binarios y convención de prefabs/datos.
- Crear un menú técnico mínimo para iniciar, pausar, reiniciar y salir.

### Entregables

- Proyecto abre y compila con cero errores.
- Escena VR muestra suelo, cámara XR y controladores.
- Primer commit restaurable de la base técnica.
- README con versión de Unity y procedimiento de apertura.

### Criterio de salida

El visor entra al almacén vacío, la escala se percibe correcta y todas las acciones de entrada se detectan.

---

## Fase 2 — Graybox, locomoción y comodidad

**Objetivo:** validar que recorrer el almacén sea legible y cómodo antes de construir mecánicas encima.

### Trabajo

- Construir recepción, tres rutas y tres depósitos con primitivas.
- Implementar locomoción continua y límites físicos del escenario.
- Añadir giro por saltos configurable; evaluar giro suave como opción.
- Configurar aceleración moderada, viñeta de confort opcional y juego sentado/de pie.
- Evitar escaleras, saltos, cambios forzados de altura y movimiento automático de cámara.
- Crear salida espejo estable para el monitor.
- Medir rendimiento base y fijar presupuesto de geometría, luces y sombras.

### Entregables

- Almacén graybox completamente recorrible.
- Menú de comodidad mínimo.
- Registro de prueba de 10 minutos con visor real.

### Criterio de salida

El jugador alcanza los tres depósitos sin atravesar geometría, perder orientación o sufrir caídas severas de rendimiento.

---

## Fase 3 — Vertical slice del bucle principal

**Objetivo:** demostrar el ciclo educativo completo con una sola mercancía.

### Trabajo

- Crear una mercancía de prueba agarrable y rotatable.
- Mostrar exactamente tres opciones al sostenerla.
- Permitir seleccionar y corregir una opción antes de entregar.
- Mostrar estado “selección registrada / entrega pendiente” sin revelar acierto.
- Crear tres zonas de depósito con nombre, icono y silueta.
- Validar categoría seleccionada y depósito únicamente al soltar dentro de una zona válida.
- Mostrar resultado, explicación corta y puntuación después de la entrega.
- Garantizar que cada mercancía sólo pueda evaluarse una vez.
- Añadir reinicio del ciclo.

### Entregables

- Una mercancía completa de recepción a resultado.
- UI diegética legible en VR.
- Pruebas de selección, cambio de selección, depósito correcto, depósito incorrecto y doble depósito.

### Criterio de salida

Una persona nueva puede inspeccionar, elegir, transportar y entregar sin que el juego revele la respuesta antes de tiempo.

**Hito:** vertical slice aprobada.

---

## Fase 4 — Sistema de contenido educativo

**Objetivo:** convertir la vertical slice en un sistema extensible sin duplicar lógica por mercancía.

### Trabajo

- Crear `MerchandiseDefinition` como `ScriptableObject`.
- Guardar nombre, material, función, presentación, dificultad, icono, prefab, tres opciones, respuesta y explicación.
- Incorporar de 6 a 8 mercancías con distractores plausibles.
- Crear validación automática que detecte objetos sin exactamente tres opciones o sin explicación.
- Asegurar que color, tamaño o sonido no revelen la respuesta.
- Separar contenido académico de reglas de puntuación.
- Redactar textos cortos, legibles y consistentes.

### Entregables

- Catálogo de mercancías v1.
- Herramienta o validador de datos.
- Matriz mercancía → pistas → opciones → explicación.

### Criterio de salida

Agregar una mercancía nueva requiere crear datos y prefab, sin modificar el evaluador central.

---

## Fase 5 — Recepción, cola, oleadas y derrota

**Objetivo:** introducir la presión principal y completar una partida con inicio y final.

### Trabajo

- Implementar generador de mercancías con puntos de recepción válidos.
- Mantener una cola visual y un conteo inequívoco de ocupación.
- Impedir apariciones superpuestas o inaccesibles.
- Acelerar gradualmente las llegadas por oleada.
- Escalar variedad y ambigüedad, no sólo velocidad.
- Terminar la partida cuando la cola alcance el máximo configurable.
- Implementar puntuación, combo, mejor récord local y estadísticas finales.
- Crear pantalla de inicio, derrota y reintento.

### Valores iniciales para balance

- Capacidad de recepción: 5 mercancías.
- Intervalo inicial: 20 segundos.
- Reducción por oleada: 1.5 segundos hasta un mínimo de 7 segundos.
- Oleada: 45 segundos o un número configurable de entregas.

### Entregables

- Partida completa de inicio a derrota y reinicio.
- Curva de dificultad editable.
- HUD con puntos, combo, oleada, cola y récord.

### Criterio de salida

La única derrota normal del MVP es la saturación; no existen cronómetros ocultos por mercancía.

**Hito:** MVP de reglas completo.

---

## Fase 6 — Integridad, obstáculos y enemigo básico

**Objetivo:** hacer significativo el transporte sin convertir el juego en combate.

### Trabajo

- Asignar 100 % de integridad inicial a cada carga.
- Mostrar integridad sólo mientras la carga relevante está en tránsito.
- Crear un robot/carrito con patrulla, advertencia y colisión predecible.
- Reducir integridad al golpear la mercancía, sin dañar directamente al jugador.
- Implementar repulsión de corto alcance con enfriamiento visible.
- Permitir esquivar y rodear obstáculos.
- Calcular puntos correctos por integridad conservada.
- Evitar daño repetido por una sola colisión y enemigos atascados.

### Entregables

- Un tipo de enemigo funcional.
- Un obstáculo estático con ruta alternativa.
- Feedback de daño, integridad y repulsión.
- Pruebas de navegación y recuperación de atascos.

### Criterio de salida

El jugador entiende cuándo la mercancía fue dañada y por qué recibió menos puntos, sin sacudidas forzadas de cámara.

---

## Fase 7 — Tutorial, interfaz, audio y feedback

**Objetivo:** lograr que una persona pueda jugar sin explicación verbal extensa.

### Trabajo

- Crear introducción breve con objetivo y botón claro para comenzar.
- Tutorial progresivo que se activa antes de exigir cada acción:
  1. locomoción y giro;
  2. agarrar, acercar y rotar;
  3. leer pistas y elegir una de tres opciones;
  4. transportar y depositar;
  5. cola y saturación;
  6. integridad, enemigo y repulsión.
- Pausar completamente la simulación durante tarjetas de tutorial.
- Diseñar HUD VR con información mínima y panel de espectador más completo.
- Añadir sonidos de agarre, selección, entrega, acierto, error, daño, repulsión, oleada y saturación.
- Mezclar música y efectos para que las señales de gameplay tengan prioridad.
- Incorporar mute y volumen persistente.
- Añadir números flotantes y feedback de combo sin bloquear la visión.

### Entregables

- Tutorial completo reiniciable.
- HUD VR y HUD espejo sin superposiciones.
- Mezcla de audio básica.
- Guía de controles dentro del juego.

### Criterio de salida

Al menos 4 de 5 personas de prueba completan su primera entrega sin ayuda externa.

---

## Fase 8 — Progresión, balance y claridad educativa

**Objetivo:** convertir los sistemas en una experiencia breve, comprensible y rejugable.

### Trabajo

- Diseñar las primeras cinco oleadas y modo supervivencia.
- Escalar frecuencia, dificultad de opciones, rutas y presencia enemiga por separado.
- Ajustar puntos base, combo, daño, enfriamiento y capacidad de cola.
- Evitar situaciones imposibles o derrotas inevitables.
- Medir tiempo de decisión, tasa de aciertos, mercancías dañadas y duración de partida sin guardar datos personales.
- Revisar que las explicaciones enseñen material, función y presentación.
- Corregir términos que puedan confundirse con una resolución jurídica o aduanera real.

### Entregables

- Tabla de balance v1.
- Secuencia de oleadas v1.
- Informe de sesiones de prueba.
- Catálogo educativo v2.

### Criterio de salida

La partida media dura entre 3 y 5 minutos, la dificultad sube de forma perceptible y los errores se pueden explicar por decisiones observables.

**Hito:** alpha cerrada.

---

## Fase 9 — Dirección visual, accesibilidad y rendimiento

**Objetivo:** alcanzar calidad de exhibición sin comprometer comodidad ni legibilidad.

### Trabajo

- Sustituir graybox por un almacén estilizado de bajo costo gráfico.
- Definir paleta, iluminación, señalética y jerarquía visual.
- Diferenciar mercancías, depósitos, amenazas y rutas por forma e iconografía.
- Añadir contornos, luces o anillos sólo donde aporten lectura.
- Incorporar tamaño de texto configurable, subtítulos y símbolos redundantes al color.
- Mantener contraste y distancias de lectura apropiadas para VR.
- Optimizar luces, sombras, materiales, físicas, garbage collection y generación de objetos.
- Probar frecuencia de imagen estable en el visor objetivo.

### Entregables

- Art bible breve.
- Escenario de exhibición.
- Perfil gráfico y reporte de rendimiento.
- Checklist de accesibilidad y comodidad.

### Criterio de salida

No hay textos ilegibles, materiales rosas, caídas frecuentes de cuadros ni señalización dependiente únicamente del color.

---

## Fase 10 — QA funcional y de VR

**Objetivo:** encontrar errores antes de empaquetar la versión candidata.

### Matriz mínima

- Inicio, pausa, reanudación, derrota y reinicio repetidos.
- Agarre con ambas manos/controladores cuando aplique.
- Mercancía soltada fuera del mapa, dentro de paredes o entre depósitos.
- Cambio de selección antes de entregar.
- Entrega correcta, categoría incorrecta y destino incorrecto.
- Doble evaluación y duplicación de puntos.
- Cola llena mientras hay un tutorial o pausa.
- Enemigo bloqueado, fuera de ruta o golpeando varias veces por cuadro.
- Pérdida temporal de tracking/controlador.
- Cambio sentado/de pie y recentrado.
- Volumen, mute, récord y opciones después de reiniciar la aplicación.
- Resoluciones de salida espejo 1920×1080 y 1280×720.
- Sesión continua de 30 minutos sin degradación progresiva.

### Automatización recomendada

- EditMode: evaluación de respuestas, puntuación, integridad y curva de oleadas.
- PlayMode: aparición, saturación, entrega única y transición de estados.
- Validación de contenido: exactamente tres opciones por mercancía.

### Entregables

- Matriz de QA con evidencia y resultado.
- Registro de defectos por severidad.
- Cero errores de compilación y cero defectos bloqueantes.

### Criterio de salida

Todos los P0 y P1 están cerrados; los pendientes menores están documentados y no rompen el recorrido principal.

**Hito:** release candidate.

---

## Fase 11 — Builds, operación y documentación

**Objetivo:** entregar una versión instalable y operable por otra persona.

### Trabajo

- Generar build Windows x64 PC-VR con OpenXR.
- Verificar arranque con el runtime y visor objetivo desconectado/conectado.
- Configurar pantalla completa o ventana espejo según exhibición.
- Crear paquete ZIP, manifiesto de versión y hash SHA-256.
- Documentar instalación, conexión del visor, calibración, controles y solución de fallos comunes.
- Documentar higiene, espacio seguro, recentrado, pausa de emergencia y cierre correcto.
- Preparar modo atracción o pantalla inicial para periodos sin jugador.
- Ejecutar una prueba de montaje completa en el equipo de exhibición.

### Entregables

- Build candidata para Windows PC-VR.
- README de instalación y operación.
- Checklist de apertura/cierre de exhibición.
- Manifiesto de archivos y versión.

### Criterio de salida

Una persona distinta al desarrollador puede instalar, iniciar, calibrar, jugar, reiniciar y cerrar la experiencia siguiendo únicamente la documentación.

---

## Fase 12 — Publicación, cierre y mantenimiento

**Objetivo:** integrar una versión estable sin perder trazabilidad y preparar el siguiente ciclo.

### Trabajo

- Congelar contenido y balance de la versión 1.0.
- Crear PR desde `codex/comercio-mvp` hacia `main`.
- Revisar código, documentación, licencias de assets y archivos LFS.
- Etiquetar la versión y conservar hashes de los builds.
- Ejecutar prueba de humo desde un clon limpio.
- Documentar limitaciones conocidas y backlog posterior al MVP.
- Definir responsable, frecuencia de respaldo y procedimiento de actualización.

### Entregables

- PR de versión estable.
- Tag/release 1.0.
- Notas de versión.
- Backlog de mejoras.

### Criterio de salida

La versión publicada coincide con el commit etiquetado, el build puede reproducirse y no quedan archivos de caché o datos personales en el repositorio.

**Hito:** proyecto listo para exhibición.

## 6. Dependencias entre fases

```text
F0 decisiones
 └─ F1 base técnica
     └─ F2 locomoción y graybox
         └─ F3 vertical slice
             ├─ F4 contenido
             └─ F5 cola y oleadas
                 └─ F6 integridad y enemigos
                     └─ F7 tutorial y feedback
                         └─ F8 balance
                             └─ F9 arte y rendimiento
                                 └─ F10 QA
                                     └─ F11 builds
                                         └─ F12 publicación
```

F4 puede avanzar en paralelo con F5 después de aprobar la vertical slice. El arte final no debe comenzar antes de validar la escala, locomoción y bucle principal.

## 7. Calendario de referencia

Estimación para una persona con acceso estable al visor:

| Bloque | Fases | Duración estimada |
|---|---|---:|
| Preproducción | 0 | 1–2 días |
| Base técnica y VR | 1–2 | 3–5 días |
| Vertical slice | 3 | 3–4 días |
| MVP de sistemas | 4–6 | 6–9 días |
| Experiencia completa | 7–9 | 6–10 días |
| QA y entrega | 10–12 | 4–6 días |
| **Total orientativo** | 0–12 | **23–36 días laborables** |

La estimación no incluye espera por hardware, compra de assets ni integraciones físicas posteriores.

## 8. Definición de terminado del proyecto

El proyecto se considera listo cuando:

- [ ] El bucle inspeccionar → elegir → transportar → entregar → aprender funciona con todas las mercancías.
- [ ] Cada mercancía tiene exactamente tres opciones y no revela la respuesta antes de la entrega.
- [ ] La cola progresa y la saturación termina la partida de forma clara.
- [ ] Integridad, enemigo y repulsión funcionan sin bloqueos.
- [ ] Tutorial, pausa y modales detienen toda la simulación pertinente.
- [ ] Hay puntuación, combo, récord, resultados y reinicio.
- [ ] El contenido explica material, función y presentación sin simular asesoría aduanera real.
- [ ] El control VR, recentrado y opciones de comodidad están probados en el visor objetivo.
- [ ] La frecuencia de imagen es estable y no existen errores bloqueantes.
- [ ] Audio, mute, textos, iconos y señalización son legibles en VR y en la salida espejo.
- [ ] El build se instala y ejecuta en el equipo de exhibición desde un paquete limpio.
- [ ] README, QA, créditos, operación y limitaciones están documentados.
- [ ] El código, los datos y los builds están versionados en la rama correcta y listos para PR.

## 9. Backlog posterior al MVP

1. Más mercancías, categorías y rutas.
2. Montacargas y diablito con físicas diferenciadas.
3. Enemigos adicionales con comportamientos legibles.
4. Modo cooperativo.
5. Modo de demostración en pantalla sin visor.
6. Props físicos y lectura RFID/ArUco.
7. Cámara cenital y proyección sobre mesa.
8. Clasificador visual local como demostración complementaria.
9. Estadísticas anónimas de exhibición.
10. Port a visor autónomo, sólo después de medir alcance y rendimiento.

## 10. Primer trabajo recomendado

Comenzar únicamente con la **Fase 0**. Una vez cerrados visor, plataforma, controles y catálogo inicial, crear el proyecto base de la Fase 1 y no avanzar al arte hasta que la vertical slice de la Fase 3 esté aprobada.

