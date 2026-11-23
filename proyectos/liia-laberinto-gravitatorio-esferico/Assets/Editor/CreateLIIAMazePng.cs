using System.IO;
using UnityEditor;
using UnityEngine;

namespace LIIA.SphereMaze.Editor
{
    public static class CreateLIIAMazePng
    {
        private const string OutputDirectory = "Assets/Resources/Maps";
        private static readonly MapDefinition[] Maps =
        {
            new MapDefinition("laberinto_01", new[]
            {
                "#####################",
                "#S.*.C#.............#",
                "#.###.#.###########.#",
                "#..X#.#.....#.....#.#",
                "###.#.#####.#.###.#.#",
                "#...#.....#.#.#...#.#",
                "#.#######.#.#.#.###.#",
                "#.......#.#.#.#.....#",
                "#######.#.#.#.#######",
                "#.....#.#.#.#.......#",
                "#.###.#.#.#.#######.#",
                "#.#...#.#.#.......#.#",
                "#.#.###.#.#######.#.#",
                "#.................#.G",
                "#####################"
            }),
            new MapDefinition("laberinto_02", new[]
            {
                "#####################",
                "#S.*.C..............#",
                "###################.#",
                "#...................#",
                "#.###################",
                "#.......X...........#",
                "###################.#",
                "#...................#",
                "#.###################",
                "#........*..........#",
                "###################.#",
                "#...................#",
                "#.###################",
                "#G..................#",
                "#####################"
            }),
            new MapDefinition("laberinto_03", new[]
            {
                "#####################",
                "#S.*.C....X.........#",
                "###################.#",
                "#...................#",
                "#.###################",
                "#..*....X...........#",
                "###################.#",
                "#...................#",
                "#.###################",
                "#........*.....C....#",
                "###################.#",
                "#.....X.............#",
                "#.###################",
                "#G..................#",
                "#####################"
            })
        };

        [MenuItem("LIIA/Generar mapas PNG de ejemplo")]
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
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.SaveAndReimport();
        }

        private static string MapPath(string mapName) => $"{OutputDirectory}/{mapName}.png";

        private readonly struct MapDefinition
        {
            public MapDefinition(string name, string[] rows) { Name = name; Rows = rows; }
            public string Name { get; }
            public string[] Rows { get; }
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
