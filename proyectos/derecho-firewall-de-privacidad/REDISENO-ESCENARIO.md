# Rediseño del escenario: Firewall de Privacidad

## Cambios implementados

- Arena futurista modular con piso azul oscuro, iluminación ambiental y luz cian central.
- Centro de Datos sobre plataforma escalonada con anillo pulsante.
- Anillo defensivo incompleto alrededor del Centro, con cuatro accesos transitables.
- Coberturas asimétricas, corredores secundarios y señales rojas de entrada en los cuatro extremos.
- Barreras sólidas con colliders visibles y detalles cian/dorado no colisionables.
- Marcadores geométricos para diferenciar tipos de amenaza sin depender solo del color.
- Supervivencia infinita: ya no existe derrota por temporizador ni máximo de oleadas.
- Progresión acelerada de forma moderada: oleadas cada 20 segundos y reducción más rápida de las cadencias de aparición.
- Cámara con zona segura automática: reserva el espacio de HUD superior e inferior según la resolución para que la interfaz no cubra el mapa.

## Colisión y navegación

- `WorldCollision` se usa para Guardián, solicitudes y amenazas; bloquea paredes y permite deslizamiento lateral.
- `NpcNavigation` usa A* en una cuadrícula de celdas transitables y mantiene una ruta corta por entidad; se actualiza cuando cambia el objetivo para evitar acumulaciones en barreras.
- Los proyectiles realizan SphereCast y se destruyen al impactar una barrera sólida.
- Los spawns se mantienen en los cuatro bordes, fuera de los obstáculos.

## Pruebas realizadas

- Build Windows exitoso, sin errores CS.
- Ejecución verificada a 1280×720.
- Cámara validada a 1280×720 con HUD: el escenario queda renderizado entre las franjas de interfaz.
- Inspección visual de arena, HUD y rutas iniciales: solicitudes y amenazas aparecen fuera de muros; el Centro conserva accesos.
- Captura de referencia: `Builds/Windows/arena-redesign-validation.png`.

## Limitaciones

- La cuadrícula A* está pensada para la arena estática actual. Si se agregan paredes dinámicas, convendrá invalidar rutas al cambiar la geometría.
