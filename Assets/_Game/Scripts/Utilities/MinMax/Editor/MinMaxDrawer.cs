using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMax))]
public class MinMaxDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        Rect minRect = new Rect(position.x, position.y, 30, position.height);
        Rect maxRect = new Rect(position.x + 35, position.y, 50, position.height);

        float min = property.FindPropertyRelative("min").floatValue;
        float max = property.FindPropertyRelative("max").floatValue;

        GUIContent[] labels = new GUIContent[2] { new GUIContent("Min"), new GUIContent("Max") };
        float[] values = new float[2] { min, max };

        EditorGUI.BeginChangeCheck();

        EditorGUI.MultiFloatField(position, new GUIContent(""), labels, values);

        if (EditorGUI.EndChangeCheck())
        {
            property.FindPropertyRelative("min").floatValue = values[0];
            property.FindPropertyRelative("max").floatValue = values[1];
        }
    }
}
