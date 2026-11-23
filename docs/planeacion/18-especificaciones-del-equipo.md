# Especificaciones del equipo de desarrollo

> Inventario tomado el 22 de noviembre de 2026. Se omiten números de serie, nombres de usuario y otros datos personales.

## Resumen

| Componente | Especificación detectada |
|---|---|
| Sistema operativo | Ubuntu 24.04.3 LTS, Linux 7.0.0-42-generic, arquitectura x86_64 |
| Procesador | Intel Core i7-10750H a 2.60 GHz |
| CPU lógica | 6 núcleos / 12 hilos |
| Memoria | 31 GiB utilizables (aprox. 32 GB instalados) |
| GPU detectada | Intel UHD Graphics (Comet Lake-H GT2) |
| Almacenamiento | SSD NVMe WDC PC SN730 de 953.9 GB + SSD NVMe Kingston SNV2S4000G de 3.6 TB |
| Capacidad bruta total | Aproximadamente 4.55 TB |
| Virtualización | Intel VT-x disponible |

## Estado de almacenamiento

La partición que contiene el proyecto dispone de **40 GB libres de 342 GB** (88 % ocupada) al momento del registro. Es suficiente para documentación, código y prototipos pequeños, pero conviene liberar espacio antes de compilar muchos builds de Unity, guardar capturas o instalar SDKs de VR/AR, pues estos pueden ocupar varias decenas de GB.

## Herramientas verificadas

| Herramienta | Versión |
|---|---|
| Git | 2.43.0 |
| GitHub CLI | 2.45.0 |
| Visual Studio Code | 1.145.2 |
| Node.js | v25.4.0 |
| .NET SDK | 8.0.134 |

## Implicaciones para los prototipos del museo

- El procesador y la memoria permiten desarrollar en Unity, usar visión por computadora local y ejecutar herramientas de modelado o edición en paralelo con moderación.
- La única GPU expuesta por el sistema es la Intel UHD integrada. Para prototipos 2D, interfaces, cámaras web, OpenCV/MediaPipe ligeros y builds de escritorio es adecuada; no debe asumirse rendimiento suficiente para VR exigente, render 3D pesado o modelos de IA grandes en tiempo real.
- Antes de definir una estación VR, hay que confirmar si existe una GPU dedicada que no esté siendo detectada por el sistema o utilizar un equipo con GPU compatible para las pruebas y la exhibición final.
- Los SSD NVMe ofrecen capacidad útil para repositorios, assets y compilaciones; se recomienda mantener al menos 80--100 GB libres en la partición de desarrollo para evitar problemas durante builds y actualizaciones.

## Alcance de este registro

Este archivo describe el equipo desde el que se está documentando y desarrollando. No sustituye las especificaciones de la computadora de exhibición, de visores VR, cámaras, proyectores, microcontroladores o periféricos que se elijan para cada minijuego.

