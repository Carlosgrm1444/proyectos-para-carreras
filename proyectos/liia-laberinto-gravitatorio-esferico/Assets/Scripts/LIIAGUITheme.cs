using UnityEngine;

namespace LIIA.SphereMaze
{
    public static class LIIAGUITheme
    {
        public static readonly Color Panel = new Color(.018f, .055f, .085f, .9f);
        public static readonly Color PanelSoft = new Color(.025f, .085f, .12f, .82f);
        public static readonly Color Gold = new Color(1f, .66f, .2f, 1f);
        public static readonly Color GoldSoft = new Color(.75f, .55f, .28f, .88f);
        public static readonly Color Ivory = new Color(.94f, .95f, .91f, 1f);
        public static readonly Color Muted = new Color(.72f, .8f, .84f, 1f);
        public static readonly Color Blue = new Color(.16f, .68f, 1f, 1f);
        public static readonly Color Purple = new Color(.82f, .3f, 1f, 1f);

        public static float BeginCanvas(float width = 1920f, float height = 1080f)
        {
            var scale = Mathf.Min(Screen.width / width, Screen.height / height);
            var offsetX = (Screen.width - width * scale) * .5f;
            var offsetY = (Screen.height - height * scale) * .5f;
            GUI.matrix = Matrix4x4.TRS(new Vector3(offsetX, offsetY, 0f), Quaternion.identity, Vector3.one * scale);
            return scale;
        }

        public static void EndCanvas()
        {
            GUI.matrix = Matrix4x4.identity;
            GUI.color = Color.white;
        }

        public static void PanelBox(Rect rect, float border = 1.5f, Color? fill = null)
        {
            Fill(rect, GoldSoft);
            Fill(new Rect(rect.x + border, rect.y + border, rect.width - border * 2f, rect.height - border * 2f), fill ?? Panel);
        }

        public static void Fill(Rect rect, Color color)
        {
            var previous = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            GUI.color = previous;
        }

        public static void Line(float x, float y, float width, Color color, float height = 1f)
        {
            Fill(new Rect(x, y, width, height), color);
        }

        public static GUIStyle Label(int size, Color color, FontStyle style = FontStyle.Normal, TextAnchor alignment = TextAnchor.UpperLeft)
        {
            return new GUIStyle(GUI.skin.label)
            {
                fontSize = size,
                fontStyle = style,
                alignment = alignment,
                wordWrap = true,
                normal = { textColor = color }
            };
        }
    }
}
