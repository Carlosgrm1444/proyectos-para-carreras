# Controlador físico LIIA

Firmware para un **ESP32-S3 DevKitC-1** y cuatro botones momentáneos. Se compila con PlatformIO; no se requiere Wi‑Fi ni Bluetooth.

| Acción | GPIO | Cableado |
|---|---:|---|
| Arriba | 4 | botón entre GPIO 4 y GND |
| Abajo | 5 | botón entre GPIO 5 y GND |
| Izquierda | 6 | botón entre GPIO 6 y GND |
| Derecha | 7 | botón entre GPIO 7 y GND |

Usa `INPUT_PULLUP`: no conectar los botones a 5 V. Todos comparten GND. El firmware lee a 100 Hz, aplica 20 ms de antirrebote y transmite a 50 Hz.

## Protocolo USB serial

- Arranque o respuesta a `PING`: `HELLO,LIIA,1`
- Estado: `BTN,<secuencia>,<arriba>,<abajo>,<izquierda>,<derecha>`
- Cada botón vale `0` o `1`; el salto de línea es obligatorio.

## Carga y prueba

1. Conectar por USB de datos.
2. Desde esta carpeta: `pio run --target upload`.
3. Abrir `pio device monitor` a 115200 baudios.
4. Pulsar cada botón y verificar los cambios `BTN`.
5. Iniciar el juego, abrir diagnóstico con `F3` y confirmar puerto, secuencia y fuente `ESP32`.

Si el juego no reconoce el dispositivo, usar teclado; verificar cable, permisos del puerto y el monitor serial antes de reiniciar la estación.
