using UnityEngine;

public sealed class HudController : MonoBehaviour
{
    private GameSession session;
    private GUIStyle style;
    private GUIStyle smallStyle;
    private bool helpVisible = true;

    public void Configure(GameSession owner)
    {
        session = owner;
    }

    private void OnGUI()
    {
        if (session == null) return;
        if (style == null)
        {
            var scale = Mathf.Clamp(Screen.height / 900f, .8f, 1.25f);
            style = new GUIStyle(GUI.skin.label) { fontSize = Mathf.RoundToInt(24f * scale), alignment = TextAnchor.UpperLeft, richText = true, wordWrap = true };
            style.normal.textColor = Color.white;
            smallStyle = new GUIStyle(style) { fontSize = Mathf.RoundToInt(16f * scale), wordWrap = true };
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton6)) helpVisible = !helpVisible;
        var weapons = FindObjectOfType<GuardianWeapons>();
        var counter = weapons != null && weapons.CounterReady ? "\n<b>CONTRAATAQUE LISTO · RB</b>" : "";
        var minutes = Mathf.FloorToInt(session.TimeRemaining / 60f);
        var seconds = Mathf.FloorToInt(session.TimeRemaining % 60f);
        var message = $"FIREWALL DE PRIVACIDAD\nPuntos: {session.Score}   Mejor: {session.BestScore}   Racha: {session.Streak}   Oleada: {session.Wave}   Tiempo: {minutes:00}:{seconds:00}";
        GUI.color = new Color(.02f, .04f, .07f, .76f);
        GUI.Box(new Rect(16f, 12f, 800f, 132f), GUIContent.none);
        GUI.color = Color.white;
        GUI.Label(new Rect(24f, 20f, 760f, 160f), message, style);
        var barWidth = Mathf.Clamp(Screen.width * .20f, 245f, 340f);
        var barX = Screen.width - barWidth - 24f;
        DrawHealthBar(new Rect(barX, 24f, barWidth, 36f), session.Guardian.Health, session.Guardian.MaxHealth, new Color(1f, .75f, .08f), "GUARDIÁN");
        DrawHealthBar(new Rect(barX, 68f, barWidth, 36f), session.Center.Health, session.Center.MaxHealth, new Color(.08f, .75f, 1f), "CENTRO DE DATOS");
        DrawPowerUpStatus(barX, 114f, barWidth, weapons);
        GUI.Label(new Rect(24f, Screen.height - 58f, 900f, 28f), "Select: controles", smallStyle);
        if (helpVisible) DrawHelp();
        if (session.IsPaused) DrawPauseOverlay();
    }

    private void DrawPowerUpStatus(float x, float y, float width, GuardianWeapons weapons)
    {
        var speed = session.Guardian.SpeedBoostRemaining;
        var damage = weapons == null ? 0f : weapons.DamageBoostRemaining;
        var status = "POWER-UPS: ninguno";
        if (speed > 0f || damage > 0f)
        {
            status = "ACTIVOS: " + (speed > 0f ? $"velocidad {speed:0.0}s" : "") + (speed > 0f && damage > 0f ? " | " : "") + (damage > 0f ? $"daño {damage:0.0}s" : "");
        }
        else if (session.IsPowerUpMessageVisible) status = session.PowerUpMessage;
        if (weapons != null && weapons.ShieldCooldownRemaining > 0f)
            status += $"   |   ESCUDO: recarga {weapons.ShieldCooldownRemaining:0.0}s";
        else if (weapons != null && weapons.ShieldRemaining > 0f)
            status += $"   |   ESCUDO ACTIVO: {weapons.ShieldRemaining:0.0}s";
        GUI.color = new Color(.02f, .04f, .07f, .92f);
        GUI.Box(new Rect(x, y, width, 34f), GUIContent.none);
        GUI.color = Color.white;
        GUI.Label(new Rect(x + 8f, y + 3f, width - 16f, 28f), status, smallStyle);
    }

    private void DrawHelp()
    {
        GUI.color = new Color(.03f, .06f, .1f, .9f);
        GUI.Box(new Rect(24f, Screen.height - 238f, 500f, 190f), GUIContent.none);
        GUI.color = Color.white;
        GUI.Label(new Rect(42f, Screen.height - 225f, 466f, 170f), "OBJETIVO\nProtege el Centro y salva las solicitudes verdes.\n\nXbox: LS mover | RS apuntar | RB espada\nRT arco | LT escudo | Start pausa\nSelect oculta/muestra esta ayuda", smallStyle);
    }

    private static void DrawPauseOverlay()
    {
        GUI.color = new Color(0f, 0f, 0f, .55f);
        GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height), GUIContent.none);
        GUI.color = Color.white;
        var pauseStyle = new GUIStyle(GUI.skin.label) { fontSize = 36, alignment = TextAnchor.MiddleCenter };
        GUI.Label(new Rect(0f, Screen.height * .42f, Screen.width, 70f), "PAUSA\nStart continuar", pauseStyle);
    }

    private static void DrawHealthBar(Rect rect, int value, int maximum, Color fill, string title)
    {
        GUI.color = new Color(.03f, .03f, .03f, .9f);
        GUI.Box(rect, GUIContent.none);
        GUI.color = fill;
        var ratio = maximum <= 0 ? 0f : Mathf.Clamp01((float)value / maximum);
        GUI.DrawTexture(new Rect(rect.x + 3f, rect.y + 3f, (rect.width - 6f) * ratio, rect.height - 6f), Texture2D.whiteTexture);
        GUI.color = Color.white;
        GUI.Label(new Rect(rect.x + 8f, rect.y + 3f, rect.width - 16f, rect.height - 4f), $"{title}: {value}/{maximum}");
    }
}
