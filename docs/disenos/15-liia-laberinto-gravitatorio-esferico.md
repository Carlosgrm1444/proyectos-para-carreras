# LIIA — Laberinto gravitatorio esférico (diseño de juego v0.2)

> **Estado:** concepto jugable seleccionado en la fase de diseño; **todavía no implementado** y pendiente de validar el sistema de visualización de la esfera física.
> **Carrera:** Licenciatura en Informática e Inteligencia Artificial (LIIA), no LTI.
> **Tecnología diferencial:** controlador físico de 4 botones hecho con electrónica, que manipula una esfera 3D con físicas.
> **Referencia:** [estación esférica y limitaciones técnicas](09-liia-esfera-interactiva.md).

## Concepto

Videojuego arcade en el que una **bolita rueda por caminos y laberintos construidos alrededor de la superficie exterior de una esfera tridimensional**. El jugador no dirige la bolita: **inclina/gira virtualmente la esfera** mediante cuatro botones físicos; la gravedad simulada, fricción, colisiones e inercia determinan el desplazamiento de la bolita. Hay que alcanzar una meta y se pueden recoger **estrellas de bonificación** en caminos alternativos, algunos más arriesgados, para obtener más puntos.

No pretender enseñar directamente un tema del plan de estudios: esta estación servirá para **demostrar una experiencia interactiva creada con programación, electrónica y simulación física**. Se puede añadir explicación breve de esas tecnologías fuera de la partida.

## Controles

| Botón electrónico | Acción pretendida |
|---|---|
| Arriba | Inclina / gira virtualmente la esfera en un eje para desplazar la bolita hacia la región superior visible |
| Abajo | Inclina / gira en sentido opuesto |
| Izquierda | Inclina / gira en el eje lateral |
| Derecha | Inclina / gira en sentido opuesto |

- **Pulsación mantenida:** rotación gradual de la esfera, no un salto angular fijo; soltar detiene la rotación aplicada por el control, no la inercia de la bolita.
- **Dos botones a la vez:** inclinación diagonal, por validar con usuarios.
- Es necesario distinguir **cámara, orientación de la esfera, orientación local del laberinto y gravedad** para que el control siga siendo predecible al pasar al otro lado de la esfera.
- Controlador por USB con microcontrolador como ESP32-S3 y botones físicos, sujeto a pruebas eléctricas, de latencia y compatibilidad con el equipo. Considerar rebote de contactos y botón de reinicio/pausa **fuera de los cuatro controles de dirección** si es necesario para exhibición.

## Laberintos y físicas

- Los caminos se modelan sobre una **superficie esférica exterior**, con muros laterales, tramos estrechos, bifurcaciones, curvas, obstáculos y zonas de caída.
- Una esfera física convencional en el mundo real no mantiene una bolita adherida a toda su superficie exterior; el videojuego debe usar **restricción de movimiento a la superficie, fuerza hacia el centro y/o gravedad tangencial controlada** para que parezca rodar por cualquier cara. Esto es una **simulación lúdica**, no una gravedad real convencional.
- Usar colisiones 3D, velocidad, aceleración, fricción y amortiguación para comunicar peso. La bolita no se puede teletransportar por botón.
- El mapa gira visualmente y la cámara debe mantener el objetivo legible; evaluar seguimiento suave de bolita y referencias de orientación para evitar mareo visual.
- Rotación y fuerzas deben probarse a través de varias regiones del globo, no sólo en un tramo frontal.

## Coleccionables y rutas

- **Estrellas doradas originales**, no copiar iconografía o assets de Mario; se ubican en rutas secundarias y zonas difíciles.
- Cada estrella suma puntos y desaparece al recogerla hasta la próxima partida.
- Puede haber caminos cortos con pocas estrellas y desvíos arriesgados con mayor recompensa.
- Los caminos deben permitir llegar a meta sin recoger todas las estrellas (decisión propuesta para evitar bloquear a principiantes).
- Si una estrella está en una zona muy peligrosa, recompensa mayor o puntuación de combo **pendiente de balance**.

## Victoria, fallos, récord y dificultad

- **Victoria:** llegar a la meta del laberinto.
- **Puntuación:** estrellas recogidas + bonificación por llegar a meta + bono de rapidez (pesos exactos pendientes).
- **Récord:** mayor puntuación de la partida **por laberinto**; registrar menor tiempo como desempate y métrica secundaria. Asegurar que dos partidas del mismo nivel sean comparables.
- **Vidas:** iniciar cada intento con **tres vidas**, tomando como base la opción planteada y aceptada en la conversación; mostrar las vidas restantes.
- **Caída/fracaso:** cuando la bolita cae del camino o entra en una trampa letal, **pierde una vida y regresa al último checkpoint activado**. Si aún no activó ninguno, regresa al inicio del laberinto.
- **Agotamiento de vidas:** al perder la última vida, **se reinicia por completo el laberinto** desde el inicio, con vidas restauradas, cronómetro/puntuación del intento reiniciados y coleccionables disponibles nuevamente.
- **Persistencia dentro del intento:** las estrellas ya recogidas no vuelven a puntuar al reaparecer en un checkpoint, para evitar recolección infinita. Sólo reaparecen al reiniciar por agotamiento de vidas o empezar una partida nueva.
- **Progresión:** varios laberintos, cada uno más complejo: caminos estrechos, agujeros, bifurcaciones, obstáculos móviles, superficies con fricción distinta y estrellas ubicadas en rutas de más riesgo.
- **Duración objetivo de exhibición:** probar 90–180 segundos por intento/escenario, ajustable según equilibrio real.
- **Accesibilidad:** no depender sólo de color, mostrar caminos y estrellas con formas distinguibles; tutorial mínimo de los cuatro botones.

## Secuencia de una partida

```text
Elegir laberinto / comienza intento
        ↓
Bolita aparece en zona inicial sobre la esfera
        ↓
Jugador pulsa 4 botones físicos → esfera 3D gira/inclina gradualmente
        ↓
Bolita rueda por gravedad simulada e inercia
        ↓
Se pueden recoger estrellas en caminos secundarios
        ↓
Evitar caídas y obstáculos; una caída resta 1 vida
        ↓
Si quedan vidas, regresar al último checkpoint sin repetir estrellas
        ↓
Si se agotan todas, reiniciar laberinto y restaurar 3 vidas
        ↓
Al llegar a la meta, puntuación = estrellas + meta + bonus de rapidez
        ↓
Se muestra récord del laberinto y opción de reintentar
```

## Fases técnicas y restricciones

1. **Juego de PC en pantalla convencional primero**: esfera y bolita 3D, un camino exterior y físicas consistentes.
2. **Integrar electrónica**: 4 botones físicos y lectura estable de entrada; dejar teclado de respaldo para pruebas.
3. **Diseño de nivel**: muros, agujeros, meta, coleccionables, cronómetro, puntuación y récord local.
4. **Validar estación física real**: la esfera del video parece reflectante, **no está demostrado que tenga capacidad de pantalla**. Probar superficie, proyección, brillo, geometría y visibilidad para espectadores antes de prometer que el videojuego se verá directamente en ella.
5. **Módulo de salida visual** sólo si la validación confirma una forma segura y legible de mostrar imágenes. Si no, pantalla próxima a la esfera con el laberinto virtual y esfera real como pieza escenográfica.
6. **Desarrollo futuro con Codex** previa autorización; documentación de hardware, proyecto, instrucciones de ejecución, pruebas y resultados.

## Criterios de aceptación del primer prototipo

1. Cuatro botones físicos controlan la rotación/inclinación virtual en los ejes esperados.
2. La bolita acelera, conserva inercia, frena por fricción y colisiona con paredes.
3. La bolita puede recorrer rutas sobre **varias regiones exteriores** del globo sin atravesarlo ni desprenderse indebidamente.
4. El jugador puede alcanzar meta de un laberinto corto.
5. Las estrellas se recogen, suman puntos y no se contabilizan doble.
6. Caer resta una vida y devuelve al último checkpoint activado (o al inicio si no existe); al agotar **tres vidas**, reinicia el laberinto completo.
7. Las estrellas recogidas antes de una caída no se pueden reclamar de nuevo desde el checkpoint, pero reaparecen en la siguiente partida completa.
8. Se calcula puntuación y se guarda récord local, con tiempo como desempate.
9. Si falla la conexión de botones, hay una prueba de diagnóstico y entrada de teclado de respaldo.
10. No se afirma que la esfera física sirva como pantalla hasta contar con evidencia experimental.

## Pendientes

- Confirmar puntos exactos por estrella y bono por tiempo.
- Número de niveles y selección aleatoria o fija de laberintos.
- Geometría, escala, interfaz de cámara y dimensiones del controlador.
- Datos reales de la esfera física y prueba de visualización.

**Decisión registrada:** LIIA queda con el concepto **Laberinto gravitatorio esférico**, caminos sobre superficie exterior, controlador de cuatro botones, estrellas coleccionables y puntuación con récord. **Cada caída consume una de tres vidas y devuelve al último checkpoint; al perder la última vida se reinicia el laberinto completo.**

