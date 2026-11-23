# Minijuegos FCACS Nuevo Laredo

Repositorio de planificación y desarrollo de siete experiencias interactivas para presentar carreras de la Facultad de Comercio, Administración y Ciencias Sociales de Nuevo Laredo. Cada propuesta mezcla aprendizaje con partidas breves, puntuación, progresión y récords.

## Estructura

- `docs/`: documentación histórica, académica, de diseño, planeación y paquetes de MVP.
- `proyectos/`: una raíz autónoma por minijuego, preparada para que un agente empiece a trabajar sin mezclar contexto ni archivos con otro juego.

Cada carpeta en `proyectos/` contiene los archivos operativos de `agent-init`:

- `agent.md`: papel, límites y orden de trabajo del agente.
- `context.md`: alcance y fuentes vinculantes.
- `task.md`: primera tarea ejecutable.
- `skills.md`, `skills-required.md`, `skills-optional.md` y `skills/`: reglas de capacidades aplicables.
- `memory.md`: decisiones y restricciones que el agente debe conservar.

## Proyectos listos para iniciar

| Proyecto | Carpeta operativa | Diseño y paquete MVP |
|---|---|---|
| Administración — Almacén al límite | [proyecto](proyectos/administracion-almacen-al-limite/) | [diseño](docs/disenos/11-administracion-almacen-al-limite-hibrido.md) · [MVP](docs/paquetes-codex/23-codex-mvp-administracion.md) |
| Autotransporte — Carga equilibrada VR | [proyecto](proyectos/autotransporte-carga-equilibrada-vr/) | [diseño](docs/disenos/17-autotransporte-carga-equilibrada-grua-vr.md) · [MVP](docs/paquetes-codex/28-codex-mvp-autotransporte.md) |
| Comercio Exterior — Clasificador en caos VR | [proyecto](proyectos/comercio-exterior-clasificador-en-caos-vr/) | [diseño](docs/disenos/12-comercio-exterior-clasificador-en-caos-vr.md) · [MVP](docs/paquetes-codex/27-codex-mvp-comercio.md) |
| Contador Público — Flujo de caja AR | [proyecto](proyectos/contador-publico-flujo-de-caja-ar/) | [diseño](docs/disenos/13-contador-publico-flujo-caja-ar.md) · [MVP](docs/paquetes-codex/24-codex-mvp-contador.md) |
| Derecho — Firewall de privacidad | [proyecto](proyectos/derecho-firewall-de-privacidad/) | [diseño](docs/disenos/14-derecho-firewall-de-privacidad.md) · [MVP](docs/paquetes-codex/22-codex-mvp-derecho.md) |
| Psicología — Atención bajo ataque | [proyecto](proyectos/psicologia-atencion-bajo-ataque/) | [diseño](docs/disenos/16-psicologia-atencion-bajo-ataque-mediapipe.md) · [MVP](docs/paquetes-codex/25-codex-mvp-psicologia.md) |
| LIIA — Laberinto gravitatorio esférico | [proyecto](proyectos/liia-laberinto-gravitatorio-esferico/) | [diseño](docs/disenos/15-liia-laberinto-gravitatorio-esferico.md) · [MVP](docs/paquetes-codex/26-codex-mvp-liia.md) |

## Punto de partida compartido

Lee primero la [arquitectura arcade de museo](docs/arquitectura/00-arquitectura-y-criterios-de-museo.md) y el [sprint de siete prototipos](docs/planeacion/21-sprint-48h-siete-videojuegos.md). El código aún no está iniciado: los proyectos deben crear una vertical slice funcional antes de arte final o integraciones físicas complejas.

Las integraciones VR, AR, cámara, proyección y electrónica sólo cuentan como implementadas cuando se prueban con el hardware real. Los modos de teclado, ratón y monitor son respaldos imprescindibles, no sustitutos de esa validación.
