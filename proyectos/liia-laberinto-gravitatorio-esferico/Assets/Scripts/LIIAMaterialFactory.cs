using UnityEngine;

namespace LIIA.SphereMaze
{
    public static class LIIAMaterialFactory
    {
        private static Material template;

        public static Material Create(Color color, float emission = 0f)
        {
            if (template == null) template = Resources.Load<Material>("Materials/LIIAStandard");
            var source = template;
            if (source == null || source.shader == null) return null;

            var material = new Material(source) { color = color };
            if (emission > 0f)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color * emission);
            }
            return material;
        }
    }
}
