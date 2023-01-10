using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

[CustomPropertyDrawer(typeof(EventDrawerAttribute))]
public class EventAttributeDrawer : PropertyDrawer
{
    private UnityEventDrawer _drawer;
    private UnityEventDrawer drawer => _drawer == null 
        ? _drawer = new UnityEventDrawer() : _drawer;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();

        drawer.OnGUI(position, property, label);

        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return drawer.GetPropertyHeight(property, label);
    }
}
#endif

[AttributeUsage(AttributeTargets.Field)]
public class EventDrawerAttribute : PropertyAttribute
{
    public EventDrawerAttribute()
    {
        order = 69;
    }
}
