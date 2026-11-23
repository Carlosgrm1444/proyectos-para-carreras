using System.Collections.Generic;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public enum MazeTile { Wall, Floor, Start, Goal, Star, Checkpoint, Trap }

    /// <summary>Interpreta el PNG y lo proyecta sobre una superficie esférica continua.</summary>
    public sealed class SphericalMazeLayout : MonoBehaviour
    {
        [SerializeField] private Texture2D source;
        [SerializeField] private float radius = 5.08f;
        [SerializeField] private float longitudeSpan = 360f;
        [SerializeField] private float latitudeSpan = 140f;
        [SerializeField] private bool wrapHorizontally = true;

        private MazeTile[,] tiles;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float Radius => radius;
        public float LongitudeSpan => longitudeSpan;
        public float LatitudeSpan => latitudeSpan;
        public bool WrapHorizontally => wrapHorizontally;
        public Texture2D SourceTexture => source;
        public Vector2Int StartCell { get; private set; }
        public Vector2Int GoalCell { get; private set; }
        public List<Vector2Int> StarCells { get; } = new List<Vector2Int>();
        public List<Vector2Int> CheckpointCells { get; } = new List<Vector2Int>();
        public List<Vector2Int> TrapCells { get; } = new List<Vector2Int>();

        public void Configure(
            Texture2D texture,
            float surfaceRadius,
            float horizontalSpan = 360f,
            float verticalSpan = 140f,
            bool horizontalWrap = true)
        {
            source = texture;
            radius = surfaceRadius;
            longitudeSpan = Mathf.Clamp(horizontalSpan, 10f, 360f);
            latitudeSpan = Mathf.Clamp(verticalSpan, 10f, 170f);
            wrapHorizontally = horizontalWrap;
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
            if (tiles == null || y < 0 || y >= Height) return MazeTile.Wall;
            if (wrapHorizontally) x = WrapColumn(x);
            else if (x < 0 || x >= Width) return MazeTile.Wall;
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

        public Vector2 NormalizeGridPosition(Vector2 position)
        {
            if (!wrapHorizontally || Width <= 0) return position;
            var minimum = -.5f;
            var maximum = Width - .5f;
            while (position.x < minimum) position.x += Width;
            while (position.x >= maximum) position.x -= Width;
            return position;
        }

        public Vector2Int NormalizeCell(Vector2Int cell)
        {
            if (wrapHorizontally) cell.x = WrapColumn(cell.x);
            return cell;
        }

        public Vector3 SurfacePoint(float gridX, float gridY, float radialOffset = 0f)
        {
            var longitudeT = wrapHorizontally
                ? (gridX + .5f) / Width
                : gridX / Mathf.Max(1f, Width - 1f);
            var latitudeT = (gridY + .5f) / Height;
            var longitude = Mathf.Lerp(-longitudeSpan * .5f, longitudeSpan * .5f, longitudeT) * Mathf.Deg2Rad;
            var latitude = Mathf.Lerp(-latitudeSpan * .5f, latitudeSpan * .5f, latitudeT) * Mathf.Deg2Rad;
            var normal = new Vector3(
                Mathf.Cos(latitude) * Mathf.Sin(longitude),
                Mathf.Sin(latitude),
                Mathf.Cos(latitude) * Mathf.Cos(longitude)).normalized;
            return transform.position + transform.TransformDirection(normal) * (radius + radialOffset);
        }

        public Vector3 SurfaceNormal(float gridX, float gridY) =>
            (SurfacePoint(gridX, gridY) - transform.position).normalized;

        private int WrapColumn(int column)
        {
            column %= Width;
            return column < 0 ? column + Width : column;
        }

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
