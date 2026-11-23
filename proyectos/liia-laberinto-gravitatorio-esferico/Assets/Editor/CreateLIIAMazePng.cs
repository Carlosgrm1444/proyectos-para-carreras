using System.IO;
using UnityEditor;
using UnityEngine;

namespace LIIA.SphereMaze.Editor
{
    public static class CreateLIIAMazePng
    {
        private const string OutputPath = "Assets/Resources/Maps/laberinto_01.png";

        [MenuItem("LIIA/Generar mapa PNG de ejemplo")]
        public static void Generate()
        {
            var rows = new[]
            {
                "#####################",
                "#S....#.............#",
                "#.###.#.###########.#",
                "#...#.#.....#.....#.#",
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
            };
            var width = rows[0].Length;
            var texture = new Texture2D(width, rows.Length, TextureFormat.RGBA32, false);
            for (var row = 0; row < rows.Length; row++)
            {
                if (rows[row].Length != width) throw new System.InvalidOperationException("Todas las filas del mapa deben tener el mismo ancho.");
                for (var x = 0; x < width; x++)
                    texture.SetPixel(x, rows.Length - 1 - row, ColorFor(rows[row][x]));
            }
            texture.Apply(false, false);

            Directory.CreateDirectory(Path.GetDirectoryName(OutputPath));
            File.WriteAllBytes(OutputPath, texture.EncodeToPNG());
            AssetDatabase.ImportAsset(OutputPath, ImportAssetOptions.ForceUpdate);
            var importer = AssetImporter.GetAtPath(OutputPath) as TextureImporter;
            importer.textureType = TextureImporterType.Default;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.isReadable = true;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.SaveAndReimport();
            AssetDatabase.Refresh();
        }

        private static Color ColorFor(char tile)
        {
            switch (tile)
            {
                case '#': return new Color(.03f, .06f, .1f, 1f);
                case 'S': return new Color(.1f, .9f, .4f, 1f);
                case 'G': return new Color(.95f, .15f, .28f, 1f);
                default: return new Color(.85f, .95f, 1f, 1f);
            }
        }
    }
}
