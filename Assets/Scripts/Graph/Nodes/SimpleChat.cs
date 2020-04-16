using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#FFA500")]
public class SimpleChat : OceanNode
{
    [TextArea(5,100)] public string message;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);
        PlayerController.instance.LockPlayer(true);
        AddMessage(message);
	}

	public override void NextNode()
	{
        PlayerController.instance.LockPlayer(false);
        base.NextNode();
	}
}
