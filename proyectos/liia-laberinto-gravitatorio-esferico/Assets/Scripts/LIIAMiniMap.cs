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
                labelStyle = LIIAGUITheme.Label(15, LIIAGUITheme.Ivory, FontStyle.Bold);

            LIIAGUITheme.BeginCanvas();
            var panel = new Rect(1581, 20, 315, 230);
            var mapRect = new Rect(1602, 62, 273, 166);
            LIIAGUITheme.PanelBox(panel);
            GUI.Label(new Rect(1602, 34, 255, 24), "MAPA DEL NIVEL", labelStyle);
            LIIAGUITheme.Fill(mapRect, new Color(.01f, .025f, .045f, .96f));

            var cellWidth = mapRect.width / layout.Width;
            var cellHeight = mapRect.height / layout.Height;
            for (var y = 0; y < layout.Height; y++)
            for (var x = 0; x < layout.Width; x++)
            {
                var tile = layout.TileAt(x, y);
                if (tile == MazeTile.Wall) continue;
                var rect = CellRect(new Vector2Int(x, y), mapRect, cellWidth, cellHeight);
                LIIAGUITheme.Fill(rect, new Color(.76f, .86f, .87f, .78f));
            }

            DrawMapMarkers(mapRect, cellWidth, cellHeight);
            var px = Mathf.Repeat(ball.GridPosition.x + .5f, layout.Width) / layout.Width;
            var py = 1f - Mathf.Clamp01((ball.GridPosition.y + .5f) / layout.Height);
            LIIAGUITheme.Fill(new Rect(mapRect.x + px * mapRect.width - 5f, mapRect.y + py * mapRect.height - 5f, 10f, 10f), LIIAGUITheme.Gold);
            LIIAGUITheme.EndCanvas();
        }

        private void DrawMapMarkers(Rect mapRect, float cellWidth, float cellHeight)
        {
            var flow = GameFlow.Instance;
            foreach (var star in layout.StarCells)
            {
                if (flow != null && flow.IsStarCollected(star)) continue;
                DrawMarker(star, mapRect, cellWidth, cellHeight, new Color(1f, .8f, .13f), 6f);
            }
            foreach (var checkpoint in layout.CheckpointCells)
            {
                var active = flow != null && flow.IsCheckpointActive(checkpoint);
                DrawMarker(checkpoint, mapRect, cellWidth, cellHeight, active ? Color.white : LIIAGUITheme.Blue, active ? 7f : 5f);
            }
            foreach (var trap in layout.TrapCells)
                DrawMarker(trap, mapRect, cellWidth, cellHeight, LIIAGUITheme.Purple, 4f);
            DrawMarker(layout.GoalCell, mapRect, cellWidth, cellHeight, new Color(1f, .48f, .12f), 8f);
        }

        private static void DrawMarker(Vector2Int cell, Rect mapRect, float cellWidth, float cellHeight, Color color, float size)
        {
            var centerX = mapRect.x + (cell.x + .5f) * cellWidth;
            var centerY = mapRect.y + mapRect.height - (cell.y + .5f) * cellHeight;
            LIIAGUITheme.Fill(new Rect(centerX - size * .5f, centerY - size * .5f, size, size), color);
        }

        private static Rect CellRect(Vector2Int cell, Rect mapRect, float cellWidth, float cellHeight)
        {
            return new Rect(
                mapRect.x + cell.x * cellWidth,
                mapRect.y + mapRect.height - (cell.y + 1) * cellHeight,
                Mathf.Max(1f, cellWidth + .35f),
                Mathf.Max(1f, cellHeight + .35f));
        }
    }
}
