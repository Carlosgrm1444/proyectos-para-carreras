# Despliegue Linux de museo

1. Copiar el proyecto/build a `/opt/liia` y dar permiso de ejecución a `deployment/linux/run-liia.sh` y `Builds/LIIA-Linux/LIIA`.
2. Probar el script manualmente con monitor conectado.
3. Copiar `liia-museum.desktop` a `~/.config/autostart/` del usuario de exhibición.
4. Reiniciar sesión y confirmar arranque en pantalla completa.
5. Mantener una copia de respaldo del build validado y su commit asociado en un medio local.

Si el monitor no es 1920×1080, Unity conservará el modo pantalla completa y adaptará la interfaz. El archivo de log queda en `~/.local/state/liia-player.log`.
