using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public static class SphericalMazeValidator
    {
        public static bool TryValidate(Texture2D map, out string message)
        {
            if (map == null)
            {
                message = "No se asignó el PNG del laberinto.";
                return false;
            }

            Vector2Int? start = null;
            Vector2Int? goal = null;
            var startCount = 0;
            var goalCount = 0;
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
                if (cell == goal.Value)
                {
                    message = $"Mapa válido: {map.width}×{map.height}, inicio y meta conectados.";
                    return true;
                }

                foreach (var offset in offsets)
                {
                    var next = cell + offset;
                    if (next.x < 0 || next.y < 0 || next.x >= map.width || next.y >= map.height || visited[next.x, next.y]) continue;
                    if (SphericalMazeLayout.DecodeTile(map.GetPixel(next.x, next.y)) == MazeTile.Wall) continue;
                    visited[next.x, next.y] = true;
                    pending.Enqueue(next);
                }
            }

            message = "La meta roja no tiene una ruta transitable desde el inicio verde.";
            return false;
        }
    }
}
