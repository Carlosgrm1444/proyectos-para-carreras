using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public enum MazeTile { Wall, Floor, Start, Goal, Star, Checkpoint, Trap }

    /// <summary>Interpreta el PNG de mapa y lo proyecta sobre una banda de la esfera.</summary>
    public sealed class SphericalMazeLayout : MonoBehaviour
    {
        [SerializeField] private Texture2D source;
        [SerializeField] private float radius = 5.08f;
        [SerializeField] private float longitudeSpan = 145f;
        [SerializeField] private float latitudeSpan = 72f;

        private MazeTile[,] tiles;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float Radius => radius;
        public Vector2Int StartCell { get; private set; }
        public Vector2Int GoalCell { get; private set; }
        public List<Vector2Int> StarCells { get; } = new List<Vector2Int>();
        public List<Vector2Int> CheckpointCells { get; } = new List<Vector2Int>();
        public List<Vector2Int> TrapCells { get; } = new List<Vector2Int>();

        public void Configure(Texture2D texture, float surfaceRadius)
        {
            source = texture;
            radius = surfaceRadius;
            Width = source.width;
            Height = source.height;
            tiles = new MazeTile[Width, Height];
            StarCells.Clear();
            CheckpointCells.Clear();
            TrapCells.Clear();

            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                var pixel = source.GetPixel(x, y);
                var tile = DecodeTile(pixel);
                tiles[x, y] = tile;
                if (tile == MazeTile.Start) StartCell = new Vector2Int(x, y);
                if (tile == MazeTile.Goal) GoalCell = new Vector2Int(x, y);
                if (tile == MazeTile.Star) StarCells.Add(new Vector2Int(x, y));
                if (tile == MazeTile.Checkpoint) CheckpointCells.Add(new Vector2Int(x, y));
                if (tile == MazeTile.Trap) TrapCells.Add(new Vector2Int(x, y));
            }
        }

        public MazeTile TileAt(int x, int y)
        {
            if (tiles == null || x < 0 || y < 0 || x >= Width || y >= Height) return MazeTile.Wall;
            return tiles[x, y];
        }

        public bool IsWalkable(int x, int y) => TileAt(x, y) != MazeTile.Wall;

        public bool CanOccupy(Vector2 position, float ballRadiusInCells)
        {
            var offsets = new[]
            {
                Vector2.zero,
                new Vector2(ballRadiusInCells, 0f), new Vector2(-ballRadiusInCells, 0f),
                new Vector2(0f, ballRadiusInCells), new Vector2(0f, -ballRadiusInCells),
                new Vector2(ballRadiusInCells, ballRadiusInCells), new Vector2(-ballRadiusInCells, ballRadiusInCells),
                new Vector2(ballRadiusInCells, -ballRadiusInCells), new Vector2(-ballRadiusInCells, -ballRadiusInCells)
            };
            foreach (var offset in offsets)
            {
                if (!IsWalkable(Mathf.RoundToInt(position.x + offset.x), Mathf.RoundToInt(position.y + offset.y))) return false;
            }
            return true;
        }

        public Vector3 SurfacePoint(float gridX, float gridY, float radialOffset = 0f)
        {
            var longitude = Mathf.Lerp(-longitudeSpan * 0.5f, longitudeSpan * 0.5f, gridX / (Width - 1f)) * Mathf.Deg2Rad;
            var latitude = Mathf.Lerp(-latitudeSpan * 0.5f, latitudeSpan * 0.5f, gridY / (Height - 1f)) * Mathf.Deg2Rad;
            var normal = new Vector3(
                Mathf.Cos(latitude) * Mathf.Sin(longitude),
                Mathf.Sin(latitude),
                Mathf.Cos(latitude) * Mathf.Cos(longitude)).normalized;
            return transform.position + normal * (radius + radialOffset);
        }

        public Vector3 SurfaceNormal(float gridX, float gridY) =>
            (SurfacePoint(gridX, gridY) - transform.position).normalized;

        public static MazeTile DecodeTile(Color pixel)
        {
            if (pixel.g > 0.65f && pixel.r < 0.45f) return MazeTile.Start;
            if (pixel.r > 0.7f && pixel.g > 0.7f && pixel.b < 0.2f) return MazeTile.Star;
            if (pixel.b > 0.55f && pixel.g > 0.1f && pixel.r < 0.4f) return MazeTile.Checkpoint;
            if (pixel.r > 0.45f && pixel.b > 0.45f && pixel.g < 0.35f) return MazeTile.Trap;
            // El importador puede convertir el PNG a espacio lineal; el rojo
            // de meta ya no conserva el .95 original al leerlo con GetPixel.
            if (pixel.r > 0.55f && pixel.g < 0.5f) return MazeTile.Goal;
            return pixel.grayscale > 0.18f ? MazeTile.Floor : MazeTile.Wall;
        }
    }
}
