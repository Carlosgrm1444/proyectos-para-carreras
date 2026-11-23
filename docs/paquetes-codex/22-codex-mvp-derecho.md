# Paquete Codex 22 — Derecho: Firewall de privacidad

**Equipo:** B. **Escena prevista:** `Assets/Scenes/Derecho.unity`. **Fuente vinculante:** [diseño](../disenos/14-derecho-firewall-de-privacidad.md).

## Resultado exigido

Arcade isométrico en pantalla con mando/teclado, guardián y Centro de Datos con **vidas independientes**, NPCs legítimos que dan puntos al llegar al centro, amenazas que dañan a guardián/centro o eliminan solicitudes, tres herramientas, oleadas crecientes y récord local. **Game over inmediato si muere guardián O centro**. Sin final predeterminado.

## Vertical slice (hacer primero)

1. Suelo, 3 obstáculos, Centro de Datos y jugador con cámara isométrica fija y colisiones.
2. Una solicitud legítima recorre una ruta al centro; al entrar +puntos. Un enemigo trata de llegar a centro o jugador, reduce su vida.
3. UI con dos vidas, puntos y ronda; pantalla de derrota, reinicio, récord guardado.
4. Controles básicos: WASD/mando mover; ataque, disparo y bloqueo diferenciados.

## Funciones mínimas después de la slice

- **Espada Firewall** cuerpo a cuerpo, **Arco ARCO** proyectil a distancia, **Escudo del Consentimiento** bloquea y habilita contragolpe. Explicar visualmente que son metáforas: los derechos ARCO no son literalmente armas.
- Cinco variantes con primitivas: intruso directo; atacante remoto; blindado que requiere bloquear y contraatacar; interceptador rápido de NPC legítimo; saboteador del Centro. Usar un controlador configurable por arquetipo, no 5 motores diferentes.
- Solicitudes legítimas etiquetadas inequívocamente; al eliminarlas restar puntos. Enemigos y accesos maliciosos disminuyen salud del Centro. Generalistas priorizan objetivo cercano; al ser golpeados cambian temporalmente objetivo hacia jugador.
- Oleadas infinitas progresivas. Drops aleatorios incluso con combo 0; combos **suben probabilidad**, nunca la garantizan. Powerup de curación sana **ambos** sistemas de vida; también variaciones simples de daño, velocidad y defensa.
- Controles, HUD, guardado local, reinicio y sonidos cortos.

## Defaults de prueba, no balance aprobado

Vida jugador 5; Centro 10; impacto −1; NPC salvado +10; NPC perdido −5; oleadas de 25 s; drop aleatorio base 8 %, bonus por combo limitado a 20 %; curación +2 a ambos. Ajustables en Inspector.

## Criterios de aceptación

- [ ] Se puede entrar a la escena, mover el guardián y reintentar tras morir.
- [ ] Un NPC bueno llega y suma; si lo eliminan resta.
- [ ] El jugador puede morir mientras Centro vive y viceversa; ambas situaciones disparan game over.
- [ ] Tres armas tienen efectos distintos; blindado demanda escudo/contraataque.
- [ ] Cinco patrones de enemigo visibles y comprobables, oleadas que aumentan dificultad.
- [ ] Powerup puede aparecer sin combo y combo mejora sólo su probabilidad; curación afecta ambos.
- [ ] Récord se conserva al cerrar/reabrir; efectos de sonido básicos y mute.

## Reglas de ejecución en Codex

1. Leer [plan de 48 h](../planeacion/21-sprint-48h-siete-videojuegos.md) y la ficha de diseño enlazada antes de editar.
2. Trabajo en una carpeta/escena exclusiva del equipo. Dos proyectos Unity separados: `prototipos/equipo-a/` y `prototipos/equipo-b/`. Usar ramas `feat/mvp-48h-equipo-a` y `feat/mvp-48h-equipo-b`; **no** editar `main` directamente.
3. **Dos pasadas:** primero una vertical slice que se pueda jugar y finalizar; después reglas adicionales, guardado, dificultad y efectos de sonido.
4. Sin arte final: primitivas, colores y rótulos legibles. Efectos de sonido mínimos (acción/acierto, error/daño, resultado) y mute. No usar música comercial.
5. Todos los parámetros de balance se exponen como editables. Los valores de ejemplo son **provisionales**, no decisiones confirmadas del responsable.
6. Probar en editor y build sólo cuando estén disponibles. Registrar qué se probó realmente y qué hardware está pendiente. Modo teclado/ratón es respaldo, no prueba de AR/VR/cámara.
7. Guardar archivos fuente, escenas, `.meta`, `Packages`, `ProjectSettings`, instrucciones de arranque, controles y checklist; ignorar `Library`, `Temp`, logs y builds. Crear PR, sin merge automático.

