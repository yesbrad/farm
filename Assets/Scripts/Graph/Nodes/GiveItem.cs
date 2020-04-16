using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#FF00FF")]
public class GiveItem : OceanNode
{
    public Item item;
    public int amount;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        if (item != null)
        {
            for (int i = 0; i < amount; i++)
            {
				Inventory.instance.AddItem(item);
            }
        }
        else
            Debug.Log("Missing Item From Node: " + id);
        
        NextNode();
    }

	public override void NextNode()
	{
        base.NextNode();
	}
}
