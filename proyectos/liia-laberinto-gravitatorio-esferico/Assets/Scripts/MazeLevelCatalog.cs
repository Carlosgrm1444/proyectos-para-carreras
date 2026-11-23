using UnityEngine;

namespace LIIA.SphereMaze
{
    public readonly struct MazeLevelInfo
    {
        public MazeLevelInfo(
            string resourceName,
            string displayName,
            string description,
            float latitudeSpan,
            float parTimeSeconds,
            int minimumSafePathLength,
            int expectedStars,
            int expectedCheckpoints,
            int expectedTraps)
        {
            ResourceName = resourceName;
            DisplayName = displayName;
            Description = description;
            LatitudeSpan = latitudeSpan;
            ParTimeSeconds = parTimeSeconds;
            MinimumSafePathLength = minimumSafePathLength;
            ExpectedStars = expectedStars;
            ExpectedCheckpoints = expectedCheckpoints;
            ExpectedTraps = expectedTraps;
        }

        public string ResourceName { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public float LongitudeSpan => 360f;
        public float LatitudeSpan { get; }
        public float ParTimeSeconds { get; }
        public int MinimumSafePathLength { get; }
        public bool WrapHorizontally => true;
        public int ExpectedStars { get; }
        public int ExpectedCheckpoints { get; }
        public int ExpectedTraps { get; }
    }

    public static class MazeLevelCatalog
    {
        private const string SelectionKey = "LIIA_SELECTED_LEVEL";
        private static readonly MazeLevelInfo[] Levels =
        {
            new MazeLevelInfo("laberinto_01", "Órbita de arranque", "Una vuelta completa para dominar la costura del planeta.", 132f, 60f, 40, 3, 1, 1),
            new MazeLevelInfo("laberinto_02", "Doble hélice", "Dos órbitas enlazadas con atajos de riesgo.", 138f, 75f, 70, 4, 2, 2),
            new MazeLevelInfo("laberinto_03", "Cruce de meridianos", "Alterna entre hemisferios por corredores verticales.", 144f, 90f, 100, 5, 2, 3),
            new MazeLevelInfo("laberinto_04", "Red neuronal", "Una red de rutas seguras y conexiones de alto valor.", 146f, 105f, 110, 6, 3, 4),
            new MazeLevelInfo("laberinto_05", "Singularidad", "El circuito final: cuatro vueltas y máxima presión.", 150f, 120f, 135, 7, 3, 5)
        };

        public static int Count => Levels.Length;
        public static int SelectedIndex => Mathf.Clamp(PlayerPrefs.GetInt(SelectionKey, 0), 0, Levels.Length - 1);
        public static MazeLevelInfo Selected => Levels[SelectedIndex];
        public static MazeLevelInfo Get(int index) => Levels[Mathf.Clamp(index, 0, Levels.Length - 1)];

        public static void Select(int index)
        {
            PlayerPrefs.SetInt(SelectionKey, Mathf.Clamp(index, 0, Levels.Length - 1));
            PlayerPrefs.Save();
        }
    }
}
