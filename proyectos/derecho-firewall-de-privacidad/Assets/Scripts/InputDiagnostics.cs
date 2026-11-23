using UnityEngine;

public sealed class InputDiagnostics : MonoBehaviour
{
    private bool visible;
    private GUIStyle style;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton6)) visible = !visible;
    }

    private void OnGUI()
    {
        if (!visible) return;
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.box) { fontSize = 16, alignment = TextAnchor.UpperLeft };
            style.normal.textColor = Color.white;
        }

        var joysticks = string.Join(", ", Input.GetJoystickNames());
        var text = "DIAGNOSTICO DE MANDO (SELECT oculta)\n" +
                   $"Dispositivo: {(string.IsNullOrWhiteSpace(joysticks) ? "ninguno detectado" : joysticks)}\n" +
                   $"Stick izquierdo: {Input.GetAxisRaw("Horizontal"):0.00}, {Input.GetAxisRaw("Vertical"):0.00}\n" +
                   $"Stick derecho: {Input.GetAxisRaw("AimHorizontal"):0.00}, {Input.GetAxisRaw("AimVertical"):0.00}\n" +
                   $"LT/RT combinado: {Input.GetAxisRaw("LeftTrigger"):0.00} / {Input.GetAxisRaw("RightTrigger"):0.00}\n" +
                   $"RB: {Input.GetKey(KeyCode.JoystickButton5)} | LT: {Input.GetAxisRaw("LeftTrigger") < -.35f}\n" +
                   $"RT: {Input.GetAxisRaw("RightTrigger") > .35f} | Select: {Input.GetKey(KeyCode.JoystickButton6)}\n" +
                   $"Start: {Input.GetKey(KeyCode.JoystickButton7)}\n" +
                   "Prueba cada control; los estados deben cambiar a True al pulsarlos.";
        GUI.Box(new Rect(20f, 190f, 560f, 220f), text, style);
    }
}
