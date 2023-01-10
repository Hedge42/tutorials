using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;

public abstract class PropertyLinker<T> : MonoBehaviour
{
    public Object objectReference;
    public string propertyName;

    protected PropertyInfo propertyInfo;

    public UnityEvent<T> onReadValue;

    public T Value
    {
        get => (T)propertyInfo.GetValue(objectReference);
        set
        {
            propertyInfo.SetValue(objectReference, value);

            ReadValue();
        }
    }

    private void Awake()
    {
        propertyInfo = objectReference.GetType().GetProperty(propertyName);
    }

    private void OnEnable()
    {
        ReadValue();
    }

    public virtual void ReadValue()
    {
        onReadValue?.Invoke(Value);
    }
}
