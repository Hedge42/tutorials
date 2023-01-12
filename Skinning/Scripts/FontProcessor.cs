using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = MENU + "Font", order = ORDER)]
public class FontProcessor : TagProcessor
{
    public TMP_FontAsset font;

    public override void Process(TagContainer[] containers, ref List<Object> undoList, ref List<Action> action)
    {
        foreach (var container in containers)
        {
            if (container.ContainsAny(tags))
            {
                var tmp = container.GetComponent<TextMeshProUGUI>();
                if (tmp != null)
                {
                    undoList.Add(tmp);
                    action.Add(() => tmp.font = font);
                }
            }
        }
    }
}
