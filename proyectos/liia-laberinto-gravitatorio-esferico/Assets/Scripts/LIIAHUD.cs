using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class LIIAHUD : MonoBehaviour
    {
        private GUIStyle title;
        private GUIStyle body;

        private void OnGUI()
        {
            if (title == null)
            {
                title = new GUIStyle(GUI.skin.label) { fontSize = 24, fontStyle = FontStyle.Bold, normal = { textColor = Color.white } };
                body = new GUIStyle(GUI.skin.label) { fontSize = 18, normal = { textColor = Color.white } };
            }
            if (GameFlow.Instance == null) return;
            var flow = GameFlow.Instance;
            GUI.Label(new Rect(24, 18, 600, 40), "LIIA · LABERINTO GRAVITATORIO", title);
            var audioState = LIIAAudioFeedback.Instance != null && LIIAAudioFeedback.Instance.Muted ? "apagado" : "encendido";
            GUI.Label(new Rect(24, 58, 700, 30), $"Flechas: mover bola   |   R: reiniciar   |   M: audio {audioState}", body);
            GUI.Label(new Rect(24, 92, 900, 30), $"Nivel {flow.LevelIndex + 1}: {flow.LevelName}   |   Vidas: {flow.Lives}   Estrellas: {flow.StarsCollected}/{flow.TotalStars}   Puntos: {flow.Score}", body);
            GUI.Label(new Rect(24, 126, 500, 30), $"Tiempo: {flow.ElapsedTime:0.0}s   Récord: {flow.BestScore}", body);
            GUI.Label(new Rect(24, 160, 900, 26), "Esfera amarilla: estrella (+100)   Pilar azul: checkpoint   Bloque morado: trampa", body);
            if (flow.State == GameState.Attract)
            {
                var panel = new Rect(Screen.width / 2f - 310, Screen.height / 2f - 190, 620, 380);
                GUI.Box(panel, "LIIA · CIRCUITOS PLANETARIOS");
                GUI.Label(new Rect(panel.x + 35, panel.y + 52, panel.width - 70, 30), "Rodea el planeta, toma rutas de riesgo y supera el récord.", body);
                GUI.Label(new Rect(panel.x + 35, panel.y + 92, panel.width - 70, 30), $"SELECCIONADO: {flow.LevelIndex + 1} · {flow.LevelName}", body);
                GUI.Label(new Rect(panel.x + 35, panel.y + 132, panel.width - 70, 30), "1 Órbita   2 Hélice   3 Meridianos", body);
                GUI.Label(new Rect(panel.x + 35, panel.y + 166, panel.width - 70, 30), "4 Red neuronal   5 Singularidad", body);
                GUI.Label(new Rect(panel.x + 35, panel.y + 220, panel.width - 70, 30), "PULSA ENTER, ESPACIO O UNA FLECHA PARA JUGAR", body);
                GUI.Label(new Rect(panel.x + 35, panel.y + 266, panel.width - 70, 30), $"RÉCORD DEL NIVEL: {flow.BestScore}", body);
                GUI.Label(new Rect(panel.x + 35, panel.y + 306, panel.width - 70, 30), "El mapa continúa al cruzar sus bordes laterales.", body);
            }
            if (flow.HasStatusMessage && flow.State == GameState.Playing)
            {
                var messageStyle = new GUIStyle(title) { alignment = TextAnchor.MiddleCenter, fontSize = 20 };
                GUI.Box(new Rect(Screen.width / 2f - 190, Screen.height - 104, 380, 48), string.Empty);
                GUI.Label(new Rect(Screen.width / 2f - 180, Screen.height - 96, 360, 32), flow.StatusMessage, messageStyle);
            }
            if (flow.State == GameState.Won)
            {
                GUI.Box(new Rect(Screen.width / 2f - 250, Screen.height / 2f - 105, 500, 210), "META ALCANZADA");
                GUI.Label(new Rect(Screen.width / 2f - 210, Screen.height / 2f - 58, 420, 35), $"Puntuación: {flow.Score} · Tiempo: {flow.ElapsedTime:0.0}s", body);
                GUI.Label(new Rect(Screen.width / 2f - 210, Screen.height / 2f - 20, 420, 35), $"Estrellas: {flow.StarsCollected}/{flow.TotalStars} · Bono de rapidez: {flow.LastTimeBonus}", body);
                GUI.Label(new Rect(Screen.width / 2f - 210, Screen.height / 2f + 25, 420, 35), "R: repetir nivel", body);
                GUI.Label(new Rect(Screen.width / 2f - 210, Screen.height / 2f + 61, 420, 35), "Enter / Espacio / N: siguiente nivel", body);
            }
        }
    }
}
