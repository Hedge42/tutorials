using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = MENU + "Sprite", order = ORDER)]
public class SpriteProcessor : TagProcessor
{
    public Sprite sprite;
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
                    actions.Add(() => img.sprite = sprite);
                }
            }
        }
    }
}
