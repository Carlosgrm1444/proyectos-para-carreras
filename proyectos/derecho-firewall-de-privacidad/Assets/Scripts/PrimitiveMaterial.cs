using UnityEngine;

public static class PrimitiveMaterial
{
    private static Shader shader;

    public static void Configure(Shader preferredShader)
    {
        shader = preferredShader != null ? preferredShader : Shader.Find("Firewall/UnlitColor");
    }

    public static Material Apply(Renderer renderer, Color color)
    {
        if (renderer == null) return null;
        if (shader == null) Configure(null);
        var material = new Material(shader) { color = color };
        renderer.material = material;
        return material;
    }
}

