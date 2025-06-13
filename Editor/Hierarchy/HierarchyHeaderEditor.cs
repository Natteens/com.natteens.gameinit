#if UNITY_EDITOR
using UnityEditor;

namespace GameInit.Editor.Hierarchy
{
    /// <summary>
    /// Editor customizado para o componente HierarchyHeader
    /// </summary>
    [CustomEditor(typeof(GameInit.Hierarchy.HierarchyHeader))]
    public class HierarchyHeaderEditor : UnityEditor.Editor
    {
        private SerializedProperty textColorProp;
        private SerializedProperty fontStyleProp;
        private SerializedProperty textAlignmentProp;
        private SerializedProperty useGradientProp;
        private SerializedProperty backgroundColorProp;
        private SerializedProperty backgroundGradientProp;

        private void OnEnable()
        {
            textColorProp = serializedObject.FindProperty("textColor");
            fontStyleProp = serializedObject.FindProperty("fontStyle");
            textAlignmentProp = serializedObject.FindProperty("textAlignment");
            useGradientProp = serializedObject.FindProperty("useGradient");
            backgroundColorProp = serializedObject.FindProperty("backgroundColor");
            backgroundGradientProp = serializedObject.FindProperty("backgroundGradient");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Configurações de Texto", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(textColorProp);
            EditorGUILayout.PropertyField(fontStyleProp);
            EditorGUILayout.PropertyField(textAlignmentProp);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Configurações de Background", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(useGradientProp);

            EditorGUILayout.PropertyField(useGradientProp.boolValue
                ? backgroundGradientProp
                : backgroundColorProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif