using UnityEngine;

namespace LIIA.SphereMaze
{
    /// <summary>Panel sólo para ajustar control y cámara durante desarrollo. Se activa con F3.</summary>
    public sealed class LIIADevDiagnostics : MonoBehaviour
    {
        private MazeBallController ball;
        private SphericalMazeLayout layout;
        private bool visible;
        private float smoothedFps;
        private GUIStyle style;

        public void Configure(MazeBallController player, SphericalMazeLayout maze)
        {
            ball = player;
            layout = maze;
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
            GUI.Box(new Rect(20, Screen.height - 128, 330, 106), "DIAGNÓSTICO · F3 para ocultar");
            GUI.Label(new Rect(34, Screen.height - 100, 300, 20), $"FPS: {smoothedFps:0}", style);
            GUI.Label(new Rect(34, Screen.height - 78, 300, 20), $"Velocidad: {ball.Speed:0.00} celdas/s", style);
            GUI.Label(new Rect(34, Screen.height - 56, 300, 20), $"Celda: {cell.x}, {cell.y} / {layout.Width - 1}, {layout.Height - 1}", style);
            GUI.Label(new Rect(34, Screen.height - 34, 300, 20), $"Vector: {ball.Velocity.x:0.00}, {ball.Velocity.y:0.00}", style);
        }
    }
}
