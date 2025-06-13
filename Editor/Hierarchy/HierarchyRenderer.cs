#if UNITY_EDITOR
using GameInit.Hierarchy;
using UnityEditor;
using UnityEngine;

namespace GameInit.Editor.Hierarchy
{
    /// <summary>
    /// Responsável pela renderização visual dos elementos customizados na hierarchy
    /// </summary>
    public static class HierarchyRenderer
    {
        /// <summary>
        /// Renderiza um header customizado na hierarchy com estilo e cores/gradientes específicas
        /// </summary>
        public static void RenderHeader(Rect rect, string objectName, HierarchyHeader header)
        {
            RenderBackground(rect, header);
            
            RenderText(rect, objectName, header);
        }

        /// <summary>
        /// Renderiza o background do header (cor sólida ou gradiente)
        /// </summary>
        private static void RenderBackground(Rect rect, HierarchyHeader header)
        {
            if (header.UseGradient)
            {
                RenderGradientBackground(rect, header.BackgroundGradient);
            }
            else
            {
                EditorGUI.DrawRect(rect, header.BackgroundColor);
            }
        }

        /// <summary>
        /// Renderiza um gradiente como background
        /// </summary>
        private static void RenderGradientBackground(Rect rect, Gradient gradient)
        {
            if (gradient == null) return;
            
            var texture = CreateGradientTexture(gradient, (int)rect.width, (int)rect.height);
            GUI.DrawTexture(rect, texture);
            Object.DestroyImmediate(texture);
        }

        /// <summary>
        /// Renderiza o texto do header com cor sólida
        /// </summary>
        private static void RenderText(Rect rect, string objectName, HierarchyHeader header)
        {
            var displayName = FormatHeaderName(objectName);
            var style = CreateHeaderStyle(header.TextColor, header.FontStyle, header.TextAlignment);
            
            EditorGUI.LabelField(rect, displayName, style);
        }

        /// <summary>
        /// Cria uma textura com gradiente
        /// </summary>
        private static Texture2D CreateGradientTexture(Gradient gradient, int width, int height)
        {
            var texture = new Texture2D(width, height);
            var pixels = new Color[width * height];
            
            for (int x = 0; x < width; x++)
            {
                var t = width > 1 ? (float)x / (width - 1) : 0f;
                var color = gradient.Evaluate(t);
                color.a = 1.0f;
                
                for (int y = 0; y < height; y++)
                {
                    pixels[y * width + x] = color;
                }
            }
            
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// Cria o estilo visual para o header customizado
        /// </summary>
        private static GUIStyle CreateHeaderStyle(Color textColor, FontStyle fontStyle, TextAnchor alignment)
        {
            return new GUIStyle
            {
                alignment = alignment,
                fontStyle = fontStyle,
                normal = new GUIStyleState { textColor = textColor }
            };
        }

        /// <summary>
        /// Formata o nome do objeto para exibição no header
        /// </summary>
        private static string FormatHeaderName(string name)
        {
            return name.ToUpper();
        }
    }
}
#endif