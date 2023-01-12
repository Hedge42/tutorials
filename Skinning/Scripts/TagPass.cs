using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(TagPass))]
public class TagPassEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Apply to scene"))
        {
            (target as TagPass).Apply();
        }
    }
}
#endif

[CreateAssetMenu(menuName ="Tags/Pass")]
public class TagPass : ScriptableObject
{
    public TagProcessor[] processors;

    public void Apply()
    {
        var tagObjects = GameObject.FindObjectsOfType<TagContainer>();

        var actions = new List<Action>();
        var undoList = new List<Object>();

        foreach (var p in processors)
            p.Process(tagObjects, ref undoList, ref actions);

#if UNITY_EDITOR
        Undo.RecordObjects(undoList.ToArray(), $"Applying tag pass '{name}'");
#endif

        foreach (var action in actions)
            action();

#if UNITY_EDITOR
        foreach (var obj in undoList)
            EditorUtility.SetDirty(obj);
#endif

        Debug.Log($"Applied {name} to {undoList.Count} objects...\n{string.Join('\n', undoList)}");
    }
}
