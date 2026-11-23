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
            GUI.Label(new Rect(24, 92, 500, 30), $"Vidas: {flow.Lives}   Estrellas: {flow.StarsCollected}   Puntos: {flow.Score}", body);
            GUI.Label(new Rect(24, 126, 500, 30), $"Tiempo: {flow.ElapsedTime:0.0}s   Récord: {flow.BestScore}", body);
            GUI.Label(new Rect(24, 160, 900, 26), "Amarillo: estrella (+100)   Azul: checkpoint   Morado: trampa", body);
            if (flow.State == GameState.Attract)
            {
                GUI.Box(new Rect(Screen.width / 2f - 245, Screen.height / 2f - 108, 490, 216), "LIIA · LABERINTO GRAVITATORIO");
                GUI.Label(new Rect(Screen.width / 2f - 210, Screen.height / 2f - 50, 420, 30), "Llega a la meta, toma riesgos y supera el récord.", body);
                GUI.Label(new Rect(Screen.width / 2f - 210, Screen.height / 2f - 4, 420, 30), "PULSA ENTER, ESPACIO O UNA FLECHA PARA JUGAR", body);
                GUI.Label(new Rect(Screen.width / 2f - 210, Screen.height / 2f + 44, 420, 30), $"RÉCORD ACTUAL: {flow.BestScore}", body);
            }
            if (flow.HasStatusMessage && flow.State == GameState.Playing)
            {
                var messageStyle = new GUIStyle(title) { alignment = TextAnchor.MiddleCenter, fontSize = 20 };
                GUI.Box(new Rect(Screen.width / 2f - 190, Screen.height - 104, 380, 48), string.Empty);
                GUI.Label(new Rect(Screen.width / 2f - 180, Screen.height - 96, 360, 32), flow.StatusMessage, messageStyle);
            }
            if (flow.State == GameState.Won)
            {
                GUI.Box(new Rect(Screen.width / 2f - 220, Screen.height / 2f - 80, 440, 160), "META ALCANZADA");
                GUI.Label(new Rect(Screen.width / 2f - 180, Screen.height / 2f - 35, 360, 35), $"Puntuación: {flow.Score} · Tiempo: {flow.ElapsedTime:0.0}s", body);
                GUI.Label(new Rect(Screen.width / 2f - 180, Screen.height / 2f + 5, 360, 35), "Pulsa R para jugar otra vez", body);
            }
        }
    }
}
