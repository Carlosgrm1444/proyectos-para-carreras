using UnityEngine;

namespace LIIA.SphereMaze
{
    public static class LIIAMaterialFactory
    {
        private static Material template;

        public static Material Create(Color color, float emission = 0f, float metallic = 0f, float smoothness = .35f)
        {
            if (template == null) template = Resources.Load<Material>("Materials/LIIAStandard");
            var source = template;
            if (source == null || source.shader == null) return null;

            var material = new Material(source) { color = color };
            if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", Mathf.Clamp01(metallic));
            if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", Mathf.Clamp01(smoothness));
            if (emission > 0f)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color * emission);
            }
            return material;
        }
    }
}
