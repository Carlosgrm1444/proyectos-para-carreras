using UnityEngine;

namespace LIIA.SphereMaze
{
    /// <summary>Panel sólo para ajustar control y cámara durante desarrollo. Se activa con F3.</summary>
    public sealed class LIIADevDiagnostics : MonoBehaviour
    {
        private MazeBallController ball;
        private SphericalMazeLayout layout;
        private SphereTiltInputRouter input;
        private bool visible;
        private float smoothedFps;
        private GUIStyle style;

        public void Configure(MazeBallController player, SphericalMazeLayout maze, SphereTiltInputRouter inputRouter = null)
        {
            ball = player;
            layout = maze;
            input = inputRouter;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3)) visible = !visible;
            var instantFps = Time.unscaledDeltaTime > 0f ? 1f / Time.unscaledDeltaTime : 0f;
            smoothedFps = Mathf.Lerp(smoothedFps, instantFps, .12f);
        }

        private void OnGUI()
        {
            if (!visible || ball == null || layout == null) return;
            if (style == null)
                style = new GUIStyle(GUI.skin.label) { fontSize = 14, normal = { textColor = Color.white } };

            var cell = new Vector2Int(Mathf.RoundToInt(ball.GridPosition.x), Mathf.RoundToInt(ball.GridPosition.y));
            GUI.Box(new Rect(20, Screen.height - 170, 390, 148), "DIAGNÓSTICO · F3 para ocultar");
            GUI.Label(new Rect(34, Screen.height - 142, 360, 20), $"FPS: {smoothedFps:0}", style);
            GUI.Label(new Rect(34, Screen.height - 120, 360, 20), $"Velocidad: {ball.Speed:0.00} celdas/s", style);
            GUI.Label(new Rect(34, Screen.height - 98, 360, 20), $"Celda: {cell.x}, {cell.y} / {layout.Width - 1}, {layout.Height - 1}", style);
            GUI.Label(new Rect(34, Screen.height - 76, 360, 20), $"Vector: {ball.Velocity.x:0.00}, {ball.Velocity.y:0.00}", style);
            if (input == null) return;
            var esp32 = input.Esp32;
            var hardware = esp32 == null
                ? "ESP32: no inicializado"
                : $"ESP32: {(esp32.HasFreshSignal ? "activo" : esp32.IsConnected ? "sin señal" : "desconectado")} · {esp32.PortName}";
            GUI.Label(new Rect(34, Screen.height - 54, 360, 20), $"Entrada: {input.ActiveSource} · {hardware}", style);
            if (esp32 != null)
                GUI.Label(new Rect(34, Screen.height - 32, 360, 20), $"Paquete: {esp32.LastSequence} · edad: {esp32.SignalAgeSeconds:0.000}s · {esp32.LastError}", style);
        }
    }
}
