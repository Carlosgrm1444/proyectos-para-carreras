# Instrucciones de trabajo para agentes

## Organización

El repositorio contiene siete proyectos independientes bajo `proyectos/`. Antes de editar uno, abre primero sus archivos `agent.md`, `context.md`, `task.md`, `skills.md` y `memory.md`. Las fuentes de diseño correspondientes viven en `docs/` y prevalecen sobre supuestos nuevos.

No mezcles escenas, dependencias, assets o decisiones de un juego con otro. Cada proyecto podrá contener su propio proyecto Unity cuando inicie la implementación.

## Flujo obligatorio

1. Empezar por la vertical slice indicada en `task.md`.
2. Mantener valores de balance configurables y marcados como provisionales salvo que el diseño los confirme.
3. Probar el loop: iniciar, controlar, obtener consecuencia, ganar o perder, reiniciar y conservar récord cuando aplique.
4. Documentar controles, versión de Unity, pruebas ejecutadas, evidencia y bloqueos dentro de la carpeta del proyecto.
5. No declarar una integración física como validada sin prueba real.

## Reglas de seguridad y repositorio

- No subir `Library/`, `Temp/`, cachés, builds, credenciales, grabaciones de cámara ni datos de visitantes.
- Los juegos con cámara deben procesar sin almacenar imágenes o video. Psicología no es una prueba clínica.
- Las categorías de Comercio Exterior son didácticas mientras no las valide un docente; el juego de Derecho no brinda asesoría jurídica.
- Usar ramas por trabajo y PR/revisión antes de fusionar cambios de implementación a `main`.

