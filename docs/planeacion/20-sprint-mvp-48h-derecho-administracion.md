# Sprint anterior (SUPERADO) — primeros prototipos de Derecho y Administración

> **AVISO:** este plan de DOS juegos está reemplazado por el plan vigente de [SIETE juegos en 48 horas](21-sprint-48h-siete-videojuegos.md) y sus paquetes [22–28](../../AGENTS.md). No usar este archivo como encargo principal de Codex; se conserva sólo como referencia.

> **Estado:** plan de prototipo acordado como prioridad; no implica que ya exista código ni que el hardware esté validado.
> **Fuentes de requisitos vigentes:** [Derecho](../disenos/14-derecho-firewall-de-privacidad.md), [Administración](../disenos/11-administracion-almacen-al-limite-hibrido.md).
> **Horizonte:** dos días naturales de trabajo desde el arranque efectivo. No es un compromiso de entrega ni garantiza la integración física.

## Objetivo no negociable

Producir dos videojuegos **jugables de principio a fin**, con reglas, pérdida, resultado y récord, aunque el arte sean primitivas. En la versión de 48 h se posponen música, pulido, nuevas funcionalidades, ilustraciones definitivas y empaquetado de museo.

## Distribución provisional

- **Equipo A:** Ubuntu 24.04, Intel i7-10750H, ~32 GB RAM, GPU Intel UHD, 40 GB libres en la partición de trabajo al registrarse: **Administración** por trabajo de lógica, cámara y marcado. Hay que liberar espacio.
- **Equipo B:** **Derecho**, idealmente el equipo de GPU más capaz. **No consta todavía el inventario del segundo equipo en el árbol de `main` revisado**. Ajustar al comprobarlo.
- **Dos ramas separadas**: `feat/mvp-administracion-48h` y `feat/mvp-derecho-48h` nacidas de `main`, sin editar simultáneamente archivos compartidos.
- **Estructura propuesta**: `juegos/administracion/` y `juegos/derecho/` como **proyectos Unity independientes** dentro del mismo repositorio. Posponer paquete compartido `ArcadeCore` hasta que ambos funcionen; no gastar el sprint en infraestructura que no permite jugar.
- Guardar archivos `.meta`, scripts, escenas, `ProjectSettings` y assets propios. Excluir `Library/`, `Temp/`, `Logs/`, builds y cachés con `.gitignore`.

## Trabajo por jornada

| Día | Administración / Equipo A | Derecho / Equipo B |
|---|---|---|
| **1** | Escena 2D/2.5D proyectable, 4 anaqueles reubicables manualmente como entrada de respaldo, personaje con mando/teclado, compra y entrega, pedidos, lotes PEPS/UEPS | Escena isométrica low-poly, jugador y dos barras de vida, Centro de Datos, NPCs legítimos y hostiles, primer enemigo, puntuación y derrota |
| **2** | Rondas, demanda diferente por partida, límite de atrasos, récord local, y **adaptador de webcam/ArUco de cuatro fichas si existe el hardware listo**; calibración mínima de pantalla/proyector | Completar espada, arco ARCO y escudo, cinco variantes sencillas de enemigos, oleadas progresivas, powerups aleatorios y probabilidad mayor por combo, récord y reinicio |
| **Cierre** | Prueba de un pedido completo, incumplimiento PEPS/UEPS, reposición, cambio de demanda y derrota; evidenciar si el tracking físico funciona | Prueba NPC sano llega al centro, enemigo lo intercepta, ambas barras de vida llegan a cero en casos independientes, tres armas, powerups, oleadas y récord |

## Prototipo Derecho — mínimos comprobables

**Fuente normativa de juego:** `14-derecho-firewall-de-privacidad.md`. Se conservan las reglas fundamentales:

1. Vista isométrica, guardián controlable con teclado y, si está disponible, mando; mapa con centro y obstáculos.
2. NPCs buenos caminan al Centro de Datos y suman puntos al llegar. Si los matan enemigos, **restan puntos**.
3. Amenazas malas dañan centro o guardián; el final es inmediato si **cualquiera de los dos** se queda sin vida.
4. Tres acciones jugables distintas: **espada cuerpo a cuerpo**, **arco a distancia**, **escudo que bloquea** y abre contraataque.
5. Cinco variantes de comportamiento preferentemente con **primitivas y scripts compartidos**, no cinco modelos 3D: intruso básico, enemigo remoto, blindado vulnerable tras bloqueo, interceptador y saboteador.
6. Enemigo que golpea el guardián vs. centro elige objetivo cercano salvo hostiles de objetivo específico; si el jugador golpea un enemigo de doble objetivo, éste cambia su aggro temporalmente hacia el jugador.
7. Oleadas indefinidas progresivas con dificultad creciente, puntuación acumulada y récord persistente local.
8. Drops aleatorios **también sin combo**; la racha **aumenta probabilidad, nunca garantiza**. Incluir al menos curación que restaura **ambas** vidas simultáneamente (límite al máximo). Otros powerups pueden ser variaciones sencillas de velocidad/defensa/daño.
9. Interfaz mínima: vida guardián, vida centro, puntos, oleada, combo y fin de partida; botón de reiniciar.
10. Textos breves que mantengan sentido de **protección de datos personales**, no juego de hackeo. Las denominaciones de las armas son metáforas, no la función jurídica literal de ARCO.

**Valores iniciales sugeridos para pruebas, NO decisiones cerradas:** 5 unidades de vida guardián y 10 centro; daño básico 1; NPC bueno +10 puntos y perdido −5; salud de ambos +2 por powerup; oleadas de 20–30 s. Ajustar por jugabilidad.

**Recortes autorizados en 48h:** primitivas en lugar de personajes finales, iconos/textos planos, caminos prefijados en vez de navegación avanzada, sin audio final, sin multiplayer ni tienda. **No recortar** dos vidas, NPCs buenos, 3 herramientas, enemigos, drops por probabilidad, game-over y récord.

## Prototipo Administración — mínimos comprobables

**Fuente de juego:** `11-administracion-almacen-al-limite-hibrido.md`. Se conservan las reglas fundamentales:

1. Mapa de almacén simple en pantalla (proyectable): **únicamente cuatro anaqueles representan piezas físicas**; el resto del escenario (personaje, compra, entrega, mercancías, HUD) es **digital**.
2. Cada anaquel aloja **un tipo de producto**; ubicación y **orientación** del anaquel cambian puntos de acceso y colisiones del mapa.
3. Personaje con control convencional; puede ir al punto de compra/reposición, al anaquel y al punto de entrega.
4. Cada producto tiene lotes etiquetados por orden de ingreso: **PEPS** despacha primero lotes antiguos y usa entrada posterior/salida frontal; **UEPS** despacha primero el lote más reciente. Mostrar regla y lote seleccionado.
5. Si el jugador extrae desde cara incorrecta o viola orden de lotes, penalizar puntos; confirmar la regla UEPS de orientación antes de declarar detalle didáctico final.
6. Cada partida genera **nuevo patrón de demanda entre los cuatro productos**, no constante. Rondas progresivas con más pedidos/velocidad y oportunidad de reacomodar racks **entre rondas** como solución temporal del MVP.
7. La partida se pierde cuando una **cola o atraso de pedidos supera límite visible**. Puntuación por pedidos correctos y eficiencia, récord guardado en local.
8. **Camino rápido del hardware real**: primero aceptar 4 posiciones/orientaciones editadas con ratón (respaldo verificable); crear interfaz `RackPoseInput` y adaptador de **webcam + ArUco** para las fichas reales, transformación cámara→escenario, congelar última pose estable si se pierde tracking. Probar frente a un proyector **si ambos equipos están disponibles**.
9. Cada rack debe tener ID inequívoco. No duplicar racks ni representar físicamente cajas, puntos de compra, entrega o personaje.

**Valores iniciales sugeridos para pruebas, NO decisiones cerradas:** cuatro categorías A/B/C/D, dos lotes por categoría, pedido nuevo cada 12 s al inicio y más rápido por ronda, derrota por 5 pedidos atrasados, +10 por pedido correctamente entregado. Cambiar con pruebas.

**Recortes autorizados en 48h:** modelos 2D simples, un solo mapa, una sola animación, sin economía de dinero, sin interfaz avanzada, calibración manual de proyector. **El reconocimiento de cuatro marcadores y la calibración física son de alto riesgo**; si no funcionan, registrar explícitamente el límite y mantener modo manual jugable, **no presentar el modo manual como prueba de videomapping físico**.

## Instrucciones de ejecución para Codex

1. Leer ficha específica `11` o `14` y este sprint `20`. No modificar otros juegos.
2. Crear proyecto en ruta aislada indicada; fijar versión exacta de Unity LTS compatible con el equipo al instalar, sin introducir paquetes no probados. Registrar `README` propio.
3. Entregar una **vertical slice** primero: jugar una acción completa y llegar al resultado. No implementar toda la arquitectura antes de conseguir una escena operativa.
4. Compilar/ejecutar/validar en el hardware local y anotar qué pruebas **sí** se realizaron; no afirmar pruebas físicas que no existieron.
5. Confirmar que `git status` no incluya cachés de Unity, credenciales ni video de cámara.
6. Abrir un PR por proyecto. No trabajar directamente en `main` desde dos equipos; sólo integrar PRs revisados.
7. **Si la unidad bloquea por instalación o hardware**, entregar prototipo manual con evidencia y lista de bloqueos, no sustituir silenciosamente las reglas aprobadas por otra jugabilidad.

## Checklist de aceptación a las 48 horas

- [ ] Derecho: escena que arranca, jugador se mueve, protege NPC, tres herramientas usables, amenaza ataca.
- [ ] Derecho: guardián y centro pueden perder por separado, oleadas y récord/guardar/reiniciar.
- [ ] Derecho: cinco variantes simples, powerups aleatorios y racha modifica probabilidad.
- [ ] Administración: escena que arranca, cuatro racks reubicables manualmente, compra→rack→entrega.
- [ ] Administración: lotes PEPS y UEPS, demanda varía entre partidas, rondas y pérdida por atraso.
- [ ] Administración: prueba del adaptador de cámara y proyección documentada como funcional o pendiente, sin falsear estado.
- [ ] Ambos: instrucciones reproducibles, controles, evidencia de prueba, limitaciones y PR separado.

## Después de las 48 horas

Priorizar calibración y fiabilidad del videomapping, mejoras de arte/control de Derecho, validación académica de metáforas y método de almacenamiento, y finalmente extracción del núcleo `ArcadeCore` reutilizable. 

