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
        /// Renderiza um header customizado na hierarchy com estilo e cores específicas
        /// </summary>
        public static void RenderHeader(Rect rect, string objectName, HierarchyHeader header)
        {
            var style = CreateHeaderStyle(header.TextColor);
            var displayName = FormatHeaderName(objectName);
            
            EditorGUI.DrawRect(rect, header.BackgroundColor);
            EditorGUI.LabelField(rect, displayName, style);
        }

        /// <summary>
        /// Cria o estilo visual para o header customizado
        /// </summary>
        private static GUIStyle CreateHeaderStyle(Color textColor)
        {
            return new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
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