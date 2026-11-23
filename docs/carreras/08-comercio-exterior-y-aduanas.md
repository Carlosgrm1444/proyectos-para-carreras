# Comercio Exterior y Aduanas

## Base académica

Este programa nuevo se ofrece en modalidad mixta y periodicidad cuatrimestral. Incluye Sistema Armonizado, merceología, química arancelaria, clasificación avanzada, regímenes, despacho, documentación, restricciones no arancelarias, tráfico internacional, fiscalización, sistemas electrónicos aduaneros y operaciones de industria maquiladora.

- [Documento curricular oficial](https://www.uat.edu.mx/DocumentosCurriculares2023/COMERCIO%20EXTERIOR%20Y%20ADUANAS%20DC-RC%202023-2026.pdf)
- [Malla curricular oficial](https://www.uat.edu.mx/MallasCurriculares2023/COMERCIO%20EXTERIOR%20Y%20ADUANAS_MC.pdf)

## Relación con Licenciado en Comercio Exterior

Las dos carreras comparten gran parte del proceso aduanero. Para no mostrar dos juegos idénticos:

- **Comercio Exterior** puede presentar el recorrido completo del embarque con *Aduana Express*.
- **Comercio Exterior y Aduanas** puede enfocarse en la investigación física de la mercancía y la justificación de su clasificación.

Si la facultad decide usar una sola estación, *Mercancía misteriosa* puede ser la primera fase de *Aduana Express*.

## Aprendizaje que debe llevarse el visitante

La apariencia comercial de un producto no basta para clasificarlo. Material, composición, función, grado de elaboración y presentación pueden cambiar su tratamiento. El profesional observa, documenta, compara reglas y justifica una decisión.

## Concepto recomendado: Mercancía misteriosa - Laboratorio aduanero

### Pitch

Una mercancía sin expediente llega al laboratorio. De dos a tres jugadores deben examinarla con herramientas físicas, descubrir propiedades relevantes, seguir un árbol de decisión y enviar un dictamen simplificado antes de que el embarque pierda su ventana de cruce.

El objeto puede parecer una taza común, pero el juego pregunta mediante acciones: ¿de qué material está hecho?, ¿cuál es su función principal?, ¿está terminado?, ¿forma parte de otro producto? Cada prueba desbloquea rutas dentro de un pequeño mundo de laboratorio y aduana.

### Proceso profesional simplificado

| Etapa | Acción jugable | Aprendizaje |
|---|---|---|
| Recibir muestra | Escanear objeto y etiqueta | Identidad y presentación |
| Observar | Girar, abrir o comparar piezas | Merceología |
| Probar | Usar báscula, imán, luz o ficha de composición | Propiedades físicas y química arancelaria |
| Interpretar | Distinguir material y función | Criterios de clasificación |
| Consultar | Recorrer árbol de categorías | Sistema Armonizado simplificado |
| Verificar | Revisar restricción o documento | Cumplimiento aduanero |
| Dictaminar | Colocar evidencia y justificar familia | Clasificación fundamentada |

### Bucle de juego

1. La estación selecciona uno de seis objetos.
2. El jugador de laboratorio coloca el objeto en dos instrumentos.
3. El clasificador recibe propiedades, no la respuesta.
4. El documentador busca la ficha requerida.
5. El equipo guía un avatar por el árbol de decisión.
6. Una inspección final acepta el expediente o señala qué evidencia faltó.
7. El cierre explica cuál propiedad resultó decisiva.

### Mecánicas

- Objetos físicos impresos en 3D o productos seguros y resistentes.
- Herramientas de laboratorio simuladas mediante sensores.
- Árbol de decisión convertido en caminos, compuertas y plataformas.
- Información distribuida entre roles para obligar a comunicar.
- Presión de tiempo suave; pedir una prueba adicional cuesta segundos, pero evita una mala decisión.
- Resultado con nivel de confianza y evidencia, no sólo correcto/incorrecto.

## Tecnología sugerida

### Versión viable

- Unity, pantalla y dos controles.
- Objetos físicos identificados mediante RFID o ArUco.
- Pruebas simuladas con botones grandes.

### Laboratorio físico

- Báscula con celda de carga y ESP32.
- Sensor de color o cámara con iluminación fija.
- Base magnética que detecta una ficha metálica oculta.
- NFC con metadatos de composición ficticia.
- LEDs y pantalla pequeña para resultados de prueba.

Los sensores no deben pretender analizar químicamente un producto real. Reconocen props preparados y representan el tipo de evidencia que un especialista consultaría.

### AR opcional

Una tableta fija puede mostrar capas internas, material, etiquetado y país de origen ficticio sobre cada objeto. También puede guiar al visitante hacia el instrumento correcto.

## Montaje físico

- Mesa con tres instrumentos claramente diferenciados.
- Seis objetos grandes y duplicados de repuesto.
- Cámara cenital o lectores en cada base.
- Pantalla frontal con avatar y árbol de clasificación.
- Bandeja de fichas-documento.

## MVP recomendado

- Cuatro objetos: taza, juguete, prenda y componente.
- Dos propiedades relevantes por objeto.
- Tres instrumentos simulados con botones.
- Cuatro familias educativas de clasificación.
- Un requisito documental por caso.
- Ronda de tres minutos.

La báscula real y AR se agregan sólo después de comprobar que el visitante entiende por qué examina la mercancía.

## Expansiones

- Productos compuestos cuya función principal cambia la ruta.
- Muestras similares con distinto grado de elaboración.
- Tráfico internacional: seleccionar puerto, carretera o ferrocarril después de clasificar.
- Modo experto con casos de industria maquiladora.
- Historial de dictámenes para que un segundo equipo audite al primero.

## Puntuación

- 35 % evidencia relevante obtenida.
- 30 % clasificación justificada.
- 20 % documentación y cumplimiento.
- 15 % eficiencia en número de pruebas.

Pedir todas las pruebas sin razonar no debe producir el mejor resultado.

## Ideas alternativas

### Expediente perdido

Escape room físico donde se reconstruye pedimento, origen, transporte y permisos siguiendo pistas distribuidas en una oficina aduanal.

### Semáforo de riesgo

Los jugadores configuran reglas transparentes para revisar embarques. Después observan falsos positivos y aprenden por qué automatizar una inspección requiere criterio y auditoría.

### Aduana nocturna

Juego cooperativo de tráfico internacional: un jugador opera patio, otro documentos y otro cruce. Se centra en flujo y coordinación en lugar de clasificación.

## Riesgos y controles

- Validación docente de todos los casos y vocabulario.
- Categorías ficticias o de alto nivel; no presentar fracciones como asesoría vigente.
- Explicar que los sensores representan pruebas, no análisis aduanero real.
- Piezas lavables, sin bordes ni sustancias.
- Diseñar un modo combinado con Comercio Exterior para no duplicar inversión.

## Tiempo orientativo

- Prototipo digital: 5 a 8 días.
- Props con RFID/ArUco: 2 a 3 semanas.
- Laboratorio con sensores y AR: 2 a 4 semanas adicionales.

## Preguntas para docentes

- ¿Cómo desean diferenciar esta carrera de Licenciado en Comercio Exterior ante aspirantes?
- ¿Qué propiedades de mercancías pueden demostrarse de forma segura?
- ¿Qué casos ilustran mejor merceología y química arancelaria?
- ¿Conviene una estación propia o una ruta avanzada dentro de Aduana Express?

