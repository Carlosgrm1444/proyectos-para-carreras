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
            VerifyEsp32Protocol();
            Debug.Log("VERIFICACIÓN LIIA APROBADA: cinco niveles 360°, costura, ciclo de juego, puntuación, récord y protocolo ESP32 correctos.");
        }

        private static void VerifyMaps()
        {
            for (var index = 0; index < MazeLevelCatalog.Count; index++)
            {
                var level = MazeLevelCatalog.Get(index);
                var map = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Resources/Maps/{level.ResourceName}.png");
                Require(map != null, $"No se encontró {level.ResourceName}.png.");
                Require(SphericalMazeValidator.TryValidate(map, out var message, out var safePathLength, level.WrapHorizontally),
                    $"{level.ResourceName}: {message}");
                Require(map.width == 32 && map.height == 21,
                    $"{level.ResourceName} debe usar la resolución final 32×21.");

                var root = new GameObject($"VerifyMap{index + 1}");
                var layout = root.AddComponent<SphericalMazeLayout>();
                layout.Configure(map, 5.08f, level.LongitudeSpan, level.LatitudeSpan, level.WrapHorizontally);
                Require(layout.StarCells.Count == level.ExpectedStars,
                    $"{level.ResourceName} debe tener {level.ExpectedStars} estrellas.");
                Require(layout.CheckpointCells.Count == level.ExpectedCheckpoints,
                    $"{level.ResourceName} debe tener {level.ExpectedCheckpoints} checkpoints.");
                Require(layout.TrapCells.Count == level.ExpectedTraps,
                    $"{level.ResourceName} debe tener {level.ExpectedTraps} trampas.");
                Require(safePathLength >= level.MinimumSafePathLength,
                    $"{level.ResourceName} necesita al menos {level.MinimumSafePathLength} celdas de ruta segura; tiene {safePathLength}.");
                Require(Vector3.Distance(layout.SurfacePoint(-.5f, 10f), layout.SurfacePoint(map.width - .5f, 10f)) < .001f,
                    $"{level.ResourceName} no cierra correctamente la costura de 360°.");
                Require(Mathf.Approximately(layout.NormalizeGridPosition(new Vector2(-.6f, 10f)).x, map.width - .6f),
                    $"{level.ResourceName} no normaliza el cruce de la costura.");
                UnityEngine.Object.DestroyImmediate(root);
                Debug.Log($"Nivel final {index + 1}: {message}");
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
            Require(Mathf.Abs(Vector3.Distance(ball.transform.position, layout.transform.position) - 5.46f) < .001f,
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

        private static void VerifyEsp32Protocol()
        {
            Require(Esp32UsbBridgeTiltInput.TryParsePacket("BTN,42,1,0,0,1", out var sequence, out var direction),
                "Un paquete ESP32 válido debe poder leerse.");
            Require(sequence == 42 && Vector2.Distance(direction, new Vector2(1f, 1f).normalized) < .0001f,
                "El paquete ESP32 debe preservar secuencia y diagonal.");
            Require(!Esp32UsbBridgeTiltInput.TryParsePacket("BTN,1,2,0,0,0", out _, out _),
                "El protocolo ESP32 debe rechazar estados de botón inválidos.");
            Require(!Esp32UsbBridgeTiltInput.TryParsePacket("BTN,1,0,0,0", out _, out _),
                "El protocolo ESP32 debe rechazar paquetes incompletos.");
        }

        private static void Require(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException(message);
        }
    }
}
