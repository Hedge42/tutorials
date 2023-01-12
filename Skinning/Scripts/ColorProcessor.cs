using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = MENU + "Color", order =ORDER)]
public class ColorProcessor : TagProcessor
{
    public Color color;
    public override void Process(TagContainer[] containers, ref List<Object> undoList, ref List<Action> actions)
    {
        foreach (var container in containers)
        {
            if (container.ContainsAny(tags))
            {
                var img = container.GetComponent<Image>();
                if (img != null)
                {
                    undoList.Add(img);
                    actions.Add(() => img.color = color);
                    continue;
                }

                var tmp = container.GetComponent<TextMeshProUGUI>();
                if (tmp != null)
                {
                    undoList.Add(tmp);
                    actions.Add(()=> tmp.color = color);
                }
            }
        }
    }
}
