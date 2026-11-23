using System.Collections.Generic;
using UnityEngine;

public sealed class TutorialDirector : MonoBehaviour
{
    private GameSession session;
    private GUIStyle boxStyle;
    private GUIStyle titleStyle;
    private GUIStyle bodyStyle;
    private GUIStyle actionStyle;
    private string title;
    private string body;
    private bool guideOpen;
    public static bool IsBlockingInput { get; private set; }
    private bool introShown;
    private bool movementShown;
    private bool controlsShown;
    private bool swordShown;
    private bool bowShown;
    private bool shieldShown;
    private readonly HashSet<ThreatType> explainedThreats = new HashSet<ThreatType>();
    private bool powerUpShown;

    public void Configure(GameSession owner) => session = owner;

    private void Start()
    {
        introShown = true;
        Show("FIREWALL DE PRIVACIDAD", "Protege las solicitudes legítimas y defiende el Centro de Datos.\nEl consentimiento, la finalidad y la proporcionalidad orientan qué debe protegerse.\nLas armas son metáforas educativas, no acciones jurídicas reales.");
    }

    private void Update()
    {
        if (session == null) return;
        if (guideOpen)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton7)) CloseGuide();
            return;
        }

        var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!movementShown && move.sqrMagnitude > .12f)
        {
            movementShown = true;
            Show("MOVERSE", "Stick izquierdo: mueve al Guardián.\nUsa el escenario para rodear amenazas y proteger el Centro.");
        }
        if (!swordShown && Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            swordShown = true;
            Show("ESPADA", "RB: golpe cercano.\nÚsala contra amenazas que se acerquen al Guardián o al Centro.");
        }
        if (!bowShown && Input.GetAxisRaw("RightTrigger") > .35f)
        {
            bowShown = true;
            Show("ARCO", "RT: dispara a distancia.\nRS apunta en la dirección que necesitas cubrir.");
        }
        if (!shieldShown && (Input.GetKey(KeyCode.JoystickButton4) || Input.GetAxisRaw("LeftTrigger") < -.35f))
        {
            shieldShown = true;
            Show("ESCUDO", "LT: bloquea durante 1.5 segundos.\nSe rompe al terminar y recarga durante 2 segundos.");
        }

        foreach (var threat in FindObjectsOfType<Threat>())
        {
            if (explainedThreats.Contains(threat.Type)) continue;
            explainedThreats.Add(threat.Type);
            ShowThreat(threat.Type);
            break;
        }
        if (!powerUpShown && FindObjectsOfType<PowerUp>().Length > 0)
        {
            powerUpShown = true;
            Show("POWER-UP", "Acércate para recogerlo.\nVerde: recuperación · amarillo: velocidad · magenta: daño reforzado.");
        }
    }

    private void ShowThreat(ThreatType type)
    {
        var text = type == ThreatType.Intruder ? "Intruso: se acerca al Centro. Usa RB para detenerlo." :
                   type == ThreatType.RemoteLeak ? "Filtrador: ataca desde lejos. Usa RS para apuntar y RT para responder." :
                   type == ThreatType.ArmoredImpostor ? "Blindado: bloquea sus golpes con LT y contraataca con RB." :
                   type == ThreatType.Interceptor ? "Interceptor: persigue las solicitudes verdes. Priorízalas y detenlo con RB o RT." :
                   "Saboteador: va directo al Centro. Intercepta su avance antes de que llegue.";
        Show("NUEVA AMENAZA", text);
    }

    private void Show(string newTitle, string newBody)
    {
        title = newTitle;
        body = newBody;
        guideOpen = true;
        IsBlockingInput = true;
        Time.timeScale = 0f;
    }

    private void CloseGuide()
    {
        guideOpen = false;
        if (introShown && !movementShown)
        {
            movementShown = true;
            Show("MOVERSE", "Stick izquierdo: mueve al Guardián.\nRodea las amenazas y protege el Centro.");
            return;
        }
        if (movementShown && !controlsShown)
        {
            controlsShown = true;
            Show("CONTEXTO", "Protege datos personales y solicitudes legítimas.\nARCO representa Acceso, Rectificación, Cancelación y Oposición; las armas son metáforas educativas.");
            return;
        }
        IsBlockingInput = false;
        if (!session.IsPaused && !session.IsGameOver) Time.timeScale = 1f;
    }

    private void OnGUI()
    {
        if (!guideOpen || string.IsNullOrEmpty(body)) return;
        if (boxStyle == null)
        {
            boxStyle = new GUIStyle(GUI.skin.box) { alignment = TextAnchor.MiddleCenter };
            var scale = Mathf.Clamp(Screen.height / 900f, .8f, 1.2f);
            titleStyle = new GUIStyle(GUI.skin.label) { fontSize = Mathf.RoundToInt(28f * scale), alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
            bodyStyle = new GUIStyle(GUI.skin.label) { fontSize = Mathf.RoundToInt(20f * scale), alignment = TextAnchor.MiddleCenter, wordWrap = true };
            actionStyle = new GUIStyle(GUI.skin.label) { fontSize = Mathf.RoundToInt(18f * scale), alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        }
        GUI.color = new Color(.078f, .102f, .141f, .58f);
        GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), Texture2D.whiteTexture);
        var width = Mathf.Min(700f, Screen.width * .68f);
        var height = 260f;
        var x = (Screen.width - width) * .5f;
        var y = (Screen.height - height) * .5f;
        GUI.color = new Color(.20f, .27f, .36f, 1f);
        GUI.DrawTexture(new Rect(x - 2f, y - 2f, width + 4f, height + 4f), Texture2D.whiteTexture);
        GUI.color = new Color(.13f, .17f, .24f, .98f);
        GUI.DrawTexture(new Rect(x, y, width, height), Texture2D.whiteTexture);
        GUI.color = Color.white;
        GUI.Label(new Rect(x + 30f, y + 18f, width - 60f, 42f), title, titleStyle);
        GUI.Label(new Rect(x + 48f, y + 68f, width - 96f, 104f), body, bodyStyle);
        GUI.color = new Color(.22f, .74f, .97f, 1f);
        GUI.DrawTexture(new Rect(x + width * .5f - 110f, y + 194f, 220f, 44f), Texture2D.whiteTexture);
        GUI.color = Color.white;
        var isOpening = title == "FIREWALL DE PRIVACIDAD";
        var action = isOpening ? "A  COMENZAR" : "A  CONTINUAR";
        GUI.Label(new Rect(x + width * .5f - 110f, y + 194f, 220f, 44f), action, actionStyle);
    }

}

