using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#00F0FF")]
public class TakeItem : OceanNode
{
    public Item item;
    public int amount;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        if (item != null)
        {
	        if (Inventory.instance.HasItem(item, amount))
	        {
		        for (int i = 0; i < amount; i++)
	            {
					Inventory.instance.RemoveItem(item);
	            }
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
