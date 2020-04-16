using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#FFFF00")]
public class TogglePlayer : OceanNode
{
    public bool lockPlayer;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        PlayerController.instance.LockPlayer(lockPlayer);
        
        base.NextNode();
    }
}
