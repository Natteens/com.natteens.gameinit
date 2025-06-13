#if UNITY_EDITOR
using UnityEngine;

namespace GameInit.Editor.Hierarchy
{
    /// <summary>
    /// Configurações centralizadas para o sistema de ícones da hierarquia
    /// </summary>
    public static class HierarchyIconSettings
    {
        private static readonly System.Type[] IgnoredComponentTypes = 
        {
            typeof(Transform),
            typeof(RectTransform),
            typeof(CanvasRenderer)
        };
        
        /// <summary>
        /// Verifica se o tipo de componente deve ser ignorado
        /// </summary>
        public static bool ShouldIgnoreComponent(System.Type componentType)
        {
            return System.Array.Exists(IgnoredComponentTypes, type => type == componentType);
        }
    }
}
#endif