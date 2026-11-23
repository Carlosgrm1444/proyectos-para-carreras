# Plan histórico de distribución y decisiones pendientes

> **ATENCIÓN — Alcance actualizado:** la estimación de varias semanas más abajo corresponde a siete **estaciones terminadas para exhibición**, no al nuevo objetivo de **siete prototipos funcionales en 48 horas**. Para trabajo inmediato usar [21-sprint-48h-siete-videojuegos.md](21-sprint-48h-siete-videojuegos.md) y las tareas [22–28](../../AGENTS.md).

> Fecha: 22 de noviembre de 2026. Estado: **planificación, no asignación definitiva, ni autorización para implementar**.
> Repositorio: `Carlosgrm1444/proyectos-para-carreras`.
> Inventario de hardware actualmente comprobado en `main`: [18-especificaciones-del-equipo.md](18-especificaciones-del-equipo.md).
> **Limitación:** en `main` se encontró **un solo inventario**; las especificaciones de la segunda computadora aún no están verificadas. La distribución hacia el equipo B es condicional.

## 1. Hardware comprobado y primera evaluación

**Equipo A, Ubuntu 24.04:** Intel Core i7-10750H (6 núcleos/12 hilos), ~32 GB de RAM, Intel UHD Graphics detectada, SSD NVMe ~1 TB y ~4 TB. En la partición de trabajo sólo había **40 GB libres** al registrar el inventario. Versiones de Git, GitHub CLI, VS Code, Node y .NET están en [el inventario](18-especificaciones-del-equipo.md).

- Encaja mejor inicialmente en lógica, código C#, gestión de proyectos, pruebas ligeras, programación de microcontroladores, detección de marcadores, prototipos de MediaPipe y pruebas de servicios locales.
- **No asumir compatibilidad ni rendimiento adecuado** del editor Unity/OpenXR sobre Intel UHD en Ubuntu antes de probar una escena gris. Si no funciona, preparar código/visión en Ubuntu y probar Unity en otra máquina.
- Liberar o trasladar espacio de trabajo antes de instalar editor, Android SDK, proyectos y builds; referencia de planificación **80–100 GB libres** en volumen de desarrollo.
- La detección de una GPU dedicada, de existir, sigue pendiente: no se debe inferir ausencia física absoluta de GPU desde un inventario que sólo registra Intel UHD.

**Equipo B:** SO, CPU, RAM, GPU dedicada y almacenamiento **no confirmados en los archivos actuales**. Usarlo para VR/3D solamente si se comprueba GPU, runtime VR, modelo de visor, estabilidad térmica y plataforma objetivo. Si es Meta Quest Link PCVR, verificar compatibilidad Windows y GPU; el desarrollo de VR standalone (Quest/Android) requiere una estrategia distinta.

## 2. Repartición provisional por proyecto

| Equipo recomendado | Proyecto | Carga principal | Dependencia crítica |
|---|---|---|---|
| A: Ubuntu | [Psicología — Atención bajo ataque](../disenos/16-psicologia-atencion-bajo-ataque-mediapipe.md) | MediaPipe y cámara, reglas, puntuación, combos y vida | Prueba de latencia de cámara/seguimiento y entrada al juego |
| A: Ubuntu | [Administración — Almacén al límite](../disenos/11-administracion-almacen-al-limite-hibrido.md) | PEPS/UEPS, pedidos, cámara y detección de 4 anaqueles físicos | Reconocimiento ArUco/ubicación/orientación y mapping; Unity en Ubuntu sujeto a test |
| A: Ubuntu | [Contador Público — Flujo de caja](../disenos/13-contador-publico-flujo-caja-ar.md) | Prototipo multi-marcador, detección de giro y lógica de válvulas | Cuatro targets reconocibles y anclaje estable del mapa |
| A: Ubuntu | [LIIA — Laberinto gravitatorio esférico](../disenos/15-liia-laberinto-gravitatorio-esferico.md) | Firmware ESP32 y cuatro botones, movimiento/físicas, puntuación | Prueba de controlador y **visualización física de la esfera**, aún no verificada |
| B: sujeto a GPU/VR | [Derecho — Firewall de privacidad](../disenos/14-derecho-firewall-de-privacidad.md) | Unity 3D isométrico, enemigos, animación, armas, oleadas | Rendimiento y sistema de input de mando |
| B: sujeto a GPU/VR | [Comercio Exterior — Clasificador en caos](../disenos/12-comercio-exterior-clasificador-en-caos-vr.md) | VR de almacén, manipulación de mercancía y locomoción por joystick | Visor y runtime XR compatibles, pruebas de confort |
| B: sujeto a GPU/VR | [Autotransporte — Carga equilibrada VR](../disenos/17-autotransporte-carga-equilibrada-grua-vr.md) | VR con grúa cenital y apilamiento de físicas | Visor, manipulador de piezas, rendimiento físico |
| Ambos | [Arquitectura compartida](../arquitectura/00-arquitectura-y-criterios-de-museo.md) | ArcadeCore, guardado de récords, menús, pruebas, builds, documentación | Contratos de módulos, versión Unity y sistema Git consistentes |

**Si B no cuenta con GPU VR compatible:** no iniciar integración PCVR y no prometer fechas de puesta en marcha; mientras tanto prototipar las escenas sin visor en monitor y explorar despliegue standalone si el modelo de visor lo permite. Reasignar tareas al conocer el equipo real.

## 3. Especificaciones que faltan cerrar por videojuego

| Juego | Decisiones bloqueantes P0 | Afinaciones de balance P1 |
|---|---|---|
| Administración | Flujo exacto de reposición/retirada PEPS-UEPS; accesos de racks; cuándo se pueden mover físicamente; prueba de 4 marcadores en pared | Lotes iniciales, demanda, puntos y límite de atrasos |
| Comercio Exterior VR | Modelo de visor, destino/3 opciones y condición de entrega; movimiento por joystick y comodidad; conjunto real de mercancías validado por docentes | Aparición de productos, límite de saturación, enemigos, daño a mercancía, puntos |
| Contador AR | **Función del cuarto target**; reconocimiento de marcadores simultáneos y giros; cámara/dispositivo; qué hace el personaje al topar con enemigos | Mapa, metas de liquidez, cronómetro, condición exacta de derrota |
| Derecho | Pistas visibles para distinguir amenazas; controles espada/arco/escudo; validación de metáforas jurídicas | Vida de jugador y centro, daño, tipos/frecuencias de enemigos, puntos, drops, control de duración |
| LIIA | Cómo mostrar el juego en la esfera real (no verificado que sea pantalla); eje/gravedad y controles de cuatro botones | Número de laberintos, puntuación de estrellas, checkpoint/tiempo y dificultad |
| Psicología | Arquitectura de cámara/MediaPipe y calibración; qué movimientos se reconocen; modo sentado y pérdida de tracking | Vida inicial, umbral y cantidad de cura por combo, regla/velocidad de oleadas, puntos |
| Autotransporte VR | Detección de derrumbe que quita vida; restablecimiento de la **ronda actual** tras perder una vida; control y giro de las cajas; visor | **3 rondas y 3 vidas confirmadas**; piezas por ronda, masa, tiempo, puntuación y valores de estabilidad |

**Reglas ya cerradas que no deben reabrirse sin solicitud:** Administración sólo tiene anaqueles físicos; todo el resto del escenario se proyecta. Comercio Exterior usa joystick VR sin teletransporte y evalúa la categoría sólo al entregar. Contador usa récord por **menor tiempo**, no por puntuación. Derecho termina si muere el guardián o el centro; juego de oleadas sin final. LIIA tiene 3 vidas y checkpoints; al agotar vidas reinicia laberinto. Psicología pierde vida con errores y recupera con combos. Autotransporte es apilamiento desde grúa cenital, no conducción ni extracción de piezas; 3 rondas, 3 vidas y reinicio en ronda 1 al agotarlas.

## 4. Estándares compartidos a acordar antes de Codex

1. **Plataformas y motores**: una versión fija de Unity y paquetes compatibles; decidir entrega PC (Windows/Linux), Quest standalone o PCVR **por juego**. Evitar suponer que una compilación de escritorio funciona en todos los dispositivos.
2. **Repositorio**: elegir explícitamente un proyecto Unity por minijuego vs proyecto principal con escenas y módulos compartidos; `ArcadeCore` con interfaz versionada. No crear siete implementaciones divergentes de vidas, puntuación o guardado.
3. **Flujo Codex**: cada tarea acotada debe indicar especificación fuente, rama propia, criterio de aceptación, pruebas y PR; integrar tras revisión, no editar simultáneamente el mismo archivo/escena en dos computadoras.
4. **Archivos pesados y entornos**: definir `.gitignore` Unity (no subir `Library`, `Temp`, cachés, builds), Git LFS para binarios grandes si necesario, y no guardar credenciales ni grabaciones de cámara.
5. **Hardware de exhibición**: inventario verificable de visores, cámaras, micrófonos (si aplican), proyectores, esfera, mandos, ESP32, cables, espacio físico y permisos de instalación.
6. **Diseño de la exhibición**: sesiones, tutorial corto, reinicio automático, modo sin dispositivo especial, calibración, seguridad física, higiene VR, accesibilidad y privacidad.
7. **Validación académica**: verificar ejemplos de mercancías, principios PEPS/UEPS, conceptos de liquidez, protección jurídica y explicación de procesos psicológicos.

## 5. Orden sugerido de pruebas de viabilidad (antes de juegos completos)

- **A / visión artificial**: leer simultáneamente 4 markers (Contador) y detectar 4 racks sobre pared (Administración); webcam MediaPipe a latencia suficiente; controlador de 4 botones enviando entrada a PC; probar Unity con GPU actualmente detectada.
- **B / XR**: abrir escena VR con el visor y controlador real; manipular una caja y desplazarse con joystick; medir rendimiento y confort, estabilidad en apilamiento; confirmar que la GPU soporta el runtime previsto.
- **Física/esfera LIIA**: prototipo en monitor; **separar** validación del juego de la capacidad del montaje de esfera para mostrarlo. Si falla proyección física, habilitar pantalla alternativa.
- **Juego base Derecho**: construir primero una vertical slice simple (jugador, centro, enemigo, vida, puntaje), a partir de la cual extraer/validar el núcleo arcade común.

## 6. Estimación de tiempos (orientativa, no compromiso)

**Hipótesis:** una persona coordinando ambas computadoras y Codex, **25–35 horas semanales de trabajo humano**, visores y demás hardware disponibles, alcance de versiones 1 relativamente contenido. Las horas de revisión, pruebas, configuración y corrección son trabajo humano, no se eliminan por usar Codex.

| Entregable | Rango estimado |
|---|---|
| Especificaciones/hardware y riesgos P0 | 1–2 semanas |
| Arquitectura compartida de juegos y herramientas | 2–3 semanas |
| MVPs iniciales de los 7 juegos (en paralelo cuando se pueda) | 9–14 semanas |
| Integración y pruebas iniciales | 2–3 semanas |
| **Todos los MVPs jugables** | **14–22 semanas** acumuladas |
| Pulido de arte, QA de exhibición, calibración y montaje | +10–18 semanas |
| **Siete estaciones listas para una exhibición estable** | **24–40 semanas** acumuladas |

**No es un plazo garantizado.** Puede crecer si la segunda máquina no tiene GPU VR, la esfera no puede visualizar imágenes, AR y mapeo presentan problemas o se trabaja menos horas/semana. Dos computadoras no sustituyen dos personas. Las estimaciones se revisarán tras las pruebas P0.

## 7. Próxima actualización necesaria

1. Incorporar **especificaciones verificadas de la computadora B** (CPU, RAM, GPU, SO, almacenamiento) al repositorio; no basarse en equipos mencionados en otras conversaciones sin confirmar cuál es.
2. Confirmar **modelo de visor VR** y posibilidad real de pruebas.
3. Elegir si los siete juegos serán **proyectos Unity independientes** o un **proyecto modular compartido**; requiere atender el desarrollo paralelo y la compatibilidad de paquetes.
4. Priorizar la prueba de tres incertidumbres técnicas: **esfera**, **cuatro marcadores AR** y **visor XR real**.
5. Cerrar las decisiones P0 por juego antes de enviar tareas completas a Codex.

**Fuentes internas:** fichas del catálogo `11`–`17`, [protocolo de planificación](../disenos/10-guia-de-planificacion-y-ejecucion-codex.md) e inventario `18`.

