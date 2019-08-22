using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#FF0F55")]
public class HasItem : OceanNodeCheck
{
    public Item item;
    public int amount;

    public override void Use()
    {
        base.Use();
        NextNode(Inventory.instance.HasItem(item , amount));
    }
}
