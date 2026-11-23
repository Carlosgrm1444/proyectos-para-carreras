using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public static class SphericalMazeValidator
    {
        public static bool TryValidate(Texture2D map, out string message, bool wrapHorizontally = true)
        {
            return TryValidate(map, out message, out _, wrapHorizontally);
        }

        public static bool TryValidate(Texture2D map, out string message, out int safePathLength, bool wrapHorizontally = true)
        {
            safePathLength = -1;
            if (map == null)
            {
                message = "No se asignó el PNG del laberinto.";
                return false;
            }

            Vector2Int? start = null;
            Vector2Int? goal = null;
            var startCount = 0;
            var goalCount = 0;
            var starCount = 0;
            var checkpointCount = 0;
            var trapCount = 0;
            var gameplayCells = new List<Vector2Int>();
            var safeObjectiveCells = new List<Vector2Int>();
            for (var y = 0; y < map.height; y++)
            for (var x = 0; x < map.width; x++)
            {
                switch (SphericalMazeLayout.DecodeTile(map.GetPixel(x, y)))
                {
                    case MazeTile.Start:
                        startCount++;
                        if (start.HasValue)
                        {
                            message = "El PNG tiene más de un inicio verde.";
                            return false;
                        }
                        start = new Vector2Int(x, y);
                        break;
                    case MazeTile.Goal:
                        goalCount++;
                        if (goal.HasValue)
                        {
                            message = "El PNG tiene más de una meta roja.";
                            return false;
                        }
                        goal = new Vector2Int(x, y);
                        break;
                    case MazeTile.Star:
                        starCount++;
                        gameplayCells.Add(new Vector2Int(x, y));
                        safeObjectiveCells.Add(new Vector2Int(x, y));
                        break;
                    case MazeTile.Checkpoint:
                        checkpointCount++;
                        gameplayCells.Add(new Vector2Int(x, y));
                        safeObjectiveCells.Add(new Vector2Int(x, y));
                        break;
                    case MazeTile.Trap:
                        trapCount++;
                        gameplayCells.Add(new Vector2Int(x, y));
                        break;
                }
            }

            if (!start.HasValue || !goal.HasValue)
            {
                message = $"El PNG necesita exactamente un inicio verde y una meta roja. Detectados: inicio={startCount}, meta={goalCount}.";
                return false;
            }

            var visited = new bool[map.width, map.height];
            var pending = new Queue<Vector2Int>();
            pending.Enqueue(start.Value);
            visited[start.Value.x, start.Value.y] = true;
            var offsets = new[] { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
            while (pending.Count > 0)
            {
                var cell = pending.Dequeue();
                foreach (var offset in offsets)
                {
                    var next = cell + offset;
                    if (wrapHorizontally)
                    {
                        next.x %= map.width;
                        if (next.x < 0) next.x += map.width;
                    }
                    if (next.x < 0 || next.y < 0 || next.x >= map.width || next.y >= map.height || visited[next.x, next.y]) continue;
                    if (SphericalMazeLayout.DecodeTile(map.GetPixel(next.x, next.y)) == MazeTile.Wall) continue;
                    visited[next.x, next.y] = true;
                    pending.Enqueue(next);
                }
            }

            if (!visited[goal.Value.x, goal.Value.y])
            {
                message = "La meta roja no tiene una ruta transitable desde el inicio verde.";
                return false;
            }

            foreach (var gameplayCell in gameplayCells)
            {
                if (visited[gameplayCell.x, gameplayCell.y]) continue;
                message = $"Hay un marcador de juego aislado en ({gameplayCell.x}, {gameplayCell.y}).";
                return false;
            }

            var safeVisited = new bool[map.width, map.height];
            var safeDistance = new int[map.width, map.height];
            pending.Clear();
            pending.Enqueue(start.Value);
            safeVisited[start.Value.x, start.Value.y] = true;
            while (pending.Count > 0)
            {
                var cell = pending.Dequeue();
                foreach (var offset in offsets)
                {
                    var next = cell + offset;
                    if (wrapHorizontally)
                    {
                        next.x %= map.width;
                        if (next.x < 0) next.x += map.width;
                    }
                    if (next.x < 0 || next.y < 0 || next.x >= map.width || next.y >= map.height || safeVisited[next.x, next.y]) continue;
                    var tile = SphericalMazeLayout.DecodeTile(map.GetPixel(next.x, next.y));
                    if (tile == MazeTile.Wall || tile == MazeTile.Trap) continue;
                    safeVisited[next.x, next.y] = true;
                    safeDistance[next.x, next.y] = safeDistance[cell.x, cell.y] + 1;
                    pending.Enqueue(next);
                }
            }

            if (!safeVisited[goal.Value.x, goal.Value.y])
            {
                message = "La meta sólo puede alcanzarse atravesando una trampa; debe existir una ruta segura.";
                return false;
            }
            foreach (var objective in safeObjectiveCells)
            {
                if (safeVisited[objective.x, objective.y]) continue;
                message = $"La estrella o checkpoint en ({objective.x}, {objective.y}) queda bloqueado detrás de una trampa.";
                return false;
            }

            safePathLength = safeDistance[goal.Value.x, goal.Value.y];
            message = $"Mapa válido: {map.width}×{map.height}, ruta segura mínima={safePathLength} celdas, " +
                      $"estrellas={starCount}, checkpoints={checkpointCount}, trampas={trapCount}, " +
                      $"costura 360°={(wrapHorizontally ? "activa" : "cerrada")}.";
            return true;
        }
    }
}
