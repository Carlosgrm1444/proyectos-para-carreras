# Paquete Codex 26 — LIIA: Laberinto gravitatorio esférico

**Equipo:** A. **Escena:** `Assets/Scenes/LIIA.unity`. **Fuente vinculante:** [ficha de esfera](15-liia-laberinto-gravitatorio-esferico.md).

## Resultado exigido

Una **bolita rueda por caminos alrededor de la superficie exterior de una esfera virtual**. El jugador rota/inclina la esfera con **cuatro direcciones**, y las físicas —inercia, aceleración, fricción, muros, caída— determinan su recorrido. Recoge estrellas originales para puntos, alcanza meta, se beneficia de checkpoints, tiene **3 vidas** y reinicia el laberinto completo al agotarlas. Récord por puntuación, tiempo como desempate.

## Vertical slice primero

1. Esfera visible con un sendero exterior que rodee al menos **dos regiones** del globo, bolita, muros y un hueco.
2. Flechas/WASD rotan la esfera virtual paulatinamente; **no mueven directamente la bolita**. Cámara con seguimiento suave y referencia de orientación.
3. Simular restricción radial/fuerza hacia centro para mantener bolita en superficie + componente gravitacional tangencial según la rotación. Responder a colisiones y evitar que atraviese o se desprenda por un fallo numérico.
4. Una estrella, un checkpoint, un agujero y una meta que suma puntos; mostrar tiempo, vidas, score y reinicio.

## Mecánicas y hardware

- Crear un solo laberinto corto realmente completable, con rutas bifurcadas: camino seguro corto y desvíos de riesgo para estrella extra. Estrellas al recoger se desactivan y no vuelven tras checkpoint, pero sí cuando **se reinicia todo el laberinto**.
- Caída = −1 vida, reaparece en último checkpoint (si no hay, salida). Si se agotan las tres vidas, **restaurar 3, resetear puntos/tiempo/estrellas y comenzar de inicio**.
- Proponer bono por llegada y rapidez sin hacer inalcanzables récords de rutas diferentes; puntuación por estrellas y meta.
- Crear `ISphereTiltInput`: teclado como fallback; ESP32 con 4 botones vía serial/USB **si está disponible**, mostrando estado de conexión. Antirrebote y control continuo mientras se mantiene pulsado.
- **La esfera física del video parece reflectante, no una pantalla confirmada**. MVP solamente en monitor salvo prueba física real; no afirmar videomapping posible.

## Defaults configurables

3 vidas (confirmado), estrella +100, llegada +200, bono hasta +100 por rapidez, inclinación máxima 20°/eje, aceleración/gravedad y damping ajustables; laberinto de 60–120 s de prueba. Salir con R para volver a menú.

## Criterios

- [ ] Los cuatro botones/teclas giran esfera, no trasladan directamente la bolita.
- [ ] Bolita conserva inercia, rebota en muro y puede recorrer senderos de diferentes caras.
- [ ] Estrella suma una vez por intento; checkpoint preserva estrellas, caída resta vida.
- [ ] La tercera caída reinicia todas las condiciones del laberinto.
- [ ] Llegar a meta calcula puntos/tiempo, resultado y récord.
- [ ] Control físico verificado con ESP32 o registrado pendiente; funcionamiento en monitor siempre.
- [ ] Sonidos de estrella, caída y meta, opción mute.

## Contrato de entrega para Codex

- Lee [el sprint completo](21-sprint-48h-siete-videojuegos.md) y la ficha de diseño antes de escribir código; respeta lo ya acordado.
- Usa escenas y scripts dentro del proyecto Unity de **tu equipo**, sin editar carpetas del otro. Rama A: `feat/mvp-48h-equipo-a`; rama B: `feat/mvp-48h-equipo-b`.
- Primero entrega una vertical slice que inicie, se juegue, finalice y reinicie; luego afina reglas y progresión.
- Todas las cifras de balance son configurables y provisionales; figuras simples, texto legible, 3–5 sonidos originales básicos y opción de silenciar, **sin arte final**.
- Implementa entrada de teclado/ratón para pruebas cuando falle hardware. El fallback NO demuestra integración real de XR, MediaPipe, AR o esfera.
- Guarda récord local, documenta plataforma, versión de Unity, controles, procedimiento de arranque, pruebas ejecutadas y pendientes en README de la escena. No se suben cachés `Library`, `Temp`, builds ni datos personales.
- Commit, push y PR; no merge a `main` sin revisión. Nunca afirmar haber ejecutado lo que sólo se escribió.
