# Validación de UI

## Evidencia automática

- Build Windows: `Builds/Windows/build.log`
- Resultado esperado: `Build Successful` y `Firewall build: Succeeded`.
- Scripts principales revisados: `HudController`, `GameJuice`, `TutorialDirector`, `GameSession`.

## Matriz manual

| Prueba | Resultado esperado |
|---|---|
| Ventana 1280x720 | HUD legible; vida y power-ups no se salen de pantalla. |
| Ventana 1920x1080 | métricas, estado central y recursos quedan separados. |
| Select | muestra u oculta la ayuda sin detener la partida. |
| Inicio del juego | aparece contexto; el tiempo no avanza hasta continuar. |
| A/Start o botón CONTINUAR | avanza una guía y reanuda el juego solo al terminarla. |
| RB, RT y LT | aparece feedback breve de espada, arco y escudo. |
| Contraataque | aparece `CONTRAATAQUE LISTO · RB` y vibra el control compatible. |
| Start | pausa el juego y muestra una capa de pausa. |

## Criterio de cierre

No debe haber texto importante sobre el centro del escenario, mensajes que desaparezcan sin confirmación durante tutorial, ni errores de compilación en el build de Windows.
