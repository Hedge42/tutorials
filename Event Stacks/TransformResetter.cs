using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransformResetter : MonoBehaviour
{
    [Flags]
    public enum ValueType
    {
        Position = 1, // 0001
        Rotation = 2, // 0010
        Scale = 4,    // 0100
    }
    public ValueType valueType;

    [VisibleIf(nameof(valueType), ValueType.Position)]
    public Vector3 position;
    [VisibleIf(nameof(valueType), ValueType.Rotation)]
    public Vector3 rotation;
    [VisibleIf(nameof(valueType), ValueType.Scale)]
    public Vector3 scale;

    public void ResetTransform()
    {
        if (valueType.HasFlag(ValueType.Position))
            transform.localPosition = position;
        if (valueType.HasFlag(ValueType.Scale))
            transform.localScale = scale;
        if (valueType.HasFlag(ValueType.Rotation))
            transform.localEulerAngles = rotation;
    }
}
