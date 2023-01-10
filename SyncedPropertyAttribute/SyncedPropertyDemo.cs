using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedPropertyDemo : MonoBehaviour
{
    // works, but... why?
    [field: SerializeField]
    public float imAProperty { get; set; }

    // does not work :(
    private float _f;
    // [field: SerializeField]
    public float imASadProperty
    {
        get => _f;
        set
        {
            // validation...
            // onValueChanged...
            // update dependencies...
            // etc
            _f = value;
        }
    }

    [SerializeField, SyncedProperty(nameof(syncedProperty))]
    private float syncedField;
    public float syncedProperty
    {
        get => syncedField;
        set
        {
            syncedField = Mathf.Clamp(value, -10f, 10f);
            Debug.Log("Property changed!");
        }
    }

    [SerializeField, SyncedProperty(nameof(staticProperty))]
    private float staticField;
    private const string KEY = "idk LOOOL";
    public static float staticProperty
    {
        get => PlayerPrefs.GetFloat(KEY);
        set => PlayerPrefs.SetFloat(KEY, Mathf.Clamp01(value));
    }
}
