using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#FFA500")]
public class SimpleChat : OceanNode
{
    [TextArea(5,100)] public string message;

	public override void Use()
	{
        PlayerController.instance.LockPlayer(true);
        base.Use();
        AddMessage(message);
	}

	public override void NextNode()
	{
        PlayerController.instance.LockPlayer(false);
        base.NextNode();
	}
}
