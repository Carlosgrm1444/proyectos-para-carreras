# Publicar exportables con Git LFS

El proyecto ya contiene `.gitattributes` para tratar los ZIP de `Builds/Packages` con Git LFS. El equipo local tiene Git y Git LFS instalados, pero GitHub requiere autenticación para subir a este repositorio privado.

## Una sola vez

Configura Git Credential Manager e inicia sesión con una cuenta que tenga permiso de escritura en `Carlosgrm1444/proyectos-para-carreras`.

## Publicación

Desde la raíz del repositorio:

```powershell
powershell -ExecutionPolicy Bypass -File .\publish-git-lfs.ps1
```

El script:

- comprueba la autenticación sin guardar tokens en el proyecto;
- clona `codex/distribucion-multiplataforma`;
- sincroniza fuente, configuración y documentación, excluyendo caches;
- conserva los ZIP de Windows y WebGL;
- registra los ZIP con Git LFS;
- hace push únicamente a `codex/distribucion-multiplataforma`.

Linux se agregará automáticamente cuando exista `Builds/Packages/DerechoFirewall-Linux-x64.zip`.

