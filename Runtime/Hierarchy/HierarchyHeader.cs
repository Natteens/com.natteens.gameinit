#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameInit.Hierarchy
{
    /// <summary>
    /// Componente que define as propriedades visuais de um header customizado na hierarchy
    /// </summary>
    public class HierarchyHeader : MonoBehaviour
    {
        [SerializeField] private Color textColor = Color.white;
        [SerializeField] private Color backgroundColor = Color.red;

        public Color TextColor => textColor;
        public Color BackgroundColor => backgroundColor;

        /// <summary>
        /// Força a atualização visual da hierarchy quando propriedades são modificadas
        /// </summary>
        private void OnValidate()
        {
            if (Application.isPlaying) return;
            
            EditorApplication.delayCall += () =>
            {
                if (this)
                    EditorApplication.RepaintHierarchyWindow();
            };
        }
    }
}
#endif