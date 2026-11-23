# Sprint 48 horas: siete videojuegos jugables

> **Prioridad vigente (22 de noviembre de 2026).** El objetivo confirmado es **tener un primer prototipo funcional de los SIETE juegos en 48 horas**. Sustituye la prioridad de sólo dos videojuegos del documento 20, que queda como referencia histórica. No se desarrollan todavía versiones terminadas de museo.
>
> **Entrega:** código fuente, escenas o aplicaciones ejecutables en local, instrucciones para iniciar, evidencia de una partida de cada juego y registro de limitaciones. **Arte definitivo NO**; usar formas, colores, texto y **efectos sonoros básicos** (acierto, error, daño, victoria, derrota). **No afirmar VR, AR, cámara ni videomapping funcionales sin haberlo probado con hardware real.**
>
> **Reglas permanentes:** consultar las fichas originales [11–17](../../README.md) y las tareas de Codex enlazadas en este documento. Las simplificaciones afectan presentación/escala, no las decisiones principales de jugabilidad.

## 1. Asignación por computadora (condicional)

| Máquina | Proyectos | Paquetes de trabajo |
|---|---|---|
| **A — Ubuntu i7-10750H, 32 GB, GPU Intel UHD registrada** | Administración, Psicología, Contador Público y LIIA | [Administración](../paquetes-codex/23-codex-mvp-administracion.md), [Psicología](../paquetes-codex/25-codex-mvp-psicologia.md), [Contador](../paquetes-codex/24-codex-mvp-contador.md), [LIIA](../paquetes-codex/26-codex-mvp-liia.md) |
| **B — preferentemente mayor GPU** | Derecho, Autotransporte y Comercio Exterior | [Derecho](../paquetes-codex/22-codex-mvp-derecho.md), [Autotransporte](../paquetes-codex/28-codex-mvp-autotransporte.md), [Comercio Exterior](../paquetes-codex/27-codex-mvp-comercio.md) |

**Advertencia:** en la rama `main` examinada sólo consta el inventario [18-especificaciones-del-equipo.md](18-especificaciones-del-equipo.md), con 40 GB libres en la partición de trabajo al registrarlo; verificar la segunda máquina y liberar espacio de compilación. Si su GPU es inferior, pasar Derecho y prototipos 3D de pantalla a la más capaz. El editor Unity en Linux y el runtime VR específicos requieren una prueba de arranque antes de comprometerse.

## 2. Política de alcance: 7 bucles cerrados primero

Para que un juego cuente como **MVP funcional** debe:
1. Iniciar una escena desde una instrucción reproducible, mostrar controles, responder al jugador y ejecutar **la acción central real de ese juego**.
2. Mostrar consecuencias, vida/tiempo/puntos, aumentar dificultad o progresar rondas, y tener victoria o derrota según su ficha.
3. Mostrar resultados, reiniciar sin reiniciar el editor y conservar **récord local** (Contador: menor tiempo; los otros: máxima puntuación).
4. Tener al menos tres sonidos breves identificables (interacción/acierto, error/daño, fin), generados original o de licencia compatible, con opción de silenciar.
5. Entregar proyecto/fuentes versionados, pasos de ejecución y **registro honesto** de qué adaptadores físicos funcionan, cuál es simulado y cuál está pendiente.
6. Conservar modos fallback de teclado/ratón para depuración. **Fallback NO equivale a integración hardware**.

Los siete MVPs pueden estar en pantalla; VR/AR/MediaPipe/ESP32/videomapping deben probarse en paralelo con cortes de tiempo. El compromiso de dos días es un objetivo exigente, no una garantía: no sacrificar un bucle jugable por pasar horas intentando calibrar un sensor.

## 3. Arquitectura rápida, sin sobreingeniería

**Dos proyectos Unity aislados en el mismo repositorio:**
```text
prototipos/
  equipo-a/                 # Proyecto Unity A
    Assets/Scenes/Administracion.unity
    Assets/Scenes/Psicologia.unity
    Assets/Scenes/Contador.unity
    Assets/Scenes/LIIA.unity
    Assets/Scripts/Shared/
    Assets/Scripts/<Juego>/
    Packages/
    ProjectSettings/
  equipo-b/                 # Proyecto Unity B
    Assets/Scenes/Derecho.unity
    Assets/Scenes/Autotransporte.unity
    Assets/Scenes/Comercio.unity
    Assets/Scripts/Shared/
    Assets/Scripts/<Juego>/
    Packages/
    ProjectSettings/
docs/sprint-48h/            # opcional: bitácoras, pruebas y capturas sin datos personales
```
La estructura es recomendación para Codex, **no indica que esas carpetas ya existan**. Cada PC crea sólo su propio proyecto y su rama:
- Equipo A: `feat/mvp-48h-equipo-a`
- Equipo B: `feat/mvp-48h-equipo-b`

**No comparten bibliotecas binarias ni editan el mismo archivo al mismo tiempo.** Reutilizar una interfaz simple por proyecto: `GameState` (menu/playing/result), `ScoreTimeLives`, `HighScoreStore`, `SimpleSfx`, `Restart` y un `InputAdapter` por juego. Unity Input System u otra solución ya instalada, sin instalar paquetes XR en A sólo por compatibilidad teórica.

**Git:** guardar `Assets/`, `Packages/`, `ProjectSettings/`, escenas, scripts y `.meta`; ignorar `Library/`, `Temp/`, `Obj/`, `Logs/`, `UserSettings/`, `Build/`, `Builds/` y cachés. Si los dos proyectos usan distinta versión/paquetes, documentarla. No introducir un monorepo compartido con escenas de ambos equipos en un solo proyecto durante el sprint.

## 4. Orden operacional de 48 horas (reloj desde el inicio real)

| Horas acumuladas | A: cuatro juegos | B: tres juegos | Resultado obligatorio |
|---|---|---|---|
| **0–3 h** | Probar Unity, estructura y menú de 4 escenas | Probar Unity, versión, proyecto y menú de 3 escenas | Se pueden abrir proyectos, iniciar/reiniciar escenas, guardar código |
| **3–13 h** | Administración y Psicología: núcleo jugable | Derecho: guardián/NPC/centro/combate inicial | **3 bucles esenciales** con entrada y resultado |
| **13–23 h** | Contador y LIIA: núcleos jugables | Autotransporte: grúa y colisiones | **6 bucles esenciales** |
| **23–30 h** | Reglas completas, vidas, ré­cord de los 4; prototipo cámara/USB en paralelo | Comercio Exterior: clasificar + entregar y saturación | **7 bucles esenciales** en pantalla |
| **30–39 h** | Integración preferente MediaPipe, 4 marcadores ArUco o botones ESP32, sin bloquear juegos | Intentar XR con visor real, incrementar enemigos y dificultad | Mínimos acordados implementados y hardware probado o documentado como pendiente |
| **39–45 h** | Sonidos, pruebas, corrección de errores, README, capturas | Sonidos, pruebas, corrección de errores, README, capturas | Partidas completas, reinicios y récords verificados |
| **45–48 h** | Commit/PR y transferencia | Commit/PR y transferencia | Código en ramas y PRs, checklist de 7 juegos, documentación de limitaciones |

No intentar siete videojuegos al mismo tiempo desde cero: **un bucle mínimo por proyecto y luego una segunda pasada para completar reglas**. Invertir un máximo inicial de ~90 minutos en solucionar un problema de hardware por juego; si no se resuelve, activar fallback y registrar bloqueo.

## 5. Tabla ejecutiva de alcance

| Juego | Acción central NO negociable | Derrota/victoria y récord | Hardware |
|---|---|---|---|
| Administración | 4 racks, un producto/rack, pedidos compra→rack→entrega, PEPS/UEPS, movimiento de racks y cambio de demanda entre partidas | Saturación de pedidos; récord por puntos | ArUco en pared + proyector; fallback racks digitales |
| Psicología | Movimiento por cámara, tocar objetivos, evitar distractores, combos curan vida | Oleadas crecientes hasta vida 0; puntos | MediaPipe/webcam; fallback mouse/teclado |
| Contador Público | Mapa, personaje, válvulas de entrada/salida de efectivo, huir de enemigos | Cumplir obligaciones y terminar rápido; menor tiempo | 4 marcadores AR (función cuarto por definir); fallback entradas virtuales |
| LIIA | Rotar esfera virtual de 4 direcciones, física rodante en superficie exterior, estrellas/checkpoints | 3 vidas; agotar reinicia laberinto; mayor puntaje | ESP32 + 4 botones; pantalla fallback; esfera real no validada |
| Derecho | Guardián isométrico, NPCs buenos/malos, espada/arco/escudo, 2 vidas independientes, drops aleatorios | Oleadas infinitas hasta cae guardián **o centro**; récord puntos | Mando/teclado |
| Autotransporte | Grúa cenital toma cajas del suelo y apila con físicas dentro del tráiler | **3 rondas y 3 vidas**; agotar reinicia ronda 1; récord puntos | XR/mandos; fallback cámara cenital mouse |
| Comercio Exterior | Agarrar/inspeccionar, elegir 1 de 3, transportar con joystick y entregar antes de recibir feedback, defender mercancía | Saturación de recepción; récord puntos | XR/joystick; fallback FPS con mouse/teclado |

## 6. Reglas de prioridad e integraciones

**P0 — imprescindible antes del fin:** acciones principales de las fichas, victoria/derrota, rondas/oleadas, score o tiempo, restart, entrada fallback. **P1 — intentar sin comprometer P0:** seguimiento físico (ArUco, MediaPipe, ESP32), VR real (Comercio/Autotransporte), proyector/AR multi-target, audio mínimo. **P2 — después:** arte, optimizaciones gráficas, niveles adicionales, UI compleja, assets comerciales, efectos espectaculares, modos extra, integración de esfera real.

Para los dos juegos VR, desarrollar **primero** un manipulador/locomoción con controles equivalentes para monitor y probar XR real temprano con un cubo; si falla compatibilidad del visor, no afirmar que se completó VR. Para Contador, **no inventar el cuarto target como requisito cerrado**; usar una tecla virtual provisional hasta decidirlo. En LIIA, la esfera exterior necesita fuerza hacia el centro + componente tangencial de gravedad/rotación para mantener trayectoria jugable; no tratar la esfera reflectante física como pantalla confirmada.

## 7. Instrucciones de cada sesión de Codex

1. Hacer `git pull --ff-only origin main`, crear rama del equipo, leer **este plan + ficha de tarea 22–28 + ficha original 11–17**. Confirmar carpeta asignada, y no alterar la del otro equipo.
2. Describir en README local la versión exacta de Unity instalada, plataforma de prueba, controles y arranque del juego.
3. Implementar en **dos pasadas**: (A) loop jugable gris con resultado; (B) reglas restantes, guardado, sonidos y hardware opcional.
4. Trabajar con valores de balance **serializados/configurables**. Cuando una decisión sea provisional, marcarla `MVP_DEFAULT_NOT_FINAL` en documentación, no confundirla con requisito aprobado.
5. Si el editor no está disponible para Codex, producir código, escena reproducible y checklist manual, pero NO marcar «probado». La persona en computadora debe ejecutar la build.
6. Al terminar: registrar qué pasó y falló, commit, push a rama, abrir PR. No editar `main` directamente ni hacer merge de trabajo no probado.

## 8. Orden de recortes si la cuenta regresiva aprieta

1. Quitar modelados, texturas, animación compleja, música y menús secundarios: conservar 3 SFX simples.
2. Reducir **cantidad de objetos/enemigos y mapas**, manteniendo **sus tipos y reglas distintas**.
3. Pausar hardware/proyección al alcanzar el tiempo límite de diagnóstico; conservar adaptador y fallback que permiten jugar.
4. Quitar balance avanzado y opciones de calidad de vida; mantener muerte/victoria/vida/score/record/reinicio.
5. **Nunca** eliminar silenciosamente las mecánicas distintivas: ARCO/consentimiento de Derecho, PEPS/UEPS de Administración, feedback al entregar en Comercio, 3 rondas/3 vidas de Autotransporte, combos sanan en Psicología, checkpoint de LIIA, récord por tiempo de Contador.

## 9. Evidencia y entrega final

Por juego: `README` con comando/acción de arranque y controles, captura de escena o video corto sin participantes, captura de derrota/victoria y récord, resultado de pruebas manuales, hardware conectado (si aplica) y fallos conocidos. Al finalizar el sprint, el README principal deberá mostrar qué juegos son jugables, cuáles usan fallback y qué integraciones permanecen pendientes. **No prometer builds que no se han generado**.

## 10. Paquetes de trabajo de Codex, listos para usar

- [22 — Derecho](../paquetes-codex/22-codex-mvp-derecho.md)
- [23 — Administración](../paquetes-codex/23-codex-mvp-administracion.md)
- [24 — Contador Público](../paquetes-codex/24-codex-mvp-contador.md)
- [25 — Psicología](../paquetes-codex/25-codex-mvp-psicologia.md)
- [26 — LIIA](../paquetes-codex/26-codex-mvp-liia.md)
- [27 — Comercio Exterior](../paquetes-codex/27-codex-mvp-comercio.md)
- [28 — Autotransporte de Carga](../paquetes-codex/28-codex-mvp-autotransporte.md)

**Estas instrucciones habilitan planificación para Codex; todavía no son software desarrollado.**
