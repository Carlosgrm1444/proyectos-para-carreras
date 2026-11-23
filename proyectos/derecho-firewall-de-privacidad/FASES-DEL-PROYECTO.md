# Ruta de desarrollo — Firewall de privacidad

Este documento organiza el trabajo restante desde el prototipo actual hasta una versión lista para una exhibición local. La fuente funcional es [el diseño del juego](../../docs/disenos/14-derecho-firewall-de-privacidad.md). La evidencia detallada y los faltantes reales están en [AUDITORIA-ESPECIFICACION.md](AUDITORIA-ESPECIFICACION.md).

## Estado actual

Existe una vertical slice técnica con arena, guardián, Centro de Datos, solicitudes legítimas, amenazas, combate básico, dos vidas, HUD, reinicio y obstáculos con desvío inicial de NPCs. No se considera todavía un MVP validado: faltan pruebas estables de entrada Xbox, equilibrio, contenido completo, guardado y build.

## Fase 0 — Estabilización técnica

Objetivo: obtener una escena que se pueda abrir y jugar sin errores de consola.

- [ ] Resolver definitivamente el mapeo de LT, RT y stick derecho en el control Xbox conectado; registrar los ejes reales en `InputManager.asset`.
- [ ] Confirmar que RB activa espada, RT dispara, LT sostiene escudo y ambos sticks responden de forma consistente en hardware real.
- [ ] Corregir HUD, cámara, barras de vida, proyectiles, hitboxes y navegación hasta que no generen excepciones.
- [ ] Verificar que los NPCs no atraviesen obstáculos ni queden atrapados durante al menos cinco minutos.
- [ ] Crear una lista de pruebas manuales y registrar resultado, versión de Unity y modelo del mando.

**Salida de fase:** abrir escena, pulsar Play, jugar cinco minutos con mando sin errores, reiniciar y repetir.

## Fase 1 — Bucle de combate completo

Objetivo: que las tres herramientas tengan decisiones claras y necesarias.

- [x] Espada Firewall: alcance, enfriamiento, efecto visual y daño configurables.
- [x] Arco ARCO: proyectil, alcance, cadencia, impacto y enemigo vulnerable a distancia.
- [x] Escudo del Consentimiento: bloqueo visible, duración/recuperación y contraataque breve tras bloquear.
- [ ] Separar capas y hitboxes de guardián, amenaza, solicitud, proyectil, Centro y obstáculo.
- [x] Añadir retroalimentación de impacto: destello, sonido provisional y breve invulnerabilidad del guardián.
- [ ] Evitar que armas dañen solicitudes legítimas por accidente, salvo que se apruebe explícitamente como regla.

**Salida de fase:** cada arma tiene una utilidad comprobable y el jugador puede explicar cuándo usarla.

## Fase 2 — NPCs, amenazas y objetivos

Objetivo: convertir la arena en un problema de protección y prioridades.

- [x] Solicitudes legítimas: rutas variadas, llegada al centro con puntos y pérdida con penalización.
- [x] Intruso: amenaza cercana básica.
- [x] Fuga remota: mantiene distancia y obliga a usar arco.
- [x] Impostor blindado: solo vulnerable tras bloquear un ataque anunciado.
- [x] Interceptador: prioriza solicitudes legítimas.
- [x] Saboteador: prioriza Centro de Datos.
- [ ] Agresión dinámica: enemigos generales eligen objetivo cercano y persiguen temporalmente al guardián cuando éste los golpea.
- [ ] Sustituir el desvío simple actual por rutas robustas o NavMesh si el mapa final requiere corredores complejos.

**Salida de fase:** los cinco arquetipos se distinguen visual y mecánicamente; cada uno exige una respuesta diferente.

## Fase 3 — Progresión, puntuación y supervivencia

Objetivo: transformar la prueba técnica en una partida infinita comparable.

- [x] Director de oleadas: dificultad gradual por cantidad, frecuencia, combinaciones y posición de aparición.
- [x] Puntuación: solicitud salvada, solicitud perdida, amenaza derrotada y bonus claramente diferenciados.
- [x] Racha: reglas de inicio, incremento y ruptura documentadas.
- [x] Powerups: probabilidad base mayor que cero y aumento limitado por racha; nunca drops garantizados.
- [x] Restauración de derechos: cura simultáneamente guardián y Centro hasta su máximo.
- [x] Powerups temporales de velocidad y daño; alcance queda pendiente para una iteración posterior.
- [x] Récord local persistente; mostrar récord antes y después de cada partida.
- [x] Supervivencia infinita sin victoria por temporizador y salida voluntaria con Q durante pausa.
- [x] Reinicio con mando: A o Start reinician después de game over.

**Salida de fase:** implementada técnicamente; falta validarla en una partida real de dos a cinco minutos con el mando conectado.

## Fase 4 — Experiencia de usuario y accesibilidad

Objetivo: que una persona nueva pueda jugar sin explicación extensa.

- [x] Ayuda inicial con objetivo, controles y récord visible durante la partida.
- [x] Tutorial práctico: mover, espada, arco y escudo descritos en pantalla.
- [x] HUD legible a distancia: ambas vidas, puntaje, racha, oleada y pausa.
- [x] Indicadores visuales con texto, barras y contraste alto, no solo color.
- [x] La experiencia usa exclusivamente mando Xbox; sensibilidad de sticks queda pendiente.
- [x] Pausa con Start y reinicio seguro con A/Start tras game over.

**Salida de fase:** implementada técnicamente; falta validar con una persona que no conozca el proyecto.

## Fase 5 — Arte, sonido y rendimiento

Objetivo: sustituir primitivas sin comprometer claridad ni estabilidad.

- [ ] Diseño original de guardián, Centro, solicitudes, amenazas y cinco siluetas enemigas.
- [ ] Materiales, iluminación y cámara coherentes con vista isométrica; no copiar recursos de otros videojuegos.
- [x] Efectos visuales para golpe, bloqueo, proyectil, daño, curación y derrota.
- [x] Sonidos propios generados en tiempo de ejecución: ataque, bloqueo, acierto, daño, solicitud salvada, derrota, ruptura de escudo y power-up.
- [ ] Ajuste de pools para proyectiles, enemigos y efectos; evitar instanciación excesiva.
- [ ] Medir rendimiento sostenido en la computadora de exhibición objetivo.

**Salida de fase:** 60 FPS objetivo en la computadora de exhibición, sin audio o efectos que oculten información relevante.

## Fase 6 — Validación académica y de seguridad

Objetivo: asegurar que el juego comunique protección de datos sin presentarse como asesoría jurídica.

- [ ] Docente valida nombres, metáforas y una explicación breve de cada enemigo, arma y poder.
- [ ] Revisar referencias a consentimiento, derechos ARCO, finalidad, proporcionalidad y responsabilidad.
- [x] Etiquetar el juego como simulación didáctica; no usar casos reales ni datos personales.
- [ ] Confirmar que solicitudes legítimas y amenazas son distinguibles mediante pistas jugables claras.
- [ ] Revisar lenguaje de pantalla, tutorial y créditos con la facultad.

**Salida de fase:** contenido educativo visible, coherente y con límites claros dentro del juego.

## Fase 7 — QA, build y piloto

Objetivo: entregar un ejecutable repetible y resistente para una prueba real.

- [x] Matriz de pruebas creada para cada condición de derrota, armas, enemigos, power-ups y controles.
- [ ] Pruebas de todas las armas, enemigos, powerups, rutas, oleadas, guardado y reinicio.
- [ ] Prueba de desconexión/reconexión del mando y comportamiento seguro sin dispositivo.
- [ ] Build Windows desde Unity 6 y prueba en una carpeta limpia sin Unity Editor.
- [ ] Registrar versión, hash/commit, controles, dependencias y procedimiento de arranque.
- [ ] Piloto con al menos cinco jugadores; registrar solo observaciones anónimas de usabilidad.
- [ ] Corregir bloqueos críticos y volver a ejecutar la lista completa de pruebas.

**Salida de fase:** ejecutable Windows probado, instrucciones de operación y lista conocida de limitaciones.

## Fase 8 — Preparación de exhibición

Objetivo: operar la estación de forma segura durante una muestra.

- [ ] Configurar resolución, pantalla completa, audio y arranque directo al juego.
- [ ] Confirmar ergonomía de mando, señalización, silla/alternativa accesible y supervisión.
- [x] Crear hoja de operación: encendido, inicio, reinicio, cierre y recuperación ante fallo.
- [ ] Tener copia offline del build, controles de respaldo y versión estable etiquetada.
- [ ] Verificar que no se almacenan cuentas, grabaciones ni datos personales.

**Salida de fase:** documentación de operación lista; falta generar y probar el ejecutable final en una carpeta limpia.

## Prioridad inmediata

1. Corregir los P0 de `AUDITORIA-ESPECIFICACION.md`: agresión tras golpe, Fuga remota, blindado/contraataque y reinicio con mando.
2. Incorporar contenido jurídico visible y coherente dentro del juego.
3. Ejecutar QA prolongado de controles, rutas, rendimiento, derrotas y reinicio antes de invertir en arte final.

No avanzar a la exhibición final sin validar controles, derrotas, reinicio, récord local y contenido jurídico dentro del juego.
