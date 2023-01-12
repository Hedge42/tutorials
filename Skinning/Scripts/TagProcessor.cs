using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

public abstract class TagProcessor : ScriptableObject
{
    public const string MENU = "Tags/Processors/";
    public const int ORDER = -2;

    public ScriptableTag[] tags;

    public abstract void Process(TagContainer[] containers, ref List<Object> undoList, ref List<Action> actions);
}
