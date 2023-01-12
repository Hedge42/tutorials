using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = MENU + "Selectable", order = ORDER)]
public class SelectableProcessor : TagProcessor
{
    public Color normalColor;
    public Color highlightColor;
    public Color pressedColor;
    public Color selectedColor;
    public Color disabledColor;

    public override void Process(TagContainer[] containers, ref List<Object> undoList, ref List<Action> actions)
    {
        foreach (var container in containers)
        {
            if (!container.ContainsAny(tags))
                continue;

            var selectable = container.GetComponent<Selectable>();

            if (selectable != null)
            {
                undoList.Add(selectable);

                actions.Add(() =>
                {
                    var block = selectable.colors;

                    block.normalColor = normalColor;
                    block.highlightedColor = highlightColor;
                    block.pressedColor = pressedColor;
                    block.selectedColor = selectedColor;
                    block.disabledColor = disabledColor;

                    selectable.colors = block;
                });
            }
        }
    }
}
