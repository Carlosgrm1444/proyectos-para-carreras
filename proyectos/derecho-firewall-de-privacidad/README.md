# Firewall de privacidad — prototipo arcade educativo

Micro-vertical slice del arcade de Derecho. El jugador protege un Centro de Datos y acompaña solicitudes legítimas mientras evita amenazas.

## Estado

- Prototipo funcional en Unity 6000.6.10f1; la escena se construye automáticamente.
- La escena se construye automáticamente al ejecutar `GameBootstrap`.
- Valores de balance provisionales y editables desde el Inspector.

## Controles

- Mando: stick izquierdo para mover.
- Xbox: stick izquierdo mueve, stick derecho apunta, RB espada, gatillo derecho arco, gatillo izquierdo escudo.
- Xbox: Start pausa/reanuda y Select muestra u oculta la ayuda.
- Música: loop de acción CC0 integrado.

El proyecto usa el Input Manager clásico para evitar incompatibilidades del paquete Input System con Unity 6. Los ejes del mando se configuran como `AimHorizontal`, `AimVertical`, `LeftTrigger` y `RightTrigger`.
- Reiniciar: botón A o Start del mando después de game over.

## Cómo ejecutar

1. Abrir el proyecto con Unity 6000.6.10f1.
2. Abrir `Assets/Scenes/Main.unity`.
3. La escena ya incluye un GameObject con `GameBootstrap`.
4. Pulsar Play o construir desde Build Profiles.

El bootstrap crea arena, cámara, Centro de Datos, guardián, solicitudes, amenazas, obstáculos y HUD sin assets externos.

## Pruebas pendientes

- Ejecutar con Unity Editor y completar la lista de pruebas de las fases.
- Confirmar el eje del mando conectado.
- Verificar reinicio y game over en hardware real.
- Ajustar velocidad y legibilidad después de una primera prueba.

La revisión requisito por requisito se mantiene en [AUDITORIA-ESPECIFICACION.md](AUDITORIA-ESPECIFICACION.md). Ese documento prevalece sobre casillas antiguas de los planes cuando exista una contradicción de estado.

## Alcance deliberadamente excluido

Arte final, balance definitivo, power-up de alcance y publicación final de builds quedan para las siguientes fases. La partida es de supervivencia infinita: no existe límite de duración como condición de victoria.

