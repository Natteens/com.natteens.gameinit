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
        [SerializeField] private FontStyle fontStyle = FontStyle.Bold;
        [SerializeField] private TextAnchor textAlignment = TextAnchor.MiddleCenter;
        [SerializeField] private bool useGradient = true;
        [SerializeField] private Color backgroundColor = Color.red;
        [SerializeField, HideInInspector] private Gradient backgroundGradient;

        public Color TextColor => ForceOpaqueColor(textColor);
        public FontStyle FontStyle => fontStyle;
        public TextAnchor TextAlignment => textAlignment;
        
        public bool UseGradient => useGradient;
        public Color BackgroundColor => ForceOpaqueColor(backgroundColor);
        public Gradient BackgroundGradient => backgroundGradient;

        /// <summary>
        /// Remove a transparência da cor, mantendo apenas os valores RGB
        /// </summary>
        private Color ForceOpaqueColor(Color color)
        {
            return new Color(color.r, color.g, color.b, 1.0f);
        }

        /// <summary>
        /// Inicializa o gradiente padrão se necessário
        /// </summary>
        private void Awake()
        {
            if (backgroundGradient == null)
            {
                InitializeDefaultGradient();
            }
        }

        /// <summary>
        /// Inicializa o gradiente padrão baseado no JSON fornecido
        /// </summary>
        private void InitializeDefaultGradient()
        {
            backgroundGradient = new Gradient();
            
            var colorKeys = new GradientColorKey[3];
            colorKeys[0] = new GradientColorKey(new Color(1.0f, 0.0f, 0.0f, 1.0f), 0.02f);   
            colorKeys[1] = new GradientColorKey(new Color(0.0f, 0.0f, 0.0f, 1.0f), 0.98f);  
            colorKeys[2] = new GradientColorKey(new Color(1.0f, 0.0f, 0.0f, 1.0f), 1.0f);    
            
            var alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);    
            alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);

            backgroundGradient.mode = GradientMode.Fixed;
            backgroundGradient.colorSpace = ColorSpace.Gamma;
            
            backgroundGradient.SetKeys(colorKeys, alphaKeys);
        }

        /// <summary>
        /// Força a atualização visual da hierarchy quando propriedades são modificadas
        /// </summary>
        private void OnValidate()
        {
            if (Application.isPlaying) return;
            
            if (backgroundGradient == null)
            {
                InitializeDefaultGradient();
            }
            
            ForceOpaqueGradient(backgroundGradient);
            
            EditorApplication.delayCall += () =>
            {
                if (this)
                    EditorApplication.RepaintHierarchyWindow();
            };
        }

        /// <summary>
        /// Remove a transparência de todos os pontos do gradiente
        /// </summary>
        private void ForceOpaqueGradient(Gradient gradient)
        {
            if (gradient == null) return;
            
            var colorKeys = gradient.colorKeys;
            var alphaKeys = gradient.alphaKeys;
            
            for (int i = 0; i < alphaKeys.Length; i++)
            {
                alphaKeys[i].alpha = 1.0f;
            }
            
            gradient.SetKeys(colorKeys, alphaKeys);
        }

        /// <summary>
        /// Método para resetar o gradiente para o padrão (útil para testes)
        /// </summary>
        [ContextMenu("Resetar Gradiente para Padrão")]
        private void ResetGradientToDefault()
        {
            InitializeDefaultGradient();
            ForceOpaqueGradient(backgroundGradient);
            
            #if UNITY_EDITOR
            EditorApplication.RepaintHierarchyWindow();
            #endif
        }
    }
}
#endif