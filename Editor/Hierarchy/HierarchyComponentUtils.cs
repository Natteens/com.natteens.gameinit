#if UNITY_EDITOR
using UnityEngine;
using System.Linq;

namespace GameInit.Editor.Hierarchy
{
    /// <summary>
    /// Utilitários para trabalhar com componentes na hierarquia
    /// </summary>
    public static class HierarchyComponentUtils
    {
        /// <summary>
        /// Obtém o componente representativo para exibição do ícone
        /// </summary>
        /// <param name="gameObject">GameObject alvo</param>
        /// <returns>Componente para exibir o ícone, ou null se não encontrado</returns>
        public static Component GetRepresentativeComponent(GameObject gameObject)
        {
            if (gameObject == null)
                return null;
            
            Component[] components = gameObject.GetComponents<Component>();
            if (components == null || components.Length == 0)
                return null;
            
            Component[] validComponents = components
                .Where(comp => comp != null && !HierarchyIconSettings.ShouldIgnoreComponent(comp.GetType()))
                .ToArray();
            
            return validComponents.Length > 0 ? validComponents[0] : null;
        }
        
        /// <summary>
        /// Cria o conteúdo GUI para o componente
        /// </summary>
        /// <param name="component">Componente alvo</param>
        /// <returns>GUIContent configurado ou null se inválido</returns>
        public static GUIContent CreateComponentGUIContent(Component component)
        {
            if (component == null)
                return null;
            
            System.Type componentType = component.GetType();
            GUIContent content = UnityEditor.EditorGUIUtility.ObjectContent(component, componentType);
            
            if (content.image == null)
                return null;
            
            content.text = null;
            content.tooltip = componentType.Name;
            
            return content;
        }
    }
}
#endif