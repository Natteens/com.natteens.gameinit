using GameInit.Hierarchy;
using UnityEditor;
using UnityEngine;

namespace GameInit.Editor.Hierarchy
{
    [CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
    public class RequiredFieldDrawer : PropertyDrawer {
        private readonly Texture2D requiredIcon = EditorGUIUtility.IconContent("console.erroricon.sml").image as Texture2D;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
        
            EditorGUI.BeginChangeCheck();
        
            Rect fieldRect = new (position.x, position.y, position.width - 20, position.height);
            EditorGUI.PropertyField(fieldRect, property, label);
        
            if (IsFieldUnassigned(property)) {
                Rect iconRect = new (position.xMax - 18, fieldRect.y - 8f, 32, 32);
                GUI.Label(iconRect, new GUIContent(requiredIcon, "This field is required and is either missing or empty!"));
            }

            if (EditorGUI.EndChangeCheck()) {
                property.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            
                EditorApplication.RepaintHierarchyWindow();
            }
        
            EditorGUI.EndProperty();
        }

        bool IsFieldUnassigned(SerializedProperty property) {
            switch (property.propertyType) {
                case SerializedPropertyType.ObjectReference when property.objectReferenceValue:
                case SerializedPropertyType.ExposedReference when property.exposedReferenceValue:
                case SerializedPropertyType.AnimationCurve when property.animationCurveValue is { length: > 0 }:
                case SerializedPropertyType.String when !string.IsNullOrEmpty( property.stringValue ):
                    return false;
                default:
                    return true;
            }
        }
    }
}