using System.Collections;
using UnityEngine;

public sealed class GameJuice : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 cameraBasePosition;
    private float shakeTime;
    private float shakeStrength;
    private Color flashColor;
    private float flashTime;
    private GUIStyle popupStyle;
    private string popupText;
    private Color popupColor;
    private float popupTime;
    private string actionText;
    private Color actionColor;
    private float actionTime;

    public void Configure(Camera camera)
    {
        mainCamera = camera;
        cameraBasePosition = camera != null ? camera.transform.position : Vector3.zero;
    }

    private void Update()
    {
        if (shakeTime > 0f)
        {
            shakeTime -= Time.unscaledDeltaTime;
            if (mainCamera != null)
                mainCamera.transform.position = cameraBasePosition + Random.insideUnitSphere * shakeStrength;
        }
        else if (mainCamera != null)
        {
            mainCamera.transform.position = cameraBasePosition;
        }
        flashTime = Mathf.Max(0f, flashTime - Time.unscaledDeltaTime);
        popupTime = Mathf.Max(0f, popupTime - Time.unscaledDeltaTime);
        actionTime = Mathf.Max(0f, actionTime - Time.unscaledDeltaTime);
    }

    public void Shake(float strength, float duration)
    {
        shakeStrength = Mathf.Max(shakeStrength, strength);
        shakeTime = Mathf.Max(shakeTime, duration);
    }

    public void Flash(Color color, float duration)
    {
        flashColor = color;
        flashTime = Mathf.Max(flashTime, duration);
    }

    public void Popup(string text, Color color)
    {
        popupText = text;
        popupColor = color;
        popupTime = 1.1f;
    }

    public void ActionFeedback(string text, Color color)
    {
        actionText = text;
        actionColor = color;
        actionTime = .7f;
    }

    public void Burst(Vector3 position, Color color, float scale = 1f)
    {
        var burst = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        burst.name = "JuiceBurst";
        burst.transform.position = position;
        burst.transform.localScale = Vector3.one * (.95f * scale);
        var renderer = burst.GetComponent<Renderer>();
        PrimitiveMaterial.Apply(renderer, color);
        burst.GetComponent<Collider>().enabled = false;
        StartCoroutine(ExpandAndDestroy(burst, .18f, 3.4f * scale));
    }

    private IEnumerator ExpandAndDestroy(GameObject effect, float duration, float finalScale)
    {
        var start = effect.transform.localScale.x;
        var elapsed = 0f;
        while (effect != null && elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            var ratio = Mathf.Clamp01(elapsed / duration);
            effect.transform.localScale = Vector3.one * Mathf.Lerp(start, finalScale, ratio);
            yield return null;
        }
        if (effect != null) Destroy(effect);
    }

    private void OnGUI()
    {
        if (flashTime > 0f)
        {
            GUI.color = new Color(flashColor.r, flashColor.g, flashColor.b, Mathf.Clamp01(flashTime * 3f));
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), Texture2D.whiteTexture);
            GUI.color = Color.white;
        }
        if (popupTime > 0f && !string.IsNullOrEmpty(popupText))
        {
            if (popupStyle == null) popupStyle = new GUIStyle(GUI.skin.label) { fontSize = 28, alignment = TextAnchor.MiddleCenter };
            popupStyle.normal.textColor = popupColor;
            var rise = (1.1f - popupTime) * 34f;
            GUI.color = new Color(1f, 1f, 1f, Mathf.Clamp01(popupTime * 2f));
            GUI.Label(new Rect(0f, 178f - rise, Screen.width, 50f), popupText, popupStyle);
            GUI.color = Color.white;
        }
        if (actionTime > 0f && !string.IsNullOrEmpty(actionText))
        {
            var alpha = Mathf.Clamp01(actionTime * 2.5f) * .55f;
            for (var layer = 0; layer < 3; layer++)
            {
                var thickness = Mathf.Clamp(Screen.height * (.014f + layer * .012f), 10f, 42f);
                var layerAlpha = alpha * (1f - layer * .27f);
                GUI.color = new Color(actionColor.r, actionColor.g, actionColor.b, layerAlpha);
                GUI.DrawTexture(new Rect(0f, 0f, Screen.width, thickness), Texture2D.whiteTexture);
                GUI.DrawTexture(new Rect(0f, Screen.height - thickness, Screen.width, thickness), Texture2D.whiteTexture);
                GUI.DrawTexture(new Rect(0f, 0f, thickness, Screen.height), Texture2D.whiteTexture);
                GUI.DrawTexture(new Rect(Screen.width - thickness, 0f, thickness, Screen.height), Texture2D.whiteTexture);
            }
            GUI.color = Color.white;
        }
    }
}
