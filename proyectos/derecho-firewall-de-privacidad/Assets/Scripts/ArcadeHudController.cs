using UnityEngine;

public sealed class ArcadeHudController : MonoBehaviour
{
    private static readonly Color Background = Hex("141A24");
    private static readonly Color Panel = Hex("222C3C");
    private static readonly Color Border = Hex("34445B");
    private static readonly Color Cyan = Hex("38BDF8");
    private static readonly Color Gold = Hex("FFC247");
    private static readonly Color Green = Hex("36C78A");
    private static readonly Color Red = Hex("F26B6B");
    private static readonly Color Muted = Hex("A9B8CC");

    private GameSession session;
    private GuardianWeapons weapons;
    private bool helpVisible;
    private float guardianDisplay = 1f;
    private float centerDisplay = 1f;
    private GUIStyle title;
    private GUIStyle value;
    private GUIStyle label;
    private GUIStyle small;
    private GUIStyle centered;
    private GUIStyle button;

    public void Configure(GameSession owner)
    {
        session = owner;
        weapons = FindObjectOfType<GuardianWeapons>();
    }

    private void Update()
    {
        if (session == null) return;
        if (Input.GetKeyDown(KeyCode.JoystickButton6)) helpVisible = !helpVisible;
        guardianDisplay = Mathf.MoveTowards(guardianDisplay, Ratio(session.Guardian.Health, session.Guardian.MaxHealth), Time.unscaledDeltaTime * 1.7f);
        centerDisplay = Mathf.MoveTowards(centerDisplay, Ratio(session.Center.Health, session.Center.MaxHealth), Time.unscaledDeltaTime * 1.7f);
        if (weapons == null) weapons = FindObjectOfType<GuardianWeapons>();
    }

    private void OnGUI()
    {
        if (session == null) return;
        EnsureStyles();
        var oldMatrix = GUI.matrix;
        var scale = Mathf.Min(Screen.width / 1920f, Screen.height / 1080f);
        var offsetX = (Screen.width - 1920f * scale) * .5f;
        var offsetY = (Screen.height - 1080f * scale) * .5f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(offsetX, offsetY, 0f), Quaternion.identity, new Vector3(scale, scale, 1f));

        DrawTopHud();
        DrawBottomHud();
        if (helpVisible) DrawHelp();
        if (session.IsPaused) DrawPause();
        if (session.IsGameOver) DrawGameOver();

        GUI.matrix = oldMatrix;
        GUI.color = Color.white;
    }

    private void DrawTopHud()
    {
        PanelBox(new Rect(24f, 20f, 560f, 124f));
        GUI.Label(new Rect(46f, 34f, 510f, 26f), "PUNTUACIÓN", label);
        GUI.Label(new Rect(46f, 60f, 260f, 42f), session.Score.ToString("N0"), value);
        GUI.Label(new Rect(46f, 105f, 240f, 24f), $"Récord: {session.BestScore:N0}", small);
        GUI.Label(new Rect(300f, 60f, 240f, 34f), $"COMBO  x{session.Streak}", centered);

        PanelBox(new Rect(596f, 20f, 560f, 124f));
        GUI.Label(new Rect(616f, 34f, 520f, 24f), "OLEADA", centered);
        GUI.Label(new Rect(616f, 58f, 520f, 38f), session.Wave.ToString("00"), valueCentered());
        var elapsed = session.TimeElapsed;
        GUI.Label(new Rect(616f, 100f, 520f, 24f), $"{Mathf.FloorToInt(elapsed / 60f):00}:{Mathf.FloorToInt(elapsed % 60f):00}", centered);

        PanelBox(new Rect(1168f, 20f, 728f, 124f));
        DrawBar(new Rect(1192f, 39f, 680f, 32f), guardianDisplay, Gold, $"GUARDIÁN  {session.Guardian.Health}/{session.Guardian.MaxHealth}");
        DrawBar(new Rect(1192f, 84f, 680f, 32f), centerDisplay, Cyan, $"CENTRO DE DATOS  {session.Center.Health}/{session.Center.MaxHealth}");

        if (centerDisplay <= .3f && !session.IsGameOver)
        {
            var pulse = .55f + Mathf.Sin(Time.unscaledTime * 8f) * .25f;
            GUI.color = new Color(Red.r, Red.g, Red.b, pulse);
            GUI.DrawTexture(new Rect(1168f, 20f, 728f, 5f), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(1168f, 139f, 728f, 5f), Texture2D.whiteTexture);
            GUI.color = Color.white;
            GUI.Label(new Rect(1168f, 146f, 728f, 28f), "⚠ INTEGRIDAD DEL CENTRO CRÍTICA", centered);
        }
    }

    private void DrawBottomHud()
    {
        PanelBox(new Rect(24f, 974f, 1872f, 82f));
        var controls = "[LS] Mover    [RS] Apuntar    [RB] Espada    [RT] Arco    [LT] Escudo    [START] Pausa";
        GUI.Label(new Rect(46f, 991f, 1130f, 28f), controls, small);
        GUI.Label(new Rect(46f, 1022f, 700f, 22f), "Select: ayuda completa", label);

        var status = PowerStatus();
        var color = status == "Sin power-ups" ? Muted : Green;
        GUI.Label(new Rect(1190f, 990f, 680f, 26f), status, RightStyle(color));
        var shield = weapons == null ? "ESCUDO: --" : weapons.ShieldRemaining > 0f ? $"ESCUDO ACTIVO  {weapons.ShieldRemaining:0.0}s" : weapons.ShieldCooldownRemaining > 0f ? $"ESCUDO RECARGANDO  {weapons.ShieldCooldownRemaining:0.0}s" : "ESCUDO LISTO";
        GUI.Label(new Rect(1190f, 1022f, 680f, 24f), shield, RightStyle(Cyan));
    }

    private string PowerStatus()
    {
        var active = new System.Collections.Generic.List<string>();
        if (weapons != null && weapons.DamageBoostRemaining > 0f) active.Add($"DAÑO {weapons.DamageBoostRemaining:0.0}s");
        if (session.Guardian.SpeedBoostRemaining > 0f) active.Add($"VELOCIDAD {session.Guardian.SpeedBoostRemaining:0.0}s");
        if (active.Count > 0) return string.Join("  |  ", active);
        if (session.IsPowerUpMessageVisible) return session.PowerUpMessage;
        return "Sin power-ups";
    }

    private void DrawHelp()
    {
        Dim(.55f);
        var rect = new Rect(570f, 258f, 780f, 520f);
        PanelBox(rect);
        GUI.Label(new Rect(610f, 292f, 700f, 48f), "CONTROLES", title);
        var text = "LS   Mover\nRS   Apuntar\nRB   Espada\nRT   Arco\nLT   Escudo temporal\nStart   Pausa\nSelect   Cerrar ayuda";
        GUI.Label(new Rect(635f, 360f, 650f, 330f), text, centered);
        GUI.Label(new Rect(610f, 716f, 700f, 34f), "Pulsa Select para cerrar", smallCentered());
    }

    private void DrawPause()
    {
        Dim(.62f);
        PanelBox(new Rect(650f, 390f, 620f, 280f));
        GUI.Label(new Rect(690f, 430f, 540f, 54f), "PAUSA", title);
        GUI.Label(new Rect(690f, 505f, 540f, 70f), "Start para continuar", centered);
        GUI.Label(new Rect(690f, 596f, 540f, 30f), "Select: ayuda", smallCentered());
    }

    private void DrawGameOver()
    {
        Dim(.72f);
        PanelBox(new Rect(530f, 235f, 860f, 610f));
        var reason = session.Center.Health <= 0 ? "CENTRO DE DATOS DESTRUIDO" : session.Guardian.Health <= 0 ? "GUARDIÁN DERROTADO" : "SESIÓN COMPLETADA";
        GUI.Label(new Rect(575f, 275f, 770f, 60f), reason, title);
        GUI.Label(new Rect(600f, 370f, 720f, 210f), $"PUNTUACIÓN\n{session.Score:N0}\n\nRécord: {session.BestScore:N0}    Oleada: {session.Wave:00}\nTiempo: {Mathf.FloorToInt(session.TimeElapsed / 60f):00}:{Mathf.FloorToInt(session.TimeElapsed % 60f):00}", centered);
        if (GUI.Button(new Rect(710f, 650f, 500f, 72f), "REINTENTAR", button)) session.Restart();
        GUI.Label(new Rect(650f, 748f, 620f, 32f), "A / Start", smallCentered());
    }

    private void DrawBar(Rect rect, float ratio, Color fill, string text)
    {
        DrawRect(rect, new Color(.08f, .11f, .16f, 1f));
        DrawRect(new Rect(rect.x + 4f, rect.y + 4f, (rect.width - 8f) * Mathf.Clamp01(ratio), rect.height - 8f), fill);
        GUI.Label(new Rect(rect.x + 10f, rect.y - 1f, rect.width - 20f, rect.height), text, small);
    }

    private void PanelBox(Rect rect)
    {
        DrawRect(rect, Border);
        DrawRect(new Rect(rect.x + 2f, rect.y + 2f, rect.width - 4f, rect.height - 4f), new Color(Panel.r, Panel.g, Panel.b, .96f));
    }

    private static void DrawRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, Texture2D.whiteTexture);
        GUI.color = Color.white;
    }

    private static void Dim(float alpha) => DrawRect(new Rect(0f, 0f, 1920f, 1080f), new Color(Background.r, Background.g, Background.b, alpha));
    private static float Ratio(int value, int max) => max <= 0 ? 0f : Mathf.Clamp01((float)value / max);

    private void EnsureStyles()
    {
        if (title != null) return;
        title = Style(36, TextAnchor.MiddleCenter, Color.white, FontStyle.Bold);
        value = Style(34, TextAnchor.MiddleLeft, Color.white, FontStyle.Bold);
        label = Style(17, TextAnchor.MiddleLeft, Muted);
        small = Style(20, TextAnchor.MiddleLeft, Color.white);
        centered = Style(24, TextAnchor.MiddleCenter, Color.white);
        button = new GUIStyle(GUI.skin.button) { fontSize = 26, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
    }

    private GUIStyle valueCentered() => Style(34, TextAnchor.MiddleCenter, Color.white, FontStyle.Bold);
    private GUIStyle smallCentered() => Style(20, TextAnchor.MiddleCenter, Muted);
    private GUIStyle RightStyle(Color color) => Style(19, TextAnchor.MiddleRight, color, FontStyle.Bold);

    private static GUIStyle Style(int size, TextAnchor anchor, Color color, FontStyle fontStyle = FontStyle.Normal)
    {
        var result = new GUIStyle(GUI.skin.label) { fontSize = size, alignment = anchor, normal = { textColor = color }, fontStyle = fontStyle, richText = true, wordWrap = true };
        return result;
    }

    private static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString("#" + hex, out var color);
        return color;
    }
}
