# Auditoría estricta de idea, documentación e implementación

**Fecha de auditoría:** 23 de noviembre de 2026  
**Versión revisada:** Unity `6000.6.10f1`  
**Escena de build:** `Assets/Scenes/Main.unity`  
**Fuente funcional vinculante:** `docs/disenos/14-derecho-firewall-de-privacidad.md`

## Alcance y criterio

Esta auditoría compara la idea documentada con el código, la configuración, los builds y la evidencia de QA disponible. Un elemento solo se marca como terminado cuando existe evidencia directa en la implementación o una prueba registrada. Una casilla marcada en un plan no se considera evidencia por sí sola.

Estados utilizados:

- **Cumplido:** existe implementación directa y coherente con el requisito.
- **Parcial:** existe una parte, pero falta comportamiento, robustez o coherencia.
- **Falta:** el requisito no está implementado.
- **No verificado:** podría funcionar, pero falta prueba manual, hardware o validación externa.

## Resultado ejecutivo

El proyecto ya es un prototipo jugable con bucle infinito, dos vidas, cinco arquetipos visuales, tres herramientas, puntuación, racha, drops probabilísticos, récord local, tutorial, HUD, sonido, vibración, navegación y builds Windows/WebGL. Sin embargo, todavía no cumple completamente la idea documentada ni puede considerarse listo para exhibición académica final.

Los riesgos principales son:

1. El enemigo remoto no tiene un ataque a distancia real.
2. Los enemigos no cambian temporalmente su objetivo al ser golpeados por el guardián.
3. El blindado queda vulnerable después de atacar aunque el golpe no haya sido bloqueado; la regla de escudo + contraataque no está enlazada de forma estricta.
4. El contraataque queda preparado indefinidamente hasta usar la espada; no existe la ventana breve prometida.
5. La progresión se estabiliza al alcanzar las cadencias mínimas; no continúa aumentando indefinidamente.
6. El tutorial explica controles, pero casi no enseña los conceptos jurídicos que justifican el proyecto educativo.
7. La documentación afirma validaciones, tiempos y comportamientos que no coinciden con el código actual.
8. Faltan pruebas completas con mando, sesiones largas, reconexión, rendimiento y piloto.
9. Linux no se ha compilado porque falta el módulo de soporte de Unity; los paquetes no están publicados como Release.

## Trazabilidad de los 13 criterios vinculantes

| ID | Criterio del diseño | Estado | Evidencia y detalle |
|---|---|---|---|
| D-01 | Partida sin ronda final ni victoria | **Cumplido** | `GameSession` acumula `TimeElapsed` y solo termina por vida; `EntitySpawner` aumenta `Wave` sin máximo. |
| D-02 | + puntos por solicitud salvada y − puntos por solicitud perdida | **Cumplido** | `RequestSaved`: +10 y racha +1. `RequestLost`: −5 y racha a 0. |
| D-03 | Solicitud maliciosa o enemigo reduce vida del Centro | **Parcial** | Las amenazas que alcanzan el Centro hacen daño, incluido el saboteador. No existe una entidad separada de “solicitud maliciosa” con reglas y señales propias. |
| D-04 | Ataques al jugador reducen vida del guardián | **Cumplido** | `ThreatHitGuardian` llama `TryTakeDamage`; existe invulnerabilidad breve. |
| D-05 | Game over inmediato si guardián **o** Centro llega a 0 | **Cumplido** | `GameSession.Update` usa la condición `guardian.Health <= 0 || center.Health <= 0`. La pantalla indica la causa. |
| D-06 | Espada, arco y escudo distintos y necesarios; blindado exige bloqueo/contraataque | **Parcial** | Las tres herramientas existen. El blindado ignora daño fuera de vulnerabilidad, pero se vuelve vulnerable después de atacar al guardián incluso si el golpe no fue bloqueado. El arco y la espada dañan los mismos tipos; no hay vulnerabilidad exclusiva que haga necesario el arco. |
| D-07 | Objetivo cercano y cambio temporal de agresión tras recibir golpe | **Parcial** | Intruso/generalistas comparan distancia entre guardián y Centro. `Threat.TakeDamage` no registra atacante ni activa un temporizador de agresión hacia el jugador. |
| D-08 | Obstáculos y rutas no triviales | **Cumplido con riesgo** | Arena con barreras, colisión por `SphereCast` y A* en cuadrícula. Falta prueba prolongada de que ninguna entidad quede atrapada. |
| D-09 | Dificultad gradual y puntuación comparable | **Parcial** | La cadencia aumenta y desde oleadas posteriores aparecen solicitudes/amenazas adicionales. Aún se estanca la cadencia mínima y faltan escalados completos de salud, daño y rutas. Récord local sí existe. |
| D-10 | Drop base > 0 y racha solo aumenta probabilidad | **Cumplido** | Probabilidad `0.08 + min(racha × 0.02, 0.12)`, máximo 20 %, nunca garantizada. |
| D-11 | Curación restaura ambos; otros poderes temporales | **Cumplido mínimo** | Curación +2 a guardián y Centro; velocidad y daño duran 8 s. No existen todavía defensa reforzada ni alcance, ambos propuestos pero no obligatorios para el mínimo. |
| D-12 | Récord persistente y HUD con dos vidas/puntos | **Cumplido** | `PlayerPrefs`, dos barras numéricas suavizadas, puntuación, combo, oleada y tiempo. |
| D-13 | Metáforas explicables jurídicamente | **Parcial** | La introducción ya explica consentimiento, finalidad, proporcionalidad y que las armas son metáforas; falta ampliar responsabilidad, protección de las personas y un debrief final. |

## Auditoría detallada por sistema

### 1. Bucle, vidas, derrota y reinicio

- **Cumplido:** supervivencia infinita, vidas independientes, derrota por cualquiera de las dos vidas, puntuación final, oleada y tiempo sobrevivido.
- **Cumplido:** pausa con Start y reinicio con A/Start desde game over.
- **No verificado:** no existe una prueba registrada de ambas causas de derrota y reinicio repetido en build limpio.
- **Falta:** una opción documentada para restablecer el récord local si la estación requiere mantenimiento.

### 2. Controles y compatibilidad

- **Cumplido:** LS mueve, RS apunta, RB espada, RT arco, LT escudo, Start pausa y Select ayuda, según el mapeo clásico configurado.
- **Cumplido:** se respeta la limitación del eje combinado de gatillos: LT = −1, RT = +1, ambos = 0.
- **Decisión aplicada:** el juego se controla exclusivamente con mando Xbox; no se ofrece ruta de teclado o ratón.
- **Cumplido:** F11 y Alt+Enter alternan pantalla completa en el build de escritorio.
- **No verificado:** desconexión/reconexión del mando, modelo exacto, sensibilidad, dead zones y funcionamiento durante una sesión completa.
- **No verificado:** WebGL puede requerir una interacción explícita del usuario para aceptar pantalla completa por políticas del navegador.

### 3. Espada, arco, escudo y contraataque

- **Cumplido:** enfriamientos y bloqueo mutuo de acciones; no se puede atacar, disparar y escudar simultáneamente.
- **Cumplido:** escudo visible, duración 1.5 s, desvanecimiento, ruptura, efecto, sonido y cooldown de 2 s.
- **Parcial:** la espada muestra el efecto al frente, pero el daño usa una esfera completa alrededor del guardián; puede golpear amenazas detrás del personaje.
- **Parcial:** el proyectil se detiene en obstáculos y daña amenazas; aún falta hacer que algunos tipos requieran explícitamente la respuesta a distancia.
- **Cumplido:** el estado de contraataque caduca después de 1.25 s.
- **Cumplido:** el blindado solo abre vulnerabilidad cuando el impacto contra el guardián fue bloqueado por el escudo.
- **Cumplido:** las armas buscan exclusivamente amenazas y no dañan solicitudes legítimas.
- **No verificado:** capas/hitboxes dedicadas. La implementación usa distancias y búsquedas globales, no una matriz de capas separada.

### 4. Enemigos y selección de objetivos

- **Cumplido visualmente:** cinco tipos con color y marcador geométrico distintos.
- **Cumplido:** Interceptor busca la solicitud más cercana; Saboteador prioriza el Centro; generalistas comparan guardián/Centro.
- **Cumplido mínimo:** Fuga remota dispara proyectiles visibles hacia el guardián y se mantiene a distancia; falta una señal telegráfica más clara.
- **Cumplido:** las amenazas reciben una prioridad temporal hacia el guardián al ser golpeadas.
- **Parcial:** todos los tipos aparecen aleatoriamente desde el principio. No hay desbloqueo o composición dirigida por oleada, pese a que el diseño pide combinaciones progresivas.
- **Parcial:** barras de vida existen y hacen billboard, pero falta una prueba visual sistemática en todos los ángulos/resoluciones y una transición suavizada.
- **Falta:** señales telegráficas consistentes antes de ataques, especialmente blindado, remoto y saboteador.

### 5. Solicitudes y significado del objetivo

- **Cumplido:** solicitudes verdes aparecen en cuatro lados, navegan hacia el Centro, suman al llegar y pueden ser eliminadas por interceptores.
- **Parcial:** no hay variantes de solicitud legítima que representen acceso, rectificación, cancelación u oposición.
- **Falta:** no existe una solicitud maliciosa distinguible como entidad separada; toda amenaza se modela como enemigo combatible.
- **No verificado:** el juego no demuestra todavía que ninguna solicitud aparezca condenada por ruta/temporización imposible.

### 6. Oleadas y dificultad

- **Cumplido:** oleada cada 20 segundos y aceleración de solicitudes/amenazas.
- **Parcial:** la dificultad solo escala por frecuencia y deja de aumentar cuando alcanza los mínimos.
- **Falta:** escalado gradual de salud, velocidad, daño, composición, rutas o número simultáneo según oleada.
- **Falta:** reglas de presión que preserven justicia; no hay presupuesto de amenazas, límite de simultáneos ni validación de situaciones imposibles.
- **No verificado:** duración media esperada y curva de dificultad con jugadores reales.

### 7. Racha, puntuación y power-ups

- **Cumplido:** +10 solicitud, −5 pérdida, +2 amenaza; pérdida rompe racha.
- **Cumplido:** base 8 %, bonificación limitada, máximo 20 %, no garantizado.
- **Cumplido:** curación de ambos, velocidad y daño temporales.
- **Parcial:** el HUD muestra solo un estado prioritario. Velocidad y daño pueden estar activos al mismo tiempo, pero no se visualizan simultáneamente.
- **Falta opcional:** protección reforzada/defensa y alcance del arco, propuestos en la idea ampliada.
- **Falta:** política explícita de acumulación o renovación de cada efecto; el código renueva al máximo, pero no está documentado como decisión de diseño.

### 8. Tutorial y contenido educativo

- **Cumplido:** modales pausan mediante `Time.timeScale = 0` y esperan A/Start; controles y primer encuentro de enemigos/power-up se presentan una vez por partida.
- **Parcial:** espada, arco y escudo se presentan en la introducción general antes del uso, pero luego vuelven a abrirse al detectar la acción. Esto es funcional, aunque la secuencia puede interrumpir el primer combate.
- **Parcial:** la introducción explica consentimiento, finalidad y proporcionalidad; falta ampliar responsabilidad y protección de las personas con ejemplos interactivos.
- **Cumplido mínimo:** el contexto inicial explica ARCO como Acceso, Rectificación, Cancelación y Oposición y aclara que es una metáfora.
- **Falta:** analogía jurídica específica de cada enemigo en los mensajes mostrados.
- **Falta:** aviso educativo breve de que las acciones son metáforas y no asesoría jurídica, al menos en inicio/créditos o ayuda completa. No debe permanecer ocupando el escenario.
- **Falta:** cierre/debrief que conecte resultados con conceptos aprendidos.

### 9. HUD, UX y accesibilidad

- **Cumplido:** distribución superior izquierda/centro/derecha, controles abajo, barras suaves, números, alerta crítica, power-up, escudo, causa de derrota y escalado 1920×1080/1280×720.
- **Cumplido:** entidades no dependen únicamente del color; las amenazas tienen marcadores geométricos.
- **Parcial:** la ayuda completa cubre el centro como modal y no pausa. Esto puede ser aceptable como ayuda voluntaria, pero debe validarse con usuarios.
- **Parcial:** no se muestran cooldowns de espada/arco ni disponibilidad del contraataque persistentemente; solo el escudo tiene estado continuo.
- **Parcial técnico:** la UI usa IMGUI (`OnGUI`) y no Canvas/TextMeshPro. Funciona para el prototipo, pero dificulta navegación, accesibilidad, localización y pruebas automatizadas.
- **Falta:** modo accesible configurable, reasignación de controles, tamaño de texto, reducción de vibración/destellos y alternativa para sensibilidad del stick.
- **No verificado:** ultrawide, 1366×768, escalado de Windows distinto de 100 %, pantalla completa real y navegación solo con mando.

### 10. Audio, música, vibración y feedback

- **Cumplido:** efectos procedurales para acciones y eventos principales, música CC0 en loop a volumen menor y mute conjunto.
- **Cumplido en Windows:** vibración XInput con fallback silencioso en otras plataformas.
- **No verificado:** mezcla en altavoces del equipo de exhibición; ausencia de clipping, inteligibilidad y volumen seguro.
- **Parcial:** no existen controles de volumen separados para música/efectos.
- **Falta:** opción para desactivar vibración y destellos por accesibilidad.

### 11. Navegación, rendimiento y estabilidad

- **Cumplido funcional:** A* estático y colisión/deslizamiento con obstáculos.
- **Riesgo:** `NpcNavigation` mantiene un diccionario estático por `Transform` y no elimina entradas al destruir NPCs. En una partida infinita puede crecer sin límite.
- **Riesgo:** múltiples `FindObjectsOfType` por frame y por amenaza/arma/tutorial; el costo aumenta con la densidad.
- **Riesgo:** proyectiles, enemigos, barras y efectos se crean/destruyen continuamente; no hay pooling.
- **Falta:** límite de efectos simultáneos y presupuesto de entidades.
- **No verificado:** sesión larga, memoria, FPS, picos de GC y comportamiento con oleadas altas en la computadora objetivo.

### 12. Builds, distribución y operación

- **Cumplido:** build Windows generado y log con `Firewall Windows build: Succeeded`.
- **Cumplido:** build WebGL generado con `index.html`, `wasm`, `data` y framework.
- **Falta:** build Linux; el módulo `Linux Build Support` no está instalado.
- **Falta:** publicación verificable de ZIPs mediante GitHub Release o Git LFS.
- **Cumplido:** scripts de build y empaquetado, documentación de operación y hashes locales.
- **No verificado:** ejecución desde carpeta limpia, WebGL servido por HTTP/HTTPS, Linux real, permisos, fullscreen WebGL y descarga desde el repositorio.

### 13. Privacidad y contenido educativo

- **Cumplido:** no solicita cuentas, nombres, grabaciones ni expedientes; solo usa `PlayerPrefs` para récord.
- **Falta:** comprobación formal de qué claves se guardan y procedimiento de borrado.
- **Falta:** referencia legal/académica trazable para cada metáfora presentada al público.

## Inconsistencias documentales corregibles

1. `FASES-DEL-PROYECTO.md` afirmaba un límite de cinco minutos, contrario al diseño infinito y al código actual.
2. `FASE-3-CHECKLIST.md` y `QA-MATRIZ.md` hablaban de oleadas de 25 s; el código usa 20 s.
3. `PLAN-TUTORIAL-PROGRESIVO.md` decía simultáneamente que el tutorial no detiene, que pausa y que el juego continúa.
4. `VALIDACION-ACADEMICA.md` afirmaba que varios mensajes jurídicos estaban incorporados, pero `TutorialDirector` no contiene esas explicaciones.
5. `DISTRIBUCION.md` decía que WebGL seguía fallando, aunque existe un build exitoso.
6. Varios checklists indicaban `DerechoMVP.unity`; la escena de build y ejecución actual es `Main.unity`.
7. Algunos documentos antiguos indicaban A para espada; el control vigente es RB.
8. La pantalla de derrota dice A/Start, pero el reinicio real no admite Start.
9. Documentos antiguos usaban Unity `6.6.0f1`; el proyecto y build verificados usan `6000.6.10f1`.

## Backlog obligatorio recomendado

### P0 — Antes de declarar el MVP completo

- Implementar agresión temporal al guardián tras golpear una amenaza.
- Implementar proyectil/ataque remoto real y telegráfico para Fuga remota.
- Vincular la vulnerabilidad del blindado a un bloqueo exitoso y hacer caducar el contraataque.
- Corregir el reinicio con mando o el texto de A/Start.
- Evitar crecimiento indefinido del caché de navegación.
- Ejecutar QA de las dos derrotas, reinicio, armas, cinco enemigos, tres power-ups y sesión prolongada.
- Completar contenido educativo visible y coherente dentro del juego.

### P1 — Antes de exhibición pública

- Ampliar dificultad después de alcanzar las cadencias mínimas.
- Añadir telegráficos, límites de entidades y pooling básico.
- Mostrar simultáneamente todos los power-ups activos.
- Probar desconexión/reconexión, pantalla completa, audio y 1280×720/1920×1080 en hardware objetivo.
- Realizar piloto con al menos cinco participantes y registrar observaciones anónimas.
- Generar build Linux y probar WebGL servido desde un servidor real.

### P2 — Calidad y accesibilidad

- Añadir controles de volumen, vibración/destellos y sensibilidad.
- Evaluar migrar HUD de IMGUI a Canvas/TextMeshPro.
- Añadir poder de defensa/alcance si se mantiene en el alcance final.
- Sustituir primitivas por arte original sin perder siluetas ni contraste.
- Añadir debrief educativo y mecanismo documentado para borrar el récord.

## Definición de “listo”

El proyecto solo debe declararse listo cuando:

1. Los P0 estén implementados y probados.
2. Los 13 criterios vinculantes estén en estado Cumplido o tengan una exclusión aprobada y documentada.
3. La matriz QA tenga fecha, versión, hardware, responsable y evidencia.
4. El build objetivo arranque desde carpeta limpia, mantenga rendimiento estable y tenga procedimiento de recuperación.
5. La documentación, los controles mostrados y el comportamiento real coincidan.

