using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class LIIAHUD : MonoBehaviour
    {
        private GUIStyle title;
        private GUIStyle subtitle;
        private GUIStyle body;
        private GUIStyle compact;
        private GUIStyle centerTitle;
        private GUIStyle centerBody;

        private void EnsureStyles()
        {
            if (title != null) return;
            title = LIIAGUITheme.Label(21, LIIAGUITheme.Ivory, FontStyle.Bold);
            subtitle = LIIAGUITheme.Label(12, LIIAGUITheme.Muted, FontStyle.Bold);
            body = LIIAGUITheme.Label(17, LIIAGUITheme.Ivory);
            compact = LIIAGUITheme.Label(15, LIIAGUITheme.Ivory);
            centerTitle = LIIAGUITheme.Label(35, LIIAGUITheme.Ivory, FontStyle.Bold, TextAnchor.MiddleCenter);
            centerBody = LIIAGUITheme.Label(18, LIIAGUITheme.Ivory, FontStyle.Normal, TextAnchor.MiddleCenter);
        }

        private void OnGUI()
        {
            if (GameFlow.Instance == null) return;
            EnsureStyles();
            LIIAGUITheme.BeginCanvas();
            var flow = GameFlow.Instance;

            DrawIdentityCard();
            DrawStats(flow);
            DrawBottomBar(flow);

            if (flow.State == GameState.Attract) DrawStartCard(flow);
            else if (flow.State == GameState.Won) DrawVictoryCard(flow);
            else if (flow.State == GameState.Paused) DrawPauseCard();

            if (flow.HasStatusMessage && flow.State == GameState.Playing)
                DrawStatus(flow.StatusMessage);
            LIIAGUITheme.EndCanvas();
        }

        private void DrawIdentityCard()
        {
            var panel = new Rect(24, 20, 324, 292);
            LIIAGUITheme.PanelBox(panel);
            GUI.Label(new Rect(43, 39, 58, 52), "◉", LIIAGUITheme.Label(43, LIIAGUITheme.Gold, FontStyle.Bold, TextAnchor.MiddleCenter));
            GUI.Label(new Rect(105, 34, 220, 48), "LIIA · LABERINTO\nGRAVITATORIO", title);
            GUI.Label(new Rect(44, 91, 275, 22), "CIRCUITOS PLANETARIOS", subtitle);
            LIIAGUITheme.Line(44, 125, 280, LIIAGUITheme.GoldSoft);
            GUI.Label(new Rect(44, 138, 280, 22), "CONTROLES", subtitle);
            DrawControl(44, 172, "WASD / ↑↓←→", "Inclinar esfera");
            DrawControl(44, 205, "R", "Reiniciar nivel");
            DrawControl(44, 238, "C", "Recentrar cámara");
            DrawControl(44, 270, "M", "Audio", 82);
            DrawControl(178, 270, "ESC", "Pausa", 80);
        }

        private void DrawControl(float x, float y, string key, string action, float actionWidth = 140f)
        {
            var keyWidth = Mathf.Clamp(key.Length * 9.5f + 16f, 35f, 130f);
            LIIAGUITheme.Fill(new Rect(x, y, keyWidth, 26), new Color(.18f, .25f, .29f, .95f));
            GUI.Label(new Rect(x, y + 1, keyWidth, 24), key, LIIAGUITheme.Label(13, LIIAGUITheme.Ivory, FontStyle.Bold, TextAnchor.MiddleCenter));
            GUI.Label(new Rect(x + keyWidth + 10, y + 2, actionWidth, 24), action, LIIAGUITheme.Label(14, LIIAGUITheme.Ivory));
        }

        private void DrawStats(GameFlow flow)
        {
            var panel = new Rect(388, 20, 1160, 74);
            LIIAGUITheme.PanelBox(panel);
            GUI.Label(new Rect(414, 41, 250, 32), $"◉  Nivel {flow.LevelIndex + 1} · {flow.LevelName}", LIIAGUITheme.Label(18, LIIAGUITheme.Ivory, FontStyle.Bold));
            LIIAGUITheme.Line(674, 38, 1, LIIAGUITheme.GoldSoft, 38);
            GUI.Label(new Rect(697, 42, 145, 27), $"♥  Vidas: {flow.Lives}", LIIAGUITheme.Label(18, new Color(1f, .42f, .36f)));
            GUI.Label(new Rect(846, 42, 170, 27), $"★  {flow.StarsCollected}/{flow.TotalStars}", LIIAGUITheme.Label(18, new Color(1f, .78f, .2f)));
            GUI.Label(new Rect(1015, 42, 175, 27), $"◆  Puntos: {flow.Score}", compact);
            GUI.Label(new Rect(1184, 42, 155, 27), $"◷  {flow.ElapsedTime:0.0}s", compact);
            var record = flow.BestTime > 0f ? $"{flow.BestScore} · {flow.BestTime:0.0}s" : "Sin récord";
            GUI.Label(new Rect(1340, 42, 190, 27), $"♛  {record}", LIIAGUITheme.Label(14, LIIAGUITheme.Gold));
        }

        private void DrawBottomBar(GameFlow flow)
        {
            var panel = new Rect(24, 1001, 1872, 56);
            LIIAGUITheme.PanelBox(panel);
            GUI.Label(new Rect(53, 1018, 790, 24), $"OBJETIVO:  {Objective(flow)}", compact);
            LIIAGUITheme.Line(850, 1013, 1, LIIAGUITheme.GoldSoft, 32);
            GUI.Label(new Rect(884, 1018, 190, 24), "★  Estrella  +100", LIIAGUITheme.Label(15, new Color(1f, .78f, .2f)));
            GUI.Label(new Rect(1094, 1018, 205, 24), "▣  Checkpoint", LIIAGUITheme.Label(15, LIIAGUITheme.Blue));
            GUI.Label(new Rect(1311, 1018, 188, 24), "▲▲▲  Trampa", LIIAGUITheme.Label(15, LIIAGUITheme.Purple));
            GUI.Label(new Rect(1540, 1018, 330, 24), "C: cámara   M: audio   Esc: pausa", compact);
        }

        private static string Objective(GameFlow flow)
        {
            if (flow.StarsCollected < flow.TotalStars)
                return $"Explora el circuito, recoge estrellas ({flow.StarsCollected}/{flow.TotalStars}) y alcanza la meta.";
            return "Todas las estrellas recogidas. Alcanza la meta y rompe el récord.";
        }

        private void DrawStartCard(GameFlow flow)
        {
            var panel = new Rect(608, 300, 704, 408);
            LIIAGUITheme.PanelBox(panel, 2f, new Color(.018f, .07f, .095f, .95f));
            GUI.Label(new Rect(648, 324, 624, 28), $"NIVEL {flow.LevelIndex + 1}", LIIAGUITheme.Label(16, LIIAGUITheme.Gold, FontStyle.Bold, TextAnchor.MiddleCenter));
            GUI.Label(new Rect(646, 356, 628, 55), flow.LevelName, centerTitle);
            LIIAGUITheme.Line(790, 423, 340, LIIAGUITheme.GoldSoft);
            GUI.Label(new Rect(685, 446, 550, 62), flow.LevelDescription, centerBody);
            GUI.Label(new Rect(710, 520, 500, 34), $"★  {flow.TotalStars} estrellas disponibles   ·   Par {flow.ParTimeSeconds:0}s", LIIAGUITheme.Label(17, LIIAGUITheme.Gold, FontStyle.Bold, TextAnchor.MiddleCenter));
            GUI.Label(new Rect(676, 570, 568, 29), "Selecciona:  [1]  [2]  [3]  [4]  [5]", LIIAGUITheme.Label(16, LIIAGUITheme.Muted, FontStyle.Bold, TextAnchor.MiddleCenter));
            LIIAGUITheme.Fill(new Rect(780, 624, 360, 51), new Color(.58f, .36f, .08f, .94f));
            GUI.Label(new Rect(780, 629, 360, 39), "PRESIONA  ENTER  PARA COMENZAR", LIIAGUITheme.Label(16, LIIAGUITheme.Ivory, FontStyle.Bold, TextAnchor.MiddleCenter));
        }

        private void DrawVictoryCard(GameFlow flow)
        {
            var panel = new Rect(665, 359, 590, 320);
            LIIAGUITheme.PanelBox(panel, 2f, new Color(.018f, .07f, .095f, .94f));
            GUI.Label(new Rect(700, 380, 520, 32), flow.NewRecord ? "NUEVO RÉCORD" : "META ALCANZADA", LIIAGUITheme.Label(17, LIIAGUITheme.Gold, FontStyle.Bold, TextAnchor.MiddleCenter));
            GUI.Label(new Rect(700, 416, 520, 54), "META ALCANZADA", centerTitle);
            LIIAGUITheme.Line(795, 483, 330, LIIAGUITheme.GoldSoft);
            GUI.Label(new Rect(715, 505, 490, 29), $"Puntuación  {flow.Score}     ·     Tiempo  {flow.ElapsedTime:0.0}s", centerBody);
            GUI.Label(new Rect(715, 541, 490, 28), $"Estrellas  {flow.StarsCollected}/{flow.TotalStars}     ·     Bono  +{flow.LastTimeBonus}", centerBody);
            GUI.Label(new Rect(700, 591, 520, 28), "R  Repetir nivel       ·       Enter  Siguiente nivel", LIIAGUITheme.Label(16, LIIAGUITheme.Ivory, FontStyle.Bold, TextAnchor.MiddleCenter));
            GUI.Label(new Rect(700, 628, 520, 22), $"Récord: {flow.BestScore} puntos · {flow.BestTime:0.0}s", LIIAGUITheme.Label(14, LIIAGUITheme.Muted, FontStyle.Normal, TextAnchor.MiddleCenter));
        }

        private void DrawPauseCard()
        {
            var panel = new Rect(760, 420, 400, 170);
            LIIAGUITheme.PanelBox(panel, 2f);
            GUI.Label(new Rect(790, 443, 340, 55), "PAUSA", centerTitle);
            GUI.Label(new Rect(790, 511, 340, 32), "Presiona Esc para continuar", centerBody);
            GUI.Label(new Rect(790, 550, 340, 22), "La esfera está detenida", LIIAGUITheme.Label(14, LIIAGUITheme.Muted, FontStyle.Normal, TextAnchor.MiddleCenter));
        }

        private void DrawStatus(string message)
        {
            var panel = new Rect(760, 917, 400, 50);
            LIIAGUITheme.PanelBox(panel);
            GUI.Label(panel, message, LIIAGUITheme.Label(16, LIIAGUITheme.Gold, FontStyle.Bold, TextAnchor.MiddleCenter));
        }
    }
}
