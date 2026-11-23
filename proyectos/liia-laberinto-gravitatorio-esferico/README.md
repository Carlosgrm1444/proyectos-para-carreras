# LIIA — Laberinto gravitatorio esférico

Vertical slice inicial para **Unity 2022.3.62f3 LTS**. El prototipo se genera al iniciar `Assets/Scenes/LIIA.unity`, usando primitivas para validar primero el control y las físicas.

## Cómo abrirlo

1. Instalar/abrir Unity 2022.3.62f3 con soporte de escritorio para la plataforma elegida.
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

El editor está instalado en el segundo SSD, fuera del repositorio: `/media/carlos-rodriguez/nvme0n1p1/repos/personal/Unity/Hub/Editor/2022.3.62f3/Editor/Unity`. La importación y compilación batch terminaron con salida 0; la escena ya se abrió en la interfaz gráfica y falta completar la prueba manual de Play con teclado.

No se deben presentar como probadas las integraciones de ESP32, VR, proyección o esfera física.
