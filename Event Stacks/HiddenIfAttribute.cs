using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(HiddenIfAttribute), true)]
public class HiddenIfDrawer : PropertyDrawer
{
    private HiddenIfAttribute _hiddenAttribute;
    private HiddenIfAttribute hiddenAttribute => _hiddenAttribute == null
        ? _hiddenAttribute = attribute as HiddenIfAttribute
        : _hiddenAttribute;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var conditionalProperty = property.serializedObject.FindProperty(hiddenAttribute.propertyName);

        if (IsHidden(conditionalProperty))
            return;

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(position, property, label);

        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var conditionalProperty = property.serializedObject.FindProperty(hiddenAttribute.propertyName);

        if (IsHidden(conditionalProperty))
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
        else
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
    }

    private bool IsHidden(SerializedProperty conditionalProperty)
    {
        bool result;

        if (hiddenAttribute.enumValue == null) // assume boolean
        {
            result = conditionalProperty.boolValue != attribute is VisibleIfAttribute;
        }
        else
        {
            bool hasFlags = hiddenAttribute.enumValue.GetType().GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;

            if (hasFlags)
            {
                int conditionalFlags = conditionalProperty.enumValueFlag;
                int attributeFlags = Convert.ToInt32(hiddenAttribute.enumValue);

                result = (attributeFlags & conditionalFlags) == attributeFlags;
            }
            else // standard enum
            {
                var values = hiddenAttribute.enumValue.GetType().GetEnumValues();
                int index = -1;
                for (int i = 0; i < values.Length; i++)
                {
                    if (hiddenAttribute.enumValue.Equals((Enum)values.GetValue(i)))
                    {
                        index = i;
                        break;
                    }
                }

                result = conditionalProperty.enumValueIndex == index;
            }
        }

        return result != attribute is VisibleIfAttribute;
    }
}
#endif

[AttributeUsage(AttributeTargets.Field)]
public class HiddenIfAttribute : PropertyAttribute
{
    public string propertyName;
    public Enum enumValue;
    public HiddenIfAttribute(string _propertyName)
    {
        propertyName = _propertyName;
    }

    public HiddenIfAttribute(string _propertyName, object _enumValue)
    {
        propertyName = _propertyName;

        if (_enumValue is Enum)
        {
            enumValue = (Enum)_enumValue;
        }
        else
        {
            throw new System.ArgumentException();
        }
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class VisibleIfAttribute : HiddenIfAttribute
{
    public VisibleIfAttribute(string _propertyName) : base(_propertyName)
    {
    }

    public VisibleIfAttribute(string _propertyName, object _enumValue) : base(_propertyName, _enumValue)
    {
    }
}