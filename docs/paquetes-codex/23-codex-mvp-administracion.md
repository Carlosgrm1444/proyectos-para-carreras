# Paquete Codex 23 — Administración: Almacén al límite

**Equipo:** A. **Escena:** `Assets/Scenes/Administracion.unity`. **Fuente vinculante:** [diseño](../disenos/11-administracion-almacen-al-limite-hibrido.md).

## Resultado exigido

Almacén digital 2D/2.5D proyectable con personaje controlado con mando/teclado. **Exactamente cuatro anaqueles**, cada uno de un único producto, representados físicamente mediante Post-it/ArUco cuando exista cámara; **todos los otros elementos son digitales/proyectados**. Ir a compra/reposición, rack, entrega; inventarios por lotes **PEPS/UEPS**, pedidos, demanda cambiante ENTRE partidas, rondas, retrasos que llevan a derrota y récord de puntos.

## Vertical slice primero

1. Mapa sencillo con cuatro racks A/B/C/D (posición y frente), estación de compra y entrega; jugador camina y colisiona.
2. Racks movibles/rotables en modo editor por mouse como **fallback**, actualizando obstáculos y entradas/salidas.
3. Producto A con lote antiguo y nuevo. Se recibe pedido, se retira del rack según regla, se entrega y suma punto; crear ejemplo de reposición desde compra.
4. Pantalla HUD de pedido, lote, stock, tiempo, puntos, backlog y botón de reinicio.

## Funciones MVP adicionales

- **PEPS:** extraer lote más antiguo por frente; reponer por detrás. **UEPS:** primero sale lote recién ingresado; para el prototipo, extraer por frente y reponer por detrás como convención configurable **provisional** pendiente de validación docente.
- Un solo tipo de producto por rack; mostrar lote con orden inequívoco. Error por orden/cara penaliza puntuación.
- Una unidad transportada por el jugador cada vez. Una partida genera pesos aleatorios de demanda de 4 productos; la siguiente los cambia; presión de pedidos sube en cada ronda.
- La reubicación se permite **entre rondas**, no durante manipulación/entrega, como decisión técnica provisional del sprint. Posiciones inválidas o fuera del mapa se rechazan.
- Derrota cuando backlog vencido excede límite configurable; score final y récord local.
- Abstraer `RackPoseSource`: manual desde pantalla; ArUco/webcam opcional. Para físico: identificar 4 IDs, posiciones y orientación; calibrar contra proyección, estabilizar varios frames, congelar último estado válido si pierde lectura. **Si falla hardware, el fallback manual debe seguir jugable; no afirmar que se hizo mapping real.**

## Defaults provisionales

Dos lotes iniciales por producto; nuevo pedido cada 12 segundos, acelerado por ronda; backlog máximo de 5 pedidos vencidos; acierto +10, error −5. Iniciar 3 rondas cortas de prueba, número definitivo pendiente.

## Pruebas

- [ ] Cuatro racks móviles cambian colisiones y accesos; ningún prop extra es físico.
- [ ] Pedido completo: compra/reponer → elegir lote → rack → entregar.
- [ ] PEPS usa viejo, UEPS usa nuevo y errores penalizan.
- [ ] Dos partidas distintas con perfiles de demanda diferentes.
- [ ] Rondas endurecen presión; exceso de pedidos vencidos termina partida.
- [ ] Puntos, récord, reinicio, sonido y mute.
- [ ] Cámara/4 marcadores/proyector probado o marcado claramente como pendiente; modo manual funciona.

## Reglas de ejecución en Codex

1. Leer [plan de 48 h](../planeacion/21-sprint-48h-siete-videojuegos.md) y la ficha de diseño enlazada antes de editar.
2. Trabajo en una carpeta/escena exclusiva del equipo. Dos proyectos Unity separados: `prototipos/equipo-a/` y `prototipos/equipo-b/`. Usar ramas `feat/mvp-48h-equipo-a` y `feat/mvp-48h-equipo-b`; **no** editar `main` directamente.
3. **Dos pasadas:** primero una vertical slice que se pueda jugar y finalizar; después reglas adicionales, guardado, dificultad y efectos de sonido.
4. Sin arte final: primitivas, colores y rótulos legibles. Efectos de sonido mínimos (acción/acierto, error/daño, resultado) y mute. No usar música comercial.
5. Todos los parámetros de balance se exponen como editables. Los valores de ejemplo son **provisionales**, no decisiones confirmadas del responsable.
6. Probar en editor y build sólo cuando estén disponibles. Registrar qué se probó realmente y qué hardware está pendiente. Modo teclado/ratón es respaldo, no prueba de AR/VR/cámara.
7. Guardar archivos fuente, escenas, `.meta`, `Packages`, `ProjectSettings`, instrucciones de arranque, controles y checklist; ignorar `Library`, `Temp`, logs y builds. Crear PR, sin merge automático.
