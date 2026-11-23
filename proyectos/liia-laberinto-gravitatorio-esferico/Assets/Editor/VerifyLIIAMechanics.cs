using System;
using UnityEditor;
using UnityEngine;

namespace LIIA.SphereMaze.Editor
{
    public static class VerifyLIIAMechanics
    {
        [MenuItem("LIIA/Verificar mecánicas en batch")]
        public static void Run()
        {
            VerifyMaps();
            VerifyGameplayLoop();
            Debug.Log("VERIFICACIÓN LIIA APROBADA: mapas, ciclo de juego, puntuación y récord correctos.");
        }

        private static void VerifyMaps()
        {
            foreach (var mapName in new[] { "laberinto_01", "laberinto_02", "laberinto_03" })
            {
                var map = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Resources/Maps/{mapName}.png");
                Require(map != null, $"No se encontró {mapName}.png.");
                Require(SphericalMazeValidator.TryValidate(map, out var message), $"{mapName}: {message}");
            }
        }

        private static void VerifyGameplayLoop()
        {
            var map = CreateTestMap();
            var root = new GameObject("VerifyRoot");
            var layout = root.AddComponent<SphericalMazeLayout>();
            layout.Configure(map, 5.08f);
            var input = root.AddComponent<KeyboardSphereTiltInput>();

            var ballObject = new GameObject("VerifyBall");
            var ball = ballObject.AddComponent<MazeBallController>();
            ball.Configure(layout, input, null);

            var flowObject = new GameObject("VerifyFlow");
            var flow = flowObject.AddComponent<GameFlow>();
            flow.Configure(ball, MazeLevelCatalog.Get(0));

            Require(flow.State == GameState.Attract, "El juego debe comenzar en modo de atracción.");
            Require(ball.GridPosition == (Vector2)layout.StartCell, "El modo de atracción debe ubicar la bola en el inicio.");
            flow.RestartAttempt();
            Require(flow.State == GameState.Playing && flow.Lives == 3 && flow.Score == 0,
                "Un intento nuevo debe iniciar con tres vidas y cero puntos.");
            Require(Mathf.Abs(Vector3.Distance(ball.transform.position, layout.transform.position) - 5.36f) < .001f,
                "La bola debe mantenerse sobre la superficie de la esfera.");

            var starCell = new Vector2Int(2, 1);
            flow.CollectStar(starCell);
            flow.CollectStar(starCell);
            Require(flow.StarsCollected == 1 && flow.Score == 100, "Una estrella debe sumar exactamente una vez.");

            var checkpointCell = new Vector2Int(3, 1);
            flow.SetCheckpoint(checkpointCell);
            flow.PlayerHitTrap();
            Require(flow.Lives == 2, "Una trampa debe restar una vida.");
            Require(ball.GridPosition == (Vector2)checkpointCell, "La trampa debe regresar al último checkpoint.");

            flow.PlayerHitTrap();
            flow.PlayerHitTrap();
            Require(flow.Lives == 3 && flow.StarsCollected == 0 && flow.Score == 0,
                "Tras la tercera trampa debe reiniciarse el intento completo.");

            var scoreKey = $"LIIA_BEST_SCORE_{flow.LevelIndex}";
            var timeKey = $"LIIA_BEST_TIME_{flow.LevelIndex}";
            var hadScore = PlayerPrefs.HasKey(scoreKey);
            var hadTime = PlayerPrefs.HasKey(timeKey);
            var previousScore = PlayerPrefs.GetInt(scoreKey);
            var previousTime = PlayerPrefs.GetFloat(timeKey);
            try
            {
                PlayerPrefs.DeleteKey(scoreKey);
                PlayerPrefs.DeleteKey(timeKey);
                flow.CollectStar(starCell);
                flow.ReachGoal();
                Require(flow.State == GameState.Won, "Al llegar a la meta el intento debe terminar.");
                Require(flow.Score == 400, "Estrella, meta y bono máximo deben sumar 400 puntos.");
                Require(flow.BestScore == 400 && Mathf.Approximately(flow.BestTime, 0f),
                    "La victoria debe registrar un récord por nivel.");

                flow.CollectStar(new Vector2Int(1, 1));
                flow.PlayerHitTrap();
                Require(flow.Score == 400 && flow.Lives == 3,
                    "Después de la meta ya no deben alterar el intento los marcadores.");
            }
            finally
            {
                if (hadScore) PlayerPrefs.SetInt(scoreKey, previousScore);
                else PlayerPrefs.DeleteKey(scoreKey);
                if (hadTime) PlayerPrefs.SetFloat(timeKey, previousTime);
                else PlayerPrefs.DeleteKey(timeKey);
                PlayerPrefs.Save();
            }

            UnityEngine.Object.DestroyImmediate(flowObject);
            UnityEngine.Object.DestroyImmediate(ballObject);
            UnityEngine.Object.DestroyImmediate(root);
            UnityEngine.Object.DestroyImmediate(map);
        }

        private static Texture2D CreateTestMap()
        {
            var map = new Texture2D(5, 3, TextureFormat.RGBA32, false);
            var wall = new Color(.03f, .06f, .1f);
            for (var y = 0; y < map.height; y++)
            for (var x = 0; x < map.width; x++)
                map.SetPixel(x, y, y == 1 ? Color.white : wall);
            map.SetPixel(1, 1, new Color(.1f, .9f, .4f));
            map.SetPixel(2, 1, Color.yellow);
            map.SetPixel(3, 1, new Color(.15f, .55f, 1f));
            map.SetPixel(4, 1, new Color(.95f, .15f, .28f));
            map.Apply();
            return map;
        }

        private static void Require(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException(message);
        }
    }
}
