# Fase 2 — NPCs, amenazas y objetivos

## Implementado

- Solicitudes legítimas otorgan puntos al llegar al Centro.
- Los interceptadores persiguen solicitudes legítimas.
- Una solicitud eliminada resta 5 puntos.
- Intruso: amenaza general que prioriza el objetivo más cercano.
- Fuga remota: se mantiene alejada del guardián y presiona desde otra distancia.
- Impostor blindado: tiene más vida y solo queda vulnerable después de atacar.
- Interceptador: prioriza solicitudes legítimas.
- Saboteador del Centro: prioriza el Centro de Datos.
- Los arquetipos usan colores y nombres diferenciados.
- Las rutas usan navegación lateral para rodear obstáculos.

## Prueba manual

1. Ejecutar la escena y esperar varias apariciones.
2. Identificar los cinco nombres/color de amenaza.
3. Dejar llegar una solicitud: debe sumar puntos.
4. Permitir que un interceptador alcance una solicitud: debe restar puntos.
5. Confirmar que el saboteador ataca el Centro.
6. Confirmar que las amenazas generales pueden atacar al guardián o al Centro según proximidad.
7. Probar obstáculos entre cada tipo de NPC y su destino.

## Pendiente

- [ ] Validar que cada arquetipo tenga una silueta visual propia.
- [ ] Ajustar balance de velocidades y prioridades con pruebas reales.
- [ ] Añadir ataque remoto visible para Fuga remota.
- [ ] Añadir señal visual clara de ventana vulnerable del Impostor blindado.
