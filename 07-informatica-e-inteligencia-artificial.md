# Licenciado en Informática e Inteligencia Artificial: procesos convertibles en minijuegos

## Base académica

El programa integra programación, datos, redes, sistemas operativos, nube, desarrollo web y móvil, modelos de lenguaje, automatización, agentes inteligentes, predicción, IoT, ciberseguridad, UX y operación de servicios.

- [Documento curricular oficial](https://www.uat.edu.mx/DocumentosCurriculares2023/LICENCIADO%20EN%20INFORMATICA%20E%20INTELIGENCIA%20ARTIFICIAL%20DC-RC%202023-2026.pdf)
- [Malla curricular oficial](https://www.uat.edu.mx/MallasCurriculares2023/LICENCIADO%20EN%20INFORMATICA%20E%20INTELIGENCIA%20ARTIFICIAL%20MC-RC%202023-2026%20.pdf)

## Procesos que pueden representarse

- Recolectar, limpiar y etiquetar datos.
- Programar una secuencia lógica.
- Entrenar, probar y corregir un modelo.
- Conectar y enrutar dispositivos.
- Mantener servicios disponibles.
- Detectar y responder a incidentes.
- Automatizar tareas con agentes.
- Recibir y monitorear datos de sensores.

El juego debe mostrar que la IA aprende patrones y puede equivocarse. No debe usar rostros, emociones ni datos personales.

## Qué significan estos procesos

| Idea | Proceso explicado | Ejemplo real simplificado |
|---|---|---|
| Arena de datos | **Preparar datos** es revisar, limpiar, organizar y etiquetar información antes de usarla en un sistema o análisis. | Una tabla de ventas con fechas duplicadas, nombres distintos para el mismo producto y campos vacíos produce resultados poco confiables. |
| Entrena al robot | **Entrenar y probar un modelo** es mostrar ejemplos etiquetados, medir cómo responde ante casos nuevos y corregir datos o reglas cuando falla. | Si un robot sólo vio botellas azules, puede equivocarse al recibir una botella verde aunque siga siendo botella. |
| Debug Runner | **Depurar** es localizar por qué un programa no se comporta como se espera, observar variables y corregir instrucciones. | Una app no muestra resultados porque una condición nunca se cumple; se revisa paso a paso hasta encontrarla. |
| Red enrutada | **Administrar redes** es conectar dispositivos y dirigir datos por rutas disponibles, rápidas y seguras. | Si un enlace se satura, los paquetes pueden tomar otra ruta para llegar al servicio. |
| Defensa cibernética | **Responder a incidentes** es detectar señales de riesgo, contener el problema, recuperar servicios y aprender para evitar repetición. | Una cuenta tiene actividad extraña; se limita el acceso, se revisan registros y se restablece de forma segura. |
| Nube en equilibrio | **Operar servicios en la nube** es distribuir recursos de cómputo para que una aplicación siga disponible cuando cambia la demanda. | Una tienda en línea recibe muchos visitantes durante una promoción y necesita más capacidad temporalmente. |
| Fábrica de agentes | **Automatizar con agentes** es diseñar sistemas que observan, deciden y actúan dentro de límites, con verificación humana o reglas. | Un agente organiza solicitudes repetitivas, pero debe pedir confirmación antes de modificar información importante. |
| Planta IoT | **Trabajar con IoT** es conectar sensores y dispositivos para medir, transmitir y usar datos del entorno. | Un sensor de temperatura avisa que una cámara fría se está calentando antes de que el producto se dañe. |

## Banco de ideas

### 1. Arena de datos

- **Proceso:** limpieza, clasificación y preparación de datos.
- **Gameplay:** el jugador mueve paquetes de datos a estaciones de limpiar, completar, etiquetar o descartar.
- **Herramientas:** escáner para duplicados, reparador para campos faltantes y etiquetadora; cada una consume tiempo o energía.
- **Puntos:** datos útiles preparados, errores corregidos y combo de lotes.
- **Dificultad:** mayor volumen, formatos distintos, duplicados sutiles y ruido.
- **Derrota:** el búfer se llena o demasiados datos erróneos llegan al modelo.
- **Modos:** supervivencia individual o ingeniero y verificador cooperando.
- **Tecnología posible:** pantalla, controles y paquetes físicos con RFID.

### 2. Entrena al robot

- **Proceso:** recolectar ejemplos, etiquetar, entrenar, probar y mejorar.
- **Gameplay:** mostrar objetos a una cámara, etiquetarlos y después observar cómo un robot los separa.
- **Puntos:** diversidad de datos, desempeño en objetos nuevos y manejo de baja confianza.
- **Dificultad:** objetos girados, luz distinta, clases parecidas y ejemplos sesgados.
- **Derrota:** demasiados errores bloquean la fábrica.
- **Modos:** cooperación entre recolector, entrenador y auditor; versus con conjuntos equivalentes.
- **Tecnología posible:** OpenCV, Unity Sentis, objetos 3D y banda con servo.

### 3. Debug Runner

- **Proceso:** localizar y corregir errores de programación.
- **Gameplay:** un avatar corre dentro de un programa; las instrucciones forman plataformas y los bugs rompen el camino.
- **Herramientas:** depurador que pausa, lupa que muestra variables y parche que corrige una instrucción.
- **Puntos:** bugs corregidos, ejecución completada y pocas pistas.
- **Dificultad:** bucles, condiciones, eventos simultáneos y errores intermitentes.
- **Derrota:** se agota el tiempo de ejecución o el programa cae demasiadas veces.
- **Modos:** individual, relevo o programador y tester cooperando.
- **Tecnología posible:** controles y bloques físicos de código.

### 4. Red enrutada

- **Proceso:** conectar dispositivos y dirigir tráfico.
- **Gameplay:** paquetes recorren una ciudad digital; el jugador coloca rutas, switches y prioridades para que lleguen.
- **Puntos:** paquetes entregados, latencia baja y red estable.
- **Dificultad:** tráfico creciente, enlaces caídos, destinos nuevos y congestión.
- **Derrota:** demasiados paquetes expirados o servicios desconectados.
- **Modos:** tower defense individual o dos administradores de red.
- **Tecnología posible:** mesa proyectada, cables físicos y nodos iluminados.

### 5. Defensa cibernética

- **Proceso:** detectar, contener y recuperar incidentes.
- **Gameplay:** amenazas atacan servicios representados como torres; el jugador configura permisos, parches y respaldos.
- **Puntos:** servicios protegidos, ataques contenidos y falsos bloqueos evitados.
- **Dificultad:** más vectores, ataques combinados, recursos limitados y señuelos.
- **Derrota:** se comprometen servicios críticos.
- **Modos:** supervivencia, versus atacante-defensor educativo o equipo de respuesta.
- **Tecnología posible:** pantalla, botones físicos y mural de red.
- **Cuidado:** no incluir instrucciones que faciliten ataques reales.

### 6. Nube en equilibrio

- **Proceso:** desplegar, escalar y mantener servicios.
- **Gameplay:** usuarios llegan por oleadas; el jugador distribuye contenedores, memoria y cómputo para mantener una aplicación.
- **Puntos:** solicitudes atendidas, costo bajo y disponibilidad.
- **Dificultad:** picos de demanda, fallas regionales, servicios dependientes y presupuesto limitado.
- **Derrota:** la aplicación permanece caída demasiado tiempo.
- **Modos:** individual o operaciones y arquitectura cooperando.
- **Tecnología posible:** simulación visual; Docker real sólo para demostración técnica separada.

### 7. Fábrica de agentes

- **Proceso:** diseñar automatizaciones con objetivos, herramientas y verificación.
- **Gameplay:** construir agentes colocando módulos de observar, decidir, actuar y comprobar; luego se envían a resolver un escenario.
- **Puntos:** tareas completas, pasos eficientes y errores detectados antes de actuar.
- **Dificultad:** instrucciones ambiguas, herramientas limitadas y condiciones cambiantes.
- **Derrota:** el agente repite acciones, consume recursos o ejecuta una acción no verificada.
- **Modos:** cooperación entre diseñador y auditor.
- **Tecnología posible:** reglas visuales, modelo local opcional y bloques físicos.
- **Cuidado:** enseñar supervisión, no presentar al agente como infalible.

### 8. Planta IoT

- **Proceso:** sensores, telemetría y mantenimiento predictivo.
- **Gameplay:** máquinas físicas y digitales envían datos; el jugador conecta sensores, interpreta alertas y atiende fallas.
- **Puntos:** producción estable, fallas prevenidas y energía eficiente.
- **Dificultad:** sensores ruidosos, conexiones caídas, varias máquinas y alertas falsas.
- **Derrota:** demasiadas máquinas se detienen o el sistema toma decisiones con datos incorrectos.
- **Modos:** técnico y analista cooperando o dos plantas compitiendo.
- **Tecnología posible:** ESP32, sensores reales, luces y gemelo digital en Unity.

## Variables de dificultad específicas

- Volumen y ruido de datos.
- Similitud entre clases.
- Cantidad de errores.
- Tráfico y latencia.
- Recursos de cómputo.
- Servicios dependientes.
- Ataques o fallas simultáneas.
- Calidad de sensores.

## Formas de puntuar

- Datos correctos y diversos.
- Errores detectados.
- Servicios disponibles.
- Paquetes entregados.
- Ataques contenidos sin bloquear usuarios legítimos.
- Modelos que reconocen incertidumbre.
- Automatizaciones verificadas.
- Combo por resolver cadenas completas.

## Combinaciones posibles

- **Arena de datos + Entrena al robot:** preparar datos y probar el modelo.
- **Red enrutada + Defensa cibernética:** construir y proteger.
- **Nube en equilibrio + Fábrica de agentes:** operar una plataforma automatizada.
- **Planta IoT + Autotransporte:** monitorear una flotilla o cadena de frío.

## Estación esférica (propuesta exploratoria de LIIA)

La esfera mostrada en el video de referencia está reservada como **posible soporte central de una experiencia de Informática e Inteligencia Artificial**, sin suponer que pueda proyectar o reproducir gráficos. La compatibilidad técnica sigue pendiente de validar.

La [ficha de la esfera](09-liia-esfera-interactiva.md) contempla opciones como **Red planetaria**, **Nube en equilibrio**, **Entrena al planeta**, **Pulso IoT** y defensa de sistemas. *Red planetaria* es la primera opción recomendada para comparar prototipos; **aún no hay minijuego elegido**, ni programación autorizada. La estación no constituye otra carrera.

## Preguntas para docentes

- ¿Qué debe representar primero la nueva carrera: IA, programación, datos, nube o ciberseguridad?
- ¿Qué modelo pequeño puede ejecutarse localmente?
- ¿Qué error de datos comprendería mejor el público?
- ¿Qué partes deben ser físicamente reales y cuáles pueden ser simuladas?

