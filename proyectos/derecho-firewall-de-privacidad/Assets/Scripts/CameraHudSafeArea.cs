using UnityEngine;

[RequireComponent(typeof(Camera))]
public sealed class CameraHudSafeArea : MonoBehaviour
{
    // Coincide con la retícula virtual de 1920x1080 de ArcadeHudController.
    [SerializeField] private float hudTopUnits = 154f;
    [SerializeField] private float hudBottomUnits = 112f;
    private Camera targetCamera;
    private int lastWidth;
    private int lastHeight;

    private void Awake()
    {
        targetCamera = GetComponent<Camera>();
        ApplyViewport();
    }

    private void Update()
    {
        if (Screen.width != lastWidth || Screen.height != lastHeight) ApplyViewport();
    }

    private void ApplyViewport()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;
        if (targetCamera == null || lastHeight <= 0) return;
        var uiScale = Mathf.Min(lastWidth / 1920f, lastHeight / 1080f);
        var bottom = Mathf.Clamp01(hudBottomUnits * uiScale / lastHeight);
        var top = Mathf.Clamp01(hudTopUnits * uiScale / lastHeight);
        targetCamera.rect = new Rect(0f, bottom, 1f, Mathf.Max(.2f, 1f - top - bottom));
    }
}

