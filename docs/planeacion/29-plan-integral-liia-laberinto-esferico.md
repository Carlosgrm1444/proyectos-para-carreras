# Plan integral de desarrollo — LIIA: Laberinto gravitatorio esférico

> **Versión:** 1.0  
> **Propósito:** llevar el prototipo actual hasta una estación museográfica jugable, verificable y mantenible.  
> **Alcance de esta versión:** monitor + teclado, después ESP32 por USB y, sólo tras una prueba técnica, integración con la esfera física.  
> **Supuesto de estimación:** una persona de desarrollo Unity, con apoyo puntual de diseño/nivel y electrónica. Duración objetivo: **8 a 10 semanas** de trabajo enfocado.

## 1. Resultado que se quiere entregar

Una estación para presentar la LIIA en la que una bola rueda por un laberinto construido sobre la superficie de un planeta. El visitante usa cuatro direcciones; debe alcanzar la meta, puede tomar rutas con estrellas y compite por puntuación y tiempo.

La sensación será física, pero la simulación será **restringida a la superficie**, no una física libre. Esto es intencional: la bola tiene aceleración, inercia, fricción, choque y giro visual, pero nunca se desprende del planeta por un error numérico ni atraviesa el mapa.

La cámara siempre mira el área local de juego desde arriba; la zona donde está la bola se percibe como piso. El planeta no necesita girar físicamente para que el control sea legible.

## 2. Decisiones técnicas ya tomadas

| Tema | Decisión actual | Motivo |
|---|---|---|
| Motor | Unity 2022.3.62f3 LTS | Instalado y validado en el equipo. |
| Movimiento | Cinemática con velocidad, aceleración, fricción y colisión por mapa | Estable en cualquier zona de la esfera; evita caídas no deseadas. |
| Mapa | PNG de celdas + generación de mesh curvo extruido | Permite diseñar niveles visualmente y reconstruirlos de forma repetible. |
| Colisión | Validación de celdas transitables; `MeshCollider` como geometría estática de apoyo | La regla del juego y la apariencia salen de la misma fuente. |
| Cámara | Seguimiento cenital local sobre un arco exterior | Sin atravesar el planeta ni giros de cámara bruscos. |
| Entrada inicial | Teclado (flechas/WASD) | Permite iterar sin depender de hardware. |
| Entrada de exposición | ESP32 por USB serial/HID con cuatro botones | Control tangible y fácil de diagnosticar. |
| Visualización | Monitor junto a la esfera hasta validar una alternativa | No se asume que la esfera física sea una pantalla o admita proyección. |

## 3. Arquitectura objetivo

```text
PNG de nivel ──→ lector de mapa ──→ mesh curvo + muros + marcadores
       │                 │
       │                 └──→ reglas de celdas, rutas y colisiones
       │
Teclado / ESP32 ──→ adaptador de entrada ──→ controlador de bola
                                              │
                                              ├── velocidad, fricción, choques y giro visual
                                              └── posición anclada a la superficie esférica

Controlador de bola ──→ cámara local ──→ HUD / audio / puntuación / récord
```

### Contrato del PNG de un nivel

Cada nivel tendrá un PNG pequeño, sin antialiasing y con píxeles sólidos. La primera versión usará una banda de la esfera, no los polos, para que las celdas no se deformen demasiado.

| Color / símbolo | Significado |
|---|---|
| Negro u oscuro | Muro / zona no transitable |
| Blanco o claro | Piso / ruta transitable |
| Verde | Inicio |
| Rojo | Meta |
| Amarillo (fase posterior) | Estrella |
| Azul (fase posterior) | Checkpoint |
| Morado (fase posterior) | Trampa o zona especial |

El generador debe rechazar un mapa inválido: sin inicio, sin meta, sin ruta entre ambos o con colores no reconocidos.

## 4. Fases, entregables y criterios de salida

### Fase 0 — Dirección y preparación (2–3 días)

**Objetivo:** congelar el alcance del primer nivel y evitar construir funciones que no se usarán en el museo.

- Definir duración de una partida: objetivo de 90–180 segundos.
- Definir público, altura de monitor, distancia de observación y tiempo máximo por visitante.
- Elegir el formato de la esfera/control físico: cuatro botones, joystick, inclinómetro o combinación.
- Crear tablero de tareas y reglas de rama/commit.
- Medir espacio disponible, versión de Unity, resolución y FPS base del equipo.

**Entregables:** ficha de nivel 01, lista de restricciones físicas, calendario, responsable por área y lista de hardware disponible.

**Salida:** una sola versión de la experiencia está aprobada para implementar. Cualquier función no esencial pasa a backlog.

### Fase 1 — Núcleo de movimiento estable (3–5 días)

**Objetivo:** que la bola sea divertida de mover antes de invertir tiempo en arte o hardware.

- Calibrar aceleración, velocidad máxima, fricción y respuesta al choque.
- Mantener la bola anclada matemáticamente a la superficie.
- Aplicar giro visual proporcional al recorrido.
- Mejorar cámara: centrado, distancia, amortiguación y orientación sin roll brusco.
- Añadir pantalla de diagnóstico con velocidad, celda y FPS, activable sólo en desarrollo.
- Definir pruebas de control: giro rápido, diagonal, borde, choque, reinicio y meta.

**Criterios de salida:**

- La bola responde en menos de 100 ms en teclado.
- Al soltar una dirección se desliza brevemente, pero no se siente pesada ni se detiene de golpe.
- No puede salir del mapa, atravesar muro o despegarse del planeta durante 10 minutos de prueba continua.
- La cámara no corta a través de la esfera ni desorienta al jugador.

### Fase 2 — Pipeline de mapas PNG (4–6 días)

**Objetivo:** que el equipo pueda diseñar y validar niveles sin editar código.

- Formalizar paleta y dimensiones admitidas del PNG.
- Validar inicio, meta, conectividad, borde y número de celdas.
- Generar piso, muros, meta e inicio desde la imagen.
- Crear herramienta/editor para previsualizar el mapa plano y la proyección esférica.
- Guardar parámetros por nivel: radio, amplitud angular, velocidad, cámara, dificultad y récord.
- Diseñar tres mapas: tutorial, estándar y reto.

**Criterios de salida:**

- Cambiar el PNG y pulsar Play reconstruye el nivel sin modificar scripts.
- Un error de diseño del PNG genera un mensaje comprensible, no un nivel roto.
- El mesh de muros es combinado; no se generan cientos de GameObjects por celda.
- El mapa estándar se completa desde inicio hasta meta.

### Fase 3 — Bucle de juego y dificultad (4–6 días)

**Objetivo:** convertir el recorrido en un juego competitivo comprensible en segundos.

- Añadir estrellas, rutas opcionales, checkpoints, zonas de caída/trampa y obstáculos estáticos.
- Recuperar las reglas definitivas: 3 vidas, reinicio desde checkpoint, reinicio total al agotar vidas.
- Puntuación: estrellas + meta + bonificación de tiempo; registrar récord por nivel.
- Añadir menú breve: jugar, elegir nivel, instrucciones y récord.
- Diseñar dificultad progresiva: tutorial → bifurcaciones → rutas estrechas → riesgo/recompensa.
- Balancear con sesiones reales; ningún nivel debe depender de una maniobra accidental.

**Criterios de salida:**

- El jugador entiende en menos de 15 segundos qué hacer y cómo reiniciar.
- Es posible llegar a meta sin tomar las rutas de riesgo.
- Una estrella no puede contar dos veces dentro del mismo intento.
- Los récords se guardan sin incluir datos personales.

### Fase 4 — Presentación, accesibilidad y audio (3–5 días)

**Objetivo:** que la estación se vea terminada y se pueda usar en un museo ruidoso.

- Arte coherente: materiales, luz, muro, bola, meta y marcadores de ruta.
- Colores complementados con formas e iconos; no depender sólo de rojo/verde.
- Sonidos originales para rodar, chocar, estrella, checkpoint, caída y meta; botón de silencio.
- HUD legible a distancia: puntos, tiempo, vidas y acción siguiente.
- Modo atracción cuando no hay jugador: demo visual, instrucciones y récord destacado.
- Pantalla de resultado de 3–5 segundos y regreso automático a atracción.

**Criterios de salida:**

- Texto y estados principales se distinguen a la distancia real de montaje.
- Hay feedback visual y sonoro de toda acción importante.
- El juego puede iniciar una nueva partida sin teclado de administración.

### Fase 5 — ESP32 y controlador físico (5–8 días)

**Objetivo:** sustituir o complementar el teclado sin perder capacidad de diagnóstico.

- Prototipo eléctrico de cuatro botones, resistencia/pull-up y alimentación segura.
- Firmware ESP32 con antirrebote, estado de botones y mensaje de conexión.
- Elegir protocolo: HID USB si el modelo/OS lo permite; de lo contrario serial USB con formato documentado.
- Crear `IInputSource`/adaptador para intercambiar teclado, serial y HID sin tocar el juego.
- Añadir pantalla de estado: conectado, último dato, latencia estimada y fallback activo.
- Probar cable, reconexión, rebote, pulsaciones simultáneas y funcionamiento prolongado.

**Criterios de salida:**

- Cuatro botones y combinaciones diagonales funcionan de manera repetible.
- Desconectar el ESP32 no bloquea el juego; teclado sigue disponible para mantenimiento.
- El sistema se reconecta o muestra instrucciones claras para el operador.
- Latencia total percibida es menor de 100 ms.

### Fase 6 — Esfera física y estación (3–6 días)

**Objetivo:** convertir el software en una instalación segura y repetible.

- Medir esfera: diámetro, material, brillo, estabilidad, montaje y seguridad de bordes/cables.
- Definir su papel: control decorativo, carcasa del controlador, objeto escenográfico o superficie de proyección.
- Probar visualización real antes de comprometerla: proyector, iluminación, ángulo de espectadores y oclusiones.
- Diseñar soporte para monitor, esfera, botones, cableado, etiqueta de instrucciones y acceso al operador.
- Configurar inicio automático de la aplicación, modo ventana/pantalla completa y recuperación tras reinicio.
- Preparar checklist de encendido, apagado y solución de fallos.

**Criterios de salida:**

- Una persona puede montar y abrir la estación siguiendo el checklist.
- No hay cable suelto, pieza inestable ni botón expuesto de forma peligrosa.
- El monitor sigue siendo la ruta funcional si la proyección/esfera falla.

### Fase 7 — Pruebas con visitantes y corrección (5–7 días)

**Objetivo:** validar comprensión, diversión y confiabilidad antes de la exhibición.

- Pruebas con al menos 10 personas que no participaron en el desarrollo.
- Registrar: tiempo para entender, duración de partida, muertes, meta alcanzada, confusiones y comentarios.
- Probar niños/adultos, usuarios diestros/zurdos y condiciones de iluminación/ruido similares al museo.
- Prueba de resistencia: 2–4 horas continuas con partidas repetidas.
- Corregir los tres problemas más frecuentes antes de añadir funciones nuevas.

**Criterios de salida:**

- Al menos 80% de visitantes inicia una partida sin ayuda adicional después de leer la instrucción.
- No hay bloqueo, fuga de memoria perceptible ni pérdida de control durante la prueba prolongada.
- El nivel inicial tiene tasa de finalización aceptable para la duración definida.

### Fase 8 — Cierre y entrega (2–3 días)

**Objetivo:** dejar una instalación recuperable aunque no esté presente quien la programó.

- Crear build de exposición versionado y copia de respaldo local.
- Etiquetar versión/commit de producción.
- Actualizar README: instalación, controles, mapa activo, parámetros, pruebas y pendientes conocidos.
- Entregar manual de operador de una página y guía técnica más detallada.
- Registrar firmware ESP32, diagrama eléctrico, lista de piezas y código fuente.
- Realizar ensayo completo de montaje y desmontaje.

**Criterios de salida:**

- El build, el proyecto Unity y el firmware se abren desde instrucciones escritas.
- Existe una copia de respaldo probada.
- Las limitaciones no verificadas se indican claramente; no se prometen VR, proyección o pantalla esférica sin evidencia.

## 5. Cronograma sugerido

| Semana | Prioridad principal | Hito verificable |
|---|---|---|
| 1 | Fases 0–1 | Movimiento, cámara y choque agradables en teclado. |
| 2 | Fase 2 | Tres PNG de nivel válidos y generador reutilizable. |
| 3 | Fase 3 | Partida completa con meta, vida, checkpoint, estrellas y récord. |
| 4 | Fase 4 | HUD, audio, accesibilidad y modo atracción. |
| 5 | Fase 5 | ESP32 funcional y teclado de respaldo. |
| 6 | Fase 6 | Primera estación montada en banco. |
| 7 | Fase 7 | Pruebas con visitantes y correcciones. |
| 8 | Fase 8 | Build final, respaldo, manual y ensayo de museo. |
| 9–10 | Reserva | Ajustes por hardware, arte o pruebas reales. |

## 6. Backlog priorizado

### Imprescindible para exhibir

- Movimiento estable y agradable.
- Un nivel completo, meta, reinicio y récord local.
- Cámara legible, HUD e instrucciones.
- Mapa PNG validado y reconstruible.
- Teclado de mantenimiento y cuatro botones ESP32 funcionales.
- Inicio automático, recuperación ante desconexión y checklist de operador.

### Importante, pero posterior al primer nivel

- Estrellas, checkpoints, trampas y rutas alternativas.
- Tres niveles y selector de nivel.
- Audio, modo atracción y tabla local de récords.
- Mejoras visuales y animaciones.

### Sólo si queda tiempo y se valida físicamente

- Proyección sobre esfera o videomapping.
- Sensores de inclinación reales.
- Marcador público o multijugador asíncrono.
- Obstáculos móviles y modificadores de superficie.
- VR/AR. No forman parte del MVP actual.

## 7. Pruebas que deben quedar documentadas

| Área | Prueba | Evidencia esperada |
|---|---|---|
| Movimiento | Mantener cada dirección y las diagonales | Video corto y parámetros usados. |
| Mapa | PNG inválido, sin meta, sin inicio, ruta cerrada | Mensaje claro y sin fallo de Unity. |
| Cámara | Recorrer bordes y regiones altas/bajas | No atraviesa planeta ni hace roll inesperado. |
| Puntuación | Estrella, checkpoint, muerte, reinicio y meta | Valores correctos y sin duplicación. |
| Hardware | Rebote, cable desconectado, reconexión | Diagnóstico y fallback funcional. |
| Rendimiento | Sesión de 2–4 horas | FPS, temperatura y errores registrados. |
| Montaje | Encendido desde equipo apagado | Checklist completado por otra persona. |

## 8. Riesgos y respuestas

| Riesgo | Señal temprana | Respuesta |
|---|---|---|
| La esfera física no admite imagen legible | Reflejo, baja luminosidad o curvatura excesiva | Usar monitor como salida principal; esfera como interfaz/escenografía. |
| Cámara marea o pierde orientación | Visitantes dejan de mover o preguntan dónde está la bola | Reducir rango del mapa, bloquear roll y aumentar referencias visuales. |
| Control físico tiene rebote o latencia | Movimiento múltiple por una pulsación | Antirrebote en firmware y software; telemetría de entrada. |
| Nivel demasiado difícil | Pocas metas, muchas muertes tempranas | Ruta segura corta, tutorial y atajos claros. |
| Mapa PNG se vuelve inconsistente | Muros visuales no coinciden con reglas | Una sola fuente PNG y validador automático. |
| Build falla durante exposición | Crash o ventana incorrecta | Build probado, inicio automático, reinicio rápido y copia de respaldo. |

## 9. Definición global de terminado

El juego estará listo para museo cuando una persona ajena al equipo pueda:

1. Encender la estación y abrir la experiencia siguiendo el manual.
2. Entender los cuatro controles y comenzar una partida sin explicación oral.
3. Mover una bola con sensación de rodamiento, sin que se desprenda del planeta.
4. Llegar a meta, ver puntuación/tiempo y reiniciar.
5. Usar el controlador ESP32; si falla, el operador puede pasar a teclado sin detener la estación.
6. Repetir la operación durante una sesión prolongada sin errores críticos.

## 10. Próxima secuencia recomendada

1. Terminar el ajuste fino de aceleración, fricción, cámara y escala de la bola con pruebas de 5 minutos.
2. Implementar validador de PNG y corregir la presentación del mapa actual.
3. Añadir meta, estrella, checkpoint y trampa al contrato del PNG.
4. Crear el primer nivel completo y hacer una prueba con cinco personas.
5. Sólo después conectar el ESP32; así los problemas de hardware no ocultan problemas de diseño del juego.

