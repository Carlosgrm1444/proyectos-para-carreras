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
            GUI.Label(new Rect(24, 58, 600, 30), $"Flechas: inclinar esfera   |   R: reiniciar", body);
            GUI.Label(new Rect(24, 92, 500, 30), $"Vidas: {flow.Lives}   Estrellas: {flow.StarsCollected}   Puntos: {flow.Score}", body);
            GUI.Label(new Rect(24, 126, 500, 30), $"Tiempo: {flow.ElapsedTime:0.0}s   Récord: {flow.BestScore}", body);
            if (flow.State == GameState.Won)
            {
                GUI.Box(new Rect(Screen.width / 2f - 220, Screen.height / 2f - 80, 440, 160), "META ALCANZADA");
                GUI.Label(new Rect(Screen.width / 2f - 180, Screen.height / 2f - 35, 360, 35), $"Puntuación: {flow.Score} · Tiempo: {flow.ElapsedTime:0.0}s", body);
                GUI.Label(new Rect(Screen.width / 2f - 180, Screen.height / 2f + 5, 360, 35), "Pulsa R para jugar otra vez", body);
            }
        }
    }
}
