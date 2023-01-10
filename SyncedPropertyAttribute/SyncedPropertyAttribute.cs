using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
[CustomPropertyDrawer(typeof(SyncedPropertyAttribute))]
public class SyncedPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var target = property.serializedObject.targetObject;

        var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Static;
        var propertyInfo = target.GetType().GetProperty((attribute as SyncedPropertyAttribute).propertyInfoName, flags);

        var isValid = propertyInfo != null && propertyInfo.PropertyType.Equals(fieldInfo.FieldType);

        if (isValid)
        {
            fieldInfo.SetValue(target, propertyInfo.GetValue(target));
            property.serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.BeginChangeCheck();

        EditorGUI.PropertyField(position, property, label);

        if (!isValid)
        {
            var rect = GUILayoutUtility.GetRect(position.width, 30);
            EditorGUI.HelpBox(rect, "Invalid property", MessageType.Error);
        }

        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties();

            if (isValid)
                propertyInfo.SetValue(target, fieldInfo.GetValue(target));

            EditorUtility.SetDirty(target);
        }
    }
}
#endif

[AttributeUsage(AttributeTargets.Field)]
public class SyncedPropertyAttribute : PropertyAttribute
{
    public string propertyInfoName;

    public SyncedPropertyAttribute(string propertyInfoName)
    {
        this.propertyInfoName = propertyInfoName;
    }
}
