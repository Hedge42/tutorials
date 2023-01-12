using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TagContainer : MonoBehaviour
{
    public ScriptableTag[] tags;

    public bool ContainsAny(ScriptableTag[] other)
    {
        return other.Intersect(tags).Count() > 0;
    }
}
