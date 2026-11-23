using UnityEngine;

namespace LIIA.SphereMaze
{
    public readonly struct MazeLevelInfo
    {
        public MazeLevelInfo(string resourceName, string displayName, string description)
        {
            ResourceName = resourceName;
            DisplayName = displayName;
            Description = description;
        }

        public string ResourceName { get; }
        public string DisplayName { get; }
        public string Description { get; }
    }

    public static class MazeLevelCatalog
    {
        private const string SelectionKey = "LIIA_SELECTED_LEVEL";
        private static readonly MazeLevelInfo[] Levels =
        {
            new MazeLevelInfo("laberinto_01", "Ruta de inicio", "Explora estrella, checkpoint y trampa."),
            new MazeLevelInfo("laberinto_02", "Circuito orbital", "Un recorrido largo para dominar la inercia."),
            new MazeLevelInfo("laberinto_03", "Desafío de datos", "Más riesgos y recompensas en el mismo trayecto.")
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
