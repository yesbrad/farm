using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#000000")]
public class SaveNode : OceanNode
{
	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        SaveManager.Save();
        Debug.Log("SAVED FROM NODE");

        base.NextNode();
    }
}
