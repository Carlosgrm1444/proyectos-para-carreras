#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
BUILD_DIR="${SCRIPT_DIR}/../../Builds/LIIA-Linux"
exec "${BUILD_DIR}/LIIA" -screen-fullscreen 1 -screen-width 1920 -screen-height 1080 -logFile "${HOME}/.local/state/liia-player.log"
