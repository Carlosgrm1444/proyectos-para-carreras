using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class LIIAMiniMap : MonoBehaviour
    {
        private MazeBallController ball;
        private SphericalMazeLayout layout;
        private GUIStyle labelStyle;

        public void Configure(MazeBallController player, SphericalMazeLayout maze)
        {
            ball = player;
            layout = maze;
        }

        private void OnGUI()
        {
            if (ball == null || layout == null || layout.SourceTexture == null) return;
            if (labelStyle == null)
                labelStyle = new GUIStyle(GUI.skin.label) { fontSize = 14, fontStyle = FontStyle.Bold, normal = { textColor = Color.white } };

            const float width = 210f;
            const float height = 150f;
            var mapRect = new Rect(Screen.width - width - 22f, 22f, width, height);
            GUI.Box(new Rect(mapRect.x - 6f, mapRect.y - 26f, width + 12f, height + 32f), string.Empty);
            GUI.Label(new Rect(mapRect.x, mapRect.y - 23f, width, 20f), "MAPA", labelStyle);
            GUI.DrawTexture(mapRect, layout.SourceTexture, ScaleMode.StretchToFill, false);

            var x = Mathf.Clamp01(ball.GridPosition.x / (layout.Width - 1f));
            var y = 1f - Mathf.Clamp01(ball.GridPosition.y / (layout.Height - 1f));
            var dotSize = 10f;
            var playerRect = new Rect(
                mapRect.x + x * mapRect.width - dotSize * .5f,
                mapRect.y + y * mapRect.height - dotSize * .5f,
                dotSize,
                dotSize);
            var previousColor = GUI.color;
            GUI.color = new Color(1f, .75f, .05f);
            GUI.DrawTexture(playerRect, Texture2D.whiteTexture);
            GUI.color = previousColor;
        }
    }
}
