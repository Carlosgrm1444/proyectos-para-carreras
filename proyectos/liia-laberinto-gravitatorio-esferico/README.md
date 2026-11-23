# LIIA — Laberinto gravitatorio esférico

Vertical slice inicial para **Unity 2022.3 LTS**. El prototipo se genera al iniciar `Assets/Scenes/LIIA.unity`, usando primitivas para validar primero el control y las físicas.

## Cómo abrirlo

1. Instalar Unity 2022.3 LTS con soporte de escritorio para la plataforma elegida.
2. Abrir esta carpeta como proyecto Unity.
3. Abrir `Assets/Scenes/LIIA.unity`.
4. Pulsar Play.

Si la escena no construye el nivel automáticamente, seleccionar `LIIAPrototype` y ejecutar el menú contextual **Build LIIA prototype**.

## Controles

- Flechas o WASD: inclinar la esfera.
- Mantener pulsada una dirección para aplicar inclinación gradual.
- Combinar dos direcciones para inclinar diagonalmente.
- `R`: reiniciar el intento.

La entrada actual es teclado. No existe todavía comunicación con ESP32 ni integración con la esfera física.

## Loop implementado

- Esfera visual con caminos en el ecuador y una región superior conectada.
- Bolita con `Rigidbody`, aceleración tangencial, fricción y corrección radial.
- Hueco por discontinuidad del camino.
- Estrella de puntuación, checkpoint y meta.
- Tres vidas; caída al checkpoint; tercera caída reinicia el intento completo.
- Puntuación, cronómetro, récord local y pantalla de victoria.

## Estructura principal

- `LIIAPrototypeBootstrap.cs`: genera la escena gris y sus primitivas.
- `SphereTiltController.cs`: rota la esfera a partir de `ISphereTiltInput`.
- `SurfaceBallController.cs`: física de la bolita sobre la superficie.
- `GameFlow.cs`: vidas, estados, puntuación, reinicio y récord.
- `CollectibleStar.cs`, `Checkpoint.cs`, `Goal.cs`: eventos del nivel.
- `LIIAHUD.cs`: HUD de prueba sin assets externos.

## Estado de verificación

La máquina de desarrollo no tiene Unity 2022.3 disponible en el `PATH`, por lo que todavía no se ha ejecutado el editor, la escena ni una compilación. El siguiente paso es abrir el proyecto en Unity, corregir cualquier diferencia de API/importación y registrar aquí la evidencia de ejecución.

No se deben presentar como probadas las integraciones de ESP32, VR, proyección o esfera física.
