using UnityEditor;
using UnityEngine;

namespace MyNamespace.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;

            SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionFieldName);
            if (conditionProperty != null)
            {
                bool conditionMet = conditionProperty.boolValue == showIf.ShowIfTrue;

                if (conditionMet)
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
            else
            {
                Debug.LogWarning($"Field '{showIf.ConditionFieldName}' not found in {property.serializedObject.targetObject.GetType()}.");
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionFieldName);

            if (conditionProperty != null && conditionProperty.boolValue == showIf.ShowIfTrue)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else
            {
                return 0f;
            }
        }
    }
}
