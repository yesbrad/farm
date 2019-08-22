using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#FFFF00")]
public class TogglePlayer : OceanNode
{
    public bool lockPlayer;

	public override void Use()
	{
        base.Use();

        PlayerController.instance.LockPlayer(lockPlayer);
        
        NextNode();
    }

	public override void NextNode()
	{
        base.NextNode();
	}
}
