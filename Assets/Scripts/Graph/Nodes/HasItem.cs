using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#FF0F55")]
public class HasItem : OceanNode
{
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode output1;

    public Item item;
    public int amount;

    public override void Use(Interactable interactable)
    {
        base.Use(interactable);
        NextNode(Inventory.instance.HasItem(item , amount) ? 0 : 1);
    }
}
