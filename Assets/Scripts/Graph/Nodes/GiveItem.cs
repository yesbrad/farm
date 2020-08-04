using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#FF00FF")]
public class GiveItem : OceanNode
{
    public Item item;
    public int amount;
    public bool autoMessage = true;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        if (item != null)
        {
	        for (int i = 0; i < amount; i++)
	        {
		        Inventory.instance.AddItem(item);
	        }

	        if (autoMessage)
	        {
		        AudioController.instance.Play(AudioController.instance.defaultItemGiveSound);
		        UI_Speech.instance.AddMessage("You recieved " + amount + " " + item.name, (() => { base.NextNode(); }));
	        }
	        else
	        {
		        base.NextNode();
	        }
        }
        else
        {
	        Debug.Log("Missing Item From Node: " + id);
			base.NextNode();
        }
        
    }
}
