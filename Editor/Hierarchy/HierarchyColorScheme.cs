#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameInit.Editor.Hierarchy
{
    /// <summary>
    /// Esquema de cores para elementos da hierarquia baseado no tema do Unity
    /// </summary>
    public static class HierarchyColorScheme
    {
        private readonly struct ColorPair
        {
            private readonly Color Light;
            private readonly Color Dark;
            
            public ColorPair(Color light, Color dark)
            {
                Light = light;
                Dark = dark;
            }
            
            public Color GetCurrent() => EditorGUIUtility.isProSkin ? Dark : Light;
        }
        
        private static readonly ColorPair DefaultColors = new(
            new Color(0.7843f, 0.7843f, 0.7843f),
            new Color(0.2196f, 0.2196f, 0.2196f)
        );
        
        private static readonly ColorPair SelectedColors = new(
            new Color(0.2274f, 0.447f, 0.6902f),
            new Color(0.1725f, 0.3647f, 0.5294f)
        );
        
        private static readonly ColorPair SelectedUnfocusedColors = new(
            new Color(0.68f, 0.68f, 0.68f),
            new Color(0.3f, 0.3f, 0.3f)
        );
        
        private static readonly ColorPair HoveredColors = new(
            new Color(0.698f, 0.698f, 0.698f),
            new Color(0.2706f, 0.2706f, 0.2706f)
        );
        
        /// <summary>
        /// Obtém a cor de fundo apropriada baseada no estado do elemento
        /// </summary>
        /// <param name="state">Estado atual do elemento na hierarquia</param>
        /// <returns>Cor de fundo apropriada</returns>
        public static Color GetBackgroundColor(HierarchyElementState state)
        {
            return state switch
            {
                { IsSelected: true, IsWindowFocused: true } => SelectedColors.GetCurrent(),
                { IsSelected: true, IsWindowFocused: false } => SelectedUnfocusedColors.GetCurrent(),
                { IsHovered: true } => HoveredColors.GetCurrent(),
                _ => DefaultColors.GetCurrent()
            };
        }
    }
    
    /// <summary>
    /// Representa o estado de um elemento na hierarquia
    /// </summary>
    public readonly struct HierarchyElementState
    {
        public readonly bool IsSelected;
        public readonly bool IsHovered;
        public readonly bool IsWindowFocused;
        
        public HierarchyElementState(bool isSelected, bool isHovered, bool isWindowFocused)
        {
            IsSelected = isSelected;
            IsHovered = isHovered;
            IsWindowFocused = isWindowFocused;
        }
    }
}
#endif