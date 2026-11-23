# Fase 1 — Bucle de combate completo

## Implementado

- Espada con alcance, daño y enfriamiento.
- Arco con proyectiles y cadencia limitada.
- Escudo visible mientras se mantiene LT.
- Ataque bloqueado prepara un contraataque de espada con daño adicional.
- Amenazas con hitbox y vida propia.
- Destello blanco al recibir daño.
- Invulnerabilidad breve del guardián después de un impacto.
- Las armas solo buscan amenazas; no dañan solicitudes legítimas.

## Prueba manual

1. Ejecutar `Main.unity`.
2. Acercarse a una amenaza y pulsar RB: debe recibir daño y mostrar destello.
3. Mantener LT cuando la amenaza golpee: la vida del guardián no debe bajar.
4. Soltar LT y pulsar RB: debe aparecer el contraataque reforzado.
5. Apuntar con el stick derecho y mantener RT: deben salir proyectiles en esa dirección.
6. Confirmar que los proyectiles dañan amenazas y no solicitudes verdes.
7. Verificar que una amenaza derrotada desaparece y suma puntos.
8. Recibir daño dos veces rápidamente: el segundo impacto dentro de 0.35 s debe ignorarse.

## Pendiente para cerrar la fase

- [ ] Probar en Unity con el mando físico durante una sesión completa.
- [ ] Confirmar que las capas de colisión del proyecto no producen daño accidental entre hitboxes.
- [x] Añadir sonido provisional de espada, impacto, bloqueo y disparo.
