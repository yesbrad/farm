using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#e66f40")]
public class HasCrop : OceanNode
{
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode hasCrop;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode noCrop;

    public CropItem item;

    public override void Use(Interactable interactable)
    {
        base.Use(interactable);

        if (CropManager.instance.HasGrowingCrop(item) || Inventory.instance.HasItem(item))
        {
            NextNode("hasCrop");
        }
        else
        {
            NextNode("noCrop");   
        }
    }
}
