# Tareas

- [x] Crear la base de la vertical slice: escena generada, esfera, sendero exterior de dos regiones, bolita física, muros, hueco, estrella, checkpoint, meta, vidas y reinicio.
- [x] Implementar `ISphereTiltInput` con teclado; controlar inclinación, nunca movimiento directo de la bolita.
- [x] Implementar la restricción radial inicial y amortiguación de la bolita; validar estabilidad en Unity antes de ajustar balance.
- [ ] Preparar interfaz de ESP32 serial sólo como integración opcional y documentar pruebas reales.

## Pendientes de verificación

- [x] Abrir el proyecto en Unity 2022.3.62f3 y resolver paquetes/compilar scripts en batchmode (`UNITY_EXIT=0`).
- [ ] Ejecutar Play y observar visualmente `Assets/Scenes/LIIA.unity`.
- [ ] Probar recorrido por las dos regiones, caída, checkpoint, meta y récord.
- [ ] Ajustar parámetros de física tras observar el primer playtest.
