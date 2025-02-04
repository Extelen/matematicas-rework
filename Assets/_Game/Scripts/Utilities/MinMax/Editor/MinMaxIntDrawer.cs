using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMaxInt))]
public class MinMaxIntDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        Rect minRect = new Rect(position.x, position.y, 30, position.height);
        Rect maxRect = new Rect(position.x + 35, position.y, 50, position.height);

        int min = property.FindPropertyRelative("min").intValue;
        int max = property.FindPropertyRelative("max").intValue;

        GUIContent[] labels = new GUIContent[2] { new GUIContent("Min"), new GUIContent("Max") };
        int[] values = new int[2] { min, max };

        EditorGUI.BeginChangeCheck();

        EditorGUI.MultiIntField(position, labels, values);

        if (EditorGUI.EndChangeCheck())
        {
            property.FindPropertyRelative("min").intValue = values[0];
            property.FindPropertyRelative("max").intValue = values[1];
        }
    }
}
