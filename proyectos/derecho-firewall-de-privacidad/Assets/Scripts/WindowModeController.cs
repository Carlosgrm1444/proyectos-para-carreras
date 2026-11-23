using UnityEngine;

/// <summary>
/// Mantiene la ventana en pantalla completa y permite alternarla sin reiniciar.
/// En WebGL el navegador puede exigir una interacción del usuario para aceptar fullscreen.
/// </summary>
public sealed class WindowModeController : MonoBehaviour
{
    private void Awake()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.fullScreen = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11) ||
            (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return)))
        {
            Screen.fullScreen = !Screen.fullScreen;
            if (Screen.fullScreen) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
}

