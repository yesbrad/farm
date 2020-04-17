using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#FF0F55")]
public class HasItem : OceanNode
{
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode hasItem;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode noItem;

    public Item item;
    public int amount;

    public override void Use(Interactable interactable)
    {
        base.Use(interactable);
        NextNode(Inventory.instance.HasItem(item , amount) ? "hasItem" : "noItem");
    }
}
