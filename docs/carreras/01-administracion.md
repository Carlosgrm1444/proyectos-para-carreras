# Licenciado en Administración: procesos convertibles en minijuegos

## Base académica

La carrera integra administración estratégica, operaciones, recursos humanos, mercadotecnia, finanzas, logística, calidad, inventarios, sistemas de información y reingeniería de procesos.

- [Documento curricular oficial](https://www.uat.edu.mx/DocumentosCurriculares2023/LICENCIADO%20EN%20ADMINISTRACION%20_RC%202023.pdf)
- [Malla curricular oficial](https://www.uat.edu.mx/MallasCurriculares2023/LICENCIADO%20EN%20ADMINISTRACION%20MC_RC%202023.pdf)

## Procesos que pueden representarse

- Planear metas y prioridades.
- Asignar personas, presupuesto y materiales.
- Coordinar compras, almacén, producción y entrega.
- Identificar cuellos de botella.
- Mantener inventario suficiente sin acumular demasiado.
- Responder a cambios de demanda.
- Revisar calidad y mejorar procesos.
- Equilibrar productividad, finanzas y bienestar del equipo.

## Qué significan estos procesos

| Idea | Proceso explicado | Ejemplo real simplificado |
|---|---|---|
| Fábrica en caos | **Administrar operaciones** es coordinar personas, materiales, máquinas y tiempos para convertir insumos en productos o servicios sin detener el flujo. | Una fábrica recibe piezas, las ensambla, revisa calidad y entrega pedidos; si pintura tarda demasiado, todo lo que sigue se acumula. |
| Crisis de recursos | **Planear y asignar recursos** es decidir dónde usar primero el dinero, personal y tiempo limitado de una organización. | Una pequeña empresa debe elegir entre comprar inventario, contratar apoyo o invertir en publicidad antes de temporada alta. |
| Almacén al límite | **Gestionar inventario** es mantener suficiente producto para atender ventas sin pagar por almacenar de más o quedarse sin existencias. | Una tienda debe reponer los artículos que más salen sin llenar su bodega de productos que nadie compra. |
| Caza de defectos | **Gestionar calidad** es revisar que un producto o servicio cumpla características antes de llegar al cliente y corregir la causa de fallas repetidas. | En una línea de producción se detectan envases mal cerrados y se ajusta la máquina que los genera. |
| Mercado impredecible | **Investigar el mercado** es observar qué necesitan las personas, qué ofrece la competencia y cómo reaccionan ante precio, producto y promoción. | Una cafetería prueba una promoción para estudiantes y compara si atrae clientes sin perder rentabilidad. |
| Equipo bajo presión | **Administrar capital humano** es organizar trabajo, capacitación, comunicación y condiciones para que las personas puedan cumplir objetivos. | Si sólo una persona sabe usar una máquina, capacitar a otra evita que la producción se detenga cuando falte. |
| Cadena sincronizada | **Coordinar cadena de suministro** es enlazar proveedores, inventario, producción y distribución para que cada etapa reciba lo necesario a tiempo. | Un proveedor entrega tela, la planta fabrica ropa y el almacén prepara pedidos; un retraso inicial afecta todo el recorrido. |
| Torre de decisiones | **Administrar estratégicamente** es comparar opciones de crecimiento considerando riesgo, costo, objetivos y efectos a largo plazo. | Una empresa evalúa abrir sucursal, comprar equipo o conservar efectivo para resistir una baja de ventas. |

## Banco de ideas

### 1. Fábrica en caos

- **Proceso:** administración de operaciones y cuellos de botella.
- **Gameplay:** uno a cuatro personajes transportan materiales entre almacén, producción, calidad y entrega.
- **Puntos:** pedidos completos, calidad y poco desperdicio; penalización por filas y productos rechazados.
- **Dificultad:** entran más pedidos, aparecen máquinas lentas y se activan dos líneas simultáneas.
- **Modos:** cooperativo por récord o dos equipos en fábricas espejo.
- **Tecnología posible:** controles, proyección sobre mesa y fichas físicas de recursos.

### 2. Crisis de recursos

- **Proceso:** planeación y asignación de presupuesto.
- **Gameplay:** el jugador corre por una empresa recogiendo monedas de presupuesto y colocándolas en personal, maquinaria, marketing o inventario antes de cada oleada.
- **Puntos:** supervivencia de la empresa, metas cumplidas y recursos no desperdiciados.
- **Dificultad:** recortes, ausencias, proveedores tardíos y eventos que obligan a reorganizar.
- **Modos:** individual contra récord o cooperación con decisiones por votación.
- **Tecnología posible:** botones físicos o fichas RFID para asignar recursos.

### 3. Almacén al límite — concepto híbrido seleccionado para planificar

- **Proceso:** gestión de inventarios, distribución de almacén y optimización de recorridos.
- **Gameplay:** un personaje controlado mediante mando convencional compra/repone y surte pedidos en un escenario con cuatro racks físicos reubicables, representados por post-its/fichas sobre una pared. La cámara reconoce su posición y orientación para que el juego actualice el mapa y las rutas.
- **Reglas:** PEPS y UEPS exclusivamente. PEPS: reponer por detrás y retirar por delante; UEPS: priorizar la última entrada, con orientación física aún por definir.
- **Configuración:** un tipo de mercancía por rack. El jugador redistribuye racks para aprovechar la demanda y acortar los desplazamientos.
- **Partidas y rondas:** la demanda relativa de los productos cambia **entre partidas**, no se fija siempre igual; **cada ronda** de una partida aumenta la dificultad.
- **Puntos y derrota:** pedidos correctos, cumplimiento de PEPS/UEPS y eficiencia generan puntaje; se pierde si hay demasiadas solicitudes atrasadas. Récord por puntos.
- **Tecnología prevista:** videomapping + cámara y marcadores físicos + controlador normal + Unity, pendiente de prueba física.
- **Definición pendiente:** confirmar cuáles elementos (personaje, mercancía y puntos de compra/entrega) aparecen proyectados y cuáles son físicos.
- **Ficha de diseño:** [Almacén al límite — interacción híbrida y requisitos preliminares](../disenos/11-administracion-almacen-al-limite-hibrido.md).

### 4. Caza de defectos

- **Proceso:** administración de calidad y mejora continua.
- **Gameplay:** objetos avanzan por una línea; el jugador inspecciona, corrige o deja pasar cada uno usando herramientas dentro del escenario.
- **Puntos:** defectos detectados, falsos rechazos evitados y combo de calidad.
- **Dificultad:** defectos más pequeños, varios criterios y mayor velocidad.
- **Modos:** individual, versus o un inspector y un operador cooperando.
- **Tecnología posible:** visión artificial para reconocer props y proyección tipo banda.

### 5. Mercado impredecible

- **Proceso:** investigación de mercado, ventas y mercadotecnia.
- **Gameplay:** el jugador mueve productos, precios y campañas en una ciudad viva; los clientes reaccionan dentro del mundo.
- **Puntos:** ventas rentables, satisfacción y pronósticos acertados.
- **Dificultad:** tendencias breves, competidores, segmentos distintos y campañas virales.
- **Modos:** dos marcas compiten o un equipo divide análisis y ejecución.
- **Tecnología posible:** mesa táctil, AR sobre productos o tarjetas de campaña.

### 6. Equipo bajo presión

- **Proceso:** recursos humanos, capacitación y liderazgo.
- **Gameplay:** personajes con habilidades diferentes deben asignarse a tareas; el jugador puede capacitar, rotar o descansar al equipo.
- **Puntos:** productividad sostenible, aprendizaje y bienestar.
- **Dificultad:** ausencias, tareas especializadas, conflictos y fatiga.
- **Modos:** cooperación asimétrica entre líder y operadores.
- **Tecnología posible:** fichas de personajes con NFC y tablero proyectado.
- **Cuidado:** no convertir personas en recursos desechables; el bienestar debe afectar la puntuación.

### 7. Cadena sincronizada

- **Proceso:** compras, proveedores, producción y suministros.
- **Gameplay:** cada jugador controla un eslabón. Si uno produce demasiado o tarde, el mundo se satura.
- **Puntos:** entregas completas, sincronización y bajo inventario detenido.
- **Dificultad:** rutas bloqueadas, proveedor incierto y pedidos urgentes.
- **Modos:** cooperativo de dos a cuatro jugadores con récord grupal.
- **Tecnología posible:** cuatro controles, botones de estación o módulos físicos conectados.

### 8. Torre de decisiones

- **Proceso:** administración estratégica y evaluación de proyectos.
- **Gameplay:** una empresa se representa como una torre. Cada inversión construye pisos, pero deuda, riesgo y operación pueden debilitarla.
- **Puntos:** crecimiento estable, liquidez, impacto y objetivos cumplidos.
- **Dificultad:** escenarios económicos, decisiones encadenadas y menos información perfecta.
- **Modos:** individual por rondas o equipos con la misma semilla.
- **Tecnología posible:** pantalla táctil, bloques físicos o proyección mapping.

## Variables de dificultad específicas

- Cantidad de pedidos activos.
- Variabilidad de demanda.
- Capacidad de almacén.
- Fatiga del equipo.
- Número de áreas que deben coordinarse.
- Presupuesto disponible.
- Frecuencia de incidentes.
- Similitud entre productos y prioridades.

## Formas de puntuar sin convertirlo en examen

- Entregas correctas consecutivas.
- Tiempo con el sistema estable.
- Recuperación después de una crisis.
- Mejora de un cuello de botella.
- Uso eficiente de presupuesto.
- Calidad y bienestar.
- Bonificación por cooperación sincronizada.

## Combinaciones posibles

- **Fábrica en caos + Caza de defectos:** producción y calidad dentro de la misma partida.
- **Almacén al límite + Mercado impredecible:** la demanda modifica inventario en tiempo real.
- **Crisis de recursos + Torre de decisiones:** versión estratégica con oleadas.
- **Cadena sincronizada + Autotransporte:** experiencia compartida entre carreras.

## Preguntas para docentes

- ¿Qué sector local conviene representar: maquiladora, transporte, comercio o servicios?
- ¿Qué decisiones distinguen a Administración de Contaduría?
- ¿Qué indicador debe tener mayor peso: productividad, calidad, finanzas o bienestar?
- ¿Qué crisis realista puede simplificarse sin deformar la profesión?

