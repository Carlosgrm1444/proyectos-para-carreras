# Matriz de QA del MVP

Registrar fecha, versión de Unity, mando utilizado y resultado de cada prueba.

| ID | Prueba | Resultado esperado | Estado |
|---|---|---|---|
| QA-01 | Mover con stick izquierdo | El guardián se mueve y respeta los límites | Pendiente |
| QA-02 | Apuntar con stick derecho | El guardián gira hacia la dirección apuntada | Pendiente |
| QA-03 | RB | Espada visible, sonido y daño cercano | Pendiente |
| QA-04 | RT | Proyectil visible, sonido y daño a distancia | Pendiente |
| QA-05 | LT | Escudo activo, transparente, bloquea y se rompe tras 1.5 s | Pendiente |
| QA-06 | Cooldown de escudo | No puede reactivarse durante 2 s | Pendiente |
| QA-07 | Select | Muestra/oculta ayuda | Pendiente |
| QA-08 | Start | Pausa y reanuda sin avance de entidades | Pendiente |
| QA-09 | M | Silencia y reactiva sonidos | Pendiente |
| QA-10 | Solicitud verde | Llegar al Centro suma puntos y racha | Pendiente |
| QA-11 | Interceptador naranja | Prioriza y puede perder solicitudes | Pendiente |
| QA-12 | Power-up | Se recoge, muestra estado y aplica efecto | Pendiente |
| QA-13 | Oleada | La cadencia aumenta después de 20 s | Pendiente |
| QA-14 | Derrota del guardián | Game over y reinicio funcional | Pendiente |
| QA-15 | Derrota del Centro | Game over y reinicio funcional | Pendiente |
| QA-16 | Récord | El mejor puntaje persiste después de reiniciar | Pendiente |
| QA-17 | Sesión de 5 minutos | Sin errores, bloqueo ni ralentización crítica | Pendiente |
| QA-18 | Supervivencia prolongada | La partida continúa después de cinco minutos y solo termina por vida del Guardián o Centro | Pendiente |
| QA-19 | Build limpio | El ejecutable arranca desde `Main.unity` sin abrir el Editor | Aprobado: build generado y arranque confirmado |
| QA-20 | Protección de build | El script detecta Unity abierto y evita dos procesos sobre el mismo proyecto | Aprobado |
| QA-21 | Salida voluntaria | Q solo cierra el build desde pausa y guarda el récord | Pendiente |
| QA-22 | Renderizado Windows | La arena, enemigos, power-ups, Centro y HUD se renderizan sin magenta | Aprobado mediante captura del build |
| QA-23 | Agresión tras golpe | Una amenaza general golpeada prioriza temporalmente al Guardián | Falta implementar |
| QA-24 | Fuga remota | Ejecuta un ataque visible a distancia y permite anticiparlo | Falta implementar |
| QA-25 | Blindado | Solo abre vulnerabilidad después de que su golpe haya sido bloqueado | Falta corregir |
| QA-26 | Reinicio con mando | El botón anunciado en HUD reinicia desde game over | Falta alinear código/texto |
| QA-27 | Sesión larga | Navegación, memoria y FPS permanecen estables durante al menos 20 minutos | Pendiente |
| QA-28 | Contenido jurídico | Tutorial explica ARCO, consentimiento/finalidad y metáforas aprobadas | Falta implementar/validar |
