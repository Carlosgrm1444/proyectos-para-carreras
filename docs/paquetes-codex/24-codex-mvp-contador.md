# Paquete Codex 24 — Contador Público: Flujo de caja superviviente

**Equipo:** A. **Escena:** `Assets/Scenes/Contador.unity`. **Fuente vinculante:** [diseño de AR](../disenos/13-contador-publico-flujo-caja-ar.md).

## Resultado exigido

Juego AR de tablero con **personaje virtual que huye de enemigos**, flujos de caja representados por tuberías y válvulas. El jugador abre/cierra válvulas para recibir efectivo y cumplir obligaciones. La victoria consiste en cumplir metas financieras; derrota por liquidez insuficiente/incumplimiento crítico; **récord por menor tiempo**, nunca mayor puntaje.

## Vertical slice primero

1. Mapa cenital sobre hoja/tablero simulado, personaje, un tramo de tubería, una válvula y una obligación con plazo.
2. Jugador llega a la válvula, la gira/activa; el flujo ingresa o se corta. Pagar obligación consume efectivo. Cronómetro, victoria/derrota, reinicio y mínimo tiempo guardado.
3. Un enemigo perseguidor o patrulla bloquea paso; no se permite atacar. Tocar enemigo envía a zona segura y penaliza tiempo **como regla provisional**.

## Cuatro marcadores y mecanismo de respaldo

- **Target 1** fijo central para anclar el mapa.
- **Target 2** marcador móvil colocado en cuatro cuadrados de una cruz física para avanzar personaje arriba/abajo/izquierda/derecha.
- **Target 3** marcador rotatorio para abrir/cerrar válvulas.
- **Target 4** deliberadamente **sin uso definitivo confirmado**: reservar su identificador; MVP selecciona válvula por proximidad del personaje como simplificación marcada provisional.
- Implementar `ARInputAdapter` y `VirtualInputAdapter`: teclado/flechas y teclas de giro o mouse permiten pruebas sin cámara. **No presentar fallback como AR validada**.
- Si SDK disponible, demostrar detección simultánea de marcadores, estabilidad del ancla y giro; al perder tracking pausar control, no mover personaje erráticamente.

## Dificultad y balance inicial

Al menos 2 escenarios con metas distintas, 3 patrones de enemigos: patrulla, persecución y bloqueo. Defaults editables: saldo inicial 100, ingreso +25 por intervalo si válvula abierta, obligación 60 antes de 45 s, penalización de contacto +5 s. No afirmar realismo contable: ejemplo didáctico simplificado.

## Criterios de aceptación

- [ ] Un personaje recorre mapa, esquiva enemigos sin armas y cambia válvulas.
- [ ] Abrir válvulas altera entradas/salidas, se ve saldo y hay obligación con fecha.
- [ ] Gana cuando cumple metas; pierde si no cumple o falla el efectivo.
- [ ] Guarda **menor tiempo por escenario** sólo tras victoria.
- [ ] Se simulan por controles convencionales las funciones de tres targets.
- [ ] Cuarto target no tiene función definitiva inventada; tracking AR declarado probado o pendiente.
- [ ] SFX básicos, mute, reinicio, instrucciones.

## Reglas de ejecución en Codex

1. Leer [plan de 48 h](../planeacion/21-sprint-48h-siete-videojuegos.md) y la ficha de diseño enlazada antes de editar.
2. Trabajo en una carpeta/escena exclusiva del equipo. Dos proyectos Unity separados: `prototipos/equipo-a/` y `prototipos/equipo-b/`. Usar ramas `feat/mvp-48h-equipo-a` y `feat/mvp-48h-equipo-b`; **no** editar `main` directamente.
3. **Dos pasadas:** primero una vertical slice que se pueda jugar y finalizar; después reglas adicionales, guardado, dificultad y efectos de sonido.
4. Sin arte final: primitivas, colores y rótulos legibles. Efectos de sonido mínimos (acción/acierto, error/daño, resultado) y mute. No usar música comercial.
5. Todos los parámetros de balance se exponen como editables. Los valores de ejemplo son **provisionales**, no decisiones confirmadas del responsable.
6. Probar en editor y build sólo cuando estén disponibles. Registrar qué se probó realmente y qué hardware está pendiente. Modo teclado/ratón es respaldo, no prueba de AR/VR/cámara.
7. Guardar archivos fuente, escenas, `.meta`, `Packages`, `ProjectSettings`, instrucciones de arranque, controles y checklist; ignorar `Library`, `Temp`, logs y builds. Crear PR, sin merge automático.
