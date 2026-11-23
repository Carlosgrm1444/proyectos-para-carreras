using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LIIA.SphereMaze.Editor
{
    public static class CreateLIIAMazePng
    {
        private const string OutputDirectory = "Assets/Resources/Maps";
        private const int FinalMapWidth = 32;
        private const int FinalMapHeight = 21;
        private static readonly MapDefinition[] Maps = CreateFinalMaps();

        [MenuItem("LIIA/Generar mapas PNG finales")]
        public static void Generate()
        {
            foreach (var map in Maps) GenerateMap(map);
            AssetDatabase.Refresh();
        }

        [MenuItem("LIIA/Validar mapas PNG activos")]
        public static void ValidateActiveMap()
        {
            foreach (var definition in Maps)
            {
                var path = MapPath(definition.Name);
                var map = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                if (!SphericalMazeValidator.TryValidate(map, out var message))
                    throw new System.InvalidOperationException($"{definition.Name}: {message}");
                Debug.Log($"{definition.Name}: {message}");
            }
        }

        private static void GenerateMap(MapDefinition definition)
        {
            var rows = definition.Rows;
            var width = rows[0].Length;
            var texture = new Texture2D(width, rows.Length, TextureFormat.RGBA32, false);
            for (var row = 0; row < rows.Length; row++)
            {
                if (rows[row].Length != width) throw new System.InvalidOperationException("Todas las filas del mapa deben tener el mismo ancho.");
                for (var x = 0; x < width; x++)
                    texture.SetPixel(x, rows.Length - 1 - row, ColorFor(rows[row][x]));
            }
            texture.Apply(false, false);

            var outputPath = MapPath(definition.Name);
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            File.WriteAllBytes(outputPath, texture.EncodeToPNG());
            AssetDatabase.ImportAsset(outputPath, ImportAssetOptions.ForceUpdate);
            var importer = AssetImporter.GetAtPath(outputPath) as TextureImporter;
            importer.textureType = TextureImporterType.Default;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.isReadable = true;
            importer.wrapMode = TextureWrapMode.Repeat;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.SaveAndReimport();
        }

        private static string MapPath(string mapName) => $"{OutputDirectory}/{mapName}.png";

        private static MapDefinition[] CreateFinalMaps()
        {
            return new[]
            {
                CreateOrbitLevel(),
                CreateDoubleHelixLevel(),
                CreateMeridianLevel(),
                CreateNeuralNetworkLevel(),
                CreateSingularityLevel()
            };
        }

        private static MapDefinition CreateOrbitLevel()
        {
            var map = new MapCanvas(FinalMapWidth, FinalMapHeight);
            map.CarvePath(0,
                P(2, 3), P(6, 3), P(6, 5), P(10, 5), P(10, 7), P(14, 7), P(14, 9), P(18, 9),
                P(18, 11), P(22, 11), P(22, 13), P(26, 13), P(26, 15), P(30, 15), P(30, 17), P(34, 17));
            map.CarvePath(0, P(14, 9), P(14, 11), P(18, 11));
            map.Place(2, 3, 'S');
            map.Place(34, 17, 'G');
            map.Place(5, 3, '*');
            map.Place(9, 5, '*');
            map.Place(25, 13, '*');
            map.Place(18, 9, 'C');
            map.Place(14, 10, 'X');
            return new MapDefinition("laberinto_01", map.ToRows());
        }

        private static MapDefinition CreateDoubleHelixLevel()
        {
            var map = new MapCanvas(FinalMapWidth, FinalMapHeight);
            map.CarvePath(0,
                P(3, 2), P(11, 2), P(11, 4), P(19, 4), P(19, 6), P(27, 6), P(27, 8), P(35, 8),
                P(35, 10), P(43, 10), P(43, 12), P(51, 12), P(51, 14), P(59, 14), P(59, 16),
                P(67, 16), P(67, 18));
            map.CarvePath(0, P(19, 4), P(23, 4), P(23, 8), P(27, 8));
            map.CarvePath(0, P(43, 10), P(47, 10), P(47, 14), P(51, 14));
            map.Place(3, 2, 'S');
            map.Place(67, 18, 'G');
            map.Place(8, 2, '*');
            map.Place(23, 7, '*');
            map.Place(47, 13, '*');
            map.Place(63, 16, '*');
            map.Place(35, 8, 'C');
            map.Place(59, 14, 'C');
            map.Place(23, 5, 'X');
            map.Place(47, 11, 'X');
            return new MapDefinition("laberinto_02", map.ToRows());
        }

        private static MapDefinition CreateMeridianLevel()
        {
            var map = new MapCanvas(FinalMapWidth, FinalMapHeight);
            map.CarvePath(0,
                P(0, 2), P(6, 2), P(6, 18), P(12, 18), P(12, 2), P(18, 2), P(18, 18),
                P(24, 18), P(24, 2), P(30, 2), P(30, 18));
            map.CarvePath(0, P(6, 6), P(12, 6));
            map.CarvePath(0, P(12, 10), P(18, 10));
            map.CarvePath(0, P(18, 14), P(24, 14));
            map.Place(0, 2, 'S');
            map.Place(30, 18, 'G');
            map.Place(6, 10, '*');
            map.Place(12, 10, '*');
            map.Place(18, 10, '*');
            map.Place(24, 10, '*');
            map.Place(30, 10, '*');
            map.Place(12, 18, 'C');
            map.Place(24, 2, 'C');
            map.Place(9, 6, 'X');
            map.Place(15, 10, 'X');
            map.Place(21, 14, 'X');
            return new MapDefinition("laberinto_03", map.ToRows());
        }

        private static MapDefinition CreateNeuralNetworkLevel()
        {
            var map = new MapCanvas(FinalMapWidth, FinalMapHeight);
            map.CarvePath(0,
                P(1, 2), P(13, 2), P(13, 4), P(25, 4), P(25, 6), P(37, 6), P(37, 8),
                P(49, 8), P(49, 10), P(61, 10), P(61, 12), P(73, 12), P(73, 14),
                P(85, 14), P(85, 16), P(97, 16), P(97, 18));
            map.CarvePath(0, P(13, 4), P(17, 4), P(17, 8), P(37, 8));
            map.CarvePath(0, P(49, 10), P(53, 10), P(53, 14), P(73, 14));
            map.CarvePath(0, P(73, 12), P(77, 12), P(77, 16), P(97, 16));
            map.CarvePath(0, P(7, 2), P(7, 4));
            map.Place(1, 2, 'S');
            map.Place(97, 18, 'G');
            map.Place(7, 2, '*');
            map.Place(21, 4, '*');
            map.Place(33, 6, '*');
            map.Place(45, 8, '*');
            map.Place(69, 12, '*');
            map.Place(89, 16, '*');
            map.Place(37, 6, 'C');
            map.Place(61, 10, 'C');
            map.Place(85, 14, 'C');
            map.Place(17, 6, 'X');
            map.Place(53, 12, 'X');
            map.Place(77, 14, 'X');
            map.Place(7, 4, 'X');
            return new MapDefinition("laberinto_04", map.ToRows());
        }

        private static MapDefinition CreateSingularityLevel()
        {
            var map = new MapCanvas(FinalMapWidth, FinalMapHeight);
            map.CarvePath(0,
                P(1, 1), P(17, 1), P(17, 3), P(33, 3), P(33, 5), P(49, 5), P(49, 7),
                P(65, 7), P(65, 9), P(81, 9), P(81, 11), P(97, 11), P(97, 13),
                P(113, 13), P(113, 15), P(129, 15), P(129, 19));
            map.CarvePath(0, P(17, 3), P(21, 3), P(21, 7), P(49, 7));
            map.CarvePath(0, P(65, 9), P(69, 9), P(69, 13), P(97, 13));
            map.CarvePath(0, P(113, 15), P(117, 15), P(117, 19), P(129, 19));
            map.CarvePath(0, P(9, 1), P(9, 3));
            map.CarvePath(0, P(73, 9), P(73, 11));
            map.Place(1, 1, 'S');
            map.Place(129, 19, 'G');
            map.Place(9, 1, '*');
            map.Place(25, 3, '*');
            map.Place(41, 5, '*');
            map.Place(57, 7, '*');
            map.Place(73, 9, '*');
            map.Place(89, 11, '*');
            map.Place(105, 13, '*');
            map.Place(49, 5, 'C');
            map.Place(81, 9, 'C');
            map.Place(113, 13, 'C');
            map.Place(21, 5, 'X');
            map.Place(69, 11, 'X');
            map.Place(117, 17, 'X');
            map.Place(9, 3, 'X');
            map.Place(73, 11, 'X');
            return new MapDefinition("laberinto_05", map.ToRows());
        }

        private static Vector2Int P(int x, int y) => new Vector2Int(x, y);

        private readonly struct MapDefinition
        {
            public MapDefinition(string name, string[] rows) { Name = name; Rows = rows; }
            public string Name { get; }
            public string[] Rows { get; }
        }

        private sealed class MapCanvas
        {
            private readonly char[,] cells;
            private readonly int width;
            private readonly int height;

            public MapCanvas(int mapWidth, int mapHeight)
            {
                width = mapWidth;
                height = mapHeight;
                cells = new char[width, height];
                for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    cells[x, y] = '#';
            }

            public void CarvePath(int halfWidth, params Vector2Int[] points)
            {
                if (points == null || points.Length < 2) throw new InvalidOperationException("Una ruta necesita al menos dos puntos.");
                for (var index = 1; index < points.Length; index++)
                    CarveLine(points[index - 1], points[index], halfWidth);
            }

            public void Place(int x, int y, char marker)
            {
                var wrappedX = Wrap(x);
                if (y < 0 || y >= height || cells[wrappedX, y] == '#')
                    throw new InvalidOperationException($"El marcador {marker} no está sobre una ruta en ({x}, {y}).");
                cells[wrappedX, y] = marker;
            }

            public string[] ToRows()
            {
                var result = new string[height];
                for (var row = 0; row < height; row++)
                {
                    var y = height - 1 - row;
                    var data = new char[width];
                    for (var x = 0; x < width; x++) data[x] = cells[x, y];
                    result[row] = new string(data);
                }
                return result;
            }

            private void CarveLine(Vector2Int from, Vector2Int to, int halfWidth)
            {
                var x = from.x;
                var y = from.y;
                var dx = Mathf.Abs(to.x - from.x);
                var dy = Mathf.Abs(to.y - from.y);
                var stepX = from.x < to.x ? 1 : -1;
                var stepY = from.y < to.y ? 1 : -1;
                var error = dx - dy;

                while (true)
                {
                    for (var offsetY = -halfWidth; offsetY <= halfWidth; offsetY++)
                    for (var offsetX = -halfWidth; offsetX <= halfWidth; offsetX++)
                        SetFloor(x + offsetX, y + offsetY);
                    if (x == to.x && y == to.y) break;
                    var doubleError = error * 2;
                    if (doubleError > -dy) { error -= dy; x += stepX; }
                    if (doubleError < dx) { error += dx; y += stepY; }
                }
            }

            private void SetFloor(int x, int y)
            {
                if (y <= 0 || y >= height - 1) return;
                cells[Wrap(x), y] = '.';
            }

            private int Wrap(int x)
            {
                x %= width;
                return x < 0 ? x + width : x;
            }
        }

        private static Color ColorFor(char tile)
        {
            switch (tile)
            {
                case '#': return new Color(.03f, .06f, .1f, 1f);
                case 'S': return new Color(.1f, .9f, .4f, 1f);
                case 'G': return new Color(.95f, .15f, .28f, 1f);
                case '*': return Color.yellow;
                case 'C': return new Color(.15f, .55f, 1f, 1f);
                case 'X': return new Color(.85f, .1f, .9f, 1f);
                default: return new Color(.85f, .95f, 1f, 1f);
            }
        }
    }
}
